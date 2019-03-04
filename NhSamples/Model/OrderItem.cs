using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhSamples.Model
{
    public class OrderItem
    {
        public virtual long Id { get; set; }

        public virtual long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual decimal Price { get; set; }

        public virtual int Quantity { get; set; }

        public virtual decimal Total { get; set; }

        public virtual long OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
