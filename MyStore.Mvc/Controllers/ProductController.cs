using AutoMapper;
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
        public ActionResult List([ModelBinder(typeof(ProductsListViewModelBinder))] ProductsListViewModel model)
        {
            var availableOrderFields = AppConfiguration.AvailableProductSortFields.ToList();

            model.PageNumber = model.PageNumber <= 0 ? 0 : model.PageNumber;
            model.PageSize = model.PageSize <= 0 ? AppConfiguration.DefaultPageSize : model.PageSize;
        
            IList<ProductListItemViewModel> products;

            if (!string.IsNullOrEmpty(model.Query))
            {
                availableOrderFields.Insert(0, "");

                var searchResults = productSearchManager.Search(new SearchOptions
                {
                    Query = model.Query,
                    PageNumber = model.PageNumber,
                    ResultFields = ProductFields.AllExcept(ProductFields.Description),
                    PageSize = model.PageSize,
                    InverseOrder = model.InverseOrder,
                    SortField = model.OrderField
                });
                model.DefaultOrderField = "";
                model.TotalItemsCount = searchResults.TotalCount;
                products = Mapper.Map<IList<ProductListItemViewModel>>(searchResults.Items);
            }
            else
            {
                if (string.IsNullOrEmpty(model.OrderField))
                    model.OrderField = AppConfiguration.DefaulOrderField;


                var results = productManager.GetPageOrderedBy(
                    model.OrderField,
                    model.InverseOrder,
                    model.PageSize,
                    model.PageNumber);

                model.DefaultOrderField = AppConfiguration.DefaulOrderField;
                model.TotalItemsCount = results.TotalCount;
                products = Mapper.Map<IList<ProductListItemViewModel>>(results.Items);
            }

            model.AvailableOrderFields = availableOrderFields.ToArray();
            model.Items = products;

            return View(model);
        }

        public ActionResult ProductsPage(ProductsListViewModel model)
        {
            if (!Request.IsAjaxRequest() && !ControllerContext.IsChildAction)
                return HttpNotFound();

            if (string.IsNullOrEmpty(model.OrderField))
                model.OrderField = AppConfiguration.DefaulOrderField;

            model.PageNumber = model.PageNumber <= 0 ? 0 : model.PageNumber;
            model.PageSize = model.PageSize <= 0 ? AppConfiguration.DefaultPageSize : model.PageSize;

            if (!AppConfiguration.AvailableProductSortFields.Contains(model.OrderField))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var products = productManager.GetPageOrderedBy(model.OrderField, model.InverseOrder, model.PageSize, model.PageNumber);
            model.Items = Mapper.Map<IList<ProductListItemViewModel>>(products);

            return PartialView("ProductsPagePartial", model);
        }

        [ChildActionOnly]
        public ActionResult TopProducts()
        {
            var count = int.Parse(WebConfigurationManager.AppSettings["TopProductsCount"]);
            var products = productManager.GetTop(ProductFields.SellsCount, count);

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(products);
            return PartialView("TopProductsPartial", model);
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
                return HttpNotFound();

            var products = productSearchManager.Search(new SearchOptions
            {
                Query = query,
                ResultFields = ProductFields.AllExcept(ProductFields.Description),
                PageSize = AppConfiguration.SearchResultsCount
            });

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(products.Items);
            return PartialView("SearchResultsPartial", model);
        }

    }
}