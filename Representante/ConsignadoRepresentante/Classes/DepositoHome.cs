using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoRepresentante;
using System.Windows.Forms;

namespace ConsignadoRepresentante
{
    public partial class DepositoHome
    {
        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////
        ///
        public FormDeposito localDeposito = null;

        public DepositoHome(FormDeposito formDeposito)
        {

            localDeposito = formDeposito;

        }


    }
}
