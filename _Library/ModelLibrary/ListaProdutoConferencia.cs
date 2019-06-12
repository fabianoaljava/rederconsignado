using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaProdutoConferencia
    {

        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> Vendido { get; set; }
        public Nullable<decimal> Carga { get; set; }
        public Nullable<decimal> Retorno { get; set; }
        public Nullable<decimal> Consignado { get; set; }
        public Nullable<decimal> SaldoCarro { get; set; }

    }
}
