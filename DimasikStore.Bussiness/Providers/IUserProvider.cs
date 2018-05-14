using DimasikStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikStore.Business.Providers
{
    public interface IUserProvider : IProvider<AppUser>
    {
        Task<AppUser> GetByEmail(string email);
    }
}
