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
    public partial class FormRecebimento : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;
        public string cModo, cTipo;       
        public int cReceberId, cRecebimentoId, cPedidoId, cCargaId, cVendedorId;        
        public double cValorOriginal = 0;


        public FormRecebimento(FormDeposito formDeposito, string pTipo, int pPedidoId, int pReceberId, int pCargaId)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;

            cReceberId = pReceberId;

            cPedidoId = pPedidoId;

            cTipo = pTipo;

            cCargaId = pCargaId;
            
        }

        private void CarregarRecebimento()
        {
            try
            {
                if (cTipo == "Pedido")
                {
                    ModelLibrary.Pedido pedido = ModelLibrary.MetodosDeposito.ObterPedido("", cPedidoId);

                    cVendedorId = pedido.VendedorId;

                    txtReferencia.Text = "Pedido: " + pedido.CodigoPedido + " | " + string.Format("{0:C2}", pedido.ValorLiquido + pedido.ValorAReceber);


                    double vValorAcerto = (pedido.ValorAcerto != null) ? Convert.ToDouble(pedido.ValorAcerto) : 0;

                    dlbTotalRecebido.Text = String.Format("{0:0.00}", vValorAcerto);
                    dlbTotalAReceber.Text = String.Format("{0:0.00}", (pedido.ValorLiquido + pedido.ValorAReceber - vValorAcerto));


                    if (pedido.Status == "4" || pedido.Status == "5")
                    {
                        btnConfirmar.Enabled = false;
                        lblStatus.Visible = true;
                        lblStatus.Text = "Este pedido foi fechado na criação de um novo e não pode ser editado";
                    }
                    else if (pedido.Status == "0" || pedido.Status == "1")
                    {
                        btnConfirmar.Enabled = false;
                        lblStatus.Text = "Este pedido ainda não foi retornado e por isso não é possível fazer o acerto. Por favor, realize o retorno primeiro.";
                        lblStatus.Visible = true;
                    }
                    else
                    {
                        btnConfirmar.Enabled = true;
                        lblStatus.Visible = false;

                        lblStatus.Text = "";
                    }



                }
                else
                {

                    ModelLibrary.Receber receber = ModelLibrary.MetodosDeposito.ObterAReceber(cReceberId);

                    cVendedorId = Convert.ToInt32(receber.VendedorId);
                    txtReferencia.Text = "Titulo: " + receber.Documento + "/" + receber.Serie + " | " + string.Format("{0:C2}", receber.ValorDuplicata);

                    dlbTotalRecebido.Text = String.Format("{0:0.00}", receber.ValorDuplicata - receber.ValorAReceber);
                    dlbTotalAReceber.Text = String.Format("{0:0.00}", receber.ValorAReceber);


                }



                List<ModelLibrary.ListaRecebimento> recebimento = ModelLibrary.MetodosDeposito.ObterListaRecebimento(cVendedorId, cReceberId);

                BindingListView<ModelLibrary.ListaRecebimento> view = new BindingListView<ModelLibrary.ListaRecebimento>(recebimento);

                grdRecebimento.DataSource = view;


                grdRecebimento.Columns[0].Width = 250;
                grdRecebimento.Columns[1].DefaultCellStyle.Format = "c";

                grdRecebimento.Columns[5].Visible = false;
                grdRecebimento.Columns[6].Visible = false;
                grdRecebimento.Columns[7].Visible = false;
                grdRecebimento.Columns[8].Visible = false;
                grdRecebimento.Columns[9].Visible = false;
                grdRecebimento.Columns[10].Visible = false;


                btnConfirmar.Enabled = true;

                lblStatus.Text = "";
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " FormReceberBaixa.CarregarRecebimento()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void Limpar()
        {



            txtValorRecebido.Text = "";
            cbbDataPagamento.Value = DateTime.Now;

            txtValorRecebido.Text = "";
            cbbFormaPagamento.SelectedIndex = -1;

            txtObservacao.Text = "";


            dlbTotalAReceber.Text = "0,00";
            dlbTotalRecebido.Text = "0,00";

            lblStatus.Visible = false;
            lblStatus.Text = "";

            cRecebimentoId = 0;
            cValorOriginal = 0;
            cVendedorId = 0;

            btnConfirmar.Enabled = true;


        }


        public Boolean ValidarForm()
        {


            try
            {
                if (txtValorRecebido.Text == "")
                {
                    MessageBox.Show("Informe o valor!", "Recebimento", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtValorRecebido.Focus();
                    return false;
                }
                else
                {
                    double vValorRecebido = Convert.ToDouble(txtValorRecebido.Text);

                    double vValorAReceber = (dlbTotalAReceber.Text != "") ? Convert.ToDouble(dlbTotalAReceber.Text) : 0;

                    if (vValorRecebido - cValorOriginal > vValorAReceber)
                    {
                        MessageBox.Show("O valor recebido não pode ser maior que o valor a receber.", "Recebimento", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtValorRecebido.Focus();
                        return false;
                    }
                    else if (cbbFormaPagamento.SelectedIndex < 0)
                    {
                        MessageBox.Show("Selecione a forma de pagamento!", "Recebimento", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cbbFormaPagamento.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " FormReceberBaixa.ValidarForm()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }


        public void ExibirRecebimento(int pRecebimentoId)
        {
            try
            {
                ModelLibrary.Recebimento recebimento = ModelLibrary.MetodosDeposito.ObterRecebimento(pRecebimentoId);


                if (recebimento != null)
                {

                    cReceberId = Convert.ToInt32(recebimento.ReceberId);
                    cRecebimentoId = recebimento.Id;
                    //txtReferencia.Text = recebimento.Tipo;
                    txtValorRecebido.Text = recebimento.ValorRecebido.Value.ToString("N");
                    cValorOriginal = Convert.ToDouble(recebimento.ValorRecebido);
                    cbbFormaPagamento.Text = recebimento.FormaPagamento;
                    cbbDataPagamento.Value = recebimento.DataPagamento.Value;
                    txtObservacao.Text = recebimento.Observacao;

                }
                else
                {

                    MessageBox.Show("Não foi possível carregar o recebimento. Por favor entre em contato com o administrador do sistema.", "Erro ao carregar título a receber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpar();
                    this.Close();
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " FormReceberBaixa.ExibirRecebimento()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void IncluirRecebimento()
        {
            try
            {
                if (ValidarForm())
                {

                    double vValorRecebido = Convert.ToDouble(txtValorRecebido.Text);

                    int vVendedorId;
                    string vCodigoPedido = "";


                    if (cTipo == "Pedido")
                    {
                        var pedido = ModelLibrary.MetodosDeposito.ObterPedido("", cPedidoId);
                        vCodigoPedido = (pedido != null) ? pedido.CodigoPedido : "";
                        vVendedorId = pedido.VendedorId;
                        cReceberId = 0;
                    }
                    else
                    {
                        var receber = ModelLibrary.MetodosDeposito.ObterAReceber(cReceberId);
                        vVendedorId = Convert.ToInt32(receber.VendedorId);
                        cPedidoId = 0;
                    }

                    ModelLibrary.MetodosDeposito.InserirRecebimento(cTipo, cCargaId, vVendedorId, vValorRecebido, cbbFormaPagamento.Text, txtObservacao.Text, cReceberId, cPedidoId, vCodigoPedido);

                    Limpar();
                    CarregarRecebimento();

                    localDepositoForm.cRetorno.CarregarContasAReceber();

                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " FormReceberBaixa.IncluirRecebimento()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        

        }

        public void AlterarRecebimento()
        {
            try
            {
                if (ValidarForm())
                {
                    double vValorRecebido = Convert.ToDouble(txtValorRecebido.Text);

                    ModelLibrary.MetodosDeposito.AlterarRecebimento(cRecebimentoId, vValorRecebido, cbbFormaPagamento.Text, txtObservacao.Text);

                    Limpar();
                    CarregarRecebimento();

                    localDepositoForm.cRetorno.CarregarContasAReceber();
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " FormReceberBaixa.AlterarRecebimento()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void ExcluirRecebimento(int pRecebimentoId)
        {
            try
            {
                if (MessageBox.Show("Deseja realmente excluir o pagamento selecionado?", "ATENÇÃO! Exclusão de Pagamento", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {


                    ModelLibrary.MetodosDeposito.ExcluirRecebimento(pRecebimentoId);


                    MessageBox.Show("Pagamento Excluído com sucesso!", "Excluir pagamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Limpar Form
                    Limpar();

                    CarregarRecebimento();

                    localDepositoForm.cRetorno.CarregarContasAReceber();

                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " FormReceberBaixa.ExcluirRecebimento()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            CarregarRecebimento();

            
        }

        private void btnGradeConfirmar_Click(object sender, EventArgs e)
        {


            if (cRecebimentoId != 0) // cModo = Editar
            {
                AlterarRecebimento();
            }
            else //cModo = Incluir
            {
                IncluirRecebimento();
            }

        }

        private void btnGradeCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void grdReceberBaixa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            ExibirRecebimento(Convert.ToInt32(grdRecebimento.CurrentRow.Cells["Id"].Value));
        }

        private void grdReceberBaixa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ExcluirRecebimento(Convert.ToInt32(grdRecebimento.CurrentRow.Cells["Id"].Value));
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Limpar();
            this.Close();
        }
    }
}
