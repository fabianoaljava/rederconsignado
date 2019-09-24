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


        private void FormProduto_Load(object sender, EventArgs e)
        {

            CarregarListaProdutos();
            CarregarListaCategoria();
            CarregarListaFornecedor();
            CarregarListaCor();
            CarregarListaTamanho();

        }


    }
}
