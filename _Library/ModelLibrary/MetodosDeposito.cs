using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class MetodosDeposito
    {

        /// <summary>
        /// Método de Autenticação de Usuario passando Login/Senha
        /// Retorno: array de string contendo
        /// 0: Autenticado (Y/N)
        /// 1: Mensagem
        /// 2: TipoModulo
        /// 3: Nome
        /// </summary>
        public static string[] Autenticar(String pLogin, String pSenha)
        {

            var ret = new String[4];

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var usuario = deposito.Usuario.FirstOrDefault(u => u.Login.ToLower() == pLogin.ToLower());

                if (usuario != null)
                {
                    if (usuario.Senha == pSenha)
                    {
                        ret[0] = "Y";
                        ret[1] = "Usuário autenticado com sucesso.";
                        ret[2] = usuario.TipoModulo;
                        ret[3] = usuario.Nome;
                        return ret;
                    }
                    else
                    {
                        ret[0] = "N";
                        ret[1] = "Usuário e senha inválidos!";
                        return ret;
                    }
                }
                else
                {
                    ret[0] = "N";
                    ret[1] = "Usuário " + pLogin + " não encontrado.";
                    return ret;
                }
            }
        }


        public static bool VerificarServidor()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                try
                {
                    deposito.Database.Connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }


               
        public static List<Praca> ObterListaPracas()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Praca.ToList<Praca>();
            }
        }

        public static Praca ObterPraca(int i)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var praca = deposito.Praca.FirstOrDefault(p => p.Id == i);
                return praca;
            }
        }


        public static List<Representante> ObterListaRepresentantes()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Representante.ToList<Representante>();
            }
        }


        public static Representante ObterRepresentante(int i)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var representante = deposito.Representante.FirstOrDefault(p => p.Id == i);
                return representante;
            }
        }


        public static List<Carga> ObterListaCargas()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Carga.ToList<Carga>();
            }
        }

        public static Carga ObterCarga(long pRepresentanteId, long pPracaId, int pMes, int pAno)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var carga = deposito.Carga.FirstOrDefault(r => r.RepresentanteId == pRepresentanteId && r.PracaId == pPracaId && r.Mes == pMes && r.Ano == pAno);

                return carga;

            }
        }


        public static Carga ObterCargaById(long pCargaId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var carga = deposito.Carga.FirstOrDefault(c => c.Id == pCargaId);

                return carga;

            }
        }

        public static Carga ObterCargaAnterior(long pRepresentanteId, long pPracaId, DateTime pDataAbertura)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var viagemanterior = (from c in deposito.Carga
                                      orderby c.DataAbertura descending
                                      where c.RepresentanteId == pRepresentanteId && c.PracaId == pPracaId && c.DataAbertura < pDataAbertura
                                      select c).FirstOrDefault<Carga>();
                return viagemanterior;

                

            }
        }


        public static List<ListaPesquisaCarga> PesquisarCarga(long pPracaId = 0, long pRepresentanteId = 0, string pAnoMesInicial = null, string pAnoMesFinal = null)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string query = @"SELECT Carga.Id Id, PracaId, Praca.Descricao Praca, RepresentanteId, Representante.Nome Representante, Ano, Mes, 
                                    DataAbertura, DataExportacao, DataRetorno, DataConferencia, DataFinalizacao, Carga.Status, Cast(Concat(Ano,RIGHT('0' + CAST(Mes AS varchar(2)), 2)) AS int)  AnoMes
	                            FROM Carga
	                                INNER JOIN Praca ON Carga.PracaId = Praca.Id
	                                INNER JOIN Representante ON Carga.RepresentanteId = Representante.Id
                                WHERE 1=1 ";
                
                if (pPracaId != 0)
                {
                    query += " AND Carga.PracaId = " + pPracaId.ToString();
                }


                if (pRepresentanteId != 0)
                {
                    query += " AND Carga.RepresentanteId = " + pRepresentanteId.ToString();
                }

                if (pAnoMesInicial != null && pAnoMesFinal != null)
                {
                    query += " AND Cast(Concat(Ano,RIGHT('0' + CAST(Mes AS varchar(2)), 2)) AS int)  between " + pAnoMesInicial + " AND " + pAnoMesFinal;
                } else if (pAnoMesInicial != null)
                {
                    query += " AND Cast(Concat(Ano,RIGHT('0' + CAST(Mes AS varchar(2)), 2)) AS int)  >= " + pAnoMesInicial;
                } else if (pAnoMesFinal != null)
                {
                    query += " AND Cast(Concat(Ano,RIGHT('0' + CAST(Mes AS varchar(2)), 2)) AS int)  <= " + pAnoMesFinal;
                }


                var result = deposito.Database.SqlQuery<ListaPesquisaCarga>(query).ToList<ListaPesquisaCarga>();

                return result.ToList<ListaPesquisaCarga>();

            }

        }

        public static string ValidarInclusaoCarga(int pPracaId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                // verificar se existe carga não finalizada
                var carga = deposito.Carga.FirstOrDefault(cg => cg.Status != "F" && cg.PracaId == pPracaId);
                if (carga == null)
                {
                    return "OK";
                } else
                {
                    return "A Carga referente ao mes " + carga.Mes.ToString() + "/" + carga.Ano.ToString() + " ainda não foi finalizada. Não foi possível gerar nova carga.";
               }
            }                    
        }


        public static int InserirCarga(int pRepresentanteId, int pPracaId, int pMes, int pAno)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                DateTime dataabertura = DateTime.Now;



                var novacarga = new Carga
                {
                    RepresentanteId = pRepresentanteId,
                    PracaId = pPracaId,
                    Mes = pMes,
                    Ano = pAno,
                    DataAbertura = dataabertura,
                    Status = "A"
                };

                deposito.Carga.Add(novacarga);
                deposito.SaveChanges();

                var maxCarga = deposito.Carga.OrderByDescending(i => i.Id).FirstOrDefault();

                int newId = maxCarga == null ? 1 : maxCarga.Id;

                return newId;
                

            }

        }


        public static Boolean ExcluirCarga(long pCargaId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var cargaproduto = deposito.CargaProduto.FirstOrDefault(cp => cp.CargaId == pCargaId);
                if (cargaproduto != null)
                {
                    return false;
                } else
                {
                    deposito.Database.ExecuteSqlCommand("DELETE FROM Carga WHERE Id = @pCargaId", new SqlParameter("@pCargaId", pCargaId));
                    return true;
                }
            }

        }

        public static void AlterarStatusCarga(long pCargaId, string pStatus)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var carga = deposito.Carga.SingleOrDefault(cg => cg.Id == pCargaId);
                if (carga != null)
                {
                    carga.Status = pStatus;


                    switch (pStatus)
                    {
                        case "A": /* Revertendo status */
                            carga.DataExportacao = null;
                            carga.DataRetorno = null;
                            carga.DataConferencia = null;
                            carga.DataFinalizacao = null;
                            break;
                        case "E":
                            carga.DataExportacao = DateTime.Now;
                            // atualizar estoque produto
                            AtualizarCargaRetornoEstoque(pCargaId, "Carga");
                            break;
                        case "R":
                            carga.DataRetorno = DateTime.Now;
                            break;
                        case "C":
                            carga.DataConferencia = DateTime.Now;
                            break;
                        case "F":
                            carga.DataFinalizacao = DateTime.Now;
                            //Alterar Status dos Pedidos sem ValorAReceber = 1;
                            var pedido = deposito.Pedido.Where(pd => pd.CargaId == pCargaId && (pd.Status == "0" || pd.Status == "1" || pd.Status == "2"));
                            foreach (Pedido row in pedido)
                            {

                                if (row.QuantidadeRemarcado >= 2)
                                {
                                    pedido.SingleOrDefault(pd => pd.Id == row.Id).Status = "3";
                                    NegativarVendedor(row.VendedorId);
                                    // Negativar Vendedor
                                } else
                                {
                                    if (row.ValorLiquido + row.ValorAReceber - row.ValorAcerto <= 0)
                                    {
                                        pedido.SingleOrDefault(pd => pd.Id == row.Id).Status = "4";
                                    } else
                                    {
                                        pedido.SingleOrDefault(pd => pd.Id == row.Id).Status = "1";
                                    }                                    
                                    pedido.SingleOrDefault(pd => pd.Id == row.Id).Remarcado = 1;
                                    pedido.SingleOrDefault(pd => pd.Id == row.Id).QuantidadeRemarcado++;                                        
                                }                                    
                            };
                            
                            var receber = deposito.Receber
                                    .Join(deposito.Carga, rc => rc.CargaId, ca => ca.Id, (rc, ca) => new { Receber = rc, Carga = ca })
                                    .Where(q => q.Receber.Status == "0" && q.Carga.PracaId == carga.PracaId)
                                    .Select(rc => rc.Receber);
                            foreach (Receber row in receber)
                            {
                                receber.SingleOrDefault(rc => rc.Id == row.Id).QuantidadeRemarcado++;
                            }


                            // atualizar estoque produto
                            AtualizarCargaRetornoEstoque(pCargaId, "Retorno");

                           break;
                    }


                    deposito.SaveChanges();
                }


            }

        }






        public static List<ListaProdutosCarga> ObterProdutosCarga(int pCargaId, Boolean pRetorno = false)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string query = @"SELECT 
	                                Produto.CodigoBarras + ProdutoGrade.Digito CodigoBarras,
	                                Produto.Descricao,
	                                Cor,
	                                Tamanho,
	                                CargaProduto.Quantidade,
	                                Retorno,
	                                ValorSaida,
	                                ValorCusto,
                                    Tipo,
	                                Carga.Id CargaId,
	                                ProdutoGrade.Id ProdutoGradeId
                                FROM Carga 
	                                INNER JOIN CargaProduto ON CargaProduto.CargaId = Carga.Id
	                                INNER JOIN ProdutoGrade ON ProdutoGrade.Id = ProdutoGradeId
	                                INNER JOIN Produto ON Produto.Id = ProdutoGrade.ProdutoId
                                WHERE Carga.Id = @p0";

                if (pRetorno) query += " AND Retorno > 0";

                var result = deposito.Database.SqlQuery<ListaProdutosCarga>(query, pCargaId).ToList<ListaProdutosCarga>();

                return result.ToList<ListaProdutosCarga>();                

            }
        }

        public static ListaProdutosCarga ObterProdutoCarga(int pCargaId, int pProdutoGradeId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                string query = @"SELECT 
	                                Produto.CodigoBarras + ProdutoGrade.Digito CodigoBarras,
	                                Produto.Descricao,
	                                Cor,
	                                Tamanho,
	                                CargaProduto.Quantidade,
	                                Retorno,
	                                ValorSaida,
	                                ValorCusto,
                                    Tipo,
	                                Carga.Id CargaId,
	                                ProdutoGrade.Id ProdutoGradeId
                                FROM Carga 
	                                INNER JOIN CargaProduto ON CargaProduto.CargaId = Carga.Id
	                                INNER JOIN ProdutoGrade ON ProdutoGrade.Id = ProdutoGradeId
	                                INNER JOIN Produto ON Produto.Id = ProdutoGrade.ProdutoId
                                WHERE Carga.Id = @p0 AND ProdutoGrade.Id = @p1 ";

                ListaProdutosCarga produtocarga = deposito.Database.SqlQuery<ListaProdutosCarga>(query, pCargaId, pProdutoGradeId).FirstOrDefault();

                return produtocarga;

            }
        }


        public static List<Produto> ObterListaProduto()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Produto.Where(pd => pd.Status == "1").ToList<Produto>();
            }
        }


        public static List<ListaProdutos> ObterListaProdutos(Dictionary<string, string> pCriterio = null)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {                

                string vCriterio = "";                

                string query = @"SELECT ProdutoGrade.CodigoBarras + '' + ProdutoGrade.Digito CodigoBarras, 
                                    Descricao, Tamanho, Cor, ISNULL(ValorSaida, 0) ValorSaida, ISNULL(ValorCusto, 0) ValorCusto,
                                    ISNULL(ProdutoGrade.QUantidade,0) SaldoEstoque, ISNULL(ProdutoGrade.QUantidade,0) * ISNULL(ValorSaida, 0) Valor,
                                    ProdutoGrade.Id ProdutoGradeId 
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


                    vCriterio = (vCriterio!="")?"(" + vCriterio + ")":"";

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

                var result = deposito.Database.SqlQuery<ListaProdutos>(query);


                return result.ToList<ListaProdutos>();

            }
        }

        public static Produto ObterProduto(string pCodigo)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var produto = (from p in deposito.Produto
                                    where (p.CodigoBarras == pCodigo)
                                    select p).FirstOrDefault<Produto>();

                return produto;
            }

        }

        public static void SalvarProduto(string pModo, Produto pProduto, long pProdutoId = 0)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                if (pModo == "Create")
                {

                        deposito.Produto.Add(pProduto);
                        deposito.SaveChanges();

                } else
                {
                    var produto = deposito.Produto.SingleOrDefault(pd => pd.Id == pProdutoId);
                    if (produto != null)
                    {

                        produto.Descricao = pProduto.Descricao;
                        produto.CategoriaId = pProduto.CategoriaId;
                        produto.FornecedorId = pProduto.FornecedorId;
                        produto.Unidade = pProduto.Unidade;

                        if (produto.CodigoBarras != pProduto.CodigoBarras)
                        {

                            produto.CodigoBarras = pProduto.CodigoBarras;
                            string query = "UPDATE ProdutoGrade SET CodigoBarras = @p0 WHERE ProdutoId = @p1";
                            deposito.Database.ExecuteSqlCommand(query, pProduto.CodigoBarras, pProdutoId);
                        }
                        
                        produto.Digito = pProduto.Digito;

                        deposito.SaveChanges();
                    }
                }
            }


        }


        public static void ExcluirProduto (long pProdutoId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var produto = deposito.Produto.SingleOrDefault(pd => pd.Id == pProdutoId);

                produto.Status = "0";

                string query = "UPDATE ProdutoGrade SET Status = '0', DataFinal = GetDate() WHERE ProdutoId = @p0";
                deposito.Database.ExecuteSqlCommand(query, pProdutoId);


                deposito.SaveChanges();

            }


        }


        public static List<ListaProdutoGrade> ObterListaProdutoGrade(long pProdutoId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {               

                string query = @"SELECT Id, CodigoBarras, Digito, Cor, Tamanho, ValorSaida, ValorCusto, DataFinal
                                    FROM ProdutoGrade 
                                WHERE ProdutoId = @p0";

                var result = deposito.Database.SqlQuery<ListaProdutoGrade>(query, pProdutoId);

                return result.ToList<ListaProdutoGrade>();
            }

        }

        public static List<ProdutoGrade> ObterProdutosGrade(string pPesquisa)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                if (pPesquisa != "")
                {
                    string vCodigoSemDigito = pPesquisa.Substring(0, pPesquisa.Length - 1);
                    string vDigito = pPesquisa.Substring(pPesquisa.Length - 1);

                    long vProdutoId = Convert.ToInt64(pPesquisa);

                    Console.WriteLine(vCodigoSemDigito + ':' + vDigito);

                    var produtograde = (from pg in deposito.ProdutoGrade
                                        where ((pg.CodigoBarras == vCodigoSemDigito && pg.Digito == vDigito) || pg.ProdutoId == vProdutoId)
                                        select pg).ToList<ProdutoGrade>();

                    return produtograde;

                }
                else
                {
                    return null;
                }
            }

        }

        public static ProdutoGrade ObterProdutoGrade(string pCodigo, long pProdutoGradeId = 0)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                if (pProdutoGradeId > 0)
                {
                    var produtograde = (from pg in deposito.ProdutoGrade
                                        where (pg.Id == pProdutoGradeId)
                                        select pg).FirstOrDefault<ProdutoGrade>();

                    return produtograde;
                }
                else
                {

                    if (pCodigo != "")
                    {
                        string vCodigoSemDigito = pCodigo.Substring(0, pCodigo.Length - 1);
                        string vDigito = pCodigo.Substring(pCodigo.Length - 1);

                        Console.WriteLine(vCodigoSemDigito + ':' + vDigito);

                        var produtograde = (from pg in deposito.ProdutoGrade
                                            where (pg.CodigoBarras == vCodigoSemDigito && pg.Digito == vDigito)
                                            select pg).FirstOrDefault<ProdutoGrade>();

                        return produtograde;
                    } else
                    {
                        return null;
                    }
                }

            }

        }


        public static int ObterUltimoProdutoGrade(int pProdutoId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var maxDV = deposito.ProdutoGrade.OrderByDescending(i => i.Digito).Where(pg => pg.ProdutoId == pProdutoId).FirstOrDefault();

                int DV = maxDV == null ? 1 : Convert.ToInt32(maxDV.Digito) + 1;


                return DV;
            }

        }


        public static void ProdutoGradeExcluir(int pProdutoGradeId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var produtograde = deposito.ProdutoGrade.SingleOrDefault(pd => pd.Id == pProdutoGradeId);


                produtograde.DataFinal = DateTime.Now;
                produtograde.Status = "0";
                deposito.SaveChanges();

            }
            
        }

        public static bool ProdutoGradeValidarDV(int pProdutoId, string pDigito)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var DV = deposito.ProdutoGrade.OrderByDescending(i => i.Digito).Where(pg => pg.ProdutoId == pProdutoId && pg.Digito == pDigito).FirstOrDefault();

                if (DV == null)
                {
                    return true;
                } else
                {
                    return false;
                }


            }                


        }

        public static void SalvarProdutoGrade(ProdutoGrade pProdutoGrade, int pProdutoGradeId = 0)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                if (pProdutoGradeId == 0) //INSERIR
                {
                    deposito.ProdutoGrade.Add(pProdutoGrade);
                    deposito.SaveChanges();

                }
                else // atualizar
                {
                    var produtograde = deposito.ProdutoGrade.SingleOrDefault(pd => pd.Id == pProdutoGradeId);
                    if (produtograde != null)
                    {

                        produtograde.Digito = pProdutoGrade.Digito;
                        produtograde.Tamanho = pProdutoGrade.Tamanho;
                        produtograde.Cor = pProdutoGrade.Cor;
                        produtograde.ValorCusto = pProdutoGrade.ValorCusto;
                        produtograde.ValorSaida = pProdutoGrade.ValorSaida;
                        deposito.SaveChanges();
                    }
                }
            }



        }


        public static List<ListaProdutoConferencia> ObterListaProdutoConferencia(long pCargaId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var carga = deposito.Carga.FirstOrDefault(c => c.Id == pCargaId);


                int vCargaId = carga != null ? carga.Id : 0;


                string query = @"SELECT ProdutoGrade.CodigoBarras + '' + ProdutoGrade.Digito as CodigoBarras, Produto.Descricao + ' ' + ProdutoGrade.Tamanho Descricao,
	                                    SUM(ISNULL(Retornado.Vendido,0)) Vendido,
	                                    SUM(ISNULL(CargaProduto.Quantidade,0)) Carga,	
	                                    SUM(ISNULL(Retornado.Retorno,0)) Retorno,
	                                    SUM(ISNULL(Consignado.Consignado,0)) Consignado,
	                                    SUM(ISNULL(CargaProduto.Quantidade,0))-SUM(ISNULL(Retornado.Vendido,0))+SUM(ISNULL(Retornado.Retorno,0))-SUM(ISNULL(Consignado.Consignado,0)) AS SaldoCarro,
	                                    SUM(ISNULL(CargaProduto.Retorno,0)) ContagemCarro,
	                                    SUM(ISNULL(CargaProduto.Retorno,0)) - (SUM(ISNULL(CargaProduto.Quantidade,0))-SUM(ISNULL(Retornado.Vendido,0))+SUM(ISNULL(Retornado.Retorno,0))-SUM(ISNULL(Consignado.Consignado,0))) AS Falta,
	                                    (SUM(ISNULL(CargaProduto.Retorno,0)) - (SUM(ISNULL(CargaProduto.Quantidade,0))-SUM(ISNULL(Retornado.Vendido,0))+SUM(ISNULL(Retornado.Retorno,0))-SUM(ISNULL(Consignado.Consignado,0)))) * ISNULL(ProdutoGrade.ValorSaida,0) VrDiferenca 	
	                            FROM Produto
	                               INNER JOIN ProdutoGrade ON Produto.Id = ProdutoGrade.ProdutoId
	                               LEFT JOIN CargaProduto on CargaProduto.ProdutoGradeId = ProdutoGrade.Id
	                               LEFT JOIN Carga ON CargaProduto.CargaId = Carga.Id
	                               LEFT JOIN (
		                                    SELECT ProdutoGradeId, CargaId, SUM(ISNULL(PedidoItem.Quantidade,0)) Vendido, SUM(ISNULL(PedidoItem.Retorno,0)) Retorno FROM Pedido
			                                    LEFT JOIN PedidoItem ON PedidoItem.PedidoId = Pedido.Id
			                                WHERE Pedido.CargaId = @p0 AND Status >= 2
			                                GROUP BY ProdutoGradeId, CargaId
                                            ) as Retornado ON Retornado.ProdutoGradeId = ProdutoGrade.Id AND Retornado.CargaId = Carga.Id
	                                LEFT JOIN (
                                            SELECT ProdutoGradeId, CargaId, SUM(ISNULL(PedidoItem.Quantidade,0)) Consignado FROM Pedido
			                                    LEFT JOIN PedidoItem ON PedidoItem.PedidoId = Pedido.Id
			                                WHERE Pedido.CargaId = @p0 AND Status < 2
			                                GROUP BY ProdutoGradeId, CargaId
                                            ) as Consignado ON Consignado.ProdutoGradeId = ProdutoGrade.Id AND Consignado.CargaId = Carga.Id
                                 WHERE Carga.Id = @p0
                                 GROUP BY ProdutoGrade.CodigoBarras, ProdutoGrade.Digito, Produto.Descricao, ProdutoGrade.Tamanho, Carga.Id, ProdutoGrade.ValorSaida";

                var result = deposito.Database.SqlQuery<ListaProdutoConferencia>(query, vCargaId);

                return result.ToList<ListaProdutoConferencia>();

            }

        }


        public static List<ListaProdutosConsignados> ObterListaProdutosConsignados(long pCargaId)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                

                string query = @"SELECT Produto.CodigoBarras + '' + ProdutoGrade.Digito as CodigoBarras, Produto.Descricao as Nome, sum(PedidoItem.Quantidade - PedidoItem.Retorno) as Quantidade
                                FROM Pedido 
                                 INNER JOIN PedidoItem ON Pedido.Id = PedidoItem.PedidoId
                                 INNER JOIN ProdutoGrade ON PedidoItem.ProdutoGradeId= ProdutoGrade.Id
                                 INNER JOIN Produto ON ProdutoGrade.ProdutoId = Produto.Id 
                                WHERE Pedido.CargaId = @p0
                                GROUP BY Produto.CodigoBarras, ProdutoGrade.Digito, Produto.Descricao";

                var result = deposito.Database.SqlQuery<ListaProdutosConsignados>(query, pCargaId);

                return result.ToList<ListaProdutosConsignados>();

            }

        }


        public static void InserirCargaProduto(int pCargaId, int pProdutoGradeId, double pQuantidade)
        {

            
            /// Se o CargaProduto existir na lista - incrementar quantidade
            

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var result = deposito.CargaProduto.SingleOrDefault(cp => cp.ProdutoGradeId == pProdutoGradeId && cp.CargaId == pCargaId);

                if (result != null)
                {

                    var tmpQtd = result.Quantidade;

                    Console.WriteLine("Valor:" + tmpQtd.ToString());
                    result.Quantidade = tmpQtd + pQuantidade;
                    
                } else {


                    var novacargaproduto = new CargaProduto
                    {
                        CargaId = pCargaId,
                        ProdutoGradeId = pProdutoGradeId,
                        Quantidade = pQuantidade,
                        Retorno = 0,
                        Tipo = "C"
                        
                    };

                    deposito.CargaProduto.Add(novacargaproduto);
                    
                }


                //atualizar estoque produto



                deposito.SaveChanges();



            }


        }


        public static void AlterarCargaProduto(int pCargaId, int pCargaProdutoGradeId, double pQuantidade)
        {


            ///alterar com base no 
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var result = deposito.CargaProduto.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pCargaProdutoGradeId);
                if (result != null)
                {
                    result.Quantidade = pQuantidade;
                    deposito.SaveChanges();
                }

                //atualizar estoque produto
            }




        }


        public static void ExcluirCargaProduto(int pCargaId, int pCargaProdutoGradeId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                deposito.Database.ExecuteSqlCommand("DELETE FROM CargaProduto WHERE CargaId = @pCargaId AND ProdutoGradeId = @pProdutoGradeId", new SqlParameter("@pCargaId", pCargaId), new SqlParameter("@pProdutoGradeId", pCargaProdutoGradeId));

                //atualizar estoque produto
            }
        }


        public static void RefazerRetorno(int pCargaId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string query = "UPDATE Carga SET Status = 'R', DataConferencia = null, DataFinalizacao = null WHERE Id = @p0";

                deposito.Database.ExecuteSqlCommand(query, pCargaId);

            }

        }


        public static void AlterarRetornoProduto(int pCargaId, int pRetornoProdutoGradeId, double pQuantidade)
        {


            ///alterar com base no 
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var result = deposito.CargaProduto.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pRetornoProdutoGradeId);
                if (result != null)
                {
                    Console.WriteLine("Alterando Retorno Produto - CargaId: " + pCargaId.ToString() + " ProdutoId: " + pRetornoProdutoGradeId.ToString());
                    result.Retorno = pQuantidade;
                    deposito.SaveChanges();
                } else
                {
                    var novacargaproduto = new CargaProduto
                    {
                        CargaId = pCargaId,
                        ProdutoGradeId = pRetornoProdutoGradeId,
                        Quantidade = 0,
                        Retorno = pQuantidade
                    };

                    deposito.CargaProduto.Add(novacargaproduto);
                    deposito.SaveChanges();

                }
            }




        }


        public static Pedido ObterPedido(string pCodigoPedido, int pPedidoId = 0)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                if (pPedidoId == 0)
                {
                    return deposito.Pedido.SingleOrDefault(pi => pi.CodigoPedido == pCodigoPedido);
                } else
                {
                    return deposito.Pedido.SingleOrDefault(pi => pi.Id == pPedidoId);
                }

            }


        }


        public static string InserirPedido(int pVendedorId, int pCargaId)
        {

            Console.WriteLine("Inserindo Pedido");

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var maxPedido = deposito.Pedido.OrderByDescending(i => i.Id).FirstOrDefault();

                int newId = maxPedido == null ? 1 : maxPedido.Id + 1;

                var carga = deposito.Carga.FirstOrDefault();


                var pedidoanterior = deposito.Pedido.FirstOrDefault(pd => pd.VendedorId == pVendedorId);


                /***********
                 * 
                 * A FaixaComissao será implementada manualmente, via código por enquanto. 
                 * Durante a atividade (Trello) Configuração - será implementado via banco de dados
                 * Sugestão: Criar uma tabela especifica para comissao com adição de N faixas.
                 * 
                */



                string vCodigoPedido = "";

                vCodigoPedido += carga.PracaId.ToString().PadLeft(3, '0');
                vCodigoPedido += carga.RepresentanteId.ToString().PadLeft(3, '0');
                vCodigoPedido += carga.Mes.ToString().PadLeft(2, '0');
                vCodigoPedido += carga.Ano.ToString().Substring(2, 2);
                vCodigoPedido += pVendedorId.ToString().PadLeft(5, '0');
                vCodigoPedido += newId.ToString().PadLeft(7, '0');


                var novopedido = new Pedido
                {
                    VendedorId = pVendedorId,
                    CargaId = pCargaId,
                    CargaOriginal = pCargaId,
                    RepresentanteId = carga.RepresentanteId,
                    CodigoPedido = vCodigoPedido,
                    DataLancamento = DateTime.Now,
                    DataPrevisaoRetorno = DateTime.Now.AddDays(50),
                    ValorPedido = 0,
                    ValorCompra = 0,
                    PercentualCompra = 0,
                    FaixaComissao = 0,
                    PercentualFaixa = 0,
                    ValorComissao = 0,
                    ValorLiquido = 0,
                    ValorAReceber = pedidoanterior == null ? 0 : pedidoanterior.ValorLiquido - pedidoanterior.ValorAcerto,
                    ValorAcerto = null,
                    QuantidadeRemarcado = 0,
                    Remarcado = 0,
                    Status = "0"
                };

                deposito.Pedido.Add(novopedido);


                deposito.SaveChanges();

                return vCodigoPedido;


            }
        }

        public static void AtualizarPedido(long pPedidoId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var pedido = deposito.Pedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {



                    var pedidoitem = deposito.PedidoItem.GroupBy(pi => pi.PedidoId)
                   .Select(g => new { pedidoid = g.Key, valorcompra = g.Sum(i => ((i.Quantidade - i.Retorno) * i.Preco)), valorpedido = g.Sum(i => ((i.Quantidade) * i.Preco)) })
                   .FirstOrDefault(pi => pi.pedidoid == pedido.Id);

                    var vValorPedido = pedidoitem.valorpedido; // + (pQuantidade - pRetorno) * pPreco;
                    var vValorCompra = pedidoitem.valorcompra; // + (pQuantidade - pRetorno) * pPreco;

                    Console.WriteLine("ValorCompra:" + vValorCompra.ToString());
                    Console.WriteLine("ValorPedido:" + vValorPedido.ToString());

                    pedido.ValorPedido = vValorPedido;
                    pedido.ValorCompra = vValorCompra;
                    pedido.PercentualCompra = (pedido.ValorCompra / pedido.ValorPedido) * 100;


                    if (pedido.PercentualCompra < 35) // primeira faixa
                    {
                        pedido.FaixaComissao = 35;
                        pedido.PercentualFaixa = 35;
                    }
                    else if (pedido.PercentualCompra >= 35 && pedido.PercentualCompra < 80) // segunda faixa
                    {
                        pedido.FaixaComissao = 80;
                        pedido.PercentualFaixa = 40;
                    }
                    else // terceira faixa
                    {
                        pedido.FaixaComissao = 100;
                        pedido.PercentualFaixa = 45;
                    }

                    pedido.ValorComissao = pedido.ValorCompra * (pedido.PercentualFaixa / 100);
                    pedido.ValorLiquido = pedido.ValorCompra - pedido.ValorComissao;

                    deposito.SaveChanges();
                }
            }
        }


        public static Pedido ObterPedidosAbertoVendedor(long pVendedorId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var pedidos = deposito.Pedido.OrderByDescending(pd => pd.Id).Where(pd => pd.VendedorId == pVendedorId && (pd.Status == "0" || pd.Status == "1" || pd.Status == "2"));
                return pedidos.FirstOrDefault();
            }

        }

        public static List<ListaPedidosRetorno> ObterListaPedidosRetorno(long pCargaId, bool pAtual = true)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {



                string query = @"SELECT CodigoPedido, VendedorId, Nome, ValorPedido, DataLancamento  
	                                FROM Pedido 
	                                INNER JOIN Vendedor ON Pedido.VendedorId = Vendedor.Id
                                WHERE CargaId = @p0 AND CargaOriginal = @p0";

                var result = deposito.Database.SqlQuery<ListaPedidosRetorno>(query, pCargaId);

                return result.ToList<ListaPedidosRetorno>();

            }

        }


        
        public static List<ListaPedidosFechados> ObterListaPedidosFechados(long pCargaId, bool pAtual = true)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                long vCargaId;

                if (pAtual)
                {
                    vCargaId = pCargaId;

                }
                else
                {

                    var carga = deposito.Carga.FirstOrDefault(c => c.Id == pCargaId);

                    var cargaanterior = deposito.Carga.Where(c => c.PracaId == carga.PracaId && c.Id < pCargaId).OrderByDescending(i => i.Id).FirstOrDefault();

                    vCargaId = cargaanterior != null ? cargaanterior.Id : 0;
                }

                string query = @"SELECT CodigoPedido, Nome, ValorPedido, ValorCompra, ValorLiquido, ValorAReceber, ValorAcerto, ValorLiquido+ValorAReceber-ValorAcerto as ValorAberto, DataLancamento, 
	                            CASE Pedido.Status 
	                                WHEN '0' THEN '0 - Aberto'
	                                WHEN '1' THEN '1 - Retorno'
	                                WHEN '2' THEN '2 - Retornado'
	                                WHEN '3' THEN '3 - Fechado'
	                                WHEN '4' THEN '4 - Recebido'
	                                ELSE 'Indefinido' END as Status
	                            FROM Pedido 
	                            INNER JOIN Vendedor ON Pedido.VendedorId = Vendedor.Id
                                WHERE (CargaId = @p0 OR CargaOriginal = @p0) AND DataRetorno IS NOT NULL";

                var result = deposito.Database.SqlQuery<ListaPedidosFechados>(query, vCargaId);

                return result.ToList<ListaPedidosFechados>();

            }

        }



        public static List<ListaPedidoItem> ObterListaPedidoItem(long pPedidoId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string query = @"SELECT PedidoItem.Id as PedidoItemId, Produto.CodigoBarras + '' + ProdutoGrade.Digito as CodigoBarras, Produto.Descricao NomeProduto, PedidoItem.Quantidade, PedidoItem.Retorno, PedidoItem.Preco, ProdutoGradeId
	                            FROM PedidoItem 
		                            INNER JOIN ProdutoGrade ON PedidoItem.ProdutoGradeId = ProdutoGrade.Id
		                            INNER JOIN Produto ON ProdutoGrade.ProdutoId = Produto.Id
                            WHERE PedidoItem.PedidoId = @p0";

                Console.WriteLine("Exibindo lista de itens do pedido # " + pPedidoId.ToString());

                var result = deposito.Database.SqlQuery<ListaPedidoItem>(query, pPedidoId);

                return result.ToList<ListaPedidoItem>();
            }

        }



        public static void AlterarPedidoItem(int pPedidoItemId, double pQuantidade, double pQtdRetorno, double pPreco)
        {            
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var pedidoitem = deposito.PedidoItem.SingleOrDefault(pi => pi.Id == pPedidoItemId);
                if (pedidoitem != null)
                {

                    
                    pedidoitem.Quantidade = pQuantidade;
                    pedidoitem.Retorno = pQtdRetorno;
                    pedidoitem.Preco = pPreco;
                    
                    deposito.SaveChanges();


                    AtualizarPedido(pedidoitem.PedidoId);
                }
            }

        }

        public static void InserirPedidoItem(int pPedidoId, int pProdutoGradeId, double pQuantidade, double pQtdRetorno, double pPreco)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                DateTime dataabertura = DateTime.Now;

                var maxPedidoItem = deposito.PedidoItem.OrderByDescending(i => i.Id).FirstOrDefault();

                int newId = maxPedidoItem == null ? 1 : maxPedidoItem.Id + 1;

                var novopedidoitem = new PedidoItem
                {
                    Id = newId,
                    PedidoId = pPedidoId,
                    ProdutoGradeId = pProdutoGradeId,
                    Quantidade = pQuantidade,
                    Retorno = pQtdRetorno,
                    Preco = pPreco
                };                

                deposito.PedidoItem.Add(novopedidoitem);
                deposito.SaveChanges();


                AtualizarPedido(pPedidoId);


            }

        }

        public static void ExcluirPedidoItem(long pPedidoItemId, long pPedidoId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                
                deposito.Database.ExecuteSqlCommand("DELETE FROM PedidoItem WHERE Id = @pPedidoItemId", new SqlParameter("@pPedidoItemId", pPedidoItemId));

                AtualizarPedido(pPedidoId);

            }

        }



        public static Receber ObterAReceber(int pReceberId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var receber = deposito.Receber.Where(rc => rc.Id == pReceberId).FirstOrDefault();

                return receber;
            }


        }
        public static List<ListaAReceber> ObterListaAReceber(long pCargaId)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var carga = deposito.Carga.FirstOrDefault(c => c.Id == pCargaId);

                int vPracaId = 0;

                vPracaId = carga != null ? Convert.ToInt32(carga.PracaId) : 0;

                string query = @"SELECT Receber.Id, Max(Recebimento.Id) as RecebimentoId, Documento, Serie, Nome, Sum(ValorDuplicata) ValorTotal, Max(ValorAReceber) ValorAReceber, Sum(Recebimento.ValorRecebido) as ValorPago, Max(Recebimento.DataPagamento) DataPagamento
	                                FROM Receber 
		                                INNER JOIN Vendedor ON Receber.VendedorId = Vendedor.Id
		                                LEFT JOIN Recebimento ON Receber.Id = Recebimento.ReceberId
		                                WHERE Receber.CargaId = @p0		                                
			                                AND ValorNF > 0 
			                                AND Receber.CargaId <= @p0
                                GROUP BY Receber.Id, Documento, Serie, Nome
                                ORDER BY Nome";

                Console.WriteLine("Obtendo Lista a Receber - CargaId: " + pCargaId.ToString());

                var result = deposito.Database.SqlQuery<ListaAReceber>(query, pCargaId, vPracaId);

                return result.ToList<ListaAReceber>();

            }

        }


        



        public static void InserirReceber(Receber pReceber)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                

                deposito.Receber.Add(pReceber);
                deposito.SaveChanges();


            }
        }



        public static void AtualizarReceber(Receber pReceber)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var receber = deposito.Receber.FirstOrDefault(rc => rc.Id == pReceber.Id);


                receber.ValorNF = pReceber.ValorNF;
                receber.ValorDuplicata = pReceber.ValorDuplicata;
                receber.ValorAReceber = pReceber.ValorAReceber;

                receber.DataEmissao = pReceber.DataEmissao;
                receber.DataLancamento = pReceber.DataLancamento;
                receber.DataVencimento = pReceber.DataVencimento;

                receber.Observacoes = pReceber.Observacoes;
                
                deposito.SaveChanges();


            }
        }



        public static void AtualizarStatusReceber(int pReceberId, string pStatus, DateTime? pDataPagamento = null)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var receber = deposito.Receber.FirstOrDefault(rc => rc.Id == pReceberId);


                receber.Status = pStatus;
                receber.DataPagamento = pDataPagamento;

                deposito.SaveChanges();


            }
        }

        



        public static Nullable<Double> ObterValorAReceberVendedor(long pVendedorId)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                string query = @"SELECT Sum(ValorAReceber) as ValorAReceber
	                                FROM Receber 
		                                INNER JOIN Vendedor ON Receber.VendedorId = Vendedor.Id
		                                LEFT JOIN ReceberBaixa ON Receber.Id = ReceberBaixa.ReceberId
		                                WHERE (Receber.VendedorId = @p0) 
			                                AND Receber.DataPagamento IS NULL 
			                                AND ValorNF > 0";

                var result = deposito.Database.SqlQuery<Nullable<Double>>(query, pVendedorId);

                return result.FirstOrDefault();

            }

        }


        public static bool ExcluirAReceber(int pReceberId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var receberbaixa = deposito.ReceberBaixa.SingleOrDefault(pd => pd.ReceberId == pReceberId);

                if (receberbaixa == null)
                {
                    return false;
                } else
                {
                    string query = "DELETE FROM Receber WHERE Id = @p0";
                    deposito.Database.ExecuteSqlCommand(query, pReceberId);


                    deposito.SaveChanges();

                    return true;
                }



            }


        }


        public static Recebimento ObterRecebimento(int pRecebimentoId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var recebimento = deposito.Recebimento.Where(rc => rc.Id == pRecebimentoId).FirstOrDefault();

                return recebimento;
            }

        }

        public static List<ListaRecebimento> ObterListaRecebimento(long pVendedorId, long pReceberId = 0)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                string query = @"SELECT 
                                    CASE 
                                        WHEN PedidoId != 0 THEN CONCAT('Pedido:',CodigoPedido)
                                        WHEN ReceberId != 0 THEN CONCAT('Titulo:', Receber.Documento, '/', Receber.Serie)
                                        ELSE '' END as Referencia, 
                                    ValorRecebido, Recebimento.DataPagamento, FormaPagamento, Observacao, Recebimento.Id, 
                                    Recebimento.CargaId, Recebimento.VendedorId, ReceberId, PedidoId, CodigoPedido 
                                        FROM Recebimento
                                            LEFT JOIN Receber ON Receber.VendedorId = Recebimento.VendedorId AND Receber.Id = ReceberId
                                    WHERE Recebimento.VendedorId = @p0 AND Recebimento.ReceberId = @p1";


                var result = deposito.Database.SqlQuery<ListaRecebimento>(query, pVendedorId, pReceberId);

                return result.ToList<ListaRecebimento>();

            }

        }




        public static void InserirRecebimento(string pTipo, int pCargaId, int pVendedorId, double pValorRecebido, string pFormaPagamento, string pObservacao, int pReceberId = 0, int pPedidoId = 0, string pCodigoPedido = "")
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var maxRecebimento = deposito.Recebimento.OrderByDescending(i => i.Id).FirstOrDefault();

                var novorecebimento = new Recebimento
                {
                    Tipo = pTipo,
                    CargaId = pCargaId,
                    VendedorId = pVendedorId,
                    ReceberId = pReceberId,
                    PedidoId = pPedidoId,
                    CodigoPedido = pCodigoPedido,
                    ValorRecebido = pValorRecebido,
                    DataPagamento = DateTime.Now,
                    FormaPagamento = pFormaPagamento,
                    Observacao = pObservacao
                };

                deposito.Recebimento.Add(novorecebimento);

                //atualizar tabela Pedido / Receber

                if (pPedidoId != 0)
                    ReceberPedido(pPedidoId, pValorRecebido);

                if (pReceberId != 0)
                    ReceberTitulo(pReceberId, pCargaId, pValorRecebido);


                deposito.SaveChanges();

            }

        }

        public static void AlterarRecebimento(int pRecebimentoId, double pValorRecebido, string pFormaPagamento, string pObservacao)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var recebimento = deposito.Recebimento.FirstOrDefault(rc => rc.Id == pRecebimentoId);


                if (recebimento != null)
                {

                    double tmpValor = Math.Round(Convert.ToDouble(recebimento.ValorRecebido), 2);

                    recebimento.ValorRecebido = pValorRecebido;
                    recebimento.FormaPagamento = pFormaPagamento;
                    recebimento.Observacao = pObservacao;

                    deposito.SaveChanges();


                    //atualizar tabela Pedido / Receber-- tratar alteração de valor
                    if (recebimento.PedidoId != null && recebimento.PedidoId != 0)
                        ReceberPedido(Convert.ToInt64(recebimento.PedidoId), pValorRecebido, tmpValor);


                    if (recebimento.ReceberId != null && recebimento.ReceberId != 0)
                        ReceberTitulo(Convert.ToInt32(recebimento.ReceberId), Convert.ToInt32(recebimento.CargaId), Math.Round(pValorRecebido, 2), Math.Round(tmpValor, 2));




                }

                deposito.SaveChanges();






            }



        }


        public static void ExcluirRecebimento(long pRecebimentoId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var recebimento = deposito.Recebimento.FirstOrDefault(rc => rc.Id == pRecebimentoId);

                if (recebimento != null)
                {

                    if (recebimento.PedidoId != null && recebimento.PedidoId != 0)
                        ReceberPedido(Convert.ToInt32(recebimento.PedidoId), 0, Convert.ToDouble(recebimento.ValorRecebido));

                    if (recebimento.ReceberId != null && recebimento.ReceberId != 0)
                        ReceberTitulo(Convert.ToInt32(recebimento.ReceberId), Convert.ToInt32(recebimento.CargaId), 0, Convert.ToDouble(recebimento.ValorRecebido));

                    deposito.Database.ExecuteSqlCommand("DELETE FROM Recebimento WHERE Id = @pRecebimentoId", new SqlParameter("@pRecebimentoId", pRecebimentoId));

                    deposito.SaveChanges();
                }

            }

        }



        public static void ReceberPedido(long pPedidoId, double pValor, double? pValorAnterior = null)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var pedido = deposito.Pedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {


                    if (pedido.DataRetorno == null)
                    {

                        pedido.DataRetorno = DateTime.Now.Date;
                    }

                    if (pValorAnterior == null)
                    {
                        if (pedido.ValorAcerto == null) pedido.ValorAcerto = 0;
                        pedido.ValorAcerto += pValor;

                    }
                    else
                    {
                        pedido.ValorAcerto = pedido.ValorAcerto - pValorAnterior + pValor;
                    }

                    pedido.Status = "3";

                    deposito.SaveChanges();

                }



            }

        }


        public static void ReceberTitulo(long pReceberId, long pCargaId, double pValor, double? pValorAnterior = null)
        {

            Console.WriteLine("Inserindo Receber Baixa");

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var receber = deposito.Receber.SingleOrDefault(rc => rc.Id == pReceberId);

                if (receber != null)
                {


                    if (pValorAnterior == null)
                    {
                        receber.ValorAReceber = Math.Round(Convert.ToDouble(receber.ValorAReceber-pValor),2);


                    }
                    else
                    {
                        receber.ValorAReceber = receber.ValorAReceber + pValorAnterior - pValor;
                    }

                    if (receber.ValorAReceber <= 0)
                    {
                        receber.DataPagamento = DateTime.Now;
                        receber.Status = "1";
                    }
                    else
                    {
                        receber.DataPagamento = null;
                        receber.Status = "0";
                    }

                    deposito.SaveChanges();

                }


            }

        }



        public static void SalvarAReceberBaixa(int pReceberId, int pReceberBaixaId, double pValor, string pData, int? pCargaId = null)
        {



            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                if (pReceberBaixaId == 0)
                {

                    var maxReceberBaixa = deposito.ReceberBaixa.OrderByDescending(i => i.Id).FirstOrDefault();



                    int newId = maxReceberBaixa == null ? 1 : maxReceberBaixa.Id + 1;

                    Console.WriteLine("Inserindo Baixa A Receber - ReceberId: " + pReceberId.ToString());

                    //insert
                    var receberbaixa = new ReceberBaixa
                    {
                        Id = newId,
                        ReceberId = Convert.ToInt32(pReceberId),
                        CargaId = pCargaId,
                        Valor = pValor,
                        DataPagamento = Convert.ToDateTime(pData),
                        DataBaixa = DateTime.Now

                    };

                    deposito.ReceberBaixa.Add(receberbaixa);
                    deposito.SaveChanges();



                }
                else
                {

                    var result = deposito.ReceberBaixa.SingleOrDefault(rb => rb.Id == pReceberBaixaId);
                    if (result != null)
                    {
                        Console.WriteLine("Alterando Baixa A Receber - Id: " + pReceberBaixaId.ToString());
                        result.Valor = pValor;
                        result.DataPagamento = Convert.ToDateTime(pData);
                        deposito.SaveChanges();
                    }

                }

            }




        }



        public static void ExcluirReceberBaixa(long pReceberBaixaId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {



                string query = "DELETE FROM ReceberBaixa WHERE Id = @p0";
                deposito.Database.ExecuteSqlCommand(query, pReceberBaixaId);


                deposito.SaveChanges();

            }


        }




        public static List<ListaAcerto> ObterListaAcerto(long pCargaId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string query = @"SELECT Nome, '' ValorPedido, '' ValorCompra, '' ValorLiquido, 0 ValorReceber, ValorDuplicata-ValorAReceber ValorAcerto, ValorAReceber ValorAberto, DataEmissao Data 
                                    FROM Vendedor 
                                        INNER JOIN Receber ON Vendedor.Id = Receber.VendedorId
                                      WHERE ValorDuplicata > ValorAReceber AND CargaId = @p0
                                    UNION 
                                      SELECT Nome, ValorPedido, ValorCompra, ValorLiquido, ValorAReceber ValorReceber, ValorAcerto, ValorLiquido+ValorAReceber-ValorAcerto ValorAberto, DataLancamento Data 
                                        FROM Vendedor
                                            INNER JOIN Pedido ON Vendedor.Id = Pedido.VendedorId
                                         WHERE CargaId = @p0;";

                var result = deposito.Database.SqlQuery<ListaAcerto>(query, pCargaId);

                return result.ToList<ListaAcerto>();

            }

        }



        public static List<Vendedor> ObterListaVendedor(long pCargaId = 0)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                List<Vendedor> result;

                if (pCargaId == 0) {

                    result = deposito.Vendedor.OrderBy(vd => vd.Nome).ToList<Vendedor>();

                } else
                {


                    var carga = deposito.Carga.Where(c => c.Id == pCargaId).FirstOrDefault();

                    if (carga != null)
                    {


                        string query = @"SELECT * 
                                    FROM Vendedor 
                                    WHERE 
                                    /* Com pedido anterior */
                                    Id IN(
                                    SELECT Distinct VendedorId 
                                    FROM Pedido 
                                    WHERE CargaId in(SELECT Id FROM Carga WHERE RepresentanteId = @p0 and PracaId = @p1 and FORMAT(DataAbertura, 'yyyyMM') <= @p2)
                                    ) 
                                    /* Sem pedido atual */
                                    OR  Id IN (
                                    SELECT Distinct VendedorId 
                                    FROM Receber 
                                    INNER JOIN Carga ON Receber.CargaId = Carga.Id
                                    WHERE PracaId = @p1
                                    ) ORDER BY Nome";


                        result = deposito.Database.SqlQuery<Vendedor>(query, carga.RepresentanteId, carga.PracaId, carga.Mes.ToString() + carga.Ano.ToString()).ToList<Vendedor>();

                    } else
                    {

                        result = null;
                    }

                                       


                }


                return result;
            }
        }


        public static Vendedor ObterVendedor(long pVendedorId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vendedor = deposito.Vendedor.Where(vd => vd.Id == pVendedorId).FirstOrDefault();

                return vendedor;

            }
        }


        public static List<ListaVendedor> PesquisarVendedor(long pVendedorId = 0, string pCPFCNPJ = "", string pNome = "")
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string query = @"SELECT Id, Nome, CPFCnpj, Cidade + '/' + UF Cidade, Status, Observacao FROM Vendedor WHERE 1=1 ";

                if (pVendedorId != 0)
                {
                    query += " AND Id = " + pVendedorId;
                }

                if (pCPFCNPJ != "")
                {
                    query += " AND CPFCnpj = '" + pCPFCNPJ + "'";
                }

                if (pNome != "")
                {
                    query += " AND Nome Like '%" + pNome + "%'";
                }

                var result = deposito.Database.SqlQuery<ListaVendedor>(query).ToList<ListaVendedor>();


                return result;


            }
        }

        //public static Vendedor PesquisarVendedor(string pCPFCnpj = "")
        //{
        //    using (DepositoDBEntities deposito = new DepositoDBEntities())
        //    {

        //        var vendedor = deposito.Vendedor.Where(vd => vd.CpfCnpj.Trim() == pCPFCnpj).FirstOrDefault();

        //        return vendedor;

        //    }
        //}
        

        public static void NegativarVendedor(long pVendedorId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var vendedor = deposito.Vendedor.SingleOrDefault(vd => vd.Id == pVendedorId);
                if (vendedor != null)
                {
                    vendedor.Status = "N";
                    deposito.SaveChanges();
                }
            }

        }



        public static Estoque ObterEstoqueMovimentacao(long pEstoqueId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var estoque = deposito.Estoque.Where(vd => vd.Id == pEstoqueId).FirstOrDefault();

                return estoque;

            }
        }

        public static List<Estoque> ObterListaEstoqueMovimentacao(int pProdutoGradeId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                string query = @"SELECT 
	                                Id, CASE TipoMovimentacao 
	                                WHEN 'E' THEN '(+) Entrada'
	                                WHEN 'S' THEN '(-) Saida'	       
	                                ELSE 'Indefinido' END as TipoMovimentacao, Quantidade, Observacao, ProdutoGradeId, temp_old_id
                                FROM Estoque 
                                    WHERE ProdutoGradeId = @p0";                

                var result = deposito.Database.SqlQuery<Estoque>(query, pProdutoGradeId).ToList<Estoque>();

                return result.ToList<Estoque>();

            }
        }


        public static void SalvarEstoqueMovimentacao(int pEstoqueId, int pProdutoGradeId, string pTipoMovimentacao, double pQuantidade, string pObservacao)
        {



            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                if (pEstoqueId == 0) // Incluir
                {

                    var maxEstoque = deposito.Estoque.OrderByDescending(i => i.Id).FirstOrDefault();



                    int newId = maxEstoque == null ? 1 : maxEstoque.Id + 1;


                    //insert
                    var estoque = new Estoque
                    {
                        Id = newId,
                        ProdutoGradeId = pProdutoGradeId,
                        TipoMovimentacao = pTipoMovimentacao,
                        Quantidade = pQuantidade,
                        Observacao = pObservacao                        
                    };

                    deposito.Estoque.Add(estoque);


                    var produtograde = deposito.ProdutoGrade.SingleOrDefault(rb => rb.Id == pProdutoGradeId);
                    if (pTipoMovimentacao == "E")
                    {
                        produtograde.Quantidade += pQuantidade;

                    } else
                    {
                        produtograde.Quantidade -= pQuantidade;
                    }


                    deposito.SaveChanges();



                }
                else
                {

                    var result = deposito.Estoque.SingleOrDefault(rb => rb.Id == pEstoqueId);
                    if (result != null)
                    {
                        var produtograde = deposito.ProdutoGrade.SingleOrDefault(rb => rb.Id == result.ProdutoGradeId);

                        if (result.TipoMovimentacao == "E")
                        {
                            produtograde.Quantidade -= result.Quantidade;
                        } else
                        {
                            produtograde.Quantidade += result.Quantidade;
                        }

                        
                        result.TipoMovimentacao = pTipoMovimentacao;
                        result.Quantidade = pQuantidade;
                        result.Observacao = pObservacao;


                        if (pTipoMovimentacao == "E")
                        {
                            produtograde.Quantidade += pQuantidade;
                        }
                        else
                        {
                            produtograde.Quantidade -= pQuantidade;
                        }


                        deposito.SaveChanges();



                    }

                }

            }




        }



        public static void ExcluirEstoqueMovimentacao(long pEstoqueId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var result = deposito.Estoque.SingleOrDefault(rb => rb.Id == pEstoqueId);
                if (result != null)
                {
                    var produtograde = deposito.ProdutoGrade.SingleOrDefault(rb => rb.Id == result.ProdutoGradeId);

                    if (result.TipoMovimentacao == "E")
                    {
                        produtograde.Quantidade -= result.Quantidade;
                    }
                    else
                    {
                        produtograde.Quantidade += result.Quantidade;
                    }

                    string query = "DELETE FROM Estoque WHERE Id = @p0";
                    deposito.Database.ExecuteSqlCommand(query, pEstoqueId);

                    deposito.SaveChanges();


                }

            }


        }


        public static void AtualizarCargaRetornoEstoque(long pCargaId, string pFuncao)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                // obter lista de cargaproduto

                foreach (var row in deposito.CargaProduto.Where(cp => cp.CargaId == pCargaId))
                {
                    if (pFuncao == "Carga")
                    {
                        // retirar do estoque a Quantidade
                        EstoqueMovimentacaoAutomatica(row.ProdutoGradeId, -Convert.ToInt32(row.Quantidade), "Carga: " + pCargaId.ToString());

                    } else
                    {
                        // acrescentar ao estoque o Retorno
                        EstoqueMovimentacaoAutomatica(row.ProdutoGradeId, Convert.ToInt32(row.Quantidade), "Retorno: " + pCargaId.ToString());

                    }


                }

            }



        }


        public static void EstoqueMovimentacaoAutomatica(int pProdutoGradeId, double pQuantidade, string pInfo)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var maxEstoque = deposito.Estoque.OrderByDescending(i => i.Id).FirstOrDefault();
                               
                int newId = maxEstoque == null ? 1 : maxEstoque.Id + 1;

                string vTipoMovimentacao;

                double vQuantidade;


                if (pQuantidade > 0)
                {
                    vTipoMovimentacao = "E";
                    vQuantidade = pQuantidade;
                } else
                {
                    vTipoMovimentacao = "S";
                    vQuantidade = -pQuantidade;
                }


                //insert
                var estoque = new Estoque
                {
                    Id = newId,
                    ProdutoGradeId = pProdutoGradeId,
                    TipoMovimentacao = vTipoMovimentacao,
                    Quantidade = vQuantidade,
                    Observacao = "Movimentação automatica - " + pInfo
                };

                deposito.Estoque.Add(estoque);


                var produtograde = deposito.ProdutoGrade.SingleOrDefault(rb => rb.Id == pProdutoGradeId);
                if (vTipoMovimentacao == "E")
                {
                    produtograde.Quantidade += vQuantidade;

                }
                else
                {
                    produtograde.Quantidade -= vQuantidade;
                }


                deposito.SaveChanges();
            }

        }


        public static List<Categoria> ObterListaCategorias()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Categoria.ToList<Categoria>();
            }
        }

        public static List<Fornecedor> ObterListaFornecedores()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Fornecedor.ToList<Fornecedor>();
            }
        }

        public static List<Cor> ObterListaCores()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Cor.ToList<Cor>();
            }
        }


        public static List<Tamanho> ObterListaTamanhos()
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                return deposito.Tamanho.ToList<Tamanho>();
            }
        }







        public static ListaTotalizadoresDeposito ObterTotalizadores(int pCargaId)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                ListaTotalizadoresDeposito vTotalizador = new ListaTotalizadoresDeposito();



                string query = @"SELECT Carga.Id, sum(CargaProduto.Quantidade) QtdProdutos, sum(CargaProduto.Quantidade)*sum(ValorSaida) TotalProdutos  
                                    FROM Carga 
                                    INNER JOIN CargaProduto On CargaProduto.CargaId = Carga.Id 
                                    INNER JOIN ProdutoGrade ON ProdutoGradeId = ProdutoGrade.Id 
                                 WHERE Carga.Id = @p0
                                 GROUP BY Carga.Id";



                var totalizador = deposito.Database.SqlQuery<ListaTotalizadoresDeposito>(query, pCargaId).FirstOrDefault();

                if (totalizador != null)
                {
                    vTotalizador.QtdProdutos = totalizador.QtdProdutos;
                    vTotalizador.TotalProdutos = totalizador.TotalProdutos;
                } else
                {
                    vTotalizador.QtdProdutos = 0;
                    vTotalizador.TotalProdutos = 0;
                }



                return vTotalizador;


            }

        }


    }
}
