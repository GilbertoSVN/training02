using System;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

using System.IO;

namespace GAtec.Agro.App
{
    class Program
    {
        // -----------------------------------------------------------------------------------
        // Métodos utilizados nos exemplos.
        // -----------------------------------------------------------------------------------

        static void ImprimirTexto(object o)
        {
            Console.WriteLine(o.ToString());
        }
        
        static void ImprimirPreco(Lampada lomp)
        {
            Console.WriteLine(lomp.Preco.ToString("C2"));
        }

        static void IniciarObjeto(IAcendivel acendivel)
        {
            acendivel.Acender();
        }

        static void ImprimirObjeto(IImprimivel imprimivel)
        {
            imprimivel.Imprimir();
        }

        static void ImprimirPreco(Lampada lomp, params object[] inteiros)
        {
            ImprimirPreco(lomp);
            var s = new StringBuilder();
            for (int i = 0; i < inteiros.Length; i++)
            {
                s.AppendFormat("Indice: {0}, Tipo: {1}, Valor: {2}{3}.", i , inteiros[i].GetType().Name, inteiros[i], Environment.NewLine);
            }
            Console.WriteLine(s);        
        }
        
        static Produto ObterProduto(ref int idProduto)
        {
            var ex = new ArgumentException("Qeubrou!", "idProduto");

            if (idProduto <= 0)
                throw ex;

            idProduto++;

            var p = new Produto()
            {
                Id = idProduto,
                Nome = "Produto " + idProduto,
                Preco = Convert.ToDecimal(idProduto)
            };

            return p;
        }


        static void ModificarPreco(Produto p, decimal percentual)
        {
            p.Preco += p.Preco * (percentual / 100m);
        }

        static void DobrarPreco(Produto pr)
        {
            pr.Preco *= 2;
        }

        static void DobrarPreco(Produto p, out decimal precoAnterior)
        {
            precoAnterior = p.Preco;
            DobrarPreco(p);
        }


        // -----------------------------------------------------------------------------------
        // EXEMPLOS
        // -----------------------------------------------------------------------------------

        public static void ExemploMudarCulturaAtual()
        {
            // instancia um objeto de cultura baseado no ingles americano
            var enUs = new CultureInfo("en-US");

            // seta a cultura na Thread corrente do seu programa
            // assim as mensagens em geral serão baseadas em inglês
            Thread.CurrentThread.CurrentCulture = enUs;
            Thread.CurrentThread.CurrentUICulture = enUs;
        }

        public static void ExemplosDeEstruturasDoCSharp()
        {
            try
            {
                int id;
                Console.Write("Digite um id: ");
                string inputId = Console.ReadLine();

                if (!int.TryParse(inputId, out id))
                {
                    Console.Write("Digite um id valido.");
                    Console.Read();
                    return;
                }

                var now = DateTime.Now;

                var prod = ObterProduto(ref id) ?? new ProdutoDeLimpeza() { Preco = 5, Qualidade = "Bom" };

                var prodLimpeza = (ProdutoDeLimpeza)prod;
                if (prodLimpeza != null)
                {
                    Console.WriteLine("Qualidade: " + prodLimpeza.Qualidade);
                }

                decimal pAnterior;
                DobrarPreco(prod, out pAnterior);

                int percentual;
                Console.Write("Percentual: ");
                percentual = Convert.ToInt32(Console.ReadLine());

                ModificarPreco(prod, (decimal)percentual);

                Console.WriteLine("Preco Anterior: " + pAnterior.ToString("C2"));
                Console.WriteLine("Preco dobrado: " + prod.Preco.ToString("C2"));

                decimal a, b, r = 0m;
                string op;

                Console.Write("Digite a: ");
                a = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Digite b: ");
                b = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Op: ");
                op = Console.ReadLine();

                if (op == "+")
                {
                    r = a + b;
                }
                else if (op == "-")
                {
                    r = a - b;
                }
                else if (op == "/" || b != 0)
                {
                    r = (b == 0) ? 0 : a / b;
                }

                switch (op)
                {
                    case "+":
                        r = a + b;
                        break;
                    case "-":
                        r = a - b;
                        break;
                    default:
                        r = a / b;
                        break;
                }

                Console.WriteLine("r: " + r);

            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Conversao invalida");
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Divisao por zero");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro geral");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("FIM!");
            }
        }

