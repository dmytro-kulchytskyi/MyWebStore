using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Providers
{
    public interface IBasketProvider : IProvider<Basket>
    {
        Basket GetBasket(string userId);
        BasketProduct GetBasketProduct(string userId, string productId);
        void AddToBasket(string userId, string productId, int count = 1);
        void RemoveFromBasket(string userId, string productId);
        void ChangeProductCount(string userId, string productId, int newCount);
        bool IsInBasket(string userId, string productId);
    }
}
