using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Linq;

using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using System.Reflection;
using NhSamples.Model;

namespace NhSamples
{
    class Program
    {
        static ISessionFactory GetSessionFactory()
        {
            var configuration = new Configuration().Configure();

            configuration = Fluently.Configure(configuration)
                                    .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                                    .BuildConfiguration();

            var factory = configuration.BuildSessionFactory();

            return factory;
        }


        public static void ListarProdutos(ISession session)
        {
            var products = session.Query<Product>()
                                  .OrderBy(x => x.Price)
                                  .Select(x => new
                                  {
                                      NomeDoProduto = x.Name,
                                      EstoqueTotal = (x.Price * x.Stock)
                                  })
                                  .ToList();

            foreach (var p in products)
            {
                //Console.WriteLine("Id: " + p.Id);
                //Console.WriteLine("Name: " + p.Name);
                //Console.WriteLine("Price: " + p.Price);
                Console.WriteLine("Nome: " + p.NomeDoProduto);
                Console.WriteLine("Total: " + p.EstoqueTotal);
                Console.WriteLine("--------------------");
            }
        }

        public static void ListarCategorias(ISession session)
        {
            var categorias = session.Query<Category>()
                                    .FetchMany(x => x.Products)
                                    .OrderBy(x => x.Name)
                                    .ToList();

            foreach (var categoria in categorias)
            {
                Console.WriteLine(categoria.Name);
                foreach (var produto in categoria.Products)
                {
                    Console.WriteLine("\t" + produto.Name);
                }
            }
        }


        public static void ListarProdutos2(ISession session)
        {
            var produtos = session.Query<Product>()
                                  .Fetch(x => x.Category)
                                  .OrderBy(x => x.Name)
                                  .ToList();

            foreach (var prod in produtos)
            {
                Console.WriteLine(prod.Name);
                Console.WriteLine(prod.Category.Name);
                Console.WriteLine("--------------------");

            }


            var cat = session.Get<Category>(9L);


        }

        static void Main(string[] args)
        {
            var factory = GetSessionFactory();

#if DEBUG
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif

            using (var session = factory.OpenSession())
            {
                using (var tx = session.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var category = session.Query<Category>()
                                              .FetchMany(x => x.Products)
                                              .Where(x => x.Name.StartsWith("a") && x.Products.Any(p => p.Price > 100))
                                              .OrderByDescending(x => x.Name)
                                              .Take(10)
                                              .ToList();

                        /*
                        var prod = session.Load<Product>(155L);

                        session.Delete(prod);
                        */

                        /*
                        var cat = new Category();
                        cat.Name = "Armas";

                        session.Save(cat);
                        */

                        /*
                        string categoryName = "armas";
                        var cat = session.Query<Category>()
                                          .Where(x => x.Name.ToUpper().Trim() == categoryName.ToUpper().Trim())
                                          .FirstOrDefault();
                                          */
                        /*
                        for (int i = 0; i < 100; i++)
                        {

                            var prod1 = new Product()
                            {
                                Code = "AK" + i,
                                Category = cat,
                                Name = "AK-47 " + i,
                                Price = 45000m,
                                Stock = 10
                            };

                            session.Save(prod1);

                            var prod2 = new Product()
                            {
                                Code = "M4" + i,
                                Category = cat,
                                Name = "M4A1 " + i,
                                Price = 40000m,
                                Stock = 15
                            };

                            session.Save(prod2);
                        }

                        */

                        /*
                        session.CreateQuery("delete from Product where CategoryId=:id")
                               .SetParameter("id", cat.Id)
                               .ExecuteUpdate();

    */


                        /*
                        var categories = session.Query<Category>()
                                                .FetchMany(x => x.Products)
                                                .ToList();
                                               

                        var categories = session.CreateQuery("from Category")
                                                .List<Category>();

                        var prods = session.CreateQuery("from Product p join fetch p.Category")
                            .List<Product>();


                        Category category = null;

                        var prod = session.QueryOver<Product>()
                            .JoinAlias(x => x.Category, () => category)
                            .Where(x => x.Name == "Armas")
                                            .And(x => x.CategoryId == 10)
                                                .OrderBy(x => category.Name)
                                                .Asc
                                                .List<Product>();



                        foreach (var c in categories)
                        {
                            Console.WriteLine(c.Name + " - " + c.Products.Sum(x => x.Stock * x.Price).ToString("C2"));
                        }
                         */
                        tx.Commit();
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }

                }


                Console.WriteLine("\n\nfim ...");

                Console.ReadLine();
            }
        }

        private static void ExemploQuery(ISession session)
        {
            var orders = session.Query<Order>().ToList();

            var produtos = session.Query<Product>()
                .Where(x => !session.Query<OrderItem>().Any(i => i.Product == x))
                .Select(x => x.Category.Name)
                .ToList();
        }
    }
}
