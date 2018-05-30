using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Providers
{
    public interface IAddressProvider : IProvider<Address>
    {
        IList<Country> GetAvailableCountries();
        IList<Address> GetByUserId(string userId);
    }
}
