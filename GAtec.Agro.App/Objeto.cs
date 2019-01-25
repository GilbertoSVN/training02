using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.Agro.App
{
    public abstract class Objeto
    {
        public void JogarObjeto()
        {
            // implementações ...

            CalcularSaldo();

            // implementações ...

        }

        public abstract void CalcularSaldo();
    }
}
