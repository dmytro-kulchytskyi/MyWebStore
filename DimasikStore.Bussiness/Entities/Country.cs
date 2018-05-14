using System;
using System.Collections.Generic;
using System.Text;

namespace DimasilStore.Business.Entities
{
    public class Country : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
    }
}
