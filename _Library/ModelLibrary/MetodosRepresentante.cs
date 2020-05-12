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
    public class MetodosRepresentante
    {


        /*Usuario usuario = new Usuario();
        Representante representante = new Representante();
        Praca praca = new Praca();*/

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

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var usuario = representante.RepUsuario.FirstOrDefault(u => u.Login.ToLower() == pLogin.ToLower());

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

        public static Boolean VerificarImportacao()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                var count = representante.RepUsuario.Count();

                if (count > 0) return true; else return false;  

                

            }            
            
        }



        public static List<RepPraca> ObterListaPracas()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                return representante.RepPraca.ToList<RepPraca>();
            }
        }

        public static RepPraca ObterPraca(int i)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                var praca = representante.RepPraca.FirstOrDefault(p => p.Id == i);
                return praca;
            }
        }


        public static List<RepRepresentante> ObterListaRepresentantes()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                return representante.RepRepresentante.ToList<RepRepresentante>();
            }
        }


        public static RepRepresentante ObterRepresentante(int i)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                var orepresentante = representante.RepRepresentante.FirstOrDefault(p => p.Id == i);
                return orepresentante;
            }
        }


        public static List<RepCarga> ObterListaCargas()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                return representante.RepCarga.ToList<RepCarga>();
            }
        }

        public static RepCarga ObterCarga(long pRepresentanteId, long pPracaId, int pMes, int pAno)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var carga = representante.RepCarga.FirstOrDefault(r => r.RepresentanteId == pRepresentanteId && r.PracaId == pPracaId && r.Mes == pMes && r.Ano == pAno);

                return carga;

            }
        }

        public static RepCarga ObterCargaAtual()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var carga = representante.RepCarga.FirstOrDefault();

                return carga;

            }
        }


        public static RepCargaAnterior ObterCargaAnterior()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var carganterior = representante.RepCargaAnterior.OrderByDescending(c => c.Id).FirstOrDefault();

                return carganterior;

            }
        }


        public static RepCargaProduto ObterCargaProduto(long pCargaId, long pProdutoId)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var cargaproduto = representante.RepCargaProduto.FirstOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pProdutoId);

                return cargaproduto;

            }

        }


        public static void AlterarStatusCarga(long pCargaId, string pStatus)
        {


            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                var result = representante.RepCarga.SingleOrDefault(cg => cg.Id == pCargaId);
                if (result != null)
                {
                    result.Status = pStatus;


                    switch (pStatus)
                    {
                        case "E":
                            result.DataExportacao = DateTime.Now;
                            break;
                        case "R":
                            result.DataRetorno = DateTime.Now;
                            break;
                        case "C":
                            result.DataConferencia = DateTime.Now;
                            break;
                        case "F":
                            result.DataFinalizacao = DateTime.Now;
                            break;
                    }


                    representante.SaveChanges();
                }


            }

        }

        public static List<ListaRepProdutosConferencia> ObterProdutosConferencia(long pCargaId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                

                string query = @"SELECT RepProduto.CodigoBarras || RepProdutoGrade.Digito as CodigoBarras,
	                                RepProduto.Descricao,
	                                RepCargaProduto.Quantidade as QuantidadeCarga,
	                                RepCargaConferencia.Quantidade as QuantidadeInformada,
	                                RepCargaProduto.Quantidade - IFNULL(RepCargaConferencia.Quantidade, 0) as Diferenca,
									(RepCargaProduto.Quantidade - IFNULL(RepCargaConferencia.Quantidade, 0)) * RepProdutoGrade.ValorSaida as ValorDiferenca,
	                                RepProduto.Id as ProdutoId,
	                                RepCargaProduto.Id as CargaProdutoId,
	                                RepProdutoGrade.Id as ProdutoGradeId,
	                                RepCarga.Id as CargaId,
                                    RepCargaProduto.Tipo,
                                    '' as Acao
	                                FROM RepCarga 
                                INNER JOIN RepCargaProduto ON RepCarga.Id = RepCargaProduto.CargaId
                                INNER JOIN RepProdutoGrade ON RepCargaProduto.ProdutoGradeId = RepProdutoGrade.Id
                                INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id
                                LEFT JOIN RepCargaConferencia ON RepCargaConferencia.CargaId = RepCarga.Id AND RepCargaConferencia.ProdutoGradeId = RepProdutoGrade.Id
                                WHERE Tipo != 'S' AND Tipo != 'P'";

                var result = representante.Database.SqlQuery<ListaRepProdutosConferencia>(query);


                return result.ToList<ListaRepProdutosConferencia>();

            }
        }

        public static Boolean ConferenciaFinalizada()
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                string query = @"SELECT SUM(RepCargaProduto.Quantidade - IFNULL(RepCargaConferencia.Quantidade, 0)) as Diferenca
	                            FROM RepCarga 
                                    INNER JOIN RepCargaProduto ON RepCarga.Id = RepCargaProduto.CargaId
                                    INNER JOIN RepProdutoGrade ON RepCargaProduto.ProdutoGradeId = RepProdutoGrade.Id
                                    INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id
                                    LEFT JOIN RepCargaConferencia ON RepCargaConferencia.CargaId = RepCarga.Id AND RepCargaConferencia.ProdutoGradeId = RepProdutoGrade.Id
                                WHERE Tipo != 'S' AND Tipo != 'P'";

                var result = representante.Database.SqlQuery<int>(query);

                if (result.FirstOrDefault<int>() > 0)
                {
                    return false;
                } else
                {
                    return true;
                }
            }

        }


        public static List<ListaRepProdutos> ObterListaProdutos(Dictionary<string, string> pCriterio = null)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                //" CodigoBarras Like '%" + localRepresentanteForm.txtProdutosCodigoBarras.Text + "%'"

                string vCriterio = "";

                string query = @"SELECT Produto.CodigoBarras || '' || Produto.Digito as CodigoBarras, 
                                    Produto.Descricao, Produto.Tamanho, Produto.Cor, Produto.ValorSaida, 
	                                (IFNULL(Carga.ViagemPlus,0) - IFNULL(Vendido.Vendido,0) - IFNULL(Vendido.RetornoPlus,0)+IFNULL(Consignado.ConsignadoRetorno,0)  - IFNULL(Consignado.Consignado,0)) SaldoEstoque, Produto.ProdutoGradeId
	                                FROM
		                                (SELECT RepProduto.Id Id, RepProdutoGrade.Id ProdutoGradeId, RepProduto.CodigoBarras, RepProdutoGrade.Digito, RepProduto.Descricao, RepProdutoGrade.Tamanho, RepProdutoGrade.Cor, RepProdutoGrade.ValorSaida
			                                FROM RepProduto 
			                                INNER JOIN RepProdutoGrade ON RepProdutoGrade.ProdutoId = RepProduto.Id) AS Produto
	                                LEFT JOIN 
		                                (SELECT RepProdutoGrade.Id ProdutoGradeId, RepProdutoGrade.ValorSaida Preco, RepCargaProduto.Quantidade ViagemPlus, RepCargaProduto.Retorno ContagemCarro 
			                                FROM RepCargaProduto 
			                                INNER JOIN RepProdutoGrade ON RepProdutoGrade.Id = RepCargaProduto.ProdutoGradeId WHERE RepCargaProduto.Tipo != 'P') AS Carga ON Produto.ProdutoGradeId = Carga.ProdutoGradeId
	                                LEFT JOIN 
		                                (SELECT ProdutoGradeId, SUM(RepPedidoItem.Retorno) ConsignadoRetorno, SUM(RepPedidoItem.Quantidade) Consignado 
			                                FROM RepPedido
			                                INNER JOIN RepPedidoItem ON RepPedidoItem.PedidoId = RepPedido.Id
			                                WHERE  RepPedido.ValorAcerto <= 0 OR RepPedido.ValorAcerto IS NULL											
			                                GROUP BY ProdutoGradeId) AS Consignado ON Produto.ProdutoGradeId = Consignado.ProdutoGradeId
	                                LEFT JOIN 
		                                (SELECT ProdutoGradeId, SUM(RepPedidoItem.Quantidade) Vendido, SUM(RepPedidoItem.Retorno) RetornoPlus  
			                                FROM RepPedido
			                                INNER JOIN RepPedidoItem ON RepPedidoItem.PedidoId = RepPedido.Id
			                                WHERE RepPedido.ValorAcerto > 0
			                                GROUP BY ProdutoGradeId) AS Vendido ON Produto.ProdutoGradeId = Vendido.ProdutoGradeId
