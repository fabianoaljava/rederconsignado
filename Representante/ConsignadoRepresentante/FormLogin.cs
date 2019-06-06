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

namespace ConsignadoRepresentante
{
    public partial class FormLogin : Form
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////

        public string Nome;

        public FormLogin()
        {
            InitializeComponent();
        }


        ////////////////////////////////////////
        /// Funções do Formulário Principal
        ////////////////////////////////////////

        /// <summary>
        /// Verifica se a importação foi realizada 
        /// </summary>
        public void VerificarImportacao()
        {

            lblAutenticacao.Visible = true;
            lblAutenticacao.Text = "Verificando importação...";
            pgbAutenticacao.Visible = true;

            /// Se a importação foi realizada
            if (ModelLibrary.MetodosRepresentante.VerificarImportacao())
            {                
                AutenticarLocal();           /// Realiza Autenticação Local     
            } else
            {                
                AutenticarServer();         /// Realiza autenticação com o servidor
            }

        }

        /// <summary>
        /// Limpa o formulário de login
        /// </summary>
        public void LoginLimpar()
        {

            lblAutenticacao.Visible = false;
            lblAutenticacao.Text = "";
            pgbAutenticacao.Value = 0;
            pgbAutenticacao.Visible = false;
            btnLogin.Text = "Entrar";
            btnLogin.Enabled = true;

        }

        /// <summary>
        /// Realiza a autenticação no servidor
        /// </summary>
        public void AutenticarServer()
        {
            try
            {

                lblAutenticacao.Text = "Autenticando com o servidor...";
                pgbAutenticacao.Value = 50;

                string[] vAutenticacao = null;

                vAutenticacao = ModelLibrary.MetodosDeposito.Autenticar(txtLogin.Text, txtSenha.Text);


                if (vAutenticacao[0] == "Y")
                {
                    lblAutenticacao.Text = "Abrindo formulário...";
                    pgbAutenticacao.Value = 70;

                    if (vAutenticacao[2] == "DP")
                    {
                        FormDeposito formDeposito = new FormDeposito(this, txtLogin.Text, vAutenticacao[3]);
                        formDeposito.Show();
                    }
                    else
                    {

                        lblAutenticacao.Text = "Acesso Negado!";
                        pgbAutenticacao.Value = 100;
                        pgbAutenticacao.Visible = false;

                        MessageBox.Show("Você não tem acesso a este módulo, ou a importação ainda não foi realizada.", "Reder Software", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoginLimpar();

                    }

                }
                else
                {

                    lblAutenticacao.Text = vAutenticacao[1];
                    pgbAutenticacao.Value = 100;
                    pgbAutenticacao.Visible = false;

                    MessageBox.Show(vAutenticacao[1]); /// ==> Colocar em label no form depois

                    LoginLimpar();

                }

            }
            catch (IOException vE)
            {

                Console.WriteLine(vE.Message);
                lblAutenticacao.Text = "Erro!";
                pgbAutenticacao.Value = 100;
                pgbAutenticacao.Visible = false;
                MessageBox.Show("Ocorreu um erro ao processar a autenticação do usuário. Caso o problema persista, entre em contato com o suporte técnico.", "Reder Software", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoginLimpar();
            }


        }


        /// <summary>
        /// Realiza a autenticação local
        /// </summary>
        public void AutenticarLocal()
        {
            try
            {

                lblAutenticacao.Text = "Autenticando banco de dados local...";
                pgbAutenticacao.Value = 50;

                string[] vAutenticacao = null;

                vAutenticacao = ModelLibrary.MetodosRepresentante.Autenticar(txtLogin.Text, txtSenha.Text);


                if (vAutenticacao[0] == "Y")
                {
                    lblAutenticacao.Text = "Abrindo formulário...";
                    pgbAutenticacao.Value = 70;

                    if (vAutenticacao[2] == "DP")
                    {
                       

                        FormDeposito formDeposito = new FormDeposito(this, txtLogin.Text, vAutenticacao[3]);
                        formDeposito.Show();
                    }
                    else
                    {
                        FormRepresentante formRepresentante = new FormRepresentante(this, txtLogin.Text, vAutenticacao[3]);
                        formRepresentante.Show();
                    }

                }
                else
                {
                    lblAutenticacao.Text = vAutenticacao[1];
                    pgbAutenticacao.Value = 100;
                    pgbAutenticacao.Visible = false;
                    

                    MessageBox.Show(vAutenticacao[1]); /// ==> Colocar em label no form depois

                    LoginLimpar();

                }

            }
            catch (IOException vE)
            {
                Console.WriteLine(vE.Message);
                lblAutenticacao.Text = "Erro!";
                pgbAutenticacao.Value = 100;
                pgbAutenticacao.Visible = false;                
                MessageBox.Show("Ocorreu um erro ao processar a autenticação do usuário. Caso o problema persista, entre em contato com o suporte técnico.", "Reder Software", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoginLimpar();
            }
        }


        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        ////////////////   FUNÇÕES DO FORM  //////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Text = "Entrando...";
            btnLogin.Enabled = false;
            // ===>>> Colocar preloader depois.
            VerificarImportacao();

        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin_Click(sender, e);
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


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }




    }
}
