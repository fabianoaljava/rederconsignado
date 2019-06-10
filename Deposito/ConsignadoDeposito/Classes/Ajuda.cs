using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignadoDeposito;
using System.Windows.Forms;

namespace ConsignadoDeposito
{
    public partial class Ajuda
    {

        //////////////////////////////
        /// Declaração de Variáveis
        //////////////////////////////


        public FormDeposito localDepositoForm = null;

        public Ajuda(FormDeposito formDeposito)
        {

            localDepositoForm = formDeposito;

        }
    }
}
