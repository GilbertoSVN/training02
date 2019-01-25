using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.Agro.App
{
    public class LampadaLed : Lampada
    {

        public override decimal Preco
        {
            get
            {
                return base.Preco + (base.Preco * 0.2m);
            }
        }

        public LampadaLed()
        {
        }

        public void TrocarDiodo(string marca, long frequencia = 100)
        {
            Console.WriteLine("Trocando diodo da lampada de led " + marca + " com frequencia " + frequencia);
        }

        public new void Acender()
        {
            base.Acender();

            Console.WriteLine("LampadaDeLed.Acender();");
        }

    }
}
