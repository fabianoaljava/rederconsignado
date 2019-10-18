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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new MetroFramework.Controls.MetroPanel();
            this.pnlBottom = new MetroFramework.Controls.MetroPanel();
            this.btnFechar = new MetroFramework.Controls.MetroButton();
            this.pnlMain = new MetroFramework.Controls.MetroPanel();
            this.grdEstoque = new MetroFramework.Controls.MetroGrid();
            this.cbbProdutoSaldo = new MetroFramework.Controls.MetroComboBox();
            this.btnProdutosLimpar = new MetroFramework.Controls.MetroButton();
            this.txtProdutosNome = new MetroFramework.Controls.MetroTextBox();
            this.txtProdutosCodigoBarras = new MetroFramework.Controls.MetroTextBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox2 = new MetroFramework.Controls.MetroTextBox();
            this.lblValor = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstoque)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.metroLabel3);
            this.pnlTop.Controls.Add(this.metroLabel2);
            this.pnlTop.Controls.Add(this.metroLabel1);
            this.pnlTop.Controls.Add(this.lblValor);
            this.pnlTop.Controls.Add(this.metroTextBox2);
            this.pnlTop.Controls.Add(this.metroTextBox1);
            this.pnlTop.Controls.Add(this.metroButton1);
            this.pnlTop.Controls.Add(this.cbbProdutoSaldo);
            this.pnlTop.Controls.Add(this.btnProdutosLimpar);
            this.pnlTop.Controls.Add(this.txtProdutosNome);
            this.pnlTop.Controls.Add(this.txtProdutosCodigoBarras);
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
            this.btnFechar.Enabled = false;
            this.btnFechar.Location = new System.Drawing.Point(685, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 39);
            this.btnFechar.TabIndex = 11;
            this.btnFechar.Text = "&Fechar";
            this.btnFechar.UseSelectable = true;
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
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdEstoque.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grdEstoque.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdEstoque.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdEstoque.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdEstoque.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdEstoque.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdEstoque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdEstoque.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdEstoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEstoque.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdEstoque.EnableHeadersVisualStyles = false;
            this.grdEstoque.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdEstoque.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdEstoque.Location = new System.Drawing.Point(0, 0);
            this.grdEstoque.Name = "grdEstoque";
            this.grdEstoque.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdEstoque.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdEstoque.RowHeadersVisible = false;
            this.grdEstoque.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdEstoque.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdEstoque.ShowEditingIcon = false;
            this.grdEstoque.Size = new System.Drawing.Size(760, 231);
            this.grdEstoque.TabIndex = 5;
            // 
            // cbbProdutoSaldo
            // 
            this.cbbProdutoSaldo.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbbProdutoSaldo.FormattingEnabled = true;
            this.cbbProdutoSaldo.ItemHeight = 19;
            this.cbbProdutoSaldo.Items.AddRange(new object[] {
            "Entrada",
            "Saída"});
            this.cbbProdutoSaldo.Location = new System.Drawing.Point(390, 22);
            this.cbbProdutoSaldo.Name = "cbbProdutoSaldo";
            this.cbbProdutoSaldo.Size = new System.Drawing.Size(101, 25);
            this.cbbProdutoSaldo.TabIndex = 33;
            this.cbbProdutoSaldo.UseSelectable = true;
            // 
            // btnProdutosLimpar
            // 
            this.btnProdutosLimpar.Location = new System.Drawing.Point(588, 52);
            this.btnProdutosLimpar.Name = "btnProdutosLimpar";
            this.btnProdutosLimpar.Size = new System.Drawing.Size(91, 41);
            this.btnProdutosLimpar.TabIndex = 34;
            this.btnProdutosLimpar.Text = "Adicionar";
            this.btnProdutosLimpar.UseSelectable = true;
            // 
            // txtProdutosNome
            // 
            // 
            // 
            // 
            this.txtProdutosNome.CustomButton.Image = null;
            this.txtProdutosNome.CustomButton.Location = new System.Drawing.Point(242, 1);
            this.txtProdutosNome.CustomButton.Name = "";
            this.txtProdutosNome.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtProdutosNome.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProdutosNome.CustomButton.TabIndex = 1;
            this.txtProdutosNome.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProdutosNome.CustomButton.UseSelectable = true;
            this.txtProdutosNome.CustomButton.Visible = false;
            this.txtProdutosNome.Lines = new string[0];
            this.txtProdutosNome.Location = new System.Drawing.Point(120, 23);
            this.txtProdutosNome.MaxLength = 32767;
            this.txtProdutosNome.Name = "txtProdutosNome";
            this.txtProdutosNome.PasswordChar = '\0';
            this.txtProdutosNome.PromptText = "Nome do Produto";
            this.txtProdutosNome.ReadOnly = true;
            this.txtProdutosNome.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProdutosNome.SelectedText = "";
            this.txtProdutosNome.SelectionLength = 0;
            this.txtProdutosNome.SelectionStart = 0;
            this.txtProdutosNome.ShortcutsEnabled = true;
            this.txtProdutosNome.Size = new System.Drawing.Size(264, 23);
            this.txtProdutosNome.TabIndex = 32;
            this.txtProdutosNome.UseSelectable = true;
            this.txtProdutosNome.WaterMark = "Nome do Produto";
            this.txtProdutosNome.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProdutosNome.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtProdutosCodigoBarras
            // 
            // 
            // 
            // 
            this.txtProdutosCodigoBarras.CustomButton.Image = global::ConsignadoDeposito.Properties.Resources.darkmagnify1;
            this.txtProdutosCodigoBarras.CustomButton.Location = new System.Drawing.Point(88, 1);
            this.txtProdutosCodigoBarras.CustomButton.Name = "";
            this.txtProdutosCodigoBarras.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtProdutosCodigoBarras.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProdutosCodigoBarras.CustomButton.TabIndex = 1;
            this.txtProdutosCodigoBarras.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProdutosCodigoBarras.CustomButton.UseSelectable = true;
            this.txtProdutosCodigoBarras.Lines = new string[0];
            this.txtProdutosCodigoBarras.Location = new System.Drawing.Point(9, 23);
            this.txtProdutosCodigoBarras.MaxLength = 32767;
            this.txtProdutosCodigoBarras.Name = "txtProdutosCodigoBarras";
            this.txtProdutosCodigoBarras.PasswordChar = '\0';
            this.txtProdutosCodigoBarras.PromptText = "Código de Barras";
            this.txtProdutosCodigoBarras.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProdutosCodigoBarras.SelectedText = "";
            this.txtProdutosCodigoBarras.SelectionLength = 0;
            this.txtProdutosCodigoBarras.SelectionStart = 0;
            this.txtProdutosCodigoBarras.ShortcutsEnabled = true;
            this.txtProdutosCodigoBarras.ShowButton = true;
            this.txtProdutosCodigoBarras.Size = new System.Drawing.Size(110, 23);
            this.txtProdutosCodigoBarras.TabIndex = 31;
            this.txtProdutosCodigoBarras.UseSelectable = true;
            this.txtProdutosCodigoBarras.WaterMark = "Código de Barras";
            this.txtProdutosCodigoBarras.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProdutosCodigoBarras.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(685, 52);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(59, 41);
            this.metroButton1.TabIndex = 35;
            this.metroButton1.Text = "Cancelar";
            this.metroButton1.UseSelectable = true;
            // 
            // metroTextBox1
            // 
            // 
            // 
            // 
            this.metroTextBox1.CustomButton.Image = null;
            this.metroTextBox1.CustomButton.Location = new System.Drawing.Point(63, 1);
            this.metroTextBox1.CustomButton.Name = "";
            this.metroTextBox1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox1.CustomButton.TabIndex = 1;
            this.metroTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox1.CustomButton.UseSelectable = true;
            this.metroTextBox1.CustomButton.Visible = false;
            this.metroTextBox1.Lines = new string[0];
            this.metroTextBox1.Location = new System.Drawing.Point(497, 23);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.PromptText = "Quantidade";
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.SelectionLength = 0;
            this.metroTextBox1.SelectionStart = 0;
            this.metroTextBox1.ShortcutsEnabled = true;
            this.metroTextBox1.Size = new System.Drawing.Size(85, 23);
            this.metroTextBox1.TabIndex = 36;
            this.metroTextBox1.UseSelectable = true;
            this.metroTextBox1.WaterMark = "Quantidade";
            this.metroTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox2
            // 
            // 
            // 
            // 
            this.metroTextBox2.CustomButton.Image = null;
            this.metroTextBox2.CustomButton.Location = new System.Drawing.Point(551, 1);
            this.metroTextBox2.CustomButton.Name = "";
            this.metroTextBox2.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.metroTextBox2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox2.CustomButton.TabIndex = 1;
            this.metroTextBox2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox2.CustomButton.UseSelectable = true;
            this.metroTextBox2.CustomButton.Visible = false;
            this.metroTextBox2.Lines = new string[0];
            this.metroTextBox2.Location = new System.Drawing.Point(9, 52);
            this.metroTextBox2.MaxLength = 32767;
            this.metroTextBox2.Multiline = true;
            this.metroTextBox2.Name = "metroTextBox2";
            this.metroTextBox2.PasswordChar = '\0';
            this.metroTextBox2.PromptText = "Observações";
            this.metroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox2.SelectedText = "";
            this.metroTextBox2.SelectionLength = 0;
            this.metroTextBox2.SelectionStart = 0;
            this.metroTextBox2.ShortcutsEnabled = true;
            this.metroTextBox2.Size = new System.Drawing.Size(573, 41);
            this.metroTextBox2.TabIndex = 37;
            this.metroTextBox2.UseSelectable = true;
            this.metroTextBox2.WaterMark = "Observações";
            this.metroTextBox2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblValor.Location = new System.Drawing.Point(6, 5);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(99, 15);
            this.lblValor.TabIndex = 38;
            this.lblValor.Text = "Códugo de Barras";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel1.Location = new System.Drawing.Point(117, 5);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(101, 15);
            this.metroLabel1.TabIndex = 39;
            this.metroLabel1.Text = "Nome do Produto";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel2.Location = new System.Drawing.Point(387, 4);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(106, 15);
            this.metroLabel2.TabIndex = 40;
            this.metroLabel2.Text = "Tipo Movimentação";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel3.Location = new System.Drawing.Point(494, 5);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(66, 15);
            this.metroLabel3.TabIndex = 41;
            this.metroLabel3.Text = "Quantidade";
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
        public MetroFramework.Controls.MetroTextBox metroTextBox2;
        public MetroFramework.Controls.MetroTextBox metroTextBox1;
        public MetroFramework.Controls.MetroButton metroButton1;
        public MetroFramework.Controls.MetroComboBox cbbProdutoSaldo;
        public MetroFramework.Controls.MetroButton btnProdutosLimpar;
        public MetroFramework.Controls.MetroTextBox txtProdutosNome;
        public MetroFramework.Controls.MetroTextBox txtProdutosCodigoBarras;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel lblValor;
    }
}