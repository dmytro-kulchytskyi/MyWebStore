using MyStore.Business.Entities;
using MyStore.Business.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Managers
{
    public class BasketManager : Manager<Basket, IBasketProvider>
    {
        public BasketManager(IBasketProvider provider) : base(provider)
        {
        }

        public Basket GetBasket(string userId)
        {
            return provider.GetBasket(userId);
        }

        public void AddToBasket(string userId, string productId, int count = 1)
        {
            provider.AddToBasket(userId, productId, count);
        }

        public void RemoveFromBasket(string userId, string productId)
        {
            provider.RemoveFromBasket(userId, productId);
        }

        public void ChangeProductCount(string userId, string productId, int newCount)
        {
            provider.ChangeProductCount(userId, productId, newCount);
        }

        public bool IsInBasket(string userId, string productId)
        {
            return provider.IsInBasket(userId, productId);
        }

        public BasketProduct GetBasketProduct(string userId, string productId)
        {
            return provider.GetBasketProduct(userId, productId);
        }
    }
}
