namespace ModelLibrary
{
    using System;
    using System.Collections.Generic;
 

    public class ListaProdutosCarga
    {
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<double> Retorno { get; set; }
        public Nullable<double> ValorSaida { get; set; }
        public Nullable<double> ValorCusto { get; set; }
        public int CargaId { get; set; }
        public int ProdutoGradeId { get; set; }

    }
}