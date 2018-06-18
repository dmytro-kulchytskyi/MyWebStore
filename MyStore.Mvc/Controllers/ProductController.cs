using AutoMapper;
using Microsoft.AspNet.Identity;
using MyStore.Business;
using MyStore.Business.Entities;
using MyStore.Business.Managers;
using MyStore.Business.Search;
using MyStore.Business.Search.Managers;
using MyStore.Mvc.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MyStore.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager productManager;

        private readonly ProductSearchManager productSearchManager;

        public ProductController(ProductManager productManager, ProductSearchManager productSearchManager)
        {
            this.productManager = productManager;
            this.productSearchManager = productSearchManager;
        }


        [HttpGet]
        public ActionResult List(ProductsListViewModel model)
        {
            var sortField = model.SortField;
            var inverseSort = model.InverseSort;
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = AppConfiguration.DefaultSortField;
                inverseSort = AppConfiguration.InverseDefaultSortField;
            }

            if (!ProductFields.All.Contains(sortField))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var results = productManager.GetPageSortedBy(
                sortField,
                inverseSort,
                model.PageSize,
                model.PageNumber);

            if (model.SortField == AppConfiguration.DefaultSortField)
                model.SortField = string.Empty;

            model.TotalItemsCount = results.TotalCount;
            model.Items = Mapper.Map<IList<ProductListItemViewModel>>(results.Items);
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchPage(ProductsListViewModel model)
        {
            var searchResults = productSearchManager.Search(new SearchOptions
            {
                Query = model.Query,
                PageNumber = model.PageNumber,
                ResultFields = ProductFields.AllExcept(ProductFields.Description),
                PageSize = model.PageSize,
                InverseSort = model.InverseSort,
                SortField = model.SortField
            });

            model.RelevantSortTypeAvailable = true;

            model.Items = Mapper.Map<IList<ProductListItemViewModel>>(searchResults.Items);
            model.TotalItemsCount = searchResults.TotalCount;
            return View("List", model);
        }

        [ChildActionOnly]
        public ActionResult TopProducts()
        {
            var count = AppConfiguration.TopProductsCount;
            var products = productManager.GetTop(ProductFields.SellsCount, count);

            var model = Mapper.Map<IList<ProductListItemViewModel>>(products);

            return PartialView("_TopProducts", model);
        }

        [HttpGet]
        public ActionResult Details(string id, string returnUrl)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();

            var product = productManager.GetById(id);
            if (product == null)
                return HttpNotFound();

            var model = Mapper.Map<ProductDetailsViewModel>(product);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            if (!Request.IsAjaxRequest())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var products = productSearchManager.Search(new SearchOptions
            {
                Query = query,
                ResultFields = ProductFields.AllExcept(ProductFields.Description),
                PageSize = AppConfiguration.SearchDropdownItemsLimit
            });

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(products.Items);
            return PartialView("_SearchResults", model);
        }
    }
}