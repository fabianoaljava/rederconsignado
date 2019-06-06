using System;
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



        public DepositoHome cHome;
        public Importar cImportar;
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
                }
                else
                {
                    //se importacao não foi realizada
                    lblCarga.Text = "Importação de Carga Pendente.";
                    cImportar.CarregarFormularioServer(); //obter praça e representante do servidor

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

                }
                else
                {
                    //se importacao não foi realizada
                    lblCarga.Text = "Importação de Carga Pendente.";
                    /// Exibe mensagem se deseja conectar com o banco de dados até que a conexão seja efetuada / com opção para alterar informações de conexão ou pesquisar conexão (futuro)


                    if (MessageBox.Show("O servidor <<SERVER>> não foi localizado ou está desconectado. Deseja tentar novamente?", "Reder Consignado", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        CarregarRepresentante();
                    }


                }

            }
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
                btnImportar.Focus();

            }
        }


        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            string objname = ((MetroFramework.Controls.MetroTextBox)sender).Name;


            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (objname == "txtCargaCodRepresentante")
                {
                    txtImportarCodRepresentante_ButtonClick(sender, e);
                    cbbImportarMesAno.Focus();
                }
                else if (objname == "txtCargaCodPraca")
                {
                    txtImportarCodPraca_ButtonClick(sender, e);
                    txtImportarCodRepresentante.Focus();
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }

                e.Handled = true;//set to false if you need that textbox gets enter key
            }
        }


        private void btnImportar_Click(object sender, EventArgs e)
        {

            cImportar.ImportarCarga();
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
                cImportar.ExcluirImportacao();
            }
        }

        ////////////////////////////////////////
        /// IMPORTAR - CONFERIR PRODUTOS
        ////////////////////////////////////////

        private void txtConfCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cImportar.PesquisarConferenciaProduto(txtConfCodigoBarras.Text);

            }
        }

        private void txtConfCodigoBarras_Leave(object sender, EventArgs e)
        {
            if (txtConfCodigoBarras.Text != "")
            {
                cImportar.PesquisarConferenciaProduto(txtConfCodigoBarras.Text);
            }
        }


        private void btnConferenciaConfirmar_Click(object sender, EventArgs e)
        {

            cImportar.ConfirmarCargaProdutoConferencia();

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

        private void grdConfProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cImportar.EditarConferenciaProduto();
        }


        private void grdConfProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {

                cImportar.ExcluirCargaProdutoConferencia();
            }

        }

        private void grdConfProduto_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            if (grdConfProduto.Rows[e.RowIndex].Cells[3].Value != null) {
                if (Convert.ToInt32(grdConfProduto.Rows[e.RowIndex].Cells[4].Value.ToString()) > 0)
                {
                    grdConfProduto.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
                }
                else if (Convert.ToInt32(grdConfProduto.Rows[e.RowIndex].Cells[4].Value.ToString()) == 0)
                {
                    grdConfProduto.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
                }
                else
                {
                    grdConfProduto.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            
            
        }

        private void btnConfCancelar_Click(object sender, EventArgs e)
        {
            cImportar.ConferenciaProdutoLimpar();
        }


        ////////////////////////////////////////
        /// ORGANIZAR DAQUI PRA BAIXO
        ////////////////////////////////////////





    }
}
