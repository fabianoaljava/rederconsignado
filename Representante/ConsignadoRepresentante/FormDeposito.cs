﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignadoRepresentante;

namespace ConsignadoRepresentante
{
    public partial class FormDeposito : MetroFramework.Forms.MetroForm
    {


        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////

        public long cCargaId;
        public string cStatus;
       
    

        public DepositoHome cHome;
        public Importar cImportar;
        public ConferirProdutos cConferirProdutos;
        public Exportar cExportar;
        public Suplemento cSuplemento;
        public Relatorio cRelatorio;
        public Configuracao cConfiguracao;
        public Ajuda cAjuda;


        public System.Windows.Forms.Form loginWindow = null;



        /// <summary>
        /// Inicialização do formulário Deposito
        /// </summary>
        /// <param name="loginForm">Formulário de Login</param>
        /// <param name="pUsuario">Login do Usuário Autenticado</param>
        /// <param name="pNome">Nome do Usuário Autenticado</param>
        public FormDeposito(System.Windows.Forms.Form loginForm, string pUsuario, string pNome)
        {
            InitializeComponent();

            this.loginWindow = loginForm;
            this.loginWindow.Hide();

            lblUsuario.Text = pUsuario;
            lblNome.Text = pNome;


            cHome = new DepositoHome(this);
            cImportar = new Importar(this);
            cConferirProdutos = new ConferirProdutos(this);
            cExportar = new Exportar(this);
            cSuplemento = new Suplemento(this);
            cRelatorio = new Relatorio(this);
            cConfiguracao = new Configuracao(this);

        }


        ////////////////////////////////////////
        /// Funções do Formulário Principal
        ////////////////////////////////////////



        public void CarregarRepresentante()
        {
            Boolean vServerConectado;

            //se servidor estiver online 
            if (ModelLibrary.MetodosDeposito.VerificarServidor())
            {
                lblServerStatus.Text = "Servidor Online";
                lblServerStatus.ForeColor = Color.Green;
                vServerConectado = true;
            }
            else
            {
                lblServerStatus.Text = "Servidor Offline";
                lblServerStatus.ForeColor = Color.Red;
                vServerConectado = false;
            }

            //se estiver conectado ao servidor
            if (vServerConectado)
            {
                //verifica se importação foi realizada
                if (ModelLibrary.MetodosRepresentante.VerificarImportacao())
                {
                    //se importacao foi realizada
                    lblCarga.Text = "Carga Obtida.";
                    cImportar.CarregarFormularioLocal(); //obter praça e representante local
                    cImportar.ExibirImportacao();
                    cConferirProdutos.ExibirConferenciaProduto(cCargaId);
                    cSuplemento.ExibirSuplementoProduto(cCargaId);
                    pnlExportacaoMain.Enabled = true;
                }
                else
                {
                    //se importacao não foi realizada
                    lblCarga.Text = "Importação de Carga Pendente.";
                    cImportar.CarregarFormularioServer(); //obter praça e representante do servidor
                    pnlExportacaoMain.Enabled = false;

                }
            }
            else // se não estiver conectado ao servidor
            {
                //verifica se importação foi realizada
                if (ModelLibrary.MetodosRepresentante.VerificarImportacao())
                {
                    //se importacao foi realizada
                    lblCarga.Text = "Carga Obtida.";
                    cImportar.CarregarFormularioLocal(); //obter praça e representante local
                    cImportar.ExibirImportacao();
                    cConferirProdutos.ExibirConferenciaProduto(cCargaId);
                    cSuplemento.ExibirSuplementoProduto(cCargaId);
                    pnlExportacaoMain.Enabled = true;
                }
                else
                {
                    //se importacao não foi realizada
                    lblCarga.Text = "Importação de Carga Pendente.";
                    /// Exibe mensagem se deseja conectar com o banco de dados até que a conexão seja efetuada / com opção para alterar informações de conexão ou pesquisar conexão (futuro)
                    pnlExportacaoMain.Enabled = false;

                    if (MessageBox.Show("O servidor <<SERVER>> não foi localizado ou está desconectado. Deseja tentar novamente?", "Reder Consignado", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        CarregarRepresentante();
                    }


                }

            }
        }


        public void RecarregarDados()
        {

            cImportar.RecarregarDados();

        }












        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////////   FUNÇÕES DO FORM  //////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////




        private void FormDeposito_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CarregarRepresentante();
            tbcPrincipal.SelectedTab = tabHome;
            Cursor.Current = Cursors.Default;
        }


