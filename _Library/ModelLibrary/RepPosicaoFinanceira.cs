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
    
    public partial class RepPosicaoFinanceira
    {
        public long Id { get; set; }
        public Nullable<long> VendedorId { get; set; }
        public Nullable<decimal> ValorAReceber { get; set; }
        public Nullable<decimal> ValorRecebido { get; set; }
        public Nullable<decimal> ValorAberto { get; set; }
    }
}