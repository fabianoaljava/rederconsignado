using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using ConsignadoRepresentante;


namespace ConsignadoRepresentante
{
    public partial class FormRepresentante : MetroFramework.Forms.MetroForm
    {
        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////

        public long cCargaId;

        public RepresentanteHome cHome;
        public Vendedor cVendedor;
        public Financeiro cFinanceiro;
        public Produto cProduto;
        public Estoque cEstoque;
        public Ajuda cAjuda;



        public System.Windows.Forms.Form loginWindow = null;


        /// <summary>
        /// Inicialização do Formulário Representante
        /// </summary>
        /// <param name="loginForm">Formulário de Login</param>
        /// <param name="pUsuario">Login do Usuário Autenticado</param>
        /// <param name="pNome">Nome do Usuário Autenticado</param>
        public FormRepresentante(System.Windows.Forms.Form loginForm, string pUsuario, string pNome)
        {
            InitializeComponent();

            this.loginWindow = loginForm;
            this.loginWindow.Hide();
            
            lblUsuario.Text = pUsuario;
            lblNome.Text = pNome;


            cHome = new RepresentanteHome(this);
            cVendedor = new Vendedor(this);
            cFinanceiro = new Financeiro(this);
            cProduto = new Produto(this);
            cEstoque = new Estoque(this);
            cAjuda = new Ajuda(this);


        }


        ////////////////////////////////////////
        /// Funções do Formulário Principal
        ////////////////////////////////////////

        /// <summary>
        /// Rotina de Carregamento do Formulário. Verifica se o servidor está conectado e se a importação foi realizada
        /// Caso de Sucesso: CarregarFormulario()
        /// </summary>
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
                    CarregarFormulario();
                    /// Exibir botão para Sincronizar (Futuro)
                }
                else
                {
                    //se importacao não foi realizada
                    lblCarga.Text = "Importação de Carga Pendente.";
                    MessageBox.Show("Este módulo não pode ser acessado sem a realização da importação da carga. A importação da base de dados pode não ter sido realizada corretamente. Por favor entre em contato com o depósito e solicite uma nova importação da carga. \n \n Essa aplicação será encerrada.", "Reder Consignado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();

                }
            }
            else // se não estiver conectado ao servidor
            {
                //verifica se importação foi realizada
                if (ModelLibrary.MetodosRepresentante.VerificarImportacao())
                {
                    //se importacao foi realizada
                    lblCarga.Text = "Carga Obtida.";
                    CarregarFormulario();
                }
                else
                {
                    //se importacao não foi realizada
                    lblCarga.Text = "Importação de Carga Pendente.";
                    MessageBox.Show("Este módulo não pode ser acessado sem a realização da importação da carga. A importação da base de dados pode não ter sido realizada corretamente. Por favor entre em contato com o depósito e solicite uma nova importação da carga. \n \n Essa aplicação será encerrada.", "Reder Consignado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();


                }

            }
        }


        private void CarregarFormulario()
        {
            ExibirCarga();

            cHome.CarregarFormulario();
            cVendedor.CarregarFormulario();
            cFinanceiro.CarregarFormulario();


        }

        /// <summary>
        /// Rotina para Obter a Carga Atual e carregar os labels
        /// </summary>
        private void ExibirCarga()
        {

            //obtem dados da carga (local) com tudo bloquead

            var carga = ModelLibrary.MetodosRepresentante.ObterCargaAtual();

            int cargaId = Convert.ToInt32(carga.Id);

            cCargaId = cargaId;

            int representanteId = Convert.ToInt32(carga.RepresentanteId);
            int pracaId = Convert.ToInt32(carga.PracaId);

            var praca = ModelLibrary.MetodosRepresentante.ObterPraca(pracaId);
            
            var representante = ModelLibrary.MetodosRepresentante.ObterRepresentante(representanteId);
            
            
            lblCarga.Text += " " + praca.Descricao.Trim() + " | " + representante.Nome.Trim() + " | " + carga.Mes.ToString() + "/" + carga.Ano.ToString() ;            


        }


        ///////////////////////////////////////////////
        /// Mascaras do Formulário
        ///////////////////////////////////////////////

