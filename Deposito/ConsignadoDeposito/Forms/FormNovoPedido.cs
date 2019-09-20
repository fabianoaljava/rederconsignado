using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignadoDeposito.Forms
{
    public partial class FormNovoPedido : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;

        public Retorno cRetorno;
        public int cVendedorId;

        public FormNovoPedido(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;

            cRetorno = new Retorno(formDeposito);
        }



        public void CarregarListaVendedor()
        {

            cbbVendedor.DataSource = ModelLibrary.MetodosDeposito.ObterListaVendedor();            
            cbbVendedor.DisplayMember = "Nome";
            cbbVendedor.ValueMember = "Id";
            cbbVendedor.Invalidate();
            cbbVendedor.SelectedIndex = -1;

            cbbVendedor.SelectedIndexChanged += Vendedor_Change;

        }



        public void Vendedor_Change(object sender, EventArgs e)
        {
            if (cbbVendedor.SelectedIndex >= 0)
            {

                btnIncluir.Enabled = false;
                cVendedorId = 0;
                txtResultado.Text = "Selecione o vendedor...";

                ModelLibrary.Vendedor vendedor = (ModelLibrary.Vendedor)cbbVendedor.SelectedItem;

                if (vendedor != null)
                {

                    


                    //verificar se vendedor possui pedidos fechados 

                    // negativado
                    if (vendedor.Status == "N")
                    {
                        txtResultado.Text = "Este vendedor está negativado";
                    }
                    else // pedidos anteriores em aberto
                    {

                        ModelLibrary.Pedido pedidosanteriores = ModelLibrary.MetodosDeposito.ObterPedidosAbertoVendedor(vendedor.Id);

                        if (pedidosanteriores != null)
                        {
                            txtResultado.Text = "O vendedor possui pedido anterior em aberto no valor de " + pedidosanteriores.ValorLiquido.ToString() + " lançado em " + pedidosanteriores.DataLancamento.ToString() + ". Código Pedido: " + pedidosanteriores.CodigoPedido + ". Não será possível incluir um novo pedido para este vendedor";
                        } else
                        {

                            Nullable<Double> contasareceber = ModelLibrary.MetodosDeposito.ObterValorAReceberVendedor(vendedor.Id);

                            if (contasareceber != null)
                            {

                                txtResultado.Text = "O vendedor possui contas a receber no valor de " + contasareceber.ToString() + ". Não será possível incluir um novo pedido para este vendedor.";

                            } else
                            {
                                txtResultado.Text = "O vendedor está OK. Clique em Incluir.";
                                cVendedorId = vendedor.Id;
                                btnIncluir.Enabled = true;
                            }


                        }


                    }
                    
                    



                } 


            }

        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {

            string vCodigoPedido = ModelLibrary.MetodosDeposito.InserirPedido(cVendedorId, localDepositoForm.cRetorno.cRetornoId);
            localDepositoForm.tbcRetorno.SelectedTab = localDepositoForm.tabRetornoPedidoDetalhe;
            localDepositoForm.cRetorno.ExibirDetalhesPedido(vCodigoPedido);
            localDepositoForm.cRetorno.CarregarPedidos();
            MessageBox.Show("Pedido Incluído com Sucesso");
            this.Hide();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FormNovoPedido_Load(object sender, EventArgs e)
        {
            CarregarListaVendedor();

        }
    }
}
