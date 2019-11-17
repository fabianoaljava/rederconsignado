using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaRecebimento
    {
        public string Referencia { get; set; }
        public double ValorRecebido { get; set; }
        public DateTime DataPagamento { get; set; }
        public string FormaPagamento { get; set; }
        public string Observacao { get; set; }
        public long Id { get; set; }
        public long CargaId { get; set; }
        public long VendedorId { get; set; }
        public long ReceberId { get; set; }
        public long PedidoId { get; set; }
        public string CodigoPedido { get; set; }
    }
}
