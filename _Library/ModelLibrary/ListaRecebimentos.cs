using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaRecebimentos
    {
        public Nullable<long> Id { get; set; }
        public long ReceberId { get; set; }
        public Nullable<long> Documento { get; set; }
        public string Serie { get; set; }
        public Nullable<decimal> ValorDuplicata { get; set; }
        public Nullable<decimal> ValorRecebido { get; set; }
        public Nullable<System.DateTime> DataEmissao { get; set; }
        public Nullable<System.DateTime> DataVencimento { get; set; }
        public Nullable<System.DateTime> DataPagamento { get; set; }
        public string Observacoes { get; set; }

    }
}
