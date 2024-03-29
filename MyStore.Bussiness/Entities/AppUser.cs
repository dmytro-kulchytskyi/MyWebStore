﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyStore.Business.Entities
{
    public class AppUser : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string Role { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual bool Banned { get; set; }

        public virtual IList<Address> Address { get; set; } = new List<Address>();
   }
}
