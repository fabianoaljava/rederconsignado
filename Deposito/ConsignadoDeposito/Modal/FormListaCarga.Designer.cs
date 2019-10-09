namespace ConsignadoDeposito.Modal
{
    partial class FormListaCarga
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.btnCancelar = new MetroFramework.Controls.MetroButton();
            this.btnConfirmar = new MetroFramework.Controls.MetroButton();
            this.grdCarga = new MetroFramework.Controls.MetroGrid();
            this.txtCargaCodRepresentante = new MetroFramework.Controls.MetroTextBox();
            this.txtCargaCodPraca = new MetroFramework.Controls.MetroTextBox();
            this.cbbCargaRepresentante = new MetroFramework.Controls.MetroComboBox();
            this.cbbCargaPraca = new MetroFramework.Controls.MetroComboBox();
            this.lblCargaRepresentante = new MetroFramework.Controls.MetroLabel();
            this.lblCargaPraca = new MetroFramework.Controls.MetroLabel();
            this.cbbCargaMesAno = new System.Windows.Forms.DateTimePicker();
            this.lblCargaMesAno = new MetroFramework.Controls.MetroLabel();
            this.lblInfo = new MetroFramework.Controls.MetroLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblE = new MetroFramework.Controls.MetroLabel();
            this.btnFiltrar = new MetroFramework.Controls.MetroButton();
            this.btnLimpar = new MetroFramework.Controls.MetroButton();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCarga)).BeginInit();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.btnLimpar);
            this.metroPanel1.Controls.Add(this.btnFiltrar);
            this.metroPanel1.Controls.Add(this.lblE);
            this.metroPanel1.Controls.Add(this.dateTimePicker1);
            this.metroPanel1.Controls.Add(this.lblInfo);
            this.metroPanel1.Controls.Add(this.cbbCargaMesAno);
            this.metroPanel1.Controls.Add(this.lblCargaMesAno);
            this.metroPanel1.Controls.Add(this.txtCargaCodRepresentante);
            this.metroPanel1.Controls.Add(this.txtCargaCodPraca);
            this.metroPanel1.Controls.Add(this.cbbCargaRepresentante);
            this.metroPanel1.Controls.Add(this.cbbCargaPraca);
            this.metroPanel1.Controls.Add(this.lblCargaRepresentante);
            this.metroPanel1.Controls.Add(this.lblCargaPraca);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(760, 99);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.btnCancelar);
            this.metroPanel2.Controls.Add(this.btnConfirmar);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(20, 457);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(760, 39);
            this.metroPanel2.TabIndex = 1;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroPanel3
            // 
            this.metroPanel3.Controls.Add(this.grdCarga);
            this.metroPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(20, 159);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(760, 298);
            this.metroPanel3.TabIndex = 2;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancelar.Location = new System.Drawing.Point(0, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 39);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Ca&ncelar";
            this.btnCancelar.UseSelectable = true;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(685, 0);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 39);
            this.btnConfirmar.TabIndex = 9;
            this.btnConfirmar.Text = "&Confirmar";
            this.btnConfirmar.UseSelectable = true;
            // 
            // grdCarga
            // 
            this.grdCarga.AllowUserToAddRows = false;
            this.grdCarga.AllowUserToDeleteRows = false;
            this.grdCarga.AllowUserToOrderColumns = true;
            this.grdCarga.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdCarga.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdCarga.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdCarga.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdCarga.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdCarga.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdCarga.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdCarga.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdCarga.ColumnHeadersHeight = 22;
            this.grdCarga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdCarga.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdCarga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCarga.EnableHeadersVisualStyles = false;
            this.grdCarga.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdCarga.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdCarga.Location = new System.Drawing.Point(0, 0);
            this.grdCarga.MultiSelect = false;
            this.grdCarga.Name = "grdCarga";
            this.grdCarga.ReadOnly = true;
            this.grdCarga.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdCarga.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdCarga.RowHeadersVisible = false;
            this.grdCarga.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdCarga.RowTemplate.ReadOnly = true;
            this.grdCarga.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdCarga.ShowEditingIcon = false;
            this.grdCarga.Size = new System.Drawing.Size(760, 298);
            this.grdCarga.StandardTab = true;
            this.grdCarga.TabIndex = 8;
            this.grdCarga.UseCustomBackColor = true;
            this.grdCarga.UseCustomForeColor = true;
            this.grdCarga.UseStyleColors = true;
            // 
            // txtCargaCodRepresentante
            // 
            // 
            // 
            // 
            this.txtCargaCodRepresentante.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtCargaCodRepresentante.CustomButton.Location = new System.Drawing.Point(39, 1);
            this.txtCargaCodRepresentante.CustomButton.Name = "";
            this.txtCargaCodRepresentante.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCargaCodRepresentante.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCargaCodRepresentante.CustomButton.TabIndex = 1;
            this.txtCargaCodRepresentante.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCargaCodRepresentante.CustomButton.UseSelectable = true;
            this.txtCargaCodRepresentante.DisplayIcon = true;
            this.txtCargaCodRepresentante.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCargaCodRepresentante.IconRight = true;
            this.txtCargaCodRepresentante.Lines = new string[0];
            this.txtCargaCodRepresentante.Location = new System.Drawing.Point(118, 56);
            this.txtCargaCodRepresentante.Margin = new System.Windows.Forms.Padding(5);
            this.txtCargaCodRepresentante.MaxLength = 32767;
            this.txtCargaCodRepresentante.Name = "txtCargaCodRepresentante";
            this.txtCargaCodRepresentante.PasswordChar = '\0';
            this.txtCargaCodRepresentante.PromptText = "Cod";
            this.txtCargaCodRepresentante.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCargaCodRepresentante.SelectedText = "";
            this.txtCargaCodRepresentante.SelectionLength = 0;
            this.txtCargaCodRepresentante.SelectionStart = 0;
            this.txtCargaCodRepresentante.ShortcutsEnabled = true;
            this.txtCargaCodRepresentante.ShowButton = true;
            this.txtCargaCodRepresentante.Size = new System.Drawing.Size(67, 29);
            this.txtCargaCodRepresentante.TabIndex = 2;
            this.txtCargaCodRepresentante.UseSelectable = true;
            this.txtCargaCodRepresentante.WaterMark = "Cod";
            this.txtCargaCodRepresentante.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCargaCodRepresentante.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCargaCodRepresentante.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(this.txtCargaCodRepresentante_ButtonClick);
            this.txtCargaCodRepresentante.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CargaKeyEnter);
            this.txtCargaCodRepresentante.Validating += new System.ComponentModel.CancelEventHandler(this.txtCargaCodRepresentante_Validating);
            // 
            // txtCargaCodPraca
            // 
            // 
            // 
            // 
            this.txtCargaCodPraca.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtCargaCodPraca.CustomButton.Location = new System.Drawing.Point(39, 1);
            this.txtCargaCodPraca.CustomButton.Name = "";
            this.txtCargaCodPraca.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCargaCodPraca.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCargaCodPraca.CustomButton.TabIndex = 1;
            this.txtCargaCodPraca.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCargaCodPraca.CustomButton.UseSelectable = true;
            this.txtCargaCodPraca.DisplayIcon = true;
            this.txtCargaCodPraca.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCargaCodPraca.IconRight = true;
            this.txtCargaCodPraca.Lines = new string[0];
            this.txtCargaCodPraca.Location = new System.Drawing.Point(118, 21);
            this.txtCargaCodPraca.Margin = new System.Windows.Forms.Padding(5);
            this.txtCargaCodPraca.MaxLength = 32767;
            this.txtCargaCodPraca.Name = "txtCargaCodPraca";
            this.txtCargaCodPraca.PasswordChar = '\0';
            this.txtCargaCodPraca.PromptText = "Cod";
            this.txtCargaCodPraca.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCargaCodPraca.SelectedText = "";
            this.txtCargaCodPraca.SelectionLength = 0;
            this.txtCargaCodPraca.SelectionStart = 0;
            this.txtCargaCodPraca.ShortcutsEnabled = true;
            this.txtCargaCodPraca.ShowButton = true;
            this.txtCargaCodPraca.Size = new System.Drawing.Size(67, 29);
            this.txtCargaCodPraca.TabIndex = 0;
            this.txtCargaCodPraca.UseSelectable = true;
            this.txtCargaCodPraca.WaterMark = "Cod";
            this.txtCargaCodPraca.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCargaCodPraca.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCargaCodPraca.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(this.txtCargaCodPraca_ButtonClick);
            this.txtCargaCodPraca.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CargaKeyEnter);
            this.txtCargaCodPraca.Validating += new System.ComponentModel.CancelEventHandler(this.txtCargaCodPraca_Validating);
            // 
            // cbbCargaRepresentante
            // 
            this.cbbCargaRepresentante.FormattingEnabled = true;
            this.cbbCargaRepresentante.ItemHeight = 23;
            this.cbbCargaRepresentante.Location = new System.Drawing.Point(193, 56);
            this.cbbCargaRepresentante.Name = "cbbCargaRepresentante";
            this.cbbCargaRepresentante.PromptText = "Selecione o Representante";
            this.cbbCargaRepresentante.Size = new System.Drawing.Size(247, 29);
            this.cbbCargaRepresentante.Sorted = true;
            this.cbbCargaRepresentante.TabIndex = 3;
            this.cbbCargaRepresentante.UseSelectable = true;
            this.cbbCargaRepresentante.SelectedIndexChanged += new System.EventHandler(this.cbbCargaRepresentante_SelectedIndexChanged);
            // 
            // cbbCargaPraca
            // 
            this.cbbCargaPraca.FormattingEnabled = true;
            this.cbbCargaPraca.ItemHeight = 23;
            this.cbbCargaPraca.Location = new System.Drawing.Point(193, 21);
            this.cbbCargaPraca.Name = "cbbCargaPraca";
            this.cbbCargaPraca.PromptText = "Selecione a Praça";
            this.cbbCargaPraca.Size = new System.Drawing.Size(247, 29);
            this.cbbCargaPraca.Sorted = true;
            this.cbbCargaPraca.TabIndex = 1;
            this.cbbCargaPraca.UseSelectable = true;
            this.cbbCargaPraca.SelectedValueChanged += new System.EventHandler(this.cbbCargaPraca_SelectedValueChanged);
            // 
            // lblCargaRepresentante
            // 
            this.lblCargaRepresentante.AutoSize = true;
            this.lblCargaRepresentante.Location = new System.Drawing.Point(21, 62);
            this.lblCargaRepresentante.Name = "lblCargaRepresentante";
            this.lblCargaRepresentante.Size = new System.Drawing.Size(92, 19);
            this.lblCargaRepresentante.TabIndex = 9;
            this.lblCargaRepresentante.Text = "Representante";
            // 
            // lblCargaPraca
            // 
            this.lblCargaPraca.AutoSize = true;
            this.lblCargaPraca.Location = new System.Drawing.Point(21, 27);
            this.lblCargaPraca.Name = "lblCargaPraca";
            this.lblCargaPraca.Size = new System.Drawing.Size(42, 19);
            this.lblCargaPraca.TabIndex = 7;
            this.lblCargaPraca.Text = "Praça";
            // 
            // cbbCargaMesAno
            // 
            this.cbbCargaMesAno.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbCargaMesAno.CustomFormat = "MM/yyyy";
            this.cbbCargaMesAno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbCargaMesAno.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cbbCargaMesAno.Location = new System.Drawing.Point(542, 24);
            this.cbbCargaMesAno.Margin = new System.Windows.Forms.Padding(5);
            this.cbbCargaMesAno.Name = "cbbCargaMesAno";
            this.cbbCargaMesAno.ShowUpDown = true;
            this.cbbCargaMesAno.Size = new System.Drawing.Size(97, 26);
            this.cbbCargaMesAno.TabIndex = 4;
            this.cbbCargaMesAno.Value = new System.DateTime(2019, 1, 11, 0, 0, 0, 0);
            this.cbbCargaMesAno.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // lblCargaMesAno
            // 
            this.lblCargaMesAno.AutoSize = true;
            this.lblCargaMesAno.Location = new System.Drawing.Point(449, 27);
            this.lblCargaMesAno.Name = "lblCargaMesAno";
            this.lblCargaMesAno.Size = new System.Drawing.Size(92, 19);
            this.lblCargaMesAno.TabIndex = 11;
            this.lblCargaMesAno.Text = "Período Entre:";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(72, 19);
            this.lblInfo.TabIndex = 12;
            this.lblInfo.Text = "Filtrar por:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(542, 56);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(5);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(97, 26);
            this.dateTimePicker1.TabIndex = 5;
            this.dateTimePicker1.Value = new System.DateTime(2019, 7, 11, 0, 0, 0, 0);
            this.dateTimePicker1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // lblE
            // 
            this.lblE.AutoSize = true;
            this.lblE.Location = new System.Drawing.Point(520, 59);
            this.lblE.Name = "lblE";
            this.lblE.Size = new System.Drawing.Size(16, 19);
            this.lblE.TabIndex = 14;
            this.lblE.Text = "e";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(647, 24);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(90, 34);
            this.btnFiltrar.TabIndex = 6;
            this.btnFiltrar.Text = "&Filtrar";
            this.btnFiltrar.UseSelectable = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(647, 64);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(90, 18);
            this.btnLimpar.TabIndex = 7;
            this.btnLimpar.Text = "&Limpar";
            this.btnLimpar.UseSelectable = true;
            // 
            // FormListaCarga
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(800, 516);
            this.Controls.Add(this.metroPanel3);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.Name = "FormListaCarga";
            this.Text = "Selecionar Carga/Retorno";
            this.Load += new System.EventHandler(this.FormListaCarga_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCarga)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroButton btnCancelar;
        private MetroFramework.Controls.MetroButton btnConfirmar;
        public MetroFramework.Controls.MetroGrid grdCarga;
        public MetroFramework.Controls.MetroTextBox txtCargaCodRepresentante;
        public MetroFramework.Controls.MetroTextBox txtCargaCodPraca;
        public MetroFramework.Controls.MetroComboBox cbbCargaRepresentante;
        public MetroFramework.Controls.MetroComboBox cbbCargaPraca;
        private MetroFramework.Controls.MetroLabel lblCargaRepresentante;
        private MetroFramework.Controls.MetroLabel lblCargaPraca;
        public System.Windows.Forms.DateTimePicker cbbCargaMesAno;
        private MetroFramework.Controls.MetroLabel lblCargaMesAno;
        private MetroFramework.Controls.MetroButton btnFiltrar;
        private MetroFramework.Controls.MetroLabel lblE;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private MetroFramework.Controls.MetroLabel lblInfo;
        private MetroFramework.Controls.MetroButton btnLimpar;
    }
}