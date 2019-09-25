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
    public partial class FormProduto : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;


        public long cCodigoProduto;

        public FormProduto(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;
        }        


        public void CarregarListaProdutos()
        {
            cbbPesquisaProduto.DataSource = ModelLibrary.MetodosDeposito.ObterListaProdutos();
            cbbPesquisaProduto.DisplayMember = "Descricao";
            cbbPesquisaProduto.ValueMember = "Id";
            cbbPesquisaProduto.Invalidate();
            cbbPesquisaProduto.SelectedIndex = -1;


            cbbPesquisaProduto.SelectedIndexChanged += PesquisarProduto;


        }


        public void CarregarListaCategoria()
        {
            cbbCategoria.DataSource = ModelLibrary.MetodosDeposito.ObterListaCategorias();
            cbbCategoria.DisplayMember = "Descricao";
            cbbCategoria.ValueMember = "Id";
            cbbCategoria.Invalidate();
            cbbCategoria.SelectedIndex = -1;

        }



        public void CarregarListaFornecedor()
        {
            cbbFornecedor.DataSource = ModelLibrary.MetodosDeposito.ObterListaFornecedores();
            cbbFornecedor.DisplayMember = "NomeFantasia";
            cbbFornecedor.ValueMember = "Id";
            cbbFornecedor.Invalidate();
            cbbFornecedor.SelectedIndex = -1;
        }


        public void CarregarListaCor()
        {
            cbbGradeCor.DataSource = ModelLibrary.MetodosDeposito.ObterListaCores();
            cbbGradeCor.DisplayMember = "Descricao";
            cbbGradeCor.ValueMember = "Id";
            cbbGradeCor.Invalidate();
            cbbGradeCor.SelectedIndex = -1;
        }


        public void CarregarListaTamanho()
        {
            cbbGradeTamanho.DataSource = ModelLibrary.MetodosDeposito.ObterListaTamanhos();
            cbbGradeTamanho.DisplayMember = "Descricao";
            cbbGradeTamanho.ValueMember = "Id";
            cbbGradeTamanho.Invalidate();
            cbbGradeTamanho.SelectedIndex = -1;
        }


        public void CarregarGrade()
        {


        }

        public void CarregarFormulario()
        {

        }

        public void LimparFormulario()
        {

            txtPesquisaCodProduto.Text = "";
            cbbPesquisaProduto.SelectedIndex = -1;

            txtNomeProduto.Text = "";
            cbbCategoria.SelectedIndex = -1;
            cbbFornecedor.SelectedIndex = -1;
            txtUnidade.Text = "";
            txtCodigoBarras.Text = "";
            txtDigitoVerificador.Text = "";
            cCodigoProduto = 0;

            btnCancelarProduto.Enabled = false;
            btnSalvarProduto.Text = "Salvar";
            btnSalvarProduto.Enabled = false;
            btnExcluirProduto.Enabled = false;

            grpProduto.Visible = false;
            pnlProdutoItem.Visible = false;
            pnlProdutoGrade.Visible = false;

        }

        public void PesquisarProduto(object sender, EventArgs e)
        {


            ModelLibrary.Produto produto = (ModelLibrary.Produto)cbbPesquisaProduto.SelectedItem;
            ExibirProduto(produto.CodigoBarras);
            txtPesquisaCodProduto.Text = "";

            /*
            try
            {

            }
            catch
            {
                txtPesquisaCodProduto.Text = "";
            }*/
        }

        public void ExibirProduto(string pCodigo)
        {

            ModelLibrary.Produto produto = ModelLibrary.MetodosDeposito.ObterProduto(pCodigo);

            if (produto != null)
            {
                cbbCategoria.SelectedIndex = -1;
                cbbFornecedor.SelectedIndex = -1;

                txtNomeProduto.Text = produto.Descricao;

                cbbCategoria.SelectedValue = (produto.CategoriaId == null) ? -1 : produto.CategoriaId.Value;

                cbbFornecedor.SelectedValue = (produto.FornecedorId == null)?-1: produto.FornecedorId.Value;

                txtUnidade.Text = produto.Unidade.Trim();
                txtCodigoBarras.Text = produto.CodigoBarras;
                txtDigitoVerificador.Text = produto.Digito;
                cCodigoProduto = produto.Id;

                grpProduto.Visible = true;
                pnlProdutoItem.Visible = true;
                pnlProdutoGrade.Visible = true;

                btnCancelarProduto.Enabled = true;
                btnSalvarProduto.Enabled = true;
                btnExcluirProduto.Enabled = true;


                CarregarGrade();




            } else
            {

                MessageBox.Show("Produto não encontrado", "Pesquisa Produto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        public void SalvarProduto()
        {


            Cursor.Current = Cursors.WaitCursor;

            btnSalvarProduto.Text = "Salvando...";
            btnSalvarProduto.Enabled = false;
            btnCancelarProduto.Enabled = false;
            btnExcluirProduto.Enabled = false;


            ModelLibrary.Produto produto = new ModelLibrary.Produto();

            produto.Descricao = txtNomeProduto.Text;
            produto.Unidade = txtUnidade.Text;
            produto.CodigoBarras = txtCodigoBarras.Text;
            produto.Digito = txtDigitoVerificador.Text;
            produto.CategoriaId = ((ModelLibrary.Categoria)cbbCategoria.SelectedItem).Id;
            produto.FornecedorId = ((ModelLibrary.Fornecedor)cbbFornecedor.SelectedItem).Id;

            if (cCodigoProduto == 0) // novo produto
            {

                ModelLibrary.MetodosDeposito.SalvarProduto("Create", produto);
                ExibirProduto(txtCodigoBarras.Text);

            } else
            {
                ModelLibrary.MetodosDeposito.SalvarProduto("Update", produto, cCodigoProduto);

            }


            btnSalvarProduto.Text = "Salvar";
            btnSalvarProduto.Enabled = true;
            btnCancelarProduto.Enabled = true;
            btnExcluirProduto.Enabled = true;

            Cursor.Current = Cursors.Default;

        }

        private void FormProduto_Load(object sender, EventArgs e)
        {

            CarregarListaProdutos();
            CarregarListaCategoria();
            CarregarListaFornecedor();
            CarregarListaCor();
            CarregarListaTamanho();

        }


        private void txtPesquisaCodProduto_Click(object sender, EventArgs e)
        {
            cbbFornecedor.SelectedIndex = -1;
            ExibirProduto(txtPesquisaCodProduto.Text);
        }

        private void btnSalvarProduto_Click(object sender, EventArgs e)
        {
            // validar form
            // verificar se codigo de barras já existe
            SalvarProduto();
        }

        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            cCodigoProduto = 0;

            LimparFormulario();

            grpProduto.Visible = true;

            btnCancelarProduto.Enabled = true;
            btnSalvarProduto.Enabled = true;
            btnExcluirProduto.Enabled = true;



        }

        private void btnCancelarProduto_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }
    }
}
