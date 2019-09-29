using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaProdutosConsignados
    {
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public Nullable<double> Quantidade { get; set; }
    }
}
