using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoDeposito;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using System.Diagnostics;

namespace ConsignadoDeposito
{
    public partial class Estoque
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////


        public FormDeposito localDepositoForm = null;

        public Estoque(FormDeposito formDeposito)
        {

            localDepositoForm = formDeposito;

        }



        public void ExibirProdutos()
        {


            try
            {
                List<ModelLibrary.ListaProdutos> produtos = ModelLibrary.MetodosDeposito.ObterListaProdutos();

                BindingListView<ModelLibrary.ListaProdutos> view = new BindingListView<ModelLibrary.ListaProdutos>(produtos);

                localDepositoForm.grdProdutos.DataSource = view;

                localDepositoForm.grdProdutos.ColumnHeadersHeight = 40;

                localDepositoForm.grdProdutos.Columns[0].Width = 100;
                localDepositoForm.grdProdutos.Columns[1].Width = 250;
                localDepositoForm.grdProdutos.Columns[2].Width = 70;
                localDepositoForm.grdProdutos.Columns[3].Width = 70;
                localDepositoForm.grdProdutos.Columns[4].Width = 80;
                localDepositoForm.grdProdutos.Columns[4].HeaderText = "Valor Saída";
                localDepositoForm.grdProdutos.Columns[4].DefaultCellStyle.Format = "c";
                localDepositoForm.grdProdutos.Columns[5].Width = 80;
                localDepositoForm.grdProdutos.Columns[5].HeaderText = "Valor Custo";
                localDepositoForm.grdProdutos.Columns[5].DefaultCellStyle.Format = "c";
                localDepositoForm.grdProdutos.Columns[6].Width = 70;
                localDepositoForm.grdProdutos.Columns[6].HeaderText = "Saldo em Estoque";
                localDepositoForm.grdProdutos.Columns[7].Width = 80;
                localDepositoForm.grdProdutos.Columns[7].DefaultCellStyle.Format = "c";
                localDepositoForm.grdProdutos.Columns[8].Visible = false;



                localDepositoForm.cbbProdutoSaldo.Text = "Todos";
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Estoque.ExibirProdutos()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }


        public void PesquisarProdutos()
        {

            try
            {
                var vCriterio = new Dictionary<string, string>();

                if (localDepositoForm.txtProdutosCodigoBarras.Text != "") vCriterio["CodigoGeral"] = localDepositoForm.txtProdutosCodigoBarras.Text;

                if (localDepositoForm.txtProdutosNome.Text != "") vCriterio["Nome"] = localDepositoForm.txtProdutosNome.Text;


                if (localDepositoForm.cbbProdutoSaldo.Text == "Com Saldo em Estoque")
                {
                    vCriterio["SaldoEstoque"] = "Y";
                }
                else if (localDepositoForm.cbbProdutoSaldo.Text == "Sem Saldo em Estoque")
                {
                    vCriterio["SaldoEstoque"] = "N";
                }

                List<ModelLibrary.ListaProdutos> produtos = ModelLibrary.MetodosDeposito.ObterListaProdutos(vCriterio);

                BindingListView<ModelLibrary.ListaProdutos> view = new BindingListView<ModelLibrary.ListaProdutos>(produtos);

                localDepositoForm.grdProdutos.DataSource = view;


                localDepositoForm.grdProdutos.ColumnHeadersHeight = 40;

                localDepositoForm.grdProdutos.Columns[0].Width = 100;
                localDepositoForm.grdProdutos.Columns[1].Width = 250;
                localDepositoForm.grdProdutos.Columns[2].Width = 70;
                localDepositoForm.grdProdutos.Columns[3].Width = 70;
                localDepositoForm.grdProdutos.Columns[4].Width = 80;
                localDepositoForm.grdProdutos.Columns[4].HeaderText = "Valor Saída";
                localDepositoForm.grdProdutos.Columns[4].DefaultCellStyle.Format = "c";
                localDepositoForm.grdProdutos.Columns[5].Width = 80;
                localDepositoForm.grdProdutos.Columns[5].HeaderText = "Valor Custo";
                localDepositoForm.grdProdutos.Columns[5].DefaultCellStyle.Format = "c";
                localDepositoForm.grdProdutos.Columns[6].Width = 70;
                localDepositoForm.grdProdutos.Columns[6].HeaderText = "Saldo em Estoque";
                localDepositoForm.grdProdutos.Columns[7].Width = 80;
                localDepositoForm.grdProdutos.Columns[7].DefaultCellStyle.Format = "c";
                localDepositoForm.grdProdutos.Columns[8].Visible = false;



                localDepositoForm.grdProdutos.Refresh();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "Estoque.PesquisarProdutos()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public void ProdutosLimpar()
        {
            localDepositoForm.txtProdutosCodigoBarras.Text = "";
            localDepositoForm.txtProdutosNome.Text = "";
            localDepositoForm.cbbProdutoSaldo.ResetText();

            ExibirProdutos();
        }
    }
}
