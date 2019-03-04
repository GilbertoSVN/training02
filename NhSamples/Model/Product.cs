using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhSamples.Model
{
    public class Product
    {
        public virtual long Id { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal Price { get; set; }

        public virtual int Stock { get; set; }

        public virtual long CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Product()
        {                
        }        
    }
}
