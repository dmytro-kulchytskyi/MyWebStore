using DimasikStore.Business.Entities;
using DimasikStore.Business.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikStore.Business.Managers
{
    public class UserManager : Manager<AppUser, IUserProvider>
    {
        public UserManager(IUserProvider provider) : base(provider)
        {
        }

        public Task<AppUser> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException(nameof(email));

            return provider.GetByEmail(email);
        }

        public async Task CreateUser(AppUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Banned = false;

            await provider.Save(user);
        }

        public async Task UpdateUser(AppUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(user.Id))
                throw new ArgumentException(nameof(user) + nameof(user.Id));

            await provider.Update(user);
        }
    }
}
