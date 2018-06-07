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
        private readonly ProductISearchIndexManager productSearchIndexManager;

        public AdminController(ProductISearchIndexManager productSearchManager)
        {
            this.productSearchIndexManager = productSearchManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchSettings()
        {
            var model = new SearchSettingsViewModel
            {
                IndexStatus = productSearchIndexManager.IndexStatus,
                IndexProgress = productSearchIndexManager.IndexProgress,
                ErrorMessage = productSearchIndexManager.IndexError
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSearchIndex()
        {
            if (productSearchIndexManager.IndexStatus != IndexStatus.InProgress)
                productSearchIndexManager.CreateSearchIndex();

            return RedirectToAction("SearchSettings");
        }
    }
}