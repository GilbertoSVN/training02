using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.Agro.App
{
    public class Cigarro : IAcendivel, IImprimivel
    {
        public int Consumo { get; set; }

        public decimal Preco
        {
            get
            {
                return 10m;
            }
        }

        public Cigarro()
        {
            Consumo = 10;
        }

        public void Acender()
        {
            if (Consumo > 0)
                Consumo -= 2;
        }

        public void Imprimir()
        {
            Console.WriteLine("Cigarro com Consumo em: " + Consumo);
        }
    }
}
