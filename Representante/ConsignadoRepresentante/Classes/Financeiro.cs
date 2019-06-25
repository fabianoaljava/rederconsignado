using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;
using Equin.ApplicationFramework;

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



        }

    }
}
