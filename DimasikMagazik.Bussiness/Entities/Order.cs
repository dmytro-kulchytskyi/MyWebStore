using System;
using System.Collections.Generic;
using System.Text;

namespace DimasikMagazik.Business.Entities
{
    public class Order : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual string UserAddressId { get; set; }
    }
}
