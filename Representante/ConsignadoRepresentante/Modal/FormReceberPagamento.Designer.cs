namespace ConsignadoRepresentante.Modal
{
    partial class FormReceberPagamento
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlReceberTop = new MetroFramework.Controls.MetroPanel();
            this.grpRecebimento = new System.Windows.Forms.GroupBox();
            this.btnConfirmar = new MetroFramework.Controls.MetroButton();
            this.txtValorRecebido = new MetroFramework.Controls.MetroTextBox();
            this.lblValorRecebido = new MetroFramework.Controls.MetroLabel();
            this.pnlBottomForm = new MetroFramework.Controls.MetroPanel();
            this.btnFechar = new MetroFramework.Controls.MetroButton();
            this.pnlReceberMain = new MetroFramework.Controls.MetroPanel();
            this.cbbFormaPagamento = new MetroFramework.Controls.MetroComboBox();
            this.lblFormaPagamento = new MetroFramework.Controls.MetroLabel();
            this.lblReferencia = new MetroFramework.Controls.MetroLabel();
            this.cbbReferencia = new MetroFramework.Controls.MetroComboBox();
            this.txtObservacao = new MetroFramework.Controls.MetroTextBox();
            this.grdRecebimentos = new MetroFramework.Controls.MetroGrid();
            this.btnLimpar = new MetroFramework.Controls.MetroButton();
            this.pnlReceberTop.SuspendLayout();
            this.grpRecebimento.SuspendLayout();
            this.pnlBottomForm.SuspendLayout();
            this.pnlReceberMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecebimentos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlReceberTop
            // 
            this.pnlReceberTop.Controls.Add(this.grpRecebimento);
            this.pnlReceberTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReceberTop.HorizontalScrollbarBarColor = true;
            this.pnlReceberTop.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlReceberTop.HorizontalScrollbarSize = 10;
            this.pnlReceberTop.Location = new System.Drawing.Point(20, 60);
            this.pnlReceberTop.Name = "pnlReceberTop";
            this.pnlReceberTop.Size = new System.Drawing.Size(728, 152);
            this.pnlReceberTop.TabIndex = 0;
            this.pnlReceberTop.VerticalScrollbarBarColor = true;
            this.pnlReceberTop.VerticalScrollbarHighlightOnWheel = false;
            this.pnlReceberTop.VerticalScrollbarSize = 10;
            // 
            // grpRecebimento
            // 
            this.grpRecebimento.BackColor = System.Drawing.Color.White;
            this.grpRecebimento.Controls.Add(this.btnLimpar);
            this.grpRecebimento.Controls.Add(this.txtObservacao);
            this.grpRecebimento.Controls.Add(this.lblReferencia);
            this.grpRecebimento.Controls.Add(this.cbbReferencia);
            this.grpRecebimento.Controls.Add(this.lblFormaPagamento);
            this.grpRecebimento.Controls.Add(this.cbbFormaPagamento);
            this.grpRecebimento.Controls.Add(this.btnConfirmar);
            this.grpRecebimento.Controls.Add(this.txtValorRecebido);
            this.grpRecebimento.Controls.Add(this.lblValorRecebido);
            this.grpRecebimento.Location = new System.Drawing.Point(13, 11);
            this.grpRecebimento.Name = "grpRecebimento";
            this.grpRecebimento.Size = new System.Drawing.Size(693, 130);
            this.grpRecebimento.TabIndex = 27;
            this.grpRecebimento.TabStop = false;
            this.grpRecebimento.Text = "Recebimento";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(544, 80);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(66, 44);
            this.btnConfirmar.TabIndex = 4;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseSelectable = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtValorRecebido
            // 
            // 
            // 
            // 
            this.txtValorRecebido.CustomButton.Image = null;
            this.txtValorRecebido.CustomButton.Location = new System.Drawing.Point(76, 1);
            this.txtValorRecebido.CustomButton.Name = "";
            this.txtValorRecebido.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtValorRecebido.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtValorRecebido.CustomButton.TabIndex = 1;
            this.txtValorRecebido.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtValorRecebido.CustomButton.UseSelectable = true;
            this.txtValorRecebido.CustomButton.Visible = false;
            this.txtValorRecebido.Lines = new string[0];
            this.txtValorRecebido.Location = new System.Drawing.Point(453, 45);
            this.txtValorRecebido.MaxLength = 32767;
            this.txtValorRecebido.Name = "txtValorRecebido";
            this.txtValorRecebido.PasswordChar = '\0';
            this.txtValorRecebido.PromptText = "Valor Recebido";
            this.txtValorRecebido.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtValorRecebido.SelectedText = "";
            this.txtValorRecebido.SelectionLength = 0;
            this.txtValorRecebido.SelectionStart = 0;
            this.txtValorRecebido.ShortcutsEnabled = true;
            this.txtValorRecebido.Size = new System.Drawing.Size(104, 29);
            this.txtValorRecebido.TabIndex = 1;
            this.txtValorRecebido.UseSelectable = true;
            this.txtValorRecebido.WaterMark = "Valor Recebido";
            this.txtValorRecebido.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtValorRecebido.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtValorRecebido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyNumbers);
            this.txtValorRecebido.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // lblValorRecebido
            // 
            this.lblValorRecebido.AutoSize = true;
            this.lblValorRecebido.Location = new System.Drawing.Point(453, 23);
            this.lblValorRecebido.Name = "lblValorRecebido";
            this.lblValorRecebido.Size = new System.Drawing.Size(100, 19);
            this.lblValorRecebido.TabIndex = 26;
            this.lblValorRecebido.Text = "Valor Recebido:";
            // 
            // pnlBottomForm
            // 
            this.pnlBottomForm.Controls.Add(this.btnFechar);
            this.pnlBottomForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomForm.HorizontalScrollbarBarColor = true;
            this.pnlBottomForm.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlBottomForm.HorizontalScrollbarSize = 10;
            this.pnlBottomForm.Location = new System.Drawing.Point(20, 488);
            this.pnlBottomForm.Name = "pnlBottomForm";
            this.pnlBottomForm.Size = new System.Drawing.Size(728, 39);
            this.pnlBottomForm.TabIndex = 5;
            this.pnlBottomForm.VerticalScrollbarBarColor = true;
            this.pnlBottomForm.VerticalScrollbarHighlightOnWheel = false;
            this.pnlBottomForm.VerticalScrollbarSize = 10;
            // 
            // btnFechar
            // 
            this.btnFechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFechar.Location = new System.Drawing.Point(653, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 39);
            this.btnFechar.TabIndex = 7;
            this.btnFechar.Text = "&Fechar";
            this.btnFechar.UseSelectable = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // pnlReceberMain
            // 
            this.pnlReceberMain.Controls.Add(this.grdRecebimentos);
            this.pnlReceberMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReceberMain.HorizontalScrollbarBarColor = true;
            this.pnlReceberMain.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlReceberMain.HorizontalScrollbarSize = 10;
            this.pnlReceberMain.Location = new System.Drawing.Point(20, 212);
            this.pnlReceberMain.Name = "pnlReceberMain";
            this.pnlReceberMain.Size = new System.Drawing.Size(728, 276);
            this.pnlReceberMain.TabIndex = 6;
            this.pnlReceberMain.VerticalScrollbarBarColor = true;
            this.pnlReceberMain.VerticalScrollbarHighlightOnWheel = false;
            this.pnlReceberMain.VerticalScrollbarSize = 10;
            // 
            // cbbFormaPagamento
            // 
            this.cbbFormaPagamento.FormattingEnabled = true;
            this.cbbFormaPagamento.ItemHeight = 23;
            this.cbbFormaPagamento.Items.AddRange(new object[] {
            "Dinheiro",
            "Cheque",
            "Cartão Crédito",
            "Cartão Débito",
            "Boleto",
            "Outros"});
            this.cbbFormaPagamento.Location = new System.Drawing.Point(563, 45);
            this.cbbFormaPagamento.Name = "cbbFormaPagamento";
            this.cbbFormaPagamento.PromptText = "Forma de pagamento";
            this.cbbFormaPagamento.Size = new System.Drawing.Size(122, 29);
            this.cbbFormaPagamento.TabIndex = 2;
            this.cbbFormaPagamento.UseSelectable = true;
            this.cbbFormaPagamento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Location = new System.Drawing.Point(563, 23);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(122, 19);
            this.lblFormaPagamento.TabIndex = 36;
            this.lblFormaPagamento.Text = "Forma Pagamento:";
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.Location = new System.Drawing.Point(6, 23);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(73, 19);
            this.lblReferencia.TabIndex = 38;
            this.lblReferencia.Text = "Referencia:";
            // 
            // cbbReferencia
            // 
            this.cbbReferencia.FormattingEnabled = true;
            this.cbbReferencia.ItemHeight = 23;
            this.cbbReferencia.Location = new System.Drawing.Point(7, 45);
            this.cbbReferencia.Name = "cbbReferencia";
            this.cbbReferencia.PromptText = "Selecione a Referencia";
            this.cbbReferencia.Size = new System.Drawing.Size(440, 29);
            this.cbbReferencia.TabIndex = 0;
            this.cbbReferencia.UseSelectable = true;
            this.cbbReferencia.SelectedIndexChanged += new System.EventHandler(this.cbbReferencia_SelectedIndexChanged);
            this.cbbReferencia.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            // 
            // txtObservacao
            // 
            // 
            // 
            // 
            this.txtObservacao.CustomButton.Image = null;
            this.txtObservacao.CustomButton.Location = new System.Drawing.Point(489, 2);
            this.txtObservacao.CustomButton.Name = "";
            this.txtObservacao.CustomButton.Size = new System.Drawing.Size(39, 39);
            this.txtObservacao.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtObservacao.CustomButton.TabIndex = 1;
            this.txtObservacao.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtObservacao.CustomButton.UseSelectable = true;
            this.txtObservacao.CustomButton.Visible = false;
            this.txtObservacao.Lines = new string[0];
            this.txtObservacao.Location = new System.Drawing.Point(7, 80);
            this.txtObservacao.MaxLength = 32767;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.PasswordChar = '\0';
            this.txtObservacao.PromptText = "Observação";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtObservacao.SelectedText = "";
            this.txtObservacao.SelectionLength = 0;
            this.txtObservacao.SelectionStart = 0;
            this.txtObservacao.ShortcutsEnabled = true;
            this.txtObservacao.Size = new System.Drawing.Size(531, 44);
            this.txtObservacao.TabIndex = 3;
            this.txtObservacao.UseSelectable = true;
            this.txtObservacao.WaterMark = "Observação";
            this.txtObservacao.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtObservacao.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // grdRecebimentos
            // 
            this.grdRecebimentos.AllowUserToAddRows = false;
            this.grdRecebimentos.AllowUserToDeleteRows = false;
            this.grdRecebimentos.AllowUserToOrderColumns = true;
            this.grdRecebimentos.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdRecebimentos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.grdRecebimentos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdRecebimentos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdRecebimentos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdRecebimentos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdRecebimentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.grdRecebimentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdRecebimentos.DefaultCellStyle = dataGridViewCellStyle11;
            this.grdRecebimentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRecebimentos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdRecebimentos.EnableHeadersVisualStyles = false;
            this.grdRecebimentos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdRecebimentos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdRecebimentos.Location = new System.Drawing.Point(0, 0);
            this.grdRecebimentos.Name = "grdRecebimentos";
            this.grdRecebimentos.ReadOnly = true;
            this.grdRecebimentos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdRecebimentos.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.grdRecebimentos.RowHeadersVisible = false;
            this.grdRecebimentos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdRecebimentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdRecebimentos.ShowEditingIcon = false;
            this.grdRecebimentos.Size = new System.Drawing.Size(728, 276);
            this.grdRecebimentos.TabIndex = 6;
            this.grdRecebimentos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdRecebimentos_CellDoubleClick);
            this.grdRecebimentos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdRecebimentos_KeyUp);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(619, 80);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(66, 44);
            this.btnLimpar.TabIndex = 5;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseSelectable = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // FormReceberPagamento
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 547);
            this.Controls.Add(this.pnlReceberMain);
            this.Controls.Add(this.pnlBottomForm);
            this.Controls.Add(this.pnlReceberTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReceberPagamento";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Receber Pagamento";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormReceberPagamento_Load);
            this.pnlReceberTop.ResumeLayout(false);
            this.grpRecebimento.ResumeLayout(false);
            this.grpRecebimento.PerformLayout();
            this.pnlBottomForm.ResumeLayout(false);
            this.pnlReceberMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRecebimentos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlReceberTop;
        public System.Windows.Forms.GroupBox grpRecebimento;
        public MetroFramework.Controls.MetroButton btnConfirmar;
        public MetroFramework.Controls.MetroTextBox txtValorRecebido;
        private MetroFramework.Controls.MetroLabel lblValorRecebido;
        private MetroFramework.Controls.MetroPanel pnlBottomForm;
        private MetroFramework.Controls.MetroButton btnFechar;
        private MetroFramework.Controls.MetroPanel pnlReceberMain;
        private MetroFramework.Controls.MetroLabel lblFormaPagamento;
        private MetroFramework.Controls.MetroComboBox cbbFormaPagamento;
        private MetroFramework.Controls.MetroLabel lblReferencia;
        private MetroFramework.Controls.MetroComboBox cbbReferencia;
        private MetroFramework.Controls.MetroTextBox txtObservacao;
        public MetroFramework.Controls.MetroGrid grdRecebimentos;
        public MetroFramework.Controls.MetroButton btnLimpar;
    }
}