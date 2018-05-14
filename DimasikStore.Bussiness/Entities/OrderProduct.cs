using System;
using System.Collections.Generic;
using System.Text;

namespace DimasilStore.Business.Entities
{
    public class OrderProduct : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string OrderId { get; set; }

        public virtual string ProductId { get; set; }

        public virtual string Count { get; set; }
    }
}
