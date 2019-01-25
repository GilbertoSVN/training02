using System;
using System.Text;
using System.Globalization;
using System.Threading;

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

        // -----------------------------------------------------------------------------------
        // EXEMPLOS
        // -----------------------------------------------------------------------------------


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

        public static void TiposPrimitivos()
        {
            DateTime dt = DateTime.Now;
            dt.AddMinutes(5);

            Boolean g2 = false;
            bool g = true;

            ushort sh = 123;
            uint i = 0;
            ulong l = 123;

            float f = 123.2f;
            double d = 2.4;
            decimal dec = 123;

            Int32 i32 = 0;

            string s = "texto";
            String k = ".Net";

            Console.WriteLine("Hello world! This is a string");
            Console.WriteLine('a');            
        }

        // -----------------------------------------------------------------------------------

        static void Main(string[] args)
        {
            // A implementação foi organizada em métodos com prefixo Exemplo.
            // Aqui você pode chamar um dos métodos de exemplo.

            ExemplosObjetos();

            // para a execução do console application e espera que uma tecla seja pressionada.
            Console.Read();
        }
    }
}
