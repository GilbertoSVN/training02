using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.Consultas
{
    class Program
    {

        static void MinhaSoma(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        static void ExemplosActionFunc()
        {

            Action olaMundo = () =>
            {
                Console.WriteLine("Olá mundo!");
            };

            Func<int, Func<int, double>, double> cubo = (i, f) =>
            {
                return Math.Pow(f(i), 3);
            };

            Func<int, double> quadrado = x => Math.Pow(x, 2);

            var retorno = cubo(2, quadrado);

            Console.WriteLine(retorno);

            Action<int, int> soma = MinhaSoma;

            Func<string> obterTexto = () =>
            {
                return "Texto!";
            };

            Func<int, int, int> subtracao = (a, b) => a - b;

            Func<int, bool> maiorQueZero = x => x > 0;

            olaMundo();

            soma(20, 22);

            var s = obterTexto();

            Console.WriteLine(s);

            Console.WriteLine(subtracao(10, 2));
        }



        public class Produto
        {
            public string Nome { get; set; }

            public decimal Preco { get; set; }

            public string Categoria { get; set; }

            public override string ToString()
            {
                return Nome + " - " + Preco.ToString("c2") + " - " + Categoria;
            }

            public List<decimal> PrecosConcorrentes { get; set; }

        }

        static bool ProdutoCaro(Produto prod, decimal caro = 500)
        {
            return prod.Preco > caro;
        }

        static void ExemplosLinq()
        {

            Random r = new Random();

            string[] categorias = new[] { "Software", "Hardware", "Armas", "Balas" };

            List<int> lista = new List<int>();
            List<Produto> produtos = new List<Produto>();

            for (int i = 0; i < 10; i++)
            {
                var valor = r.Next(1, 50);
                var preco = r.Next(10, 1000);

                var prod = new Produto() { Nome = "Produto " + valor, Preco = r.Next(10, 1000), Categoria = categorias[r.Next(0, categorias.Length - 1)] };

                if (i % 2 == 0)
                {
                    var prod2 = new Produto() { Nome = "Produto " + r.Next(1, 10), Preco = preco, Categoria = categorias[r.Next(0, categorias.Length - 1)] };
                    var prod3 = new Produto() { Nome = "Produto " + r.Next(1, 10), Preco = preco, Categoria = categorias[r.Next(0, categorias.Length - 1)] };

                    produtos.Add(prod2);
                    produtos.Add(prod3);
                }

                produtos.Add(prod);

                Console.WriteLine(prod);
            }

            Console.WriteLine("----------------------------------------");

            //Console.WriteLine(lista.Sum());
            //
            //Console.WriteLine(lista.Average());
            //
            //Console.WriteLine(lista.Max());
            //
            //Console.WriteLine(lista.Min());

            //foreach (var i in lista.OrderBy(x => x))
            //{
            //    Console.Write(i + " , ");
            //}

            var mediaDePrecos = produtos.Average(x => x.Preco);

            var dados = produtos.Where(x => x.Preco > mediaDePrecos)
                                .OrderBy(x => x.Preco)
                                .ThenBy(x => x.Nome);


            Console.WriteLine("Total de Produtos: " + produtos.Count());
            Console.WriteLine("Média: " + mediaDePrecos.ToString("C2"));
            foreach (var item in dados)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("----------------------------------------");

            var resultadoAgrupado = produtos.GroupBy(x => new { x.Categoria, x.Preco });

            foreach (var grupo in resultadoAgrupado.Where(x => x.Count() > 1))
            {
                Console.WriteLine("Preco: " + grupo.Key.Preco + " Categoria: " + grupo.Key.Categoria);

                foreach (var item in grupo)
                {
                    Console.WriteLine("\t" + item);
                }
            }

            Console.WriteLine("----------------------------------------");

            var categs = produtos.Select(p => p.Categoria);

            foreach (var cat in categs)
            {
                Console.WriteLine(cat);
            }

            Console.WriteLine("----------------------------------------");

            var categsDistinct = produtos.Select(p => p.Categoria).Distinct();

            foreach (var cat in categsDistinct)
            {
                Console.WriteLine(cat);
            }


            Console.WriteLine("----------------------------------------");

            var categsGroups = produtos.GroupBy(x => x.Categoria);

            foreach (var cat in categsGroups)
            {
                Console.WriteLine(cat.Key + " - " + cat.Count() + " - " + cat.Max(x => x.Preco));
            }


            Console.WriteLine("------ Produtos Caros ----------------------------------");



            var produtosCaros = produtos.Where(p =>
            {
                var resultado = ProdutoCaro(p);

                return resultado;
            });



            var produtosCaros2 = produtos.Where(x => x.Preco > 500);

            foreach (var prod in produtosCaros)
            {
                Console.WriteLine(prod);
            }


            produtos.Any(x => x.Preco > 900);

            produtos.Count(x => x.Preco > 900);

            produtos.First();
            produtos.FirstOrDefault();

            produtos.FirstOrDefault(x => x.Preco > 900 && x.Categoria == "Hardware");

            produtos.OrderBy(x => x.Categoria).FirstOrDefault(x => x.Preco > 900);

            produtos.Skip(10).Take(5);

            produtos.Last();
            produtos.LastOrDefault();


            produtos.Where(x => x.Preco < x.PrecosConcorrentes.Min());

            produtos.Where(x => x.PrecosConcorrentes.Any());
        }

        


        static void Main(string[] args)
        {
            Random r = new Random();
            
            string[] categorias = new[] { "Software", "Hardware", "Armas", "Balas" };

            List<int> lista = new List<int>();
            List<Produto> produtos = new List<Produto>();

            for (int i = 0; i < 10; i++)
            {
                var valor = r.Next(1, 50);
                var preco = r.Next(10, 1000);

                var prod = new Produto() { Nome = "Produto " + valor, Preco = r.Next(10, 1000), Categoria = categorias[r.Next(0, categorias.Length - 1)] };

                if (i % 2 == 0)
                {
                    var prod2 = new Produto() { Nome = "Produto " + r.Next(1, 10), Preco = preco, Categoria = categorias[r.Next(0, categorias.Length - 1)] };
                    var prod3 = new Produto() { Nome = "Produto " + r.Next(1, 10), Preco = preco, Categoria = categorias[r.Next(0, categorias.Length - 1)] };

                    produtos.Add(prod2);
                    produtos.Add(prod3);
                }

                produtos.Add(prod);

                Console.WriteLine(prod);
            }

            Console.WriteLine("----------------------------------------");


            var query = (from p in produtos
                        //where ProdutoCaro(p)
                        group p by p.Categoria into g
                        where g.Any(x => x.Preco > 10)
                        select new
                        {
                            g.Key,
                            Count = g.Count(),
                            Produtos = g.ToList()
                        }).ToList();

            foreach (var q in query)
            {
                Console.WriteLine(q.Key);
                Console.WriteLine(q.Count);
                foreach(var p in q.Produtos.Where(x => x.Preco > 500))
                {
                    Console.WriteLine(p.Nome);
                }
                Console.WriteLine("---------------------");
            }
            









            Console.ReadLine();
        }
    }
}
