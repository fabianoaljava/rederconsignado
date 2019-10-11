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

            List<ModelLibrary.ListaProdutos> produtosgrade = ModelLibrary.MetodosDeposito.ObterListaProdutos(vCriterio);

            BindingListView<ModelLibrary.ListaProdutos> view = new BindingListView<ModelLibrary.ListaProdutos>(produtosgrade);

            grdProdutoGrade.DataSource = view;

            grdProdutoGrade.ColumnHeadersHeight = 40;

            grdProdutoGrade.Columns[0].Width = 100;
            grdProdutoGrade.Columns[1].Width = 250;
            grdProdutoGrade.Columns[2].Width = 70;
            grdProdutoGrade.Columns[3].Width = 70;
            grdProdutoGrade.Columns[4].Width = 80;
            grdProdutoGrade.Columns[4].HeaderText = "Valor Saída";
            grdProdutoGrade.Columns[4].DefaultCellStyle.Format = "c";
            grdProdutoGrade.Columns[5].Width = 80;
            grdProdutoGrade.Columns[5].HeaderText = "Valor Custo";
            grdProdutoGrade.Columns[5].DefaultCellStyle.Format = "c";
            grdProdutoGrade.Columns[6].Width = 70;
            grdProdutoGrade.Columns[6].HeaderText = "Saldo em Estoque";
            grdProdutoGrade.Columns[7].Width = 80;
            grdProdutoGrade.Columns[7].DefaultCellStyle.Format = "c";
            grdProdutoGrade.Columns[8].Visible = false;




        }

        private void grdProdutoGrade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cProdutoGradeId = Convert.ToInt64(grdProdutoGrade.CurrentRow.Cells["ProdutoGradeId"].Value);
            btnConfirmar.Enabled = true;
        }

        private void grdProdutoGrade_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cProdutoGradeId > 0)
            {
                btnConfirmar_Click(sender, e);
            }
        }
    }
}
