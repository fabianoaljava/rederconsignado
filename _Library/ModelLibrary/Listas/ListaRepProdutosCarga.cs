namespace ModelLibrary
{
    using System;
    using System.Collections.Generic;
 

    public class ListaRepProdutosCarga
    {
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> QuantidadeRetorno { get; set; }
        public Nullable<decimal> ValorSaida { get; set; }
        public Nullable<decimal> ValorCusto { get; set; }
        public int CargaId { get; set; }
        public long ProdutoGradeId { get; set; }

    }
}