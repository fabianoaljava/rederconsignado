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
    public partial class FormListaCarga : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;
        public string cOrigem;

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
                        cbbCargaMesAno.Focus();
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
            //ResetarVariaveis();
            //LimparGradeCarga();

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

           // ResetarVariaveis();
            //LimparGradeCarga();

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

        }
    }
}
