using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaReceberBaixa
    {

        public int Id { get; set; }
        public int ReceberId { get; set; }
        public Nullable<double> Valor { get; set; }
        public Nullable<DateTime> DataPagamento { get; set; }
        public Nullable<DateTime> DataBaixa { get; set; }
    }
}
