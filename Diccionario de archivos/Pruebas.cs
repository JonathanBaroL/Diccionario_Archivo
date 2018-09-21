using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_de_archivos
{
    public partial class Pruebas : Form
    {
        CDiccionario diccionarioPruebas;
        public Pruebas()
        {
            InitializeComponent();
            diccionarioPruebas = new CDiccionario();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tam = tbString.ToString();
            //MessageBox.Show(tam.Length.ToString());
            diccionarioPruebas.rellenaString(tam);
        }
    }
}
