using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_archivos
{
    public class CEntidad
    {
        // el string vale 30, el long vale 8 nos da un total de 62 bytes
        private string nombre;
        private long prt_ent_sig;
        private long ptr_entidad;
        private long ptr_atrib;
        private long ptr_datos;
        private List<CAtributo> lista_Atrb;
        private List<CRegistro> lista_Registros;
        private List<CIndexP> lista_Indices;

        public CEntidad()
        {
            lista_Atrb = new List<CAtributo>();
            Lista_Registros = new List<CRegistro>();
            lista_Indices = new List<CIndexP>();
            ptr_atrib = -1;
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public long Ptr_atrib
        {
            get
            {
                return ptr_atrib;
            }

            set
            {
                ptr_atrib = value;
            }
        }

        public long Ptr_datos
        {
            get
            {
                return ptr_datos;
            }

            set
            {
                ptr_datos = value;
            }
        }

        public long Prt_ent_sig
        {
            get
            {
                return prt_ent_sig;
            }

            set
            {
                prt_ent_sig = value;
            }
        }

        public long Ptr_entidad
        {
            get
            {
                return ptr_entidad;
            }

            set
            {
                ptr_entidad = value;
            }
        }

        public List<CAtributo> Lista_Atrb
        {
            get
            {
                return lista_Atrb;
            }

            set
            {
                lista_Atrb = value;
            }
        }

        internal List<CRegistro> Lista_Registros
        {
            get
            {
                return lista_Registros;
            }

            set
            {
                lista_Registros = value;
            }
        }

        internal List<CIndexP> Lista_Indices
        {
            get
            {
                return lista_Indices;
            }

            set
            {
                lista_Indices = value;
            }
        }
    }
}
