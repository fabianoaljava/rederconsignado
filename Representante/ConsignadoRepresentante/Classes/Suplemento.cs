using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignadoRepresentante;
using Equin.ApplicationFramework;

namespace ConsignadoRepresentante
{
    public partial class Suplemento
    {
        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        ///
        public long cProdutoGradeId;
        public string cModoSuplemento;

        public FormDeposito localDeposito = null;

        public Suplemento(FormDeposito formDeposito)
        {

            localDeposito = formDeposito;

        }


        ////////////////////////////////////////
        /// Suplemento
        ////////////////////////////////////////


        public void SuplementoProdutoLimpar()
        {
            localDeposito.txtSuplCodigoBarras.Text = "";
            localDeposito.txtSuplProduto.Text = "";
            localDeposito.txtSuplQuantidade.Text = "";
            localDeposito.btnSuplConfirmar.Enabled = false;
            localDeposito.btnSuplCancelar.Enabled = false;
            localDeposito.txtSuplCodigoBarras.ReadOnly = false;
            cProdutoGradeId = 0;
            cModoSuplemento = "Insert";
            localDeposito.txtSuplCodigoBarras.Focus();
        }


        public void PesquisarSuplementoProduto(string pCodigo)
        {



            var produtograde = ModelLibrary.MetodosRepresentante.ObterProdutoGrade(pCodigo);

            if (produtograde != null)
            {

                var produto = ModelLibrary.MetodosRepresentante.ObterProduto(produtograde.CodigoBarras);

                localDeposito.txtSuplProduto.Text = produto.Descricao;

                if (localDeposito.txtSuplCodigoBarras.Text != produtograde.CodigoBarras + produtograde.Digito)
                {
                    localDeposito.txtSuplCodigoBarras.Text = produtograde.CodigoBarras + produtograde.Digito;
                    if (localDeposito.chkSuplQuantidade.Checked == false)
                    {
                        localDeposito.chkSuplQuantidade.Checked = true;
                        localDeposito.txtSuplQuantidade.Enabled = true;
                    }
                }



                cProdutoGradeId = produtograde.Id;

                localDeposito.btnSuplConfirmar.Enabled = true;
                localDeposito.btnSuplCancelar.Enabled = true;

                if (localDeposito.chkSuplQuantidade.Checked)
                {
                    localDeposito.txtSuplQuantidade.Focus();

                }
                else
                {
                    //inserir direto qtd=1
                    InserirCargaProduto();
                }

            }
            else
            {

                MessageBox.Show("Dígito verificador inválido. Não foi possível encontrar a grade deste produto.");

                cProdutoGradeId = 0;
                localDeposito.txtSuplCodigoBarras.Text = "";
                localDeposito.txtSuplCodigoBarras.Focus();
                localDeposito.btnSuplConfirmar.Enabled = false;
                localDeposito.btnSuplCancelar.Enabled = false;


            }
        }

        public void ExibirSuplementoProduto(long pCargaId)
        {

            Console.WriteLine("Exibindo o Suplemento da CargaID = " + pCargaId.ToString());           


            
             List<ModelLibrary.ListaRepCargaProduto> cargaproduto = ModelLibrary.MetodosRepresentante.ObterListaSuplemento();

            BindingListView<ModelLibrary.ListaRepCargaProduto> view = new BindingListView<ModelLibrary.ListaRepCargaProduto>(cargaproduto);

            localDeposito.grdSuplemento.DataSource = view;

            /// Ocultar coluna CargaProdutoId
            localDeposito.grdSuplemento.Columns[3].Visible = false;
            localDeposito.grdSuplemento.Columns[4].Visible = false;
            localDeposito.grdSuplemento.Columns[5].Visible = false;


            localDeposito.grdSuplemento.Columns[1].Width = 450;


        }

        
        public void InserirCargaProduto()
        {
            try
            {
                decimal vQuantidade;

                if (localDeposito.chkSuplQuantidade.Checked)
                {

                    if (localDeposito.txtSuplQuantidade.Text != "")
                    {
                        vQuantidade = Convert.ToDecimal(localDeposito.txtSuplQuantidade.Text);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, informe uma quantidade.");
                        vQuantidade = 0;
                    }

                }
                else
                {

                    vQuantidade = 1;

                }

                if (vQuantidade > 0)
                {
                    ModelLibrary.MetodosRepresentante.InserirSuplemento(localDeposito.cCargaId, cProdutoGradeId, vQuantidade);

                    ExibirSuplementoProduto(localDeposito.cCargaId);
                    SuplementoProdutoLimpar();
                }


            }
            catch (IOException vE)
            {
                MessageBox.Show("Ocorreu um erro ao Inserir o produto. Verifique os dados digitados e tente novamente. Se o erro persisitr, contate o administrador.");
                Console.WriteLine(vE.Message);
            }


        }




        public void EditarCargaProduto()
        {

            //ClearCargaProduto();


            cModoSuplemento = "Edit";
            cProdutoGradeId = Convert.ToInt32(localDeposito.grdSuplemento.CurrentRow.Cells["ProdutoGradeId"].Value);

            localDeposito.txtSuplCodigoBarras.Text = localDeposito.grdSuplemento.CurrentRow.Cells[0].Value.ToString();
            localDeposito.txtSuplCodigoBarras.ReadOnly = true;


            if (localDeposito.chkSuplQuantidade.Checked == false)
            {
                localDeposito.chkSuplQuantidade.Checked = true;
                localDeposito.txtSuplQuantidade.Enabled = true;
            }

            if (localDeposito.grdSuplemento.CurrentRow.Cells[2].Value is null)
            {
                cModoSuplemento = "Insert";
                localDeposito.txtSuplQuantidade.Text = "";
                localDeposito.txtSuplQuantidade.Focus();
            }
            else
            {
                localDeposito.txtSuplQuantidade.Text = localDeposito.grdSuplemento.CurrentRow.Cells[2].Value.ToString();
                localDeposito.txtSuplQuantidade.Focus();
            }


            localDeposito.txtSuplProduto.Text = localDeposito.grdSuplemento.CurrentRow.Cells[1].Value.ToString();


            localDeposito.btnSuplConfirmar.Enabled = true;
            localDeposito.btnSuplCancelar.Enabled = true;


        }




        public void AlterarCargaProduto()
        {

            /*try
            {*/

            ModelLibrary.MetodosRepresentante.AlterarSuplemento(localDeposito.cCargaId, cProdutoGradeId, Convert.ToDecimal(localDeposito.txtSuplQuantidade.Text));

            ExibirSuplementoProduto(localDeposito.cCargaId);

            SuplementoProdutoLimpar();

            /*} catch
            {

                MessageBox.Show("Não foi possível alterar o produto. Por favor, verifique os dados digitados e tente novamente");

            }*/


        }


        public void ConfirmarSuplemento()
        {

            if (cModoSuplemento == "Edit")
            {
                AlterarCargaProduto();
            }
            else
            {
                InserirCargaProduto();
            }

        }

        public void ExcluirCargaProduto()
        {



            if (MessageBox.Show("Deseja realmente excluir o lançamento selecionado?", "ATENÇÃO! Exclusão de Produto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                SuplementoProdutoLimpar();
                cProdutoGradeId = Convert.ToInt32(localDeposito.grdSuplemento.CurrentRow.Cells["ProdutoGradeId"].Value);


                ModelLibrary.MetodosRepresentante.ExcluirSuplemento(localDeposito.cCargaId, cProdutoGradeId);

                ExibirSuplementoProduto(localDeposito.cCargaId);
            }


        }



    }
}
