using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Entities
{
    public class Basket : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual IList<BasketProduct> Products { get; set; } = new List<BasketProduct>();
    }
}
