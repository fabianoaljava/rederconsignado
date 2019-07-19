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
    public partial class Estoque
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////


        public FormRepresentante localRepresentanteForm = null;


        public Estoque(FormRepresentante formRepresentante)
        {

            localRepresentanteForm = formRepresentante;

        }

        public void ExibirEstoque()
        {

            List<ModelLibrary.ListaRepEstoque> estoque = ModelLibrary.MetodosRepresentante.ObterListaEstoque();

            BindingListView<ModelLibrary.ListaRepEstoque> view = new BindingListView<ModelLibrary.ListaRepEstoque>(estoque);

            localRepresentanteForm.grdEstoque.DataSource = view;

            localRepresentanteForm.grdEstoque.Columns[1].Width = 250;
            localRepresentanteForm.grdEstoque.Columns[9].DefaultCellStyle.Format = "c";

        }
    }
}
