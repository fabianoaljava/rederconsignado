using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaAReceber
    {
        public int Id { get; set; }
        public Nullable<int> ReceberBaixaId { get; set; }
        public int Documento { get; set; }
        public string Serie { get; set; }
        public string Nome { get; set; }
        public Nullable<double> ValorTotal { get; set; }
        public Nullable<double> ValorAReceber { get; set; }
        public Nullable<double> ValorPago { get; set; }
        public Nullable<DateTime> DataPagamento { get; set; }

    }
}
