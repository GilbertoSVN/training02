
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;

using GAtec.NHTest.Model;
using GAtec.NHTest.Mapping;

namespace GAtec.NHTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new Configuration().Configure();

            configuration = Fluently.Configure(configuration)
                                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<CultureMap>())
                                    .BuildConfiguration();
            
            var factory = configuration.BuildSessionFactory();

            using (var session = factory.OpenSession())
            {
                var data = session.Query<Culture>()
                    .Where(x => x.Code.Contains("F"))
                                  .OrderBy(x => x.CreationDate)
                                  .ToList();
                
                foreach (var item in data)
                {
                    Console.WriteLine(item.Id + " - " + item.Code);
                }

                session.Close();
            }

            factory.Dispose();

            Console.ReadLine();
        }
    }
}
