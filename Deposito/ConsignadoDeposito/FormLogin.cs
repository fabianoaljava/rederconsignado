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

namespace ConsignadoDeposito
{
    public partial class FormLogin : Form
    {


        public string Nome;

        public FormLogin()
        {
            InitializeComponent();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {

            // ===>>> Colocar preloader depois.

            try
            {


                btnLogin.Enabled = false;
                btnLogin.Text = "Entrando...";
                Cursor.Current = Cursors.WaitCursor;


                string[] vAutenticacao = null;

                vAutenticacao = ModelLibrary.MetodosDeposito.Autenticar(txtLogin.Text, txtSenha.Text);


                if (vAutenticacao[0] == "Y")
                {

                    if (vAutenticacao[2] == "DP")
                    {
                        FormDeposito formDeposito = new FormDeposito(this, txtLogin.Text, vAutenticacao[3]);
                        formDeposito.Show();
                    }
                    else
                    {

                        MessageBox.Show("Você não tem acesso a este módulo.", "Reder Software", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnLogin.Enabled = true;
                        btnLogin.Text = "Entrar";
                        Cursor.Current = Cursors.Default;
                    }

                }
                else
                {

                    MessageBox.Show(vAutenticacao[1]); /// ==> Colocar em label no form depois
                    btnLogin.Enabled = true;
                    btnLogin.Text = "Entrar";
                    Cursor.Current = Cursors.Default;

                }

           } catch(IOException vE)
            {
                Console.WriteLine(vE.Message);
                MessageBox.Show("Ocorreu um erro ao processar a autenticação do usuário. Caso o problema persista, entre em contato com o suporte técnico.", "Reder Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin.Enabled = true;
                btnLogin.Text = "Entrar";
                Cursor.Current = Cursors.Default;

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
    }
}
