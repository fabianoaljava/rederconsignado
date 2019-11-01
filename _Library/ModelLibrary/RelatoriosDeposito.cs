using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.ComponentModel;

namespace ModelLibrary
{
    public class RelatoriosDeposito
    {

        public class EstoqueProduto
        {
            public string CodigoBarras { get; set; }
            public string Descricao { get; set; }
            public string Cor { get; set; }
            public string Tamanho { get; set; }
            public Double ValorCusto { get; set; }
            public Double ValorSaida { get; set; }
            public Double SaldoEstoque { get; set; }
            public Double Valor { get; set; }            
        }


        public static List<EstoqueProduto> RelatorioEstoqueProduto(Dictionary<string, string> pCriterio = null)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string vCriterio = "";

                string query = @"SELECT ProdutoGrade.CodigoBarras + '' + ProdutoGrade.Digito CodigoBarras, 
                                    Descricao, Tamanho, Cor, ISNULL(ValorSaida, 0) ValorSaida, ISNULL(ValorCusto, 0) ValorCusto,
                                    ISNULL(ProdutoGrade.QUantidade,0) SaldoEstoque, ISNULL(ProdutoGrade.QUantidade,0) * ISNULL(ValorSaida, 0) Valor                                    
                                FROM Produto                                    
                                    INNER JOIN ProdutoGrade ON Produto.Id = ProdutoGrade.ProdutoId";

                if (pCriterio != null)
                {
                    if (pCriterio.ContainsKey("CodigoBarras"))
                    {
                        vCriterio = " ProdutoGrade.CodigoBarras + '' + ProdutoGrade.Digito LIKE '%" + pCriterio["CodigoBarras"] + "%'";
                    }


                    if (pCriterio.ContainsKey("CodigoGeral"))
                    {
                        vCriterio = "(ProdutoGrade.CodigoBarras + '' + ProdutoGrade.Digito = '" + pCriterio["CodigoGeral"] + "' OR ProdutoGrade.ProdutoId = " + pCriterio["CodigoGeral"] + ")";
                    }

                    if (pCriterio.ContainsKey("Nome"))
                    {
                        vCriterio += vCriterio != "" ? " OR " : "";
                        vCriterio += " Descricao LIKE '%" + pCriterio["Nome"] + "%'";
                    }


                    vCriterio = (vCriterio != "") ? "(" + vCriterio + ")" : "";

                    if (pCriterio.ContainsKey("SaldoEstoque"))
                    {
                        if (pCriterio["SaldoEstoque"] == "Y")
                        {
                            vCriterio += vCriterio != "" ? " AND " : "";
                            vCriterio += " ISNULL(ProdutoGrade.QUantidade,0) > 0 ";
                        }
                        else if (pCriterio["SaldoEstoque"] == "N")
                        {
                            vCriterio += vCriterio != "" ? " AND " : "";
                            vCriterio += " ISNULL(ProdutoGrade.QUantidade,0) = 0 ";
                        }
                    }


                }

                query += vCriterio != "" ? " WHERE " + vCriterio : "";


                Console.WriteLine("Listando Produtos Query: " + query);

                var result = deposito.Database.SqlQuery<EstoqueProduto>(query);


