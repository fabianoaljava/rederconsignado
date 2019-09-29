using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaPedidoItem
    {

        public int PedidoItemId { get; set; }
        public string CodigoBarras { get; set; }
        public string NomeProduto { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<double> Retorno { get; set; }
        public Nullable<double> Preco { get; set; }

    }
}
