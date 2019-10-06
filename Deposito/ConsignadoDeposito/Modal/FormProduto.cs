using Equin.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignadoDeposito.Modal
{
    public partial class FormProduto : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;


        public int cProdutoId, cProdutoGradeId;
        public string cCodigoBarras, cGradeDV;

        public bool cDVChanged = false;

        public FormProduto(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;
        }        


        public void CarregarListaProdutos()
        {
            cbbPesquisaProduto.DataSource = ModelLibrary.MetodosDeposito.ObterListaProduto();
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
            cbbGradeCor.ValueMember = "Abreviatura";
            cbbGradeCor.Invalidate();
            cbbGradeCor.SelectedIndex = -1;
        }


        public void CarregarListaTamanho()
        {
            cbbGradeTamanho.DataSource = ModelLibrary.MetodosDeposito.ObterListaTamanhos();
            cbbGradeTamanho.DisplayMember = "Descricao";
            cbbGradeTamanho.ValueMember = "Abreviatura";
            cbbGradeTamanho.Invalidate();
            cbbGradeTamanho.SelectedIndex = -1;
        }


        public void CarregarProdutoGrade()
        {

            //grdProdutoGrade


            List<ModelLibrary.ListaProdutoGrade> produtosgrade = ModelLibrary.MetodosDeposito.ObterListaProdutoGrade(cProdutoId);

            BindingListView<ModelLibrary.ListaProdutoGrade> view = new BindingListView<ModelLibrary.ListaProdutoGrade>(produtosgrade);
            

            grdProdutoGrade.DataSource = view;

            grdProdutoGrade.Columns[0].Visible = false;


            grdProdutoGrade.Columns[3].Width = 300;
            grdProdutoGrade.Columns[4].Width = 300;

            grdProdutoGrade.Columns[5].DefaultCellStyle.Format = "c";
            grdProdutoGrade.Columns[6].DefaultCellStyle.Format = "c";

            grdProdutoGrade.Columns[5].HeaderText = "Valor Saída";
            grdProdutoGrade.Columns[6].HeaderText = "Valor Custo";

            grdProdutoGrade.Columns[7].Visible = false;



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
            cProdutoId = 0;
            cCodigoBarras = "";

            btnCancelarProduto.Enabled = false;
            btnSalvarProduto.Text = "Salvar";
            btnSalvarProduto.Enabled = false;
            btnExcluirProduto.Enabled = false;

            grpProduto.Enabled = false;
            pnlProdutoItem.Enabled = false;
            pnlProdutoGrade.Enabled = false;


            grdProdutoGrade.DataSource = null;


            cProdutoGradeId = 0;
            txtGradeDV.Text = "";
            cbbGradeCor.SelectedIndex = -1;
            cbbGradeTamanho.SelectedIndex = -1;
            txtValorCusto.Text = "";
            txtValorSaida.Text = "";

            cGradeDV = "";

            btnGradeConfirmar.Enabled = false;
            btnGradeCancelar.Enabled = false;



        }

        public void PesquisarProduto(object sender, EventArgs e)
        {

            if (cbbPesquisaProduto.SelectedIndex >= 0) {
                ModelLibrary.Produto produto = (ModelLibrary.Produto)cbbPesquisaProduto.SelectedItem;
                ExibirProduto(produto.CodigoBarras);
                txtPesquisaCodProduto.Text = "";
            }


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
                cProdutoId = produto.Id;
                cCodigoBarras = produto.CodigoBarras;


                if (produto.Status == "0")
                {

                    MessageBox.Show("Este produto foi excluído e não pode ser editado.");


                    grpProduto.Enabled = false;
                    pnlProdutoItem.Enabled = false;
                    pnlProdutoGrade.Enabled = false;

                    btnCancelarProduto.Enabled = false;
                    btnSalvarProduto.Enabled = false;
                    btnExcluirProduto.Enabled = false;

                    btnGradeConfirmar.Enabled = false;
                    btnGradeCancelar.Enabled = false;

                }
                else
                {
                    grpProduto.Enabled = true;
                    pnlProdutoItem.Enabled = true;
                    pnlProdutoGrade.Enabled = true;

                    btnCancelarProduto.Enabled = true;
                    btnSalvarProduto.Enabled = true;
                    btnExcluirProduto.Enabled = true;

                    btnGradeConfirmar.Enabled = true;
                    btnGradeCancelar.Enabled = true;
                }




                CarregarProdutoGrade();


                




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

            if (cProdutoId == 0) // novo produto
            {

                ModelLibrary.MetodosDeposito.SalvarProduto("Create", produto);
                ExibirProduto(txtCodigoBarras.Text);

            } else
            {
                ModelLibrary.MetodosDeposito.SalvarProduto("Update", produto, cProdutoId);

            }

            cCodigoBarras = txtCodigoBarras.Text;

            
            btnSalvarProduto.Text = "Salvar";
            btnSalvarProduto.Enabled = true;
            btnCancelarProduto.Enabled = true;
            btnExcluirProduto.Enabled = true;


            CarregarProdutoGrade();

            Cursor.Current = Cursors.Default;

        }


        public void ExcluirProduto()
        {

            ModelLibrary.MetodosDeposito.ExcluirProduto(cProdutoId);
            MessageBox.Show("Produto Excluído com Sucesso!");
            LimparFormulario();

        }


        public void LimparProdutoGrade()
        {
            cProdutoGradeId = 0;


            int maxDV = ModelLibrary.MetodosDeposito.ObterUltimoProdutoGrade(cProdutoId);

            txtGradeDV.Text = maxDV.ToString(); // obter max de produtograde
            cbbGradeCor.SelectedIndex = -1;
            cbbGradeTamanho.SelectedIndex = -1;
            txtValorCusto.Text = "";
            txtValorSaida.Text = "";

            cGradeDV = "";

            btnGradeConfirmar.Enabled = true;
            btnGradeCancelar.Enabled = true;

        }

        public void ExibirProdutoGrade()
        {

            cProdutoGradeId = Convert.ToInt32(grdProdutoGrade.CurrentRow.Cells["Id"].Value);

            txtGradeDV.Text = grdProdutoGrade.CurrentRow.Cells["Digito"].Value.ToString();
            cbbGradeCor.SelectedValue = grdProdutoGrade.CurrentRow.Cells["Cor"].Value.ToString();
            cbbGradeTamanho.SelectedValue = grdProdutoGrade.CurrentRow.Cells["Tamanho"].Value.ToString().Trim();
            
            txtValorCusto.Text = (grdProdutoGrade.CurrentRow.Cells["ValorCusto"].Value!= null)?grdProdutoGrade.CurrentRow.Cells["ValorCusto"].Value.ToString():"";
            txtValorSaida.Text = (grdProdutoGrade.CurrentRow.Cells["ValorSaida"].Value != null) ? grdProdutoGrade.CurrentRow.Cells["ValorSaida"].Value.ToString() : "";


            btnGradeConfirmar.Enabled = true;
            btnGradeCancelar.Enabled = true;

            cGradeDV = txtGradeDV.Text;
            cDVChanged = false;



        }

        public void SalvarProdutoGrade()
        {

            ModelLibrary.ProdutoGrade produtograde = new ModelLibrary.ProdutoGrade();

            produtograde.CodigoBarras = cCodigoBarras;
            produtograde.Digito = txtGradeDV.Text;
            produtograde.ProdutoId = cProdutoId;
            produtograde.Tamanho = cbbGradeTamanho.SelectedValue.ToString();
            produtograde.Cor = cbbGradeCor.SelectedValue.ToString();
            produtograde.ValorCusto = (txtValorCusto.Text != "")?Convert.ToDouble(txtValorCusto.Text):0;
            produtograde.ValorSaida = (txtValorSaida.Text != "")?Convert.ToDouble(txtValorSaida.Text):0;
            produtograde.DataInicial = DateTime.Now;
            produtograde.PesoBruto = 0;
            produtograde.PesoLiquido = 0;


            ModelLibrary.MetodosDeposito.SalvarProdutoGrade(produtograde, cProdutoGradeId);

            cGradeDV = txtGradeDV.Text;
            CarregarProdutoGrade();



        }

        public void ExcluirProdutoGrade()
        {

            ModelLibrary.MetodosDeposito.ProdutoGradeExcluir(Convert.ToInt32(grdProdutoGrade.CurrentRow.Cells["Id"].Value));
            CarregarProdutoGrade();

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


        private void txtPesquisaCodProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (txtPesquisaCodProduto.Text != "")
                {
                    txtPesquisaCodProduto_Click(sender, e);
                }
            }

        }

        private void btnSalvarProduto_Click(object sender, EventArgs e)
        {
            // validar form
            // verificar se codigo de barras já existe
            SalvarProduto();
        }

        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            cProdutoId = 0;

            LimparFormulario();

            grpProduto.Enabled = true;
            
            pnlProdutoGrade.Enabled = false;

            btnCancelarProduto.Enabled = true;
            btnSalvarProduto.Enabled = true;
            btnExcluirProduto.Enabled = true;



        }

        private void btnCancelarProduto_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }


        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este produto? ATENÇÃO: Esta opção não poderá ser desfeita posteriormente.", "Excluir Produto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ExcluirProduto();
            }

        }

        private void grdProdutoGrade_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ExibirProdutoGrade();
        }

        private void grdProdutoGrade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (MessageBox.Show("Deseja realmente excluir este item da grade? ATENÇÃO: Esta opção não poderá ser desfeita!.", "Excluir Grade de Produto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ExcluirProdutoGrade();
                }
                    
            }
        }

        private void btnGradeConfirmar_Click(object sender, EventArgs e)
        {
            SalvarProdutoGrade();
        }

        private void txtCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoBarras.Text != cCodigoBarras)
            {
                

                ModelLibrary.Produto produto = ModelLibrary.MetodosDeposito.ObterProduto(txtCodigoBarras.Text);
                if (produto != null)
                {

                    MessageBox.Show("Este Código de Barras já está cadastrado em outro produto.", "Alteração de Código de Barras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;

                }
            }
        }

        private void txtGradeDV_Validating(object sender, CancelEventArgs e)
        {
            if (txtGradeDV.Text != cGradeDV)
            {
                if (!ModelLibrary.MetodosDeposito.ProdutoGradeValidarDV(cProdutoId, txtGradeDV.Text))
                {

                    MessageBox.Show("Este Dígito já está cadastrado para este produto.", "Alteração de Digito da Grade", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;

                }
            }
                
        }


        private void grdProdutoGrade_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            Font fontriscada = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Strikeout, GraphicsUnit.Point);


            if (grdProdutoGrade.Rows[e.RowIndex].Cells["DataFinal"].Value != null)
            {
                grdProdutoGrade.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                grdProdutoGrade.Rows[e.RowIndex].DefaultCellStyle.Font = fontriscada;

            }
        }

        private void txtGradeDV_TextChanged(object sender, EventArgs e)
        {
            cDVChanged = true;
        }

        private void btnGradeCancelar_Click(object sender, EventArgs e)
        {
            LimparProdutoGrade();
        }
    }
}
