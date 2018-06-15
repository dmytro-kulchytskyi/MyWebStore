using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Entities
{
    public class BasketProduct : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string ProductId { get; set; }

        public virtual string BasketId { get; set; }

        public virtual int Count { get; set; }
    }
}
