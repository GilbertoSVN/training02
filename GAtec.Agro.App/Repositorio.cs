using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAtec.Agro.App
{
    public class Repositorio<T>
        where T : class
    {
        private static ArrayList list;

        public Repositorio()
        {
            list = new ArrayList();
        }

        public void Adicionar(T item)
        {
            list.Add(item);
        }

        public IEnumerable Obter()
        {
            return list;
        }



    }
}
