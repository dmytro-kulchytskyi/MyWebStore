using MyStore.Business.Managers;
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
        private readonly ProductManager productManager;

        public AdminController(ProductManager productManager)
        {
            this.productManager = productManager;
        }
        
        public ActionResult CreateSearchIndex()
        {
            productManager.CreateSearchIndex();

            return View();
        }
    }
}