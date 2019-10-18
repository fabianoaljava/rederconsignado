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
    public partial class FormReceberBaixa : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;
        public string cModo;
        public int cReceberBaixaId;
        public int cReceberId;
        public int cCargaId;


        public FormReceberBaixa(FormDeposito formDeposito, int pReceberId, int pCargaId = 0)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;

            cReceberId = pReceberId;

            cCargaId = pCargaId;

        }

        private void CarregarReceberBaixa()
        {

            List<ModelLibrary.ListaReceberBaixa> receberbaixa = ModelLibrary.MetodosDeposito.ObterListaReceberBaixa(cReceberId);

            BindingListView<ModelLibrary.ListaReceberBaixa> view = new BindingListView<ModelLibrary.ListaReceberBaixa>(receberbaixa);

            grdReceberBaixa.DataSource = view;


            grdReceberBaixa.Columns[0].Visible = false;
            grdReceberBaixa.Columns[1].Visible = false;
            grdReceberBaixa.Columns[2].DefaultCellStyle.Format = "c";


        }


        public void Limpar()
        {

            cReceberBaixaId = 0;

            txtValor.Text = "";
            cbbDataPagamento.Value = DateTime.Now;      

        }


        public void ExibirPagamento(int pReceberBaixaId)
        {


            ModelLibrary.ReceberBaixa receberBaixa = ModelLibrary.MetodosDeposito.ObterReceberBaixa(pReceberBaixaId);


            if (receberBaixa != null)
            {

                cReceberId = Convert.ToInt32(receberBaixa.ReceberId);
                cReceberBaixaId = receberBaixa.Id;
                txtValor.Text = receberBaixa.Valor.Value.ToString("N");
                cbbDataPagamento.Value = receberBaixa.DataPagamento.Value;
                

            } else
            {

                MessageBox.Show("Não foi possível carregar o Título a Receber. Por favor entre em contato com o administrador do sistema.", "Erro ao carregar título a receber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpar();
                this.Close();
            }




        }

        public void AdicionarPagamento()
        {

            double vTotalPago = 0;
            for (int i = 0; i < grdReceberBaixa.Rows.Count; ++i)
            {
                vTotalPago += Convert.ToInt32(grdReceberBaixa.Rows[i].Cells[2].Value);
            }

            vTotalPago = vTotalPago + Convert.ToDouble(txtValor.Text);

            if (vTotalPago > Convert.ToDouble(localDepositoForm.grdContasAReceber.CurrentRow.Cells["ValorAReceber"].Value)) 
            {
                MessageBox.Show("O valor total pago informado está acima do valor a receber", "Incluir pagamento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            } else
            {
                

                ModelLibrary.MetodosDeposito.SalvarAReceberBaixa(cReceberId, 0, Convert.ToDouble(txtValor.Text), cbbDataPagamento.Text, cCargaId);

                if (vTotalPago == Convert.ToDouble(localDepositoForm.grdContasAReceber.CurrentRow.Cells["ValorAReceber"].Value))
                {
                    ModelLibrary.MetodosDeposito.AtualizarStatusReceber(cReceberId, "1", cbbDataPagamento.Value);
                }

                MessageBox.Show("Pagamento Incluído com sucesso!", "Incluir pagamento", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Limpar();

                CarregarReceberBaixa();

                localDepositoForm.cRetorno.CarregarContasAReceber();
            }


        }

        public void AlterarPagamento()
        {
            double vTotalPago = 0;
            for (int i = 0; i < grdReceberBaixa.Rows.Count; ++i)
            {
                vTotalPago += Convert.ToInt32(grdReceberBaixa.Rows[i].Cells[2].Value);
            }

            vTotalPago = vTotalPago + Convert.ToDouble(txtValor.Text);

            if (vTotalPago > Convert.ToDouble(localDepositoForm.grdContasAReceber.CurrentRow.Cells["ValorAReceber"].Value))
            {
                MessageBox.Show("O valor total pago informado está acima do valor a receber", "Alterar pagamento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                ModelLibrary.MetodosDeposito.SalvarAReceberBaixa(cReceberId, cReceberBaixaId, Convert.ToDouble(txtValor.Text), cbbDataPagamento.Text, cCargaId);


                if (vTotalPago == Convert.ToDouble(localDepositoForm.grdContasAReceber.CurrentRow.Cells["ValorAReceber"].Value))
                {
                    ModelLibrary.MetodosDeposito.AtualizarStatusReceber(cReceberId, "1", cbbDataPagamento.Value);
                }

                MessageBox.Show("Pagamento alterado com sucesso!", "Alterar pagamento", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Limpar();

                CarregarReceberBaixa();

                localDepositoForm.cRetorno.CarregarContasAReceber();
            }

        }

        public void ExcluirPagamento(int pReceberBaixaId)
        {
            if (MessageBox.Show("Deseja realmente excluir o pagamento selecionado?", "ATENÇÃO! Exclusão de Pagamento", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {


                ModelLibrary.MetodosDeposito.ExcluirReceberBaixa(pReceberBaixaId);

                ModelLibrary.MetodosDeposito.AtualizarStatusReceber(cReceberId, "0");

                MessageBox.Show("Pagamento Excluído com sucesso!", "Excluir pagamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Limpar Form
                Limpar();

                CarregarReceberBaixa();

                localDepositoForm.cRetorno.CarregarContasAReceber();

            }

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


        private void FormReceberBaixa_Load(object sender, EventArgs e)
        {

            cReceberId = Convert.ToInt32(localDepositoForm.grdContasAReceber.CurrentRow.Cells["Id"].Value);
            Limpar();
            CarregarReceberBaixa();

            
        }

        private void btnGradeConfirmar_Click(object sender, EventArgs e)
        {

            if (txtValor.Text != "")
            {
                if (cReceberBaixaId == 0)
                {
                    AdicionarPagamento();
                }
                else
                {
                    AlterarPagamento();
                }
            } else
            {
                MessageBox.Show("Informe o valor", "Confirmar pagamento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnGradeCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void grdReceberBaixa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            ExibirPagamento(Convert.ToInt32(grdReceberBaixa.CurrentRow.Cells["Id"].Value));
        }

        private void grdReceberBaixa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ExcluirPagamento(Convert.ToInt32(grdReceberBaixa.CurrentRow.Cells["Id"].Value));
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Limpar();
            this.Close();
        }
    }
}
