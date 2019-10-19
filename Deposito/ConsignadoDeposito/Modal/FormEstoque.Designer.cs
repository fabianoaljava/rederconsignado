namespace ConsignadoDeposito.Modal
{
    partial class FormEstoque
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
            this.pnlTop = new MetroFramework.Controls.MetroPanel();
            this.dlbSaldo = new MetroFramework.Controls.MetroLabel();
            this.lblSaldo = new MetroFramework.Controls.MetroLabel();
            this.lblQuantidade = new MetroFramework.Controls.MetroLabel();
            this.lblTipoMovimentacao = new MetroFramework.Controls.MetroLabel();
            this.lblNome = new MetroFramework.Controls.MetroLabel();
            this.lblCodigoBarras = new MetroFramework.Controls.MetroLabel();
            this.txtObservacoes = new MetroFramework.Controls.MetroTextBox();
            this.txtQuantidade = new MetroFramework.Controls.MetroTextBox();
            this.btnCancelar = new MetroFramework.Controls.MetroButton();
            this.cbbTipoMovimentacao = new MetroFramework.Controls.MetroComboBox();
            this.btnAdicionar = new MetroFramework.Controls.MetroButton();
            this.txtNome = new MetroFramework.Controls.MetroTextBox();
            this.txtCodigoBarras = new MetroFramework.Controls.MetroTextBox();
            this.pnlBottom = new MetroFramework.Controls.MetroPanel();
            this.btnFechar = new MetroFramework.Controls.MetroButton();
            this.pnlMain = new MetroFramework.Controls.MetroPanel();
            this.grdEstoque = new MetroFramework.Controls.MetroGrid();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstoque)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.dlbSaldo);
            this.pnlTop.Controls.Add(this.lblSaldo);
            this.pnlTop.Controls.Add(this.lblQuantidade);
            this.pnlTop.Controls.Add(this.lblTipoMovimentacao);
            this.pnlTop.Controls.Add(this.lblNome);
            this.pnlTop.Controls.Add(this.lblCodigoBarras);
            this.pnlTop.Controls.Add(this.txtObservacoes);
            this.pnlTop.Controls.Add(this.txtQuantidade);
            this.pnlTop.Controls.Add(this.btnCancelar);
            this.pnlTop.Controls.Add(this.cbbTipoMovimentacao);
            this.pnlTop.Controls.Add(this.btnAdicionar);
            this.pnlTop.Controls.Add(this.txtNome);
            this.pnlTop.Controls.Add(this.txtCodigoBarras);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.HorizontalScrollbarBarColor = true;
            this.pnlTop.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlTop.HorizontalScrollbarSize = 10;
            this.pnlTop.Location = new System.Drawing.Point(20, 60);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(760, 100);
            this.pnlTop.TabIndex = 0;
            this.pnlTop.VerticalScrollbarBarColor = true;
            this.pnlTop.VerticalScrollbarHighlightOnWheel = false;
            this.pnlTop.VerticalScrollbarSize = 10;
            // 
            // dlbSaldo
            // 
            this.dlbSaldo.AutoSize = true;
            this.dlbSaldo.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.dlbSaldo.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.dlbSaldo.Location = new System.Drawing.Point(641, 20);
            this.dlbSaldo.Name = "dlbSaldo";
            this.dlbSaldo.Size = new System.Drawing.Size(22, 25);
            this.dlbSaldo.TabIndex = 43;
            this.dlbSaldo.Text = "0";
            this.dlbSaldo.Visible = false;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblSaldo.Location = new System.Drawing.Point(640, 5);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(98, 15);
            this.lblSaldo.TabIndex = 42;
            this.lblSaldo.Text = "Saldo em Estoque";
            this.lblSaldo.Visible = false;
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblQuantidade.Location = new System.Drawing.Point(494, 5);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(66, 15);
            this.lblQuantidade.TabIndex = 41;
            this.lblQuantidade.Text = "Quantidade";
            // 
            // lblTipoMovimentacao
            // 
            this.lblTipoMovimentacao.AutoSize = true;
            this.lblTipoMovimentacao.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblTipoMovimentacao.Location = new System.Drawing.Point(387, 4);
            this.lblTipoMovimentacao.Name = "lblTipoMovimentacao";
            this.lblTipoMovimentacao.Size = new System.Drawing.Size(106, 15);
            this.lblTipoMovimentacao.TabIndex = 40;
            this.lblTipoMovimentacao.Text = "Tipo Movimentação";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblNome.Location = new System.Drawing.Point(117, 5);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(101, 15);
            this.lblNome.TabIndex = 39;
            this.lblNome.Text = "Nome do Produto";
            // 
            // lblCodigoBarras
            // 
            this.lblCodigoBarras.AutoSize = true;
            this.lblCodigoBarras.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblCodigoBarras.Location = new System.Drawing.Point(6, 5);
            this.lblCodigoBarras.Name = "lblCodigoBarras";
            this.lblCodigoBarras.Size = new System.Drawing.Size(99, 15);
            this.lblCodigoBarras.TabIndex = 38;
            this.lblCodigoBarras.Text = "Códugo de Barras";
            // 
            // txtObservacoes
            // 
            // 
            // 
            // 
            this.txtObservacoes.CustomButton.Image = null;
            this.txtObservacoes.CustomButton.Location = new System.Drawing.Point(533, 1);
            this.txtObservacoes.CustomButton.Name = "";
            this.txtObservacoes.CustomButton.Size = new System.Drawing.Size(39, 39);
            this.txtObservacoes.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtObservacoes.CustomButton.TabIndex = 1;
            this.txtObservacoes.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtObservacoes.CustomButton.UseSelectable = true;
            this.txtObservacoes.CustomButton.Visible = false;
            this.txtObservacoes.Lines = new string[0];
            this.txtObservacoes.Location = new System.Drawing.Point(9, 52);
            this.txtObservacoes.MaxLength = 32767;
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.PasswordChar = '\0';
            this.txtObservacoes.PromptText = "Observações";
            this.txtObservacoes.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtObservacoes.SelectedText = "";
            this.txtObservacoes.SelectionLength = 0;
            this.txtObservacoes.SelectionStart = 0;
            this.txtObservacoes.ShortcutsEnabled = true;
            this.txtObservacoes.Size = new System.Drawing.Size(573, 41);
            this.txtObservacoes.TabIndex = 37;
            this.txtObservacoes.UseSelectable = true;
            this.txtObservacoes.WaterMark = "Observações";
            this.txtObservacoes.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtObservacoes.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtQuantidade
            // 
            // 
            // 
            // 
            this.txtQuantidade.CustomButton.Image = null;
            this.txtQuantidade.CustomButton.Location = new System.Drawing.Point(63, 1);
            this.txtQuantidade.CustomButton.Name = "";
            this.txtQuantidade.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtQuantidade.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtQuantidade.CustomButton.TabIndex = 1;
            this.txtQuantidade.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtQuantidade.CustomButton.UseSelectable = true;
            this.txtQuantidade.CustomButton.Visible = false;
            this.txtQuantidade.Lines = new string[0];
            this.txtQuantidade.Location = new System.Drawing.Point(497, 23);
            this.txtQuantidade.MaxLength = 32767;
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.PasswordChar = '\0';
            this.txtQuantidade.PromptText = "Quantidade";
            this.txtQuantidade.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtQuantidade.SelectedText = "";
            this.txtQuantidade.SelectionLength = 0;
            this.txtQuantidade.SelectionStart = 0;
            this.txtQuantidade.ShortcutsEnabled = true;
            this.txtQuantidade.Size = new System.Drawing.Size(85, 23);
            this.txtQuantidade.TabIndex = 36;
            this.txtQuantidade.UseSelectable = true;
            this.txtQuantidade.WaterMark = "Quantidade";
            this.txtQuantidade.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtQuantidade.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Location = new System.Drawing.Point(685, 52);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(59, 41);
            this.btnCancelar.TabIndex = 35;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseSelectable = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cbbTipoMovimentacao
            // 
            this.cbbTipoMovimentacao.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbbTipoMovimentacao.FormattingEnabled = true;
            this.cbbTipoMovimentacao.ItemHeight = 19;
            this.cbbTipoMovimentacao.Items.AddRange(new object[] {
            "Entrada",
            "Saída"});
            this.cbbTipoMovimentacao.Location = new System.Drawing.Point(390, 22);
            this.cbbTipoMovimentacao.Name = "cbbTipoMovimentacao";
            this.cbbTipoMovimentacao.Size = new System.Drawing.Size(101, 25);
            this.cbbTipoMovimentacao.TabIndex = 33;
            this.cbbTipoMovimentacao.UseSelectable = true;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Enabled = false;
            this.btnAdicionar.Location = new System.Drawing.Point(588, 52);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(91, 41);
            this.btnAdicionar.TabIndex = 34;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseSelectable = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // txtNome
            // 
            // 
            // 
            // 
            this.txtNome.CustomButton.Image = null;
            this.txtNome.CustomButton.Location = new System.Drawing.Point(242, 1);
            this.txtNome.CustomButton.Name = "";
            this.txtNome.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtNome.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNome.CustomButton.TabIndex = 1;
            this.txtNome.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNome.CustomButton.UseSelectable = true;
            this.txtNome.CustomButton.Visible = false;
            this.txtNome.Lines = new string[0];
            this.txtNome.Location = new System.Drawing.Point(120, 23);
            this.txtNome.MaxLength = 32767;
            this.txtNome.Name = "txtNome";
            this.txtNome.PasswordChar = '\0';
            this.txtNome.PromptText = "Nome do Produto";
            this.txtNome.ReadOnly = true;
            this.txtNome.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNome.SelectedText = "";
            this.txtNome.SelectionLength = 0;
            this.txtNome.SelectionStart = 0;
            this.txtNome.ShortcutsEnabled = true;
            this.txtNome.Size = new System.Drawing.Size(264, 23);
            this.txtNome.TabIndex = 32;
            this.txtNome.UseSelectable = true;
            this.txtNome.WaterMark = "Nome do Produto";
            this.txtNome.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNome.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtCodigoBarras
            // 
            // 
            // 
            // 
            this.txtCodigoBarras.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtCodigoBarras.CustomButton.Location = new System.Drawing.Point(88, 1);
            this.txtCodigoBarras.CustomButton.Name = "";
            this.txtCodigoBarras.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtCodigoBarras.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCodigoBarras.CustomButton.TabIndex = 1;
            this.txtCodigoBarras.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCodigoBarras.CustomButton.UseSelectable = true;
            this.txtCodigoBarras.Lines = new string[0];
            this.txtCodigoBarras.Location = new System.Drawing.Point(9, 23);
            this.txtCodigoBarras.MaxLength = 32767;
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.PasswordChar = '\0';
            this.txtCodigoBarras.PromptText = "Código de Barras";
            this.txtCodigoBarras.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCodigoBarras.SelectedText = "";
            this.txtCodigoBarras.SelectionLength = 0;
            this.txtCodigoBarras.SelectionStart = 0;
            this.txtCodigoBarras.ShortcutsEnabled = true;
            this.txtCodigoBarras.ShowButton = true;
            this.txtCodigoBarras.Size = new System.Drawing.Size(110, 23);
            this.txtCodigoBarras.TabIndex = 31;
            this.txtCodigoBarras.UseSelectable = true;
            this.txtCodigoBarras.WaterMark = "Código de Barras";
            this.txtCodigoBarras.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCodigoBarras.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCodigoBarras.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(this.txtCodigoBarras_ButtonClick);
            this.txtCodigoBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            this.txtCodigoBarras.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarras_KeyUp);
            this.txtCodigoBarras.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoBarras_Validating);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnFechar);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.HorizontalScrollbarBarColor = true;
            this.pnlBottom.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlBottom.HorizontalScrollbarSize = 10;
            this.pnlBottom.Location = new System.Drawing.Point(20, 391);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(760, 39);
            this.pnlBottom.TabIndex = 5;
            this.pnlBottom.VerticalScrollbarBarColor = true;
            this.pnlBottom.VerticalScrollbarHighlightOnWheel = false;
            this.pnlBottom.VerticalScrollbarSize = 10;
            // 
            // btnFechar
            // 
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFechar.Location = new System.Drawing.Point(685, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 39);
            this.btnFechar.TabIndex = 11;
            this.btnFechar.Text = "&Fechar";
            this.btnFechar.UseSelectable = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdEstoque);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.HorizontalScrollbarBarColor = true;
            this.pnlMain.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlMain.HorizontalScrollbarSize = 10;
            this.pnlMain.Location = new System.Drawing.Point(20, 160);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(760, 231);
            this.pnlMain.TabIndex = 6;
            this.pnlMain.VerticalScrollbarBarColor = true;
            this.pnlMain.VerticalScrollbarHighlightOnWheel = false;
            this.pnlMain.VerticalScrollbarSize = 10;
            // 
            // grdEstoque
            // 
            this.grdEstoque.AllowUserToAddRows = false;
            this.grdEstoque.AllowUserToDeleteRows = false;
            this.grdEstoque.AllowUserToOrderColumns = true;
            this.grdEstoque.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdEstoque.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdEstoque.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdEstoque.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdEstoque.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdEstoque.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdEstoque.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdEstoque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdEstoque.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdEstoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEstoque.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdEstoque.EnableHeadersVisualStyles = false;
            this.grdEstoque.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdEstoque.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdEstoque.Location = new System.Drawing.Point(0, 0);
            this.grdEstoque.Name = "grdEstoque";
            this.grdEstoque.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdEstoque.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdEstoque.RowHeadersVisible = false;
            this.grdEstoque.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdEstoque.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdEstoque.ShowEditingIcon = false;
            this.grdEstoque.Size = new System.Drawing.Size(760, 231);
            this.grdEstoque.TabIndex = 5;
            this.grdEstoque.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdEstoque_CellDoubleClick);
            this.grdEstoque.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdEstoque_KeyUp);
            // 
            // FormEstoque
            // 
            this.AcceptButton = this.btnFechar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEstoque";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Movimentação de Estoque";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormEstoque_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdEstoque)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlTop;
        private MetroFramework.Controls.MetroPanel pnlBottom;
        private MetroFramework.Controls.MetroButton btnFechar;
        private MetroFramework.Controls.MetroPanel pnlMain;
        public MetroFramework.Controls.MetroGrid grdEstoque;
        public MetroFramework.Controls.MetroTextBox txtObservacoes;
        public MetroFramework.Controls.MetroTextBox txtQuantidade;
        public MetroFramework.Controls.MetroButton btnCancelar;
        public MetroFramework.Controls.MetroComboBox cbbTipoMovimentacao;
        public MetroFramework.Controls.MetroButton btnAdicionar;
        public MetroFramework.Controls.MetroTextBox txtNome;
        public MetroFramework.Controls.MetroTextBox txtCodigoBarras;
        private MetroFramework.Controls.MetroLabel lblQuantidade;
        private MetroFramework.Controls.MetroLabel lblTipoMovimentacao;
        private MetroFramework.Controls.MetroLabel lblNome;
        private MetroFramework.Controls.MetroLabel lblCodigoBarras;
        private MetroFramework.Controls.MetroLabel dlbSaldo;
        private MetroFramework.Controls.MetroLabel lblSaldo;
    }
}