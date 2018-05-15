using DimasikMagazik.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DimasikMagazik.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager productManager;

        public ProductController(ProductManager productManager)
        {
            this.productManager = productManager;
        }

        // GET: Product
        public ActionResult List()
        {
            return View();
        }

        public async Task<ActionResult> ProductsPage(DateTime? startDate = null)
        {
            var count = int.Parse(WebConfigurationManager.AppSettings["ProductsOnPage"]);

            var products = await productManager.GetSegmentOrderedByByDate(count, startDate ?? DateTime.Now);

            return PartialView(products);
        }
    }
}