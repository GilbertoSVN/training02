using System;
using System.Collections.Generic;

namespace NhSamples.Model
{
    public class Order
    {
        public virtual long Id { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual decimal Total { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }
    }
}
