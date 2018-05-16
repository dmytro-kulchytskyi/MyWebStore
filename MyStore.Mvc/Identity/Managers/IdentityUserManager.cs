using MyStore.Mvc.Identity.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyStore.Mvc.Identity.Managers
{
    public class IdentityUserManager : UserManager<IdentityUser, string>
    {
        public IdentityUserManager(IUserStore<IdentityUser, string> store)
            : base(store)
        {
            UserValidator = new UserValidator<IdentityUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = false;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
        }

        public override Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            user.Role = "User";
            return base.CreateAsync(user, password);
        }
    }
}