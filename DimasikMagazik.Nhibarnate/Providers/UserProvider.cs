using DimasikMagazik.Business.Entities;
using DimasikMagazik.Business.Providers;
using DimasikMagazik.Nhibernate.Wrappers.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimasikMagazik.Nhibarnate.Providers
{
    public class UserProvider : Provider<AppUser>, IUserProvider
    {
        public UserProvider(SessionWrapperFactory sessionWrapperFactory, TransactionWrapperFactory transactionWrapperFactory) 
            : base(sessionWrapperFactory, transactionWrapperFactory)
        {
        }

        public Task<AppUser> GetByEmail(string email)
        {
            return providerHelper.Invoke(s => s.QueryOver<AppUser>().Where(it => it.Email == email).SingleOrDefaultAsync());
        }
    }
}