        public static void ExemploGlobalizacao()
        {
            var ptBr = new CultureInfo("pt-BR");
            var enUS = new CultureInfo("en-US");

            Thread.CurrentThread.CurrentCulture = enUS;

            decimal preco = 1.99m;
            var agora = DateTime.Now;

            Console.WriteLine(preco.ToString("C4"));
            Console.WriteLine(agora.ToString(ptBr));
        }

        public static void ExemploStringSplit()
        {
            string nome = "1,2,3,4,5,6,7,8";

            var arr = nome.Trim().Split(',');

            for (int x = 0; x < arr.Length; x++)
            {
                Console.WriteLine(arr[x]);
            }

            Console.WriteLine();

            Console.Read();
            return;
        }

        public static void ExemploStringSubString()
        {
            string texto = "Agronegócio";

            var agro = texto.Substring(0, 4);
            var negocio = texto.Substring(5);
            
            Console.WriteLine(agro);
            Console.WriteLine(negocio);

            Console.Read();
            return;
        }

        public static void ExemploStringFormat()
        {
            decimal valor = 10.5m;
            string nome = "João";
            
            // não use desta forma string.Format e o operador '+', não há sentido nisso.
            Console.WriteLine(string.Format("Apresentando o valor " + 12));

            // formate uma string e use assim
            string textoFormatado = string.Format("Apresentando os valores {0}, {1}, {2}.", 10, 11, 12);

            // use assim
            Console.WriteLine(textoFormatado);

            // em alguns casos, temos implementado no próximo método o suporte a formatação
            // como é o caso do Console.WriteLine
            Console.WriteLine("Olá {0}, este produto custa {1}", nome, valor);
        }


        public static void ExemplosObjetos()
        {
            // instancia uma lampada 
            var a = new Lampada();

            // instancia um cigarro
            var c = new Cigarro();

            // inicia objetos da interface IAcendivel (polimorfismo com interfaces)
            IniciarObjeto(a);
            IniciarObjeto(c);

            // executa objetos da interface IImprimivel (polimorfismo com interfaces)
            ImprimirObjeto(a);
            ImprimirObjeto(c);
            
            var b = new LampadaLed();

            b.TrocarDiodo("ABC", 123);

            ImprimirPreco(a, 18, 48, "Femminino", false, DateTime.Now, b);

            Console.ReadKey();

            return;
        }

        public static void ExemploEstadoDeObjetos()
        {
            Lampada lamp = new Lampada();
            var led = new LampadaLed();
            Lampada limp = new LampadaLed();

            var lemp = Lampada.Criar("leds");

            Console.WriteLine("Lampada: " + lamp.Preco);
            Console.WriteLine("Led: " + led.Preco);
            Console.WriteLine("Poli: " + limp.Preco);

            ImprimirPreco(lamp);
            ImprimirPreco(led);
            ImprimirPreco(limp);
            ImprimirPreco(lemp);

            var teste = led.EstaAceso();

            Console.WriteLine(lamp.EstaAceso());
            Console.WriteLine(led.EstaAceso());

            lamp.Acender();
            led.Acender();

            Console.WriteLine(lamp.EstaAceso());
            Console.WriteLine(led.EstaAceso());

            lamp.Apagar();
            led.Apagar();

            Console.WriteLine(lamp.EstaAceso());
            Console.WriteLine(led.EstaAceso());

            Console.Read();
            return;
        }

