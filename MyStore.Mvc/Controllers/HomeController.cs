using AutoMapper;
using MyStore.Business.Managers;
using MyStore.Mvc.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductManager productManager;

        public HomeController(ProductManager productManager)
        {
            this.productManager = productManager;
        }

        public async Task<ActionResult> Index()
        {
            var topProducts = await productManager.GetTopBySellingCount(int.Parse(WebConfigurationManager.AppSettings["TopProductsCount"]));

            var model = Mapper.Map<IEnumerable<ProductListItemViewModel>>(topProducts);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}