using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaRepPosicaoFinanceira
    {

        public long Id { get; set; }
        public string Nome { get; set; }
        public Nullable<decimal> Receber { get; set; }
        public Nullable<decimal> Recebido { get; set; }
        public Nullable<decimal> Aberto { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<int> Retorno { get; set; }
        public Nullable<int> Remarcado { get; set; }
        public Nullable<int> Visitado { get; set; }
        public Nullable<int> PedidoNovo { get; set; }


    }
}