        public static void ExemploDateTime()
        {
            // Isso é um DateTime que representa uma data e hora
            DateTime dataHora = DateTime.Now;
            dataHora.AddMinutes(5); // incrementa 5 minutos nesta cada
            dataHora.AddDays(2); // incrementa 2 dias nesta cada

            // TimeSpan é um objeto que representa uma unidade de medida de tempo
            // Neste exemplo, temos um TimeSpan que representa 15 minutos
            TimeSpan quinzeMinutos = TimeSpan.FromMinutes(15);
            TimeSpan umaHora = TimeSpan.FromHours(1);

            // incrementa 15 minutos no TimeSpan 'umaHora' passando a ter seu valor alterado
            umaHora.Add(quinzeMinutos);

            // um timeSpan pode ser utilizado em um Datetime de modo a incrementar a data
            // neste caso, incrementa 15 minutos nesta data e hora
            dataHora.Add(quinzeMinutos);


            // para imprimir datas e horas e tempo, utilize a especificação da Microsoft para formatação
            // https://docs.microsoft.com/pt-br/dotnet/standard/base-types/custom-date-and-time-format-strings

            Console.WriteLine(dataHora.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.WriteLine(umaHora.ToString("HH:mm:ss"));
        }

        public static void ExemploTiposPrimitivos()
        {
            


            // 'bool' vem da especificação do C# que é um atalho para 'Boolean' que é o tipo da plataforma 
            bool g = true;
            // 'Boolean' é o tipo da plataforma
            Boolean g2 = false;

            // representação de um caracter simples usando o tipo char entre 'aspas simples'
            char a = 'a';
            Char a2 = 'b';

            // representação de uma string usando o tipo String entre "aspas duplas"
            string s = "texto";
            String k = ".Net";

            // todo tipo numérico em C# tem uma especificação
            // Veja mais neste link
            // https://docs.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/value-types-table
            // Alguns valor numérico tem um sufixo após sua escrita de modo a identificar o tipo do valor (numero).
            // Um simples 10 é um inteiro (int), já um 10L é um inteiro longo (long) e assim por diante.
            // Veja os exemplos abaixo.

            // tipos numériso inteiros
            byte inteiro8Bits = 255; // System.Byte, sem sufixo, inteiro de 8 bits
            sbyte inteiro8BitsComSinal = -15; // System.SByte, sem sufixo, inteiro de 8 bits com sinal. O 's' bem se 'signed'.
            short inteiro16Bits = 10; // System.Int16, sem sufixo
            int inteiro32Bits = 10; // System.Int32, sem sufixo
            long inteiro64Bits = 10l; // System.Int64, sufixo 'l' ou 'L'

            // tipos numéricos decimais
            float pontoFlutuante = 123.2f; // System.Single, sufixo 'f' ou 'F'
            double doub = 2.4; // System.Double, sem sufixo
            decimal dec = 123m; // System.Decimal, sufixo 'm'

            // tipos numéricos inteiros sem sinal (que aproveita o último bit e aumenta o valor, possibilitando apenas positivos)
            ushort unsignedShort = 123; // System.UInt16, sem sufixo
            uint unsignedInteger = 0u; // System.UInt32, sufixo 'u'
            ulong unsignedLong = 123ul; // System.UInt64, sufixo 'ul'
            
            // Utilize 'var' na declaração apenas quando for inicializar um valor.
            // A plataforma precisa conhecer o tipo da referência/valor que você está alocando em memória.
            var inteiroComVar = 10;
            var decimalComVar = 10m;
            var doubleComVar = 10.5;
            var uintComVar = 10u;
            var lampada = new Lampada();
            var texto = "String com Var";
            string texto2 = "Isso é a mesma coisa";
            

            Console.WriteLine("Hello world! This is a string.");
            Console.WriteLine('a');            
        }

        // -----------------------------------------------------------------------------------

        public static void ExemploDeRepeticao()
        {
            var random = new Random();

            int[] valores = new int[10];
            
            for (int i = 0; i < valores.Length; i++)
            {
                valores[i] = random.Next(1, 100);
            }

            Console.WriteLine("----------------------------");

            int contador = 0;
            while (contador < valores.Length)
            {
                Console.WriteLine(valores[contador]);
                contador++;
            }

            Console.WriteLine("----------------------------");

            contador = 0;
            do
            {
                Console.WriteLine(valores[contador]);
                contador++;

            } while (contador < valores.Length);
                        
            Console.WriteLine("----------------------------");

            for (int i = 0; i < valores.Length; i++)
            {
                var valor = valores[i];

                Console.WriteLine(valor);
            }

            foreach (var valor in valores)
                Console.WriteLine(valor);
        }

        public static void ExemploColecoes()
        {

            ArrayList list = new ArrayList();

            list.Add("GAtec");
            list.Add(123);
            list.Add(50m);
            list.Add(true);

            foreach (var item in list)
            {
                Console.WriteLine("{0}: {1}", item.GetType().Name, item);
            }

            ArrayList precos = new ArrayList();

            precos.Add(12.50m);
            precos.Add(20.45m);
            precos.Add(150m);

            for (int i = 0; i < precos.Count; i++)
            {
                var preco = (decimal) precos[i];

                precos[i] = preco + (preco * 0.1m);
            }

            foreach (var preco in precos)
            {
                Console.WriteLine("{0:c2}", preco);
            }

            var d = new Data();

            var dados = d.GetEnumerator();

            foreach (var n in d)
            {
                Console.WriteLine(n);
            }


        }
        

        class Data : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return 11;


                yield return false;

                yield break;


                yield return 33;


                yield return 44;
            }
        }


        static void TrocarValor<T>(T v1, T v2)
        {
            T aux = v1;
            v1 = v2;
            v2 = aux;
        }


        public static void ExemploGenerics()
        {


            var p1 = new Produto() { Id = 1 };
            var p2 = new Produto() { Id = 2 };

            TrocarValor<Produto>(p1, p2);

            Console.WriteLine(p2.Id);

            var repo = new Repositorio<Produto>();

            repo.Adicionar(new Produto()
            {
                Id = 1,
                Nome = "Simple Farm",
                Preco = 1000m
            });

            repo.Adicionar(new Produto()
            {
                Id = 2,
                Nome = "WMS",
                Preco = 999m
            });

            repo.Adicionar(new Produto()
            {
                Id = 3,
                Nome = "Simple Viewer",
                Preco = 1001m
            });

            foreach (var item in repo.Obter())
            {
                Console.WriteLine(item);
            }


        }

        public static void ExemploColecoesGenerics()
        {
            List<Produto> produtos = new List<Produto>();

            produtos.Add(new Produto()
            {
                Id = 1,
                Nome = "Simple Farm",
                Preco = 1000m
            });

            produtos.Add(new Produto()
            {
                Id = 2,
                Nome = "WMS",
                Preco = 999m
            });

            produtos.Add(new Produto()
            {
                Id = 3,
                Nome = "Simple Viewer",
                Preco = 1001m
            });

            foreach (var p in produtos)
            {
                Console.WriteLine(p);
            }


            List<decimal> precos = new List<decimal>();

            precos.Add(100m);
            precos.Add(140m);
            precos.Add(150m);
            precos.Add(180m);

            for (int i = 0; i < precos.Count; i++)
            {
                precos[i] = precos[i] + (precos[i] * 0.5m);
            }

            foreach(var p in precos)
            {
                Console.WriteLine(p);
            }


           

        }

        class JsonData
        {
            public string nome { get; set; }

            public int idade { get; set; }

            public decimal valor { get; set; }
            
            public bool ligado { get; set; }
        }



        public static void DeserializarJson()
        {
            
            var json = "{ nome: 'GAtec', idade: 17, valor: 10000.45 }";
            var jsonList = "[{ nome: 'GAtec', idade: 17, valor: 10000.45, ligado: true }, { nome: 'Simple Farm', idade: 3, valor: 500.45, ligado: false }]";

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonData>(json);
            

            //Console.WriteLine(data.nome);
            //Console.WriteLine(data.idade);
            //Console.WriteLine(data.ligado);

            var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            foreach (var chave in dic)
            {
               // Console.WriteLine(chave.Key + " " + chave.Value);
            }

            int k = default(int);


            var dicList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonList);


            foreach(var d in dicList)
            {
                Console.WriteLine("-------------------------------");
                foreach (var chave in d)
                {
                    Console.WriteLine(chave.Key + " " + chave.Value);
                }

            }

            var prod = new Produto()
            {
                Id = 2,
                Nome = "WMS",
                Preco = 999m
            };

            var jsonProd = Newtonsoft.Json.JsonConvert.SerializeObject(prod);






            string complexJson = "{ produto: {Id: 1, Nome: 'SPF', Preco: 123.45} }";

            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(complexJson);


            





            Console.WriteLine(jsonProd);
        }

        class Item
        {
            public Produto produto { get; set; }

            public Lampada lampada { get; set; }
        }


        public static void ExemploExtensionMethods()
        {


            decimal valor = 45.50m;

            var p = new Produto() { Preco = 20.46m };

            var br = valor.FormatCurrencyBR();
            var us = valor.FormatCurrencyUS();
            var fr = valor.FormatCurrency("cn-CN");

            Console.WriteLine(br);
            Console.WriteLine(us);
            Console.WriteLine(fr);

            p.Print();

            Produto prod = null;

            "Arnaldo Cesar Coelho".Print();

            1323.Print();

            true.Print();


            valor.Print();

            List<Produto> produtos = new List<Produto>();


        }


        public static void ExemploTipoAnulaveis()
        {

            int? inteiroAnulavel = 0;
            int? inteiroNull;

            DateTime? dataAnulavel = DateTime.Now;
            
            double? d = 100.10;
            
            dataAnulavel = null;

            if (!inteiroAnulavel.HasValue)
            {

            }

            var i = default(Produto);
        }


        public static void ExemploSistemaDeArquivos()
        {
            string pasta = @"C:\temp\BancoInformix";

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }
            
            var fileInfo = new FileInfo(Path.Combine(pasta, "projeto.log"));
            
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
            }
            
            // fileInfo.Refresh();
            // fileInfo.CopyTo(pasta + "\\projeto2.log");

            var diretorios = Directory.GetDirectories(@"C:\temp", "*Informix*", SearchOption.AllDirectories);
            
            foreach (var dir in diretorios)
            {
                var dirInfo = new DirectoryInfo(dir);
                
                //Console.WriteLine(dir);

                var arquivos = Directory.GetFiles(dir, "*.log");

                foreach (var arquivo in arquivos)
                {
                    Console.WriteLine(arquivo);

                    if (File.Exists(arquivo))
                    {
                        //var conteudo = File.ReadAllText(arquivo);

                        //conteudo.Print();

                        string log = string.Format("\n{0} - {1}", DateTime.Now, "Operacao do sistema do gibao");

                        FileStream arquivoStream = File.Open(arquivo, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                        StreamWriter writer = new StreamWriter(arquivoStream);

                        //arquivoStream.Seek(0, SeekOrigin.End);


                        //var logEmBytes = Encoding.Default.GetBytes(log);

                        //arquivoStream.Write(logEmBytes, 0, logEmBytes.Length);
                        
                        arquivoStream.Seek(0, SeekOrigin.End);

                        writer.Write(log);

                        writer.Flush();
                        

                        arquivoStream.Seek(0, SeekOrigin.Begin);

                        StreamReader reader = new StreamReader(arquivoStream);

                        var content = reader.ReadToEnd();

                        Console.WriteLine(content);

                    }
                }

                

            }

        }


        static void Main(string[] args)
        {
            // A implementação foi organizada em métodos com prefixo Exemplo.
            // Aqui você pode chamar um dos métodos de exemplo.

            ExemploSistemaDeArquivos();

            // para a execução do console application e espera que uma tecla seja pressionada.
            Console.Read();
        }
    }
}
