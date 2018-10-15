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
        public string nameArch2;
        public CDiccionario diccionario_datos;
        private FileStream archivo;
        private CRegistro removeRegistro;
        private string atributo;
        int lx, ly;
        int sw, sh;

        public Form1()
        {
            InitializeComponent();
            pantallaCompleta();
            diccionario_datos = new CDiccionario();
            atributo = " ";
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
                    entidad.Ptr_datos = -1;
                    entidad.Ptr_entidad = diccionario_datos.dimeTamArch(nameArch);
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
                if (valor == diccionario_datos.Lista_Ent.Count)
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
                nameArch += ".dd";
            }
            escribeCab(-1, nameArch);
            dataGridView1.Rows.Clear();
            tb_Cab.Text = "CAB " + "-1";
            diccionario_datos.Name = nameArch;
            diccionario_datos.Lista_Ent.Clear();
            cBox_Entidades1.Items.Clear();
            cBox_Entidades2.Items.Clear();
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
                cBox_Entidades2.Items.Clear();
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
            cBox_Entidades2.Items.Clear();
            tbNaAtri.Clear();
            cBox_Entidades1.Text = " ";
            cBox_Entidades2.Text = " ";
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
                            if (ent.Lista_Registros.Count > 0)
                            {
                                btnAgregar2.Enabled = false;
                                btn_Eliminar2.Enabled = false;
                                btn_modificar2.Enabled = false;
                            }
                            else
                            {
                                btnAgregar2.Enabled = true;
                                btn_Eliminar2.Enabled = true;
                                btn_modificar2.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No tiene atributos");
                            dataGridView_atributos.Rows.Clear();
                            btnAgregar2.Enabled = true;
                            btn_Eliminar2.Enabled = true;
                            btn_modificar2.Enabled = true;
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
            dataGridView2.Columns.Clear();
            dataGridView2.Width = 45;
            dataGridView3.Columns.Clear();
            dataGridView3.Width = 45;

            foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
            {
                if (cBox_Entidades2.Text == ENT.Nombre)
                {
                    if (ENT.Lista_Registros.Count() == 0)
                    {
                        
                        diccionario_datos.Consulta_Registros(ENT);

                        dataGridView1.Rows.Clear();
                        foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                        {
                            dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                        }
                    }

                   /*DataGridViewTextBoxColumn columnaaux1 = new DataGridViewTextBoxColumn();
                    DataGridViewTextBoxColumn columnaaux2 = new DataGridViewTextBoxColumn();
                    columnaaux1.HeaderText = "Direccion";
                    columnaaux2.HeaderText = "Direccion";
                    dataGridView2.Columns.Add(columnaaux1);
                    dataGridView2.Width += columnaaux1.Width;
                    dataGridView3.Columns.Add(columnaaux2);
                    dataGridView3.Width += columnaaux1.Width;*/
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
                    /* columna.HeaderText = "Direccion siguiente";
                     columna2.HeaderText = "Direccion siguiente";
                     dataGridView2.Columns.Add(columna);
                     dataGridView2.Width += columna.Width;
                     dataGridView3.Columns.Add(columna2);
                     dataGridView3.Width += columna.Width;*/
                    for (int j = 0; j < ENT.Lista_Registros.Count; j++)
                    {
                        for (int i = 0; i < dataGridView3.Columns.Count; i++)
                        {
                            try
                            {
                                //MessageBox.Show("Registro");
                                dataGridView3.Rows.Add();
                                //dataGridView3.Rows[j].Cells[0].Value = ENT.Lista_Registros[j].Reg_dir;
                                //MessageBox.Show("paro");
                                //MessageBox.Show(ENT.Lista_Registros[j].Lista_Atributos[i].ToString());
                                dataGridView3.Rows[j].Cells[i].Value = ENT.Lista_Registros[j].Lista_Atributos[i];
                                //MessageBox.Show("paro2");
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                //MessageBox.Show("fuera de intervalo");
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void cboxTipo_TextChanged(object sender, EventArgs e)
        {
            if (cboxTipo.Text == "I")
            {
                numLongi.Value = 4;
                numLongi.Enabled = false;
            }
            else
            {
                if (cboxTipo.Text == "S")
                {
                    numLongi.Value = 30;
                    numLongi.Enabled = true;
                }
            }
        }

        private void btnTXT_Click(object sender, EventArgs e)
        {
            OpenFileDialog open;
            open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                nameArch2 = open.FileName;
                //MessageBox.Show(nameArch2);
                dataGridView3.Columns.Clear();
                dataGridView3.Width = 45;
                foreach (CEntidad ENTIDAD in diccionario_datos.Lista_Ent)
                {
                    ENTIDAD.Ptr_datos = 0;
                    if (cBox_Entidades2.Text == ENTIDAD.Nombre)
                    {

                        StreamReader objReader = new StreamReader(nameArch2);
                        string sLine = "";
                        string sLine2 = "";
                        int i = 0;
                        int j = 0;
                        while (sLine != null)
                        {
                            sLine = objReader.ReadLine();
                            i = 0;
                            j = 0;

                            CRegistro REG = new CRegistro();

                            try
                            {
                                //MessageBox.Show("Entre aqui en el try");
                                while (sLine[i].ToString() != "\n")
                                {
                                    sLine2 += sLine[i];
                                    i++;
                                    if (sLine[i].ToString() == "," || sLine[i].ToString() == "\n")
                                    {
                                        // DATO


                                        switch (ENTIDAD.Lista_Atrb[j].Tipo)
                                        {
                                            case 'I'://INT
                                                int datoI = Convert.ToInt32(sLine2);
                                                //MessageBox.Show("DATO:  " + datoI);
                                                REG.Lista_Atributos.Add(datoI);

                                                break;
                                            case 'S'://String
                                                string datoS = sLine2.ToString();
                                                //MessageBox.Show("DATO:  " + datoS);
                                                datoS = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[j].Tamaño);
                                                REG.Lista_Atributos.Add(datoS);
                                                break;
                                        }

                                        //MessageBox.Show(sLine2);
                                        sLine2 = "";
                                        i++;
                                        j++;
                                    }
                                }
                            }
                            catch
                            {
                                // DATO
                                //MessageBox.Show("Entre aqui en el catch");
                                switch (ENTIDAD.Lista_Atrb[j].Tipo)
                                    {
                                        case 'I'://INT
                                            if(sLine2 != "")
                                            {
                                                int datoI = Convert.ToInt32(sLine2);
                                                //MessageBox.Show("DATO:  " + datoI);
                                                REG.Lista_Atributos.Add(datoI);
                                             }
                                            break;
                                        case 'S'://String
                                        if (sLine2 != "")
                                        {
                                            string datoS = sLine2.ToString();
                                            //MessageBox.Show("DATO:  " + datoS);
                                            datoS = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[j].Tamaño);
                                            REG.Lista_Atributos.Add(datoS);
                                        }
                                            break;
                                    }


                                    //MessageBox.Show(sLine2);
                                    sLine2 = "";
                                    i = 0;
                            }


                            if (REG.Lista_Atributos.Count != 0)
                            {
                                //MessageBox.Show(REG.Lista_Atributos[1].ToString());
                                string nombre = diccionario_datos.NombreReg(ENTIDAD.Nombre);
                                if (ENTIDAD.Lista_Registros.Count() >= 1)
                                {
                                    REG.Reg_dir = diccionario_datos.dimeTamArch(nombre);
                                    REG.Reg_sig = -1;
                                }
                                else
                                {
                                    REG.Reg_dir = 0;
                                    REG.Reg_sig = -1;
                                    ENTIDAD.Ptr_datos = 0;
                                }

                                diccionario_datos.Inserta_Registro2(REG, ENTIDAD, ENTIDAD.Nombre, 0);
                            }

                        }
                        
                        btnAgregar2.Enabled = false;
                        btn_Eliminar2.Enabled = false;
                        btn_modificar2.Enabled = false;

                        objReader.Close();


                        foreach (CAtributo ATR in ENTIDAD.Lista_Atrb)
                        {
                            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columna2 = new DataGridViewTextBoxColumn();
                            columna.HeaderText = ATR.Nombre;
                            columna2.HeaderText = ATR.Nombre;
                            dataGridView3.Columns.Add(columna2);
                            dataGridView3.Width += columna.Width;
                        }

                        for (int o = 0; o < ENTIDAD.Lista_Registros.Count; o++)
                        {
                            for (int u = 0; u < dataGridView3.Columns.Count; u++)
                            {
                                dataGridView3.Rows.Add();
                                    //MessageBox.Show(ENT.Lista_Registros[j].Lista_Atributos[i].ToString());
                                    dataGridView3.Rows[o].Cells[u].Value = ENTIDAD.Lista_Registros[o].Lista_Atributos[u];
  
                            }
                            //MessageBox.Show("SALI");
                        }
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Inicia una actividad");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CEntidad ENTIDAD = new CEntidad();

            foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
            {
                if (cBox_Entidades2.Text == ENT.Nombre)
                {
                    ENTIDAD = ENT;
                    break;
                }
            }
            long val = ENTIDAD.Ptr_datos;
            diccionario_datos.Elimina_Registro(removeRegistro, ENTIDAD);

            if (ENTIDAD.Ptr_datos != val)
            {
                dataGridView1.Rows.Clear();
                foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                {
                    dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                }
                //MessageBox.Show("Cambio el apuntador a datos");
            }

            dataGridView3.Rows.Clear();
            for (int j = 0; j < ENTIDAD.Lista_Registros.Count; j++)
            {
                for (int i = 0; i < dataGridView3.Columns.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[j].Cells[i].Value = ENTIDAD.Lista_Registros[j].Lista_Atributos[i];
                }
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CEntidad ENTIDAD = new CEntidad();

                foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
                {
                    if (cBox_Entidades2.Text == ENT.Nombre)
                    {
                        ENTIDAD = ENT;
                        break;
                    }
                }

                int tam = ENTIDAD.Lista_Atrb.Count();

                for (int i = 0; i < tam; i++)
                {
                    dataGridView2.Rows[0].Cells[i].Value = dataGridView3.Rows[e.RowIndex].Cells[i].Value;
                }

                foreach(CRegistro reg in ENTIDAD.Lista_Registros)
                {
                    if (reg.Lista_Atributos[0] == dataGridView2.Rows[0].Cells[0].Value)
                    {
                        removeRegistro = reg;
                        //MessageBox.Show("Lo encontre " + reg.Lista_Atributos[0] + " = " + removeRegistro.Lista_Atributos[0]);
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cBox_Entidades2.Text != "")
            {
                CEntidad ENTIDAD = new CEntidad();

                foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
                {
                    if (cBox_Entidades2.Text == ENT.Nombre)
                    {
                        ENTIDAD = ENT;
                        break;
                    }
                }

                string nombre = diccionario_datos.NombreReg(ENTIDAD.Nombre);

                foreach (CRegistro reg in ENTIDAD.Lista_Registros)
                {
                    if(reg.Reg_sig == removeRegistro.Reg_dir)
                    {
                        reg.Reg_sig = removeRegistro.Reg_sig;
                        diccionario_datos.Archivo.modifica_reg_sig(reg.Reg_dir, nombre, ENTIDAD, reg);
                        ENTIDAD.Lista_Registros.Remove(removeRegistro);
                        break;
                    }
                    else
                    {
                        if (ENTIDAD.Ptr_datos == removeRegistro.Reg_dir)
                        {
                            ENTIDAD.Ptr_datos = removeRegistro.Reg_sig;
                            diccionario_datos.Archivo.modifica_ent_sig(ENTIDAD.Ptr_entidad, nameArch, ENTIDAD);
                            ENTIDAD.Lista_Registros.Remove(removeRegistro);
                            break;
                        }
                    }

                }


                removeRegistro.Lista_Atributos.Clear();
                removeRegistro.Reg_sig = -1;
                int pos = 0;
                foreach (CAtributo atr in ENTIDAD.Lista_Atrb)
                {
                    switch (atr.Tipo)
                    {
                        case 'I'://INT
                            try
                            {
                                int datoI = Convert.ToInt32(dataGridView2.Rows[0].Cells[pos].Value);
                                //MessageBox.Show(datoI);
                                if (datoI >= 0)
                                {
                                    removeRegistro.Lista_Atributos.Add(datoI);
                                }
                                else
                                {
                                    MessageBox.Show("No puedes ingresar datos negativos");
                                    datoI = 0;
                                    removeRegistro.Lista_Atributos.Add(datoI);
                                }
                                pos++;
                            }
                            catch
                            {
                                int datoI = 0;
                                removeRegistro.Lista_Atributos.Add(datoI);
                                pos++;
                            }
                            break;
                        case 'S'://String
                            try
                            {
                                string datoS = dataGridView2.Rows[0].Cells[pos].Value.ToString();
                                string newString = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[pos].Tamaño);
                                removeRegistro.Lista_Atributos.Add(newString);
                                pos++;
                            }
                            catch
                            {
                                string datoS = "Inserta dato";
                                string newString = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[pos].Tamaño);
                                removeRegistro.Lista_Atributos.Add(newString);
                                pos++;
                            }
                            break;
                    }
                }


                diccionario_datos.Inserta_Registro2(removeRegistro, ENTIDAD, ENTIDAD.Nombre, 1);

                dataGridView3.Rows.Clear();
                for (int j = 0; j < ENTIDAD.Lista_Registros.Count; j++)
                {
                    for (int i = 0; i < dataGridView3.Columns.Count; i++)
                    {
                        dataGridView3.Rows.Add();
                        dataGridView3.Rows[j].Cells[i].Value = ENTIDAD.Lista_Registros[j].Lista_Atributos[i];
                    }
                }



                dataGridView1.Rows.Clear();
                foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                {
                    dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una entidad", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregar2_Click_1(object sender, EventArgs e)
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
                            if (ATRIBUTO.Nombre == diccionario_datos.rellenaString(tbNaAtri.Text))
                            {
                                MessageBox.Show("Ya existe el atributo");
                                igual = 1;
                                break;
                            }
                            if (numericIndice.Value == 1)
                            {
                                if (ATRIBUTO.Indice == numericIndice.Value)
                                {
                                    MessageBox.Show("indice ya registrado");
                                    igual = 1;
                                    break;
                                }
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

        private void bntAddReg2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows[0].Cells[0].Value != null)
            {
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
                            int datoI = 0;
                            try
                            {
                                datoI = Convert.ToInt32(dataGridView2.Rows[0].Cells[i].Value);
                                if (datoI < 0)
                                {
                                    datoI = 0;
                                }
                            }
                            catch
                            {
                                
                            }
                            if(ENTIDAD.Lista_Atrb[i].Indice == 1)
                            {
                                if(ENTIDAD.Lista_Registros.Count() == 0)
                                {
                                    reg.Lista_Atributos.Add(datoI);

                                }
                                else
                                {
                                    int registrado = 0;
                                    foreach (CRegistro REGISTRO in ENTIDAD.Lista_Registros)
                                    {
                                        if (Convert.ToInt32(REGISTRO.Lista_Atributos[i]) == datoI)
                                        {
                                            datoI = 0;
                                            reg.Lista_Atributos.Add(datoI);
                                            registrado = 1;
                                            break;
                                        }
                                    }
                                    if (registrado != 1)
                                    {
                                        reg.Lista_Atributos.Add(datoI);
                                    }
                                }

                            }
                            else
                            {
                                reg.Lista_Atributos.Add(datoI);
                            }
                            break;
                        case 'S'://String
                            try
                            {
                                string datoS = dataGridView2.Rows[0].Cells[i].Value.ToString();
                                string newString = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[i].Tamaño);
                                reg.Lista_Atributos.Add(newString);
                            }
                            catch
                            {
                                string datoS = "Inserta dato";
                                string newString = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[i].Tamaño);
                                reg.Lista_Atributos.Add(newString);
                            }
                            break;
                    }
                }
                
                string nombre = diccionario_datos.NombreReg(ENTIDAD.Nombre);



                if (ENTIDAD.Lista_Registros.Count() >= 1)
                {
                    reg.Reg_dir = diccionario_datos.dimeTamArch(nombre);
                    reg.Reg_sig = -1;
                }
                else
                {
                    reg.Reg_dir = 0;
                    reg.Reg_sig = -1;
                    ENTIDAD.Ptr_datos = 0;
                }

                diccionario_datos.Inserta_Registro2(reg, ENTIDAD, ENTIDAD.Nombre, 0);

                dataGridView1.Rows.Clear();
                foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                {
                    dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                }


                for (int j = 0; j < ENTIDAD.Lista_Registros.Count; j++)
                {
                    for (int i = 0; i < dataGridView3.Columns.Count; i++)
                    {
                        dataGridView3.Rows.Add();
                        dataGridView3.Rows[j].Cells[i].Value = ENTIDAD.Lista_Registros[j].Lista_Atributos[i];
                    }
                }
                btnAgregar2.Enabled = false;
                btn_Eliminar2.Enabled = false;
                btn_modificar2.Enabled = false;
                dataGridView2.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Rellena los campos");
            }
        }

        private void btnAgregar2_click(object sender, EventArgs e)
        {
          
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
