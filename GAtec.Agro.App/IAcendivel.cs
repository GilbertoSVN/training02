using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.Agro.App
{
    public interface IAcendivel
    {
        decimal Preco { get; }

        void Acender();
    }

}
