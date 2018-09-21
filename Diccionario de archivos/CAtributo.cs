using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_archivos
{
    public class CAtributo
    {
        private string nombre;
        private long direccion;
        private char tipo;
        private int tamaño;
        private int indice;
        private long dir_Indice;
        private long sig_Atributo;


        public CAtributo()
        {

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

        public long Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }


        public int Tamaño
        {
            get
            {
                return tamaño;
            }

            set
            {
                tamaño = value;
            }
        }

        public int Indice
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

        public long Dir_Indice
        {
            get
            {
                return dir_Indice;
            }

            set
            {
                dir_Indice = value;
            }
        }

        public long Sig_Atributo
        {
            get
            {
                return sig_Atributo;
            }

            set
            {
                sig_Atributo = value;
            }
        }

        public char Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }
    }
}
