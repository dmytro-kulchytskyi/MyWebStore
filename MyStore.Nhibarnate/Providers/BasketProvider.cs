using MyStore.Business.Entities;
using MyStore.Business.Providers;
using MyStore.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate.Providers
{
    public class BasketProvider : BusinessEntityProvider<Basket>, IBasketProvider
    {
        public BasketProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory)
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public override Basket GetById(string id)
        {
            return providerHelper.Invoke(s =>
            {
                var basket = base.GetById(id);

                if (basket != null)
                    basket.Products = GetBasketProducts(basket.Id);

                return basket;
            });
        }

        private IList<BasketProduct> GetBasketProducts(string basketId)
        {
            return providerHelper.Invoke(s => s.QueryOver<BasketProduct>().Where(it => it.BasketId == basketId).List());
        }

        public Basket GetBasket(string userId)
        {
            return providerHelper.Invoke(s =>
            {
                var basket = s.QueryOver<Basket>().Where(it => it.UserId == userId).SingleOrDefault();

                if (basket != null)
                    basket.Products = GetBasketProducts(basket.Id);

                return basket;
            });
        }

        public void AddToBasket(string userId, string productId, int count = 1)
        {
            if (count < 1)
                throw new ArgumentException($"{nameof(count)} must be positive number");

            providerHelper.Invoke(s =>
            {
                using (var uow = GetUnitOfWork())
                {
                    uow.BeginTransaction();

                    var basket = GetBasket(userId);

                    if (basket == null)
                    {
                        basket = new Basket
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = userId
                        };

                        s.Save(basket);
                    }

                    var basketProduct = s.QueryOver<BasketProduct>().Where(it => it.ProductId == productId).SingleOrDefault() ?? new BasketProduct
                    {
                        BasketId = basket.Id,
                        Id = Guid.NewGuid().ToString(),
                        ProductId = productId
                    };
                    basketProduct.Count += count;

                    s.SaveOrUpdate(basketProduct);

                    uow.Commit();
                }
            });
        }

        public void RemoveFromBasket(string userId, string productId)
        {
            providerHelper.Invoke(s =>
            {
                using (var uow = GetUnitOfWork())
                {
                    uow.BeginTransaction();

                    var basket = s.QueryOver<Basket>().Where(it => it.UserId == userId).SingleOrDefault() ??
                        throw new InvalidOperationException("No basket witch given id");

                    var basketProduct = s.QueryOver<BasketProduct>().Where(it => it.BasketId == basket.Id && it.ProductId == productId).SingleOrDefault();

                    s.Delete(basketProduct);

                    uow.Commit();
                }
            });
        }

        public void ChangeProductCount(string userId, string productId, int newCount)
        {
            if (newCount < 1)
                throw new ArgumentException($"{nameof(newCount)} must be positive number");

            providerHelper.Invoke(s =>
            {
                using (var uow = GetUnitOfWork())
                {
                    uow.BeginTransaction();

                    var basket = s.QueryOver<Basket>().Where(it => it.UserId == userId).SingleOrDefault() ??
                        throw new InvalidOperationException("No basket witch given id");

                    var basketProduct = s.QueryOver<BasketProduct>().Where(it => it.BasketId == basket.Id).SingleOrDefault();

                    basketProduct.Count = newCount;

                    s.Update(basketProduct);

                    uow.Commit();
                }
            });
        }

        public bool IsInBasket(string userId, string productId)
        {
            return providerHelper.Invoke(s =>
            {
                var basket = s.QueryOver<Basket>().Where(it => it.UserId == userId).SingleOrDefault();
                if (basket == null)
                    return false;

                return s.QueryOver<BasketProduct>().Where(it => it.ProductId == productId && it.BasketId == basket.Id).SingleOrDefault() != null;
            });
        }

        public BasketProduct GetBasketProduct(string userId, string productId)
        {
            return providerHelper.Invoke(s =>
            {
                var basket = s.QueryOver<Basket>().Where(it => it.UserId == userId).SingleOrDefault();

                if (basket == null)
                    return null;

                return s.QueryOver<BasketProduct>().Where(it => it.BasketId == basket.Id && it.ProductId == productId).SingleOrDefault();
            });
        }
    }
}
