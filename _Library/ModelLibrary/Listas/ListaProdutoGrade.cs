using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaProdutoGrade
    {

        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Digito { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }        
        public Nullable<double> ValorSaida { get; set; }
        public Nullable<double> ValorCusto { get; set; }
        public Nullable<DateTime> DataFinal { get; set; }
    }
}
