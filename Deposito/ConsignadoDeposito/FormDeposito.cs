﻿using System;
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
using CrystalDecisions.CrystalReports.Engine;
using ConsignadoDeposito.Reports;


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
        public Estoque cEstoque;




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
            cEstoque = new Estoque(this);



        }

        public void ControlOnlyNumbers(object sender, KeyPressEventArgs e)
        {


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
    (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }

        }


        public void ControlOnlyInt(object sender, KeyPressEventArgs e)
        {


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }


        ////////////////////////////////////////
        /// Funções do Formulário Principal
        ////////////////////////////////////////



        public void CarregarDeposito()
        {
            DateTime date = DateTime.Today;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            cbbCargaMesAno.Value = firstDayOfMonth;
            cbbRetornoMesAno.Value = firstDayOfMonth;
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
            if (cbbCargaPraca.SelectedIndex != -1 && cbbCargaRepresentante.SelectedIndex != -1)
            {
                cCarga.PesquisarCarga();
            } else
            {
                MessageBox.Show("Praça ou Representante não encontrados.");
            }
        }

        private void bntCargaOK_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (cbbCargaPraca.SelectedIndex != -1 && cbbCargaRepresentante.SelectedIndex != -1)
                {
                    e.SuppressKeyPress = true;
                    bntCargaOK_Click(sender, e);
                }
            }
        }

        private void btnCargaPesquisar_Click(object sender, EventArgs e)
        {
            
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
                    //cCarga.PesquisarCargaProduto(txtCargaCodigoBarras.Text);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void txtCargaCodigoBarras_Leave(object sender, EventArgs e)
        {
            //if (txtCargaCodigoBarras.Text != "")
            //{
            //    cCarga.PesquisarCargaProduto(txtCargaCodigoBarras.Text);
            //}
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
            if (cbbRetornoPraca.SelectedIndex != -1 && cbbRetornoRepresentante.SelectedIndex != -1)
            {
                cRetorno.PesquisarCarga();
            } else
            {
                MessageBox.Show("Praça ou Representante não encontrados.");
            }
            

        }

        private void bntRetornoOK_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (cbbRetornoPraca.SelectedIndex != -1 && cbbRetornoRepresentante.SelectedIndex != -1)
                {
                    e.SuppressKeyPress = true;
                    bntRetornoOK_Click(sender, e);
                }
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
                    //cRetorno.PesquisarRetornoProduto(txtRetornoCodigoBarras.Text);
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void txtRetornoCodigoBarras_Leave(object sender, EventArgs e)
        {
            //if (txtRetornoCodigoBarras.Text != "")
            //{
            //    cRetorno.PesquisarRetornoProduto(txtRetornoCodigoBarras.Text);
            //}
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
            //Exibir Detalhes
        }



        

        private void btnFinalizarAcerto_Click(object sender, EventArgs e)
        {

        }

        private void btnConferenciaProdutos_Click(object sender, EventArgs e)
        {


            
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

            Cursor.Current = Cursors.WaitCursor;
            Modal.FormPedido formPedido = new Modal.FormPedido(this);
            formPedido.ExibirDetalhesPedido(grdRetornoPedido.CurrentRow.Cells["CodigoPedido"].Value.ToString());
            formPedido.ShowDialog();
            
            Cursor.Current = Cursors.Default;

        }

        private void lblRetornoResumoSugestao_Click(object sender, EventArgs e)
        {

        }


        private void btnRelatorioCarga_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRetornoRefazer_Click(object sender, EventArgs e)
        {

            
        }

        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormNovoPedido formNovoPedido = new Modal.FormNovoPedido(this);
            formNovoPedido.ShowDialog();
            Cursor.Current = Cursors.Default;

        }

        private void btnAcoes_Click(object sender, EventArgs e)
        {
            
        }

        private void menuCadastroProduto_Click(object sender, EventArgs e)
        {

        }

        private void txtCargaCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            if (txtCargaCodigoBarras.Text != "")
            {
                cCarga.PesquisarCargaProduto(txtCargaCodigoBarras.Text);
            }
        }

        private void txtRetornoCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            if (txtRetornoCodigoBarras.Text != "")
            {
                cRetorno.PesquisarRetornoProduto(txtRetornoCodigoBarras.Text);
            }
        }


        private void btnRetornoPesquisar_Click_1(object sender, EventArgs e)
        {

        }

        private void txtProdutosCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            cEstoque.PesquisarProdutos();
        }

        private void txtProdutosNome_KeyUp(object sender, KeyEventArgs e)
        {
            cEstoque.PesquisarProdutos();
        }

        private void btnProdutosLimpar_Click(object sender, EventArgs e)
        {
            cEstoque.ProdutosLimpar();
        }

        private void cbbProdutoSaldo_SelectedValueChanged(object sender, EventArgs e)
        {
            cEstoque.PesquisarProdutos();
        }

        private void mnuProdutoAdicionar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormProduto formProduto = new Modal.FormProduto(this);
            formProduto.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void mnuCargaPesquisar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormListaCarga formPesquisa = new Modal.FormListaCarga(this, "Carga");
            var result = formPesquisa.ShowDialog();

            if (result == DialogResult.OK)
            {

                txtCargaCodPraca.Text = formPesquisa.cPracaId.ToString();
                txtCargaCodPraca_ButtonClick(sender, e);
                txtCargaCodRepresentante.Text = formPesquisa.cRepresentanteId.ToString();
                txtCargaCodRepresentante_ButtonClick(sender, e);
                cbbCargaMesAno.Value = Convert.ToDateTime(formPesquisa.cAno.ToString() + "-" + formPesquisa.cMes.ToString() + "-01");

                // MessageBox.Show(Convert.ToDateTime(formPesquisa.cAno.ToString() + "-" + formPesquisa.cMes.ToString() + "-01").ToString());

                cCarga.PesquisarCarga();
            }

            Cursor.Current = Cursors.Default;
        }

        private void mnuRetornoPesquisar_Click(object sender, EventArgs e)
        {


            Cursor.Current = Cursors.WaitCursor;
            Modal.FormListaCarga formPesquisa = new Modal.FormListaCarga(this, "Retorno");
            var result = formPesquisa.ShowDialog();

            if (result == DialogResult.OK)
            {

                txtRetornoCodPraca.Text = formPesquisa.cPracaId.ToString();
                txtRetornoCodPraca_ButtonClick(sender, e);
                txtRetornoCodRepresentante.Text = formPesquisa.cRepresentanteId.ToString();
                txtRetornoCodRepresentante_ButtonClick(sender, e);
                cbbRetornoMesAno.Value = Convert.ToDateTime(formPesquisa.cAno.ToString() + "-" + formPesquisa.cMes.ToString() + "-01");


                cRetorno.PesquisarCarga();

            }
        }

        private void mnuRetornoAcoes_Click(object sender, EventArgs e)
        {
            switch (mnuRetornoAcoes.Text)
            {
                case "Finalizar Conferencia de Produtos":
                    if (MessageBox.Show("Deseja Realmente finalizar a Conferência de Produtos? ATENÇÃO: Não será possível retornar produtos após confirmar essa ação.", "Finalizar Conferência de Produtos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cRetorno.FinalizarRetorno();
                    }
                    break;
                case "Finalizar Acerto":
                    if (MessageBox.Show("Deseja Realmente finalizar o Acerto? ATENÇÃO: Não será possível retornar produtos ou pedidos após confirmar essa ação.", "Finalizar Acerto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cRetorno.FinalizarAcerto();
                    }
                    break;
                case "Refazer Retorno":
                    if (MessageBox.Show("Deseja realmente refazer o retorno? ATENÇÃO: Todas as informações de retorno registradas serão perdidas!", "Refazer Retorno", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        cRetorno.RefazerRetorno();
                    }
                    break;
            }
        }



        private void btnRetornoAcertoIncluir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormAReceber formAReceber = new Modal.FormAReceber(this, "Create");
            formAReceber.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void smnRetornoAcertoTitulo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormAReceber formAReceber = new Modal.FormAReceber(this, "Update");
            formAReceber.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void grdContasAReceber_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                cmsRetornoAcerto.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void grdContasAReceber_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    grdContasAReceber.CurrentCell = grdContasAReceber.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    grdContasAReceber.Rows[e.RowIndex].Selected = true;
                    grdContasAReceber.Focus();

                    cRetorno.cRetornoReceberId = Convert.ToInt32(grdContasAReceber.Rows[e.RowIndex].Cells[1].Value);
                }
                catch (Exception)
                {

                }
            }
        }

        private void smnRetornoAcertoPagamento_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormRecebimento formReceberBaixa = new Modal.FormRecebimento(this, "Titulo", Convert.ToInt32(grdContasAReceber.CurrentRow.Cells["Id"].Value), 0, cRetorno.cRetornoId);
            formReceberBaixa.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void grdContasAReceber_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            smnRetornoAcertoPagamento_Click(sender, e);
        }

        private void mnuProdutoMovimentar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormEstoque formEstoque = new Modal.FormEstoque(this);
            formEstoque.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void mnuProdutoImprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            mnuProdutoImprimir.Text = "Imprimindo...";
            mnuProdutoImprimir.Enabled = false;



            var vCriterio = new Dictionary<string, string>();

            if (txtProdutosCodigoBarras.Text != "") vCriterio["CodigoGeral"] = txtProdutosCodigoBarras.Text;

            if (txtProdutosNome.Text != "") vCriterio["Nome"] = txtProdutosNome.Text;


            if (cbbProdutoSaldo.Text == "Com Saldo em Estoque")
            {
                vCriterio["SaldoEstoque"] = "Y";
            }
            else if (cbbProdutoSaldo.Text == "Sem Saldo em Estoque")
            {
                vCriterio["SaldoEstoque"] = "N";
            }


            List<ModelLibrary.RelatoriosDeposito.EstoqueProduto> estoqueproduto = ModelLibrary.RelatoriosDeposito.RelatorioEstoqueProduto(vCriterio);

            if (estoqueproduto == null)
            {
                MessageBox.Show("Erro ao imprimir relatório - Não foi possível encontrar produto.", "Reder - Impressão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                BindingSource bs = new BindingSource();

                Reports.EstoqueProduto relatorioEstoqueProduto = new Reports.EstoqueProduto();

                bs.DataSource = estoqueproduto;
                relatorioEstoqueProduto.SetDataSource(bs);


                relatorioEstoqueProduto.PrintToPrinter(1, true, 0, 0);

                mnuProdutoImprimir.Text = "Imprimir";
                mnuProdutoImprimir.Enabled = true;
                Cursor.Current = Cursors.Default;


                //FormRelatorio formRelatorio = new FormRelatorio();
                //formRelatorio.Show();


                //formRelatorio.crvRelatorio.ReportSource = relatorioEstoqueProduto;
                //formRelatorio.crvRelatorio.RefreshReport();
            }




        }

        private void mnuAjudaAtualizacoes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em desenvolvimento...");
        }

        private void mnuAjudaSobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em desenvolvimento...");
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smnCobrancaViagem_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;



            List<ModelLibrary.RelatoriosDeposito.CobrancaCarga> cobrancaCargas = ModelLibrary.RelatoriosDeposito.RelatorioCobrancaCarga(cCarga.cCargaId);

            if (cobrancaCargas == null)
            {
                MessageBox.Show("Erro ao imprimir relatório - O relatório está vazio.", "Reder - Impressão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                BindingSource bs = new BindingSource();

                Reports.CobrancaCarga relatorioCobrancaCarga = new Reports.CobrancaCarga();

                bs.DataSource = cobrancaCargas;
                relatorioCobrancaCarga.SetDataSource(bs);


                FormRelatorio formRelatorio = new FormRelatorio();
                formRelatorio.Show();


                formRelatorio.crvRelatorio.ReportSource = relatorioCobrancaCarga;
                formRelatorio.crvRelatorio.RefreshReport();
            }


            Cursor.Current = Cursors.Default;

        }

        private void smnRetornoAnalise_Click(object sender, EventArgs e)
        {


            Cursor.Current = Cursors.WaitCursor;



            List<ModelLibrary.RelatoriosDeposito.AnaliseRetorno> analiseRetorno = ModelLibrary.RelatoriosDeposito.RelatorioAnaliseRetorno(cRetorno.cRetornoId);

            if (analiseRetorno == null)
            {
                MessageBox.Show("Erro ao imprimir relatório - O relatório está vazio.", "Reder - Impressão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                BindingSource bs = new BindingSource();

                Reports.AnaliseRetorno relatorioAnaliseRetorno = new Reports.AnaliseRetorno();

                bs.DataSource = analiseRetorno;
                relatorioAnaliseRetorno.SetDataSource(bs);


                FormRelatorio formRelatorio = new FormRelatorio();
                formRelatorio.Show();


                formRelatorio.crvRelatorio.ReportSource = relatorioAnaliseRetorno;
                formRelatorio.crvRelatorio.RefreshReport();
            }


            Cursor.Current = Cursors.Default;

        }

        private void mnuCargaExcluir_Click(object sender, EventArgs e)
        {
            cCarga.CargaExcluir();
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


