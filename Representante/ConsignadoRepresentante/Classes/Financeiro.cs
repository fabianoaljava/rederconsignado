using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using System.Drawing;

namespace ConsignadoRepresentante
{
    public partial class Financeiro
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////


        public FormRepresentante localRepresentanteForm = null;


        public Financeiro(FormRepresentante formRepresentante)
        {

            localRepresentanteForm = formRepresentante;

        }


        public void CarregarFormulario()
        {


            ExibirPosicaoFinancera();

        }

        public void ExibirPosicaoFinancera()
        {


            List<ModelLibrary.ListaRepPosicaoFinanceira> financeiro = ModelLibrary.MetodosRepresentante.ObterPosicaoFinanceira();

            BindingListView<ModelLibrary.ListaRepPosicaoFinanceira> view = new BindingListView<ModelLibrary.ListaRepPosicaoFinanceira>(financeiro);


            localRepresentanteForm.grdPosicaoFinanceira.DataSource = view;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[0].Width = 100;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[1].Width = 300;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[2].DefaultCellStyle.Format = "n";
            localRepresentanteForm.grdPosicaoFinanceira.Columns[3].DefaultCellStyle.Format = "n";
            localRepresentanteForm.grdPosicaoFinanceira.Columns[4].DefaultCellStyle.Format = "n";


            foreach (DataGridViewRow row in localRepresentanteForm.grdPosicaoFinanceira.Rows)
            {



                //if (Convert.ToDecimal(localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].Cells[4].Value.ToString()) == 0)
                //{
                //    localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].DefaultCellStyle.ForeColor = Color.Green;
                //}

                //if (Convert.ToDecimal(localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].Cells[4].Value.ToString()) > 0)
                //{
                //    localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].DefaultCellStyle.ForeColor = Color.DeepSkyBlue;
                //}

                //if (localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].Cells[5].Value.ToString() == localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].Cells[6].Value.ToString())
                //{
                //    localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].DefaultCellStyle.ForeColor = Color.DarkOrange;
                //}



                //if (Convert.ToInt32(localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].Cells[6].Value.ToString()) == 0)
                //{
                //    localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].DefaultCellStyle.ForeColor = Color.Red;
                //}

                //if (Convert.ToInt32(localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].Cells[6].Value.ToString()) == 0 && Convert.ToDecimal(localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].Cells[3].Value.ToString()) == 0)
                //{
                //    localRepresentanteForm.grdPosicaoFinanceira.Rows[row.Index].DefaultCellStyle.ForeColor = Color.Purple;
                //}
            }


            localRepresentanteForm.grdPosicaoFinanceira.Columns[5].Visible = false;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[6].Visible = false;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[7].Visible = false;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[8].Visible = false;






        }

    }
}
