using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;

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
    }
}
