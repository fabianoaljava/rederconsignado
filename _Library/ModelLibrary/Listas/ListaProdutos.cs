using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaProdutos
    {
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public Nullable<double> ValorCusto { get; set; }
        public Nullable<double> ValorSaida { get; set; }
        public Nullable<double> SaldoEstoque { get; set; }
        public Nullable<double> Valor { get; set; }
        public int ProdutoGradeId { get; set; }
    }
}
