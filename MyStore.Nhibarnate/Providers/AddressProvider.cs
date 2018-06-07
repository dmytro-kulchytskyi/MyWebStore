using AutoMapper;
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
    public class AddressProvider : BusinessEntityProvider<Address>, IAddressProvider
    {
        public AddressProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory)
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public IList<Country> GetAvailableCountries()
        {
            return providerHelper.Invoke(s => s.QueryOver<Country>().Where(it => it.Available).List());
        }

        public IList<Address> GetByUserId(string userId)
        {
            return providerHelper.Invoke(s => s.QueryOver<Address>().Where(it => it.UserId == userId).List());
        }
    }
}
