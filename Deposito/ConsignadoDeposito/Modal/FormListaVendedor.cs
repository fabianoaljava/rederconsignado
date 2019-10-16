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
    public partial class FormListaVendedor : MetroFramework.Forms.MetroForm
    {

        public int cVendedorId;
        public string cVendedorNome;

        public FormListaVendedor()
        {
            InitializeComponent();       
        }


        public void ControlOnlyInt(object sender, KeyPressEventArgs e)
        {


            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }


        }

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


        public void ValidarCPFCnpj(string pCPFCnpj, object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (pCPFCnpj != "")
            {
                //verificar se o CPF/CNPJ é valido
                if (!ControllerLibrary.Funcoes.CpfCnpjUtils.IsValid(pCPFCnpj))
                {
                    MessageBox.Show("CPF/CNPJ Inválido!");
                    e.Cancel = true;
                    
                } 

                
            } 


        }

        public void LimparPesquisa()
        {
            txtCodigo.Text = "";
            txtCPFCnpj.Text = "";
            txtNome.Text = "";


            this.cVendedorId = 0;

            CarregarListaVendedor();

        }

        private void CarregarListaVendedor()
        {
            
            List<ModelLibrary.ListaVendedor> vendedor = ModelLibrary.MetodosDeposito.PesquisarVendedor((txtCodigo.Text == "")?0: Convert.ToInt32(txtCodigo.Text), txtCPFCnpj.Text, txtNome.Text);

            BindingListView<ModelLibrary.ListaVendedor> view = new BindingListView<ModelLibrary.ListaVendedor>(vendedor);

            grdVendedor.DataSource = view;



            grdVendedor.Columns[0].Width = 20;
            grdVendedor.Columns[1].Width = 230;
            grdVendedor.Columns[2].Width = 90;
            grdVendedor.Columns[3].Width = 110;
            grdVendedor.Columns[4].Width = 30;
            grdVendedor.Columns[5].Width = 230;






        }

        private void FormListaVendedor_Load(object sender, EventArgs e)
        {
            CarregarListaVendedor();
        }

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                CarregarListaVendedor();
            }
        }

        private void grdVendedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cVendedorId = Convert.ToInt32(grdVendedor.CurrentRow.Cells["Id"].Value);
            this.cVendedorNome = grdVendedor.CurrentRow.Cells["Nome"].Value.ToString();

            btnConfirmar.Enabled = true;
        }

        private void grdVendedor_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cVendedorId > 0)
            {
                btnConfirmar_Click(sender, e);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparPesquisa();
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparPesquisa();
        }

        private void txtCPFCnpj_Validated(object sender, EventArgs e)
        {
            txtCPFCnpj.Text = MascaraCnpjCpf(txtCPFCnpj.Text);
            CarregarListaVendedor();
        }

        private void txtCPFCnpj_Validating(object sender, CancelEventArgs e)
        {
            ValidarCPFCnpj(txtCPFCnpj.Text, sender, e);
        }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNome.Text.Length >= 3)
            {
                CarregarListaVendedor();
            }
        }
    }
}