";

                if (pCriterio != null)
                {
                    if (pCriterio.ContainsKey("CodigoBarras"))
                    {
                        vCriterio = " Produto.CodigoBarras || '' || Produto.Digito  LIKE '%" + pCriterio["CodigoBarras"] + "%'";
                    }


                    if (pCriterio.ContainsKey("CodigoGeral"))
                    {
                        if (pCriterio["CodigoGeral"].Length > 6) //pesquisa pelo codigo de barras
                        {
                            vCriterio = " Produto.CodigoBarras || '' || Produto.Digito  LIKE '%" + pCriterio["CodigoGeral"] + "%' ";
                        } else // pesquisa pelo ID
                        {
                            vCriterio = " Produto.Id = " + pCriterio["CodigoGeral"] + " ";
                        }
                        
                    }

                    if (pCriterio.ContainsKey("Nome"))
                    {
                        vCriterio += vCriterio != "" ? " AND " : ""; 
                        vCriterio += " Descricao LIKE '%" + pCriterio["Nome"] + "%'";
                    }      

                    if (pCriterio.ContainsKey("SaldoEstoque"))
                    {
                        if (pCriterio["SaldoEstoque"] == "Y")
                        {
                            vCriterio += vCriterio != "" ? " AND " : "";
                            vCriterio += " (IFNULL(Carga.ViagemPlus,0) - IFNULL(Vendido.Vendido,0) - IFNULL(Vendido.RetornoPlus,0)+IFNULL(Consignado.ConsignadoRetorno,0)  - IFNULL(Consignado.Consignado,0)) > 0 ";
                        }
                        else if (pCriterio["SaldoEstoque"] == "N")
                        {
                            vCriterio += vCriterio != "" ? " AND " : "";
                            vCriterio += " (IFNULL(Carga.ViagemPlus,0) - IFNULL(Vendido.Vendido,0) - IFNULL(Vendido.RetornoPlus,0)+IFNULL(Consignado.ConsignadoRetorno,0)  - IFNULL(Consignado.Consignado,0)) <= 0 ";
                        }
                    }
                    

                }

                query += vCriterio != "" ? " WHERE " + vCriterio : "";


                Console.WriteLine("Listando Produtos Query: " + query);

                var result = representante.Database.SqlQuery<ListaRepProdutos>(query);


                return result.ToList<ListaRepProdutos>();

            }
        }



        public static RepProduto ObterProduto(string pCodigo)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var produto = (from p in representante.RepProduto
                               where (p.CodigoBarras == pCodigo)
                               select p).FirstOrDefault<RepProduto>();

                return produto;
            }

        }


        public static List<RepProdutoGrade> ObterProdutosGrade(string pPesquisa)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                if (pPesquisa != "")
                {


                    if (pPesquisa.Length > 6) // pesquisa pelo codigo de barras
                    {
                        string vCodigoSemDigito = pPesquisa.Substring(0, pPesquisa.Length - 1);
                        string vDigito = pPesquisa.Substring(pPesquisa.Length - 1);

                        return (from pg in representante.RepProdutoGrade
                                            where (((pg.CodigoBarras.ToString() + pg.Digito.ToString()).Contains(pPesquisa)))
                                select pg).ToList<RepProdutoGrade>();

                    } else // pesquisa pelo ID
                    {
                        long vProdutoId = Convert.ToInt64(pPesquisa);


                        return  (from pg in representante.RepProdutoGrade
                                            where (pg.ProdutoId == vProdutoId)
                                            select pg).ToList<RepProdutoGrade>();
                  
                    }



                    

                } else
                {
                    return null;
                }
            }

        }

        public static RepProdutoGrade ObterProdutoGrade(string pCodigo, long pProdutoGradeId = 0)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                if (pProdutoGradeId > 0)
                {
                    var produtograde = (from pg in representante.RepProdutoGrade
                                        where (pg.Id == pProdutoGradeId)
                                        select pg).FirstOrDefault<RepProdutoGrade>();

                    return produtograde;
                } else
                {
                    if (pCodigo != "")
                    {
                        string vCodigoSemDigito = pCodigo.Substring(0, pCodigo.Length - 1);
                        string vDigito = pCodigo.Substring(pCodigo.Length - 1);

                        var produtograde = (from pg in representante.RepProdutoGrade
                                            where (pg.CodigoBarras == vCodigoSemDigito && pg.Digito == vDigito)
                                            select pg).FirstOrDefault<RepProdutoGrade>();

                        return produtograde;
                    }
                    else
                    {
                        return null;
                    }
                }


                
            }

        }





        public static void InserirProdutoConferencia(long pCargaId, long pProdutoGradeId, decimal pQuantidade)
        {


            Console.WriteLine("InserirProdutoConferencia");

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var produto = representante.RepCargaProduto.FirstOrDefault(pd => pd.ProdutoGradeId == pProdutoGradeId);

                if (produto == null)
                {

                    var maxcargaproduto = representante.RepCargaProduto.OrderByDescending(i => i.Id).FirstOrDefault();

                    long newId = maxcargaproduto == null ? 999001 : maxcargaproduto.Id + 1;

                    var novacargaproduto = new RepCargaProduto
                    {
                        Id = newId,
                        CargaId = pCargaId,
                        ProdutoGradeId = pProdutoGradeId,
                        Quantidade = 0,
                        Retorno = 0,
                        Tipo = "I"
                    };

                    representante.RepCargaProduto.Add(novacargaproduto);

                    var novacargaconferencia = new RepCargaConferencia
                    {
                        CargaId = pCargaId,
                        ProdutoGradeId = pProdutoGradeId,
                        Quantidade = pQuantidade
                    };

                    representante.RepCargaConferencia.Add(novacargaconferencia);

                    representante.SaveChanges();

                } else
                {
                    var result = representante.RepCargaConferencia.SingleOrDefault(cp => cp.ProdutoGradeId == pProdutoGradeId && cp.CargaId == pCargaId);

                    if (result != null)
                    {
                        Console.WriteLine("Inserindo Produto Conferencia");
                        var tmpQtd = result.Quantidade;

                        Console.WriteLine("Valor:" + tmpQtd.ToString());
                        result.Quantidade = (tmpQtd + pQuantidade);

                    }
                    else
                    {


                        Console.WriteLine("Atualizando Carga Produto");
                        var novacargaproduto = new RepCargaConferencia
                        {
                            CargaId = pCargaId,
                            ProdutoGradeId = pProdutoGradeId,
                            Quantidade = pQuantidade
                        };

                        representante.RepCargaConferencia.Add(novacargaproduto);

                    }


                    representante.SaveChanges();
                }

                



            }


        }


        public static void AlterarProdutoConferencia(long pCargaId, long pCargaProdutoGradeId, decimal pQuantidade)
        {

            Console.WriteLine("Alterando Produto Conferencia - CargaId: " + pCargaId.ToString() + " CargaProdutoGradeId >" + pCargaProdutoGradeId.ToString() + " Quantidade>" + pQuantidade.ToString());

            ///alterar com base no 
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                var result = representante.RepCargaConferencia.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pCargaProdutoGradeId);
                if (result != null)
                {
                    result.Quantidade = pQuantidade;
                    representante.SaveChanges();
                }
            }

        }


        public static void ExcluirProdutoConferencia(long pCargaId, long pCargaProdutoGradeId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                representante.Database.ExecuteSqlCommand("DELETE FROM RepCargaConferencia WHERE CargaId = @pCargaId AND ProdutoGradeId = @pProdutoGradeId", new SQLiteParameter("@pCargaId", pCargaId), new SQLiteParameter("@pProdutoGradeId", pCargaProdutoGradeId));
                representante.Database.ExecuteSqlCommand("DELETE FROM RepCargaProduto WHERE CargaId = @pCargaId AND ProdutoGradeId = @pProdutoGradeId AND Tipo = 'I'", new SQLiteParameter("@pCargaId", pCargaId), new SQLiteParameter("@pProdutoGradeId", pCargaProdutoGradeId));
                representante.SaveChanges();
            }
        }






        public static void ResolverConflitoProdutoConferencia(long pCargaId, long pCargaProdutoGradeId, decimal pQuantidade)
        {

            Console.WriteLine("Alterando Produto Conferencia - CargaId: " + pCargaId.ToString() + " CargaProdutoGradeId >" + pCargaProdutoGradeId.ToString() + " Quantidade>" + pQuantidade.ToString());
           
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                var cargaconferencia = representante.RepCargaConferencia.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pCargaProdutoGradeId);
                if (cargaconferencia != null)
                {
                    cargaconferencia.Quantidade = pQuantidade;
                }

                var cargaproduto = representante.RepCargaProduto.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pCargaProdutoGradeId);
                if (cargaproduto != null)
                {
                    if (cargaproduto.Tipo == "I") {

                        cargaproduto.Tipo = "S";

                        representante.Database.ExecuteSqlCommand("DELETE FROM RepCargaConferencia WHERE CargaId = @pCargaId AND ProdutoGradeId = @pCargaProdutoGradeId", new SQLiteParameter("@pCargaId", pCargaId), new SQLiteParameter("@pCargaProdutoGradeId", pCargaProdutoGradeId));

                    }
                    cargaproduto.Quantidade = pQuantidade;
                }
                representante.SaveChanges();
            }

        }


        public static void FinalizarConferenciaProduto()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"INSERT INTO RepCargaConferencia (Id, CargaId, ProdutoGradeId, Quantidade)
                                    SELECT RepCargaConferencia.ROWID, RepCargaProduto.CargaId, RepCargaProduto.ProdutoGradeId, 0 
                                        FROM RepCargaProduto
                                            LEFT JOIN RepCargaConferencia 
                                                ON RepCargaConferencia.CargaId = RepCargaProduto.CargaId 
                                                AND RepCargaConferencia.ProdutoGradeId = RepCargaProduto.ProdutoGradeId
                                 WHERE RepCargaConferencia.Id IS NULL AND RepCargaProduto.Tipo != 'P'";

                representante.Database.ExecuteSqlCommand(query);                
                representante.SaveChanges();

            }
        }


        public static void RefazerConferenciaProduto()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                representante.Database.ExecuteSqlCommand("DELETE FROM RepCargaConferencia");
                representante.Database.ExecuteSqlCommand("DELETE FROM RepCargaProduto WHERE Tipo = 'I'");
                
                representante.SaveChanges();

                

            }
        }

        public static List<RepVendedor> ObterListaVendedor()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                return representante.RepVendedor.ToList<RepVendedor>();
            }
        }

        public static List<RepCidade> ObterListaCidade(long pEstadoId = 0)
        {
            

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                if (pEstadoId == 0)
                {
                    return representante.RepCidade.ToList<RepCidade>();
                } else
                {
                    return representante.RepCidade.Where(cid => cid.EstadoId == pEstadoId).ToList<RepCidade>();
                }                
            }

        }

        public static List<RepEstado> ObterListaEstado()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                return representante.RepEstado.ToList<RepEstado>();
            }
        }


        public static RepVendedor ObterVendedor(long pVendedorId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var vendedor = representante.RepVendedor.Where(vd => vd.Id == pVendedorId).FirstOrDefault();

                return vendedor;

            }
        }





        public static List<ListaRepVendedorHome> ObterListaVendedorHome(long pCargaId, string pFiltro = "")
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT RepVendedor.Id, Nome, CpfCnpj as Documento, Endereco || ' ' || IFNULL(Numero, '') Endereco, Complemento, Bairro, Cidade || '/' || UF as CidadeUF, Telefone || '/' || Celular as Telefones,
                                    CASE WHEN PedidoAnterior.VendedorId IS NOT NULL
                                            THEN true
                                            ELSE false
                                            END AS PedidoAnterior, 
                                    CASE 
										WHEN cast(PedidoAnterior.Status as integer) < 2 THEN 'Retornar'
	                                    WHEN ValorAberto  <= 0 THEN 'Total'
	                                    WHEN ValorRecebido = 0  THEN 'Não ' || QuantidadeRemarcado
										WHEN ValorRecebido >0  THEN 'Parcial ' || QuantidadeRemarcado
	                                    END AS Recebido,	   
                                    CASE WHEN PedidoAtual.VendedorId IS NOT NULL
                                            THEN true
                                            ELSE false
                                            END AS PedidoAtual, 
									CASE WHEN PedidoAtual.VendedorId IS NOT NULL AND ValorAberto  <= 0
										THEN CodigoPedidoAnterior
										ELSE CodigoPedidoAtual
										END AS CodigoPedido,
                                    CASE 
										WHEN (IFNULL(ValorAberto,0) > 0 OR IFNULL(Receber,0) > 0) AND IFNULL(ValorRecebido,0) <= 0 THEN true
	                                    ELSE false
	                                    END AS Receber,
								    CASE WHEN 
										RepVendedor.Status = 2 THEN true
										ELSE false
										END AS Negativado									
                                    FROM RepVendedor
                                    LEFT JOIN (SELECT max(CodigoPedido) CodigoPedidoAtual, VendedorId From RepPedido WHERE CargaId = CargaOriginal GROUP BY VendedorId) AS PedidoAtual ON RepVendedor.Id = PedidoAtual.VendedorId
                                    LEFT JOIN (SELECT VendedorId, max(CodigoPedido) CodigoPedidoAnterior, SUM(QuantidadeRemarcado)-1 QuantidadeRemarcado, SUM(ValorLiquido - ValorAcerto) ValorAberto, ValorAcerto as ValorRecebido, Status FROM RepPedido /*WHERE (RepPedido.CodigoPedido NOT IN (SELECT max(CodigoPedido) FROM RepPedido GROUP BY VendedorId) OR CargaId != CargaOriginal)*/ GROUP BY CodigoPedido, Status) AS PedidoAnterior ON RepVendedor.Id = PedidoAnterior.VendedorId
                                    LEFT JOIN (SELECT DISTINCT VendedorId as Receber FROM RepReceber WHERE ValorAReceber > 0) AS Receber ON RepVendedor.Id = Receber.Receber";


                if (pFiltro != "") query += " WHERE " + pFiltro;

                var result = representante.Database.SqlQuery<ListaRepVendedorHome>(query);


                return result.ToList<ListaRepVendedorHome>();
            }
        }





        public static RepVendedor PesquisarVendedor(string pCodigo = "", string pCPFCnpj = "")
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var vendedor = representante.RepVendedor.Where(vd => vd.Id.ToString() == pCodigo || vd.CpfCnpj.Trim() == pCPFCnpj).FirstOrDefault();

                return vendedor;

            }
        }


        public static void SalvarVendedor(string pModo, RepVendedor pVendedor, long pVendedorId = 0)
        {


            if (pModo == "Create")
            {
                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {

                    if (pVendedorId > 0)
                    {

                        pVendedor.Id = pVendedorId;

                    } else
                    {
                        var maxVendedor = representante.RepVendedor.OrderByDescending(i => i.Id).FirstOrDefault();

                        long newId = maxVendedor == null ? 999001 : maxVendedor.Id + 1;

                        pVendedor.Id = newId;
                    }
                        


                    Console.WriteLine("Incluindo Vendedor - " + pVendedorId.ToString() + " Nome> " + pVendedor.Nome + " ...");

                    representante.RepVendedor.Add(pVendedor);

                    representante.SaveChanges();

                    Console.WriteLine("Vendedor Incluído!");
                }

            } else
            {

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    Console.WriteLine("Alterando Vendedor - " + pVendedorId.ToString() + " Nome> " + pVendedor.Nome + " ...");
                    var result = representante.RepVendedor.SingleOrDefault(vd => vd.Id == pVendedorId);
                    if (result != null)
                    {

                        pVendedor.Id = pVendedorId;

                        result.TipoPessoa = pVendedor.TipoPessoa;
                        result.CpfCnpj = pVendedor.CpfCnpj;
                        result.DataInicial = pVendedor.DataInicial;
                        result.DataFinal = pVendedor.DataFinal;
                        result.Status = pVendedor.Status;
                        result.Nome = pVendedor.Nome;
                        result.RazaoSocial = pVendedor.RazaoSocial;
                        result.RGInscricao = pVendedor.RGInscricao;
                        result.DataNascimento = pVendedor.DataNascimento;
                        result.Cep = pVendedor.Cep;
                        result.Endereco = pVendedor.Endereco;
                        result.Numero = pVendedor.Numero;
                        result.Complemento = pVendedor.Complemento;
                        result.Bairro = pVendedor.Bairro;
                        result.UF = pVendedor.UF;
                        result.Cidade = pVendedor.Cidade;
                        result.Telefone = pVendedor.Telefone;
                        result.TelefoneComercial = pVendedor.TelefoneComercial;
                        result.Celular = pVendedor.Celular;
                        result.Email = pVendedor.Email;
                        result.LimitePedido = pVendedor.LimitePedido;
                        result.LimiteCredito = pVendedor.LimiteCredito;
                        result.Observacao = pVendedor.Observacao;

                        representante.SaveChanges();

                        Console.WriteLine("Vendedor Alterado!");
                    }
                }


            }

        }

        public static RepVendedorBase PesquisarVendedorBase(string pCPFCnpj = "")
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var vendedor = representante.RepVendedorBase.Where(vd => vd.CpfCnpj.Trim() == pCPFCnpj).FirstOrDefault();
                return vendedor;

            }
        }


        public static RepVendedorBase ObterVendedorBase(long pVendedorId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var vendedor = representante.RepVendedorBase.Where(vd => vd.Id == pVendedorId).FirstOrDefault();

                return vendedor;

            }
        }




        public static RepPedido ObterVendedorPedido(long pVendedorId, long pCargaId, string pCodigoPedido = "")
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                RepPedido pedido;

                if (pCodigoPedido != "")
                {
                    pedido = representante.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.CodigoPedido == pCodigoPedido);
                } else
                {
                    pedido = representante.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.VendedorId == pVendedorId && p.CargaId == pCargaId);
                }
                
                return pedido;
            }
            
        }


        public static List<RepPedido> ObterVendedorPedidos(long pVendedorId, long pCargaId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                List<RepPedido> pedidos = representante.RepPedido.OrderByDescending(i => i.Id).Where(p => p.VendedorId == pVendedorId && p.CargaId == pCargaId).ToList<RepPedido>();
                return pedidos;
            }

        }

        public static RepPedido ObterPedido(long pPedidoId, string pCodigoPedido = "")
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                RepPedido pedido; 

                if (pCodigoPedido != "")
                {
                    pedido = representante.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.CodigoPedido == pCodigoPedido);
                } else
                {
                    pedido = representante.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.Id == pPedidoId);
                }
                
                return pedido;
            }

        }

        public static int ContarPedidos(long pVendedorId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                int countpedido = representante.RepPedido.OrderByDescending(i => i.Id).Where(p => p.VendedorId == pVendedorId).Count();
                return countpedido;
            }

        }




        public static long InserirPedido(long pVendedorId, long pCargaId)
        {


            Console.WriteLine("Inserindo Pedido");

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var maxPedido = representante.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault();

                long newId = maxPedido == null ? 1 : maxPedido.Id + 1;

                var carga = representante.RepCarga.FirstOrDefault();
                


                var pedidoanterior = representante.RepPedido.FirstOrDefault(pd => pd.VendedorId == pVendedorId);


                if (pedidoanterior != null)
                {                    
                    pedidoanterior.Status = "4";                    
                }





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
                vCodigoPedido += carga.Ano.ToString().Substring(2,2);
                vCodigoPedido += pVendedorId.ToString().PadLeft(5, '0');
                vCodigoPedido += newId.ToString().PadLeft(7,'0');


                var novopedido = new RepPedido
                {
                    Id = newId,
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
                    ValorAReceber = pedidoanterior == null ? 0 : decimal.Round(Convert.ToDecimal(pedidoanterior.ValorLiquido - pedidoanterior.ValorAcerto), 2),
                    ValorAcerto = 0,
                    QuantidadeRemarcado = 0,
                    Remarcado = 0,
                    Status = "0"
                };

                representante.RepPedido.Add(novopedido);




                representante.SaveChanges();


                return newId;
            }
        }


        public static void AtualizarPedido(long pPedidoId, decimal pValor)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                var pedido = representante.RepPedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {

                    decimal tmpValor = Convert.ToDecimal(pedido.ValorPedido);


                    Console.WriteLine("ValorTotal:" + tmpValor.ToString());



                    pedido.ValorPedido = decimal.Round((tmpValor + pValor),2);
                    pedido.ValorCompra = decimal.Round((tmpValor + pValor),2);
                    if (pedido.ValorPedido == 0)
                    {
                        pedido.PercentualCompra = 0;
                    } else
                    {
                        pedido.PercentualCompra = decimal.Round(Convert.ToDecimal((pedido.ValorCompra / pedido.ValorPedido) * 100),2);
                    }
                    


                    if (pedido.PercentualCompra < 35) // primeira faixa
                    {
                        pedido.FaixaComissao = 35;
                        pedido.PercentualFaixa = 35;
                    } else if (pedido.PercentualCompra >= 35 && pedido.PercentualCompra < 80) // segunda faixa
                    {
                        pedido.FaixaComissao = 80;
                        pedido.PercentualFaixa = 40;
                    } else // terceira faixa
                    {
                        pedido.FaixaComissao = 100;
                        pedido.PercentualFaixa = 45;                    
                    }
                    
                    pedido.ValorComissao = decimal.Round(Convert.ToDecimal(pedido.ValorCompra * (pedido.PercentualFaixa / 100)), 2);
                    pedido.ValorLiquido = decimal.Round(Convert.ToDecimal(pedido.ValorCompra - pedido.ValorComissao),2);

                    representante.SaveChanges();
                }



            }

        }

        public static void RetornarPedido(long pPedidoId)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                var pedido = representante.RepPedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {

                    var tmpDataRetorno = pedido.DataRetorno;

                    if (tmpDataRetorno == null)
                    {
                        pedido.DataRetorno = DateTime.Now.Date;
                        pedido.Status = "2";
                    }



                    

                   

                    var pedidoitem = representante.RepPedidoItem.GroupBy(pi => pi.PedidoId)
                   .Select(g => new { pedidoid = g.Key, valortotal = g.Sum(i => ((i.Quantidade - i.Retorno) * i.Preco)) })
                   .FirstOrDefault(pi => pi.pedidoid == pedido.Id);



                    decimal tmpValor = Convert.ToDecimal(pedidoitem.valortotal); // + (pQuantidade - pRetorno) * pPreco;
                    
                    Console.WriteLine("ValorTotal:" + tmpValor.ToString());

                    pedido.ValorCompra = decimal.Round(tmpValor,2);
                    pedido.PercentualCompra = (pedido.ValorCompra / pedido.ValorPedido)*100;


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

                    pedido.ValorComissao = decimal.Round(Convert.ToDecimal(pedido.ValorCompra * (pedido.PercentualFaixa / 100)), 2);
                    pedido.ValorLiquido = decimal.Round(Convert.ToDecimal(pedido.ValorCompra - pedido.ValorComissao), 2);



                    representante.SaveChanges();

                }



            }

        }




        public static List<ListaRepVendedorPedido> ObterVendedorPedidoItem(long pPedidoId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var result = representante.RepPedido
                           .Join(representante.RepPedidoItem, pd => pd.Id, pi => pi.PedidoId, (pd, pi) => new { RepPedido = pd, RepPedidoItem = pi })
                           .Join(representante.RepProdutoGrade, pi => pi.RepPedidoItem.ProdutoGradeId, pg => pg.Id, (pi, pg) => new { RepPedidoItem = pi, RepProdutoGrade = pg })
                           .Join(representante.RepProduto, pg => pg.RepProdutoGrade.ProdutoId, pr => pr.Id, (pg, pr) => new { RepProdutoGrade = pg, RepProduto = pr })
                           .Where(pd => pd.RepProdutoGrade.RepPedidoItem.RepPedido.Id == pPedidoId)
                           .Select(ls => new ListaRepVendedorPedido()
                           {
                               Id = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Id,
                               PedidoId = ls.RepProdutoGrade.RepPedidoItem.RepPedido.Id,
                               ProdutoGradeId = ls.RepProdutoGrade.RepProdutoGrade.Id,
                               CodigoBarras = ls.RepProdutoGrade.RepProdutoGrade.CodigoBarras + ls.RepProdutoGrade.RepProdutoGrade.Digito,
                               Descricao = ls.RepProduto.Descricao,
                               Cor = ls.RepProdutoGrade.RepProdutoGrade.Cor,
                               Tamanho = ls.RepProdutoGrade.RepProdutoGrade.Tamanho,
                               Quantidade = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Quantidade,
                               Retorno = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Retorno,
                               Preco = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Preco
                           })
                           .OrderBy(pd => pd.CodigoBarras);


                //.Where(pd => pd.RepProdutoGrade.RepPedidoItem.RepPedido.VendedorId == pVendedorId && pd.RepProdutoGrade.RepPedidoItem.RepPedido.CargaId == pCargaId)

                return result.ToList<ListaRepVendedorPedido>();

            }
        }


        public static void InserirPedidoItem(long pCargaId, long pVendedorId, long pProdutoGradeId, decimal pQuantidade, decimal pPreco)
        {


            Console.WriteLine("InserirPedidoItem");


            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var pedido = representante.RepPedido.OrderByDescending(pd => pd.Id).FirstOrDefault(pd => pd.VendedorId == pVendedorId && pd.CargaId == pCargaId);
                long vPedidoId = 0;

                if (pedido == null)
                {

                    vPedidoId = InserirPedido(pVendedorId, pCargaId);


                } else
                {

                    vPedidoId = pedido.Id;
                }


                var result = representante.RepPedidoItem.SingleOrDefault(pi => pi.PedidoId == vPedidoId && pi.ProdutoGradeId == pProdutoGradeId);

                if (result != null)
                {
                    Console.WriteLine("Atualizando Pedido Item");
                    var tmpQtd = result.Quantidade;
                    

                    Console.WriteLine("Quantidade:" + tmpQtd.ToString());
                    result.Quantidade = (tmpQtd + pQuantidade);
                    result.Preco = pPreco;

                }
                else
                {


                    Console.WriteLine("Inserindo Pedido Item");

                    var maxPedidoItem = representante.RepPedidoItem.OrderByDescending(i => i.Id).FirstOrDefault();

                    long newId = maxPedidoItem == null ? 1 : maxPedidoItem.Id + 1;


                    var novopedidoitem = new RepPedidoItem
                    {
                        Id = newId,
                        PedidoId = vPedidoId,
                        ProdutoGradeId = pProdutoGradeId,
                        Quantidade = pQuantidade,
                        Retorno = 0,
                        Preco = pPreco
                    };

                    representante.RepPedidoItem.Add(novopedidoitem);

                }

                AtualizarPedido(vPedidoId, pQuantidade * pPreco);
                representante.SaveChanges();



            }


        }


        public static void AtualizarPedidoItem(long pPedidoId, long pProdutoGradeId, decimal pQuantidade, decimal pPreco)
        {

            Console.WriteLine("AtualizarPedidoItem: " + pProdutoGradeId.ToString());


            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var pedidoitem = representante.RepPedidoItem.FirstOrDefault(pi => pi.ProdutoGradeId == pProdutoGradeId && pi.PedidoId == pPedidoId);

                if (pedidoitem != null)
                {
                    var vValor = pedidoitem.Quantidade * pedidoitem.Preco;
                    AtualizarPedido(pedidoitem.PedidoId, -(Convert.ToDecimal(vValor)));
                    pedidoitem.Quantidade = pQuantidade;
                    pedidoitem.Preco = pPreco;

                    representante.SaveChanges();

                    AtualizarPedido(pedidoitem.PedidoId, pQuantidade * pPreco);
                }
            }
        }




        public static void ExcluirPedidoItem(long pPedidoItemId, long pPedidoId, decimal pQuantidade, decimal pPreco)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                representante.Database.ExecuteSqlCommand("DELETE FROM RepPedidoItem WHERE Id = @pPedidoItemId", new SQLiteParameter("@pPedidoItemId", pPedidoItemId));

                AtualizarPedido(pPedidoId, -(pQuantidade*pPreco));

            }

        }


        public static void RetornarPedidoItem(long pPedidoId, long pProdutoGradeId, decimal pQuantidade)
        {

            Console.WriteLine("AtualizarPedidoItem: " + pProdutoGradeId.ToString());


            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var pedidoitem = representante.RepPedidoItem.FirstOrDefault(pi => pi.ProdutoGradeId == pProdutoGradeId && pi.PedidoId == pPedidoId);

                if (pedidoitem != null)
                {

                    //= pQuantidade;


                    //decimal vValor = Convert.ToDecimal((pedidoitem.Quantidade - pedidoitem.Retorno) * pedidoitem.Preco);
                    pedidoitem.Retorno = pQuantidade;
                    representante.SaveChanges();


                    RetornarPedido(pedidoitem.PedidoId);

                }
            }
        }


        public static RepReceber ObterTitulo(long pReceberId)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var receber = representante.RepReceber.FirstOrDefault(rc => rc.Id == pReceberId);

                return receber;

            }

        }

        public static List<ListaTitulos> ObterListaTitulos(long pVendedorId)
        {


            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT Id, Documento, Serie, ValorNF 
                                as ValorDuplicata, ValorAReceber, DataEmissao, DataVencimento, 
                                DataPagamento, Observacoes 
                                FROM RepReceber 
                                WHERE RepReceber.vendedorId = @p0";  

                var result = representante.Database.SqlQuery<ListaTitulos>(query, pVendedorId);

                return result.ToList<ListaTitulos>();


            }

        }


        public static ListaTitulos ObterTotalTitulos(long pVendedorId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT MAX(RepReceberBaixa.Id) Id, MAX(RepReceber.Id) ReceberId, Max(Documento) Documento, Max(Serie) Serie, Sum(ValorNF) 
                                as ValorDuplicata, Sum(Valor) as ValorRecebido, Max(DataEmissao) DataEmissao, Max(DataVencimento) DataVencimento, 
                                Max(RepReceberBaixa.DataPagamento) DataPagamento, Max(Observacoes) Observacoes
                                FROM RepReceber LEFT JOIN RepReceberBaixa ON RepReceber.Id = RepReceberBaixa.ReceberId 
                                WHERE vendedorId = @p0";

                ListaTitulos result = representante.Database.SqlQuery<ListaTitulos>(query, pVendedorId).FirstOrDefault();

                return result;

            }
        }

        public static RepRecebimento ObterRecebimento(long pRecebimentoId)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var recebimento = representante.RepRecebimento.FirstOrDefault(rc => rc.Id == pRecebimentoId);
                return recebimento;

            }

        }


        public static List<ListaRecebimento> ObterListaRecebimento(long pVendedorId)
        {


            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT 
                                    CASE 
                                        WHEN PedidoId != 0 THEN 'Pedido:' || CodigoPedido
                                        WHEN ReceberId != 0 THEN 'Titulo:' || RepReceber.Documento || '/' || RepReceber.Serie
                                        ELSE '' END as Referencia, 
                                    ValorRecebido, RepRecebimento.DataPagamento, FormaPagamento, Observacao, RepRecebimento.Id, 
                                    RepRecebimento.CargaId, RepRecebimento.VendedorId, ReceberId, PedidoId, CodigoPedido 
                                        FROM RepRecebimento
                                            LEFT JOIN RepReceber ON RepReceber.VendedorId = RepRecebimento.VendedorId AND RepReceber.Id = ReceberId
                                    WHERE RepRecebimento.VendedorId = @p0";

                var result = representante.Database.SqlQuery<ListaRecebimento>(query, pVendedorId);

                return result.ToList<ListaRecebimento>();


            }

        }



        public static void InserirRecebimento(string pTipo, long pCargaId, long pVendedorId, decimal pValorRecebido, string pFormaPagamento, string pObservacao, long pReceberId = 0, long pPedidoId = 0, string pCodigoPedido = "")
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                var maxRecebimento = representante.RepRecebimento.OrderByDescending(i => i.Id).FirstOrDefault();

                long newId = maxRecebimento == null ? 1 : maxRecebimento.Id + 1;

                var novorecebimento = new RepRecebimento
                {
                    Id = newId,
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

                representante.RepRecebimento.Add(novorecebimento);

                //atualizar tabela Pedido / Receber

                if (pPedidoId != 0)
                    ReceberPedido(pPedidoId, pValorRecebido);

                if (pReceberId != 0)
                    ReceberTitulo(pReceberId, pCargaId, pValorRecebido);


                representante.SaveChanges();

            }

        }

        public static void AlterarRecebimento(long pRecebimentoId, decimal pValorRecebido, string pFormaPagamento, string pObservacao)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                var recebimento = representante.RepRecebimento.FirstOrDefault(rc => rc.Id == pRecebimentoId);


                if (recebimento != null)
                {

                    decimal tmpValor = decimal.Round(Convert.ToDecimal(recebimento.ValorRecebido),2);

                    recebimento.ValorRecebido = decimal.Round(pValorRecebido,2);
                    recebimento.FormaPagamento = pFormaPagamento;
                    recebimento.Observacao = pObservacao;

                    representante.SaveChanges();


                    //atualizar tabela Pedido / Receber -- tratar alteração de valor
                    if (recebimento.PedidoId != null && recebimento.PedidoId != 0)
                        ReceberPedido(Convert.ToInt64(recebimento.PedidoId), pValorRecebido, tmpValor);


                    if (recebimento.ReceberId != null && recebimento.ReceberId != 0)
                        ReceberTitulo(Convert.ToInt32(recebimento.ReceberId), Convert.ToInt32(recebimento.CargaId), decimal.Round(pValorRecebido,2), decimal.Round(tmpValor,2));




                }

                representante.SaveChanges();


                



            }



        }


        public static void ExcluirRecebimento(long pRecebimentoId)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var recebimento = representante.RepRecebimento.FirstOrDefault(rc => rc.Id == pRecebimentoId);

                if (recebimento != null)
                {
                   
                    if (recebimento.PedidoId != null && recebimento.PedidoId != 0)
                        ReceberPedido(Convert.ToInt32(recebimento.PedidoId), 0, Convert.ToDecimal(recebimento.ValorRecebido));

                    if (recebimento.ReceberId != null && recebimento.ReceberId != 0)
                        ReceberTitulo(Convert.ToInt32(recebimento.ReceberId), Convert.ToInt32(recebimento.CargaId), 0, Convert.ToDecimal(recebimento.ValorRecebido));

                    representante.Database.ExecuteSqlCommand("DELETE FROM RepRecebimento WHERE Id = @pRecebimentoId", new SQLiteParameter("@pRecebimentoId", pRecebimentoId));

                    representante.SaveChanges();
                }

            }

        }



        public static void ReceberPedido(long pPedidoId, decimal pValor, decimal? pValorAnterior = null)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                var pedido = representante.RepPedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {


                    if (pedido.DataRetorno == null) {

                        pedido.DataRetorno = DateTime.Now.Date;
                    }

                    if (pValorAnterior == null)
                    {
                        if (pedido.ValorAcerto == null) pedido.ValorAcerto = 0;
                        pedido.ValorAcerto += pValor;

                    } else 
                    {
                        pedido.ValorAcerto = pedido.ValorAcerto - pValorAnterior + pValor;
                    }

                    pedido.Status = "3";

                    representante.SaveChanges();

                }



            }

        }


        public static void ReceberTitulo(long pReceberId, long pCargaId, decimal pValor, decimal? pValorAnterior = null)
        {

            Console.WriteLine("Inserindo Receber Baixa");

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {                

                var receber = representante.RepReceber.SingleOrDefault(rc => rc.Id == pReceberId);

                if (receber != null)
                {


                    if (pValorAnterior == null)
                    {
                        receber.ValorAReceber -= pValor;

                    }
                    else
                    {
                        receber.ValorAReceber = receber.ValorAReceber + pValorAnterior - pValor;
                    }

                    if (receber.ValorAReceber <= 0)
                    {
                        receber.DataPagamento = DateTime.Now;
                        receber.Status = "1";
                    } else
                    {
                        receber.DataPagamento = null;
                        receber.Status = "0";
                    }
                        
                    representante.SaveChanges();

                }


            }

        }


        public static void ReceberExtra(long pVendedorId, long pCargaId, decimal pValor, Nullable<decimal> pValorAReceber)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                Console.WriteLine("Criando titulo a receber.");

                var maxReceber = representante.RepReceber.OrderByDescending(i => i.Id).FirstOrDefault();

                long newReceberId = maxReceber == null ? 1 : maxReceber.Id + 1;


                var novoreceber = new RepReceber
                {
                    Id = newReceberId,
                    VendedorId = pVendedorId,
                    CargaId = pCargaId,
                    Documento = pVendedorId,
                    Serie = DateTime.Now.Month.ToString() + (DateTime.Now.Year.ToString()).Substring(2, 2),
                    ValorNF = pValor,
                    ValorDuplicata = pValorAReceber,
                    ValorAReceber = pValorAReceber- pValor,
                    DataEmissao = DateTime.Now,
                    DataVencimento = DateTime.Now,
                    DataPagamento = DateTime.Now,
                    Observacoes = "Título Extra gerado pelo representante",
                    Status = "1"                  
                };

                representante.RepReceber.Add(novoreceber);

                representante.SaveChanges();


                Console.WriteLine("Criando baixa de titulo a receber.");

                var repRecebimento = representante.RepRecebimento.OrderByDescending(i => i.Id).FirstOrDefault();

                long newId = repRecebimento == null ? 1 : repRecebimento.Id + 1;


                var novorecebimento = new RepRecebimento
                {
                    Id = newId,
                    Tipo = "Extra",
                    CargaId = pCargaId,
                    VendedorId = pVendedorId,
                    ReceberId = newReceberId,
                    PedidoId = 0,
                    CodigoPedido = "",
                    ValorRecebido = pValorAReceber,
                    DataPagamento = DateTime.Now,
                    FormaPagamento = "Outros",
                    Observacao = "Recebimento extra registrado pelo representante"
                };

                representante.RepRecebimento.Add(novorecebimento);

                representante.SaveChanges();


                Console.WriteLine("Atualizando Vendedor.");

                var vendedorbase = representante.RepVendedorBase.SingleOrDefault(vd => vd.Id == pVendedorId);

                if (vendedorbase != null)
                {
                    vendedorbase.DebitoAReceber = vendedorbase.DebitoAReceber - pValor;
                }


                representante.SaveChanges();





            }



        }

        public static List<ListaRepPosicaoFinanceira> ObterPosicaoFinanceira()
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                string query = @"SELECT RepVendedor.Id, RepVendedor.Nome, SUM(IFNULL(ValorAReceber,0)) Receber, SUM(IFNULL(ValorRecebido,0)) Recebido, SUM(IFNULL(ValorAReceber,0))-SUM(IFNULL(ValorRecebido,0)) Aberto , SUM(IFNULL(Quantidade,0)) Quantidade, SUM(IFNULL(Retorno,0)) Retorno, SUM(IFNULL(Remarcado,0)) Remarcado, 
                                    CASE 
                                      WHEN Receber.VendedorId IS NULL THEN FALSE
                                      WHEN ValorRecebido IS NULL THEN FALSE
                                      ELSE TRUE END as Visitado, SUM(IFNULL(PedidoNovo,0)) PedidoNovo
                                                                    FROM RepVendedor 
                                                                    LEFT JOIN
                                                                    (
									                                    SELECT VendedorId, SUM(ValorLiquido)as ValorAReceber, SUM(ValorAcerto) as ValorRecebido, SUM(Quantidade) Quantidade, SUM(Retorno) Retorno, SUM(Remarcado) Remarcado, CASE WHEN CargaId == CargaOriginal THEN 1 ELSE 0 END as PedidoNovo
										                                    FROM RepPedido 
										                                    INNER JOIN (SELECT PedidoId, sum(Quantidade) Quantidade, sum(Retorno) Retorno FROM RepPedidoItem GROUP BY PedidoId) as RepPedidoItem ON RepPedidoItem.PedidoId = RepPedido.Id
									                                      GROUP BY VendedorId								
									                                    UNION
										                                    SELECT RepReceber.VendedorId, sum(ValorDuplicata) ValorAReceber, sum(ValorDuplicata-IFNULL(ValorAReceber, 0)) as ValorRecebido, 0 Quantidade, 0 Retorno, CASE WHEN QuantidadeRemarcado > 0 THEN 1 ELSE 0 END Remarcado, 0 PedidoNovo
										                                    FROM RepReceber											                                    
											                                    GROUP BY RepReceber.VendedorId
                                                                    ) AS Receber ON RepVendedor.Id = Receber.VendedorId
                                    GROUP BY RepVendedor.Id, RepVendedor.Nome";


                var result = representante.Database.SqlQuery<ListaRepPosicaoFinanceira>(query);


                return result.ToList<ListaRepPosicaoFinanceira>();


                /*
                var result = representante.RepPosicaoFinanceira
                           .Join(representante.RepVendedor, pf => pf.VendedorId, vd => vd.Id, (pf, vd) => new { RepPosicaoFinanceira = pf, RepVendedor = vd })
                           .Select(ls => new ListaRepPosicaoFinanceira()
                           {
                               Id = ls.RepVendedor.Id,
                               Nome = ls.RepVendedor.Nome,
                               Receber = ls.RepPosicaoFinanceira.ValorAReceber,
                               Recebido = ls.RepPosicaoFinanceira.ValorRecebido,
                               Aberto = ls.RepPosicaoFinanceira.ValorAberto
                           });
               */


            }


        }

        public static ListaTotalizadores ObterTotalizadores(int pCargaId)
        {


            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {



                ListaTotalizadores vTotalizador = new ListaTotalizadores();



                string query = @"SELECT RepCarga.Id, sum(RepCargaProduto.Quantidade) QtdProdutos, sum(RepCargaProduto.Quantidade) * sum(ValorSaida) TotalProdutos 
                                    FROM RepCarga 
                                    INNER JOIN RepCargaProduto On RepCargaProduto.CargaId = RepCarga.Id 
                                    INNER JOIN RepProdutoGrade ON ProdutoGradeId = RepProdutoGrade.Id
                                WHERE RepCarga.Id = @p0 AND RepCargaProduto.Tipo != 'P'
                                GROUP BY RepCarga.Id";




                var totalizador = representante.Database.SqlQuery<ListaTotalizadores>(query, pCargaId).FirstOrDefault();


                vTotalizador.QtdProdutos = (totalizador == null)?0:totalizador.QtdProdutos;
                vTotalizador.TotalProdutos = (totalizador == null) ? 0 : totalizador.TotalProdutos;

                return vTotalizador;

            }
        }


        public static List<ListaRepCargaProduto> ObterListaSuplemento()
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                var cargaproduto = representante.RepCargaProduto
                                    .Join(representante.RepProdutoGrade, cp => cp.ProdutoGradeId, pg => pg.Id, (cp, pg) => new { RepCargaProduto = cp, RepProdutoGrade = pg })
                                    .Join(representante.RepProduto, pg => pg.RepProdutoGrade.ProdutoId, pr => pr.Id, (pg, pr) => new { RepProdutoGrade = pg, RepProduto = pr })
                                    .Where(cp => cp.RepProdutoGrade.RepCargaProduto.Tipo == "S")
                                    .Select(pg => new ListaRepCargaProduto()
                                    {
                                        CodigoBarras = pg.RepProdutoGrade.RepProdutoGrade.CodigoBarras.ToString() + pg.RepProdutoGrade.RepProdutoGrade.Digito.ToString(),
                                        Descricao = pg.RepProduto.Descricao,
                                        Quantidade = pg.RepProdutoGrade.RepCargaProduto.Quantidade,
                                        Tipo = pg.RepProdutoGrade.RepCargaProduto.Tipo,
                                        CargaId = pg.RepProdutoGrade.RepCargaProduto.CargaId.Value,
                                        ProdutoGradeId = pg.RepProdutoGrade.RepCargaProduto.ProdutoGradeId                                   
                                    }).ToList<ListaRepCargaProduto>();

                return cargaproduto;
            }
        }

        public static void InserirSuplemento(long pCargaId, long pProdutoGradeId, decimal pQuantidade)
        {


            Console.WriteLine("InserirSuplemento");

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var cargaproduto = representante.RepCargaProduto.FirstOrDefault(pd => pd.ProdutoGradeId == pProdutoGradeId && pd.Tipo == "S");


                if (cargaproduto != null)
                {
                    Console.WriteLine("Atualizando Carga Produto (Suplemento)");
                    var tmpQtd = cargaproduto.Quantidade;
                    cargaproduto.Quantidade = (tmpQtd + pQuantidade);
                }
                else
                {
                    Console.WriteLine("Inserindo Carga Produto (Suplemento)");

                    var maxCargaProduto = representante.RepCargaProduto.OrderByDescending(i => i.Id).FirstOrDefault();

                    long newId = maxCargaProduto == null ? 1 : maxCargaProduto.Id + 1;


                    var novacargaproduto = new RepCargaProduto
                    {
                        Id = newId,
                        CargaId = pCargaId,
                        ProdutoGradeId = pProdutoGradeId,
                        Quantidade = pQuantidade,
                        Retorno = 0,
                        Tipo = "S"
                    };

                    representante.RepCargaProduto.Add(novacargaproduto);

                }


                representante.SaveChanges();





            }





        }

        public static void AlterarSuplemento(long pCargaId, long pProdutoGradeId, decimal pQuantidade)
        {


            Console.WriteLine("AlterarSuplemento");

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                var cargaproduto = representante.RepCargaProduto.FirstOrDefault(pd => pd.ProdutoGradeId == pProdutoGradeId && pd.Tipo == "S");


                if (cargaproduto != null)
                {
                    Console.WriteLine("Alterando Carga Produto (Suplemento)");
                    cargaproduto.Quantidade = pQuantidade;                    
                    representante.SaveChanges();
                }              


            }

        }

        public static void ExcluirSuplemento(long pCargaId, long pCargaProdutoGradeId)
        {
            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                representante.Database.ExecuteSqlCommand("DELETE FROM RepCargaProduto WHERE Tipo = 'S' AND CargaId = @pCargaId AND ProdutoGradeId = @pProdutoGradeId", new SQLiteParameter("@pCargaId", pCargaId), new SQLiteParameter("@pProdutoGradeId", pCargaProdutoGradeId));

            }
        }

        public static List<ListaRepEstoque> ObterListaEstoque()
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                string query = @"SELECT RepProdutoGrade.CodigoBarras || '' || RepProdutoGrade.Digito as CodigoBarras, RepProduto.Descricao || ' ' || RepProdutoGrade.Tamanho Descricao,
                                        SUM(IFNULL(Retornado.Vendido,0)) Vendido,
	                                    SUM(IFNULL(RepCargaProduto.Quantidade,0)) Carga,	
	                                    SUM(IFNULL(Retornado.Retorno,0)) Retorno,
										SUM(IFNULL(Consignado.Consignado,0)) Consignado,
										SUM(IFNULL(RepCargaProduto.Quantidade,0))+SUM(IFNULL(Retornado.Retorno,0)) AS SaldoCarro
								FROM RepProduto
	                               INNER JOIN RepProdutoGrade ON RepProduto.Id = RepProdutoGrade.ProdutoId
	                               LEFT JOIN RepCargaProduto on RepCargaProduto.ProdutoGradeId = RepProdutoGrade.Id
	                               LEFT JOIN RepCarga ON RepCargaProduto.CargaId = RepCarga.Id
								   LEFT JOIN (
		                                    SELECT ProdutoGradeId, CargaId, SUM(IFNULL(RepPedidoItem.Quantidade,0))-SUM(IFNULL(RepPedidoItem.Retorno,0)) Vendido, SUM(IFNULL(RepPedidoItem.Retorno,0)) Retorno FROM RepPedido
			                                    LEFT JOIN RepPedidoItem ON RepPedidoItem.PedidoId = RepPedido.Id
			                                WHERE (Status > 2 OR Retorno > 0)
			                                GROUP BY ProdutoGradeId, CargaId
                                            ) as Retornado ON Retornado.ProdutoGradeId = RepProdutoGrade.Id AND Retornado.CargaId = RepCarga.Id
								   LEFT JOIN (
										SELECT ProdutoGradeId, CargaId, SUM(IFNULL(RepPedidoItem.Quantidade,0))-SUM(IFNULL(RepPedidoItem.Retorno,0))  Consignado FROM RepPedido
											LEFT JOIN RepPedidoItem ON RepPedidoItem.PedidoId = RepPedido.Id
										WHERE Status <= 2
										GROUP BY ProdutoGradeId, CargaId
										) as Consignado ON Consignado.ProdutoGradeId = RepProdutoGrade.Id AND Consignado.CargaId = RepCarga.Id
									WHERE RepCargaProduto.Id IS NOT NULL
                                 GROUP BY RepProdutoGrade.CodigoBarras, RepProdutoGrade.Digito, RepProduto.Descricao, RepProdutoGrade.Tamanho, RepCarga.Id, RepProdutoGrade.ValorSaida";


                var result = representante.Database.SqlQuery<ListaRepEstoque>(query);


                return result.ToList<ListaRepEstoque>();

            }


        }

        public static void Manutencao()
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {
                // Limpar Itens com quantidade = 0
                representante.Database.ExecuteSqlCommand("DELETE FROM RepPedidoItem where Quantidade = 0 AND Retorno = 0");

                // Limpar Pedidos sem Itens                
                representante.Database.ExecuteSqlCommand("DELETE FROM RepPedido WHERE Id NOT IN (SELECT PedidoId FROM RepPedidoItem)");
                // Reajustar Status do Pedido o pedido novo não tiver itens
                representante.Database.ExecuteSqlCommand(@"UPDATE RepPedido SET Status = '2' 
                                                            WHERE Id NOT IN (
                                                                SELECT Rp1.Id 
                                                                    FROM RepPedido Rp1 
                                                                    INNER JOIN RepPedido Rp2 
                                                                        ON Rp1.VendedorId = Rp2.VendedorId AND Rp1.Status = '4' AND Rp2.Status != '4'
                                                                ) 
                                                            AND Status = '4'");
                // Reajustar Status do Pedido Quando não houver recebimento
                representante.Database.ExecuteSqlCommand("UPDATE RepPedido SET Status = '2' WHERE Status = '3' AND Id NOT IN (SELECT PedidoId FROM RepRecebimento)");
                // Reajustar DataPagamento do Receber Quando não houver recebimento
                representante.Database.ExecuteSqlCommand("UPDATE RepReceber SET DataPagamento = null, Status = '0' WHERE DataPagamento IS NOT NULL AND Id NOT IN (SELECT ReceberId FROM RepRecebimento)");


               
            }


        }

        

    }
}