        public string RemoverMascaraCnpjCpf(string pCnpjCpf)
        {

            pCnpjCpf = pCnpjCpf.Replace(".", "");
            pCnpjCpf = pCnpjCpf.Replace("-", "");
            pCnpjCpf = pCnpjCpf.Replace("/", "");

            return pCnpjCpf;
        }

        public string MascaraCnpjCpf(string pCnpjCpf)
        {
            string result = "";

            pCnpjCpf = pCnpjCpf.Replace(".", "");
            pCnpjCpf = pCnpjCpf.Replace("-", "");
            pCnpjCpf = pCnpjCpf.Replace("/", "");

            Console.WriteLine(pCnpjCpf.Length.ToString());

            if (pCnpjCpf.Length == 14)
            {
                result = pCnpjCpf.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            }
            if (pCnpjCpf.Length == 11)
            {
                result = pCnpjCpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            }
            if ((pCnpjCpf.Length != 11) && (pCnpjCpf.Length != 14))
            {
                result = pCnpjCpf;
            }
            return result;
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



        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////////   FUNÇÕES DO FORM  //////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////



        private void FormRepresentante_Load(object sender, EventArgs e)
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

        private void FormRepresentante_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        ////////////////////////////////////////
        /// Aba Vendedor
        ////////////////////////////////////////



        private void txtVendedorPesqCpfCnpj_Validated(object sender, EventArgs e)
        {
            txtVendedorPesqCpfCnpj.Text = MascaraCnpjCpf(txtVendedorPesqCpfCnpj.Text);
        }

        private void txtVendedorPesqCpfCnpj_Leave(object sender, EventArgs e)
        {
            cVendedor.ValidarCPFCnpj(txtVendedorPesqCpfCnpj.Text);
        }

        private void btnVendedorPesquisar_Click(object sender, EventArgs e)
        {
            cVendedor.VendedorPesquisar();
        }

        private void btnVendedorLimpar_Click(object sender, EventArgs e)
        {
            cVendedor.VendedorLimpar();
        }


        private void cbbTipoPessoa_SelectedValueChanged(object sender, EventArgs e)
        {
            cVendedor.SelecionarTipoPessoa(cbbTipoPessoa.Text);
        }

        private void txtCPFCnpj_Enter(object sender, EventArgs e)
        {
            txtCPFCnpj.Text = RemoverMascaraCnpjCpf(txtCPFCnpj.Text);
        }

        private void txtCPFCnpj_Leave(object sender, EventArgs e)
        {

            txtCPFCnpj.Text = MascaraCnpjCpf(txtCPFCnpj.Text);

            cVendedor.ValidarCPFCnpj(txtCPFCnpj.Text);

            if (cVendedor.cVendedorModo == "Create") cVendedor.VerificarCPFCnpjExistente(txtCPFCnpj.Text);
        }


        private void txtCep_ButtonClick(object sender, EventArgs e)
        {

            var endereco = new string[4];


            txtEndereco.Text = "Carregando...";

            endereco = ControllerLibrary.Funcoes.ObterEnderecoCep(txtCep.Text);

            cbbUF.Text = endereco[0];
            cbbCidade.Text = endereco[1];
            txtBairro.Text = endereco[2];
            txtEndereco.Text = endereco[3];
        }



        private void btnVendedorNovo_Click(object sender, EventArgs e)
        {
            cVendedor.VendedorIncluir();
        }

        private void btnVendedorCancelar_Click(object sender, EventArgs e)
        {
            cVendedor.VendedorLimpar();
        }

        private void btnVendedorSalvar_Click(object sender, EventArgs e)
        {
            cVendedor.VendedorSalvar();
        }

        ////////////////////////////////////////
        /// ORGANIZAR DAQUI PRA BAIXO
        ////////////////////////////////////////
        ///

        private void grdHome_DoubleClick(object sender, EventArgs e)
        {
            cVendedor.VendedorExibir(Convert.ToInt32(grdHome.CurrentRow.Cells[0].Value));
            tbcPrincipal.SelectedTab = tabVendedores;
            tbcVendedor.SelectedTab = tabVendedorInicio;
        }

        private void txtPedidoCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtPedidoCodigoBarras.Text != "")
                {
                    e.SuppressKeyPress = true;
                    cVendedor.PedidoPesquisar(txtPedidoCodigoBarras.Text);
                }

            }
        }

        private void txtPedidoCodigoBarras_Leave(object sender, EventArgs e)
        {
            if (txtPedidoCodigoBarras.Text != "")
            {
                cVendedor.PedidoPesquisar(txtPedidoCodigoBarras.Text);
            }
        }

        private void btnPedidoConfirmar_Click(object sender, EventArgs e)
        {
            cVendedor.ConfirmarPedido();

        }

        private void btnPedidoCancelar_Click(object sender, EventArgs e)
        {
            cVendedor.PedidoLimpar();
        }

        private void grdVendedorPedido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cVendedor.PedidoEditar();
        }

        private void grdVendedorPedido_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {

                cVendedor.PedidoExcluir();
            }

        }

        private void txtProdRetCodBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtRetornoCodigoBarras.Text != "")
                {
                    e.SuppressKeyPress = true;
                    cVendedor.RetornoProdutoPesquisar(txtRetornoCodigoBarras.Text);
                }

            }
        }

        private void txtProdRetCodBarras_Leave(object sender, EventArgs e)
        {
            if (txtRetornoCodigoBarras.Text != "")
            {
                cVendedor.RetornoProdutoPesquisar(txtRetornoCodigoBarras.Text);
            }
        }

        private void btnProdRetConfirmar_Click(object sender, EventArgs e)
        {

            cVendedor.ConfirmarRetornoProduto();

        }

        private void btnProdRetCancelar_Click(object sender, EventArgs e)
        {
            cVendedor.RetornoProdutoLimpar();
        }

        private void grdVendedorRetorno_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cVendedor.RetornoProdutoEditar(); 
        }

        private void txtValorRecebido_Leave(object sender, EventArgs e)
        {
            cVendedor.CalcularValorEmAberto();
        }

        private void btnAcertoConfirmar_Click(object sender, EventArgs e)
        {
            cVendedor.ReceberAcerto();
        }

        private void btnAcertoCancelar_Click(object sender, EventArgs e)
        {
            txtValorRecebido.Text = "";
            cVendedor.CalcularValorEmAberto();
        }



        private void grdFinanceiroRecebimentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cVendedor.ExibirDuplicata();
        }

        private void btnDuplicataConfirmar_Click(object sender, EventArgs e)
        {
            cVendedor.ReceberDuplicata();
        }

        private void btnDuplicataCancelar_Click(object sender, EventArgs e)
        {
            cVendedor.DuplicataLimpar();
        }

        private void chkVendedorListaFiltrar(object sender, EventArgs e)
        {
            
            cHome.FiltrarListaVendedores();
        }

        private void chkVendedorSemPedidoAtual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVendedorSemPedidoAtual.Checked) chkVendedorComPedidoAtual.Checked = !chkVendedorSemPedidoAtual.Checked;
            cHome.FiltrarListaVendedores();

        }

        private void chkVendedorComPedidoAtual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVendedorComPedidoAtual.Checked) chkVendedorSemPedidoAtual.Checked = !chkVendedorComPedidoAtual.Checked;
            cHome.FiltrarListaVendedores();

        }

        private void grdHome_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cVendedor.VendedorExibir(Convert.ToInt32(grdHome.CurrentRow.Cells[0].Value));
            tbcPrincipal.SelectedTab = tabVendedores;
            tbcVendedor.SelectedTab = tabVendedorInicio;
        }

        private void grdPosicaoFinanceira_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            


            try
            {

                if (Convert.ToDecimal(grdPosicaoFinanceira.Rows[e.RowIndex].Cells[4].Value.ToString()) == 0)
                {
                    grdPosicaoFinanceira.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
                }

                if (Convert.ToDecimal(grdPosicaoFinanceira.Rows[e.RowIndex].Cells[4].Value.ToString()) > 0)
                {
                    grdPosicaoFinanceira.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DeepSkyBlue;
                }
            }
            catch
            {
                /// escrever no log
            }

        }



        private void grdPosicaoFinanceira_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cVendedor.VendedorExibir(Convert.ToInt32(grdPosicaoFinanceira.CurrentRow.Cells[0].Value));

            tbcPrincipal.SelectedTab = tabVendedores;
            tbcVendedor.SelectedTab = tabVendedorInicio;
        }
    }
}
