using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.ComponentModel;

namespace ModelLibrary
{
    public class RelatoriosRepresentante
    {

        public class VendedorPedido
        {
            public string CodigoPedido { get; set; }
            public string DataPedido { get; set; }
            public string Vendedor { get; set; }
            public string CPF { get; set; }
            public string Telefone { get; set; }
            public string Endereco { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Praca { get; set; }
            public string Representante { get; set; }
            public string TelRepresentante { get; set; }
            public string DataRetorno { get; set; }
            public string Comissao { get; set; }
            public double ValorPedido { get; set; }
            public double ValorCompra    { get; set; }
            public double PercentualCompra { get; set; }
            public double FaixaComissao { get; set; }
            public double ValorComissao { get; set; }
            public double ValorLiquido { get; set; }
            public double ValorAReceber { get; set; }
            public double ValorRecebido { get; set; }            
        }



        public class ListaProdutos
        {
            public string CodigoBarras { get; set; }            
            public string Produto { get; set; }
            public double Quantidade { get; set; }
            public double Retorno { get; set; }
            public double Valor { get; set; }
            public double ValorTotal { get; set; }
        }

        

        public static VendedorPedido RelatorioVendedorPedido(long pVendedorId, long pCargaId)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT 
                                        CodigoPedido, DataLancamento DataPedido, 
                                        RepVendedor.Nome as Vendedor,
                                        RepVendedor.CpfCnpj as CPF, 
                                        RepVendedor.Telefone || ' / ' || RepVendedor.Celular as Telefone,
                                        RepVendedor.Endereco || ' ' || RepVendedor.Complemento as Endereco,
                                        RepVendedor.Bairro as Bairro,
                                        RepVendedor.Cidade || '/' || RepVendedor.UF as Cidade,
                                        RepPraca.Descricao as Praca,
                                        RepUsuario.Nome as Representante,
                                        RepUsuario.Telefone  || ' / ' || RepUsuario.Celular as TelRepresentante,
                                        RepPedido.DataRetorno as DataRetorno,
                                        RepPedido.ValorPedido as ValorPedido,
                                        RepPedido.ValorCompra as ValorCompra,
                                        RepPedido.PercentualCompra as PercentualCompra,
                                        RepPedido.FaixaComissao as FaixaComissao,
                                        RepPedido.ValorComissao as ValorComissao,
                                        RepPedido.ValorLiquido as ValorLiquido,
                                        RepPedido.ValorAReceber as ValorAReceber,
                                        RepPedido.ValorAcerto as ValorRecebido
                                    FROM RepPedido
	                                    INNER JOIN RepVendedor ON RepPedido.VendedorId = RepVendedor.Id
	                                    INNER JOIN RepCarga ON RepPedido.CargaId = RepCarga.Id
	                                    INNER JOIN RepUsuario ON RepCarga.RepresentanteId = RepUsuario.Id
	                                    INNER JOIN RepPraca ON RepCarga.PracaId = RepPraca.Id
                                    WHERE RepVendedor.Id = @p0 AND RepCarga.Id = @p1";

                VendedorPedido pedido = representante.Database.SqlQuery<VendedorPedido>(query, pVendedorId, pCargaId).FirstOrDefault();

                return pedido;

            }


        }


        public static List<ListaProdutos> RelatorioVendedorPedidoItem(string pCodigoPedido)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT RepProdutoGrade.CodigoBarras || RepProdutoGrade.Digito as CodigoBarras, Descricao  || ' ' ||  Tamanho  || ' ' || Cor AS Produto, RepPedidoItem.Quantidade as Quantidade, RepPedidoItem.Retorno as Retorno, RepPedidoItem.Preco as Valor, RepPedidoItem.Quantidade * RepPedidoItem.Preco as ValorTotal 
                                FROM RepPedidoItem 
                                    INNER JOIN RepPedido ON RepPedidoItem.PedidoId = RepPedido.Id
                                    INNER JOIN RepProdutoGrade ON RepPedidoItem.ProdutoGradeId = RepProdutoGrade.Id
                                    INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id
                                WHERE CodigoPedido = @p0";


                var result = representante.Database.SqlQuery<ListaProdutos>(query, pCodigoPedido);

                return result.ToList<ListaProdutos>();

            }
        }


        public static List<ListaProdutos> RelatorioSuplemento()
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT 
                                    RepProdutoGrade.CodigoBarras || RepProdutoGrade.Digito as CodigoBarras,
                                    Descricao  || ' ' ||  Tamanho  || ' ' || Cor AS Produto, 
                                    RepCargaProduto.Quantidade as Quantidade, 
                                    0 as Retorno, 
                                    RepProdutoGrade.ValorSaida as Valor, 
                                    RepCargaProduto.Quantidade * RepProdutoGrade.ValorSaida as ValorTotal 
                                FROM RepCargaProduto
	                                INNER JOIN RepProdutoGrade ON RepCargaProduto.ProdutoGradeId = RepProdutoGrade.Id
	                                INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id
                                WHERE Tipo = 'S'";


                var result = representante.Database.SqlQuery<ListaProdutos>(query);

                return result.ToList<ListaProdutos>();

            }
        }
    }
}
