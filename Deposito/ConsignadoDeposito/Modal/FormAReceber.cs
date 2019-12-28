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
    public partial class FormAReceber : MetroFramework.Forms.MetroForm
    {


        public FormDeposito localDepositoForm = null;
        public string cModo;
        public int cAReceberId;

        public FormAReceber(FormDeposito formDeposito, string pModo)
        {
            InitializeComponent();


            localDepositoForm = formDeposito;

            cModo = pModo;
        }

        public void AdicionarTitulo()
        {


            try
            {
                //insert AReceber


                ModelLibrary.Receber receber = new ModelLibrary.Receber();

                receber.VendedorId = Convert.ToInt32(txtVendedorId.Text);
                receber.CargaId = Convert.ToInt32(txtCargaId.Text);
                receber.Documento = Convert.ToInt32(txtDocumento.Text);
                receber.Serie = txtSerie.Text;
                receber.ValorNF = Convert.ToDouble(txtValorNF.Text);
                receber.ValorDuplicata = Convert.ToDouble(txtValorDuplicata.Text);
                receber.ValorAReceber = Convert.ToDouble(txtValorAReceber.Text);
                receber.DataEmissao = cbbDataEmissao.Value;
                receber.DataLancamento = cbbDataLancamento.Value;
                receber.DataVencimento = cbbDataVencimento.Value;
                receber.Observacoes = txtObservacoes.Text;
                receber.Status = "0";


                ModelLibrary.MetodosDeposito.InserirReceber(receber);

                MessageBox.Show("Título incluído com sucesso!", "Incluir título a receber", MessageBoxButtons.OK, MessageBoxIcon.Information);

                localDepositoForm.cRetorno.CarregarContasAReceber();

                this.Close();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormAReceber.AdicionarTitulo()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        public void ExibirTitulo()
        {

            try
            {
                if (cModo == "Update") // get the current and check if is selected {
                {
                    //load and show data from AReceber

                    cAReceberId = Convert.ToInt32(localDepositoForm.grdContasAReceber.CurrentRow.Cells["Id"].Value);


                    ModelLibrary.Receber receber = ModelLibrary.MetodosDeposito.ObterAReceber(cAReceberId);

                    if (receber != null)
                    {

                        txtVendedorId.Text = receber.VendedorId.ToString();

                        ModelLibrary.Vendedor vendedor = ModelLibrary.MetodosDeposito.ObterVendedor(Convert.ToUInt32(receber.VendedorId));

                        txtNome.Text = (vendedor != null) ? vendedor.Nome : "";

                        txtDocumento.Text = receber.Documento.ToString();
                        txtSerie.Text = receber.Serie;

                        txtCargaId.Text = receber.CargaId.ToString();

                        txtValorNF.Text = receber.ValorNF.ToString();

                        txtValorDuplicata.Text = receber.ValorDuplicata.ToString();

                        txtValorAReceber.Text = receber.ValorAReceber.ToString();

                        cbbDataEmissao.Value = receber.DataEmissao.Value;
                        cbbDataLancamento.Value = receber.DataLancamento.Value;
                        cbbDataVencimento.Value = receber.DataVencimento.Value;

                        if (receber.DataPagamento != null)
                        {
                            lblDataPagamento.Visible = true;
                            cbbDataPagamento.Visible = true;
                            cbbDataPagamento.Value = receber.DataPagamento.Value;
                        }



                        txtObservacoes.Text = receber.Observacoes;

                        btnConfirmar.Enabled = true;


                    }
                    else
                    {

                        MessageBox.Show("Não foi possível carregar o Título a Receber. Por favor entre em contato com o administrador do sistema.", "Erro ao carregar título a receber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LimparTitulo();
                        this.Close();

                    }




                }
                else
                {
                    LimparTitulo();

                    btnConfirmar.Enabled = true;
                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormAReceber.ExibirTitulo()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
        }

        public void AlterarTitulo()
        {
            try
            {
                //Update AReceber

                ModelLibrary.Receber receber = new ModelLibrary.Receber();

                receber.Id = cAReceberId;
                receber.ValorNF = Convert.ToDouble(txtValorNF.Text);
                receber.ValorDuplicata = Convert.ToDouble(txtValorDuplicata.Text);
                receber.ValorAReceber = Convert.ToDouble(txtValorAReceber.Text);
                receber.DataEmissao = cbbDataEmissao.Value;
                receber.DataLancamento = cbbDataLancamento.Value;
                receber.DataVencimento = cbbDataVencimento.Value;
                receber.Observacoes = txtObservacoes.Text;


                ModelLibrary.MetodosDeposito.AtualizarReceber(receber);

                MessageBox.Show("Título alterado com sucesso!", "Alterar título a receber", MessageBoxButtons.OK, MessageBoxIcon.Information);

                localDepositoForm.cRetorno.CarregarContasAReceber();

                this.Close();
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormAReceber.AlterarTitulo()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }


        public void ExcluirTitulo()
        {

            try
            {
                if (MessageBox.Show("Deseja realmente excluir o lançamento selecionado?", "ATENÇÃO! Exclusão de Título a Receber", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    //verificar se existem registros associados
                    if (ModelLibrary.MetodosDeposito.ExcluirAReceber(cAReceberId))
                    {
                        MessageBox.Show("Título excluído com sucesso!", "Excluir título a receber", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Limpar Form
                        LimparTitulo();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível excluir o título selecionado, pois existem pagamentos relacionados.", "Excluir título a receber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }




                }
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormAReceber.ExcluirTitulo()");
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


        public void ControlOnlyInt(object sender, KeyPressEventArgs e)
        {


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }

        public void LimparTitulo()
        {
            cAReceberId = 0;

            btnConfirmar.Enabled = false;

            txtNome.Text = "";
            txtVendedorId.Text = "";
            txtCargaId.Text = localDepositoForm.cRetorno.cRetornoId.ToString();
            txtDocumento.Text = ""; // Gerar documento
            txtSerie.Text = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(2,2);
            txtValorNF.Text = "";
            txtValorDuplicata.Text = "";
            txtValorAReceber.Text = "";

            cbbDataEmissao.Value = DateTime.Now.Date;
            cbbDataLancamento.Value = DateTime.Now.Date;
            cbbDataVencimento.Value = DateTime.Now.AddDays(60).Date;

            lblDataPagamento.Visible = false;
            cbbDataPagamento.Visible = false;

            txtObservacoes.Text = "";


        }

        public void PesquisarVendedor()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Modal.FormListaVendedor formListaVendedor = new Modal.FormListaVendedor();

                var result = formListaVendedor.ShowDialog();

                if (result == DialogResult.OK)
                {

                    txtVendedorId.Text = formListaVendedor.cVendedorId.ToString();
                    txtNome.Text = formListaVendedor.cVendedorNome.ToString();
                    txtDocumento.Text = txtVendedorId.Text;
                    txtValorNF.Focus();
                }


                Cursor.Current = Cursors.Default;
            }
            catch (Exception vE)
            {
                Trace.WriteLine(DateTime.Now.ToString() + "FormAReceber.txtNome_ButtonClick()");
                Trace.TraceError(vE.Message);
                MessageBox.Show(vE.Message, vE.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNome_ButtonClick(object sender, EventArgs e)
        {

            PesquisarVendedor();

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            if (txtVendedorId.Text != "")
            {
                // verificar se está no modo Create ou Update
                if (cModo == "Update")
                {
                    AlterarTitulo();
                }
                else
                {
                    AdicionarTitulo();
                }
            }
            else
            {
                MessageBox.Show("Selecione o vendedor (clique na lupa para localizar).", "Confirmar Titulo a Receber", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // limpar tudo e fechar formulário
            this.Close();
        }

        private void FormAReceber_Load(object sender, EventArgs e)
        {
            ExibirTitulo();
        }

        private void txtValorNF_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorDuplicata.Text == "") txtValorDuplicata.Text = txtValorNF.Text;

        }

        private void txtValorDuplicata_Validating(object sender, CancelEventArgs e)
        {
            if (txtValorAReceber.Text == "") txtValorAReceber.Text = txtValorDuplicata.Text;
        }
    }
}
