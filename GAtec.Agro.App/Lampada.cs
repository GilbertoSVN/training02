using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.Agro.App
{
    public class Lampada : Objeto, IAcendivel, IImprimivel
    {
        private bool _aceso;

        public virtual decimal Preco { get; private set; }



        public Lampada()
        {
            _aceso = false;
            Preco = 100;
        }

        ~Lampada()
        {

        }

        public void Acender()
        {
            _aceso = true;

            Console.WriteLine("Lampada.Acender();");
        }

        public void Apagar()
        {
            _aceso = false;
        }

        public bool EstaAceso()
        {
            return _aceso;
        }

        public static Lampada Criar(string tipo)
        {
            Lampada lamp = null;
            if (tipo == "led")
            {
                return new LampadaLed();
            }
            else
            {
                lamp = new Lampada();
            }


            return lamp;
        }

        public override string ToString()
        {
            return "Lampada com preco: " + Preco.ToString();
        }

        public void Imprimir()
        {
            string texto = ToString();

            Console.WriteLine(texto);
        }

        public override void CalcularSaldo()
        {
            Console.WriteLine(Preco * 10);
        }
    }
}