                return result.ToList<EstoqueProduto>();

            }
        
        }


        public class CobrancaCarga
        {

            public string Codigo { get; set; }
            public string Tipo { get; set; }
            public string Vendedor { get; set; }
            public string CPFCnpj { get; set; }
            public string RGInsc { get; set; }
            public string Endereco { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string CEP { get; set; }
            public string Telefone { get; set; }
            public Double TotalAReceber { get; set; }

        }



        public static List<CobrancaCarga> RelatorioCobrancaCarga(int pCargaId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                
                string query = @"SELECT Pedido.CodigoPedido Codigo, 'Pedido' as Tipo, 
                                        Nome as Vendedor, CpfCnpj as CPFCnpj, RGInscricao as RGInsc,
                                        Endereco + ' ' + Complemento as Endereco,
                                        Bairro, Cidade + '/' + UF as Cidade, CEP,
                                        Telefone + ' | ' + TelefoneComercial + ' | ' + Celular as Telefone,
                                        ValorLiquido + ValorAReceber - ValorAcerto as TotalAReceber
                                                FROM Vendedor 
                                                    INNER JOIN Pedido 
                                                        ON Pedido.VendedorId = Vendedor.Id
                                            WHERE Pedido.CargaId = @p0
                                                    AND ValorLiquido + ValorAReceber - ValorAcerto  > 0
                                UNION
                                    SELECT Convert(Varchar(12),Documento) + '/' + Serie Codigo, 'Receber' as Tipo, 
                                            Nome as Vendedor, CpfCnpj as CPFCnpj, RGInscricao as RGInsc,
                                            Endereco + ' ' + Complemento as Endereco,
                                            Bairro, Cidade + '/' + UF as Cidade, CEP,
                                            Telefone + ' | ' + TelefoneComercial + ' | ' + Celular as Telefone,
                                            sum(ValorNF)- sum(ISNULL(Valor,0)) TotalAReceber 
                                                FROM Vendedor 
                                                    INNER JOIN Receber ON Receber.VendedorId = Vendedor.Id
                                                    LEFT JOIN ReceberBaixa ON ReceberBaixa.ReceberId = Receber.Id 
                                            WHERE Receber.CargaId = @p0
                                            GROUP BY Documento, Serie, Nome, CpfCnpj, RGInscricao, Endereco, Complemento, Bairro, Cidade, UF, CEP, Telefone, TelefoneComercial, Celular
                                                HAVING sum(ValorNF)- sum(ISNULL(Valor,0)) > 0";


                var result = deposito.Database.SqlQuery<CobrancaCarga>(query, pCargaId);

                return result.ToList<CobrancaCarga>();

            }

        }


        public class AnaliseRetorno
        {
            public string Praca { get; set; }
            public string Representante { get; set; }
            public string MesAno { get; set; }
            public string Produto { get; set; }
            public string Carga { get; set; }
            public string Consignado { get; set; }
            public string Vendido { get; set; }
            public string PercentVendas { get; set; }
        }


        public static List<AnaliseRetorno> RelatorioAnaliseRetorno(int pRetornoId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                string query = @"SELECT                                    
                                    Convert(Varchar(10),Praca.Id) + ' - ' + Praca.Descricao Praca,
                                    Convert(Varchar(10),Representante.Id) + ' - ' + Representante.Nome Representante,
                                    Produto.CodigoBarras + ' ' + Produto.Descricao Produto,
                                    Convert(Varchar(12), CargaProduto.Quantidade) Carga,
                                    Convert(Varchar(12),Consignado.Consignado) Consignado,
                                    Convert(Varchar(12),Vendido.Vendido) Vendido,
                                    CASE 
	                                    WHEN CargaProduto.Quantidade > 0 THEN Convert(Varchar(12), Vendido.Vendido / CargaProduto.Quantidade * 100)
	                                    ELSE '' END AS PercentVendas
                                     FROM Carga 
                                    INNER JOIN Praca ON Carga.PracaId = Praca.Id
                                    INNER JOIN Representante ON Carga.RepresentanteId = Representante.Id
                                    INNER JOIN CargaProduto ON CargaProduto.CargaId = Carga.Id
                                    INNER JOIN ProdutoGrade ON CargaProduto.ProdutoGradeId = ProdutoGrade.Id
                                    INNER JOIN Produto ON ProdutoGrade.ProdutoId = Produto.Id
                                    LEFT JOIN (
	                                    SELECT CargaId, CargaOriginal, ProdutoGradeId, Sum(Quantidade-Retorno) Consignado 
	                                    FROM Pedido INNER JOIN PedidoItem ON PedidoItem.PedidoId = Pedido.Id 
	                                    WHERE Status < 2
	                                    GROUP BY CargaId, CargaOriginal, ProdutoGradeId
                                    ) AS Consignado ON (Consignado.CargaId = Carga.Id OR Consignado.CargaOriginal = Carga.Id) AND Consignado.ProdutoGradeId = ProdutoGrade.Id
                                    LEFT JOIN (
	                                    SELECT CargaId, CargaOriginal, ProdutoGradeId, Sum(Quantidade-Retorno) Vendido 
	                                    FROM Pedido INNER JOIN PedidoItem ON PedidoItem.PedidoId = Pedido.Id 
	                                    WHERE Status >= 2
	                                    GROUP BY CargaId, CargaOriginal, ProdutoGradeId
                                    ) AS Vendido ON (Vendido.CargaId = Carga.Id OR Vendido.CargaOriginal = Carga.Id) AND Vendido.ProdutoGradeId = ProdutoGrade.Id

                                    WHERE Carga.Id = @p0
                                    ORDER BY Produto.Descricao";


                var result = deposito.Database.SqlQuery<AnaliseRetorno>(query, pRetornoId);

                return result.ToList<AnaliseRetorno>();
            }
        }
    }
}
