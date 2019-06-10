using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignadoDeposito;


namespace ConsignadoDeposito
{
    public partial class FormDeposito : MetroFramework.Forms.MetroForm
    {



        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        



        public Home cHome;
        public Carga cCarga;
        public Retorno cRetorno;
        public Acerto cAcerto;
        public Financeiro cFinanceiro;
        public Relatorio cRelatorio;        
        public Ajuda cAjuda;



        public System.Windows.Forms.Form loginWindow = null;



        /// <summary>
        /// Inicialização do Formulário Depósito
        /// </summary>
        /// <param name="loginForm">Form de Autenticação - Login</param>
        /// <param name="pUsuario">Login do Usuário - Proveniente do Form Login</param>
        /// <param name="pNome">Nome do Usuário - Proveniente do Form Login</param>

        public FormDeposito(System.Windows.Forms.Form loginForm, string pUsuario, string pNome)
        {
            InitializeComponent();

            this.loginWindow = loginForm;
            this.loginWindow.Hide();

            lblUsuario.Text = pUsuario;
            lblNome.Text = pNome;


            cHome = new Home(this);
            cCarga = new Carga(this);
            cRetorno = new Retorno(this);
            cAcerto = new Acerto(this);
            cFinanceiro = new Financeiro(this);
            cRelatorio = new Relatorio(this);
            cAjuda = new Ajuda(this);


        }


        ////////////////////////////////////////
        /// Funções do Formulário Principal
        ////////////////////////////////////////
        


        public void CarregarDeposito()
        {
            cCarga.CarregarListaCarga();
            cCarga.CarregarListaRepresentante();
            tabHome.Focus();
        }



        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////////   FUNÇÕES DO FORM  //////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////

        private void FormDeposito_Load(object sender, EventArgs e)
        {

            CarregarDeposito();

        }





        private void FormDeposito_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void lblSair_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja Realmente Sair?", "Reder Consignado", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Application.Exit();
        }






        ////////////////////////////////////////
        /// Aba Home
        ////////////////////////////////////////



        ////////////////////////////////////////
        /// Aba Carga
        ////////////////////////////////////////


        private void txtCargaCodPraca_ButtonClick(object sender, EventArgs e)
        {
            cCarga.ResetarVariaveis();
            cCarga.LimparGradeCargaProduto();

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

        private void txtCargaCodPraca_Leave(object sender, EventArgs e)
        {
            if (txtCargaCodPraca.Text != "") txtCargaCodPraca_ButtonClick(sender, e);
        }

        


        private void txtCargaCodRepresentante_ButtonClick(object sender, EventArgs e)
        {

            cCarga.ResetarVariaveis();
            cCarga.LimparGradeCargaProduto();

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

        private void txtCargaCodRepresentante_Leave(object sender, EventArgs e)
        {
            if (txtCargaCodRepresentante.Text != "") txtCargaCodRepresentante_ButtonClick(sender, e);
        }



        private void CargaKeyEnter(object sender, KeyEventArgs e)
        {
            string objname = ((MetroFramework.Controls.MetroTextBox)sender).Name;


            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (objname == "txtCargaCodRepresentante")
                {
                    txtCargaCodRepresentante_ButtonClick(sender, e);
                    cbbCargaMesAno.Focus();
                }
                else if (objname == "txtCargaCodPraca")
                {
                    txtCargaCodPraca_ButtonClick(sender, e);
                    txtCargaCodRepresentante.Focus();
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }

                e.Handled = true;//set to false if you need that textbox gets enter key
            }
        }

        private void cbbCargaMesAno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                e.SuppressKeyPress = true;
                btnCargaOK.Focus();

            }
        }

        private void bntCargaOK_Click(object sender, EventArgs e)
        {

            cCarga.PesquisarCarga();

        }

        private void bntCargaOK_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bntCargaOK_Click(sender, e);
            }
        }

        private void btnCargaPesquisar_Click(object sender, EventArgs e)
        {
            ///Abre Janela de Pesquisa de Carga com opção para Alterar / Excluir - se tiver permissão
        }

        private void btnCargaLimpar_Click(object sender, EventArgs e)
        {
            cCarga.LimparCarga();
        }



        


        






        private void txtCargaCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cCarga.PesquisarCargaProduto(txtCargaCodigoBarras.Text);

            }
        }

        private void txtCargaCodigoBarras_Leave(object sender, EventArgs e)
        {
            if (txtCargaCodigoBarras.Text != "")
            {
                cCarga.PesquisarCargaProduto(txtCargaCodigoBarras.Text);
            }
        }

        private void chkCargaQuantidade_CheckedChanged(object sender, EventArgs e)
        {
            if (cCarga.cCargaProdutoGradeId != 0)
            {
                chkCargaQuantidade.Checked = true;
                txtCargaQuantidade.Enabled = true;
            }
            else
            {
                txtCargaQuantidade.Text = "";
                txtCargaQuantidade.Enabled = chkCargaQuantidade.Checked;
            }

        }

        private void bntCargaConfirmar_Click(object sender, EventArgs e)
        {

            cCarga.ConfirmarCargaProdutoGrade();

        }

        private void btnCargaCancelar_Click(object sender, EventArgs e)
        {
            cCarga.LimparCargaProduto();
        }



        private void grdCargaProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cCarga.ExibirCargaProdutoGrade();
        }

        private void grdCargaProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {

                cCarga.ExcluirCargaProdutoGrade();
            }

        }





        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cCarga.ExibirCargaProdutoGrade();
        }



        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cCarga.ExcluirCargaProdutoGrade();
        }




        ////////////////////////////////////////
        /// Aba Retorno
        ////////////////////////////////////////

        ////////////////////////////////////////
        /// Aba Acerto
        ////////////////////////////////////////

        ////////////////////////////////////////
        /// Aba Financeiro
        ////////////////////////////////////////

        ////////////////////////////////////////
        /// Aba Relatorios
        ////////////////////////////////////////

        ////////////////////////////////////////
        /// Aba Ajuda
        ////////////////////////////////////////


        ////////////////////////////////////////
        /// ORGANIZAR DAQUI PRA BAIXO
        ////////////////////////////////////////


    }
}


