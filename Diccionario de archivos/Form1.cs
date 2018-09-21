using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_de_archivos
{
    public partial class Form1 : Form
    {
        public string name;
        public string nameArch;
        public CDiccionario diccionario_datos;
        private FileStream archivo;
        private string atributo;
        private string atributo2;
        int lx, ly;
        int sw, sh;

        public Form1()
        {
            InitializeComponent();
            pantallaCompleta();
            diccionario_datos = new CDiccionario();
            atributo = " ";
            atributo2 = " ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int igual = 0; // Bandera para saber si ya existe una entidad con el mismo nombre
            dataGridView1.Refresh();
            if (textBox1.TextLength > 0)
            {
                string nombre = diccionario_datos.rellenaString(textBox1.Text);
                foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                {
                    if (nombre == ent.Nombre)
                    {
                        igual = 1;
                        MessageBox.Show("Entidad ya existente");
                    }
                }

                if (igual == 0)
                {
                    dataGridView1.Rows.Clear();
                    CEntidad entidad = new CEntidad();
                    entidad.Nombre = textBox1.Text;
                    entidad.Prt_ent_sig = -1;
                    entidad.Ptr_entidad = diccionario_datos.dimeTamArch();
                    diccionario_datos.Inserta_Entidad(entidad);
                    cBox_Entidades1.Items.Clear();
                    cBox_Entidades2.Items.Clear();

                    tb_Cab.Clear();
                    tb_Cab.Text = diccionario_datos.Cab.ToString();
                    foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                    {

                        dataGridView1.Rows.Add(ent.Ptr_entidad, ent.Nombre, ent.Prt_ent_sig, ent.Ptr_atrib, ent.Ptr_datos);
                        cBox_Entidades1.Items.Add(ent.Nombre);
                        cBox_Entidades2.Items.Add(ent.Nombre);

                    }
                }      
            }
            else
            {
                MessageBox.Show("Escribe una entidad");
            }
        }

        private void bnt_Eliminar_Click(object sender, EventArgs e)
        {

            if (textBox1.TextLength > 0)
            {
                string string_elimina = textBox1.Text;
                int tamString = textBox1.TextLength;
                int valor = 0;
                for (int i = tamString; i < 30; i++)
                {
                    string_elimina += " ";
                }

                foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                {
                    if (string_elimina == ent.Nombre)
                    {
                        cBox_Entidades1.Items.Clear();
                        diccionario_datos.Elimina_Entidad(ent);
                        dataGridView1.Rows.Clear();
                        tb_Cab.Clear();
                        tb_Cab.Text = diccionario_datos.Cab.ToString();
                        foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                        {
                            dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                            cBox_Entidades1.Items.Add(enti.Nombre);
                        }
                        break;
                    }
                    else
                    {
                        valor++;
                    }
                }
                if(valor == diccionario_datos.Lista_Ent.Count)
                {
                    if (valor > 0)
                    {
                        MessageBox.Show("No se encontro entidad");
                    }
                }
            }
            else
            {
                MessageBox.Show("Escribe una entidad");
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                lb_Modifica.Visible = true;
                textBox2.Visible = true;
                btn_ok.Visible = true;
            }
            else
            {
                MessageBox.Show("Escribe una entidad");
            }

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0)
            {
                string compara = diccionario_datos.rellenaString(textBox1.Text);
                foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                {
                    if (compara == ent.Nombre)
                    {

                        diccionario_datos.Modifica_Entidad(ent, textBox2.Text);

                        dataGridView1.Rows.Clear();
                        tb_Cab.Clear();
                        tb_Cab.Text = diccionario_datos.Cab.ToString();

                        foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                        {
                            dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                            cBox_Entidades1.Items.Add(ent.Nombre);
                        }
                        break;
                    }
                }
                lb_Modifica.Visible = false;
                textBox2.Visible = false;
                btn_ok.Visible = false;
            }
            else
            {
                MessageBox.Show("Escribe la modificacion");
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leeCab();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            validaciones();

            //guarda en un archivo de texto

            SaveFileDialog saveDialog = new SaveFileDialog();

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                nameArch = saveDialog.FileName;
            }
            escribeCab(-1, nameArch);
            dataGridView1.Rows.Clear();
            tb_Cab.Text = "CAB " + "-1";
            diccionario_datos.Name = nameArch;
            diccionario_datos.Lista_Ent.Clear();
            cBox_Entidades1.Items.Clear();
        }

        private void escribeCab(long tam, string nameArch)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.OpenOrCreate)))
                {
                    escribe.Seek(0, SeekOrigin.Begin);
                    escribe.Write(tam);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private long leeCab()
        {
            try
            {
                archivo = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
                archivo.Seek(0, SeekOrigin.Current);
                using (BinaryReader bin = new BinaryReader(archivo))
                {
                    long var = bin.ReadInt64();

                    //MessageBox.Show(var.ToString() + "  VOY AQUI");
                    return var;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "OH RAYOS");
            }
            return 0;
        }

        private void abrirDiccionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open;
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                nameArch = open.FileName;
                // MessageBox.Show(nameArch);
                diccionario_datos.Name = nameArch;
                long tam = leeCab();
                diccionario_datos.Cab = tam;
                tb_Cab.Clear();
                dataGridView1.Rows.Clear();
                cBox_Entidades1.Items.Clear();
                diccionario_datos.Lista_Ent.Clear();

                diccionario_datos.leeEntidad(tam);

                validaciones();

                tb_Cab.Text = diccionario_datos.Cab.ToString();

                foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                {
                    dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                    cBox_Entidades1.Items.Add(enti.Nombre);
                    cBox_Entidades2.Items.Add(enti.Nombre);
                }
            }
            else
            {
                MessageBox.Show("Inicia una actividad");
            }
        }

        private void cerrarDiccionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView_atributos.Rows.Clear();
            cBox_Entidades1.Items.Clear();
            tbNaAtri.Clear();
            cBox_Entidades1.Text = " ";
            tb_Cab.Text = "Inicia un diccionario";
        }

        private void validaciones()
        {
            textBox1.Visible = true;
            button1.Visible = true;
            label1.Visible = true;
            dataGridView1.Visible = true;
            tb_Cab.Visible = true;
            bnt_Eliminar.Visible = true;
            dataGridView_atributos.Visible = true;
            cBox_Entidades1.Visible = true;
            btn_Modificar.Visible = true;
            btnAgregar2.Visible = true;
            btn_Eliminar2.Visible = true;
            btn_modificar2.Visible = true;
            btn_Consultar__atr.Visible = true;
            lb_Atri.Visible = true;
            lb_Entidad.Visible = true;
            lb_Atri.Visible = true;
            lbTipo.Visible = true;
            lbTipo.Visible = true;
            lbTIndice.Visible = true;
            tbNaAtri.Visible = true;
            cboxTipo.Visible = true;
            numLongi.Visible = true;
            numericIndice.Visible = true;
            lbAtri.Visible = true;
            lbLongi.Visible = true;
            lbEnti2.Visible = true;
        }

        private void btn_Consultar__atr_Click(object sender, EventArgs e)
        {
            if (cBox_Entidades1.Text != "")
            {
                string compara = diccionario_datos.rellenaString(cBox_Entidades1.Text);
                CEntidad Entidad = new CEntidad();
                foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                {
                    if (compara == ent.Nombre)
                    {
                        if (ent.Ptr_atrib != -1)
                        {
                            dataGridView_atributos.Rows.Clear();
                            foreach (CAtributo AT in ent.Lista_Atrb)
                            {
                                //MessageBox.Show(ent.Lista_Atrb.Count().ToString());
                                dataGridView_atributos.Rows.Add(AT.Direccion, AT.Nombre, AT.Tipo, AT.Tamaño, AT.Indice, AT.Dir_Indice, AT.Sig_Atributo);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No tiene atributos");
                        }
                        break;
                    }
                }
            }
        }

        private void btn_Eliminar2_Click(object sender, EventArgs e)
        {
            if (cBox_Entidades1.Text != "" && tbNaAtri.TextLength > 0)
            {
                string compara = diccionario_datos.rellenaString(cBox_Entidades1.Text);
                foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                {
                    if (compara == ent.Nombre)
                    {
                        //MessageBox.Show("Entidad encontrada");
                        string compara2 = diccionario_datos.rellenaString(tbNaAtri.Text);
                        int val = 0;
                        foreach (CAtributo AT in ent.Lista_Atrb)
                        {
                            if (compara2 == AT.Nombre)
                            {
                                //MessageBox.Show("Eliminar atributo " + tbNaAtri.Text);
                                val = 1;
                                diccionario_datos.Elimina_Atributo(AT, ent);
                                dataGridView_atributos.Rows.Clear();

                                foreach (CAtributo ATi in ent.Lista_Atrb)
                                {

                                    dataGridView_atributos.Rows.Add(ATi.Direccion, ATi.Nombre, ATi.Tipo, ATi.Tamaño, ATi.Indice, ATi.Dir_Indice, ATi.Sig_Atributo);
                                }
                                break;
                            }
                        }
                        if (val == 0)
                        {
                            MessageBox.Show("No existe atributo, intenta de nuevo");
                        }
                        dataGridView1.Rows.Clear();
                        foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                        {

                            dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                        }
                        break;
                    }
                }
            }
        }

        private void dataGridView_atributos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                atributo = dataGridView_atributos.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbNaAtri.Text = atributo;
                cboxTipo.Text = dataGridView_atributos.Rows[e.RowIndex].Cells[2].Value.ToString();
                numLongi.Value = Convert.ToInt32(dataGridView_atributos.Rows[e.RowIndex].Cells[3].Value);
                numericIndice.Value = Convert.ToInt32(dataGridView_atributos.Rows[e.RowIndex].Cells[4].Value);
            }
            catch
            {

            }
        }

        private void btn_modificar2_Click(object sender, EventArgs e)
        {
            if (atributo == " ")
                MessageBox.Show("Seleccione un atributo", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (cBox_Entidades1.Text != "")
                {
                    string compara = diccionario_datos.rellenaString(cBox_Entidades1.Text);
                    foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                    {
                        if (compara == ent.Nombre)
                        {
                            //MessageBox.Show("Entidad encontrada");
                            foreach (CAtributo AT in ent.Lista_Atrb)
                            {
                                if (atributo == AT.Nombre)
                                {
                                   //MessageBox.Show("Modificar atributo " + tbNaAtri.Text);

                                    CAtributo ATRI = new CAtributo();

                                    ATRI.Nombre = diccionario_datos.rellenaString(tbNaAtri.Text);
                                    ATRI.Tipo = Convert.ToChar(cboxTipo.Text);
                                    ATRI.Tamaño = Convert.ToInt32(numLongi.Value);
                                    ATRI.Indice = Convert.ToInt32(numericIndice.Value);


                                    diccionario_datos.Modifica_Atributo(ent, ATRI, AT);

                                    dataGridView_atributos.Rows.Clear();
                                    foreach (CAtributo ATi in ent.Lista_Atrb)
                                    {

                                        dataGridView_atributos.Rows.Add(ATi.Direccion, ATi.Nombre, ATi.Tipo, ATi.Tamaño, ATi.Indice, ATi.Dir_Indice, ATi.Sig_Atributo);
                                    }
                                    break;
                                }
                            }
                            dataGridView1.Rows.Clear();
                            foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                            {

                                dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una entidad", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cBox_Entidades2_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("CLICK " + cBox_Entidades2.Text);
            dataGridView2.Columns.Clear();
            dataGridView2.Width = 45;
            dataGridView3.Columns.Clear();
            dataGridView3.Width = 45;
            foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
            {
                if (cBox_Entidades2.Text == ENT.Nombre)
                {
                    foreach (CAtributo ATR in ENT.Lista_Atrb)
                    {
                        DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn columna2 = new DataGridViewTextBoxColumn();
                        columna.HeaderText = ATR.Nombre;
                        columna2.HeaderText = ATR.Nombre;
                        dataGridView2.Columns.Add(columna);
                        dataGridView2.Width += columna.Width;
                        dataGridView3.Columns.Add(columna2);
                        dataGridView3.Width += columna.Width;
                    }
                    for (int j = 0; j < ENT.Lista_Registros.Count; j++)
                    {
                        for (int i = 0; i < dataGridView3.Columns.Count; i++)
                        {
                            dataGridView3.Rows.Add();
                            //MessageBox.Show(ENTIDAD.Lista_Registros[j].Lista_Atributos[i].ToString());
                            dataGridView3.Rows[j].Cells[i].Value = ENT.Lista_Registros[j].Lista_Atributos[i];
                        }
                        //MessageBox.Show("SALI");
                    }
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            /*dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Rows.Clear();*/
            CRegistro reg = new CRegistro();
            CEntidad ENTIDAD = new CEntidad();

            foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
            {
                if (cBox_Entidades2.Text == ENT.Nombre)
                {
                    ENTIDAD = ENT;
                    break;
                }
            }

            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                switch (ENTIDAD.Lista_Atrb[i].Tipo)
                {
                    case 'I'://INT
                        int datoI = Convert.ToInt32(dataGridView2.Rows[0].Cells[i].Value);
                        //MessageBox.Show("DATO:  " + datoI);
                        reg.Lista_Atributos.Add(datoI);
                        break;
                    case 'S'://String
                        string datoS = dataGridView2.Rows[0].Cells[i].Value.ToString();
                        //MessageBox.Show("DATO:  " + datoS);
                        reg.Lista_Atributos.Add(datoS);
                        break;
                }
            }
            ENTIDAD.Lista_Registros.Add(reg);

            for (int j = 0; j < ENTIDAD.Lista_Registros.Count; j++)
            {
                for (int i = 0; i < dataGridView3.Columns.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    //MessageBox.Show(ENTIDAD.Lista_Registros[j].Lista_Atributos[i].ToString());
                    dataGridView3.Rows[j].Cells[i].Value = ENTIDAD.Lista_Registros[j].Lista_Atributos[i];
                }
                //MessageBox.Show("SALI");
            }
        }



        private void btnAgregar2_Click(object sender, EventArgs e)
        {
            int igual = 0;
            if (cBox_Entidades1.Text != "" && tbNaAtri.TextLength > 0)
            {

                foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
                {
                    if (cBox_Entidades1.Text == ENT.Nombre)
                    {
                        foreach (CAtributo ATRIBUTO in ENT.Lista_Atrb)
                        {
                            if (ATRIBUTO.Nombre == diccionario_datos.rellenaString( tbNaAtri.Text))
                            {
                                MessageBox.Show("Ya existe el atributo");
                                igual = 1;
                                break;
                            }
                        }
                        break;
                    }
                }


                if (igual == 0)
                {


                    string compara = diccionario_datos.rellenaString(cBox_Entidades1.Text);
                    CEntidad Entidad = new CEntidad();
                    foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                    {
                        if (compara == ent.Nombre)
                        {
                            CAtributo ATRI = new CAtributo();
                            ATRI.Nombre = diccionario_datos.rellenaString(tbNaAtri.Text);
                            ATRI.Tipo = Convert.ToChar(cboxTipo.Text);
                            ATRI.Tamaño = Convert.ToInt32(numLongi.Value);
                            ATRI.Indice = Convert.ToInt32(numericIndice.Value);
                            diccionario_datos.Inserta_Atributo(ATRI, ent);


                            dataGridView_atributos.Rows.Clear();

                            foreach (CAtributo AT in ent.Lista_Atrb)
                            {
                                dataGridView_atributos.Rows.Add(AT.Direccion, AT.Nombre, AT.Tipo, AT.Tamaño, AT.Indice, AT.Dir_Indice, AT.Sig_Atributo);
                            }

                            break;
                        }
                    }
                    dataGridView1.Rows.Clear();
                    foreach (CEntidad ent in diccionario_datos.Lista_Ent)
                    {

                        dataGridView1.Rows.Add(ent.Ptr_entidad, ent.Nombre, ent.Prt_ent_sig, ent.Ptr_atrib, ent.Ptr_datos);
                    }
                }
            }
            else
            {
                MessageBox.Show("LLena los campos correspondientes");
            }
        }

        public void pantallaCompleta()
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            tabControl1.Width = sw + 310;
            tabControl1.Height = sh + 10;
        }
    }
}
