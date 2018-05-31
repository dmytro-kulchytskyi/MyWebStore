using AutoMapper;
using MyStore.Business;
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
            model.AvailableOrderFields = AppConfiguration.AvailableProductSortFields;

            if (string.IsNullOrEmpty(model.OrderField))
                model.OrderField = AppConfiguration.DefaulOrderField;

            if (!model.AvailableOrderFields.Contains(model.OrderField))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
            var url = Request.Url;

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
                MaxResults = AppConfiguration.SearchResultsCount
            });

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(products);
            return PartialView("SearchResultsPartial", model);
        }

        [HttpGet]
        public ActionResult SearchPage(ProductSearchListViewModel model)
        {
            if (string.IsNullOrEmpty(model.Query))
                return RedirectToAction("List");

            var availableOrderFields = AppConfiguration.AvailableProductSortFields.ToList();
            availableOrderFields.Insert(0, "");

            model.AvailableOrderFields = availableOrderFields.ToArray();

            model.ResultsCount = model.ResultsCount != 0 ? model.ResultsCount : AppConfiguration.SearchPageMaxItemCount;

            var products = productSearchManager.Search(new SearchOptions
            {
                Query = model.Query,
                ResultFields = ProductFields.AllExcept(ProductFields.Description),
                MaxResults = model.ResultsCount,
                InverseOrder = model.InverseOrder,
                SortField = model.OrderField,
                SearchFields = model.SearchFields
            });
            model.Items = Mapper.Map<IList<ProductListItemViewModel>>(products);
            return View(model);
        }
    }
}