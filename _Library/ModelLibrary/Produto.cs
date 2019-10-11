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
    
    public partial class Produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto()
        {
            this.ProdutoGrade = new HashSet<ProdutoGrade>();
        }
    
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> CategoriaId { get; set; }
        public Nullable<int> FornecedorId { get; set; }
        public string Unidade { get; set; }
        public string CodigoBarras { get; set; }
        public string Digito { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public string Status { get; set; }
        public Nullable<int> temp_old_id { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdutoGrade> ProdutoGrade { get; set; }
    }
}
