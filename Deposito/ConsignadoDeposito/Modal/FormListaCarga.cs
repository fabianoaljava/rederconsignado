using Equin.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignadoDeposito.Modal
{
    public partial class FormListaCarga : MetroFramework.Forms.MetroForm    {

        public FormDeposito localDepositoForm = null;
        public string cOrigem;



        public int cPracaId, cRepresentanteId, cMes, cAno;
        

        public FormListaCarga(FormDeposito formDeposito, string pOrigem)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;
            cOrigem = pOrigem;
        }

        public void CarregarListaCarga()
        {
            cbbCargaPraca.DataSource = ModelLibrary.MetodosDeposito.ObterListaPracas();
            cbbCargaPraca.DisplayMember = "Descricao";
            cbbCargaPraca.ValueMember = "Id";
            cbbCargaPraca.Invalidate();
            cbbCargaPraca.SelectedIndex = -1;
        }

        public void CarregarListaRepresentante()
        {
            cbbCargaRepresentante.DataSource = ModelLibrary.MetodosDeposito.ObterListaRepresentantes();
            cbbCargaRepresentante.DisplayMember = "Nome";
            cbbCargaRepresentante.ValueMember = "Id";
            cbbCargaRepresentante.Invalidate();
            cbbCargaRepresentante.SelectedIndex = -1;
        }


        public void LimparPesquisa()
        {
            txtCargaCodPraca.Text = "";
            cbbCargaPraca.SelectedIndex = -1;
            txtCargaCodRepresentante.Text = "";
            cbbCargaRepresentante.SelectedIndex = -1;
            cbbAnoMesInicial.ResetText();
            cbbAnoMesFinal.ResetText();

            LimparGrade();


            this.cRepresentanteId = 0;
            this.cPracaId = 0;
            this.cAno = 0;
            this.cMes = 0;

        }


        public void LimparGrade()
        {

            grdCarga.DataSource = null;
            grdCarga.Refresh();

        }


        private void FormListaCarga_Load(object sender, EventArgs e)
        {
            if (cOrigem == "Carga")
            {
                this.Name = "Selecionar Carga";
                this.Refresh();
            } else if (cOrigem == "Retorno")
            {
                this.Name = "Selecionar Retorno";                
                this.Refresh();
            }

            CarregarListaCarga();
            CarregarListaRepresentante();


            cbbAnoMesInicial.Value = DateTime.Now.AddDays(-180);
            cbbAnoMesFinal.Value = DateTime.Now;


        }


        private void CargaKeyEnter(object sender, KeyEventArgs e)
        {
            string objname = ((MetroFramework.Controls.MetroTextBox)sender).Name;


            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (objname == "txtCargaCodRepresentante")
                {
                    if (txtCargaCodRepresentante.Text != "")
                    {
                        txtCargaCodRepresentante_ButtonClick(sender, e);
                        cbbAnoMesInicial.Focus();
                    }
                }
                else if (objname == "txtCargaCodPraca")
                {
                    if (txtCargaCodPraca.Text != "")
                    {
                        txtCargaCodPraca_ButtonClick(sender, e);
                        txtCargaCodRepresentante.Focus();
                    }

                }
                else
                {
                    SendKeys.Send("{TAB}");
                }

                e.Handled = true;//set to false if you need that textbox gets enter key
            }
        }

        private void txtCargaCodPraca_ButtonClick(object sender, EventArgs e)
        {
            LimparGrade();

            Console.WriteLine("CargaButtonClick");

            int i = -1;
            try
            {
                i = Convert.ToInt32(txtCargaCodPraca.Text);
            }
            catch
            {
                MessageBox.Show("Código Inválido");
            }


            var praca = ModelLibrary.MetodosDeposito.ObterPraca(i);

            if (praca != null)
            {
                cbbCargaPraca.SelectedIndex = cbbCargaPraca.FindString(praca.Descricao);
            }
            else
            {
                cbbCargaPraca.SelectedIndex = -1;
            }



        }


        private void txtCargaCodRepresentante_ButtonClick(object sender, EventArgs e)
        {

            LimparGrade();

            Console.WriteLine("RepresentanteButtonClick");

            int i = -1;
            try
            {
                i = Convert.ToInt32(txtCargaCodRepresentante.Text);
            }
            catch
            {
                MessageBox.Show("Código Inválido");
            }


            var representante = ModelLibrary.MetodosDeposito.ObterRepresentante(i);

            if (representante != null)
            {
                cbbCargaRepresentante.SelectedIndex = cbbCargaRepresentante.FindString(representante.Nome);

            }
            else
            {
                cbbCargaRepresentante.SelectedIndex = -1;
            }


        }

        private void txtCargaCodPraca_Validating(object sender, CancelEventArgs e)
        {
            if (txtCargaCodPraca.Text != "") txtCargaCodPraca_ButtonClick(sender, e);
        }

        private void txtCargaCodRepresentante_Validating(object sender, CancelEventArgs e)
        {
            if (txtCargaCodRepresentante.Text != "") txtCargaCodRepresentante_ButtonClick(sender, e);
        }

        private void cbbCargaPraca_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                ModelLibrary.Praca praca = (ModelLibrary.Praca)cbbCargaPraca.SelectedItem;
                txtCargaCodPraca.Text = praca.Id.ToString();
                //txtCargaCodRepresentante.Focus();
            }
            catch
            {
                txtCargaCodPraca.Text = "";
            }

        }

        private void cbbCargaRepresentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ModelLibrary.Representante representante = (ModelLibrary.Representante)cbbCargaRepresentante.SelectedItem;
                txtCargaCodRepresentante.Text = representante.Id.ToString();
                //cbbCargaMesAno.Focus();
            }
            catch
            {
                txtCargaCodRepresentante.Text = "";
            }
        }


        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");

            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            LimparGrade();
            Cursor.Current = Cursors.WaitCursor;


            ModelLibrary.Representante representante = (ModelLibrary.Representante)cbbCargaRepresentante.SelectedItem;
            var representanteId = (representante != null) ? representante.Id :0;
            ModelLibrary.Praca praca = (ModelLibrary.Praca)cbbCargaPraca.SelectedItem;
            var pracaId = (praca != null) ? praca.Id : 0;


            string vAnoMesInicial = cbbAnoMesInicial.Value.Year.ToString() + cbbAnoMesInicial.Value.Month.ToString().PadLeft(2, '0');
            string vAnoMesFinal = cbbAnoMesFinal.Value.Year.ToString() + cbbAnoMesFinal.Value.Month.ToString().PadLeft(2, '0');



            List<ModelLibrary.ListaPesquisaCarga> carga = ModelLibrary.MetodosDeposito.PesquisarCarga(pracaId, representanteId, vAnoMesInicial, vAnoMesFinal);

            BindingListView<ModelLibrary.ListaPesquisaCarga> view = new BindingListView<ModelLibrary.ListaPesquisaCarga>(carga);

            grdCarga.DataSource = view;



            grdCarga.Columns[0].Visible = false;
            grdCarga.Columns[1].Width = 30;
            grdCarga.Columns[2].Width = 200;
            grdCarga.Columns[3].Width = 30;
            grdCarga.Columns[4].Width = 200;
            grdCarga.Columns[5].Width = 60;
            grdCarga.Columns[6].Width = 30;
            grdCarga.Columns[12].Width = 30;
            grdCarga.Columns[13].Visible = false;


            Cursor.Current = Cursors.Default;


        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparPesquisa();
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparPesquisa();
        }

        private void grdCarga_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cRepresentanteId = Convert.ToInt32(grdCarga.CurrentRow.Cells["RepresentanteId"].Value);
            this.cPracaId = Convert.ToInt32(grdCarga.CurrentRow.Cells["PracaId"].Value);
            this.cAno = Convert.ToInt32(grdCarga.CurrentRow.Cells["Ano"].Value);
            this.cMes = Convert.ToInt32(grdCarga.CurrentRow.Cells["Mes"].Value);

            btnConfirmar.Enabled = true;
        }

        private void grdCarga_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (cRepresentanteId > 0 && cPracaId > 0 && cAno > 0 && cMes > 0)
            {
                btnConfirmar_Click(sender, e);
            } 
            
        }
    }
}
