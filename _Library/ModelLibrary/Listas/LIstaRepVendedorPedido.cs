using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaRepVendedorPedido
    {

        public long Id { get; set; }
        public long PedidoId { get; set; }
        public long ProdutoGradeId { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> Retorno { get; set; }
        public Nullable<decimal> Preco { get; set; }
    }
}
