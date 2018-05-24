using Microsoft.AspNet.Identity;
using MyStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Mvc.Identity
{
    public class IdentityUser : IUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public string Role { get; set; }

        public string PasswordHash { get; set; }

        public IList<Address> Address { get; set; } = new List<Address>();
    }
}