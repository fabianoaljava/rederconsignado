using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;

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

            localRepresentanteForm.grdPosicaoFinanceira.DataSource = ModelLibrary.MetodosRepresentante.ObterPosicaoFinanceira();
            localRepresentanteForm.grdPosicaoFinanceira.Columns[0].Width = 100;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[1].Width = 300;
            localRepresentanteForm.grdPosicaoFinanceira.Columns[2].DefaultCellStyle.Format = "c";
            localRepresentanteForm.grdPosicaoFinanceira.Columns[3].DefaultCellStyle.Format = "c";
            localRepresentanteForm.grdPosicaoFinanceira.Columns[4].DefaultCellStyle.Format = "c";



        }
    }
}
