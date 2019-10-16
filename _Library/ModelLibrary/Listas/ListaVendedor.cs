using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaVendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPFCnpj { get; set; }
        public string Cidade { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
    }
}
