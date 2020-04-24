using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoDeposito;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using System.Diagnostics;

namespace ConsignadoDeposito
{
    public partial class Retorno
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////

        public Int32 cRetornoProdutoGradeId;
        public string cModoRetornoProduto;
        public Int32 cRetornoId;
        public Int32 cRetornoVendedorId;
        public Int32 cRetornoPedidoId;
        public string cRetornoPedidoCodigo;
        public Int32 cRetornoPedidoItemId;
        public Int32 cRetornoPedidoProdutoGradeId;
        public string cModoRetornoPedidoItem;
        public Int32 cRetornoReceberId;
        public Int32 cRetornoReceberBaixaId;
        public string cModoRetornoReceber;

        public FormDeposito localDepositoForm = null;

        public Retorno(FormDeposito formDeposito)
        {

            localDepositoForm = formDeposito;

        }


        ///////////////////////////////////////////
        /// Carregar Formulário de Pesquisa Retorno
        ///////////////////////////////////////////

        public void CarregarListaRetorno()
        {
            try
            {
                localDepositoForm.cbbRetornoPraca.DataSource = ModelLibrary.MetodosDeposito.ObterListaPracas();
                localDepositoForm.cbbRetornoPraca.DisplayMember = "Descricao";
                localDepositoForm.cbbRetornoPraca.ValueMember = "Id";
                localDepositoForm.cbbRetornoPraca.Invalidate();
                localDepositoForm.cbbRetornoPraca.SelectedIndex = -1;
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarListaRetorno()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void CarregarListaRepresentante()
        {

            try
            {
                localDepositoForm.cbbRetornoRepresentante.DataSource = ModelLibrary.MetodosDeposito.ObterListaRepresentantes();
                localDepositoForm.cbbRetornoRepresentante.DisplayMember = "Nome";
                localDepositoForm.cbbRetornoRepresentante.ValueMember = "Id";
                localDepositoForm.cbbRetornoRepresentante.Invalidate();
                localDepositoForm.cbbRetornoRepresentante.SelectedIndex = -1;
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarListaRepresentante()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        public void LimparRetorno()
        {

            try
            {
                localDepositoForm.cbbRetornoPraca.SelectedIndex = -1;
                localDepositoForm.cbbRetornoRepresentante.SelectedIndex = -1;
                localDepositoForm.txtRetornoCodPraca.Text = "";
                localDepositoForm.txtRetornoCodRepresentante.Text = "";
                localDepositoForm.cbbRetornoMesAno.ResetText();

                localDepositoForm.tbcRetorno.Visible = false;
                localDepositoForm.txtRetornoCodigoBarras.Text = "";
                localDepositoForm.txtRetornoProduto.Text = "";
                localDepositoForm.txtRetornoQuantidade.Text = "";
                localDepositoForm.btnRetornoConfirmar.Enabled = false;


                localDepositoForm.dlbRetornoDataAbertura.Text = "-";
                localDepositoForm.dlbRetornoDataExportacao.Text = "-";
                localDepositoForm.dlbRetornoDataRetorno.Text = "-";
                localDepositoForm.dlbRetornoDataConferencia.Text = "-";
                localDepositoForm.dlbRetornoDataFinalizacao.Text = "-";




                localDepositoForm.dlbRetornoQtdProdutos.Text = "0";
                localDepositoForm.dlbRetornoTotalProdutos.Text = "0.00";


                localDepositoForm.dlbRetornoTotalProdutos.FontSize = MetroFramework.MetroLabelSize.Medium;


                localDepositoForm.mnuRetornoAcoes.Text = "Selecione a Carga";
                localDepositoForm.mnuRetornoAcoes.Enabled = false;

                localDepositoForm.smnRetornoAnalise.Enabled = false;


                localDepositoForm.dlbTotalAcerto.Text = "-";
                localDepositoForm.dlbTotalAberto.Text = "-";
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.LimparRetorno()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }

        public void ResetarVariaveis()
        {
            cRetornoId = 0;
            cRetornoProdutoGradeId = 0;
            cRetornoId = 0;
        }


        public void RetornoReload()
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                PesquisarCarga();
                CarregarResumo();
                CarregarGradeRetornoProduto(cRetornoId);
                CarregarPedidos();
                CarregarPedidosFechados();
                CarregarContasAReceber();
                CarregarConferenciaProdutos();
                CarregarAcerto();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.RetornoReload()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        ////////////////////////////////////////
        /// Pesquisar Carga
        ////////////////////////////////////////

        public void PesquisarCarga()
        {

            try
            {

                /* Obter os Campos Selecionados */


                ModelLibrary.Representante representante = (ModelLibrary.Representante)localDepositoForm.cbbRetornoRepresentante.SelectedItem;
                var representanteId = representante.Id;
                ModelLibrary.Praca praca = (ModelLibrary.Praca)localDepositoForm.cbbRetornoPraca.SelectedItem;
                var pracaId = praca.Id;
                int mes = localDepositoForm.cbbRetornoMesAno.Value.Month;
                int ano = localDepositoForm.cbbRetornoMesAno.Value.Year;

                /* Procurar Carga no BD com os dados selecionados */


                var carga = ModelLibrary.MetodosDeposito.ObterCarga(representanteId, pracaId, mes, ano);

                if (carga != null) /* Se existir Carga */
                {
                    /* Carrega Grid com Produtos */


                    cRetornoId = carga.Id;                    

                    var totalizadores = ModelLibrary.MetodosDeposito.ObterTotalizadoresRetorno(cRetornoId);

                    localDepositoForm.dlbRetornoQtdProdutos.Text = totalizadores.QtdProdutos.ToString();
                    localDepositoForm.dlbRetornoTotalProdutos.Text = String.Format("{0:C2}", totalizadores.TotalProdutos);


                    if (totalizadores.TotalProdutos > 999999)
                    {
                        localDepositoForm.dlbRetornoTotalProdutos.FontSize = MetroFramework.MetroLabelSize.Small;
                    }
                    else
                    {
                        localDepositoForm.dlbRetornoTotalProdutos.FontSize = MetroFramework.MetroLabelSize.Medium;
                    }

                    localDepositoForm.dlbRetornoDataAbertura.Text = carga.DataAbertura.HasValue ? carga.DataAbertura.Value.ToShortDateString() : "-";
                    localDepositoForm.dlbRetornoDataExportacao.Text = carga.DataExportacao.HasValue ? carga.DataExportacao.Value.ToShortDateString() : "-";
                    localDepositoForm.dlbRetornoDataRetorno.Text = carga.DataRetorno.HasValue ? carga.DataRetorno.Value.ToShortDateString() : "-";
                    localDepositoForm.dlbRetornoDataConferencia.Text = carga.DataConferencia.HasValue ? carga.DataConferencia.Value.ToShortDateString() : "-";
                    localDepositoForm.dlbRetornoDataFinalizacao.Text = carga.DataFinalizacao.HasValue ? carga.DataFinalizacao.Value.ToShortDateString() : "-";

                    


                    CarregarFormulario();


                    localDepositoForm.smnRetornoAnalise.Enabled = true;


                    /// se status da carga = R
                    if (carga.Status == "R")
                    {
                        /// Habilita Retorno de Produtos
                        /// Habilita Finalizar Conferencia de Produtos
                        /// Habilita Lançamento de Pedidos
                        /// Habilita Acerto
                        /// Habilita Finalizar Acerto
                        localDepositoForm.pnlRetornoProduto.Enabled = true;
                        localDepositoForm.pnlRetornoPedidoTop.Enabled = true;
                        localDepositoForm.pnlContasAReceberTop.Enabled = true;

                        localDepositoForm.mnuRetornoAcoes.Text = "Finalizar Conferencia de Produtos";
                        localDepositoForm.mnuRetornoAcoes.Enabled = true;

                    }
                    else if (carga.Status == "C") {
                        /// Desabilita Retorno de Produtos
                        /// Desabilita Finalizar Conferencia de Produtos
                        /// Habilita Lançamento de Pedidos
                        /// Habilita Acerto
                        /// Habilita Finalizar Acerto
                        localDepositoForm.pnlRetornoProduto.Enabled = false;
                        localDepositoForm.pnlRetornoPedidoTop.Enabled = true;
                        localDepositoForm.pnlContasAReceberTop.Enabled = true;
                        localDepositoForm.mnuRetornoAcoes.Text = "Finalizar Acerto";
                        localDepositoForm.mnuRetornoAcoes.Enabled = true;
                    }
                    else if (carga.Status == "F")
                    {
                        /// Desabilita Retorno de Produtos
                        /// Desabilita FInalizar Conferencia de Produtos
                        /// Desabilita Lançamento de Pedidos
                        /// Desabilita Acerto
                        /// Desabilita Finalizar Acerto
                        localDepositoForm.pnlRetornoProduto.Enabled = false;
                        localDepositoForm.pnlRetornoPedidoTop.Enabled = false;
                        localDepositoForm.pnlContasAReceberTop.Enabled = false;


                        localDepositoForm.mnuRetornoAcoes.Text = "Refazer Retorno";
                        localDepositoForm.mnuRetornoAcoes.Enabled = true;
                    }
                    else if (carga.Status == "A")
                    {
                        /// Desabilita Retorno de Produtos
                        /// Desabilita FInalizar Conferencia de Produtos
                        /// Desabilita Lançamento de Pedidos
                        /// Desabilita Acerto
                        /// Desabilita Finalizar Acerto
                        localDepositoForm.pnlRetornoProduto.Enabled = false;
                        localDepositoForm.pnlRetornoPedidoTop.Enabled = false;
                        localDepositoForm.pnlContasAReceberTop.Enabled = false;


                        localDepositoForm.mnuRetornoAcoes.Text = "Retorno não iniciado";
                        localDepositoForm.mnuRetornoAcoes.Enabled = false;
                    }





                }
                else /* Se não existir */
                {
                    MessageBox.Show("Não foi encontrada nenhuma carga com os dados informados.", "Importante!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (IOException vE)
            {
                MessageBox.Show("Ocorreu um erro ao processar a sua consulta. Verifique os dados digitados e tente novamente. Se o erro persisitr, contate o administrador.", "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(vE.Message);
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.PesquisarCarga()");
                Trace.TraceError(vE.Message);
            }
        }


        public void CarregarFormulario()
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                localDepositoForm.tbcRetorno.SelectedTab = localDepositoForm.tabRetornoProdutos;
                CarregarResumo();
                CarregarGradeRetornoProduto(cRetornoId);
                CarregarPedidos();
                CarregarPedidosFechados();
                CarregarContasAReceber();
                CarregarConferenciaProdutos();
                CarregarAcerto();
                Cursor.Current = Cursors.Default;

                localDepositoForm.tbcRetorno.Visible = true;
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarFormulario()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        ////////////////////////////////////////////
        /// Carregar Resumo
        ////////////////////////////////////////////
        

        public void CarregarResumo()
        {


        }

        ////////////////////////////////////////////
        /// Carregar Pesquisa de Produtos do Retorno
        ////////////////////////////////////////////


        public void LimparRetornoProduto()
        {
            try
            {
                localDepositoForm.txtRetornoCodigoBarras.Text = "";
                localDepositoForm.txtRetornoProduto.Text = "";
                localDepositoForm.txtRetornoQuantidade.Text = "";
                localDepositoForm.btnRetornoConfirmar.Enabled = false;
                localDepositoForm.btnRetornoCancelar.Enabled = false;
                localDepositoForm.txtRetornoCodigoBarras.ReadOnly = false;
                cRetornoProdutoGradeId = 0;
                cModoRetornoProduto = "Insert";
                localDepositoForm.txtRetornoCodigoBarras.Focus();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.LimparRetornoProduto()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        void CarregarGradeRetornoProduto(int pCargaId)
        {


            try
            {
                ModelLibrary.Representante representante = (ModelLibrary.Representante)localDepositoForm.cbbRetornoRepresentante.SelectedItem;
                var representanteId = representante.Id;
                ModelLibrary.Praca praca = (ModelLibrary.Praca)localDepositoForm.cbbRetornoPraca.SelectedItem;
                var pracaId = praca.Id;
                int mes = localDepositoForm.cbbRetornoMesAno.Value.Month;
                int ano = localDepositoForm.cbbRetornoMesAno.Value.Year;

                List<ModelLibrary.ListaProdutosCarga> produtos = ModelLibrary.MetodosDeposito.ObterProdutosCarga(pCargaId, true);

                BindingListView<ModelLibrary.ListaProdutosCarga> view = new BindingListView<ModelLibrary.ListaProdutosCarga>(produtos);

                localDepositoForm.grdRetornoProduto.DataSource = view;

                localDepositoForm.grdRetornoProduto.Columns[2].Width = 250;

                /// Ocultar colunas CargaId e cRetornoProdutoGradeId
                localDepositoForm.grdRetornoProduto.Columns[9].Visible = false;
                localDepositoForm.grdRetornoProduto.Columns[10].Visible = false;

                /// Exibir Coluna como "Moeda"
                localDepositoForm.grdRetornoProduto.Columns[6].DefaultCellStyle.Format = "c";
                localDepositoForm.grdRetornoProduto.Columns[7].DefaultCellStyle.Format = "c";

                /// Alterar Título da Coluna
                localDepositoForm.grdRetornoProduto.Columns[0].HeaderText = "Código de Barras";

                //Ocultar coluna Quantidade
                localDepositoForm.grdRetornoProduto.Columns[4].Visible = false;

                localDepositoForm.grdRetornoProduto.Columns[5].HeaderText = "Quantidade Retorno";

                localDepositoForm.grdRetornoProduto.Columns[6].HeaderText = "Valor Saída";
                localDepositoForm.grdRetornoProduto.Columns[7].HeaderText = "Valor Custo";
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarGradeRetornoProduto()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }


        public void LimparGradeRetornoProduto()
        {
            localDepositoForm.grdRetornoProduto.DataSource = null;
            localDepositoForm.tbcRetorno.Visible = false;

        }


        public void PesquisarRetornoProduto(string pCodigo)
        {

            try
            {
                long vProdutoGradeId = 0;
                List<ModelLibrary.ProdutoGrade> produtosgrade = ModelLibrary.MetodosDeposito.ObterProdutosGrade(pCodigo);

                if (produtosgrade != null)
                {
                    if (produtosgrade.Count > 1)
                    {
                        Modal.FormProdutosGrade formProdutosGrade = new Modal.FormProdutosGrade(pCodigo);


                        var result = formProdutosGrade.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            vProdutoGradeId = formProdutosGrade.cProdutoGradeId;
                            ExibirProdutoGrade(vProdutoGradeId);
                        }
                        else
                        {
                            vProdutoGradeId = 0;
                        }
                    }
                    else
                    {
                        vProdutoGradeId = (produtosgrade.FirstOrDefault() != null) ? produtosgrade.FirstOrDefault().Id : 0;
                        ExibirProdutoGrade(vProdutoGradeId);
                    }
                }
                else
                {
                    vProdutoGradeId = 0;
                    ExibirProdutoGrade(vProdutoGradeId);
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.PesquisarRetornoProduto()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            

        }


        public void ExibirProdutoGrade(long pProdutoGradeId)
        {
            try
            {
                var produtograde = ModelLibrary.MetodosDeposito.ObterProdutoGrade("", pProdutoGradeId);

                if (produtograde != null)
                {

                    var produto = ModelLibrary.MetodosDeposito.ObterProduto(produtograde.CodigoBarras);

                    if (produtograde.Status != "1" || produto.Status != "1")
                    {

                        MessageBox.Show("Este produto foi excluído e não pode ser retornado.");

                    }
                    else
                    {

                        localDepositoForm.txtRetornoProduto.Text = produto.Descricao;

                        if (localDepositoForm.txtRetornoCodigoBarras.Text != produtograde.CodigoBarras + produtograde.Digito)
                        {
                            localDepositoForm.txtRetornoCodigoBarras.Text = produtograde.CodigoBarras + produtograde.Digito;
                            if (localDepositoForm.chkRetornoQuantidade.Checked == false)
                            {
                                localDepositoForm.chkRetornoQuantidade.Checked = true;
                                localDepositoForm.txtRetornoQuantidade.Enabled = true;
                            }
                        }



                        cRetornoProdutoGradeId = produtograde.Id;


                        localDepositoForm.btnRetornoConfirmar.Enabled = true;
                        localDepositoForm.btnRetornoCancelar.Enabled = true;

                        if (localDepositoForm.chkRetornoQuantidade.Checked)
                        {
                            localDepositoForm.txtRetornoQuantidade.Focus();

                        }
                        else
                        {
                            //inserir direto qtd=1
                            var cargaproduto = ModelLibrary.MetodosDeposito.ObterProdutoCarga(cRetornoId, cRetornoProdutoGradeId);

                            double tempQtd = cargaproduto.Retorno.Value;
                            localDepositoForm.txtRetornoQuantidade.Text = (tempQtd + 1).ToString();
                            AlterarRetornoProdutoGrade();

                        }

                    }



                }
                else
                {

                    MessageBox.Show("Dígito verificador inválido. Não foi possível encontrar a grade deste produto.");

                    cRetornoProdutoGradeId = 0;
                    localDepositoForm.txtRetornoCodigoBarras.Text = "";
                    localDepositoForm.txtRetornoCodigoBarras.Focus();
                    localDepositoForm.btnRetornoConfirmar.Enabled = false;
                    localDepositoForm.btnRetornoCancelar.Enabled = false;


                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.ExibirProdutoGrade()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
        }

        public void ExibirRetornoProdutoGrade()
        {


            try
            {
                //ClearRetornoProduto();


                cModoRetornoProduto = "Edit";
                cRetornoProdutoGradeId = Convert.ToInt32(localDepositoForm.grdRetornoProduto.CurrentRow.Cells["ProdutoGradeId"].Value);

                localDepositoForm.txtRetornoCodigoBarras.Text = localDepositoForm.grdRetornoProduto.CurrentRow.Cells["CodigoBarras"].Value.ToString();
                localDepositoForm.txtRetornoCodigoBarras.ReadOnly = true;


                if (localDepositoForm.chkRetornoQuantidade.Checked == false)
                {
                    localDepositoForm.chkRetornoQuantidade.Checked = true;
                    localDepositoForm.txtRetornoQuantidade.Enabled = true;
                }
                localDepositoForm.txtRetornoQuantidade.Text = localDepositoForm.grdRetornoProduto.CurrentRow.Cells["Retorno"].Value.ToString();

                localDepositoForm.txtRetornoProduto.Text = localDepositoForm.grdRetornoProduto.CurrentRow.Cells["Descricao"].Value.ToString();


                localDepositoForm.btnRetornoConfirmar.Enabled = true;
                localDepositoForm.btnRetornoCancelar.Enabled = true;
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.ExibirRetornoProdutoGrade()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }






        public void AlterarRetornoProdutoGrade()
        {


            try
            {
                ModelLibrary.MetodosDeposito.AlterarRetornoProduto(cRetornoId, cRetornoProdutoGradeId, Convert.ToDouble(localDepositoForm.txtRetornoQuantidade.Text));

                CarregarGradeRetornoProduto(cRetornoId);

                LimparRetornoProduto();

                RetornoReload();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.AlterarRetornoProdutoGrade()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        public void ConfirmarRetornoProdutoGrade()
        {

            AlterarRetornoProdutoGrade();

        }


        public void FinalizarRetorno()
        {

            try
            {
                ModelLibrary.MetodosDeposito.AlterarStatusCarga(cRetornoId, "C");

                MessageBox.Show("Retorno de Produtos Finalizado com Sucesso!");

                PesquisarCarga();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.FinalizarRetorno()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        public void RefazerRetorno()
        {

            try
            {
                ModelLibrary.MetodosDeposito.RefazerRetorno(cRetornoId);

                MessageBox.Show("O retorno pode ser refeito agora.");

                PesquisarCarga();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.RefazerRetorno()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        ////////////////////////////////////////////
        /// Pedidos
        ////////////////////////////////////////////

        public void CarregarPedidos(Boolean pAtual = true)
        {


            try
            {
                List<ModelLibrary.ListaPedidosRetorno> pedidos = ModelLibrary.MetodosDeposito.ObterListaPedidosRetorno(cRetornoId);

                BindingListView<ModelLibrary.ListaPedidosRetorno> view = new BindingListView<ModelLibrary.ListaPedidosRetorno>(pedidos);

                localDepositoForm.grdRetornoPedido.DataSource = view;

                localDepositoForm.grdRetornoPedido.Columns[1].Visible = false;
                localDepositoForm.grdRetornoPedido.Columns[2].Width = 450;
                localDepositoForm.grdRetornoPedido.Columns[3].DefaultCellStyle.Format = "c";
                localDepositoForm.grdRetornoPedido.Columns[5].Width = 200;


                localDepositoForm.grdRetornoPedido.Refresh();


                //Carregar Totalizadores


                double sumValorPedido = 0;
                double sumQtdPedido = localDepositoForm.grdRetornoPedido.Rows.Count;
                for (int i = 0; i < localDepositoForm.grdRetornoPedido.Rows.Count; ++i)
                {
                    sumValorPedido += Convert.ToDouble(localDepositoForm.grdRetornoPedido.Rows[i].Cells[3].Value);                    
                }


                localDepositoForm.dlbValorPedido.Text = sumValorPedido.ToString("C");
                localDepositoForm.dlbQuantidadePedido.Text = sumQtdPedido.ToString();

            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarPedidos()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        

        ////////////////////////////////////////////
        /// Pedidos Fechados
        ////////////////////////////////////////////

        public void CarregarPedidosFechados(Boolean pAtual = true)
        {

            try
            {
                List<ModelLibrary.ListaPedidosFechados> pedidos = ModelLibrary.MetodosDeposito.ObterListaPedidosFechados(cRetornoId, pAtual);

                BindingListView<ModelLibrary.ListaPedidosFechados> view = new BindingListView<ModelLibrary.ListaPedidosFechados>(pedidos);

                localDepositoForm.grdPedidosFechado.DataSource = view;

                localDepositoForm.grdPedidosFechado.Columns[1].Width = 250;
                localDepositoForm.grdPedidosFechado.Columns[2].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechado.Columns[3].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechado.Columns[4].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechado.Columns[5].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechado.Columns[6].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechado.Columns[7].DefaultCellStyle.Format = "c";

                localDepositoForm.grdPedidosFechado.Refresh();


                List<ModelLibrary.ListaPedidosFechados> totalpedidos = ModelLibrary.MetodosDeposito.ObterTotalPedidosFechados(cRetornoId, pAtual);

                BindingListView<ModelLibrary.ListaPedidosFechados> viewtotal = new BindingListView<ModelLibrary.ListaPedidosFechados>(totalpedidos);

                localDepositoForm.grdPedidosFechadoTotal.DataSource = viewtotal;

                localDepositoForm.grdPedidosFechadoTotal.Columns[1].Width = 250;
                localDepositoForm.grdPedidosFechadoTotal.Columns[2].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechadoTotal.Columns[3].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechadoTotal.Columns[4].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechadoTotal.Columns[5].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechadoTotal.Columns[6].DefaultCellStyle.Format = "c";
                localDepositoForm.grdPedidosFechadoTotal.Columns[7].DefaultCellStyle.Format = "c";

                localDepositoForm.grdPedidosFechadoTotal.Refresh();



            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarPedidosFechados()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        ////////////////////////////////////////////
        /// Contas a Receber
        ////////////////////////////////////////////

        public void CarregarContasAReceber()
        {


            try
            {
                List<ModelLibrary.ListaAReceber> pedidos = ModelLibrary.MetodosDeposito.ObterListaAReceber(cRetornoId);

                BindingListView<ModelLibrary.ListaAReceber> view = new BindingListView<ModelLibrary.ListaAReceber>(pedidos);


                localDepositoForm.grdContasAReceber.DataSource = view;

                localDepositoForm.grdContasAReceber.Columns[0].Visible = false;
                localDepositoForm.grdContasAReceber.Columns[1].Visible = false;
                localDepositoForm.grdContasAReceber.Columns[4].Width = 250;
                localDepositoForm.grdContasAReceber.Columns[5].DefaultCellStyle.Format = "c";
                localDepositoForm.grdContasAReceber.Columns[6].DefaultCellStyle.Format = "c";


                //Carregar Totalizadores


                double sumValorTotal = 0;
                double sumValorAReceber = 0;
                double sumValorPago = 0;
                for (int i = 0; i < localDepositoForm.grdContasAReceber.Rows.Count; ++i)
                {
                    sumValorTotal += Convert.ToDouble(localDepositoForm.grdContasAReceber.Rows[i].Cells[5].Value);
                    sumValorAReceber += Convert.ToDouble(localDepositoForm.grdContasAReceber.Rows[i].Cells[6].Value);
                    sumValorPago += Convert.ToDouble(localDepositoForm.grdContasAReceber.Rows[i].Cells[7].Value);
                }


                localDepositoForm.dlbTitulosTotal.Text = sumValorTotal.ToString("C");
                localDepositoForm.dlbTitulosAReceber.Text = sumValorAReceber.ToString("C");
                localDepositoForm.dlbTitulosPago.Text = sumValorPago.ToString("C");
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarContasAReceber()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }






        ////////////////////////////////////////////
        /// Produtos Conferencia
        ////////////////////////////////////////////


        public void CarregarConferenciaProdutos()
        {
            try
            {
                List<ModelLibrary.ListaProdutoConferencia> pedidos = ModelLibrary.MetodosDeposito.ObterListaProdutoConferencia(cRetornoId);

                BindingListView<ModelLibrary.ListaProdutoConferencia> view = new BindingListView<ModelLibrary.ListaProdutoConferencia>(pedidos);

                localDepositoForm.grdRetornoConfProdutos.DataSource = view;

                localDepositoForm.grdRetornoConfProdutos.Columns[1].Width = 250;
                localDepositoForm.grdRetornoConfProdutos.Columns[9].DefaultCellStyle.Format = "c";



                List<ModelLibrary.ListaProdutoConferencia> totalpedidos = ModelLibrary.MetodosDeposito.ObterTotalProdutoConferencia(cRetornoId);

                BindingListView<ModelLibrary.ListaProdutoConferencia> total = new BindingListView<ModelLibrary.ListaProdutoConferencia>(totalpedidos);

                localDepositoForm.grdRetornoTotalConfProdutos.DataSource = total;

                localDepositoForm.grdRetornoTotalConfProdutos.Columns[1].Width = 250;
                localDepositoForm.grdRetornoTotalConfProdutos.Columns[9].DefaultCellStyle.Format = "c";


            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarConferenciaProdutos()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            

        }



        ////////////////////////////////////////////
        /// Acerto
        ////////////////////////////////////////////

        public void CarregarAcerto()
        {
            try
            {
                List<ModelLibrary.ListaAcerto> acerto = ModelLibrary.MetodosDeposito.ObterListaAcerto(cRetornoId);

                BindingListView<ModelLibrary.ListaAcerto> view = new BindingListView<ModelLibrary.ListaAcerto>(acerto);

                localDepositoForm.grdAcerto.DataSource = view;

                localDepositoForm.grdAcerto.Columns[0].Width = 250;
                localDepositoForm.grdAcerto.Columns[1].DefaultCellStyle.Format = "c";
                localDepositoForm.grdAcerto.Columns[2].DefaultCellStyle.Format = "c";
                localDepositoForm.grdAcerto.Columns[3].DefaultCellStyle.Format = "c";
                localDepositoForm.grdAcerto.Columns[4].DefaultCellStyle.Format = "c";
                localDepositoForm.grdAcerto.Columns[5].DefaultCellStyle.Format = "c";
                localDepositoForm.grdAcerto.Columns[6].DefaultCellStyle.Format = "c";

                //Carregar Totalizadores


                double sumValorAcerto = 0;
                double sumValorAberto = 0;
                for (int i = 0; i < localDepositoForm.grdAcerto.Rows.Count; ++i)
                {
                    sumValorAcerto += Convert.ToDouble(localDepositoForm.grdAcerto.Rows[i].Cells[5].Value);
                    sumValorAberto += Convert.ToDouble(localDepositoForm.grdAcerto.Rows[i].Cells[6].Value);
                }


                localDepositoForm.dlbTotalAcerto.Text = sumValorAcerto.ToString("C");
                localDepositoForm.dlbTotalAberto.Text = sumValorAberto.ToString("C");
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.CarregarAcerto()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void FinalizarAcerto()
        {

            try
            {
                ModelLibrary.MetodosDeposito.AlterarStatusCarga(cRetornoId, "F");
                RetornoReload();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Retorno.FinalizarAcerto()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

    }
}
