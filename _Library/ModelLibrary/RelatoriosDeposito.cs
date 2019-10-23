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
    }
}
