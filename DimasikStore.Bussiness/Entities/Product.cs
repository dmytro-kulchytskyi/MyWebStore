using System;
using System.Collections.Generic;
using System.Text;

namespace DimasilStore.Business.Entities
{
    public class Product : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Image { get; set; }

        public virtual string Description { get; set; }

        public virtual double Price { get; set; }

        public virtual int Sells { get; set; }

        public virtual int Stock { get; set; }
    }
}
