using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignadoDeposito.Forms
{
    public partial class FormNovoPedido : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;

        public Retorno cRetorno;

        public FormNovoPedido(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;


            cRetorno = new Retorno(formDeposito);
        }



        public void CarregarListaVendedor()
        {

            cbbVendedor.DataSource = ModelLibrary.MetodosDeposito.ObterListaVendedor();
            cbbVendedor.DisplayMember = "Nome";
            cbbVendedor.ValueMember = "Id";
            cbbVendedor.Invalidate();
            cbbVendedor.SelectedIndex = -1;

            cbbVendedor.SelectedIndexChanged += Vendedor_Change;

        }



        public void Vendedor_Change(object sender, EventArgs e)
        {
            if (cbbVendedor.SelectedIndex >= 0)
            {

                ModelLibrary.Vendedor vendedor = (ModelLibrary.Vendedor)cbbVendedor.SelectedItem;



                //verificar se vendedor possui pedidos fechados 


                txtResultado.Text = (vendedor != null) ? vendedor.CpfCnpj : "Não encontrado";
                


            }

        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //cRetorno.ExibirDetalhesPedido();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FormNovoPedido_Load(object sender, EventArgs e)
        {
            CarregarListaVendedor();
        }
    }
}
