namespace ConsignadoDeposito.Forms
{
    partial class FormProduto
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
            this.pnlProduto = new MetroFramework.Controls.MetroPanel();
            this.grpProduto = new System.Windows.Forms.GroupBox();
            this.btnCancelarProduto = new MetroFramework.Controls.MetroButton();
            this.btnExcluirProduto = new MetroFramework.Controls.MetroButton();
            this.btnSalvarProduto = new MetroFramework.Controls.MetroButton();
            this.lblFornecedor = new MetroFramework.Controls.MetroLabel();
            this.cbbFornecedor = new MetroFramework.Controls.MetroComboBox();
            this.lblCategoria = new MetroFramework.Controls.MetroLabel();
            this.cbbCategoria = new MetroFramework.Controls.MetroComboBox();
            this.lblDigitoVerificador = new MetroFramework.Controls.MetroLabel();
            this.lblCodigoBarras = new MetroFramework.Controls.MetroLabel();
            this.txtDigitoVerificador = new MetroFramework.Controls.MetroTextBox();
            this.txtCodigoBarras = new MetroFramework.Controls.MetroTextBox();
            this.lblUnidade = new MetroFramework.Controls.MetroLabel();
            this.txtUnidade = new MetroFramework.Controls.MetroTextBox();
            this.txtNomeProduto = new MetroFramework.Controls.MetroTextBox();
            this.lblNomeProduto = new MetroFramework.Controls.MetroLabel();
            this.grpPesquisa = new System.Windows.Forms.GroupBox();
            this.txtPesquisaCodProduto = new MetroFramework.Controls.MetroTextBox();
            this.cbbPesquisaProduto = new MetroFramework.Controls.MetroComboBox();
            this.btnNovoProduto = new MetroFramework.Controls.MetroButton();
            this.grpAdicionarGrade = new System.Windows.Forms.GroupBox();
            this.txtGradeDV = new MetroFramework.Controls.MetroTextBox();
            this.txtValorCusto = new MetroFramework.Controls.MetroTextBox();
            this.txtValorSaida = new MetroFramework.Controls.MetroTextBox();
            this.cbbGradeTamanho = new MetroFramework.Controls.MetroComboBox();
            this.cbbGradeCor = new MetroFramework.Controls.MetroComboBox();
            this.btnGradeCancelar = new MetroFramework.Controls.MetroButton();
            this.btnGradeConfirmar = new MetroFramework.Controls.MetroButton();
            this.lblAdicionarGrade = new MetroFramework.Controls.MetroLabel();
            this.pnlProdutoItem = new MetroFramework.Controls.MetroPanel();
            this.pblProdutoBottom = new MetroFramework.Controls.MetroPanel();
            this.pnlProdutoGrade = new MetroFramework.Controls.MetroPanel();
            this.grdProdutoGrade = new MetroFramework.Controls.MetroGrid();
            this.pnlProduto.SuspendLayout();
            this.grpProduto.SuspendLayout();
            this.grpPesquisa.SuspendLayout();
            this.grpAdicionarGrade.SuspendLayout();
            this.pnlProdutoItem.SuspendLayout();
            this.pnlProdutoGrade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProdutoGrade)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlProduto
            // 
            this.pnlProduto.Controls.Add(this.grpProduto);
            this.pnlProduto.Controls.Add(this.grpPesquisa);
            this.pnlProduto.Controls.Add(this.btnNovoProduto);
            this.pnlProduto.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProduto.HorizontalScrollbarBarColor = true;
            this.pnlProduto.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProduto.HorizontalScrollbarSize = 10;
            this.pnlProduto.Location = new System.Drawing.Point(20, 60);
            this.pnlProduto.Name = "pnlProduto";
            this.pnlProduto.Size = new System.Drawing.Size(679, 232);
            this.pnlProduto.TabIndex = 0;
            this.pnlProduto.VerticalScrollbarBarColor = true;
            this.pnlProduto.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProduto.VerticalScrollbarSize = 10;
            // 
            // grpProduto
            // 
            this.grpProduto.Controls.Add(this.btnCancelarProduto);
            this.grpProduto.Controls.Add(this.btnExcluirProduto);
            this.grpProduto.Controls.Add(this.btnSalvarProduto);
            this.grpProduto.Controls.Add(this.lblFornecedor);
            this.grpProduto.Controls.Add(this.cbbFornecedor);
            this.grpProduto.Controls.Add(this.lblCategoria);
            this.grpProduto.Controls.Add(this.cbbCategoria);
            this.grpProduto.Controls.Add(this.lblDigitoVerificador);
            this.grpProduto.Controls.Add(this.lblCodigoBarras);
            this.grpProduto.Controls.Add(this.txtDigitoVerificador);
            this.grpProduto.Controls.Add(this.txtCodigoBarras);
            this.grpProduto.Controls.Add(this.lblUnidade);
            this.grpProduto.Controls.Add(this.txtUnidade);
            this.grpProduto.Controls.Add(this.txtNomeProduto);
            this.grpProduto.Controls.Add(this.lblNomeProduto);
            this.grpProduto.Enabled = false;
            this.grpProduto.Location = new System.Drawing.Point(6, 74);
            this.grpProduto.Name = "grpProduto";
            this.grpProduto.Size = new System.Drawing.Size(666, 152);
            this.grpProduto.TabIndex = 16;
            this.grpProduto.TabStop = false;
            this.grpProduto.Text = "Dados do Produto";
            // 
            // btnCancelarProduto
            // 
            this.btnCancelarProduto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelarProduto.Location = new System.Drawing.Point(443, 114);
            this.btnCancelarProduto.Name = "btnCancelarProduto";
            this.btnCancelarProduto.Size = new System.Drawing.Size(103, 29);
            this.btnCancelarProduto.TabIndex = 29;
            this.btnCancelarProduto.Text = "Cancelar";
            this.btnCancelarProduto.UseSelectable = true;
            this.btnCancelarProduto.Click += new System.EventHandler(this.btnCancelarProduto_Click);
            // 
            // btnExcluirProduto
            // 
            this.btnExcluirProduto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExcluirProduto.Location = new System.Drawing.Point(8, 117);
            this.btnExcluirProduto.Name = "btnExcluirProduto";
            this.btnExcluirProduto.Size = new System.Drawing.Size(103, 29);
            this.btnExcluirProduto.TabIndex = 28;
            this.btnExcluirProduto.Text = "Excluir Produto";
            this.btnExcluirProduto.UseSelectable = true;
            this.btnExcluirProduto.Click += new System.EventHandler(this.btnExcluirProduto_Click);
            // 
            // btnSalvarProduto
            // 
            this.btnSalvarProduto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSalvarProduto.Location = new System.Drawing.Point(552, 114);
            this.btnSalvarProduto.Name = "btnSalvarProduto";
            this.btnSalvarProduto.Size = new System.Drawing.Size(103, 29);
            this.btnSalvarProduto.TabIndex = 13;
            this.btnSalvarProduto.Text = "Salvar";
            this.btnSalvarProduto.UseSelectable = true;
            this.btnSalvarProduto.Click += new System.EventHandler(this.btnSalvarProduto_Click);
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblFornecedor.Location = new System.Drawing.Point(254, 65);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(65, 15);
            this.lblFornecedor.TabIndex = 27;
            this.lblFornecedor.Text = "Fornecedor";
            // 
            // cbbFornecedor
            // 
            this.cbbFornecedor.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbbFornecedor.FormattingEnabled = true;
            this.cbbFornecedor.ItemHeight = 19;
            this.cbbFornecedor.Location = new System.Drawing.Point(254, 83);
            this.cbbFornecedor.Name = "cbbFornecedor";
            this.cbbFornecedor.PromptText = "Fornecedor";
            this.cbbFornecedor.Size = new System.Drawing.Size(401, 25);
            this.cbbFornecedor.TabIndex = 26;
            this.cbbFornecedor.UseSelectable = true;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblCategoria.Location = new System.Drawing.Point(8, 65);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(56, 15);
            this.lblCategoria.TabIndex = 25;
            this.lblCategoria.Text = "Categoria";
            // 
            // cbbCategoria
            // 
            this.cbbCategoria.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbbCategoria.FormattingEnabled = true;
            this.cbbCategoria.ItemHeight = 19;
            this.cbbCategoria.Location = new System.Drawing.Point(8, 83);
            this.cbbCategoria.Name = "cbbCategoria";
            this.cbbCategoria.PromptText = "Categoria";
            this.cbbCategoria.Size = new System.Drawing.Size(238, 25);
            this.cbbCategoria.TabIndex = 24;
            this.cbbCategoria.UseSelectable = true;
            // 
            // lblDigitoVerificador
            // 
            this.lblDigitoVerificador.AutoSize = true;
            this.lblDigitoVerificador.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblDigitoVerificador.Location = new System.Drawing.Point(625, 21);
            this.lblDigitoVerificador.Name = "lblDigitoVerificador";
            this.lblDigitoVerificador.Size = new System.Drawing.Size(22, 15);
            this.lblDigitoVerificador.TabIndex = 23;
            this.lblDigitoVerificador.Text = "DV";
            // 
            // lblCodigoBarras
            // 
            this.lblCodigoBarras.AutoSize = true;
            this.lblCodigoBarras.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblCodigoBarras.Location = new System.Drawing.Point(498, 21);
            this.lblCodigoBarras.Name = "lblCodigoBarras";
            this.lblCodigoBarras.Size = new System.Drawing.Size(95, 15);
            this.lblCodigoBarras.TabIndex = 22;
            this.lblCodigoBarras.Text = "Código de Barras";
            // 
            // txtDigitoVerificador
            // 
            // 
            // 
            // 
            this.txtDigitoVerificador.CustomButton.Image = null;
            this.txtDigitoVerificador.CustomButton.Location = new System.Drawing.Point(8, 1);
            this.txtDigitoVerificador.CustomButton.Name = "";
            this.txtDigitoVerificador.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtDigitoVerificador.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDigitoVerificador.CustomButton.TabIndex = 1;
            this.txtDigitoVerificador.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDigitoVerificador.CustomButton.UseSelectable = true;
            this.txtDigitoVerificador.CustomButton.Visible = false;
            this.txtDigitoVerificador.Lines = new string[0];
            this.txtDigitoVerificador.Location = new System.Drawing.Point(625, 39);
            this.txtDigitoVerificador.MaxLength = 1;
            this.txtDigitoVerificador.Name = "txtDigitoVerificador";
            this.txtDigitoVerificador.PasswordChar = '\0';
            this.txtDigitoVerificador.PromptText = "Dígito";
            this.txtDigitoVerificador.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDigitoVerificador.SelectedText = "";
            this.txtDigitoVerificador.SelectionLength = 0;
            this.txtDigitoVerificador.SelectionStart = 0;
            this.txtDigitoVerificador.ShortcutsEnabled = true;
            this.txtDigitoVerificador.Size = new System.Drawing.Size(30, 23);
            this.txtDigitoVerificador.TabIndex = 21;
            this.txtDigitoVerificador.UseSelectable = true;
            this.txtDigitoVerificador.WaterMark = "Dígito";
            this.txtDigitoVerificador.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDigitoVerificador.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtDigitoVerificador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            // 
            // txtCodigoBarras
            // 
            // 
            // 
            // 
            this.txtCodigoBarras.CustomButton.Image = null;
            this.txtCodigoBarras.CustomButton.Location = new System.Drawing.Point(97, 1);
            this.txtCodigoBarras.CustomButton.Name = "";
            this.txtCodigoBarras.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtCodigoBarras.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCodigoBarras.CustomButton.TabIndex = 1;
            this.txtCodigoBarras.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCodigoBarras.CustomButton.UseSelectable = true;
            this.txtCodigoBarras.CustomButton.Visible = false;
            this.txtCodigoBarras.Lines = new string[0];
            this.txtCodigoBarras.Location = new System.Drawing.Point(500, 39);
            this.txtCodigoBarras.MaxLength = 32767;
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.PasswordChar = '\0';
            this.txtCodigoBarras.PromptText = "Código de Barras";
            this.txtCodigoBarras.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCodigoBarras.SelectedText = "";
            this.txtCodigoBarras.SelectionLength = 0;
            this.txtCodigoBarras.SelectionStart = 0;
            this.txtCodigoBarras.ShortcutsEnabled = true;
            this.txtCodigoBarras.Size = new System.Drawing.Size(119, 23);
            this.txtCodigoBarras.TabIndex = 20;
            this.txtCodigoBarras.UseSelectable = true;
            this.txtCodigoBarras.WaterMark = "Código de Barras";
            this.txtCodigoBarras.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCodigoBarras.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCodigoBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            this.txtCodigoBarras.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoBarras_Validating);
            // 
            // lblUnidade
            // 
            this.lblUnidade.AutoSize = true;
            this.lblUnidade.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblUnidade.Location = new System.Drawing.Point(443, 21);
            this.lblUnidade.Name = "lblUnidade";
            this.lblUnidade.Size = new System.Drawing.Size(49, 15);
            this.lblUnidade.TabIndex = 16;
            this.lblUnidade.Text = "Unidade";
            // 
            // txtUnidade
            // 
            // 
            // 
            // 
            this.txtUnidade.CustomButton.Image = null;
            this.txtUnidade.CustomButton.Location = new System.Drawing.Point(29, 1);
            this.txtUnidade.CustomButton.Name = "";
            this.txtUnidade.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtUnidade.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUnidade.CustomButton.TabIndex = 1;
            this.txtUnidade.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUnidade.CustomButton.UseSelectable = true;
            this.txtUnidade.CustomButton.Visible = false;
            this.txtUnidade.Lines = new string[0];
            this.txtUnidade.Location = new System.Drawing.Point(443, 39);
            this.txtUnidade.MaxLength = 32767;
            this.txtUnidade.Name = "txtUnidade";
            this.txtUnidade.PasswordChar = '\0';
            this.txtUnidade.PromptText = "Unidade";
            this.txtUnidade.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUnidade.SelectedText = "";
            this.txtUnidade.SelectionLength = 0;
            this.txtUnidade.SelectionStart = 0;
            this.txtUnidade.ShortcutsEnabled = true;
            this.txtUnidade.Size = new System.Drawing.Size(51, 23);
            this.txtUnidade.TabIndex = 15;
            this.txtUnidade.UseSelectable = true;
            this.txtUnidade.WaterMark = "Unidade";
            this.txtUnidade.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUnidade.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtNomeProduto
            // 
            // 
            // 
            // 
            this.txtNomeProduto.CustomButton.Image = null;
            this.txtNomeProduto.CustomButton.Location = new System.Drawing.Point(407, 1);
            this.txtNomeProduto.CustomButton.Name = "";
            this.txtNomeProduto.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtNomeProduto.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNomeProduto.CustomButton.TabIndex = 1;
            this.txtNomeProduto.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNomeProduto.CustomButton.UseSelectable = true;
            this.txtNomeProduto.CustomButton.Visible = false;
            this.txtNomeProduto.Lines = new string[0];
            this.txtNomeProduto.Location = new System.Drawing.Point(8, 39);
            this.txtNomeProduto.MaxLength = 32767;
            this.txtNomeProduto.Name = "txtNomeProduto";
            this.txtNomeProduto.PasswordChar = '\0';
            this.txtNomeProduto.PromptText = "Nome";
            this.txtNomeProduto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNomeProduto.SelectedText = "";
            this.txtNomeProduto.SelectionLength = 0;
            this.txtNomeProduto.SelectionStart = 0;
            this.txtNomeProduto.ShortcutsEnabled = true;
            this.txtNomeProduto.Size = new System.Drawing.Size(429, 23);
            this.txtNomeProduto.TabIndex = 13;
            this.txtNomeProduto.UseSelectable = true;
            this.txtNomeProduto.WaterMark = "Nome";
            this.txtNomeProduto.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNomeProduto.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblNomeProduto
            // 
            this.lblNomeProduto.AutoSize = true;
            this.lblNomeProduto.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblNomeProduto.Location = new System.Drawing.Point(6, 21);
            this.lblNomeProduto.Name = "lblNomeProduto";
            this.lblNomeProduto.Size = new System.Drawing.Size(101, 15);
            this.lblNomeProduto.TabIndex = 14;
            this.lblNomeProduto.Text = "Nome do Produto";
            // 
            // grpPesquisa
            // 
            this.grpPesquisa.Controls.Add(this.txtPesquisaCodProduto);
            this.grpPesquisa.Controls.Add(this.cbbPesquisaProduto);
            this.grpPesquisa.Location = new System.Drawing.Point(6, 8);
            this.grpPesquisa.Name = "grpPesquisa";
            this.grpPesquisa.Size = new System.Drawing.Size(554, 60);
            this.grpPesquisa.TabIndex = 14;
            this.grpPesquisa.TabStop = false;
            this.grpPesquisa.Text = "Pesquisa";
            // 
            // txtPesquisaCodProduto
            // 
            // 
            // 
            // 
            this.txtPesquisaCodProduto.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtPesquisaCodProduto.CustomButton.Location = new System.Drawing.Point(178, 1);
            this.txtPesquisaCodProduto.CustomButton.Name = "";
            this.txtPesquisaCodProduto.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtPesquisaCodProduto.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPesquisaCodProduto.CustomButton.TabIndex = 1;
            this.txtPesquisaCodProduto.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPesquisaCodProduto.CustomButton.UseSelectable = true;
            this.txtPesquisaCodProduto.DisplayIcon = true;
            this.txtPesquisaCodProduto.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPesquisaCodProduto.IconRight = true;
            this.txtPesquisaCodProduto.Lines = new string[0];
            this.txtPesquisaCodProduto.Location = new System.Drawing.Point(8, 19);
            this.txtPesquisaCodProduto.Margin = new System.Windows.Forms.Padding(5);
            this.txtPesquisaCodProduto.MaxLength = 32767;
            this.txtPesquisaCodProduto.Name = "txtPesquisaCodProduto";
            this.txtPesquisaCodProduto.PasswordChar = '\0';
            this.txtPesquisaCodProduto.PromptText = "Código de Barras";
            this.txtPesquisaCodProduto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPesquisaCodProduto.SelectedText = "";
            this.txtPesquisaCodProduto.SelectionLength = 0;
            this.txtPesquisaCodProduto.SelectionStart = 0;
            this.txtPesquisaCodProduto.ShortcutsEnabled = true;
            this.txtPesquisaCodProduto.ShowButton = true;
            this.txtPesquisaCodProduto.Size = new System.Drawing.Size(206, 29);
            this.txtPesquisaCodProduto.TabIndex = 11;
            this.txtPesquisaCodProduto.UseSelectable = true;
            this.txtPesquisaCodProduto.WaterMark = "Código de Barras";
            this.txtPesquisaCodProduto.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPesquisaCodProduto.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtPesquisaCodProduto.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(this.txtPesquisaCodProduto_Click);
            this.txtPesquisaCodProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            this.txtPesquisaCodProduto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPesquisaCodProduto_KeyUp);
            // 
            // cbbPesquisaProduto
            // 
            this.cbbPesquisaProduto.FormattingEnabled = true;
            this.cbbPesquisaProduto.ItemHeight = 23;
            this.cbbPesquisaProduto.Location = new System.Drawing.Point(222, 19);
            this.cbbPesquisaProduto.Name = "cbbPesquisaProduto";
            this.cbbPesquisaProduto.PromptText = "Selecione o Produto";
            this.cbbPesquisaProduto.Size = new System.Drawing.Size(324, 29);
            this.cbbPesquisaProduto.TabIndex = 12;
            this.cbbPesquisaProduto.UseSelectable = true;
            // 
            // btnNovoProduto
            // 
            this.btnNovoProduto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNovoProduto.Location = new System.Drawing.Point(570, 16);
            this.btnNovoProduto.Name = "btnNovoProduto";
            this.btnNovoProduto.Size = new System.Drawing.Size(100, 52);
            this.btnNovoProduto.TabIndex = 15;
            this.btnNovoProduto.Text = "Adicionar\r\nNovo";
            this.btnNovoProduto.UseSelectable = true;
            this.btnNovoProduto.Click += new System.EventHandler(this.btnNovoProduto_Click);
            // 
            // grpAdicionarGrade
            // 
            this.grpAdicionarGrade.Controls.Add(this.txtGradeDV);
            this.grpAdicionarGrade.Controls.Add(this.txtValorCusto);
            this.grpAdicionarGrade.Controls.Add(this.txtValorSaida);
            this.grpAdicionarGrade.Controls.Add(this.cbbGradeTamanho);
            this.grpAdicionarGrade.Controls.Add(this.cbbGradeCor);
            this.grpAdicionarGrade.Controls.Add(this.btnGradeCancelar);
            this.grpAdicionarGrade.Controls.Add(this.btnGradeConfirmar);
            this.grpAdicionarGrade.Controls.Add(this.lblAdicionarGrade);
            this.grpAdicionarGrade.Location = new System.Drawing.Point(6, 6);
            this.grpAdicionarGrade.Name = "grpAdicionarGrade";
            this.grpAdicionarGrade.Size = new System.Drawing.Size(666, 75);
            this.grpAdicionarGrade.TabIndex = 17;
            this.grpAdicionarGrade.TabStop = false;
            // 
            // txtGradeDV
            // 
            // 
            // 
            // 
            this.txtGradeDV.CustomButton.Image = null;
            this.txtGradeDV.CustomButton.Location = new System.Drawing.Point(25, 1);
            this.txtGradeDV.CustomButton.Name = "";
            this.txtGradeDV.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtGradeDV.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtGradeDV.CustomButton.TabIndex = 1;
            this.txtGradeDV.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtGradeDV.CustomButton.UseSelectable = true;
            this.txtGradeDV.CustomButton.Visible = false;
            this.txtGradeDV.Lines = new string[0];
            this.txtGradeDV.Location = new System.Drawing.Point(8, 37);
            this.txtGradeDV.MaxLength = 1;
            this.txtGradeDV.Name = "txtGradeDV";
            this.txtGradeDV.PasswordChar = '\0';
            this.txtGradeDV.PromptText = "Digito";
            this.txtGradeDV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtGradeDV.SelectedText = "";
            this.txtGradeDV.SelectionLength = 0;
            this.txtGradeDV.SelectionStart = 0;
            this.txtGradeDV.ShortcutsEnabled = true;
            this.txtGradeDV.Size = new System.Drawing.Size(47, 23);
            this.txtGradeDV.TabIndex = 21;
            this.txtGradeDV.UseSelectable = true;
            this.txtGradeDV.WaterMark = "Digito";
            this.txtGradeDV.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtGradeDV.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtGradeDV.TextChanged += new System.EventHandler(this.txtGradeDV_TextChanged);
            this.txtGradeDV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyInt);
            this.txtGradeDV.Validating += new System.ComponentModel.CancelEventHandler(this.txtGradeDV_Validating);
            // 
            // txtValorCusto
            // 
            // 
            // 
            // 
            this.txtValorCusto.CustomButton.Image = null;
            this.txtValorCusto.CustomButton.Location = new System.Drawing.Point(58, 1);
            this.txtValorCusto.CustomButton.Name = "";
            this.txtValorCusto.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtValorCusto.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtValorCusto.CustomButton.TabIndex = 1;
            this.txtValorCusto.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtValorCusto.CustomButton.UseSelectable = true;
            this.txtValorCusto.CustomButton.Visible = false;
            this.txtValorCusto.Lines = new string[0];
            this.txtValorCusto.Location = new System.Drawing.Point(378, 37);
            this.txtValorCusto.MaxLength = 32767;
            this.txtValorCusto.Name = "txtValorCusto";
            this.txtValorCusto.PasswordChar = '\0';
            this.txtValorCusto.PromptText = "Valor Custo";
            this.txtValorCusto.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtValorCusto.SelectedText = "";
            this.txtValorCusto.SelectionLength = 0;
            this.txtValorCusto.SelectionStart = 0;
            this.txtValorCusto.ShortcutsEnabled = true;
            this.txtValorCusto.Size = new System.Drawing.Size(80, 23);
            this.txtValorCusto.TabIndex = 20;
            this.txtValorCusto.UseSelectable = true;
            this.txtValorCusto.WaterMark = "Valor Custo";
            this.txtValorCusto.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtValorCusto.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtValorCusto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyNumbers);
            // 
            // txtValorSaida
            // 
            // 
            // 
            // 
            this.txtValorSaida.CustomButton.Image = null;
            this.txtValorSaida.CustomButton.Location = new System.Drawing.Point(58, 1);
            this.txtValorSaida.CustomButton.Name = "";
            this.txtValorSaida.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtValorSaida.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtValorSaida.CustomButton.TabIndex = 1;
            this.txtValorSaida.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtValorSaida.CustomButton.UseSelectable = true;
            this.txtValorSaida.CustomButton.Visible = false;
            this.txtValorSaida.Lines = new string[0];
            this.txtValorSaida.Location = new System.Drawing.Point(292, 37);
            this.txtValorSaida.MaxLength = 32767;
            this.txtValorSaida.Name = "txtValorSaida";
            this.txtValorSaida.PasswordChar = '\0';
            this.txtValorSaida.PromptText = "Valor Saída";
            this.txtValorSaida.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtValorSaida.SelectedText = "";
            this.txtValorSaida.SelectionLength = 0;
            this.txtValorSaida.SelectionStart = 0;
            this.txtValorSaida.ShortcutsEnabled = true;
            this.txtValorSaida.Size = new System.Drawing.Size(80, 23);
            this.txtValorSaida.TabIndex = 18;
            this.txtValorSaida.UseSelectable = true;
            this.txtValorSaida.WaterMark = "Valor Saída";
            this.txtValorSaida.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtValorSaida.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtValorSaida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyNumbers);
            // 
            // cbbGradeTamanho
            // 
            this.cbbGradeTamanho.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbbGradeTamanho.FormattingEnabled = true;
            this.cbbGradeTamanho.ItemHeight = 19;
            this.cbbGradeTamanho.Location = new System.Drawing.Point(169, 36);
            this.cbbGradeTamanho.Name = "cbbGradeTamanho";
            this.cbbGradeTamanho.PromptText = "Tam";
            this.cbbGradeTamanho.Size = new System.Drawing.Size(118, 25);
            this.cbbGradeTamanho.TabIndex = 17;
            this.cbbGradeTamanho.UseSelectable = true;
            // 
            // cbbGradeCor
            // 
            this.cbbGradeCor.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbbGradeCor.FormattingEnabled = true;
            this.cbbGradeCor.ItemHeight = 19;
            this.cbbGradeCor.Location = new System.Drawing.Point(61, 36);
            this.cbbGradeCor.Name = "cbbGradeCor";
            this.cbbGradeCor.PromptText = "Cor";
            this.cbbGradeCor.Size = new System.Drawing.Size(102, 25);
            this.cbbGradeCor.TabIndex = 16;
            this.cbbGradeCor.UseSelectable = true;
            // 
            // btnGradeCancelar
            // 
            this.btnGradeCancelar.Enabled = false;
            this.btnGradeCancelar.Location = new System.Drawing.Point(536, 36);
            this.btnGradeCancelar.Name = "btnGradeCancelar";
            this.btnGradeCancelar.Size = new System.Drawing.Size(67, 23);
            this.btnGradeCancelar.TabIndex = 15;
            this.btnGradeCancelar.Text = "Cancelar";
            this.btnGradeCancelar.UseSelectable = true;
            this.btnGradeCancelar.Click += new System.EventHandler(this.btnGradeCancelar_Click);
            // 
            // btnGradeConfirmar
            // 
            this.btnGradeConfirmar.Enabled = false;
            this.btnGradeConfirmar.Location = new System.Drawing.Point(464, 36);
            this.btnGradeConfirmar.Name = "btnGradeConfirmar";
            this.btnGradeConfirmar.Size = new System.Drawing.Size(66, 23);
            this.btnGradeConfirmar.TabIndex = 14;
            this.btnGradeConfirmar.Text = "Confirmar";
            this.btnGradeConfirmar.UseSelectable = true;
            this.btnGradeConfirmar.Click += new System.EventHandler(this.btnGradeConfirmar_Click);
            // 
            // lblAdicionarGrade
            // 
            this.lblAdicionarGrade.AutoSize = true;
            this.lblAdicionarGrade.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblAdicionarGrade.Location = new System.Drawing.Point(8, 16);
            this.lblAdicionarGrade.Name = "lblAdicionarGrade";
            this.lblAdicionarGrade.Size = new System.Drawing.Size(119, 19);
            this.lblAdicionarGrade.TabIndex = 13;
            this.lblAdicionarGrade.Text = "Adicionar Grade";
            // 
            // pnlProdutoItem
            // 
            this.pnlProdutoItem.Controls.Add(this.grpAdicionarGrade);
            this.pnlProdutoItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProdutoItem.Enabled = false;
            this.pnlProdutoItem.HorizontalScrollbarBarColor = true;
            this.pnlProdutoItem.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProdutoItem.HorizontalScrollbarSize = 10;
            this.pnlProdutoItem.Location = new System.Drawing.Point(20, 292);
            this.pnlProdutoItem.Name = "pnlProdutoItem";
            this.pnlProdutoItem.Size = new System.Drawing.Size(679, 94);
            this.pnlProdutoItem.TabIndex = 1;
            this.pnlProdutoItem.VerticalScrollbarBarColor = true;
            this.pnlProdutoItem.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProdutoItem.VerticalScrollbarSize = 10;
            // 
            // pblProdutoBottom
            // 
            this.pblProdutoBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pblProdutoBottom.HorizontalScrollbarBarColor = true;
            this.pblProdutoBottom.HorizontalScrollbarHighlightOnWheel = false;
            this.pblProdutoBottom.HorizontalScrollbarSize = 10;
            this.pblProdutoBottom.Location = new System.Drawing.Point(20, 640);
            this.pblProdutoBottom.Name = "pblProdutoBottom";
            this.pblProdutoBottom.Size = new System.Drawing.Size(679, 43);
            this.pblProdutoBottom.TabIndex = 2;
            this.pblProdutoBottom.VerticalScrollbarBarColor = true;
            this.pblProdutoBottom.VerticalScrollbarHighlightOnWheel = false;
            this.pblProdutoBottom.VerticalScrollbarSize = 10;
            // 
            // pnlProdutoGrade
            // 
            this.pnlProdutoGrade.Controls.Add(this.grdProdutoGrade);
            this.pnlProdutoGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProdutoGrade.HorizontalScrollbarBarColor = true;
            this.pnlProdutoGrade.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGrade.HorizontalScrollbarSize = 10;
            this.pnlProdutoGrade.Location = new System.Drawing.Point(20, 386);
            this.pnlProdutoGrade.Name = "pnlProdutoGrade";
            this.pnlProdutoGrade.Size = new System.Drawing.Size(679, 254);
            this.pnlProdutoGrade.TabIndex = 3;
            this.pnlProdutoGrade.VerticalScrollbarBarColor = true;
            this.pnlProdutoGrade.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGrade.VerticalScrollbarSize = 10;
            // 
            // grdProdutoGrade
            // 
            this.grdProdutoGrade.AllowUserToAddRows = false;
            this.grdProdutoGrade.AllowUserToDeleteRows = false;
            this.grdProdutoGrade.AllowUserToOrderColumns = true;
            this.grdProdutoGrade.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdProdutoGrade.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdProdutoGrade.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdProdutoGrade.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdProdutoGrade.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdProdutoGrade.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdProdutoGrade.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdProdutoGrade.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdProdutoGrade.ColumnHeadersHeight = 22;
            this.grdProdutoGrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdProdutoGrade.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdProdutoGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProdutoGrade.EnableHeadersVisualStyles = false;
            this.grdProdutoGrade.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdProdutoGrade.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdProdutoGrade.Location = new System.Drawing.Point(0, 0);
            this.grdProdutoGrade.MultiSelect = false;
            this.grdProdutoGrade.Name = "grdProdutoGrade";
            this.grdProdutoGrade.ReadOnly = true;
            this.grdProdutoGrade.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdProdutoGrade.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdProdutoGrade.RowHeadersVisible = false;
            this.grdProdutoGrade.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdProdutoGrade.RowTemplate.ReadOnly = true;
            this.grdProdutoGrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdProdutoGrade.ShowEditingIcon = false;
            this.grdProdutoGrade.Size = new System.Drawing.Size(679, 254);
            this.grdProdutoGrade.StandardTab = true;
            this.grdProdutoGrade.TabIndex = 3;
            this.grdProdutoGrade.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdProdutoGrade_CellDoubleClick);
            this.grdProdutoGrade.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdProdutoGrade_RowPostPaint);
            this.grdProdutoGrade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdProdutoGrade_KeyUp);
            // 
            // FormProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 703);
            this.Controls.Add(this.pnlProdutoGrade);
            this.Controls.Add(this.pblProdutoBottom);
            this.Controls.Add(this.pnlProdutoItem);
            this.Controls.Add(this.pnlProduto);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(719, 703);
            this.Name = "FormProduto";
            this.Text = "Cadastro de Produto";
            this.Load += new System.EventHandler(this.FormProduto_Load);
            this.pnlProduto.ResumeLayout(false);
            this.grpProduto.ResumeLayout(false);
            this.grpProduto.PerformLayout();
            this.grpPesquisa.ResumeLayout(false);
            this.grpAdicionarGrade.ResumeLayout(false);
            this.grpAdicionarGrade.PerformLayout();
            this.pnlProdutoItem.ResumeLayout(false);
            this.pnlProdutoGrade.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProdutoGrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlProduto;
        private System.Windows.Forms.GroupBox grpProduto;
        public MetroFramework.Controls.MetroButton btnCancelarProduto;
        public MetroFramework.Controls.MetroButton btnExcluirProduto;
        public MetroFramework.Controls.MetroButton btnSalvarProduto;
        private MetroFramework.Controls.MetroLabel lblFornecedor;
        public MetroFramework.Controls.MetroComboBox cbbFornecedor;
        private MetroFramework.Controls.MetroLabel lblCategoria;
        public MetroFramework.Controls.MetroComboBox cbbCategoria;
        private MetroFramework.Controls.MetroLabel lblDigitoVerificador;
        private MetroFramework.Controls.MetroLabel lblCodigoBarras;
        public MetroFramework.Controls.MetroTextBox txtDigitoVerificador;
        public MetroFramework.Controls.MetroTextBox txtCodigoBarras;
        private MetroFramework.Controls.MetroLabel lblUnidade;
        public MetroFramework.Controls.MetroTextBox txtUnidade;
        public MetroFramework.Controls.MetroTextBox txtNomeProduto;
        private MetroFramework.Controls.MetroLabel lblNomeProduto;
        private System.Windows.Forms.GroupBox grpPesquisa;
        public MetroFramework.Controls.MetroTextBox txtPesquisaCodProduto;
        public MetroFramework.Controls.MetroComboBox cbbPesquisaProduto;
        public MetroFramework.Controls.MetroButton btnNovoProduto;
        private System.Windows.Forms.GroupBox grpAdicionarGrade;
        public MetroFramework.Controls.MetroTextBox txtValorCusto;
        public MetroFramework.Controls.MetroTextBox txtValorSaida;
        public MetroFramework.Controls.MetroComboBox cbbGradeTamanho;
        public MetroFramework.Controls.MetroComboBox cbbGradeCor;
        public MetroFramework.Controls.MetroButton btnGradeCancelar;
        public MetroFramework.Controls.MetroButton btnGradeConfirmar;
        private MetroFramework.Controls.MetroLabel lblAdicionarGrade;
        private MetroFramework.Controls.MetroPanel pnlProdutoItem;
        private MetroFramework.Controls.MetroPanel pblProdutoBottom;
        private MetroFramework.Controls.MetroPanel pnlProdutoGrade;
        public MetroFramework.Controls.MetroGrid grdProdutoGrade;
        public MetroFramework.Controls.MetroTextBox txtGradeDV;
    }
}