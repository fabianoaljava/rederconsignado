﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DepositoDBEntities : DbContext
    {
        public DepositoDBEntities()
            : base("name=DepositoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carga> Carga { get; set; }
        public virtual DbSet<CargaProduto> CargaProduto { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Fornecedor> Fornecedor { get; set; }
        public virtual DbSet<Praca> Praca { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<ProdutoGrade> ProdutoGrade { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<ProdutosCarga> ProdutosCarga { get; set; }
        public virtual DbSet<Representante> Representante { get; set; }
        public virtual DbSet<Totalizadores> Totalizadores { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<Cor> Cor { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Estoque> Estoque { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoItem> PedidoItem { get; set; }
        public virtual DbSet<Receber> Receber { get; set; }
        public virtual DbSet<ReceberBaixa> ReceberBaixa { get; set; }
        public virtual DbSet<Suplemento> Suplemento { get; set; }
        public virtual DbSet<SuplementoProduto> SuplementoProduto { get; set; }
        public virtual DbSet<Tamanho> Tamanho { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }
    }
}