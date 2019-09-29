using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaImportacaoExportacao
    {
        public string Tabela { get; set; }
        public string Acao { get; set; }
        public string Rotina { get; set; }
        public int TotalLinhas { get; set; }        
        public string Status { get; set; }
    }
}
