using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaRepProdutosConferencia
    {

        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> QuantidadeCarga { get; set; }
        public Nullable<decimal> QuantidadeInformada { get; set; }
        public Nullable<decimal> Diferenca { get; set; }
        public Nullable<decimal> ValorDiferenca { get; set; }
        public long CargaId { get; set; }
        public long ProdutoId { get; set; }
        public long CargaProdutoId { get; set; }
        public long ProdutoGradeId { get; set; }

    }
}
