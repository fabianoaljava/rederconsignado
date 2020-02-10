﻿namespace ConsignadoRepresentante.Modal
{
    partial class FormProdutosGrade
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
            this.pnlProdutoGradeTop = new MetroFramework.Controls.MetroPanel();
            this.lblInfo = new MetroFramework.Controls.MetroLabel();
            this.pnlProdutoGradeBottom = new MetroFramework.Controls.MetroPanel();
            this.btnCancelar = new MetroFramework.Controls.MetroButton();
            this.btnConfirmar = new MetroFramework.Controls.MetroButton();
            this.pnlProdutoGradeMain = new MetroFramework.Controls.MetroPanel();
            this.grdProdutoGrade = new MetroFramework.Controls.MetroGrid();
            this.pnlProdutoGradeTop.SuspendLayout();
            this.pnlProdutoGradeBottom.SuspendLayout();
            this.pnlProdutoGradeMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProdutoGrade)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlProdutoGradeTop
            // 
            this.pnlProdutoGradeTop.Controls.Add(this.lblInfo);
            this.pnlProdutoGradeTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProdutoGradeTop.HorizontalScrollbarBarColor = true;
            this.pnlProdutoGradeTop.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGradeTop.HorizontalScrollbarSize = 10;
            this.pnlProdutoGradeTop.Location = new System.Drawing.Point(20, 60);
            this.pnlProdutoGradeTop.Name = "pnlProdutoGradeTop";
            this.pnlProdutoGradeTop.Size = new System.Drawing.Size(660, 24);
            this.pnlProdutoGradeTop.TabIndex = 0;
            this.pnlProdutoGradeTop.VerticalScrollbarBarColor = true;
            this.pnlProdutoGradeTop.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGradeTop.VerticalScrollbarSize = 10;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(189, 19);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Selecione o produto da grade:";
            // 
            // pnlProdutoGradeBottom
            // 
            this.pnlProdutoGradeBottom.Controls.Add(this.btnCancelar);
            this.pnlProdutoGradeBottom.Controls.Add(this.btnConfirmar);
            this.pnlProdutoGradeBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProdutoGradeBottom.HorizontalScrollbarBarColor = true;
            this.pnlProdutoGradeBottom.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGradeBottom.HorizontalScrollbarSize = 10;
            this.pnlProdutoGradeBottom.Location = new System.Drawing.Point(20, 301);
            this.pnlProdutoGradeBottom.Name = "pnlProdutoGradeBottom";
            this.pnlProdutoGradeBottom.Size = new System.Drawing.Size(660, 39);
            this.pnlProdutoGradeBottom.TabIndex = 1;
            this.pnlProdutoGradeBottom.VerticalScrollbarBarColor = true;
            this.pnlProdutoGradeBottom.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGradeBottom.VerticalScrollbarSize = 10;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancelar.Location = new System.Drawing.Point(0, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 39);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Ca&ncelar";
            this.btnCancelar.UseSelectable = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirmar.Enabled = false;
            this.btnConfirmar.Location = new System.Drawing.Point(585, 0);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 39);
            this.btnConfirmar.TabIndex = 3;
            this.btnConfirmar.Text = "&Confirmar";
            this.btnConfirmar.UseSelectable = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // pnlProdutoGradeMain
            // 
            this.pnlProdutoGradeMain.Controls.Add(this.grdProdutoGrade);
            this.pnlProdutoGradeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProdutoGradeMain.HorizontalScrollbarBarColor = true;
            this.pnlProdutoGradeMain.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGradeMain.HorizontalScrollbarSize = 10;
            this.pnlProdutoGradeMain.Location = new System.Drawing.Point(20, 84);
            this.pnlProdutoGradeMain.Name = "pnlProdutoGradeMain";
            this.pnlProdutoGradeMain.Size = new System.Drawing.Size(660, 217);
            this.pnlProdutoGradeMain.TabIndex = 2;
            this.pnlProdutoGradeMain.VerticalScrollbarBarColor = true;
            this.pnlProdutoGradeMain.VerticalScrollbarHighlightOnWheel = false;
            this.pnlProdutoGradeMain.VerticalScrollbarSize = 10;
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
            this.grdProdutoGrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdProdutoGrade.ShowEditingIcon = false;
            this.grdProdutoGrade.Size = new System.Drawing.Size(660, 217);
            this.grdProdutoGrade.StandardTab = true;
            this.grdProdutoGrade.TabIndex = 5;
            this.grdProdutoGrade.UseCustomBackColor = true;
            this.grdProdutoGrade.UseCustomForeColor = true;
            this.grdProdutoGrade.UseStyleColors = true;
            this.grdProdutoGrade.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdProdutoGrade_CellClick);
            this.grdProdutoGrade.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdProdutoGrade_CellContentDoubleClick);
            // 
            // FormProdutosGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 360);
            this.Controls.Add(this.pnlProdutoGradeMain);
            this.Controls.Add(this.pnlProdutoGradeBottom);
            this.Controls.Add(this.pnlProdutoGradeTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProdutosGrade";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Selecionar Produto da Grade";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormProdutosGrade_Load);
            this.pnlProdutoGradeTop.ResumeLayout(false);
            this.pnlProdutoGradeTop.PerformLayout();
            this.pnlProdutoGradeBottom.ResumeLayout(false);
            this.pnlProdutoGradeMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProdutoGrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel pnlProdutoGradeTop;
        private MetroFramework.Controls.MetroLabel lblInfo;
        private MetroFramework.Controls.MetroPanel pnlProdutoGradeBottom;
        private MetroFramework.Controls.MetroButton btnCancelar;
        private MetroFramework.Controls.MetroButton btnConfirmar;
        private MetroFramework.Controls.MetroPanel pnlProdutoGradeMain;
        public MetroFramework.Controls.MetroGrid grdProdutoGrade;
    }
}