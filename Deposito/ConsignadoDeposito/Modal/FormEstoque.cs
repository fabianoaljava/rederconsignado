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
using System.Diagnostics;

namespace ConsignadoDeposito.Modal
{
    public partial class FormEstoque : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;

        public int cProdutoGradeId;
        public int cEstoqueId = 0;

        public FormEstoque(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;
        }


        public void Limpar()
        {            
            cEstoqueId = 0;
            txtCodigoBarras.Text = "";
            txtCodigoBarras.ReadOnly = false;
            txtNome.Text = "";
            cbbTipoMovimentacao.SelectedIndex = -1;
            txtObservacoes.Text = "";

            btnAdicionar.Text = "Adicionar";
            btnAdicionar.Enabled = false;
            btnCancelar.Enabled = false;

            grdEstoque.DataSource = null;

            txtCodigoBarras.Focus();
        }


        public void PesquisarProduto(string pCodigo)
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
                Trace.WriteLine(DateTime.Now.ToString() + "FormEstoque.PesquisarProduto()");
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

                        MessageBox.Show("Este produto foi excluído e não pode ser movimentado.");

                        Limpar();

                    }
                    else
                    {

                        cProdutoGradeId = produtograde.Id;
                        txtCodigoBarras.Text = produtograde.CodigoBarras.ToString() + produtograde.Digito.ToString();
                        txtNome.Text = produto.Descricao;
                        cbbTipoMovimentacao.Focus();

                        CarregarMovimentacoes(produtograde.Id);

                        btnAdicionar.Enabled = true;
                        btnCancelar.Enabled = true;

                    }

                }
                else
                {
                    MessageBox.Show("Dígito verificador inválido. Não foi possível encontrar a grade deste produto.");
                    cProdutoGradeId = 0;
                    Limpar();
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormEstoque.ExibirProdutoGrade()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            


        }

        public void CarregarMovimentacoes(int pProdutoGradeId)
        {
            try
            {
                List<ModelLibrary.Estoque> estoque = ModelLibrary.MetodosDeposito.ObterListaEstoqueMovimentacao(pProdutoGradeId);

                BindingListView<ModelLibrary.Estoque> view = new BindingListView<ModelLibrary.Estoque>(estoque);

                grdEstoque.DataSource = view;


                grdEstoque.Columns[0].Visible = false;
                grdEstoque.Columns[1].HeaderText = "Tipo de Movimentação";
                grdEstoque.Columns[1].Width = 150;
                grdEstoque.Columns[3].Width = 500;
                grdEstoque.Columns[4].Visible = false;
                grdEstoque.Columns[5].Visible = false;
                grdEstoque.Columns[6].Visible = false;


                ModelLibrary.ProdutoGrade produtograde = ModelLibrary.MetodosDeposito.ObterProdutoGrade("", pProdutoGradeId);


                if (produtograde != null)
                {

                    lblSaldo.Visible = true;
                    dlbSaldo.Visible = true;
                    dlbSaldo.Text = produtograde.Quantidade.ToString();
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormEstoque.CarregarMovimentacoes()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            



        }

        public void ExibirMovimentacao(int pEstoqueId)
        {
            try
            {
                ModelLibrary.Estoque estoque = ModelLibrary.MetodosDeposito.ObterEstoqueMovimentacao(pEstoqueId);

                if (estoque != null)
                {

                    cEstoqueId = estoque.Id;

                    ModelLibrary.ProdutoGrade produtograde = ModelLibrary.MetodosDeposito.ObterProdutoGrade("", estoque.ProdutoGradeId.Value);
                    ModelLibrary.Produto produto = ModelLibrary.MetodosDeposito.ObterProduto(produtograde.CodigoBarras);

                    txtCodigoBarras.Text = produtograde.CodigoBarras + produtograde.Digito;
                    txtNome.Text = produto.Descricao;

                    cbbTipoMovimentacao.Text = (estoque.TipoMovimentacao == "E") ? "Entrada" : "Saída";
                    txtQuantidade.Text = estoque.Quantidade.ToString();
                    txtObservacoes.Text = estoque.Observacao;

                    txtCodigoBarras.ReadOnly = true;

                    btnAdicionar.Text = "Salvar";
                    btnAdicionar.Enabled = true;
                    btnCancelar.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Não foi possível carregar a Movimentação do Estoque. Por favor entre em contato com o administrador do sistema.", "Erro ao carregar título a receber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpar();
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormEstoque.ExibirMovimentacao()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void SalvarMovimentacao(int pEstoqueId = 0)
        {
            try
            {
                string vTipoMovimentacao = (cbbTipoMovimentacao.Text == "Entrada") ? "E" : "S";

                ModelLibrary.MetodosDeposito.SalvarEstoqueMovimentacao(pEstoqueId, cProdutoGradeId, vTipoMovimentacao, Convert.ToDouble(txtQuantidade.Text), txtObservacoes.Text);


                MessageBox.Show("Pagamento alterado com sucesso!", "Alterar pagamento", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarMovimentacoes(cProdutoGradeId);

                txtQuantidade.Text = "";
                cbbTipoMovimentacao.SelectedIndex = -1;
                txtObservacoes.Text = "";
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormEstoque.SalvarMovimentacao()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public void ExcluirMovimentacao(int pEstoqueId)
        {


            if (MessageBox.Show("Deseja realmente excluir essa movimentação?", "ATENÇÃO! Exclusão de Movimentação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                try
                {
                    ModelLibrary.MetodosDeposito.ExcluirEstoqueMovimentacao(pEstoqueId);
                
                    MessageBox.Show("Movimentação Excluída com sucesso!", "Excluir movimentação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    CarregarMovimentacoes(cProdutoGradeId);
                }
                catch (Exception vE)
                {
                    Trace.WriteLine(DateTime.Now.ToString() + "FormEstoque.ExcluirMovimentacao()");
                    Trace.TraceError(vE.Message);
                    MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }



        public void ControlOnlyInt(object sender, KeyPressEventArgs e)
        {


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }

        private void txtCodigoBarras_ButtonClick(object sender, EventArgs e)
        {
            if (txtCodigoBarras.Text != "")
            {
                PesquisarProduto(txtCodigoBarras.Text);

                cbbTipoMovimentacao.Focus();
            }
        }

        private void txtCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (txtCodigoBarras.Text != "")
                {
                    PesquisarProduto(txtCodigoBarras.Text);

                    cbbTipoMovimentacao.Focus();
                }
            }
        }

        private void txtCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigoBarras.Text != "")
            {
                PesquisarProduto(txtCodigoBarras.Text);

                cbbTipoMovimentacao.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (cProdutoGradeId > 0 && txtCodigoBarras.Text.Length > 0 && cbbTipoMovimentacao.SelectedIndex >= 0 && txtQuantidade.Text.Length > 0)
            {
                SalvarMovimentacao(cEstoqueId);
            } else
            {
                MessageBox.Show("Preencha todos os campos.", "Validação da Movimentação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void grdEstoque_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ExibirMovimentacao(Convert.ToInt32(grdEstoque.CurrentRow.Cells["Id"].Value));
        }

        private void grdEstoque_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ExcluirMovimentacao(Convert.ToInt32(grdEstoque.CurrentRow.Cells["Id"].Value));
            }
        }

        private void FormEstoque_Load(object sender, EventArgs e)
        {
            if (localDepositoForm.grdProdutos.CurrentRow != null)
            {
                PesquisarProduto(localDepositoForm.grdProdutos.CurrentRow.Cells["CodigoBarras"].Value.ToString());
            } else 
            if (localDepositoForm.txtProdutosCodigoBarras.Text != "")
            {
                PesquisarProduto(localDepositoForm.txtProdutosCodigoBarras.Text);
            }
        }
    }
}
