﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyStore.Business.Entities
{
    public class Address : IEntity
    {
        public virtual string Id { get; set; }

        public virtual Country Country { get; set; }

        public virtual string City { get; set; }

        public virtual string Street { get; set; }

        public virtual string HouseNumber { get; set; }

        public virtual string PostalCode { get; set; }
    }
}
