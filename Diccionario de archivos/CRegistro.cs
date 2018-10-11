using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_archivos
{
    public class CRegistro
    {
        private List<object> lista_Atributos;
        private long reg_sig;
        private long reg_dir;

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

        public long Reg_sig
        {
            get
            {
                return reg_sig;
            }

            set
            {
                reg_sig = value;
            }
        }

        public long Reg_dir
        {
            get
            {
                return reg_dir;
            }

            set
            {
                reg_dir = value;
            }
        }
    }
}
