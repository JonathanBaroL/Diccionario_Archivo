using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_de_archivos
{

    public class Nodo
    {
        public long lDirNod = -1;
        public char cTipo = 'H';
        public List<long> llDirecciones;
        public List<int> liClaves;

        public Nodo()
        {
            llDirecciones = new List<long>();
            liClaves = new List<int>();
        }

        public Nodo(long lDirNod, char cTipo)
        {
            this.cTipo = cTipo;
            this.lDirNod = lDirNod;
            llDirecciones = new List<long>();
            liClaves = new List<int>();
        }

        public void AddCveDir(int clave, long dir)
        {
            liClaves.Add(clave);
            llDirecciones.Add(dir);
        }
    }
}
