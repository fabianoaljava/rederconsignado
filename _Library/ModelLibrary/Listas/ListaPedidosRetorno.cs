using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaPedidosRetorno
    {

        public string CodigoPedido { get; set; }
        public int VendedorId { get; set; }
        public string Nome { get; set; }
        public Nullable<double> ValorPedido { get; set; }
        public Nullable<DateTime> DataLancamento { get; set; }
        public string Status { get; set; }

    }
}
