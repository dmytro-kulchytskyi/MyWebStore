using MyStore.Business.Managers;
using MyStore.Business.Search.Managers;
using MyStore.Mvc.Models.AdminViewModels;
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchSettings()
        {
            var model = new SearchSettingsViewModel
            {
                IndexStatus = productSearchManager.IndexStatus,
                IndexProgress = productSearchManager.IndexProgress,
                ErrorMessage = productSearchManager.IndexError
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSearchIndex()
        {
            if (productSearchManager.IndexStatus != IndexStatus.InProgress)
                productSearchManager.CreateSearchIndex();

            return RedirectToAction("SearchSettings");
        }
    }
}