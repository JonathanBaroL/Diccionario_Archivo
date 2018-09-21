namespace Diccionario_de_archivos
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirDiccionarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarDiccionarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cBox_Entidades2 = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView_atributos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dir_indice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sig_Atributo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAgregar2 = new System.Windows.Forms.Button();
            this.lbTIndice = new System.Windows.Forms.Label();
            this.lbEnti2 = new System.Windows.Forms.Label();
            this.lbLongi = new System.Windows.Forms.Label();
            this.cBox_Entidades1 = new System.Windows.Forms.ComboBox();
            this.lbTipo = new System.Windows.Forms.Label();
            this.numericIndice = new System.Windows.Forms.NumericUpDown();
            this.tbNaAtri = new System.Windows.Forms.TextBox();
            this.numLongi = new System.Windows.Forms.NumericUpDown();
            this.lbAtri = new System.Windows.Forms.Label();
            this.cboxTipo = new System.Windows.Forms.ComboBox();
            this.btn_Eliminar2 = new System.Windows.Forms.Button();
            this.lb_Atri = new System.Windows.Forms.Label();
            this.btn_modificar2 = new System.Windows.Forms.Button();
            this.btn_Consultar__atr = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tb_Cab = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bnt_Eliminar = new System.Windows.Forms.Button();
            this.lb_Entidad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Modificar = new System.Windows.Forms.Button();
            this.lb_Modifica = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_atributos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericIndice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongi)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DimGray;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1902, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirDiccionarioToolStripMenuItem,
            this.cerrarDiccionarioToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirDiccionarioToolStripMenuItem
            // 
            this.abrirDiccionarioToolStripMenuItem.Name = "abrirDiccionarioToolStripMenuItem";
            this.abrirDiccionarioToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.abrirDiccionarioToolStripMenuItem.Text = "Abrir Diccionario";
            this.abrirDiccionarioToolStripMenuItem.Click += new System.EventHandler(this.abrirDiccionarioToolStripMenuItem_Click);
            // 
            // cerrarDiccionarioToolStripMenuItem
            // 
            this.cerrarDiccionarioToolStripMenuItem.Name = "cerrarDiccionarioToolStripMenuItem";
            this.cerrarDiccionarioToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.cerrarDiccionarioToolStripMenuItem.Text = "Cerrar Diccionario";
            this.cerrarDiccionarioToolStripMenuItem.Click += new System.EventHandler(this.cerrarDiccionarioToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.DimGray;
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cBox_Entidades2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1230, 438);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Registro";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(115, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 28);
            this.label5.TabIndex = 41;
            this.label5.Text = "Tus registros";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(120, 312);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(812, 123);
            this.dataGridView3.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(115, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 28);
            this.label4.TabIndex = 39;
            this.label4.Text = "Da de alta un registro";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(120, 162);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(812, 106);
            this.dataGridView2.TabIndex = 38;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(430, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 32);
            this.button2.TabIndex = 33;
            this.button2.Text = "Agregar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(555, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 32);
            this.button3.TabIndex = 34;
            this.button3.Text = "Eliminar";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(597, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 28);
            this.label3.TabIndex = 37;
            this.label3.Text = "Registro";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(680, 63);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 32);
            this.button4.TabIndex = 35;
            this.button4.Text = "Modificar";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(805, 64);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(119, 32);
            this.button5.TabIndex = 36;
            this.button5.Text = "Consultar";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 32;
            this.label2.Text = "Entidades";
            // 
            // cBox_Entidades2
            // 
            this.cBox_Entidades2.FormattingEnabled = true;
            this.cBox_Entidades2.Location = new System.Drawing.Point(150, 64);
            this.cBox_Entidades2.Name = "cBox_Entidades2";
            this.cBox_Entidades2.Size = new System.Drawing.Size(179, 24);
            this.cBox_Entidades2.TabIndex = 31;
            this.cBox_Entidades2.TextChanged += new System.EventHandler(this.cBox_Entidades2_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gray;
            this.tabPage2.Controls.Add(this.dataGridView_atributos);
            this.tabPage2.Controls.Add(this.btnAgregar2);
            this.tabPage2.Controls.Add(this.lbTIndice);
            this.tabPage2.Controls.Add(this.lbEnti2);
            this.tabPage2.Controls.Add(this.lbLongi);
            this.tabPage2.Controls.Add(this.cBox_Entidades1);
            this.tabPage2.Controls.Add(this.lbTipo);
            this.tabPage2.Controls.Add(this.numericIndice);
            this.tabPage2.Controls.Add(this.tbNaAtri);
            this.tabPage2.Controls.Add(this.numLongi);
            this.tabPage2.Controls.Add(this.lbAtri);
            this.tabPage2.Controls.Add(this.cboxTipo);
            this.tabPage2.Controls.Add(this.btn_Eliminar2);
            this.tabPage2.Controls.Add(this.lb_Atri);
            this.tabPage2.Controls.Add(this.btn_modificar2);
            this.tabPage2.Controls.Add(this.btn_Consultar__atr);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1230, 438);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Atributos";
            // 
            // dataGridView_atributos
            // 
            this.dataGridView_atributos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_atributos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Dir_indice,
            this.Sig_Atributo});
            this.dataGridView_atributos.Location = new System.Drawing.Point(49, 89);
            this.dataGridView_atributos.Name = "dataGridView_atributos";
            this.dataGridView_atributos.RowTemplate.Height = 24;
            this.dataGridView_atributos.Size = new System.Drawing.Size(1034, 346);
            this.dataGridView_atributos.TabIndex = 13;
            this.dataGridView_atributos.Visible = false;
            this.dataGridView_atributos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_atributos_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Dirreción";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Atributo";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "Atributo";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tipo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Longitud";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Indice";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // Dir_indice
            // 
            this.Dir_indice.HeaderText = "Dir_Indice";
            this.Dir_indice.Name = "Dir_indice";
            // 
            // Sig_Atributo
            // 
            this.Sig_Atributo.HeaderText = "Sig_Atributo";
            this.Sig_Atributo.Name = "Sig_Atributo";
            // 
            // btnAgregar2
            // 
            this.btnAgregar2.Location = new System.Drawing.Point(604, 33);
            this.btnAgregar2.Name = "btnAgregar2";
            this.btnAgregar2.Size = new System.Drawing.Size(119, 32);
            this.btnAgregar2.TabIndex = 16;
            this.btnAgregar2.Text = "Agregar";
            this.btnAgregar2.UseVisualStyleBackColor = true;
            this.btnAgregar2.Visible = false;
            this.btnAgregar2.Click += new System.EventHandler(this.btnAgregar2_Click);
            // 
            // lbTIndice
            // 
            this.lbTIndice.AutoSize = true;
            this.lbTIndice.Location = new System.Drawing.Point(501, 21);
            this.lbTIndice.Name = "lbTIndice";
            this.lbTIndice.Size = new System.Drawing.Size(77, 17);
            this.lbTIndice.TabIndex = 29;
            this.lbTIndice.Text = "Tipo indice";
            this.lbTIndice.Visible = false;
            // 
            // lbEnti2
            // 
            this.lbEnti2.AutoSize = true;
            this.lbEnti2.Location = new System.Drawing.Point(96, 19);
            this.lbEnti2.Name = "lbEnti2";
            this.lbEnti2.Size = new System.Drawing.Size(71, 17);
            this.lbEnti2.TabIndex = 30;
            this.lbEnti2.Text = "Entidades";
            this.lbEnti2.Visible = false;
            // 
            // lbLongi
            // 
            this.lbLongi.AutoSize = true;
            this.lbLongi.Location = new System.Drawing.Point(432, 20);
            this.lbLongi.Name = "lbLongi";
            this.lbLongi.Size = new System.Drawing.Size(63, 17);
            this.lbLongi.TabIndex = 28;
            this.lbLongi.Text = "Longitud";
            this.lbLongi.Visible = false;
            // 
            // cBox_Entidades1
            // 
            this.cBox_Entidades1.FormattingEnabled = true;
            this.cBox_Entidades1.Location = new System.Drawing.Point(39, 39);
            this.cBox_Entidades1.Name = "cBox_Entidades1";
            this.cBox_Entidades1.Size = new System.Drawing.Size(179, 24);
            this.cBox_Entidades1.TabIndex = 15;
            this.cBox_Entidades1.Visible = false;
            // 
            // lbTipo
            // 
            this.lbTipo.AutoSize = true;
            this.lbTipo.Location = new System.Drawing.Point(368, 20);
            this.lbTipo.Name = "lbTipo";
            this.lbTipo.Size = new System.Drawing.Size(36, 17);
            this.lbTipo.TabIndex = 27;
            this.lbTipo.Text = "Tipo";
            this.lbTipo.Visible = false;
            // 
            // numericIndice
            // 
            this.numericIndice.Location = new System.Drawing.Point(518, 41);
            this.numericIndice.Name = "numericIndice";
            this.numericIndice.Size = new System.Drawing.Size(47, 22);
            this.numericIndice.TabIndex = 25;
            this.numericIndice.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericIndice.Visible = false;
            // 
            // tbNaAtri
            // 
            this.tbNaAtri.Location = new System.Drawing.Point(236, 41);
            this.tbNaAtri.Name = "tbNaAtri";
            this.tbNaAtri.Size = new System.Drawing.Size(119, 22);
            this.tbNaAtri.TabIndex = 22;
            this.tbNaAtri.Visible = false;
            // 
            // numLongi
            // 
            this.numLongi.Location = new System.Drawing.Point(448, 40);
            this.numLongi.Name = "numLongi";
            this.numLongi.Size = new System.Drawing.Size(47, 22);
            this.numLongi.TabIndex = 24;
            this.numLongi.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numLongi.Visible = false;
            // 
            // lbAtri
            // 
            this.lbAtri.AutoSize = true;
            this.lbAtri.Location = new System.Drawing.Point(264, 21);
            this.lbAtri.Name = "lbAtri";
            this.lbAtri.Size = new System.Drawing.Size(57, 17);
            this.lbAtri.TabIndex = 26;
            this.lbAtri.Text = "Atributo";
            this.lbAtri.Visible = false;
            // 
            // cboxTipo
            // 
            this.cboxTipo.DisplayMember = "String";
            this.cboxTipo.FormattingEnabled = true;
            this.cboxTipo.Items.AddRange(new object[] {
            "S",
            "I"});
            this.cboxTipo.Location = new System.Drawing.Point(371, 40);
            this.cboxTipo.Name = "cboxTipo";
            this.cboxTipo.Size = new System.Drawing.Size(52, 24);
            this.cboxTipo.TabIndex = 23;
            this.cboxTipo.Text = "I";
            this.cboxTipo.Visible = false;
            // 
            // btn_Eliminar2
            // 
            this.btn_Eliminar2.Location = new System.Drawing.Point(729, 33);
            this.btn_Eliminar2.Name = "btn_Eliminar2";
            this.btn_Eliminar2.Size = new System.Drawing.Size(119, 32);
            this.btn_Eliminar2.TabIndex = 17;
            this.btn_Eliminar2.Text = "Eliminar";
            this.btn_Eliminar2.UseVisualStyleBackColor = true;
            this.btn_Eliminar2.Visible = false;
            this.btn_Eliminar2.Click += new System.EventHandler(this.btn_Eliminar2_Click);
            // 
            // lb_Atri
            // 
            this.lb_Atri.AutoSize = true;
            this.lb_Atri.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Atri.Location = new System.Drawing.Point(771, -1);
            this.lb_Atri.Name = "lb_Atri";
            this.lb_Atri.Size = new System.Drawing.Size(141, 28);
            this.lb_Atri.TabIndex = 21;
            this.lb_Atri.Text = "ATRIBUTOS";
            this.lb_Atri.Visible = false;
            // 
            // btn_modificar2
            // 
            this.btn_modificar2.Location = new System.Drawing.Point(854, 32);
            this.btn_modificar2.Name = "btn_modificar2";
            this.btn_modificar2.Size = new System.Drawing.Size(119, 32);
            this.btn_modificar2.TabIndex = 18;
            this.btn_modificar2.Text = "Modificar";
            this.btn_modificar2.UseVisualStyleBackColor = true;
            this.btn_modificar2.Visible = false;
            this.btn_modificar2.Click += new System.EventHandler(this.btn_modificar2_Click);
            // 
            // btn_Consultar__atr
            // 
            this.btn_Consultar__atr.Location = new System.Drawing.Point(979, 33);
            this.btn_Consultar__atr.Name = "btn_Consultar__atr";
            this.btn_Consultar__atr.Size = new System.Drawing.Size(119, 32);
            this.btn_Consultar__atr.TabIndex = 19;
            this.btn_Consultar__atr.Text = "Consultar";
            this.btn_Consultar__atr.UseVisualStyleBackColor = true;
            this.btn_Consultar__atr.Visible = false;
            this.btn_Consultar__atr.Click += new System.EventHandler(this.btn_Consultar__atr_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DimGray;
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.tb_Cab);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.bnt_Eliminar);
            this.tabPage1.Controls.Add(this.lb_Entidad);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.btn_Modificar);
            this.tabPage1.Controls.Add(this.lb_Modifica);
            this.tabPage1.Controls.Add(this.btn_ok);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1230, 438);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Entidades";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(122, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(119, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            // 
            // tb_Cab
            // 
            this.tb_Cab.Location = new System.Drawing.Point(3, 107);
            this.tb_Cab.Name = "tb_Cab";
            this.tb_Cab.Size = new System.Drawing.Size(100, 22);
            this.tb_Cab.TabIndex = 6;
            this.tb_Cab.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(675, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(119, 22);
            this.textBox2.TabIndex = 10;
            this.textBox2.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 32);
            this.button1.TabIndex = 1;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bnt_Eliminar
            // 
            this.bnt_Eliminar.Location = new System.Drawing.Point(414, 43);
            this.bnt_Eliminar.Name = "bnt_Eliminar";
            this.bnt_Eliminar.Size = new System.Drawing.Size(119, 32);
            this.bnt_Eliminar.TabIndex = 8;
            this.bnt_Eliminar.Text = "Eliminar";
            this.bnt_Eliminar.UseVisualStyleBackColor = true;
            this.bnt_Eliminar.Visible = false;
            this.bnt_Eliminar.Click += new System.EventHandler(this.bnt_Eliminar_Click);
            // 
            // lb_Entidad
            // 
            this.lb_Entidad.AutoSize = true;
            this.lb_Entidad.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Entidad.Location = new System.Drawing.Point(381, 6);
            this.lb_Entidad.Name = "lb_Entidad";
            this.lb_Entidad.Size = new System.Drawing.Size(112, 28);
            this.lb_Entidad.TabIndex = 20;
            this.lb_Entidad.Text = "ENTIDAD";
            this.lb_Entidad.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cabecera";
            this.label1.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(109, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(724, 314);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Dirreción";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Entidad";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Direccion siguiente";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Dir_Atributo";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Datos";
            this.Column5.Name = "Column5";
            // 
            // btn_Modificar
            // 
            this.btn_Modificar.Location = new System.Drawing.Point(550, 43);
            this.btn_Modificar.Name = "btn_Modificar";
            this.btn_Modificar.Size = new System.Drawing.Size(119, 32);
            this.btn_Modificar.TabIndex = 9;
            this.btn_Modificar.Text = "Modificar";
            this.btn_Modificar.UseVisualStyleBackColor = true;
            this.btn_Modificar.Visible = false;
            this.btn_Modificar.Click += new System.EventHandler(this.btn_Modificar_Click);
            // 
            // lb_Modifica
            // 
            this.lb_Modifica.AutoSize = true;
            this.lb_Modifica.Location = new System.Drawing.Point(675, 12);
            this.lb_Modifica.Name = "lb_Modifica";
            this.lb_Modifica.Size = new System.Drawing.Size(119, 17);
            this.lb_Modifica.TabIndex = 12;
            this.lb_Modifica.Text = "   Cambia nombre";
            this.lb_Modifica.Visible = false;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(709, 55);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(48, 26);
            this.btn_ok.TabIndex = 11;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Visible = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1238, 467);
            this.tabControl1.TabIndex = 31;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1902, 817);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = " ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_atributos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericIndice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongi)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem abrirDiccionarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarDiccionarioToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBox_Entidades2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView_atributos;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dir_indice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sig_Atributo;
        private System.Windows.Forms.Button btnAgregar2;
        private System.Windows.Forms.Label lbTIndice;
        private System.Windows.Forms.Label lbEnti2;
        private System.Windows.Forms.Label lbLongi;
        private System.Windows.Forms.ComboBox cBox_Entidades1;
        private System.Windows.Forms.Label lbTipo;
        private System.Windows.Forms.NumericUpDown numericIndice;
        private System.Windows.Forms.TextBox tbNaAtri;
        private System.Windows.Forms.NumericUpDown numLongi;
        private System.Windows.Forms.Label lbAtri;
        private System.Windows.Forms.ComboBox cboxTipo;
        private System.Windows.Forms.Button btn_Eliminar2;
        private System.Windows.Forms.Label lb_Atri;
        private System.Windows.Forms.Button btn_modificar2;
        private System.Windows.Forms.Button btn_Consultar__atr;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tb_Cab;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bnt_Eliminar;
        private System.Windows.Forms.Label lb_Entidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button btn_Modificar;
        private System.Windows.Forms.Label lb_Modifica;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label4;
    }
}

