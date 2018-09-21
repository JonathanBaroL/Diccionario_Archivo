using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_archivos
{
    class CRegistro
    {

        private List<object> lista_Atributos;

        public CRegistro()
        {
            lista_Atributos = new List<object>();
        }



        public List<object> Lista_Atributos
        {
            get
            {
                return lista_Atributos;
            }

            set
            {
                lista_Atributos = value;
            }
        }
    }
}
