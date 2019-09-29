using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaRepCargaProduto
    {
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public string Tipo { get; set; }
        public long CargaId { get; set; }
        public long ProdutoGradeId { get; set; }
    }
}
