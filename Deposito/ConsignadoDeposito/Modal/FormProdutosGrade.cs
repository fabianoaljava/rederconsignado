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


    public partial class FormProdutosGrade : MetroFramework.Forms.MetroForm
    {

        public long cProdutoGradeId { get; set; }
        public string cCodigo { get; set; }


        public FormProdutosGrade(string pCodigo)
        {
            InitializeComponent();

            this.cCodigo = pCodigo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.cProdutoGradeId = cProdutoGradeId;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormProdutosGrade_Load(object sender, EventArgs e)
        {

            var vCriterio = new Dictionary<string, string>();

            vCriterio["CodigoGeral"] = cCodigo;

            List<ModelLibrary.ListaRepProdutos> produtosgrade = ModelLibrary.MetodosRepresentante.ObterListaProdutos(vCriterio);

            BindingListView<ModelLibrary.ListaRepProdutos> view = new BindingListView<ModelLibrary.ListaRepProdutos>(produtosgrade);

            grdProdutoGrade.DataSource = view;

            grdProdutoGrade.Columns[6].Visible = false;


        }

        private void grdProdutoGrade_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdProdutoGrade_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
