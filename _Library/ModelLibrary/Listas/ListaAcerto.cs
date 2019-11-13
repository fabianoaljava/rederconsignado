using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaAcerto
    {        
        public string Nome { get; set; }
        public Nullable<double> ValorPedido { get; set; }
        public Nullable<double> ValorCompra { get; set; }
        public Nullable<double> ValorLiquido { get; set; }
        public Nullable<double> ValorAReceber { get; set; }
        public Nullable<double> ValorAcerto { get; set; }
        public Nullable<double> ValorAberto { get; set; }
        public Nullable<DateTime> Data { get; set; }       
    }
}
