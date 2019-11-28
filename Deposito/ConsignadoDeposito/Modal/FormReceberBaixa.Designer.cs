namespace ConsignadoDeposito.Modal
{
    partial class FormRecebimento
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
            this.lblFormaPagamento = new MetroFramework.Controls.MetroLabel();
            this.cbbFormaPagamento = new MetroFramework.Controls.MetroComboBox();
            this.btnCancelar = new MetroFramework.Controls.MetroButton();
            this.btnConfirmar = new MetroFramework.Controls.MetroButton();
            this.lblDataPagamento = new MetroFramework.Controls.MetroLabel();
            this.cbbDataPagamento = new MetroFramework.Controls.MetroDateTime();
            this.lblValor = new MetroFramework.Controls.MetroLabel();
            this.txtValorRecebido = new MetroFramework.Controls.MetroTextBox();
            this.pnlBottom = new MetroFramework.Controls.MetroPanel();
            this.btnFechar = new MetroFramework.Controls.MetroButton();
            this.pnlMain = new MetroFramework.Controls.MetroPanel();
            this.grdRecebimento = new MetroFramework.Controls.MetroGrid();
            this.txtObservacao = new MetroFramework.Controls.MetroTextBox();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            this.dlbTotalAReceber = new MetroFramework.Controls.MetroLabel();
            this.dlbTotalRecebido = new MetroFramework.Controls.MetroLabel();
            this.lblTotalRecebido = new MetroFramework.Controls.MetroLabel();
            this.lblTotalAReceber = new MetroFramework.Controls.MetroLabel();
            this.txtReferencia = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecebimento)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.metroLabel1);
            this.pnlTop.Controls.Add(this.txtReferencia);
            this.pnlTop.Controls.Add(this.lblStatus);
            this.pnlTop.Controls.Add(this.txtObservacao);
            this.pnlTop.Controls.Add(this.lblFormaPagamento);
            this.pnlTop.Controls.Add(this.cbbFormaPagamento);
            this.pnlTop.Controls.Add(this.btnCancelar);
            this.pnlTop.Controls.Add(this.btnConfirmar);
            this.pnlTop.Controls.Add(this.lblDataPagamento);
            this.pnlTop.Controls.Add(this.cbbDataPagamento);
            this.pnlTop.Controls.Add(this.lblValor);
            this.pnlTop.Controls.Add(this.txtValorRecebido);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.HorizontalScrollbarBarColor = true;
            this.pnlTop.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlTop.HorizontalScrollbarSize = 10;
            this.pnlTop.Location = new System.Drawing.Point(20, 60);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(671, 117);
            this.pnlTop.TabIndex = 0;
            this.pnlTop.VerticalScrollbarBarColor = true;
            this.pnlTop.VerticalScrollbarHighlightOnWheel = false;
            this.pnlTop.VerticalScrollbarSize = 10;
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblFormaPagamento.Location = new System.Drawing.Point(428, 11);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(117, 15);
            this.lblFormaPagamento.TabIndex = 45;
            this.lblFormaPagamento.Text = "Forma de Pagamento";
            // 
            // cbbFormaPagamento
            // 
            this.cbbFormaPagamento.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cbbFormaPagamento.FormattingEnabled = true;
            this.cbbFormaPagamento.ItemHeight = 19;
            this.cbbFormaPagamento.Items.AddRange(new object[] {
            "Dinheiro",
            "Cheque",
            "Cartão Crédito",
            "Cartão Débito",
            "Boleto",
            "Outros"});
            this.cbbFormaPagamento.Location = new System.Drawing.Point(428, 29);
            this.cbbFormaPagamento.Name = "cbbFormaPagamento";
            this.cbbFormaPagamento.Size = new System.Drawing.Size(133, 25);
            this.cbbFormaPagamento.TabIndex = 44;
            this.cbbFormaPagamento.UseSelectable = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(592, 58);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(67, 35);
            this.btnCancelar.TabIndex = 43;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseSelectable = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnGradeCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(520, 58);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(66, 35);
            this.btnConfirmar.TabIndex = 42;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseSelectable = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnGradeConfirmar_Click);
            // 
            // lblDataPagamento
            // 
            this.lblDataPagamento.AutoSize = true;
            this.lblDataPagamento.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblDataPagamento.Location = new System.Drawing.Point(567, 11);
            this.lblDataPagamento.Name = "lblDataPagamento";
            this.lblDataPagamento.Size = new System.Drawing.Size(92, 15);
            this.lblDataPagamento.TabIndex = 39;
            this.lblDataPagamento.Text = "Data Pagamento";
            // 
            // cbbDataPagamento
            // 
            this.cbbDataPagamento.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDataPagamento.Checked = false;
            this.cbbDataPagamento.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.cbbDataPagamento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cbbDataPagamento.Location = new System.Drawing.Point(567, 29);
            this.cbbDataPagamento.MinimumSize = new System.Drawing.Size(0, 25);
            this.cbbDataPagamento.Name = "cbbDataPagamento";
            this.cbbDataPagamento.Size = new System.Drawing.Size(92, 25);
            this.cbbDataPagamento.TabIndex = 38;
            this.cbbDataPagamento.UseCustomBackColor = true;
            this.cbbDataPagamento.UseCustomForeColor = true;
            this.cbbDataPagamento.UseStyleColors = true;
            this.cbbDataPagamento.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblValor.Location = new System.Drawing.Point(326, 11);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(82, 15);
            this.lblValor.TabIndex = 29;
            this.lblValor.Text = "Valor Recebido";
            // 
            // txtValorRecebido
            // 
            // 
            // 
            // 
            this.txtValorRecebido.CustomButton.Image = null;
            this.txtValorRecebido.CustomButton.Location = new System.Drawing.Point(74, 1);
            this.txtValorRecebido.CustomButton.Name = "";
            this.txtValorRecebido.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtValorRecebido.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtValorRecebido.CustomButton.TabIndex = 1;
            this.txtValorRecebido.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtValorRecebido.CustomButton.UseSelectable = true;
            this.txtValorRecebido.CustomButton.Visible = false;
            this.txtValorRecebido.Lines = new string[0];
            this.txtValorRecebido.Location = new System.Drawing.Point(326, 29);
            this.txtValorRecebido.MaxLength = 32767;
            this.txtValorRecebido.Name = "txtValorRecebido";
            this.txtValorRecebido.PasswordChar = '\0';
            this.txtValorRecebido.PromptText = "Valor Recebido";
            this.txtValorRecebido.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtValorRecebido.SelectedText = "";
            this.txtValorRecebido.SelectionLength = 0;
            this.txtValorRecebido.SelectionStart = 0;
            this.txtValorRecebido.ShortcutsEnabled = true;
            this.txtValorRecebido.Size = new System.Drawing.Size(96, 23);
            this.txtValorRecebido.TabIndex = 28;
            this.txtValorRecebido.UseSelectable = true;
            this.txtValorRecebido.WaterMark = "Valor Recebido";
            this.txtValorRecebido.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtValorRecebido.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtValorRecebido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlOnlyNumbers);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.dlbTotalAReceber);
            this.pnlBottom.Controls.Add(this.dlbTotalRecebido);
            this.pnlBottom.Controls.Add(this.lblTotalRecebido);
            this.pnlBottom.Controls.Add(this.lblTotalAReceber);
            this.pnlBottom.Controls.Add(this.btnFechar);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.HorizontalScrollbarBarColor = true;
            this.pnlBottom.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlBottom.HorizontalScrollbarSize = 10;
            this.pnlBottom.Location = new System.Drawing.Point(20, 414);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(671, 37);
            this.pnlBottom.TabIndex = 1;
            this.pnlBottom.VerticalScrollbarBarColor = true;
            this.pnlBottom.VerticalScrollbarHighlightOnWheel = false;
            this.pnlBottom.VerticalScrollbarSize = 10;
            // 
            // btnFechar
            // 
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFechar.Location = new System.Drawing.Point(596, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 37);
            this.btnFechar.TabIndex = 13;
            this.btnFechar.Text = "&Fechar";
            this.btnFechar.UseSelectable = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdRecebimento);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.HorizontalScrollbarBarColor = true;
            this.pnlMain.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlMain.HorizontalScrollbarSize = 10;
            this.pnlMain.Location = new System.Drawing.Point(20, 177);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(671, 237);
            this.pnlMain.TabIndex = 2;
            this.pnlMain.VerticalScrollbarBarColor = true;
            this.pnlMain.VerticalScrollbarHighlightOnWheel = false;
            this.pnlMain.VerticalScrollbarSize = 10;
            // 
            // grdRecebimento
            // 
            this.grdRecebimento.AllowUserToAddRows = false;
            this.grdRecebimento.AllowUserToDeleteRows = false;
            this.grdRecebimento.AllowUserToOrderColumns = true;
            this.grdRecebimento.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdRecebimento.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdRecebimento.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdRecebimento.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdRecebimento.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdRecebimento.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdRecebimento.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdRecebimento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdRecebimento.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdRecebimento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRecebimento.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdRecebimento.EnableHeadersVisualStyles = false;
            this.grdRecebimento.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdRecebimento.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdRecebimento.Location = new System.Drawing.Point(0, 0);
            this.grdRecebimento.Name = "grdRecebimento";
            this.grdRecebimento.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdRecebimento.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdRecebimento.RowHeadersVisible = false;
            this.grdRecebimento.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdRecebimento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdRecebimento.ShowEditingIcon = false;
            this.grdRecebimento.Size = new System.Drawing.Size(671, 237);
            this.grdRecebimento.TabIndex = 4;
            this.grdRecebimento.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdReceberBaixa_CellDoubleClick);
            this.grdRecebimento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdReceberBaixa_KeyUp);
            // 
            // txtObservacao
            // 
            // 
            // 
            // 
            this.txtObservacao.CustomButton.Image = null;
            this.txtObservacao.CustomButton.Location = new System.Drawing.Point(469, 1);
            this.txtObservacao.CustomButton.Name = "";
            this.txtObservacao.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.txtObservacao.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtObservacao.CustomButton.TabIndex = 1;
            this.txtObservacao.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtObservacao.CustomButton.UseSelectable = true;
            this.txtObservacao.CustomButton.Visible = false;
            this.txtObservacao.Lines = new string[0];
            this.txtObservacao.Location = new System.Drawing.Point(11, 58);
            this.txtObservacao.MaxLength = 32767;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.PasswordChar = '\0';
            this.txtObservacao.PromptText = "Observacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtObservacao.SelectedText = "";
            this.txtObservacao.SelectionLength = 0;
            this.txtObservacao.SelectionStart = 0;
            this.txtObservacao.ShortcutsEnabled = true;
            this.txtObservacao.Size = new System.Drawing.Size(503, 35);
            this.txtObservacao.TabIndex = 46;
            this.txtObservacao.UseSelectable = true;
            this.txtObservacao.WaterMark = "Observacao";
            this.txtObservacao.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtObservacao.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblStatus
            // 
            this.lblStatus.AllowDrop = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(11, 94);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(648, 24);
            this.lblStatus.TabIndex = 47;
            this.lblStatus.Text = " <<Status>>";
            this.lblStatus.UseCustomBackColor = true;
            this.lblStatus.UseCustomForeColor = true;
            this.lblStatus.UseStyleColors = true;
            this.lblStatus.WrapToLine = true;
            // 
            // dlbTotalAReceber
            // 
            this.dlbTotalAReceber.AutoSize = true;
            this.dlbTotalAReceber.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.dlbTotalAReceber.Location = new System.Drawing.Point(290, 8);
            this.dlbTotalAReceber.Name = "dlbTotalAReceber";
            this.dlbTotalAReceber.Size = new System.Drawing.Size(37, 19);
            this.dlbTotalAReceber.TabIndex = 34;
            this.dlbTotalAReceber.Text = "0,00";
            // 
            // dlbTotalRecebido
            // 
            this.dlbTotalRecebido.AutoSize = true;
            this.dlbTotalRecebido.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.dlbTotalRecebido.Location = new System.Drawing.Point(107, 8);
            this.dlbTotalRecebido.Name = "dlbTotalRecebido";
            this.dlbTotalRecebido.Size = new System.Drawing.Size(37, 19);
            this.dlbTotalRecebido.TabIndex = 33;
            this.dlbTotalRecebido.Text = "0,00";
            // 
            // lblTotalRecebido
            // 
            this.lblTotalRecebido.AutoSize = true;
            this.lblTotalRecebido.Location = new System.Drawing.Point(9, 8);
            this.lblTotalRecebido.Name = "lblTotalRecebido";
            this.lblTotalRecebido.Size = new System.Drawing.Size(98, 19);
            this.lblTotalRecebido.TabIndex = 32;
            this.lblTotalRecebido.Text = "Total Recebido:";
            // 
            // lblTotalAReceber
            // 
            this.lblTotalAReceber.AutoSize = true;
            this.lblTotalAReceber.Location = new System.Drawing.Point(189, 8);
            this.lblTotalAReceber.Name = "lblTotalAReceber";
            this.lblTotalAReceber.Size = new System.Drawing.Size(103, 19);
            this.lblTotalAReceber.TabIndex = 31;
            this.lblTotalAReceber.Text = "Total a Receber:";
            // 
            // txtReferencia
            // 
            // 
            // 
            // 
            this.txtReferencia.CustomButton.Image = null;
            this.txtReferencia.CustomButton.Location = new System.Drawing.Point(287, 1);
            this.txtReferencia.CustomButton.Name = "";
            this.txtReferencia.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtReferencia.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtReferencia.CustomButton.TabIndex = 1;
            this.txtReferencia.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtReferencia.CustomButton.UseSelectable = true;
            this.txtReferencia.CustomButton.Visible = false;
            this.txtReferencia.Lines = new string[0];
            this.txtReferencia.Location = new System.Drawing.Point(11, 29);
            this.txtReferencia.MaxLength = 32767;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.PasswordChar = '\0';
            this.txtReferencia.PromptText = "Referência";
            this.txtReferencia.ReadOnly = true;
            this.txtReferencia.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtReferencia.SelectedText = "";
            this.txtReferencia.SelectionLength = 0;
            this.txtReferencia.SelectionStart = 0;
            this.txtReferencia.ShortcutsEnabled = true;
            this.txtReferencia.Size = new System.Drawing.Size(309, 23);
            this.txtReferencia.TabIndex = 48;
            this.txtReferencia.UseSelectable = true;
            this.txtReferencia.WaterMark = "Referência";
            this.txtReferencia.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtReferencia.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel1.Location = new System.Drawing.Point(9, 11);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(58, 15);
            this.metroLabel1.TabIndex = 49;
            this.metroLabel1.Text = "Referencia";
            // 
            // FormRecebimento
            // 
            this.AcceptButton = this.btnFechar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(711, 471);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRecebimento";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Baixa de Títulos";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormReceberBaixa_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRecebimento)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlTop;
        private MetroFramework.Controls.MetroPanel pnlBottom;
        private MetroFramework.Controls.MetroPanel pnlMain;
        private MetroFramework.Controls.MetroLabel lblValor;
        public MetroFramework.Controls.MetroTextBox txtValorRecebido;
        private MetroFramework.Controls.MetroLabel lblDataPagamento;
        public MetroFramework.Controls.MetroDateTime cbbDataPagamento;
        private MetroFramework.Controls.MetroButton btnFechar;
        public MetroFramework.Controls.MetroButton btnCancelar;
        public MetroFramework.Controls.MetroButton btnConfirmar;
        public MetroFramework.Controls.MetroGrid grdRecebimento;
        private MetroFramework.Controls.MetroLabel lblFormaPagamento;
        private MetroFramework.Controls.MetroComboBox cbbFormaPagamento;
        public MetroFramework.Controls.MetroTextBox txtObservacao;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        public MetroFramework.Controls.MetroTextBox txtReferencia;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private MetroFramework.Controls.MetroLabel dlbTotalAReceber;
        private MetroFramework.Controls.MetroLabel dlbTotalRecebido;
        private MetroFramework.Controls.MetroLabel lblTotalRecebido;
        private MetroFramework.Controls.MetroLabel lblTotalAReceber;
    }
}