using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DimasikStore.Business.Entities;

namespace DimasikStore.Mvc.Identity
{
    public class IdentityUser : IUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public string Role { get; set; }

        public string PasswordHash { get; set; }
   }
}