namespace ConsignadoDeposito.Modal
{
    partial class FormNovoPedido
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
            this.lblVendedor = new MetroFramework.Controls.MetroLabel();
            this.cbbVendedor = new MetroFramework.Controls.MetroComboBox();
            this.btnIncluir = new MetroFramework.Controls.MetroButton();
            this.btnCancelar = new MetroFramework.Controls.MetroButton();
            this.txtResultado = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Location = new System.Drawing.Point(23, 73);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(70, 19);
            this.lblVendedor.TabIndex = 0;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // cbbVendedor
            // 
            this.cbbVendedor.FormattingEnabled = true;
            this.cbbVendedor.ItemHeight = 23;
            this.cbbVendedor.Location = new System.Drawing.Point(110, 67);
            this.cbbVendedor.Name = "cbbVendedor";
            this.cbbVendedor.Size = new System.Drawing.Size(345, 29);
            this.cbbVendedor.TabIndex = 1;
            this.cbbVendedor.UseSelectable = true;
            // 
            // btnIncluir
            // 
            this.btnIncluir.Enabled = false;
            this.btnIncluir.Location = new System.Drawing.Point(380, 174);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(75, 23);
            this.btnIncluir.TabIndex = 3;
            this.btnIncluir.Text = "Incluir";
            this.btnIncluir.UseSelectable = true;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(23, 174);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseSelectable = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtResultado
            // 
            // 
            // 
            // 
            this.txtResultado.CustomButton.Image = null;
            this.txtResultado.CustomButton.Location = new System.Drawing.Point(374, 1);
            this.txtResultado.CustomButton.Name = "";
            this.txtResultado.CustomButton.Size = new System.Drawing.Size(57, 57);
            this.txtResultado.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtResultado.CustomButton.TabIndex = 1;
            this.txtResultado.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtResultado.CustomButton.UseSelectable = true;
            this.txtResultado.CustomButton.Visible = false;
            this.txtResultado.Lines = new string[0];
            this.txtResultado.Location = new System.Drawing.Point(23, 105);
            this.txtResultado.MaxLength = 32767;
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.PasswordChar = '\0';
            this.txtResultado.PromptText = "Selecione o vendedor...";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtResultado.SelectedText = "";
            this.txtResultado.SelectionLength = 0;
            this.txtResultado.SelectionStart = 0;
            this.txtResultado.ShortcutsEnabled = true;
            this.txtResultado.Size = new System.Drawing.Size(432, 59);
            this.txtResultado.TabIndex = 5;
            this.txtResultado.UseSelectable = true;
            this.txtResultado.UseStyleColors = true;
            this.txtResultado.WaterMark = "Selecione o vendedor...";
            this.txtResultado.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtResultado.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // FormNovoPedido
            // 
            this.AcceptButton = this.btnIncluir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(487, 220);
            this.ControlBox = false;
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnIncluir);
            this.Controls.Add(this.cbbVendedor);
            this.Controls.Add(this.lblVendedor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNovoPedido";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Novo Pedido";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormNovoPedido_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblVendedor;
        private MetroFramework.Controls.MetroComboBox cbbVendedor;
        private MetroFramework.Controls.MetroButton btnIncluir;
        private MetroFramework.Controls.MetroButton btnCancelar;
        private MetroFramework.Controls.MetroTextBox txtResultado;
    }
}