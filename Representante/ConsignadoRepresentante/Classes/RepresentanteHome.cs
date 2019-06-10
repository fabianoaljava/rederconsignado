using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;

namespace ConsignadoRepresentante
{
    public partial class RepresentanteHome
    {
        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////


        public FormRepresentante localRepresentanteForm = null;

        public RepresentanteHome(FormRepresentante formRepresentante)
        {

            localRepresentanteForm = formRepresentante;

        }

        public void CarregarFormulario()
        {

            localRepresentanteForm.grdHome.DataSource = ModelLibrary.MetodosRepresentante.ObterListaVendedorHome(localRepresentanteForm.cCargaId);

            localRepresentanteForm.grdHome.Columns[0].Width = 40;
            localRepresentanteForm.grdHome.Columns[0].HeaderText = "Cód.";
            localRepresentanteForm.grdHome.Columns[1].Width = 200;
            localRepresentanteForm.grdHome.Columns[3].Width = 220;
            localRepresentanteForm.grdHome.Columns[5].Width = 60;
            localRepresentanteForm.grdHome.Columns[6].Width = 80;
            localRepresentanteForm.grdHome.Columns[7].Width = 100;
            localRepresentanteForm.grdHome.Columns[8].Width = 70;
            localRepresentanteForm.grdHome.Columns[9].Width = 60;
            localRepresentanteForm.grdHome.Columns[10].Width = 70;
            localRepresentanteForm.grdHome.Columns[11].Width = 80;
            localRepresentanteForm.grdHome.Columns[12].Width = 70;


        }



    }
}
