using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoDeposito;
using System.Windows.Forms;

namespace ConsignadoDeposito
{
    public partial class Home
    {


        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////


        public FormDeposito localDepositoForm = null;

        public Home(FormDeposito formDeposito)
        {

            localDepositoForm = formDeposito;

        }


    }
}
