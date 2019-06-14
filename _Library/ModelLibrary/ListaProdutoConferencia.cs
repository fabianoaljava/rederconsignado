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
        public Nullable<double> Vendido { get; set; }
        public Nullable<double> Carga { get; set; }
        public Nullable<double> Retorno { get; set; }
        public Nullable<double> Consignado { get; set; }
        public Nullable<double> SaldoCarro { get; set; }
        public Nullable<double> ContagemCarro { get; set; }

    }
}