        private void lblSair_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja Realmente Sair?", "Reder Consignado", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Application.Exit();
        }


        private void FormDeposito_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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




        ////////////////////////////////////////
        /// IMPORTAR - PESQUISAR
        ////////////////////////////////////////



        private void cbbCargaPraca_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                ModelLibrary.Praca praca = (ModelLibrary.Praca)cbbImportarPraca.SelectedItem;
                txtImportarCodPraca.Text = praca.Id.ToString();
            }
            catch
            {
                txtImportarCodPraca.Text = "";
            }

        }


        private void txtImportarCodPraca_ButtonClick(object sender, EventArgs e)
        {
            int i = -1;
            try
            {
                i = Convert.ToInt32(txtImportarCodPraca.Text);
            }
            catch
            {
                MessageBox.Show("Código Inválido");
            }


            var praca = ModelLibrary.MetodosDeposito.ObterPraca(i);

            if (praca != null)
            {
                cbbImportarPraca.SelectedIndex = cbbImportarPraca.FindString(praca.Descricao);
            }
            else
            {
                cbbImportarPraca.SelectedIndex = -1;
            }

        }

        private void txtCargaCodPraca_Leave(object sender, EventArgs e)
        {
            if (txtImportarCodPraca.Text != "") txtImportarCodPraca_ButtonClick(sender, e);
        }


        private void cbbCargaRepresentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ModelLibrary.Representante representante = (ModelLibrary.Representante)cbbImportarRepresentante.SelectedItem;
                txtImportarCodRepresentante.Text = representante.Id.ToString();
            }
            catch
            {
                txtImportarCodRepresentante.Text = "";
            }
        }

        private void txtImportarCodRepresentante_ButtonClick(object sender, EventArgs e)
        {
            int i = -1;
            try
            {
                i = Convert.ToInt32(txtImportarCodRepresentante.Text);
            }
            catch
            {
                MessageBox.Show("Código Inválido");
            }


            var representante = ModelLibrary.MetodosDeposito.ObterRepresentante(i);

            if (representante != null)
            {
                cbbImportarRepresentante.SelectedIndex = cbbImportarRepresentante.FindString(representante.Nome);
            }
            else
            {
                cbbImportarRepresentante.SelectedIndex = -1;
            }

        }

        private void txtCargaCodRepresentante_Leave(object sender, EventArgs e)
        {
            if (txtImportarCodRepresentante.Text != "") txtImportarCodRepresentante_ButtonClick(sender, e);
        }

        private void cbbCargaMesAno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                e.SuppressKeyPress = true;
                btnImportarAnalisar.Focus();

            }
        }


        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            string objname = ((MetroFramework.Controls.MetroTextBox)sender).Name;


            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (objname == "txtImportarCodRepresentante")
                {
                    if (txtImportarCodPraca.Text != "")
                    {
                        txtImportarCodRepresentante_ButtonClick(sender, e);
                        cbbImportarMesAno.Focus();
                    }

                }
                else if (objname == "txtImportarCodPraca")
                {
                    if (txtImportarCodPraca.Text != "")
                    {
                        txtImportarCodPraca_ButtonClick(sender, e);
                        txtImportarCodRepresentante.Focus();
                    }

                }
                else
                {
                    SendKeys.Send("{TAB}");
                }

                e.Handled = true;//set to false if you need that textbox gets enter key
            }
        }


        private void btnImportarAnalisar_Click(object sender, EventArgs e)
        {
            if (cbbImportarRepresentante.SelectedIndex != -1 && cbbImportarRepresentante.SelectedIndex != -1)
            {
                cImportar.ImportarCarga(true);
            } else
            {
                MessageBox.Show("Representante ou praça não encontrados");
            }
            
        }


        private void btnImportar_Click(object sender, EventArgs e)
        {

            if (cbbImportarRepresentante.SelectedIndex != -1 && cbbImportarRepresentante.SelectedIndex != -1)
            {
                cImportar.ImportarCarga(false);
            }
            else
            {
                MessageBox.Show("Representante ou praça não encontrados");
            }
        }

        private void btnCargaPesquisar_Click(object sender, EventArgs e)
        {
            ///Abre Janela de Pesquisa de Carga com opção para Alterar / Excluir - se tiver permissão
        }

        private void btnImportarLimpar_Click(object sender, EventArgs e)
        {
            cImportar.ImportarLimpar();
        }


        private void btnExcluirImportacao_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("ATENÇÃO: TODOS OS DADOS SERÃO APAGADOS. Deseja realmente excluir a importação?", "ATENÇÃO!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                if (ModelLibrary.MetodosDeposito.VerificarServidor())
                {
                    if (cStatus == "E")
                    {
                        if (MessageBox.Show("O servidor foi encontrado Online, deseja reverter o status da Carga de \"Exportado\" para \"Aberto\"? \n \n ATENÇAO: Caso contrário NÃO será permitida a importação desta carga novamente. ", "Reverter Carga!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            cImportar.ExcluirImportacao(true);
                        }
                        else
                        {
                            cImportar.ExcluirImportacao(false);
                        }
                    } else
                    {
                        cImportar.ExcluirImportacao(false);
                    }

                }
                else
                {
                    if (MessageBox.Show("ATENÇAO: NÃO será permitida a importação desta carga novamente. Deseja realmente continuar?", "Impossível Reverter Carga!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        cImportar.ExcluirImportacao(false);
                    }

                }

            }
        }

        ////////////////////////////////////////
        /// CONFERIR PRODUTOS
        ////////////////////////////////////////

        private void txtConfCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtConfCodigoBarras.Text != "")
                {
                    e.SuppressKeyPress = true;
                    //cConferirProdutos.PesquisarConferenciaProduto(txtConfCodigoBarras.Text);
                    SendKeys.Send("{TAB}");
                }                    
            }
        }

        private void txtConfCodigoBarras_Leave(object sender, EventArgs e)
        {
            //if (txtConfCodigoBarras.Text != "")
            //{
            //    cConferirProdutos.PesquisarConferenciaProduto(txtConfCodigoBarras.Text);
            //}
        }


        private void btnConferenciaConfirmar_Click(object sender, EventArgs e)
        {

            cConferirProdutos.ConfirmarCargaProdutoConferencia();

        }

        private void chkCargaQuantidade_CheckedChanged(object sender, EventArgs e)
        {
            if (cImportar.cImportarProdutoId!= 0)
            {
                chkConfQuantidade.Checked = true;
                txtConfQuantidade.Enabled = true;
            }
            else
            {
                txtConfQuantidade.Text = "";
                txtConfQuantidade.Enabled = chkConfQuantidade.Checked;
            }

        }


        private void txtConfQuantidade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnConferenciaConfirmar_Click(sender, e);

            }
        }


        private void btnConfCancelar_Click(object sender, EventArgs e)
        {
            cConferirProdutos.ConferenciaProdutoLimpar();
        }

        private void grdConfProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cConferirProdutos.EditarConferenciaProduto();
        }

        private void grdConfProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {

                cConferirProdutos.ExcluirCargaProdutoConferencia();
            }

        }

        private void grdConfProduto_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            if (grdConfProduto.Rows[e.RowIndex].Cells[3].Value != null) {
                if (Convert.ToInt32(grdConfProduto.Rows[e.RowIndex].Cells[4].Value.ToString()) > 0)
                {
                    grdConfProduto.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
                    grdConfProduto.Rows[e.RowIndex].Cells["Acao"].Value = "Resolver Conflito";
                    grdConfProduto.Rows[e.RowIndex].Cells["Acao"].ContextMenuStrip = cmsResolverConflito;
                }
                else if (Convert.ToInt32(grdConfProduto.Rows[e.RowIndex].Cells[4].Value.ToString()) == 0)
                {
                    grdConfProduto.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
                    
                }
                else
                {
                    grdConfProduto.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    grdConfProduto.Rows[e.RowIndex].Cells["Acao"].Value = "Resolver Conflito";
                    grdConfProduto.Rows[e.RowIndex].Cells["Acao"].ContextMenuStrip = cmsResolverConflito;

                }
            }
            
            
        }

       


        ////////////////////////////////////////
        /// ORGANIZAR DAQUI PRA BAIXO
        ////////////////////////////////////////
        ///


        private void btnExportar_Click(object sender, EventArgs e)
        {


            //Task.Run(() => cExportar.AcionarThread()).Wait();

            cExportar.ExportarDados();


        }

        private void btnExportarAnalisar_Click(object sender, EventArgs e)
        {

            cExportar.ExportarAnalisar();

        }


        private void bgwExportar_DoWork(object sender, DoWorkEventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in grdExportacao.Rows)
            {
                Console.WriteLine(row.Cells["Rotina"].Value.ToString() + " em row:" + row.Index.ToString());
                cExportar.ProcessarExportacao(row.Cells["Rotina"].Value.ToString(), row.Index);
            }
        }


        private void bgwExportar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grdExportacao.Rows[grdExportacao.Rows.Count-1].DefaultCellStyle.ForeColor = Color.Green;
            btnExportar.Text = "Exportação Realizada.";
            btnExportar.Enabled = false;
            Cursor.Current = Cursors.Default;

            cExportar.ExibirExportacao();
        }

        private void bgwImportar_DoWork(object sender, DoWorkEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in grdImportacao.Rows)
            {
                Console.WriteLine(row.Cells["Rotina"].Value.ToString() + " em row:" + row.Index.ToString());
                cImportar.ProcessarImportacao(row.Cells["Rotina"].Value.ToString(), row.Index);
            }
        }

        private void bgwImportar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grdImportacao.Rows[grdImportacao.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Green;
            grdImportacao.Rows[grdImportacao.Rows.Count - 1].Cells["Status"].Value = "Importado";
            btnImportar.Text = "Importação Realizada.";
            Cursor.Current = Cursors.Default;

            CarregarRepresentante();

            cImportar.ExibirImportacao();
        }

        private void aplicarQuantidadeInformadaToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Console.WriteLine("Aplicar Quantidade Informada em " + grdConfProduto.CurrentRow.Cells["CodigoBarras"].Value.ToString());
            cConferirProdutos.ResolverConflito(Convert.ToInt32(grdConfProduto.CurrentRow.Cells["ProdutoGradeId"].Value), Convert.ToInt32(grdConfProduto.CurrentRow.Cells["QuantidadeInformada"].Value));

        }

        private void aplicarQuantidadeDaCargaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Aplicar Quantidade Carga em " + grdConfProduto.CurrentRow.Cells["CodigoBarras"].Value.ToString());
            cConferirProdutos.ResolverConflito(Convert.ToInt32(grdConfProduto.CurrentRow.Cells["ProdutoGradeId"].Value), Convert.ToInt32(grdConfProduto.CurrentRow.Cells["QuantidadeCarga"].Value));
        }

        private void grdConfProduto_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                var hti = grdConfProduto.HitTest(e.X, e.Y);
                grdConfProduto.ClearSelection();
                grdConfProduto[hti.ColumnIndex, hti.RowIndex].Selected = true;

                if (grdConfProduto.CurrentRow.Cells["Tipo"].Value.ToString() == "I")
                {
                    smsConferenciaAdicionarSuplemento.Visible = true;
                    smsConferenciaQuantidadeInformada.Visible = false;
                    smsConferenciaQuantidadeCarga.Visible = false;
                } else
                {
                    smsConferenciaAdicionarSuplemento.Visible = false;

                    if (grdConfProduto.CurrentRow.Cells[3].Value != null)
                    {
                        if (grdConfProduto.CurrentRow.Cells[3].Value.ToString() != "0")
                        {
                            smsConferenciaQuantidadeInformada.Visible = true;
                        }
                        else
                        {
                            smsConferenciaQuantidadeInformada.Visible = false;
                        }
                    }

                    
                    smsConferenciaQuantidadeCarga.Visible = true;
                }



            } 



        }


        private void cmsResolverConflito_Opening(object sender, CancelEventArgs e)
        {
            //if (cConfProdutoRightMouse) e.Cancel = true;
        }

        private void txtSuplCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtSuplCodigoBarras.Text != "")
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void txtSuplCodigoBarras_Leave(object sender, EventArgs e)
        {
            //if (txtSuplCodigoBarras.Text != "")
            //{
            //    cSuplemento.PesquisarSuplementoProduto(txtSuplCodigoBarras.Text);
            //}
        }

        private void chkSuplQuantidade_CheckedChanged(object sender, EventArgs e)
        {
            if (cSuplemento.cProdutoGradeId != 0)
            {
                chkSuplQuantidade.Checked = true;
                txtSuplQuantidade.Enabled = true;
            }
            else
            {
                txtSuplQuantidade.Text = "";
                txtSuplQuantidade.Enabled = chkSuplQuantidade.Checked;
            }
        }

        private void txtSuplQuantidade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSuplConfirmar_Click(sender, e);

            }
        }

        private void btnSuplConfirmar_Click(object sender, EventArgs e)
        {
            cSuplemento.ConfirmarSuplemento();
        }

        private void btnSuplCancelar_Click(object sender, EventArgs e)
        {
            cSuplemento.SuplementoProdutoLimpar();
        }

        private void grdSuplemento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cSuplemento.EditarCargaProduto();
        }

        private void grdSuplemento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                cSuplemento.ExcluirCargaProduto();
            }
        }

        private void ofdBDArquivo_FileOk(object sender, CancelEventArgs e)
        {
            txtBDLocal.Text = ofdBDArquivo.FileName;
        }

        private void btnBDLocalProcurar_Click(object sender, EventArgs e)
        {
            ofdBDArquivo.ShowDialog();
        }

        private void btnRelatorioPedido_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSuplementoImprimir_Click(object sender, EventArgs e)
        {


        }

        private void smnRepresentante_Click(object sender, EventArgs e)
        {
            FormRepresentante formRepresentante = new FormRepresentante(this, lblUsuario.Text, lblNome.Text);
            formRepresentante.Show();
        }

        private void txtSuplCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            if (txtSuplCodigoBarras.Text != "")
            {
                cSuplemento.PesquisarSuplementoProduto(txtSuplCodigoBarras.Text);
            } else
            {
                //e.Cancel = true; //cancel the validation.
            }
        }


        private void txtConfCodigoBarras_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfCodigoBarras.Text != "")
            {
                cConferirProdutos.PesquisarConferenciaProduto(txtConfCodigoBarras.Text);
            }
        }

        private void smnRetorno_Click(object sender, EventArgs e)
        {

        }

        private void smnPesquisar_Click(object sender, EventArgs e)
        {


            Cursor.Current = Cursors.WaitCursor;
            Modal.FormListaCarga formPesquisa = new Modal.FormListaCarga(this, "Retorno");
            var result = formPesquisa.ShowDialog();

            if (result == DialogResult.OK)
            {

                txtImportarCodPraca.Text = formPesquisa.cPracaId.ToString();
                txtImportarCodPraca_ButtonClick(sender, e);
                txtImportarCodRepresentante.Text = formPesquisa.cRepresentanteId.ToString();
                txtImportarCodRepresentante_ButtonClick(sender, e);
                cbbImportarMesAno.Value = Convert.ToDateTime(formPesquisa.cAno.ToString() + "-" + formPesquisa.cMes.ToString() + "-01");


            }
        }

        private void smnImprimir_Click(object sender, EventArgs e)
        {


            Cursor.Current = Cursors.WaitCursor;

            smnSuplementoImprimir.Text = "Imprimindo...";
            smnSuplementoImprimir.Enabled = false;

            List<ModelLibrary.RelatoriosRepresentante.ListaProdutos> suplemento = ModelLibrary.RelatoriosRepresentante.RelatorioSuplemento();



            if (suplemento == null)
            {
                MessageBox.Show("Erro ao imprimir relatório - Não foi possível encontrar suplemento.", "Reder - Impressão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                BindingSource bs = new BindingSource();

                Reports.Suplemento relatoriosuplemento = new Reports.Suplemento();

                bs.DataSource = suplemento;
                relatoriosuplemento.SetDataSource(bs);


                relatoriosuplemento.PrintToPrinter(1, true, 0, 0);

                smnSuplementoImprimir.Text = "Imprimir";
                smnSuplementoImprimir.Enabled = true;
                Cursor.Current = Cursors.Default;


                //FormRelatorio formRelatorio = new FormRelatorio();
                //formRelatorio.Show();


                //formRelatorio.crvRelatorio.ReportSource = relatoriopedido;
                //formRelatorio.crvRelatorio.RefreshReport();
            }
        }

        private void smnSobre_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Modal.FormSobre formSobre = new Modal.FormSobre();
            formSobre.ShowDialog();
        }

        private void smsConferenciaExcluir_Click(object sender, EventArgs e)
        {
            cConferirProdutos.ExcluirCargaProdutoConferencia();
        }

        private void smsConferenciaAdicionarSuplemento_Click(object sender, EventArgs e)
        {
            cConferirProdutos.ResolverConflito(Convert.ToInt32(grdConfProduto.CurrentRow.Cells["ProdutoGradeId"].Value), Convert.ToInt32(grdConfProduto.CurrentRow.Cells["QuantidadeInformada"].Value));

            cSuplemento.ExibirSuplementoProduto(cCargaId);

        }

        private void smnConferenciaFinalizar_Click(object sender, EventArgs e)
        {
            grdConfProduto.Columns[2].Visible = true;
            grdConfProduto.Columns[4].Visible = true;
            grdConfProduto.Columns[5].Visible = true;
            grdConfProduto.Columns[11].Visible = true;


            ModelLibrary.MetodosRepresentante.FinalizarConferenciaProduto();

            cConferirProdutos.ExibirConferenciaProduto(cCargaId);
        }

        private void smnConferenciaRefazer_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Deseja Realmente refazer a conferencia de produtos?", "Reder Consignado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                ModelLibrary.MetodosRepresentante.RefazerConferenciaProduto();

                cConferirProdutos.ConferenciaProdutoLimpar();

                cConferirProdutos.ExibirConferenciaProduto(cCargaId);
            }
        }

        private void FormDeposito_FormClosing(object sender, FormClosingEventArgs e)
        {
            //

            if (!cConferirProdutos.Finalizado())
            {
                MessageBox.Show("A conferencia de produtos não foi finalizada. Resolva todos os conflitos antes de fechar o aplicativo.", "Conferencia não Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
            }
        }
    }
}
