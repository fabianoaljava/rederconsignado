//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recebimento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> CargaId { get; set; }
        public int VendedorId { get; set; }
        public Nullable<int> ReceberId { get; set; }
        public Nullable<int> PedidoId { get; set; }
        public string CodigoPedido { get; set; }
        public Nullable<double> ValorRecebido { get; set; }
        public Nullable<System.DateTime> DataPagamento { get; set; }
        public string Observacao { get; set; }
        public string FormaPagamento { get; set; }
    }
}
