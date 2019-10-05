using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;
using Equin.ApplicationFramework;

namespace ConsignadoRepresentante
{
    public partial class Produto
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////


        public FormRepresentante localRepresentanteForm = null;


        public Produto(FormRepresentante formRepresentante)
        {

            localRepresentanteForm = formRepresentante;

        }


        public void ExibirProdutos()
        {


            List<ModelLibrary.ListaRepProdutos> produtos = ModelLibrary.MetodosRepresentante.ObterListaProdutos();

            BindingListView<ModelLibrary.ListaRepProdutos> view = new BindingListView<ModelLibrary.ListaRepProdutos>(produtos);

            localRepresentanteForm.grdProdutos.DataSource = view;

            localRepresentanteForm.grdProdutos.ColumnHeadersHeight = 40;

            localRepresentanteForm.grdProdutos.Columns[0].Width = 100;
            localRepresentanteForm.grdProdutos.Columns[1].Width = 250;
            localRepresentanteForm.grdProdutos.Columns[2].Width = 70;
            localRepresentanteForm.grdProdutos.Columns[3].Width = 70;
            localRepresentanteForm.grdProdutos.Columns[4].Width = 80;
            localRepresentanteForm.grdProdutos.Columns[5].Width = 70;

            localRepresentanteForm.grdProdutos.Columns[4].DefaultCellStyle.Format = "c";
            localRepresentanteForm.grdProdutos.Columns[5].HeaderText = "Saldo em Estoque";
            localRepresentanteForm.grdProdutos.Columns[6].Visible = false;

            localRepresentanteForm.cbbProdutoSaldo.Text = "Todos";            

        }


        public void PesquisarProdutos()
        {

            var vCriterio = new Dictionary<string, string>();

            if (localRepresentanteForm.txtProdutosCodigoBarras.Text != "") vCriterio["CodigoGeral"] = localRepresentanteForm.txtProdutosCodigoBarras.Text;

            if (localRepresentanteForm.txtProdutosNome.Text != "") vCriterio["Nome"] =  localRepresentanteForm.txtProdutosNome.Text;


            if (localRepresentanteForm.cbbProdutoSaldo.Text == "Com Saldo em Estoque")
            {
                vCriterio["SaldoEstoque"] = "Y"; 
            }
            else if (localRepresentanteForm.cbbProdutoSaldo.Text == "Sem Saldo em Estoque")
            {
                vCriterio["SaldoEstoque"] = "N";            
            }

            List<ModelLibrary.ListaRepProdutos> produtos = ModelLibrary.MetodosRepresentante.ObterListaProdutos(vCriterio);

            BindingListView<ModelLibrary.ListaRepProdutos> view = new BindingListView<ModelLibrary.ListaRepProdutos>(produtos);

            localRepresentanteForm.grdProdutos.DataSource = view;


            localRepresentanteForm.grdProdutos.Refresh();
        }

        public void ProdutosLimpar()
        {
            localRepresentanteForm.txtProdutosCodigoBarras.Text = "";
            localRepresentanteForm.txtProdutosNome.Text = "";
            localRepresentanteForm.cbbProdutoSaldo.ResetText();

            ExibirProdutos();
        }
    }


}
