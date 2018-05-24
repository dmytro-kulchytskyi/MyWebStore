using MyStore.Business.Entities;
using MyStore.Business.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Managers
{
    public class UserManager : Manager<AppUser, IUserProvider>
    {
        public UserManager(IUserProvider provider) : base(provider)
        {
        }

        public AppUser GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException($"{nameof(email)} can't be empty");

            return provider.GetByEmail(email);
        }

        public void CreateUser(AppUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            provider.Save(user);
        }

        public void UpdateUser(AppUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(user.Id))
                throw new ArgumentException("User Id can't be empty");

            provider.Update(user);
        }
        
    }
}
