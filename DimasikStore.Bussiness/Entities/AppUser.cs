using System;
using System.Collections.Generic;
using System.Text;

namespace DimasikStore.Business.Entities
{
    public class AppUser : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string Role { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual bool Banned { get; set; }
   }
}
