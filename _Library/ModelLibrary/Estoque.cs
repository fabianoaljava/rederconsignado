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
    
    public partial class Estoque
    {
        public int Id { get; set; }
        public Nullable<int> ProdutoId { get; set; }
        public string TipoMovimentacao { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public string Observacao { get; set; }
        public Nullable<int> temp_old_id { get; set; }
    
        public virtual Produto Produto { get; set; }
    }
}