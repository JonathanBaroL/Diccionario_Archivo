using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_archivos
{

    public class CIndexP
    {

        private long dirIndice;
        private string indice;
        private long dirRegistros;
        private List<CIndexP> lista_IndexP;

        public CIndexP()
        {
            lista_IndexP = new List<CIndexP>();
        }

        public long DirIndice
        {
            get
            {
                return dirIndice;
            }

            set
            {
                dirIndice = value;
            }
        }

        public string Indice
        {
            get
            {
                return indice;
            }

            set
            {
                indice = value;
            }
        }

        public long DirRegistros
        {
            get
            {
                return dirRegistros;
            }

            set
            {
                dirRegistros = value;
            }
        }

        public List<CIndexP> Lista_IndexP
        {
            get
            {
                return lista_IndexP;
            }

            set
            {
                lista_IndexP = value;
            }
        }
    }
}
