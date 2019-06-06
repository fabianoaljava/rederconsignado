using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignadoRepresentante;

namespace ConsignadoRepresentante
{
    public partial class Exportar
    {
        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        ///
        public FormDeposito localDeposito = null;

        public Exportar(FormDeposito formDeposito)
        {

            localDeposito = formDeposito;

        }
    }
}
