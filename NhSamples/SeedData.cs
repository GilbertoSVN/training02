using NHibernate;
using NhSamples.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NhSamples
{
    public static class SeedData
    {
        static Random rand = new Random();

        static Category[] categorias = new Category[] {
                new Category () { Name = "Software" },
                new Category() { Name = "Hardware" },
                new Category() { Name = "Eletrônicos" },
                new Category() { Name = "Games" },
                new Category() { Name = "Periféricos" },
                new Category() { Name = "Smartphones" },
                new Category() { Name = "Tablets" },
                new Category() { Name = "Cameras" }
            };

        static List<Product> produtos = new List<Product>();

        public static T RandomElement<T>(IEnumerable<T> items)
        {
            return items.ElementAt(rand.Next(items.Count()));
        }

        static DateTime RandomDay()
        {
            var start = new DateTime(2017, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }

        static void SeedOrder(Order ordem)
        {
            for (int j = 0; j < rand.Next(1, 10); j++)
            {
                var item = new OrderItem();
                item.Product = RandomElement(produtos);
                item.Price = item.Product.Price;
                item.Quantity = rand.Next(1, 5);
                item.Total = item.Price * item.Quantity;
                item.Order = ordem;

                ordem.Items.Add(item);
            }

            ordem.OrderDate = RandomDay();
            ordem.Total = ordem.Items.Sum(x => x.Total);
        }


        public static void Seed(ISession session)
        {
            foreach (var categoria in categorias)
            {
                session.Save(categoria);

                for (int i = 1; i < 20; i++)
                {
                    var p = new Product()
                    {
                        Code = string.Concat(categoria.Name.Substring(0, 3), i),
                        Name = string.Concat(categoria.Name, " ", i),
                        Category = categoria,
                        Price = Convert.ToDecimal(rand.Next(50, 2500) + Math.Truncate(rand.NextDouble())),
                        Stock = rand.Next(1, 100)
                    };

                    session.Save(p);

                    produtos.Add(p);
                }
            }

            for (int i = 0; i < 100; i++)
            {
                var compra = new PurchaseOrder();
                compra.SupplierName = RandomElement(new[] { "Samsung", "Sony", "Apple", "Asus" });
                SeedOrder(compra);

                session.Save(compra);

                var venda = new SaleOrder();
                venda.CustomerName = RandomElement(new[] { "Felipe", "Marco", "Giba", "Jack", "Daniel", "Adelia", "Lucas", "Eduardo", "William" });
                SeedOrder(venda);

                session.Save(venda);
            }
        }

        public static void Clear(ISession session)
        {
            session.CreateQuery("delete from OrderItem").ExecuteUpdate();
            session.CreateQuery("delete from SaleOrder").ExecuteUpdate();
            session.CreateQuery("delete from PurchaseOrder").ExecuteUpdate();
            session.CreateQuery("delete from Product").ExecuteUpdate();
            session.CreateQuery("delete from Category").ExecuteUpdate();
            
            session.CreateSQLQuery("DBCC CHECKIDENT ('[Order]', RESEED, 1)").ExecuteUpdate();
            session.CreateSQLQuery("DBCC CHECKIDENT ('Product', RESEED, 1)").ExecuteUpdate();
            session.CreateSQLQuery("DBCC CHECKIDENT ('Category', RESEED, 1)").ExecuteUpdate();
        }
    }
}
