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
            cRetorno.CarregarListaRetorno();
            cRetorno.CarregarListaRepresentante();
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

            Cursor.Current = Cursors.WaitCursor;
            CarregarDeposito();
            tbcPrincipal.SelectedTab = tabHome;
            Cursor.Current = Cursors.Default;

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
            if (txtCargaCodPraca.Text != "" && txtCargaCodRepresentante.Text != "")
            {
                cCarga.PesquisarCarga();
            }
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
                if (txtCargaCodigoBarras.Text != "")
                {
                    e.SuppressKeyPress = true;
                    cCarga.PesquisarCargaProduto(txtCargaCodigoBarras.Text);
                }
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
        ///



        private void txtRetornoCodPraca_ButtonClick(object sender, EventArgs e)
        {
            cRetorno.ResetarVariaveis();
            cRetorno.LimparGradeRetornoProduto();

            Console.WriteLine("RetornoButtonClick");

            int i = -1;
            try
            {
                i = Convert.ToInt32(txtRetornoCodPraca.Text);
            }
            catch
            {
                MessageBox.Show("Código Inválido");
            }


            var praca = ModelLibrary.MetodosDeposito.ObterPraca(i);

            if (praca != null)
            {
                cbbRetornoPraca.SelectedIndex = cbbRetornoPraca.FindString(praca.Descricao);
            }
            else
            {
                cbbRetornoPraca.SelectedIndex = -1;
            }



        }


        private void cbbRetornoPraca_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                ModelLibrary.Praca praca = (ModelLibrary.Praca)cbbRetornoPraca.SelectedItem;
                txtRetornoCodPraca.Text = praca.Id.ToString();
                //txtRetornoCodRepresentante.Focus();
            }
            catch
            {
                txtRetornoCodPraca.Text = "";
            }

        }

        private void txtRetornoCodPraca_Leave(object sender, EventArgs e)
        {
            if (txtRetornoCodPraca.Text != "") txtRetornoCodPraca_ButtonClick(sender, e);
        }




        private void txtRetornoCodRepresentante_ButtonClick(object sender, EventArgs e)
        {

            cRetorno.ResetarVariaveis();
            cRetorno.LimparGradeRetornoProduto();

            Console.WriteLine("RepresentanteButtonClick");

            int i = -1;
            try
            {
                i = Convert.ToInt32(txtRetornoCodRepresentante.Text);
            }
            catch
            {
                MessageBox.Show("Código Inválido");
            }


            var representante = ModelLibrary.MetodosDeposito.ObterRepresentante(i);

            if (representante != null)
            {
                cbbRetornoRepresentante.SelectedIndex = cbbRetornoRepresentante.FindString(representante.Nome);

            }
            else
            {
                cbbRetornoRepresentante.SelectedIndex = -1;
            }


        }

        private void cbbRetornoRepresentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ModelLibrary.Representante representante = (ModelLibrary.Representante)cbbRetornoRepresentante.SelectedItem;
                txtRetornoCodRepresentante.Text = representante.Id.ToString();
                //cbbRetornoMesAno.Focus();
            }
            catch
            {
                txtRetornoCodRepresentante.Text = "";
            }
        }

        private void txtRetornoCodRepresentante_Leave(object sender, EventArgs e)
        {
            if (txtRetornoCodRepresentante.Text != "") txtRetornoCodRepresentante_ButtonClick(sender, e);
        }



        private void RetornoKeyEnter(object sender, KeyEventArgs e)
        {
            string objname = ((MetroFramework.Controls.MetroTextBox)sender).Name;


            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (objname == "txtRetornoCodRepresentante")
                {
                    txtRetornoCodRepresentante_ButtonClick(sender, e);
                    cbbRetornoMesAno.Focus();
                }
                else if (objname == "txtRetornoCodPraca")
                {
                    txtRetornoCodPraca_ButtonClick(sender, e);
                    txtRetornoCodRepresentante.Focus();
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }

                e.Handled = true;//set to false if you need that textbox gets enter key
            }
        }

        private void cbbRetornoMesAno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                e.SuppressKeyPress = true;
                btnRetornoOK.Focus();

            }
        }

        private void bntRetornoOK_Click(object sender, EventArgs e)
        {
            if (txtRetornoCodPraca.Text != "" && txtRetornoCodRepresentante.Text != "")
            {
                cRetorno.PesquisarCarga();
            }
            

        }

        private void bntRetornoOK_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bntRetornoOK_Click(sender, e);
            }
        }

        private void btnRetornoPesquisar_Click(object sender, EventArgs e)
        {
            ///Abre Janela de Pesquisa de Retorno com opção para Alterar / Excluir - se tiver permissão
        }

        private void btnRetornoLimpar_Click(object sender, EventArgs e)
        {
            cRetorno.LimparRetorno();
        }



        private void txtRetornoCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtRetornoCodigoBarras.Text != "")
                {
                    e.SuppressKeyPress = true;
                    cRetorno.PesquisarRetornoProduto(txtRetornoCodigoBarras.Text);
                }
            }
        }

        private void txtRetornoCodigoBarras_Leave(object sender, EventArgs e)
        {
            if (txtRetornoCodigoBarras.Text != "")
            {
                cRetorno.PesquisarRetornoProduto(txtRetornoCodigoBarras.Text);
            }
        }

        private void chkRetornoQuantidade_CheckedChanged(object sender, EventArgs e)
        {
            if (cRetorno.cRetornoProdutoGradeId != 0)
            {
                chkRetornoQuantidade.Checked = true;
                txtRetornoQuantidade.Enabled = true;
            }
            else
            {
                txtRetornoQuantidade.Text = "";
                txtRetornoQuantidade.Enabled = chkRetornoQuantidade.Checked;
            }

        }

        private void bntRetornoConfirmar_Click(object sender, EventArgs e)
        {

            cRetorno.ConfirmarRetornoProdutoGrade();

        }

        private void btnRetornoCancelar_Click(object sender, EventArgs e)
        {
            cRetorno.LimparRetornoProduto();
        }



        private void grdRetornoProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cRetorno.ExibirRetornoProdutoGrade();
        }

        private void grdRetornoProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {

                MessageBox.Show("Opção não disponível.");
            }

        }





        private void alterarRetornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cRetorno.ExibirRetornoProdutoGrade();
        }



        private void excluirRetornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opção não disponível.");
        }

        private void btnPedidoFechadoAtualAtual_Click(object sender, EventArgs e)
        {
            cRetorno.CarregarPedidosFechados();

        }

        private void btnPedidoFechadoAnterior_Click(object sender, EventArgs e)
        {
            cRetorno.CarregarPedidosFechados(false);
        }

        private void grdContasAReceber_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cRetorno.ExibirAReceber();
        }

        private void btnRetornoRecConfirmar_Click(object sender, EventArgs e)
        {
            cRetorno.ConfirmarAReceber();
        }

        private void btnRetornoRecCancelar_Click(object sender, EventArgs e)
        {
            cRetorno.LimparAReceber();
        }

        private void txtRetornoRecDocumento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtRetornoRecSerie.Focus();

            }
        }

        private void txtRetornoRecSerie_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtRetornoRecSerie_Leave(sender, e);
            }
        }

        private void txtRetornoRecSerie_Leave(object sender, EventArgs e)
        {
            if (txtRetornoRecDocumento.Text != "")
            {
                cRetorno.PesquisarAReceber(txtRetornoRecDocumento.Text, txtRetornoRecSerie.Text);
                txtRetornoRecValor.Focus();
            } else
            {
                MessageBox.Show("Informe o Número do Documento");
                txtRetornoRecDocumento.Focus();
            }

        }

        private void txtLancPedCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtLancPedCodigoBarras_Leave(sender, e);

            }
        }

        private void txtLancPedCodigoBarras_Leave(object sender, EventArgs e)
        {
            if (txtLancPedCodigoBarras.Text != "")
            {
                cRetorno.LancamentoPedidoPesquisar(txtLancPedCodigoBarras.Text);

            } else
            {
                MessageBox.Show("Informe o Código de Barras do Produto");
            }
            
        }

        private void btnLancPedConfirmar_Click(object sender, EventArgs e)
        {
            cRetorno.SalvarLancamentoPedido();
        }

        private void btnLancPedCancelar_Click(object sender, EventArgs e)
        {
            cRetorno.LancamentoPedidoItemLimpar();
        }

        private void btnPesqVendedorOK_Click(object sender, EventArgs e)
        {
            cRetorno.VendedorPesquisar();
        }

        private void btnPesqVendedorLimpar_Click(object sender, EventArgs e)
        {
            cRetorno.VendedorLimpar();
        }

        private void grdLancPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cRetorno.LancamentoPedidoPesquisar(grdLancPedido.CurrentRow.Cells[1].Value.ToString());
        }

        private void btnFinalizarAcerto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Realmente finalizar o Acerto? ATENÇÃO: Não será possível retornar produtos ou pedidos após confirmar essa ação.", "Finalizar Acerto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cRetorno.FinalizarAcerto();
            }
        }

        private void btnConferenciaProdutos_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Deseja Realmente finalizar a Conferência de Produtos? ATENÇÃO: Não será possível retornar produtos após confirmar essa ação.", "Finalizar Conferência de Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cRetorno.FinalizarRetorno();
            }
            
        }

        private void Controls_KeyUp(object sender, KeyEventArgs e)
        {
          

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            
        }

        private void grdRetornoPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cbbPesqVendedor.Text = grdRetornoPedido.CurrentRow.Cells["Nome"].Value.ToString();
            tbcRetorno.SelectedTab = tabRetornoLancPedidos;
            //cRetorno.VendedorExibir(Convert.ToInt64(grdRetornoPedido.CurrentRow.Cells["VendedorId"].Value));
        }

        private void lblRetornoResumoSugestao_Click(object sender, EventArgs e)
        {

        }





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


