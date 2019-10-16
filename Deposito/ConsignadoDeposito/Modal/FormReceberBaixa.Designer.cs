namespace ConsignadoDeposito.Modal
{
    partial class FormReceberBaixa
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
            this.pnlBottom = new MetroFramework.Controls.MetroPanel();
            this.pnlMain = new MetroFramework.Controls.MetroPanel();
            this.lblValor = new MetroFramework.Controls.MetroLabel();
            this.txtValor = new MetroFramework.Controls.MetroTextBox();
            this.lblDataPagamento = new MetroFramework.Controls.MetroLabel();
            this.cbbDataPagamento = new MetroFramework.Controls.MetroDateTime();
            this.btnCancelar = new MetroFramework.Controls.MetroButton();
            this.btnConfirmar = new MetroFramework.Controls.MetroButton();
            this.btnGradeCancelar = new MetroFramework.Controls.MetroButton();
            this.btnGradeConfirmar = new MetroFramework.Controls.MetroButton();
            this.grdReceberBaixa = new MetroFramework.Controls.MetroGrid();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReceberBaixa)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnGradeCancelar);
            this.pnlTop.Controls.Add(this.btnGradeConfirmar);
            this.pnlTop.Controls.Add(this.lblDataPagamento);
            this.pnlTop.Controls.Add(this.cbbDataPagamento);
            this.pnlTop.Controls.Add(this.lblValor);
            this.pnlTop.Controls.Add(this.txtValor);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.HorizontalScrollbarBarColor = true;
            this.pnlTop.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlTop.HorizontalScrollbarSize = 10;
            this.pnlTop.Location = new System.Drawing.Point(20, 60);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(381, 58);
            this.pnlTop.TabIndex = 0;
            this.pnlTop.VerticalScrollbarBarColor = true;
            this.pnlTop.VerticalScrollbarHighlightOnWheel = false;
            this.pnlTop.VerticalScrollbarSize = 10;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancelar);
            this.pnlBottom.Controls.Add(this.btnConfirmar);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.HorizontalScrollbarBarColor = true;
            this.pnlBottom.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlBottom.HorizontalScrollbarSize = 10;
            this.pnlBottom.Location = new System.Drawing.Point(20, 273);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(381, 37);
            this.pnlBottom.TabIndex = 1;
            this.pnlBottom.VerticalScrollbarBarColor = true;
            this.pnlBottom.VerticalScrollbarHighlightOnWheel = false;
            this.pnlBottom.VerticalScrollbarSize = 10;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdReceberBaixa);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.HorizontalScrollbarBarColor = true;
            this.pnlMain.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlMain.HorizontalScrollbarSize = 10;
            this.pnlMain.Location = new System.Drawing.Point(20, 118);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(381, 155);
            this.pnlMain.TabIndex = 2;
            this.pnlMain.VerticalScrollbarBarColor = true;
            this.pnlMain.VerticalScrollbarHighlightOnWheel = false;
            this.pnlMain.VerticalScrollbarSize = 10;
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblValor.Location = new System.Drawing.Point(4, 7);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(32, 15);
            this.lblValor.TabIndex = 29;
            this.lblValor.Text = "Valor";
            // 
            // txtValor
            // 
            // 
            // 
            // 
            this.txtValor.CustomButton.Image = null;
            this.txtValor.CustomButton.Location = new System.Drawing.Point(91, 1);
            this.txtValor.CustomButton.Name = "";
            this.txtValor.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtValor.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtValor.CustomButton.TabIndex = 1;
            this.txtValor.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtValor.CustomButton.UseSelectable = true;
            this.txtValor.CustomButton.Visible = false;
            this.txtValor.Lines = new string[0];
            this.txtValor.Location = new System.Drawing.Point(4, 25);
            this.txtValor.MaxLength = 32767;
            this.txtValor.Name = "txtValor";
            this.txtValor.PasswordChar = '\0';
            this.txtValor.PromptText = "Valor";
            this.txtValor.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtValor.SelectedText = "";
            this.txtValor.SelectionLength = 0;
            this.txtValor.SelectionStart = 0;
            this.txtValor.ShortcutsEnabled = true;
            this.txtValor.Size = new System.Drawing.Size(113, 23);
            this.txtValor.TabIndex = 28;
            this.txtValor.UseSelectable = true;
            this.txtValor.WaterMark = "Valor";
            this.txtValor.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtValor.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblDataPagamento
            // 
            this.lblDataPagamento.AutoSize = true;
            this.lblDataPagamento.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblDataPagamento.Location = new System.Drawing.Point(123, 5);
            this.lblDataPagamento.Name = "lblDataPagamento";
            this.lblDataPagamento.Size = new System.Drawing.Size(92, 15);
            this.lblDataPagamento.TabIndex = 39;
            this.lblDataPagamento.Text = "Data Pagamento";
            // 
            // cbbDataPagamento
            // 
            this.cbbDataPagamento.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDataPagamento.Checked = false;
            this.cbbDataPagamento.Enabled = false;
            this.cbbDataPagamento.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.cbbDataPagamento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cbbDataPagamento.Location = new System.Drawing.Point(123, 23);
            this.cbbDataPagamento.MinimumSize = new System.Drawing.Size(0, 25);
            this.cbbDataPagamento.Name = "cbbDataPagamento";
            this.cbbDataPagamento.Size = new System.Drawing.Size(92, 25);
            this.cbbDataPagamento.TabIndex = 38;
            this.cbbDataPagamento.UseCustomBackColor = true;
            this.cbbDataPagamento.UseCustomForeColor = true;
            this.cbbDataPagamento.UseStyleColors = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancelar.Location = new System.Drawing.Point(0, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 37);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Ca&ncelar";
            this.btnCancelar.UseSelectable = true;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(306, 0);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 37);
            this.btnConfirmar.TabIndex = 13;
            this.btnConfirmar.Text = "&Confirmar";
            this.btnConfirmar.UseSelectable = true;
            // 
            // btnGradeCancelar
            // 
            this.btnGradeCancelar.Enabled = false;
            this.btnGradeCancelar.Location = new System.Drawing.Point(295, 23);
            this.btnGradeCancelar.Name = "btnGradeCancelar";
            this.btnGradeCancelar.Size = new System.Drawing.Size(67, 23);
            this.btnGradeCancelar.TabIndex = 43;
            this.btnGradeCancelar.Text = "Cancelar";
            this.btnGradeCancelar.UseSelectable = true;
            // 
            // btnGradeConfirmar
            // 
            this.btnGradeConfirmar.Enabled = false;
            this.btnGradeConfirmar.Location = new System.Drawing.Point(223, 23);
            this.btnGradeConfirmar.Name = "btnGradeConfirmar";
            this.btnGradeConfirmar.Size = new System.Drawing.Size(66, 23);
            this.btnGradeConfirmar.TabIndex = 42;
            this.btnGradeConfirmar.Text = "Confirmar";
            this.btnGradeConfirmar.UseSelectable = true;
            // 
            // grdReceberBaixa
            // 
            this.grdReceberBaixa.AllowUserToAddRows = false;
            this.grdReceberBaixa.AllowUserToDeleteRows = false;
            this.grdReceberBaixa.AllowUserToOrderColumns = true;
            this.grdReceberBaixa.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdReceberBaixa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdReceberBaixa.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdReceberBaixa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdReceberBaixa.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdReceberBaixa.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdReceberBaixa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdReceberBaixa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdReceberBaixa.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdReceberBaixa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReceberBaixa.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdReceberBaixa.EnableHeadersVisualStyles = false;
            this.grdReceberBaixa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdReceberBaixa.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdReceberBaixa.Location = new System.Drawing.Point(0, 0);
            this.grdReceberBaixa.Name = "grdReceberBaixa";
            this.grdReceberBaixa.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdReceberBaixa.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdReceberBaixa.RowHeadersVisible = false;
            this.grdReceberBaixa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdReceberBaixa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdReceberBaixa.ShowEditingIcon = false;
            this.grdReceberBaixa.Size = new System.Drawing.Size(381, 155);
            this.grdReceberBaixa.TabIndex = 4;
            // 
            // FormReceberBaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 330);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReceberBaixa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Baixa de Títulos";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormReceberBaixa_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReceberBaixa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlTop;
        private MetroFramework.Controls.MetroPanel pnlBottom;
        private MetroFramework.Controls.MetroPanel pnlMain;
        private MetroFramework.Controls.MetroLabel lblValor;
        public MetroFramework.Controls.MetroTextBox txtValor;
        private MetroFramework.Controls.MetroLabel lblDataPagamento;
        public MetroFramework.Controls.MetroDateTime cbbDataPagamento;
        private MetroFramework.Controls.MetroButton btnCancelar;
        private MetroFramework.Controls.MetroButton btnConfirmar;
        public MetroFramework.Controls.MetroButton btnGradeCancelar;
        public MetroFramework.Controls.MetroButton btnGradeConfirmar;
        public MetroFramework.Controls.MetroGrid grdReceberBaixa;
    }
}