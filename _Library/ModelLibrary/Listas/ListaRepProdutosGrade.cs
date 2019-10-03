namespace ModelLibrary
{
    using System;
    using System.Collections.Generic;


    public class ListaRepProdutosGrade
    {
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public Nullable<decimal> ValorSaida { get; set; }
        public Nullable<decimal> ValorCusto { get; set; }
        public long ProdutoGradeId { get; set; }

    }
}