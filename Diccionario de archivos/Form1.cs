﻿using System;
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
        private CRegistro REGGlobal;
        private CEntidad ENTIDADGlobal;
        private string atributo;
        private int indexonBloque;
        private CIndexP indP;
        int lx, ly;
        int sw, sh;
        Nodo nodo;
        int indR = 0;
        int indListNodo = 0;

        public Form1()
        {
            InitializeComponent();
            pantallaCompleta();
            diccionario_datos = new CDiccionario();
            ENTIDADGlobal = new CEntidad();
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
                escribeCab(-1, nameArch);
                dataGridView1.Rows.Clear();
                tb_Cab.Text = "CAB " + "-1";
                diccionario_datos.Name = nameArch;
                diccionario_datos.Lista_Ent.Clear();
                cBox_Entidades1.Items.Clear();
                cBox_Entidades2.Items.Clear();
            }

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
            dgBP.Rows.Clear();
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
            dgRegistroPrimario.Columns.Clear();
            dgRegistroPrimario.Width = 45;
            dgRegistroSecundario.Columns.Clear();
            dgRegistroSecundario.Width = 45;
            dataGridView3.Columns.Clear();
            dataGridView3.Width = 45;
            lbIDX.Text = cBox_Entidades2.Text;

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

                    DataGridViewTextBoxColumn columnaAux = new DataGridViewTextBoxColumn();
                    columnaAux.HeaderText = "Direccion";
                    dataGridView3.Columns.Add(columnaAux);
                    dataGridView3.Width += columnaAux.Width;

                    foreach (CAtributo ATR in ENT.Lista_Atrb)
                    {

                        DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn columna2 = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn columna3 = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn columna4 = new DataGridViewTextBoxColumn();

                        columna.HeaderText = ATR.Nombre;
                        columna2.HeaderText = ATR.Nombre;
                        columna3.HeaderText = ATR.Nombre;
                        columna4.HeaderText = ATR.Nombre;


                        dataGridView2.Columns.Add(columna);
                        dataGridView2.Width += columna.Width;
                        dgRegistroPrimario.Columns.Add(columna3);
                        dgRegistroPrimario.Width += columna3.Width;
                        dgRegistroSecundario.Columns.Add(columna4);
                        dgRegistroSecundario.Width += columna4.Width;

                        dataGridView3.Columns.Add(columna2);
                        dataGridView3.Width += columna2.Width;
                    }

                    DataGridViewTextBoxColumn columnaAux2 = new DataGridViewTextBoxColumn();
                    columnaAux2.HeaderText = "Direccion Siguiente";
                    dataGridView3.Columns.Add(columnaAux2);
                    dataGridView3.Width += columnaAux2.Width;

                    ActualizaReg(ENT);
                    ENTIDADGlobal = ENT;
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
                dataGridView3.Columns.Clear();
                dataGridView3.Width = 45;
                StreamReader objReader = new StreamReader(nameArch2);
                string sLine = "";
                string sLine2 = "";
                int i = 0;
                int j = 0;
                //int tempo = 0;
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


                                switch (ENTIDADGlobal.Lista_Atrb[j].Tipo)
                                {
                                    case 'I'://INT
                                        int datoI = Convert.ToInt32(sLine2);
                                        //MessageBox.Show("DATO:  " + datoI);
                                        REG.Lista_Atributos.Add(datoI);

                                        break;
                                    case 'S'://String
                                        string datoS = sLine2.ToString();
                                        //MessageBox.Show("DATO:  " + datoS);
                                        datoS = diccionario_datos.rellenaStringTAM(datoS, ENTIDADGlobal.Lista_Atrb[j].Tamaño);
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
                        switch (ENTIDADGlobal.Lista_Atrb[j].Tipo)
                        {
                            case 'I'://INT
                                if (sLine2 != "")
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
                                    datoS = diccionario_datos.rellenaStringTAM(datoS, ENTIDADGlobal.Lista_Atrb[j].Tamaño);
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
                        string nombre = diccionario_datos.NombreReg(ENTIDADGlobal.Nombre);
                        if (ENTIDADGlobal.Lista_Registros.Count() >= 1)
                        {
                            REG.Reg_dir = diccionario_datos.dimeTamArch(nombre);
                            REG.Reg_sig = -1;
                        }
                        else
                        {
                            REG.Reg_dir = 0;
                            REG.Reg_sig = -1;
                            ENTIDADGlobal.Ptr_datos = 0;
                        }

                        diccionario_datos.Inserta_Indice(ENTIDADGlobal);

                        diccionario_datos.InsertaRegOnIdx(ENTIDADGlobal, REG);
                        diccionario_datos.InsertaRegOnIdxS(ENTIDADGlobal, REG);
                        TabIndices(ENTIDADGlobal);
                        for (int at = 0; at < ENTIDADGlobal.Lista_Atrb.Count; at++)
                        {
                            if (ENTIDADGlobal.Lista_Atrb[at].Indice == 4)
                            {
                                if (ENTIDADGlobal.ListNod.Count == 0)
                                {
                                    InicializaArbol();
                                }
                                int valor = Convert.ToInt32(REG.Lista_Atributos[at]);
                                InserciónÁrbol(valor, REG.Reg_dir);
                                actualizaDataGridÁrbol();
                                break;
                            }
                        }
                        diccionario_datos.Inserta_Registro2(REG, ENTIDADGlobal, ENTIDADGlobal.Nombre, 0);
                        //Console.Write(tempo++);
                    }

                }

                btnAgregar2.Enabled = false;
                btn_Eliminar2.Enabled = false;
                btn_modificar2.Enabled = false;

                objReader.Close();


                DataGridViewTextBoxColumn columnaAux = new DataGridViewTextBoxColumn();
                columnaAux.HeaderText = "Direccion";
                dataGridView3.Columns.Add(columnaAux);
                dataGridView3.Width += columnaAux.Width;

                foreach (CAtributo ATR in ENTIDADGlobal.Lista_Atrb)
                {

                    DataGridViewTextBoxColumn columna2 = new DataGridViewTextBoxColumn();
                    columna2.HeaderText = ATR.Nombre;


                    dataGridView3.Columns.Add(columna2);
                    dataGridView3.Width += columna2.Width;
                }

                DataGridViewTextBoxColumn columnaAux2 = new DataGridViewTextBoxColumn();
                columnaAux2.HeaderText = "Direccion Siguiente";
                dataGridView3.Columns.Add(columnaAux2);
                dataGridView3.Width += columnaAux2.Width;

                ActualizaReg(ENTIDADGlobal);
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
            ActualizaReg(ENTIDAD);

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
                    dataGridView2.Rows[0].Cells[i].Value = dataGridView3.Rows[e.RowIndex].Cells[i+1].Value;
                }

                foreach(CRegistro reg in ENTIDAD.Lista_Registros)
                {
                    if (reg.Lista_Atributos[0] == dataGridView2.Rows[0].Cells[0].Value)
                    {
                        removeRegistro = reg;
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

                ActualizaReg(ENTIDAD);

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

                // bloque de indices
                diccionario_datos.Inserta_Indice(ENTIDAD);

                diccionario_datos.InsertaRegOnIdx(ENTIDAD, reg);
                diccionario_datos.InsertaRegOnIdxS(ENTIDAD, reg);
                TabIndices(ENTIDAD);
                for(int at = 0; at < ENTIDADGlobal.Lista_Atrb.Count; at++)
                {
                    if(ENTIDADGlobal.Lista_Atrb[at].Indice == 4)
                    {
                        if(ENTIDADGlobal.ListNod.Count == 0)
                        {
                            InicializaArbol();
                        }
                        int valor = Convert.ToInt32(reg.Lista_Atributos[at]);
                        InserciónÁrbol(valor, reg.Reg_dir);
                        actualizaDataGridÁrbol();
                        break;
                    }
                }

                // incercion al archivo de registro
                diccionario_datos.Inserta_Registro2(reg, ENTIDAD, ENTIDAD.Nombre, 0);



                dataGridView1.Rows.Clear();
                foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                {
                    dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                }

                ActualizaReg(ENTIDAD);


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

        private void lbIDX_TextChanged(object sender, EventArgs e)
        {

            dGABC.Rows.Clear();
            dgClvSec.Rows.Clear();
            CEntidad ENTIDAD = new CEntidad();

            foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
            {
                if (cBox_Entidades2.Text == ENT.Nombre)
                {
                    ENTIDAD = ENT;
                    break;
                }
            }

            if (ENTIDAD.Lista_Indices.Count() != 0)
            {
                TabIndices(ENTIDAD);
            }
            else
            {
                foreach (CAtributo atr in ENTIDAD.Lista_Atrb)
                {
                    if (atr.Indice == 2)
                    {
                        string name = diccionario_datos.NombreIdx(ENTIDAD.Nombre);
                        diccionario_datos.Archivo.leeIndices(atr, name, ENTIDAD);
                        diccionario_datos.Archivo.leeBloqueP(ENTIDAD, name, atr.Tamaño);
                        break;
                    }
                }
                foreach (CAtributo atr in ENTIDAD.Lista_Atrb)
                {
                    if (atr.Indice == 3)
                    {
                        string name = diccionario_datos.NombreIdx(ENTIDAD.Nombre);
                        diccionario_datos.Archivo.leeIndices(atr, name, ENTIDAD);
                        diccionario_datos.Archivo.leeBloqueS(ENTIDAD, name, atr.Tamaño);
                        break;
                    }
                }
                if (ENTIDAD.Lista_Indices.Count() != 0)
                {
                    TabIndices(ENTIDAD);
                }
            }
        }

        private void dGABC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgBP.Rows.Clear();
                string indAux = dGABC.Rows[e.RowIndex].Cells[1].Value.ToString();
                //MessageBox.Show(indAux);
                CEntidad ENTIDAD = new CEntidad();

                foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
                {
                    if (cBox_Entidades2.Text == ENT.Nombre)
                    {
                        ENTIDAD = ENT;
                        break;
                    }
                }

                foreach(CIndexP idxP in ENTIDAD.Lista_Indices)
                {
                    //MessageBox.Show(idxP.Indice.ToString() + "   " + indAux);
                    if(idxP.Indice == indAux)
                    {
                        indP = idxP;
                        //MessageBox.Show(idxP.Lista_IndexP.Count.ToString());
                        for (int i = 0; i < idxP.Lista_IndexP.Count(); i++)
                        {
                            dgBP.Rows.Add(idxP.Lista_IndexP[i].DirIndice, idxP.Lista_IndexP[i].Indice, idxP.Lista_IndexP[i].DirRegistros);
                        }
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        private void dgClvSec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgBS.Rows.Clear();
                string indAux = dgClvSec.Rows[e.RowIndex].Cells[1].Value.ToString();
                //MessageBox.Show(indAux);
                CEntidad ENTIDAD = new CEntidad();

                foreach (CEntidad ENT in diccionario_datos.Lista_Ent)
                {
                    if (cBox_Entidades2.Text == ENT.Nombre)
                    {
                        ENTIDAD = ENT;
                        break;
                    }
                }

                foreach (CIndexP idxP in ENTIDAD.Lista_Indices)
                {
                    //MessageBox.Show(idxP.Indice.ToString() + "   " + indAux);
                    if (idxP.Indice == indAux)
                    {
                        indP = idxP;
                        //MessageBox.Show(idxP.Lista_IndexP.Count.ToString());
                        for (int i = 0; i < idxP.Lista_IndexP.Count(); i++)
                        {
                            dgBS.Rows.Add(idxP.Lista_IndexP[i].DirIndice, idxP.Lista_IndexP[i].DirRegistros);
                        }
                        break;
                    }
                }
            }
            catch
            {

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

        private void dgBP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexonBloque = e.RowIndex;
                dgRegistroPrimario.Rows.Clear();
                string indAux = dgBP.Rows[e.RowIndex].Cells[2].Value.ToString();
                string nombre = diccionario_datos.NombreReg(ENTIDADGlobal.Nombre);
                REGGlobal = diccionario_datos.Archivo.leeRegistro(Convert.ToInt64(indAux), nombre, ENTIDADGlobal);

                int tam = ENTIDADGlobal.Lista_Atrb.Count();

                for (int i = 0; i < tam; i++)
                {
                    dgRegistroPrimario.Rows[0].Cells[i].Value = REGGlobal.Lista_Atributos[i];
                }

            }
            catch
            {

            }
        }

        private void dgBS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexonBloque = e.RowIndex;
                dgRegistroSecundario.Rows.Clear();
                string indAux = dgBS.Rows[e.RowIndex].Cells[1].Value.ToString();
                string nombre = diccionario_datos.NombreReg(ENTIDADGlobal.Nombre);
                REGGlobal = diccionario_datos.Archivo.leeRegistro(Convert.ToInt64(indAux), nombre, ENTIDADGlobal);

                int tam = ENTIDADGlobal.Lista_Atrb.Count();

                for (int i = 0; i < tam; i++)
                {
                    dgRegistroSecundario.Rows[0].Cells[i].Value = REGGlobal.Lista_Atributos[i];
                }

            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long val = ENTIDADGlobal.Ptr_datos;

            diccionario_datos.Elimina_Registro(REGGlobal, ENTIDADGlobal);

            if (ENTIDADGlobal.Ptr_datos != val)
            {
                dataGridView1.Rows.Clear();
                foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                {
                    dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                }
            }
            dgRegistroPrimario.Rows.Clear();
            int pos = 0;
            for (int j = 0; j < ENTIDADGlobal.Lista_Atrb.Count(); j++)
            {
                if (ENTIDADGlobal.Lista_Atrb[j].Indice == 2)
                {
                    pos = j;
                    break;
                }
            }

            if(ENTIDADGlobal.Lista_Atrb[pos].Tipo.ToString() == "S")
            {
                for (int i = 27; i < ENTIDADGlobal.Lista_Indices.Count(); i++)
                {
                    int res = 0;
                    foreach(CIndexP indiceAux in ENTIDADGlobal.Lista_Indices[i].Lista_IndexP)
                    {
                        if(indiceAux.DirRegistros == indP.Lista_IndexP[indexonBloque].DirRegistros)
                        {
                            indiceAux.DirRegistros = -1;
                            string nameInx = diccionario_datos.NombreIdx(ENTIDADGlobal.Nombre);
                            diccionario_datos.Archivo.modifica_IndiceS(indiceAux.DirIndice, nameInx, indiceAux);
                            res = 1;
                            break;
                        }
                    }
                    if(res == 1)
                    {
                        break;
                    }
                }
            }

            
            string cadena = diccionario_datos.stringVacio(ENTIDADGlobal.Lista_Atrb[pos].Tamaño);
            indP.Lista_IndexP[indexonBloque].Indice = cadena;
            indP.Lista_IndexP[indexonBloque].DirRegistros = -1;
            dgBP.Rows[indexonBloque].Cells[1].Value = cadena;
            dgBP.Rows[indexonBloque].Cells[2].Value = -1;
            string nameInd = diccionario_datos.NombreIdx(ENTIDADGlobal.Nombre);
            diccionario_datos.Archivo.modifica_Indice(indP.Lista_IndexP[indexonBloque].DirIndice, nameInd, indP.Lista_IndexP[indexonBloque], ENTIDADGlobal.Lista_Atrb[pos].Tamaño);

            dataGridView3.Rows.Clear();
            ActualizaReg(ENTIDADGlobal);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cBox_Entidades2.Text != "")
            {
                CEntidad ENTIDAD = ENTIDADGlobal;

                string nombre = diccionario_datos.NombreReg(ENTIDAD.Nombre);

                foreach (CRegistro reg in ENTIDAD.Lista_Registros)
                {
                    if (reg.Reg_sig == REGGlobal.Reg_dir)
                    {
                        reg.Reg_sig = REGGlobal.Reg_sig;
                        diccionario_datos.Archivo.modifica_reg_sig(REGGlobal.Reg_dir, nombre, ENTIDAD, REGGlobal);
                        foreach (CRegistro auxreg in ENTIDAD.Lista_Registros)
                        {
                            if (auxreg.Reg_dir == REGGlobal.Reg_dir)
                            {
                                ENTIDAD.Lista_Registros.Remove(auxreg);
                                break;
                            }
                        }
                        break;
                    }
                    else
                    {
                        if (ENTIDAD.Ptr_datos == REGGlobal.Reg_dir)
                        {
                            ENTIDAD.Ptr_datos = REGGlobal.Reg_sig;
                            diccionario_datos.Archivo.modifica_ent_sig(ENTIDAD.Ptr_entidad, nameArch, ENTIDAD);
                            foreach (CRegistro auxreg in ENTIDAD.Lista_Registros)
                            {
                                if (auxreg.Reg_dir == REGGlobal.Reg_dir)
                                {
                                    ENTIDAD.Lista_Registros.Remove(auxreg);
                                    break;
                                }
                            }
                            break;
                        }
                    }

                }


                REGGlobal.Lista_Atributos.Clear();
                REGGlobal.Reg_sig = -1;
                int pos = 0;
                foreach (CAtributo atr in ENTIDAD.Lista_Atrb)
                {
                    switch (atr.Tipo)
                    {
                        case 'I'://INT
                            try
                            {
                                int datoI = Convert.ToInt32(dgRegistroPrimario.Rows[0].Cells[pos].Value);
                                //MessageBox.Show(datoI);
                                if (datoI >= 0)
                                {
                                    REGGlobal.Lista_Atributos.Add(datoI);
                                }
                                else
                                {
                                    MessageBox.Show("No puedes ingresar datos negativos");
                                    datoI = 0;
                                    REGGlobal.Lista_Atributos.Add(datoI);
                                }
                                pos++;
                            }
                            catch
                            {
                                int datoI = 0;
                                REGGlobal.Lista_Atributos.Add(datoI);
                                pos++;
                            }
                            break;
                        case 'S'://String
                            try
                            {
                                string datoS = dgRegistroPrimario.Rows[0].Cells[pos].Value.ToString();
                                string newString = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[pos].Tamaño);
                                REGGlobal.Lista_Atributos.Add(newString);
                                pos++;
                            }
                            catch
                            {
                                string datoS = "Inserta dato";
                                string newString = diccionario_datos.rellenaStringTAM(datoS, ENTIDAD.Lista_Atrb[pos].Tamaño);
                                REGGlobal.Lista_Atributos.Add(newString);
                                pos++;
                            }
                            break;
                    }
                }


                diccionario_datos.Inserta_Registro2(REGGlobal, ENTIDAD, ENTIDAD.Nombre, 1);



                for (int j = 0; j < ENTIDADGlobal.Lista_Atrb.Count(); j++)
                {
                    if (ENTIDADGlobal.Lista_Atrb[j].Indice == 2)
                    {
                        pos = j;
                        break;
                    }
                }
                indP.Lista_IndexP[indexonBloque].Indice = dgRegistroPrimario.Rows[0].Cells[pos].Value.ToString();
                string nameInd = diccionario_datos.NombreIdx(ENTIDADGlobal.Nombre);

                diccionario_datos.Archivo.modifica_Indice(indP.Lista_IndexP[indexonBloque].DirIndice, nameInd, indP.Lista_IndexP[indexonBloque], ENTIDADGlobal.Lista_Atrb[pos].Tamaño);
                dgBP.Rows[indexonBloque].Cells[1].Value = indP.Lista_IndexP[indexonBloque].Indice;
                ActualizaReg(ENTIDAD);

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

        private void button6_Click(object sender, EventArgs e)
        {
            long val = ENTIDADGlobal.Ptr_datos;

            diccionario_datos.Elimina_Registro(REGGlobal, ENTIDADGlobal);

            if (ENTIDADGlobal.Ptr_datos != val)
            {
                dataGridView1.Rows.Clear();
                foreach (CEntidad enti in diccionario_datos.Lista_Ent)
                {
                    dataGridView1.Rows.Add(enti.Ptr_entidad, enti.Nombre, enti.Prt_ent_sig, enti.Ptr_atrib, enti.Ptr_datos);
                }
            }
            dgRegistroSecundario.Rows.Clear();
            int pos = 0;
            for (int j = 0; j < ENTIDADGlobal.Lista_Atrb.Count(); j++)
            {
                if (ENTIDADGlobal.Lista_Atrb[j].Indice == 2)
                {
                    pos = j;
                    break;
                }
            }


            if (ENTIDADGlobal.Lista_Atrb[pos].Tipo.ToString() == "S")
            {
                string nameInd = diccionario_datos.NombreIdx(ENTIDADGlobal.Nombre);
                for (int i = 0; i < 27; i++)
                {
                    int res = 0;
                    foreach (CIndexP indiceAux in ENTIDADGlobal.Lista_Indices[i].Lista_IndexP)
                    {
                        if (indiceAux.DirRegistros == indP.Lista_IndexP[indexonBloque].DirRegistros)
                        {
                            string cadena = diccionario_datos.stringVacio(ENTIDADGlobal.Lista_Atrb[pos].Tamaño);
                            indiceAux.Indice = cadena;
                            indiceAux.DirRegistros = -1;
                            diccionario_datos.Archivo.modifica_Indice(indiceAux.DirIndice, nameInd, indiceAux, ENTIDADGlobal.Lista_Atrb[pos].Tamaño);
                            //dgBS.Rows[indexonBloque].Cells[1].Value = -1;
                            res = 1;
                            break;
                        }
                    }
                    if (res == 1)
                    {
                        break;
                    }
                }

                for (int i = 27; i < ENTIDADGlobal.Lista_Indices.Count(); i++)
                {
                    int res = 0;
                    foreach (CIndexP indiceAux in ENTIDADGlobal.Lista_Indices[i].Lista_IndexP)
                    {
                        if (indiceAux.DirRegistros == indP.Lista_IndexP[indexonBloque].DirRegistros)
                        {
                            indiceAux.DirRegistros = -1;

                            diccionario_datos.Archivo.modifica_IndiceS(indiceAux.DirIndice, nameInd, indiceAux);
                            dgBS.Rows[indexonBloque].Cells[1].Value = -1;
                            res = 1;
                            break;
                        }
                    }
                    if (res == 1)
                    {
                        break;
                    }
                }
            }


            /*string cadena = diccionario_datos.stringVacio(ENTIDADGlobal.Lista_Atrb[pos].Tamaño);
            indP.Lista_IndexP[indexonBloque].Indice = cadena;
            indP.Lista_IndexP[indexonBloque].DirRegistros = -1;
            dgBP.Rows[indexonBloque].Cells[1].Value = cadena;
            dgBP.Rows[indexonBloque].Cells[2].Value = -1;*/

            dataGridView3.Rows.Clear();
            ActualizaReg(ENTIDADGlobal);
        }

        public void TabIndices(CEntidad ENTIDAD)
        {
            dgClvSec.Rows.Clear();
            dGABC.Rows.Clear();
            //MessageBox.Show(ENTIDAD.Lista_Indices.Count().ToString());
            foreach (CAtributo atr in ENTIDAD.Lista_Atrb)
            {
                if (atr.Indice == 2)
                {
                    if (atr.Tipo == 'S')
                    {
                        for (int i = 0; i < 27; i++)
                        {
                            //dGABC.Rows.Add((i * 16) + i, abc[i], -1);
                            dGABC.Rows.Add(ENTIDAD.Lista_Indices[i].DirIndice, ENTIDAD.Lista_Indices[i].Indice, ENTIDAD.Lista_Indices[i].DirRegistros);
                        }
                    }
                    if (atr.Tipo == 'I')
                    {
                        for (int i = 0; i < 0; i++)
                        {
                            dGABC.Rows.Add((i * 16) + i, i, -1);
                        }
                    }
                    break;
                }
            }
            foreach (CAtributo atr in ENTIDAD.Lista_Atrb)
            {
                if (atr.Indice == 3)
                {
                    int value = 27;
                    for (int i = 0; i < 50; i++)
                    {
                        //dgClvSec.Rows.Add(value + (20 * i), "null", -1);
                        dgClvSec.Rows.Add(ENTIDAD.Lista_Indices[i + value].DirIndice, ENTIDAD.Lista_Indices[i + value].Indice, ENTIDAD.Lista_Indices[i + value].DirRegistros);
                    }
                    break;
                }
            }
        }

        public void ActualizaReg(CEntidad ENTIDAD)
        {
            dataGridView3.Rows.Clear();
            for (int j = 0; j < ENTIDAD.Lista_Registros.Count; j++)
            {
                dataGridView3.Rows.Add();
                for (int i = 0; i < dataGridView3.Columns.Count; i++)
                {
                    try
                    {
                        if (i == 0)
                        {
                            dataGridView3.Rows[j].Cells[i].Value = ENTIDAD.Lista_Registros[j].Reg_dir;
                        }
                        else
                        {
                            if (i == dataGridView3.Columns.Count - 1)
                            {
                                dataGridView3.Rows[j].Cells[i].Value = ENTIDAD.Lista_Registros[j].Reg_sig;
                            }
                            else
                            {

                                dataGridView3.Rows[j].Cells[i].Value = ENTIDAD.Lista_Registros[j].Lista_Atributos[i - 1];
                            }
                        }

                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        //MessageBox.Show("fuera de intervalo");
                    }
                }
            }
        }
       
        private void InserciónÁrbol(int clave, long dir)
        {
            Nodo auxR = null;
            string fileNameIdx = diccionario_datos.NombreIdx(ENTIDADGlobal.Nombre);
            FileInfo fileidx = new FileInfo(fileNameIdx);
            long pos = fileidx.Length;

            for (int i = 0; i < ENTIDADGlobal.ListNod.Count; i++)//Checa si existe la Raiz
            {
                if (ENTIDADGlobal.ListNod[i].cTipo == 'R')
                {
                    auxR = ENTIDADGlobal.ListNod[i];
                    break;
                }
            }

            if (auxR == null)
            {
                if (nodo != null)
                {
                    if (!nodo.liClaves.Contains(clave))
                    {
                        if (nodo.liClaves.Count < 4)
                        {
                            MenosDe4(clave, dir);
                        }
                        else
                        {
                            IgualOMAy(clave, dir, ref pos, ref auxR);
                        }
                    }
                }
                else
                {
                    InicializaArbol();
                }
                if (auxR != null)
                {
                    /////
                    int iNumAtr = ENTIDADGlobal.Lista_Atrb.Count;

                    for (int i = 0; i < iNumAtr; i++)
                    {
                        CAtributo auxAt = ENTIDADGlobal.Lista_Atrb[i];

                        if (auxAt.Indice == 4)
                        {
                            auxAt.Dir_Indice = auxR.lDirNod;
                            string fileNameDD = nameArch;
                            diccionario_datos.Archivo.modifica_atri_sig(auxAt.Direccion, fileNameDD, auxAt);
                        }
                    }
                }
            }
            else
            {
                Nodo itNod, ndMan, nodAuxr = InsertarEn(auxR, clave);
                ndMan = auxR;
                if (nodAuxr.cTipo == 'I')
                {
                    ndMan = nodAuxr;
                    itNod = InsertarEn(nodAuxr, clave);
                    nodAuxr = itNod;
                }

                if (nodAuxr.liClaves.Count < 4)
                {
                    nodAuxr.AddCveDir(clave, dir);

                    if (nodAuxr.liClaves.Count > 1)
                        OrdenaDatosNodo(nodAuxr);

                    ENTIDADGlobal.ListNod[indListNodo] = nodAuxr;
                }
                else
                {
                    if (ndMan.Equals(auxR))
                    {
                        Nodo auxNod = nodAuxr;
                        nodo = new Nodo(pos, 'H');
                        pos += 65;
                        auxNod.AddCveDir(clave, dir);
                        OrdenaDatosNodo(auxNod);
                        Mitosis(auxNod, nodo);
                        OrdenaDatosNodo(nodo);
                        ENTIDADGlobal.ListNod[indListNodo] = auxNod;
                        ENTIDADGlobal.ListNod.Add(nodo);


                        if (auxR.liClaves.Count < 4)
                        {
                            auxR.AddCveDir(nodo.liClaves[0], nodo.lDirNod);
                            OrdenaDatosNodo(auxR);
                            ENTIDADGlobal.ListNod[indR] = auxR;
                        }
                        else
                        {
                            Nodo nint = new Nodo(pos, 'I');
                            pos += 65;
                            Nodo nAux = auxR;
                            nAux.liClaves.Add(nodo.liClaves[0]);
                            OrdenaDatosNodo(nAux);
                            Mitosis(nAux, nint);
                            OrdenaDatosNodo(nint);
                            nAux.cTipo = 'I';
                            for (int i = 0; i < ENTIDADGlobal.ListNod.Count; i++)
                            {
                                if (ENTIDADGlobal.ListNod[i].lDirNod == auxR.lDirNod)
                                    ENTIDADGlobal.ListNod[i] = nAux;
                            }

                            auxR = new Nodo();
                            auxR.cTipo = 'R';
                            auxR.llDirecciones.Add(nAux.lDirNod);
                            auxR.AddCveDir(nint.liClaves[0], nint.lDirNod);
                            indR = ENTIDADGlobal.ListNod.Count - 1;
                            nint.liClaves.RemoveAt(0);
                            nint.llDirecciones.RemoveAt(0);
                            nint.llDirecciones.Add(nodo.lDirNod);

                            auxR.lDirNod = pos;
                            pos += 65;
                            ENTIDADGlobal.ListNod.Add(nint);
                            ENTIDADGlobal.ListNod.Add(auxR);
                        }
                    }
                    else
                    {
                        Nodo auxNod = nodAuxr;
                        nodo = new Nodo(pos, 'H');
                        pos += 65;
                        auxNod.AddCveDir(clave, dir);
                        OrdenaDatosNodo(auxNod);
                        Mitosis(auxNod, nodo);
                        OrdenaDatosNodo(nodo);
                        ENTIDADGlobal.ListNod[indListNodo] = auxNod;
                        ENTIDADGlobal.ListNod.Add(nodo);


                        if (ndMan.liClaves.Count < 4)
                        {
                            Nodo nMid = ndMan;
                            nMid.AddCveDir(nodo.liClaves[0], nodo.lDirNod);
                            OrdenaDatosNodo(nMid);
                            for (int i = 0; i < ENTIDADGlobal.ListNod.Count; i++)
                            {
                                if (ENTIDADGlobal.ListNod[i].lDirNod == nMid.lDirNod)
                                    ENTIDADGlobal.ListNod[i] = nMid;
                            }

                            nMid = ndMan;
                        }
                        else
                        {
                            Nodo Nodnint = new Nodo(pos, 'I');
                            pos += 65;

                            ndMan.liClaves.Add(nodo.liClaves[0]);
                            OrdenaDatosNodo(ndMan);
                            auxR.AddCveDir(ndMan.liClaves[2], Nodnint.lDirNod);
                            Mitosis(ndMan, Nodnint);
                            Nodnint.llDirecciones.Add(nodo.lDirNod);
                            Nodnint.llDirecciones.RemoveAt(0);
                            Nodnint.liClaves.RemoveAt(0);

                            OrdenaDatosNodo(Nodnint);
                            OrdenaDatosNodo(auxR);

                            ENTIDADGlobal.ListNod.Add(Nodnint);

                            for (int i = 0; i < ENTIDADGlobal.ListNod.Count; i++)
                            {
                                if (ENTIDADGlobal.ListNod[i].lDirNod == ndMan.lDirNod)
                                    ENTIDADGlobal.ListNod[i] = ndMan;
                                if (ENTIDADGlobal.ListNod[i].lDirNod == auxR.lDirNod)
                                    ENTIDADGlobal.ListNod[i] = auxR;

                            }
                        }
                    }
                }
            }
            if (auxR != null)
            {
                int iNumAtr = ENTIDADGlobal.Lista_Atrb.Count;

                for (int i = 0; i < iNumAtr; i++)
                {
                    CAtributo auxAt = ENTIDADGlobal.Lista_Atrb[i];

                    if (auxAt.Indice == 4)
                    {
                        auxAt.Dir_Indice = auxR.lDirNod;
                        string fileNameDD = nameArch;
                        diccionario_datos.Archivo.modifica_atri_sig(auxAt.Direccion, fileNameDD, auxAt);
                    }
                }
            }
        }

        private void MenosDe4(int clave, long dir)
        {
            nodo.AddCveDir(clave, dir);
            if (nodo.liClaves.Count > 1)
                OrdenaDatosNodo(nodo);
            ENTIDADGlobal.ListNod[0] = nodo;
        }

        private void OrdenaDatosNodo(Nodo nod)
        {
            List<Pares> lpares = new List<Pares>();
            bool eq = false;
            if (nod.cTipo == 'H')
            {
                for (int i = 0; i < nod.liClaves.Count; i++)
                {
                    Pares p = new Pares(nod.llDirecciones[i], nod.liClaves[i]);
                    lpares.Add(p);
                }

                lpares = lpares.OrderBy(p => p.iDato).ToList();

                for (int i = 0; i < nod.liClaves.Count; i++)
                {
                    nod.llDirecciones[i] = lpares[i].lDir;
                    nod.liClaves[i] = lpares[i].iDato;
                }
            }
            else
            {
                if (nod.liClaves.Count == nod.llDirecciones.Count)
                {
                    eq = true;
                }
                long tem = 0;
                for (int i = 0; i < nod.liClaves.Count; i++)
                {
                    if (i == 0 && !eq)//&& !(nod.liClaves.Count == nod.llDirecciones.Count))
                    {
                        tem = nod.llDirecciones[i];
                        nod.llDirecciones.RemoveAt(i);
                    }
                    Pares p = new Pares(nod.llDirecciones[i], nod.liClaves[i]);
                    lpares.Add(p);
                }

                lpares = lpares.OrderBy(p => p.iDato).ToList();

                for (int i = 0; i < nod.liClaves.Count; i++)
                {
                    nod.llDirecciones[i] = lpares[i].lDir;
                    nod.liClaves[i] = lpares[i].iDato;
                }
                if (!eq)
                    nod.llDirecciones.Insert(0, tem);
            }
        }

        private void IgualOMAy(int clave, long dir, ref long pos, ref Nodo auxR)
        {
            Nodo auxNod = nodo;
            nodo = new Nodo(pos, 'H');
            pos += 65;
            auxNod.AddCveDir(clave, dir);
            OrdenaDatosNodo(auxNod);
            Mitosis(auxNod, nodo);
            OrdenaDatosNodo(nodo);
            ENTIDADGlobal.ListNod[0] = auxNod;
            ENTIDADGlobal.ListNod.Add(nodo);
            auxR = new Nodo(pos, 'R');
            pos += 65;
            auxR.llDirecciones.Add(auxNod.lDirNod);
            auxR.llDirecciones.Add(nodo.lDirNod);
            auxR.liClaves.Add(nodo.liClaves[0]);
            ENTIDADGlobal.ListNod.Add(auxR);
            indR = ENTIDADGlobal.ListNod.Count - 1;
        }

        private void Mitosis(Nodo auxNod, Nodo nde)
        {
            int j = 0;

            while (auxNod.liClaves.Count != 2)
            {

                int n = 2;
                nde.liClaves.Add(auxNod.liClaves[n]);

                if (nde.cTipo == 'I')
                {
                    if (j == 0)
                    {
                        nde.llDirecciones.Add(auxNod.llDirecciones[n]);
                        nde.llDirecciones.Add(auxNod.llDirecciones[n + 1]);
                    }
                    else
                        if (j < 2)
                        nde.llDirecciones.Add(auxNod.llDirecciones[n + 1]);
                }
                else
                    nde.llDirecciones.Add(auxNod.llDirecciones[n]);
                auxNod.liClaves.RemoveAt(n);
                if (nde.cTipo == 'I')
                {
                    if (j < 2)
                        auxNod.llDirecciones.RemoveAt(n + 1);
                }
                else
                {
                    auxNod.llDirecciones.RemoveAt(n);
                }
                j++;
            }
        }

        private void InicializaArbol()
        {
            int iNumAtr = ENTIDADGlobal.Lista_Atrb.Count;
            string fileNameIdx = diccionario_datos.NombreIdx(ENTIDADGlobal.Nombre);
            FileInfo f = new FileInfo(fileNameIdx);

            for (int i = 0; i < iNumAtr; i++)
            {
                CAtributo auxAt = ENTIDADGlobal.Lista_Atrb[i];

                if (auxAt.Indice == 4)
                {
                    nodo = new Nodo(f.Length, 'H');
                    ENTIDADGlobal.ListNod.Add(nodo);
                    auxAt.Dir_Indice = f.Length;
                    string fileNameDD = nameArch;
                    diccionario_datos.Archivo.modifica_atri_sig(auxAt.Direccion,fileNameDD, auxAt);
                }
            }
        }

        private Nodo InsertarEn(Nodo Naux, int clave)
        {
            Nodo aux = new Nodo();
            for (int i = 0; i < Naux.liClaves.Count; i++)
            {
                if (clave > Naux.liClaves[i])
                {
                    for (int ni = 0; ni < ENTIDADGlobal.ListNod.Count; ni++)
                    {
                        if (ENTIDADGlobal.ListNod[ni].lDirNod == Naux.llDirecciones[i + 1])
                        {
                            aux = ENTIDADGlobal.ListNod[ni];
                            indListNodo = ni;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < ENTIDADGlobal.ListNod.Count; j++)
                    {
                        if (ENTIDADGlobal.ListNod[j].lDirNod == Naux.llDirecciones[i])
                        {
                            aux = ENTIDADGlobal.ListNod[j];
                            indListNodo = j;
                        }
                    }
                    i = Naux.liClaves.Count;
                }
            }
            return aux;
        }

        private void actualizaDataGridÁrbol()
        {
            CEntidad auxEnt = ENTIDADGlobal;
            dgArbol.Rows.Clear();
            int j = 0, i = 0;
            //////////
            if (auxEnt.ListNod.Count != 0)
            {
                for (i = 0; i < auxEnt.ListNod.Count; i++)
                {

                    dgArbol.Rows.Add();
                    int n = 0;
                    int m = 0;
                    for (j = 0; j < dgArbol.ColumnCount; j++)
                    {
                        if (j < 2)
                        {
                            dgArbol.Rows[i].Cells[j].Value = auxEnt.ListNod[i].lDirNod;
                            j++;
                            dgArbol.Rows[i].Cells[j].Value = auxEnt.ListNod[i].cTipo;
                            j++;
                        }

                        if (n < auxEnt.ListNod[i].llDirecciones.Count)
                        {
                            dgArbol.Rows[i].Cells[j].Value = auxEnt.ListNod[i].llDirecciones[n];
                            n++;
                            j++;
                        }
                        if (m < auxEnt.ListNod[i].liClaves.Count)
                        {
                            dgArbol.Rows[i].Cells[j].Value = auxEnt.ListNod[i].liClaves[m];
                            m++;
                        }

                    }
                }
            }
        }

    }
}
