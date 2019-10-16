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
    public partial class FormReceberBaixa : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;
        public string cModo;
        public int cAReceberBaixaId;
        public int cReceberId;


        public FormReceberBaixa(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;

        }

        private void CarregarReceberBaixa()
        {

            List<ModelLibrary.ListaReceberBaixa> receberbaixa = ModelLibrary.MetodosDeposito.ObterListaReceberBaixa(cReceberId);

            BindingListView<ModelLibrary.ListaReceberBaixa> view = new BindingListView<ModelLibrary.ListaReceberBaixa>(receberbaixa);

            grdReceberBaixa.DataSource = view;


            grdReceberBaixa.Columns[0].Visible = false;
            grdReceberBaixa.Columns[1].Visible = false;
            grdReceberBaixa.Columns[2].DefaultCellStyle.Format = "c";


        }


        public void Limpar()
        {


        }


        public void CarregarPagamento()
        {

        }

        public void AdicionarPagamento()
        {

        }

        public void AlterarPagamento()
        {


        }

        public void ExcluirPagamento()
        {


        }

        

        private void FormReceberBaixa_Load(object sender, EventArgs e)
        {

            cReceberId = Convert.ToInt32(localDepositoForm.grdContasAReceber.CurrentRow.Cells["Id"].Value);

            CarregarReceberBaixa();

            
        }
    }
}
