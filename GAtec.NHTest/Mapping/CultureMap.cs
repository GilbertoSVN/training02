using FluentNHibernate.Mapping;
using GAtec.NHTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.NHTest.Mapping
{
    public class CultureMap : ClassMap<Culture>
    {
        public CultureMap()
        {
            Table("GA_CULTURE");

            Id(x => x.Id).Column("ID").GeneratedBy.Native("SEQ_GA_CULTURE");

            Map(x => x.Code).Column("CODE").Not.Nullable();
            Map(x => x.Description).Column("DESCRIPTION").Not.Nullable();
            Map(x => x.CreationDate).Column("SYS_CREATION_DATE").Not.Nullable();
        }
    }
}
