using System;

namespace GAtec.NHTest.Model
{
    public class Culture
    {
        public virtual long  Id { get; set; }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public Culture()
        {
        }
    }
}
