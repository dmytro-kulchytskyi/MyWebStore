using MyStore.Business.Managers;
using MyStore.Business.Search.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ProductSearchManager productSearchManager;

        public AdminController(ProductSearchManager productSearchManager)
        {
            this.productSearchManager = productSearchManager;
        }
        
        public ActionResult CreateSearchIndex()
        {
            productSearchManager.CreateSearchIndex();

            return View();
        }
    }
}