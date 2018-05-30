using MyStore.Business.Entities;
using MyStore.Business.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Managers
{
    public class AddressManager : Manager<Address, IAddressProvider>
    {
        public AddressManager(IAddressProvider provider) : base(provider)
        {
        }

        public IList<Country> GetAvailableCountries()
        {
            return provider.GetAvailableCountries();
        }

        public IList<Address> GetByUserId(string userId)
        {
            return provider.GetByUserId(userId);
        }
    }
}
