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

        private void InitializeProductsListViewModel(ProductsListViewModel model, int totalCount, IEnumerable<string> availableSortFields, IList<ProductListItemViewModel> products)
        {
            model.TotalItemsCount = totalCount;
            model.AvailableSortFields = availableSortFields.ToArray();
            model.Items = products;
        }

        [HttpGet]
        public ActionResult List(ProductsListViewModel model)
        {
            var availableSortFields = AppConfiguration.ProductsListAvailableOrderTypes.ToList();

            if (string.IsNullOrEmpty(model.SortField))
                model.SortField = availableSortFields.First();

            var results = productManager.GetPageSortedBy(
                model.SortField,
                model.InverseSort,
                model.PageSize,
                model.PageNumber);

            var products = Mapper.Map<IList<ProductListItemViewModel>>(results.Items);

            InitializeProductsListViewModel(model, results.TotalCount, availableSortFields, products);

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

            var products = Mapper.Map<IList<ProductListItemViewModel>>(searchResults.Items);

            var availableSortFields = AppConfiguration.ProductsListAvailableOrderTypes.ToList();
            availableSortFields.Add("");

            InitializeProductsListViewModel(model, searchResults.TotalCount, availableSortFields, products);

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