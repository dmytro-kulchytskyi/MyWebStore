using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.Entities
{
    public class Country : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual bool Available { get; set; }
    }
}
