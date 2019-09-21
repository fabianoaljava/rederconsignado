using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignadoDeposito.Forms
{
    public partial class FormProduto : MetroFramework.Forms.MetroForm
    {

        public FormDeposito localDepositoForm = null;

        public FormProduto(FormDeposito formDeposito)
        {
            InitializeComponent();

            localDepositoForm = formDeposito;
        }
    }
}
