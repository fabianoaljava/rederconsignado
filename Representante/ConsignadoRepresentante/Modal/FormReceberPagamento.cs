﻿using Equin.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignadoRepresentante.Modal
{
    public partial class FormReceberPagamento : MetroFramework.Forms.MetroForm
    {

        public FormRepresentante localRepresentanteForm = null;

        public long cRecebimentoId = 0;

        public decimal cValorOriginal = 0;


        public FormReceberPagamento(FormRepresentante formRepresentante)
        {
            InitializeComponent();

            localRepresentanteForm = formRepresentante;
        }


        private void CarregarRecebimento()
        {

            //carregar combo Referencia - obter os pedidos em aberto + titulos em aberto


            var pedido = ModelLibrary.MetodosRepresentante.ObterVendedorPedidos(localRepresentanteForm.cVendedor.cVendedorId, localRepresentanteForm.cCargaId);
            var titulo = ModelLibrary.MetodosRepresentante.ObterListaTitulos(localRepresentanteForm.cVendedor.cVendedorId);



            Dictionary<string, string> referencia = new Dictionary<string, string>();


            //referencia.Add("Selecione a referência", "0|0");

            foreach (var row in pedido)
            {
                //if (row.ValorLiquido + row.ValorAReceber > 0)
                    referencia.Add("Pedido: " + row.CodigoPedido + " | " + string.Format("{0:C2}", row.ValorLiquido + row.ValorAReceber), "Pedido|" + row.Id);
            }


            foreach (var row in titulo)
            {

                //if (row.ValorDuplicata - row.ValorAReceber > 0)
                    referencia.Add("Titulo: " + row.Documento + "/" + row.Serie + " | " + string.Format("{0:C2}", row.ValorDuplicata), "Titulo|" + row.Id);
            }


            if (referencia.Count > 0)
            {


                cbbReferencia.DataSource = new BindingSource(referencia, null);
                cbbReferencia.DisplayMember = "Key";
                cbbReferencia.ValueMember = "Value";

                // para obter o valor: string value = ((KeyValuePair<string, string>)cbbReferencia.SelectedItem).Value;

                //carregar grid recebimentos
                List<ModelLibrary.ListaRecebimento> recebimento = ModelLibrary.MetodosRepresentante.ObterListaRecebimento(localRepresentanteForm.cVendedor.cVendedorId);

                BindingListView<ModelLibrary.ListaRecebimento> view = new BindingListView<ModelLibrary.ListaRecebimento>(recebimento);

                grdRecebimentos.DataSource = view;


                grdRecebimentos.Columns[0].Width = 250;
                grdRecebimentos.Columns[1].DefaultCellStyle.Format = "c";

                grdRecebimentos.Columns[5].Visible = false;
                grdRecebimentos.Columns[6].Visible = false;
                grdRecebimentos.Columns[7].Visible = false;
                grdRecebimentos.Columns[8].Visible = false;
                grdRecebimentos.Columns[9].Visible = false;
                grdRecebimentos.Columns[10].Visible = false;



                cbbReferencia.SelectedIndex = -1;

                //CarregarTotalizador();

            } else
            {
                MessageBox.Show("O vendendor selecionado não possui acertos a receber ou o retorno ainda não foi realizado.");
                this.Close();
            }




        }        

        public void LimparRecebimento()
        {

            cbbReferencia.SelectedIndex = -1;
            cbbReferencia.Enabled = true;
            txtValorRecebido.Text = "";
            cbbFormaPagamento.SelectedIndex = -1;
            txtObservacao.Text = "";


            dlbTotalAReceber.Text = "0,00";
            dlbTotalRecebido.Text = "0,00";

            lblStatus.Visible = false;
            lblStatus.Text = "";
            cRecebimentoId = 0;
            cValorOriginal = 0;


            btnConfirmar.Enabled = true;

        }

        public Boolean ValidarForm()
        {            




            if (cbbReferencia.SelectedIndex <0)
            {
                MessageBox.Show("Selecione a referencia!", "Recebimento", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbbReferencia.Focus();
                return false;
            } else if (txtValorRecebido.Text == "")
            {
                MessageBox.Show("Informe o valor!", "Recebimento", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtValorRecebido.Focus();
                return false;
            }
            else
            {
                decimal vValorRecebido = Convert.ToDecimal(txtValorRecebido.Text);

                decimal vValorAReceber = (dlbTotalAReceber.Text != "") ? Convert.ToDecimal(dlbTotalAReceber.Text) : 0;

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

        public void ExibirRecebimento(long pRecebimentoId)
        {

            var recebimento = ModelLibrary.MetodosRepresentante.ObterRecebimento(pRecebimentoId);

            if (recebimento != null)
            {
                cRecebimentoId = recebimento.Id;
                
                if (recebimento.PedidoId != 0)
                {
                    cbbReferencia.SelectedValue = "Pedido|" + recebimento.PedidoId.ToString();
                } else
                {
                    cbbReferencia.SelectedValue = "Titulo|" + recebimento.ReceberId.ToString();
                }

                cbbReferencia.Enabled = false;

                txtValorRecebido.Text = String.Format("{0:0.00}", recebimento.ValorRecebido);
                cValorOriginal = Convert.ToDecimal(recebimento.ValorRecebido);
                cbbFormaPagamento.Text = recebimento.FormaPagamento;
                txtObservacao.Text = recebimento.Observacao;

                if (recebimento.CargaId != localRepresentanteForm.cCargaId)
                {

                    lblStatus.Text = "O recebimento selecionado pertence à outra carga e não pode ser alterado.";
                    btnConfirmar.Enabled = false;
                   
                }

                if (recebimento.Tipo == "Extra")
                {

                    lblStatus.Text = "O recebimento selecionado é referente a débido anterior e foi recebido como Extra. O valor não pode ser alterado.";
                    txtValorRecebido.ReadOnly = true;                    
                } else
                {
                    txtValorRecebido.ReadOnly = false;
                }


            }




        }

        public void IncluirRecebimento()
        {

            if (ValidarForm())
            {


                decimal vValorRecebido = Convert.ToDecimal(txtValorRecebido.Text);


                string[] vReferencia;

                vReferencia = ((KeyValuePair<string, string>)cbbReferencia.SelectedItem).Value.ToString().Split('|');

                long vPedidoId, vReceberId;
                string vCodigoPedido = "";

                if (vReferencia[0] == "Pedido")
                {
                    vPedidoId = Convert.ToInt32(vReferencia[1]);
                    var pedido = ModelLibrary.MetodosRepresentante.ObterPedido(vPedidoId);
                    vCodigoPedido = (pedido != null) ? pedido.CodigoPedido : "";
                    vReceberId = 0;

                    if (pedido.Status == "0" || pedido.Status == "1")
                    {
                        ModelLibrary.MetodosRepresentante.RetornarPedido(vPedidoId);
                    }
                }
                else
                {
                    vReceberId = Convert.ToInt32(vReferencia[1]);
                    vPedidoId = 0;
                }





                ModelLibrary.MetodosRepresentante.InserirRecebimento(vReferencia[0], localRepresentanteForm.cCargaId, localRepresentanteForm.cVendedor.cVendedorId, vValorRecebido, cbbFormaPagamento.Text, txtObservacao.Text, vReceberId, vPedidoId, vCodigoPedido);


                LimparRecebimento();
                CarregarRecebimento();

                localRepresentanteForm.cVendedor.ExibirAcerto();








            } 

        }

        public void AlterarRecebimento()
        {

            if (ValidarForm())
            {
                decimal vValorRecebido = Convert.ToDecimal(txtValorRecebido.Text);

                ModelLibrary.MetodosRepresentante.AlterarRecebimento(cRecebimentoId, vValorRecebido, cbbFormaPagamento.Text, txtObservacao.Text);

                LimparRecebimento();
                CarregarRecebimento();

                localRepresentanteForm.cVendedor.ExibirAcerto();
            }


        }


        public void ExcluirRecebimento(long pRecebimentoId)
        {

            ModelLibrary.MetodosRepresentante.ExcluirRecebimento(pRecebimentoId);

            LimparRecebimento();
            CarregarRecebimento();

            localRepresentanteForm.cVendedor.ExibirAcerto();

        }


        public void CarregarTotalizador()
        {


            if (cbbReferencia.SelectedIndex != -1)
            {


                string[] vReferencia;

                vReferencia = ((KeyValuePair<string, string>)cbbReferencia.SelectedItem).Value.ToString().Split('|');

                long vPedidoId, vReceberId;

                if (vReferencia[0] == "Pedido")
                {
                    vPedidoId = Convert.ToInt32(vReferencia[1]);
                    var pedido = ModelLibrary.MetodosRepresentante.ObterPedido(vPedidoId);

                    decimal vValorAcerto = (pedido.ValorAcerto != null) ? Convert.ToDecimal(pedido.ValorAcerto) : 0;

                    dlbTotalRecebido.Text = String.Format("{0:0.00}", vValorAcerto);
                    dlbTotalAReceber.Text = String.Format("{0:0.00}", (pedido.ValorLiquido + pedido.ValorAReceber - vValorAcerto));


                    if (pedido.Status == "4")
                    {
                        btnConfirmar.Enabled = false;
                        lblStatus.Visible = true;
                        lblStatus.Text = "Este pedido foi fechado na criação de um novo e não pode ser editado";
                    } 
                    //else if (pedido.Status == "0" || pedido.Status == "1")
                    //{
                    //    btnConfirmar.Enabled = false;
                    //    lblStatus.Text = "Este pedido ainda não foi retornado e por isso não é possível fazer o acerto. Por favor, realize o retorno primeiro.";
                    //    lblStatus.Visible = true;
                    //}
                    else
                    {
                        localRepresentanteForm.dlbTotalAcerto.Text = String.Format("{0:0.00}", pedido.ValorAcerto);
                        localRepresentanteForm.dlbTotalPendente.Text = String.Format("{0:0.00}", (pedido.ValorLiquido + pedido.ValorAReceber - pedido.ValorAcerto));

                        btnConfirmar.Enabled = true;
                        lblStatus.Visible = false;

                        lblStatus.Text = "";
                    }

                }
                else if (vReferencia[0] == "Titulo")
                {
                    vReceberId = Convert.ToInt32(vReferencia[1]);
                    var receber = ModelLibrary.MetodosRepresentante.ObterTitulo(vReceberId);


                    dlbTotalRecebido.Text = String.Format("{0:0.00}", receber.ValorDuplicata - receber.ValorAReceber);
                    dlbTotalAReceber.Text = String.Format("{0:0.00}", receber.ValorAReceber);

                    localRepresentanteForm.cVendedor.ExibirTitulos();
                    localRepresentanteForm.grdFinanceiroTitulos.Refresh();


                    btnConfirmar.Enabled = true;

                    lblStatus.Text = "";
                }

            }
            
        }



        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;//set to false if you need that textbox gets enter key
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



        private void FormReceberPagamento_Load(object sender, EventArgs e)
        {

            CarregarRecebimento();

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparRecebimento();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            if (cRecebimentoId != 0) // cModo = Editar
            {
                AlterarRecebimento();
            } else //cModo = Incluir
            {
                IncluirRecebimento();
            }

        }

        private void grdRecebimentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ExibirRecebimento(Convert.ToInt64(grdRecebimentos.CurrentRow.Cells["Id"].Value));
        }

        private void grdRecebimentos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Deseja realmente excluir o recebimento selecionado?", "ATENÇÃO! Exclusão de Recebimento", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    ExcluirRecebimento(Convert.ToInt64(grdRecebimentos.CurrentRow.Cells["Id"].Value));

                }

            }
        }

        private void cbbReferencia_SelectedIndexChanged(object sender, EventArgs e)
        {

            //CarregarTotalizador();

        }

        private void txtValorRecebido_Validating(object sender, CancelEventArgs e)
        {
            CarregarTotalizador();
        }

        private void FormReceberPagamento_FormClosing(object sender, FormClosingEventArgs e)
        {
            //localRepresentanteForm.RecarregarDados();
            localRepresentanteForm.cVendedor.VendedorReload();
            //localRepresentanteForm.cVendedor.ExibirAcerto();
            //localRepresentanteForm.cVendedor.ExibirPedido(localRepresentanteForm.cVendedor.cVendedorId);
            //localRepresentanteForm.cVendedor.ExibirRetornoProduto(localRepresentanteForm.cVendedor.cVendedorId);
        }

        private void cbbReferencia_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
