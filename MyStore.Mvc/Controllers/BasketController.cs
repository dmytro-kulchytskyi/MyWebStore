using AutoMapper;
using Microsoft.AspNet.Identity;
using MyStore.Business.Managers;
using MyStore.Mvc.Models.BasketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Mvc.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ProductManager productManager;

        private readonly BasketManager basketManager;

        public BasketController(ProductManager productManager, BasketManager basketManager)
        {
            this.productManager = productManager;
            this.basketManager = basketManager;
        }

        public ActionResult BasketPage()
        {
            var basket = basketManager.GetBasket(User.Identity.GetUserId());

            var products = productManager.GetById(basket.Products.Select(it => it.ProductId));

            var basketProductViewModelsList = products.Select(it =>
            {
                var basketProductViewModel = Mapper.Map<BasketProductViewModel>(it);
                basketProductViewModel.Count = basket.Products.FirstOrDefault(p => p.ProductId == basketProductViewModel.Id).Count;

                return basketProductViewModel;
            });

            var basketPrice = 0m;

            foreach (var p in basketProductViewModelsList)
                basketPrice += p.Price * p.Count;

            var model = new BasketViewModel()
            {
                Products = basketProductViewModelsList.ToList(),
                TotalPrice = basketPrice
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string productId)
        {
            if (!Request.IsAjaxRequest())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(productId))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = productManager.GetById(productId);
            if (product == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (product.Stock == 0)
                return Json(new AddToBasketResultViewModel
                {
                    Success = false,
                    ProductId = productId,
                    Message = "Product is out of stock"
                }, JsonRequestBehavior.AllowGet);

            basketManager.AddToBasket(User.Identity.GetUserId(), productId);

            return Json(new AddToBasketResultViewModel
            {
                Success = true,
                ProductId = productId,
                Message = "Added to basket"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string productId, string returnUrl)
        {
            if (string.IsNullOrEmpty(productId))
                throw new ArgumentException(nameof(productId) + " required");

            if (!basketManager.IsInBasket(User.Identity.GetUserId(), productId))
                throw new InvalidOperationException("Product not in basket");

            basketManager.RemoveFromBasket(User.Identity.GetUserId(), productId);

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("BasketPage");
        }
    }
}