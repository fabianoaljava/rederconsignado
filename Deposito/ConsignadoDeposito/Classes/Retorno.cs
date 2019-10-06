using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoDeposito;
using System.Windows.Forms;
using Equin.ApplicationFramework;

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
            localDepositoForm.cbbRetornoPraca.DataSource = ModelLibrary.MetodosDeposito.ObterListaPracas();
            localDepositoForm.cbbRetornoPraca.DisplayMember = "Descricao";
            localDepositoForm.cbbRetornoPraca.ValueMember = "Id";
            localDepositoForm.cbbRetornoPraca.Invalidate();
            localDepositoForm.cbbRetornoPraca.SelectedIndex = -1;
        }

        public void CarregarListaRepresentante()
        {
            localDepositoForm.cbbRetornoRepresentante.DataSource = ModelLibrary.MetodosDeposito.ObterListaRepresentantes();
            localDepositoForm.cbbRetornoRepresentante.DisplayMember = "Nome";
            localDepositoForm.cbbRetornoRepresentante.ValueMember = "Id";
            localDepositoForm.cbbRetornoRepresentante.Invalidate();
            localDepositoForm.cbbRetornoRepresentante.SelectedIndex = -1;
        }


        public void LimparRetorno()
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
        }

        public void ResetarVariaveis()
        {
            cRetornoId = 0;
            cRetornoProdutoGradeId = 0;
            cRetornoId = 0;
        }


        public void RetornoReload()
        {
            Cursor.Current = Cursors.WaitCursor;
            PesquisarCarga();
            CarregarResumo();
            CarregarGradeRetornoProduto(cRetornoId);
            CarregarPedidos();
            PedidoDetalheLimpar();
            CarregarPedidosFechados();
            CarregarContasAReceber();
            CarregarConferenciaProdutos();
            CarregarAcerto();
            Cursor.Current = Cursors.Default;
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

                    var totalizadores = ModelLibrary.MetodosDeposito.ObterTotalizadores(cRetornoId);

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
                        localDepositoForm.pnlLancPedTop.Enabled = true;
                        localDepositoForm.pnlContasAReceberTop.Enabled = true;

                        localDepositoForm.btnAcoes.Text = "Finalizar \n Conferencia \n de Produtos";
                        localDepositoForm.btnAcoes.Enabled = true;

                    }
                    else if (carga.Status == "C") {
                        /// Desabilita Retorno de Produtos
                        /// Desabilita Finalizar Conferencia de Produtos
                        /// Habilita Lançamento de Pedidos
                        /// Habilita Acerto
                        /// Habilita Finalizar Acerto
                        localDepositoForm.pnlRetornoProduto.Enabled = false;
                        localDepositoForm.pnlRetornoPedidoTop.Enabled = true;
                        localDepositoForm.pnlLancPedTop.Enabled = true;
                        localDepositoForm.pnlContasAReceberTop.Enabled = true;
                        localDepositoForm.btnAcoes.Text = "Finalizar \n Acerto";
                        localDepositoForm.btnAcoes.Enabled = true;
                    }
                    else
                    {
                        /// Desabilita Retorno de Produtos
                        /// Desabilita FInalizar Conferencia de Produtos
                        /// Desabilita Lançamento de Pedidos
                        /// Desabilita Acerto
                        /// Desabilita Finalizar Acerto
                        localDepositoForm.pnlRetornoProduto.Enabled = false;
                        localDepositoForm.pnlRetornoPedidoTop.Enabled = false;
                        localDepositoForm.pnlLancPedTop.Enabled = false;
                        localDepositoForm.pnlContasAReceberTop.Enabled = false;


                        localDepositoForm.btnAcoes.Text = "Refazer \n Retorno";
                        localDepositoForm.btnAcoes.Enabled = true;
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
            }
        }


        public void CarregarFormulario()
        {

            

            Cursor.Current = Cursors.WaitCursor;
            localDepositoForm.tbcRetorno.SelectedTab = localDepositoForm.tabRetornoProdutos;
            CarregarResumo();
            CarregarGradeRetornoProduto(cRetornoId);
            CarregarPedidos();
            PedidoDetalheLimpar();
            CarregarPedidosFechados();            
            CarregarContasAReceber();
            CarregarConferenciaProdutos();
            CarregarAcerto();
            Cursor.Current = Cursors.Default;

            localDepositoForm.tbcRetorno.Visible = true;



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



        void CarregarGradeRetornoProduto(int pCargaId)
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


        public void LimparGradeRetornoProduto()
        {
            localDepositoForm.grdRetornoProduto.DataSource = null;
            localDepositoForm.tbcRetorno.Visible = false;

        }


        public void PesquisarRetornoProduto(string pCodigo)
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
                    vProdutoGradeId = produtosgrade.FirstOrDefault().Id;
                    ExibirProdutoGrade(vProdutoGradeId);
                }
            }
            else
            {
                vProdutoGradeId = 0;
                ExibirProdutoGrade(vProdutoGradeId);
            }
            

        }


        public void ExibirProdutoGrade(long pProdutoGradeId)
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

        public void ExibirRetornoProdutoGrade()
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






        public void AlterarRetornoProdutoGrade()
        {

            /*try
            {*/

            ModelLibrary.MetodosDeposito.AlterarRetornoProduto(cRetornoId, cRetornoProdutoGradeId, Convert.ToDouble(localDepositoForm.txtRetornoQuantidade.Text));

            CarregarGradeRetornoProduto(cRetornoId);

            LimparRetornoProduto();

            RetornoReload();

            /*} catch
            {

                MessageBox.Show("Não foi possível alterar o produto. Por favor, verifique os dados digitados e tente novamente");

            }*/


        }


        public void ConfirmarRetornoProdutoGrade()
        {

            AlterarRetornoProdutoGrade();

        }


        public void FinalizarRetorno()
        {
            ModelLibrary.MetodosDeposito.AlterarStatusCarga(cRetornoId, "C");

            MessageBox.Show("Retorno de Produtos Finalizado com Sucesso!");

            PesquisarCarga();
        }


        public void RefazerRetorno()
        {
            ModelLibrary.MetodosDeposito.RefazerRetorno(cRetornoId);

            MessageBox.Show("O retorno pode ser refeito agora.");

            PesquisarCarga();
        }



        ////////////////////////////////////////////
        /// Pedidos
        ////////////////////////////////////////////

        public void CarregarPedidos(Boolean pAtual = true)
        {



            List<ModelLibrary.ListaPedidosRetorno> pedidos = ModelLibrary.MetodosDeposito.ObterListaPedidosRetorno(cRetornoId);

            BindingListView<ModelLibrary.ListaPedidosRetorno> view = new BindingListView<ModelLibrary.ListaPedidosRetorno>(pedidos);

            localDepositoForm.grdRetornoPedido.DataSource = view;

            localDepositoForm.grdRetornoPedido.Columns[1].Visible = false;
            localDepositoForm.grdRetornoPedido.Columns[2].Width = 450;
            localDepositoForm.grdRetornoPedido.Columns[3].DefaultCellStyle.Format = "c";


            localDepositoForm.grdRetornoPedido.Refresh();

            


        }

        ////////////////////////////////////////////
        /// Detalhes do Pedido
        ////////////////////////////////////////////

        public void ExibirDetalhesPedido(string pCodigoPedido)
        {

            LancamentoPedidoLimpar();



            var pedido = ModelLibrary.MetodosDeposito.ObterPedido(pCodigoPedido);

            if (pedido != null)
            {



                cRetornoPedidoId = pedido.Id;
                cRetornoPedidoCodigo = pedido.CodigoPedido;


                localDepositoForm.grpPedidoDetalhe.Visible = true;
                
                localDepositoForm.pnlLancPedTop.Visible = true;
                localDepositoForm.pnlLancPedMain.Visible = true;

                localDepositoForm.dlbCodigoPedido.Text = pedido.CodigoPedido;


                var vendedor = ModelLibrary.MetodosDeposito.ObterVendedor(pedido.VendedorId);
                localDepositoForm.dlbPedidoVendedor.Text = (vendedor != null)?vendedor.Nome:"Não encontrado";


                localDepositoForm.dlbValorPedido.Text = string.Format("{0:C2}", pedido.ValorPedido);
                localDepositoForm.dlbValorCompra.Text = string.Format("{0:C2}", pedido.ValorCompra);
                localDepositoForm.dlbPercentualComissao.Text = string.Format("{0}%", pedido.PercentualFaixa);
                localDepositoForm.dlbValorComissao.Text = string.Format("{0:C2}", pedido.ValorComissao);
                localDepositoForm.dlbValorLiquido.Text = string.Format("{0:C2}", pedido.ValorLiquido);
                localDepositoForm.dlbRecebimentoAnterior.Text = string.Format("{0:C2}", pedido.ValorAReceber);
                localDepositoForm.dlbValorAcerto.Text = string.Format("{0:C2}", pedido.ValorAcerto);
                localDepositoForm.dlbValorRestante.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber - pedido.ValorAcerto);

                localDepositoForm.dlbTotalAPagar.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber);

                string pedidostatus = "";

                switch (pedido.Status)
                {
                    case "0":
                        pedidostatus = "Aberto";
                        break;
                    case "1":
                        pedidostatus = "Aguardando retorno";
                        break;
                    case "2":
                        pedidostatus = "Retornado";
                        break;
                    case "3":
                        pedidostatus = "Acerto realizado";
                        break;
                    case "4":
                        pedidostatus = "Fechado";
                        break;
                }



                localDepositoForm.dlbPedidoStatus.Text = pedidostatus;
                CarregarListaLancamentoPedido();
            }
        }


        public void PedidoDetalheLimpar()
        {

            cRetornoPedidoId = 0;
            cRetornoPedidoCodigo = "";

            localDepositoForm.grpPedidoDetalhe.Visible = false;

            localDepositoForm.pnlLancPedTop.Visible = false;
            localDepositoForm.pnlLancPedMain.Visible = false;

            localDepositoForm.dlbCodigoPedido.Text = "";
            localDepositoForm.dlbPedidoStatus.Text = "";
            localDepositoForm.dlbPedidoVendedor.Text = "";

            LancamentoPedidoLimpar();

        }
        /*
        public void CarregarListaPesquisaVendedor()
        {

            localDepositoForm.cbbPesqVendedor.DataSource = ModelLibrary.MetodosDeposito.ObterListaVendedor(cRetornoId);
            localDepositoForm.cbbPesqVendedor.DisplayMember = "Nome";
            localDepositoForm.cbbPesqVendedor.ValueMember = "Id";
            localDepositoForm.cbbPesqVendedor.Invalidate();
            localDepositoForm.cbbPesqVendedor.SelectedIndex = -1;

            localDepositoForm.cbbPesqVendedor.SelectedIndexChanged += PesquisaVendedor_Change;

        }


        public void VendedorLimpar()
        {
            localDepositoForm.txtPesqVendedorCpfCnpj.Text = "";
            localDepositoForm.cbbPesqVendedor.SelectedIndex = -1;
            localDepositoForm.lblPesqVendedor.Visible = false;

            cRetornoVendedorId = 0;

            LancamentoPedidoLimpar();
        }

        public void PesquisaVendedor_Change(object sender, EventArgs e)
        {
            if (localDepositoForm.cbbPesqVendedor.SelectedIndex >= 0)
            {
                
                ModelLibrary.Vendedor vendedor = (ModelLibrary.Vendedor)localDepositoForm.cbbPesqVendedor.SelectedItem;

                localDepositoForm.txtPesqVendedorCpfCnpj.Text = vendedor.CpfCnpj;

                cRetornoVendedorId = vendedor.Id;

                VendedorExibir(vendedor.Id);

            }

        }

        public void VendedorPesquisar()
        {

            var vendedor = ModelLibrary.MetodosDeposito.PesquisarVendedor(localDepositoForm.txtPesqVendedorCpfCnpj.Text);

            if (vendedor != null)
            {
                localDepositoForm.cbbPesqVendedor.SelectedIndex = localDepositoForm.cbbPesqVendedor.FindString(vendedor.Nome);
            }
            else
            {
                localDepositoForm.lblPesqVendedor.Visible = true;
                localDepositoForm.lblPesqVendedor.Text = "Vendedor não encontrado.";
                LancamentoPedidoLimpar();
            }
        }

        public void VendedorExibir(long pVendedorId)
        {

            LancamentoPedidoLimpar();

            var vendedor = ModelLibrary.MetodosDeposito.ObterVendedor(pVendedorId);


            if (vendedor != null)
            {
                var pedido = ModelLibrary.MetodosDeposito.ObterVendedorPedido(vendedor.Id, cRetornoId);

                Console.WriteLine("Pesquisando Pedido do Vendedor: " + vendedor.Id.ToString() + " referente a carga: " + cRetornoId.ToString());

                

                if (pedido != null)
                {

                    cRetornoPedidoId = pedido.Id;

                    localDepositoForm.lblPesqVendedor.Visible = false;

                    localDepositoForm.grpPesqVendedorPedido.Visible = true;
                    localDepositoForm.pnlLancPedTop.Visible = true;
                    localDepositoForm.pnlLancPedMain.Visible = true;
                                       

                    localDepositoForm.dlbValorPedido.Text = string.Format("{0:C2}", pedido.ValorPedido);
                    localDepositoForm.dlbValorCompra.Text = string.Format("{0:C2}", pedido.ValorCompra);
                    localDepositoForm.dlbPercentualComissao.Text = string.Format("{0}%", pedido.PercentualFaixa);
                    localDepositoForm.dlbValorComissao.Text = string.Format("{0:C2}", pedido.ValorComissao);
                    localDepositoForm.dlbValorLiquido.Text = string.Format("{0:C2}", pedido.ValorLiquido);
                    localDepositoForm.dlbRecebimentoAnterior.Text = string.Format("{0:C2}", pedido.ValorAReceber);
                    localDepositoForm.dlbValorAcerto.Text = string.Format("{0:C2}", pedido.ValorAcerto);
                    localDepositoForm.dlbValorRestante.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber - pedido.ValorAcerto);

                    localDepositoForm.dlbTotalAPagar.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber);

                    string pedidostatus ="";

                    switch (pedido.Status)
                    {
                        case "0":
                            pedidostatus = "Aberto";
                            break;
                        case "1":
                            pedidostatus = "Aguardando retorno";
                            break;
                        case "2":
                            pedidostatus = "Retornado";
                            break;
                        case "3":
                            pedidostatus = "Acerto realizado";
                            break;
                        case "4":
                            pedidostatus = "Fechado";
                            break;
                    }
                        


                    localDepositoForm.dlbPedidoStatus.Text = pedidostatus;
                    CarregarListaLancamentoPedido();


                }

                else
                {
                    localDepositoForm.lblPesqVendedor.Visible = true;
                    localDepositoForm.lblPesqVendedor.Text = "Vendedor não possui pedidos associados a esta carga.";
                    LancamentoPedidoLimpar();
                }
            }
            else
            {
                localDepositoForm.lblPesqVendedor.Visible = true;
                localDepositoForm.lblPesqVendedor.Text = "Vendedor não encontrado.";
                LancamentoPedidoLimpar();
            }

        }

        */

        public void LancamentoPedidoLimpar()
        {
            cRetornoPedidoId = 0;
            localDepositoForm.grpPedidoDetalhe.Visible = false;
            localDepositoForm.pnlLancPedTop.Visible = false;
            localDepositoForm.pnlLancPedMain.Visible = false;
            localDepositoForm.grdLancPedido.DataSource = null;
            localDepositoForm.grdLancPedido.Refresh();
            LancamentoPedidoItemLimpar();
        }

        public void LancamentoPedidoItemLimpar()
        {
            localDepositoForm.txtLancPedCodigoBarras.Text = "";
            localDepositoForm.txtLancPedProduto.Text = "";
            localDepositoForm.txtLancPedQuantidade.Text = "";
            localDepositoForm.txtLancPedQtdRetorno.Text = "";
            localDepositoForm.txtLancPedPreco.Text = "";

            localDepositoForm.btnLancPedConfirmar.Enabled = false;
            localDepositoForm.btnLancPedCancelar.Enabled = false;



        }


        public void CarregarListaLancamentoPedido()
        {


            List<ModelLibrary.ListaPedidoItem> pedidos = ModelLibrary.MetodosDeposito.ObterListaPedidoItem(cRetornoPedidoId);

            BindingListView<ModelLibrary.ListaPedidoItem> view = new BindingListView<ModelLibrary.ListaPedidoItem>(pedidos);

            localDepositoForm.grdLancPedido.DataSource = view;

            localDepositoForm.grdLancPedido.Columns[0].Visible = false;
            localDepositoForm.grdLancPedido.Columns[2].Width = 450;
            localDepositoForm.grdLancPedido.Columns[5].DefaultCellStyle.Format = "c";
            localDepositoForm.grdLancPedido.Columns[6].Visible = false;

        }


        public void LancamentoPedidoPesquisar(string pCodigo)
        {


            int rowIndex = -1;

            DataGridViewRow pedidoitem = localDepositoForm.grdLancPedido.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["CodigoBarras"].Value.ToString().Equals(pCodigo) || r.Cells["ProdutoGradeId"].Value.ToString().Equals(pCodigo))
                .FirstOrDefault();


            if (pedidoitem != null)
            {

                rowIndex = pedidoitem.Index;

                localDepositoForm.txtLancPedProduto.Text = localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["NomeProduto"].Value.ToString();
                localDepositoForm.txtLancPedQuantidade.Text = localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["Quantidade"].Value.ToString();
                localDepositoForm.txtLancPedQtdRetorno.Text = localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["Retorno"].Value.ToString();
                localDepositoForm.txtLancPedPreco.Text = localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["Preco"].Value.ToString();


                if (localDepositoForm.txtLancPedCodigoBarras.Text != localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["CodigoBarras"].Value.ToString())
                {
                    localDepositoForm.txtLancPedCodigoBarras.Text = localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["CodigoBarras"].Value.ToString();
                    if (localDepositoForm.chkLancPedQuantidade.Checked == false)
                    {
                        localDepositoForm.chkLancPedQuantidade.Checked = true;
                        localDepositoForm.txtLancPedQuantidade.Enabled = true;
                    }
                }


                cRetornoPedidoItemId = Convert.ToInt32(localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["PedidoItemId"].Value.ToString());
                cModoRetornoPedidoItem = "Edit";

                localDepositoForm.btnLancPedConfirmar.Enabled = true;
                localDepositoForm.btnLancPedCancelar.Enabled = true;

                if (localDepositoForm.chkLancPedQuantidade.Checked)
                {
                    localDepositoForm.txtLancPedQuantidade.Focus();

                }
                else
                {
                    //inserir direto qtd=1

                    int tempQtd = localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["Quantidade"].Value != null ? Convert.ToInt32(localDepositoForm.grdLancPedido.Rows[rowIndex].Cells["Quantidade"].Value) : 0;
                    localDepositoForm.txtLancPedQuantidade.Text = (tempQtd + 1).ToString();
                    SalvarLancamentoPedido();
                }

            }
            else
            {


                /// pesquisar no BD
                /// 
                var produtograde = ModelLibrary.MetodosDeposito.ObterProdutoGrade(localDepositoForm.txtLancPedCodigoBarras.Text);

                if (produtograde != null)
                {
                    /// se existir -- carregar com ação de incluir

                    var produto = ModelLibrary.MetodosDeposito.ObterProduto(produtograde.CodigoBarras);

                    if (produtograde.Status != "1" || produto.Status != "1")
                    {

                        MessageBox.Show("Este produto foi excluído e não pode ser inserido no pedido.");

                    } else
                    {
                        localDepositoForm.txtLancPedProduto.Text = produto.Descricao;
                        localDepositoForm.txtLancPedQuantidade.Text = "";
                        localDepositoForm.txtLancPedQtdRetorno.Text = "";
                        localDepositoForm.txtLancPedPreco.Text = produtograde.ValorSaida.ToString();

                        localDepositoForm.txtLancPedQuantidade.Focus();
                        cRetornoPedidoProdutoGradeId = produtograde.Id;
                        cModoRetornoPedidoItem = "Insert";

                        localDepositoForm.btnLancPedConfirmar.Enabled = true;
                        localDepositoForm.btnLancPedCancelar.Enabled = true;
                    }


                }
                else
                {
                    /// se não - exibir mensagem de erro
                    MessageBox.Show("Dígito verificador inválido. Não foi possível encontrar a grade deste produto.");

                    cRetornoPedidoItemId = 0;
                    localDepositoForm.txtLancPedCodigoBarras.Focus();
                    localDepositoForm.btnLancPedConfirmar.Enabled = false;
                    localDepositoForm.btnLancPedCancelar.Enabled = false;
                }

            }

        }


        public void SalvarLancamentoPedido()
        {
            if (cModoRetornoPedidoItem == "Edit")
            {

                ModelLibrary.MetodosDeposito.AlterarPedidoItem(cRetornoPedidoItemId, Convert.ToDouble(localDepositoForm.txtLancPedQuantidade.Text), Convert.ToDouble(localDepositoForm.txtLancPedQtdRetorno.Text), Convert.ToDouble(localDepositoForm.txtLancPedPreco.Text));

            } else
            {
                if (localDepositoForm.txtLancPedQuantidade.Text == "") localDepositoForm.txtLancPedQuantidade.Text = "0";
                if (localDepositoForm.txtLancPedQtdRetorno.Text == "") localDepositoForm.txtLancPedQtdRetorno.Text = "0";
                ModelLibrary.MetodosDeposito.InserirPedidoItem(cRetornoPedidoId, cRetornoPedidoProdutoGradeId, Convert.ToDouble(localDepositoForm.txtLancPedQuantidade.Text), Convert.ToDouble(localDepositoForm.txtLancPedQtdRetorno.Text), Convert.ToDouble(localDepositoForm.txtLancPedPreco.Text));

            }

            CarregarListaLancamentoPedido();
            LancamentoPedidoItemLimpar();
            LancamentoPedidoReload();

        }


        public void ExcluirLancamentoPedido(int pPedidoItemId)
        {

            ModelLibrary.MetodosDeposito.ExcluirPedidoItem(pPedidoItemId, cRetornoPedidoId);

            CarregarListaLancamentoPedido();
            LancamentoPedidoItemLimpar();
            LancamentoPedidoReload();
        }

        public void LancamentoPedidoReload()
        {
            CarregarPedidos();
            ExibirDetalhesPedido(cRetornoPedidoCodigo);            
        }

        ////////////////////////////////////////////
        /// Pedidos Fechados
        ////////////////////////////////////////////

        public void CarregarPedidosFechados(Boolean pAtual = true)
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




        }

        ////////////////////////////////////////////
        /// Contas a Receber
        ////////////////////////////////////////////

        public void CarregarContasAReceber()
        {

            List<ModelLibrary.ListaAReceber> pedidos = ModelLibrary.MetodosDeposito.ObterListaAReceber(cRetornoId);

            BindingListView<ModelLibrary.ListaAReceber> view = new BindingListView<ModelLibrary.ListaAReceber>(pedidos);


            localDepositoForm.grdContasAReceber.DataSource = view;

            localDepositoForm.grdContasAReceber.Columns[0].Visible = false;
            localDepositoForm.grdContasAReceber.Columns[1].Visible = false;
            localDepositoForm.grdContasAReceber.Columns[4].Width = 250;
            localDepositoForm.grdContasAReceber.Columns[5].DefaultCellStyle.Format = "c";
            localDepositoForm.grdContasAReceber.Columns[6].DefaultCellStyle.Format = "c";


        }


        public void LimparAReceber()
        {
            localDepositoForm.txtRetornoRecDocumento.Text = "";
            localDepositoForm.txtRetornoRecDocumento.ReadOnly = false;
            localDepositoForm.txtRetornoRecSerie.Text = "";
            localDepositoForm.txtRetornoRecSerie.ReadOnly = false;


            localDepositoForm.txtRetornoRecNome.Text = "";
            localDepositoForm.txtRetornoRecValor.Text = "";
            localDepositoForm.txtRetornoRecData.Text = DateTime.Now.ToString();


            cModoRetornoReceber = null;
            cRetornoReceberBaixaId = 0;

            localDepositoForm.btnRetornoRecConfirmar.Enabled = false;
            localDepositoForm.btnRetornoRecCancelar.Enabled = false;

        }


        public void PesquisarAReceber(string pDocumento, string pSerie) 
        {


            int rowIndex = -1;

            DataGridViewRow receber = localDepositoForm.grdContasAReceber.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["Documento"].Value.ToString().Equals(pDocumento) && r.Cells["Serie"].Value.ToString().Equals(pSerie))
                .FirstOrDefault();




            if (receber != null)
            {

                rowIndex = receber.Index;

                ExibirAReceber(rowIndex);
            }
            else
            {

                MessageBox.Show("Documento ou série não encontrados. Por favor, verifique os dados digitados");

                cRetornoProdutoGradeId = 0;
                localDepositoForm.txtRetornoRecDocumento.Focus();

                localDepositoForm.btnRetornoRecConfirmar.Enabled = false;
                localDepositoForm.btnRetornoRecCancelar.Enabled = false;


            }


        }

        public void ExibirAReceber(int rowindex = -1)
        {

            
            if (rowindex == -1)
            {
                rowindex = localDepositoForm.grdContasAReceber.CurrentRow.Index;
            }


            cRetornoReceberId = Convert.ToInt32(localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["Id"].Value);

            localDepositoForm.txtRetornoRecDocumento.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["Documento"].Value.ToString();
            localDepositoForm.txtRetornoRecDocumento.ReadOnly = true;

            localDepositoForm.txtRetornoRecSerie.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["Serie"].Value.ToString();
            localDepositoForm.txtRetornoRecSerie.ReadOnly = true;


            localDepositoForm.txtRetornoRecNome.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["Nome"].Value.ToString();

            if (localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["ValorPago"].Value == null) {

                localDepositoForm.txtRetornoRecAReceber.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["ValorAReceber"].Value.ToString();
                localDepositoForm.txtRetornoRecValor.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["ValorAReceber"].Value.ToString();
                localDepositoForm.txtRetornoRecData.Text = DateTime.Now.ToString();

                cRetornoReceberBaixaId = 0;

                cModoRetornoReceber = "Insert";

            } else
            {

                localDepositoForm.txtRetornoRecAReceber.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["ValorAReceber"].Value.ToString();
                localDepositoForm.txtRetornoRecValor.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["ValorPago"].Value.ToString();
                localDepositoForm.txtRetornoRecData.Text = localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["DataPagamento"].Value.ToString();
                cRetornoReceberBaixaId = Convert.ToInt32(localDepositoForm.grdContasAReceber.Rows[rowindex].Cells["ReceberBaixaId"].Value);
                cModoRetornoReceber = "Edit";

            }
            

            localDepositoForm.btnRetornoRecConfirmar.Enabled = true;
            localDepositoForm.btnRetornoRecCancelar.Enabled = true;


        }


        public void ConfirmarAReceber()
        {

            Double vValor, vValorAReceber;
            try
            {
                vValor = Convert.ToDouble(localDepositoForm.txtRetornoRecValor.Text);
                vValorAReceber = Convert.ToDouble(localDepositoForm.txtRetornoRecAReceber.Text);
            } catch
            {
                vValor = 0;
                vValorAReceber = 0;
            }


            if (vValor > vValorAReceber)
            {
                MessageBox.Show("O valor recebido informado está acima do valor a receber. Verifique os dados digitados.");
                localDepositoForm.txtRetornoRecValor.Focus();
            } else
            {
                ModelLibrary.MetodosDeposito.SalvarAReceberBaixa(cRetornoReceberId, cRetornoReceberBaixaId, Convert.ToDouble(localDepositoForm.txtRetornoRecValor.Text), localDepositoForm.txtRetornoRecData.Text);

                CarregarContasAReceber();

                LimparAReceber();
            }



        }



        ////////////////////////////////////////////
        /// Produtos Conferencia
        ////////////////////////////////////////////


        public void CarregarConferenciaProdutos()
        {

            List<ModelLibrary.ListaProdutoConferencia> pedidos = ModelLibrary.MetodosDeposito.ObterListaProdutoConferencia(cRetornoId);

            BindingListView<ModelLibrary.ListaProdutoConferencia> view = new BindingListView<ModelLibrary.ListaProdutoConferencia>(pedidos);

            localDepositoForm.grdRetornoConfProdutos.DataSource = view;

            localDepositoForm.grdRetornoConfProdutos.Columns[1].Width = 250;
            localDepositoForm.grdRetornoConfProdutos.Columns[9].DefaultCellStyle.Format = "c";

        }



        ////////////////////////////////////////////
        /// Acerto
        ////////////////////////////////////////////

        public void CarregarAcerto()
        {



        }

        public void FinalizarAcerto()
        {

            ModelLibrary.MetodosDeposito.AlterarStatusCarga(cRetornoId, "F");
            RetornoReload();

            
        }

    }
}
