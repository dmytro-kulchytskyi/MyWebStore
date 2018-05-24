using AutoMapper;
using MyStore.Business.Managers;
using MyStore.Business.Search;
using MyStore.Business.Search.Managers;
using MyStore.Mvc.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: Product
        public ActionResult List()
        {
            return View();
        }

        public ActionResult ProductsPage(DateTime? startDate = null)
        {
            if (!Request.IsAjaxRequest() && !ControllerContext.IsChildAction)
                return HttpNotFound();

            var count = int.Parse(WebConfigurationManager.AppSettings["ProductsOnPage"]);
            var products = productManager.GetSegmentOrderedByByDate(count, startDate ?? DateTime.MaxValue);

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(products);
            return PartialView("ProductsPagePartial", model);
        }

        [ChildActionOnly]
        public ActionResult TopProducts()
        {
            var count = int.Parse(WebConfigurationManager.AppSettings["TopProductsCount"]);
            var products = productManager.GetTopBySellingCount(count);

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(products);
            return PartialView("TopProductsPartial", model);
        }

        [HttpGet]
        public ActionResult Details(string externalProductId)
        {
            if (string.IsNullOrEmpty(externalProductId))
                return HttpNotFound();

            var product = productManager.GetByExternalProductId(externalProductId);
            if (product == null)
                return HttpNotFound();

            var model = Mapper.Map<ProductDetailsViewModel>(product);
            return View(model);
        }


        public ActionResult Search(string query)
        {
            if (!Request.IsAjaxRequest() && !ControllerContext.IsChildAction)
                return HttpNotFound();

          
            var products = productSearchManager.Search(query, ProductSearchFields.AllExcept(ProductSearchFields.Description), AppConfiguartion.SearchResultsCount);

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(products);
            return PartialView("SearchResultsPartial", model);
        }
    }
}