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
        public int Id { get; set; }
        public int CargaId { get; set; }
        public int VendedorId { get; set; }
        public int ReceberId { get; set; }
        public int PedidoId { get; set; }
        public string CodigoPedido { get; set; }
    }
}
