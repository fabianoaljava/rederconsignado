using Equin.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignadoDeposito.Modal
{
    public partial class FormPedido : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;
        public Int32 cPedidoId;
        public string cPedidoCodigo;
        public Int32 cPedidoItemId;
        public Int32 cPedidoProdutoGradeId;
        public string cModoPedidoItem;

        public FormPedido(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;


        }



        ////////////////////////////////////////////
        /// Detalhes do Pedido
        ////////////////////////////////////////////

        public void ExibirDetalhesPedido(string pCodigoPedido)
        {
            try
            {
                LancamentoPedidoLimpar();



                var pedido = ModelLibrary.MetodosDeposito.ObterPedido(pCodigoPedido);

                if (pedido != null)
                {



                    cPedidoId = pedido.Id;
                    cPedidoCodigo = pedido.CodigoPedido;


                    grpPedidoDetalhe.Visible = true;

                    pnlLancPedTop.Visible = true;
                    pnlPedidoMain.Visible = true;

                    dlbCodigoPedido.Text = pedido.CodigoPedido;


                    var vendedor = ModelLibrary.MetodosDeposito.ObterVendedor(pedido.VendedorId);
                    dlbPedidoVendedor.Text = (vendedor != null) ? vendedor.Nome : "Não encontrado";


                    dlbValorPedido.Text = string.Format("{0:C2}", pedido.ValorPedido);
                    dlbValorCompra.Text = string.Format("{0:C2}", pedido.ValorCompra);
                    dlbPercentualComissao.Text = string.Format("{0}%", pedido.PercentualFaixa);
                    dlbValorComissao.Text = string.Format("{0:C2}", pedido.ValorComissao);
                    dlbValorLiquido.Text = string.Format("{0:C2}", pedido.ValorLiquido);
                    dlbRecebimentoAnterior.Text = string.Format("{0:C2}", pedido.ValorAReceber);
                    dlbValorAcerto.Text = string.Format("{0:C2}", pedido.ValorAcerto);
                    dlbValorRestante.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber - pedido.ValorAcerto);

                    dlbTotalAPagar.Text = string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber);

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


                    var carga = ModelLibrary.MetodosDeposito.ObterCargaById(pedido.CargaId);

                    if (carga != null)
                    {
                        if (carga.Status == "R" || carga.Status == "C")
                        {
                            pnlLancPedTop.Enabled = true;
                        }
                        else
                        {
                            pnlLancPedTop.Enabled = false;
                        }
                    }


                    dlbPedidoStatus.Text = pedidostatus;
                    CarregarListaLancamentoPedido();
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormPedido.ExibirDetalhesPedido()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        public void PedidoDetalheLimpar()
        {

            cPedidoId = 0;
            cPedidoCodigo = "";

            grpPedidoDetalhe.Visible = false;

            pnlLancPedTop.Visible = false;
            pnlPedidoMain.Visible = false;

            dlbCodigoPedido.Text = "";
            dlbPedidoStatus.Text = "";
            dlbPedidoVendedor.Text = "";

            LancamentoPedidoLimpar();

        }


        public void LancamentoPedidoLimpar()
        {
            cPedidoId = 0;
            grpPedidoDetalhe.Visible = false;
            pnlLancPedTop.Visible = false;
            pnlPedidoMain.Visible = false;
            grdLancPedido.DataSource = null;
            grdLancPedido.Refresh();
            LancamentoPedidoItemLimpar();
        }

        public void LancamentoPedidoItemLimpar()
        {
            txtLancPedCodigoBarras.Text = "";
            txtLancPedProduto.Text = "";
            txtLancPedQuantidade.Text = "";
            txtLancPedQtdRetorno.Text = "";
            txtLancPedPreco.Text = "";

            btnLancPedConfirmar.Enabled = false;
            btnLancPedCancelar.Enabled = false;



        }


        public void CarregarListaLancamentoPedido()
        {

            try
            {
                List<ModelLibrary.ListaPedidoItem> pedidos = ModelLibrary.MetodosDeposito.ObterListaPedidoItem(cPedidoId);

                BindingListView<ModelLibrary.ListaPedidoItem> view = new BindingListView<ModelLibrary.ListaPedidoItem>(pedidos);

                grdLancPedido.DataSource = view;

                grdLancPedido.Columns[0].Visible = false;
                grdLancPedido.Columns[2].Width = 400;
                grdLancPedido.Columns[5].DefaultCellStyle.Format = "c";
                grdLancPedido.Columns[6].Visible = false;
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormPedido.CarregarListaLancamentoPedido()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }


        public void LancamentoPedidoPesquisar(string pCodigo)
        {
            try
            {
                int rowIndex = -1;

                DataGridViewRow pedidoitem = grdLancPedido.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["CodigoBarras"].Value.ToString().Equals(pCodigo) || r.Cells["ProdutoGradeId"].Value.ToString().Equals(pCodigo))
                    .FirstOrDefault();


                if (pedidoitem != null)
                {

                    rowIndex = pedidoitem.Index;

                    txtLancPedProduto.Text = grdLancPedido.Rows[rowIndex].Cells["NomeProduto"].Value.ToString();
                    txtLancPedQuantidade.Text = grdLancPedido.Rows[rowIndex].Cells["Quantidade"].Value.ToString();
                    txtLancPedQtdRetorno.Text = grdLancPedido.Rows[rowIndex].Cells["Retorno"].Value.ToString();
                    txtLancPedPreco.Text = grdLancPedido.Rows[rowIndex].Cells["Preco"].Value.ToString();


                    if (txtLancPedCodigoBarras.Text != grdLancPedido.Rows[rowIndex].Cells["CodigoBarras"].Value.ToString())
                    {
                        txtLancPedCodigoBarras.Text = grdLancPedido.Rows[rowIndex].Cells["CodigoBarras"].Value.ToString();
                        if (chkLancPedQuantidade.Checked == false)
                        {
                            chkLancPedQuantidade.Checked = true;
                            txtLancPedQuantidade.Enabled = true;
                        }
                    }


                    cPedidoItemId = Convert.ToInt32(grdLancPedido.Rows[rowIndex].Cells["PedidoItemId"].Value.ToString());
                    cModoPedidoItem = "Edit";

                    btnLancPedConfirmar.Enabled = true;
                    btnLancPedCancelar.Enabled = true;

                    if (chkLancPedQuantidade.Checked)
                    {
                        txtLancPedQuantidade.Focus();

                    }
                    else
                    {
                        //inserir direto qtd=1

                        int tempQtd = grdLancPedido.Rows[rowIndex].Cells["Quantidade"].Value != null ? Convert.ToInt32(grdLancPedido.Rows[rowIndex].Cells["Quantidade"].Value) : 0;
                        txtLancPedQuantidade.Text = (tempQtd + 1).ToString();
                        SalvarLancamentoPedido();
                    }

                }
                else
                {


                    /// pesquisar no BD
                    /// 
                    var produtograde = ModelLibrary.MetodosDeposito.ObterProdutoGrade(txtLancPedCodigoBarras.Text);

                    if (produtograde != null)
                    {
                        /// se existir -- carregar com ação de incluir

                        var produto = ModelLibrary.MetodosDeposito.ObterProduto(produtograde.CodigoBarras);

                        if (produtograde.Status != "1" || produto.Status != "1")
                        {

                            MessageBox.Show("Este produto foi excluído e não pode ser inserido no pedido.");

                        }
                        else
                        {
                            txtLancPedProduto.Text = produto.Descricao;
                            txtLancPedQuantidade.Text = "";
                            txtLancPedQtdRetorno.Text = "";
                            txtLancPedPreco.Text = produtograde.ValorSaida.ToString();

                            txtLancPedQuantidade.Focus();
                            cPedidoProdutoGradeId = produtograde.Id;
                            cModoPedidoItem = "Insert";

                            btnLancPedConfirmar.Enabled = true;
                            btnLancPedCancelar.Enabled = true;
                        }


                    }
                    else
                    {
                        /// se não - exibir mensagem de erro
                        MessageBox.Show("Dígito verificador inválido. Não foi possível encontrar a grade deste produto.");

                        cPedidoItemId = 0;
                        txtLancPedCodigoBarras.Focus();
                        btnLancPedConfirmar.Enabled = false;
                        btnLancPedCancelar.Enabled = false;
                    }

                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormPedido.LancamentoPedidoPesquisar()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void SalvarLancamentoPedido()
        {
            try
            {
                if (cModoPedidoItem == "Edit")
                {

                    ModelLibrary.MetodosDeposito.AlterarPedidoItem(cPedidoItemId, Convert.ToDouble(txtLancPedQuantidade.Text), Convert.ToDouble(txtLancPedQtdRetorno.Text), Convert.ToDouble(txtLancPedPreco.Text));

                }
                else
                {
                    if (txtLancPedQuantidade.Text == "") txtLancPedQuantidade.Text = "0";
                    if (txtLancPedQtdRetorno.Text == "") txtLancPedQtdRetorno.Text = "0";
                    ModelLibrary.MetodosDeposito.InserirPedidoItem(cPedidoId, cPedidoProdutoGradeId, Convert.ToDouble(txtLancPedQuantidade.Text), Convert.ToDouble(txtLancPedQtdRetorno.Text), Convert.ToDouble(txtLancPedPreco.Text));

                }

                CarregarListaLancamentoPedido();
                LancamentoPedidoItemLimpar();
                LancamentoPedidoReload();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormPedido.SalvarLancamentoPedido()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void ExcluirLancamentoPedido(int pPedidoItemId)
        {
            try
            {
                ModelLibrary.MetodosDeposito.ExcluirPedidoItem(pPedidoItemId, cPedidoId);

                CarregarListaLancamentoPedido();
                LancamentoPedidoItemLimpar();
                LancamentoPedidoReload();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormPedido.ExcluirLancamentoPedido()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void LancamentoPedidoReload()
        {
            try
            {
                localDepositoForm.cRetorno.CarregarPedidos();
                ExibirDetalhesPedido(cPedidoCodigo);
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormPedido.LancamentoPedidoReload()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public void ControlOnlyInt(object sender, KeyPressEventArgs e)
        {


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }

        private void txtLancPedCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtLancPedCodigoBarras.Text != "")
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    //txtLancPedCodigoBarras_Leave(sender, e);
                    SendKeys.Send("{TAB}");

                }
            }

        }


        private void btnLancPedConfirmar_Click(object sender, EventArgs e)
        {
            SalvarLancamentoPedido();
        }

        private void btnLancPedCancelar_Click(object sender, EventArgs e)
        {
            LancamentoPedidoItemLimpar();
        }


        private void grdLancPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LancamentoPedidoPesquisar(grdLancPedido.CurrentRow.Cells[1].Value.ToString());
        }


        private void txtLancPedCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            if (txtLancPedCodigoBarras.Text != "")
            {
                LancamentoPedidoPesquisar(txtLancPedCodigoBarras.Text);

            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPedido_Load(object sender, EventArgs e)
        {

        }
    }
}
