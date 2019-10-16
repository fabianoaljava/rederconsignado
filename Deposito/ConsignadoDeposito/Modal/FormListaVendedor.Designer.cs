namespace ConsignadoDeposito.Modal
{
    partial class FormListaVendedor
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
            this.pnlSearch = new MetroFramework.Controls.MetroPanel();
            this.btnLimpar = new MetroFramework.Controls.MetroButton();
            this.btnFiltrar = new MetroFramework.Controls.MetroButton();
            this.txtCPFCnpj = new MetroFramework.Controls.MetroTextBox();
            this.txtNome = new MetroFramework.Controls.MetroTextBox();
            this.lblInfo = new MetroFramework.Controls.MetroLabel();
            this.txtCodigo = new MetroFramework.Controls.MetroTextBox();
            this.lblCargaPraca = new MetroFramework.Controls.MetroLabel();
            this.pnlBottomForm = new MetroFramework.Controls.MetroPanel();
            this.btnCancelar = new MetroFramework.Controls.MetroButton();
            this.btnConfirmar = new MetroFramework.Controls.MetroButton();
            this.pnlMainGrid = new MetroFramework.Controls.MetroPanel();
            this.grdVendedor = new MetroFramework.Controls.MetroGrid();
            this.pnlSearch.SuspendLayout();
            this.pnlBottomForm.SuspendLayout();
            this.pnlMainGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVendedor)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.btnLimpar);
            this.pnlSearch.Controls.Add(this.btnFiltrar);
            this.pnlSearch.Controls.Add(this.txtCPFCnpj);
            this.pnlSearch.Controls.Add(this.txtNome);
            this.pnlSearch.Controls.Add(this.lblInfo);
            this.pnlSearch.Controls.Add(this.txtCodigo);
            this.pnlSearch.Controls.Add(this.lblCargaPraca);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.HorizontalScrollbarBarColor = true;
            this.pnlSearch.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlSearch.HorizontalScrollbarSize = 10;
            this.pnlSearch.Location = new System.Drawing.Point(20, 60);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(739, 69);
            this.pnlSearch.TabIndex = 1;
            this.pnlSearch.VerticalScrollbarBarColor = true;
            this.pnlSearch.VerticalScrollbarHighlightOnWheel = false;
            this.pnlSearch.VerticalScrollbarSize = 10;
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(652, 27);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(72, 29);
            this.btnLimpar.TabIndex = 16;
            this.btnLimpar.Text = "&Limpar";
            this.btnLimpar.UseSelectable = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(549, 27);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(98, 29);
            this.btnFiltrar.TabIndex = 15;
            this.btnFiltrar.Text = "&Filtrar";
            this.btnFiltrar.UseSelectable = true;
            // 
            // txtCPFCnpj
            // 
            // 
            // 
            // 
            this.txtCPFCnpj.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtCPFCnpj.CustomButton.Location = new System.Drawing.Point(87, 1);
            this.txtCPFCnpj.CustomButton.Name = "";
            this.txtCPFCnpj.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCPFCnpj.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCPFCnpj.CustomButton.TabIndex = 1;
            this.txtCPFCnpj.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCPFCnpj.CustomButton.UseSelectable = true;
            this.txtCPFCnpj.CustomButton.Visible = false;
            this.txtCPFCnpj.DisplayIcon = true;
            this.txtCPFCnpj.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCPFCnpj.IconRight = true;
            this.txtCPFCnpj.Lines = new string[0];
            this.txtCPFCnpj.Location = new System.Drawing.Point(90, 27);
            this.txtCPFCnpj.Margin = new System.Windows.Forms.Padding(5);
            this.txtCPFCnpj.MaxLength = 32767;
            this.txtCPFCnpj.Name = "txtCPFCnpj";
            this.txtCPFCnpj.PasswordChar = '\0';
            this.txtCPFCnpj.PromptText = "CPF/CNPJ";
            this.txtCPFCnpj.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCPFCnpj.SelectedText = "";
            this.txtCPFCnpj.SelectionLength = 0;
            this.txtCPFCnpj.SelectionStart = 0;
            this.txtCPFCnpj.ShortcutsEnabled = true;
            this.txtCPFCnpj.Size = new System.Drawing.Size(115, 29);
            this.txtCPFCnpj.TabIndex = 14;
            this.txtCPFCnpj.UseSelectable = true;
            this.txtCPFCnpj.WaterMark = "CPF/CNPJ";
            this.txtCPFCnpj.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCPFCnpj.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCPFCnpj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            this.txtCPFCnpj.Validating += new System.ComponentModel.CancelEventHandler(this.txtCPFCnpj_Validating);
            this.txtCPFCnpj.Validated += new System.EventHandler(this.txtCPFCnpj_Validated);
            // 
            // txtNome
            // 
            // 
            // 
            // 
            this.txtNome.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtNome.CustomButton.Location = new System.Drawing.Point(300, 1);
            this.txtNome.CustomButton.Name = "";
            this.txtNome.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtNome.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNome.CustomButton.TabIndex = 1;
            this.txtNome.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNome.CustomButton.UseSelectable = true;
            this.txtNome.CustomButton.Visible = false;
            this.txtNome.DisplayIcon = true;
            this.txtNome.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtNome.IconRight = true;
            this.txtNome.Lines = new string[0];
            this.txtNome.Location = new System.Drawing.Point(215, 27);
            this.txtNome.Margin = new System.Windows.Forms.Padding(5);
            this.txtNome.MaxLength = 32767;
            this.txtNome.Name = "txtNome";
            this.txtNome.PasswordChar = '\0';
            this.txtNome.PromptText = "Nome";
            this.txtNome.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNome.SelectedText = "";
            this.txtNome.SelectionLength = 0;
            this.txtNome.SelectionStart = 0;
            this.txtNome.ShortcutsEnabled = true;
            this.txtNome.Size = new System.Drawing.Size(328, 29);
            this.txtNome.TabIndex = 13;
            this.txtNome.UseSelectable = true;
            this.txtNome.WaterMark = "Nome";
            this.txtNome.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNome.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtNome.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNome_KeyUp);
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
            // txtCodigo
            // 
            // 
            // 
            // 
            this.txtCodigo.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtCodigo.CustomButton.Location = new System.Drawing.Point(56, 1);
            this.txtCodigo.CustomButton.Name = "";
            this.txtCodigo.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCodigo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCodigo.CustomButton.TabIndex = 1;
            this.txtCodigo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCodigo.CustomButton.UseSelectable = true;
            this.txtCodigo.DisplayIcon = true;
            this.txtCodigo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCodigo.IconRight = true;
            this.txtCodigo.Lines = new string[0];
            this.txtCodigo.Location = new System.Drawing.Point(1, 27);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(5);
            this.txtCodigo.MaxLength = 32767;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PasswordChar = '\0';
            this.txtCodigo.PromptText = "Codigo";
            this.txtCodigo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCodigo.SelectedText = "";
            this.txtCodigo.SelectionLength = 0;
            this.txtCodigo.SelectionStart = 0;
            this.txtCodigo.ShortcutsEnabled = true;
            this.txtCodigo.ShowButton = true;
            this.txtCodigo.Size = new System.Drawing.Size(84, 29);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.UseSelectable = true;
            this.txtCodigo.WaterMark = "Codigo";
            this.txtCodigo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCodigo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // lblCargaPraca
            // 
            this.lblCargaPraca.AutoSize = true;
            this.lblCargaPraca.Location = new System.Drawing.Point(21, 27);
            this.lblCargaPraca.Name = "lblCargaPraca";
            this.lblCargaPraca.Size = new System.Drawing.Size(0, 0);
            this.lblCargaPraca.TabIndex = 7;
            // 
            // pnlBottomForm
            // 
            this.pnlBottomForm.Controls.Add(this.btnCancelar);
            this.pnlBottomForm.Controls.Add(this.btnConfirmar);
            this.pnlBottomForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomForm.HorizontalScrollbarBarColor = true;
            this.pnlBottomForm.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlBottomForm.HorizontalScrollbarSize = 10;
            this.pnlBottomForm.Location = new System.Drawing.Point(20, 391);
            this.pnlBottomForm.Name = "pnlBottomForm";
            this.pnlBottomForm.Size = new System.Drawing.Size(739, 39);
            this.pnlBottomForm.TabIndex = 2;
            this.pnlBottomForm.VerticalScrollbarBarColor = true;
            this.pnlBottomForm.VerticalScrollbarHighlightOnWheel = false;
            this.pnlBottomForm.VerticalScrollbarSize = 10;
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
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(664, 0);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 39);
            this.btnConfirmar.TabIndex = 9;
            this.btnConfirmar.Text = "&Confirmar";
            this.btnConfirmar.UseSelectable = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // pnlMainGrid
            // 
            this.pnlMainGrid.Controls.Add(this.grdVendedor);
            this.pnlMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainGrid.HorizontalScrollbarBarColor = true;
            this.pnlMainGrid.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlMainGrid.HorizontalScrollbarSize = 10;
            this.pnlMainGrid.Location = new System.Drawing.Point(20, 129);
            this.pnlMainGrid.Name = "pnlMainGrid";
            this.pnlMainGrid.Size = new System.Drawing.Size(739, 262);
            this.pnlMainGrid.TabIndex = 3;
            this.pnlMainGrid.VerticalScrollbarBarColor = true;
            this.pnlMainGrid.VerticalScrollbarHighlightOnWheel = false;
            this.pnlMainGrid.VerticalScrollbarSize = 10;
            // 
            // grdVendedor
            // 
            this.grdVendedor.AllowUserToAddRows = false;
            this.grdVendedor.AllowUserToDeleteRows = false;
            this.grdVendedor.AllowUserToOrderColumns = true;
            this.grdVendedor.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdVendedor.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdVendedor.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdVendedor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdVendedor.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdVendedor.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVendedor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdVendedor.ColumnHeadersHeight = 22;
            this.grdVendedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdVendedor.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdVendedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVendedor.EnableHeadersVisualStyles = false;
            this.grdVendedor.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdVendedor.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdVendedor.Location = new System.Drawing.Point(0, 0);
            this.grdVendedor.MultiSelect = false;
            this.grdVendedor.Name = "grdVendedor";
            this.grdVendedor.ReadOnly = true;
            this.grdVendedor.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVendedor.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdVendedor.RowHeadersVisible = false;
            this.grdVendedor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdVendedor.RowTemplate.ReadOnly = true;
            this.grdVendedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdVendedor.ShowEditingIcon = false;
            this.grdVendedor.Size = new System.Drawing.Size(739, 262);
            this.grdVendedor.StandardTab = true;
            this.grdVendedor.TabIndex = 8;
            this.grdVendedor.UseCustomBackColor = true;
            this.grdVendedor.UseCustomForeColor = true;
            this.grdVendedor.UseStyleColors = true;
            this.grdVendedor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdVendedor_CellClick);
            this.grdVendedor.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdVendedor_CellContentDoubleClick);
            // 
            // FormListaVendedor
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(779, 450);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMainGrid);
            this.Controls.Add(this.pnlBottomForm);
            this.Controls.Add(this.pnlSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormListaVendedor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Selecionar Vendedor";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormListaVendedor_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlBottomForm.ResumeLayout(false);
            this.pnlMainGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdVendedor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlSearch;
        private MetroFramework.Controls.MetroLabel lblInfo;
        private MetroFramework.Controls.MetroLabel lblCargaPraca;
        private MetroFramework.Controls.MetroPanel pnlBottomForm;
        private MetroFramework.Controls.MetroButton btnCancelar;
        private MetroFramework.Controls.MetroButton btnConfirmar;
        public MetroFramework.Controls.MetroTextBox txtCodigo;
        private MetroFramework.Controls.MetroPanel pnlMainGrid;
        public MetroFramework.Controls.MetroGrid grdVendedor;
        public MetroFramework.Controls.MetroTextBox txtNome;
        public MetroFramework.Controls.MetroTextBox txtCPFCnpj;
        private MetroFramework.Controls.MetroButton btnLimpar;
        private MetroFramework.Controls.MetroButton btnFiltrar;
    }
}