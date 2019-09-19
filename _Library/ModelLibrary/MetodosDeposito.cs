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
                           break;
                    }


                    deposito.SaveChanges();
                }


            }

        }


        public static void RefazerRetorno(int pCargaId)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {



                string query = "UPDATE Carga SET Retorno = 0 WHERE Id = @p0";

                deposito.Database.ExecuteSqlCommand(query, pCargaId);

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

        public static Produto ObterProduto(string pCodigo)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var produto = (from p in deposito.Produto
                                    where (p.CodigoBarras == pCodigo || p.Id.ToString() == pCodigo)
                                    select p).FirstOrDefault<Produto>();

                return produto;
            }

        }

        public static ProdutoGrade ObterProdutoGrade(string pCodigo)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                string vCodigoSemDigito = pCodigo.Substring(0, pCodigo.Length - 1);
                string vDigito = pCodigo.Substring(pCodigo.Length - 1);

                Console.WriteLine(vCodigoSemDigito + ':' + vDigito);

                var produtograde = (from pg in deposito.ProdutoGrade
                                    where (pg.CodigoBarras == vCodigoSemDigito && pg.Digito == vDigito) || pg.Id.ToString() == pCodigo
                                    select pg).FirstOrDefault<ProdutoGrade>();

                return produtograde;
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

            /// Modificar rotina para:
            /// Se o CargaProduto existir na lista - incrementar quantidade
            /// 
            Console.WriteLine("InserirCargaProduto");

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var result = deposito.CargaProduto.SingleOrDefault(cp => cp.ProdutoGradeId == pProdutoGradeId && cp.CargaId == pCargaId);

                if (result != null)
                {
                    Console.WriteLine("Inserindo Carga Produto");
                    var tmpQtd = result.Quantidade;

                    Console.WriteLine("Valor:" + tmpQtd.ToString());
                    result.Quantidade = tmpQtd + pQuantidade;
                    
                } else {


                    Console.WriteLine("Atualizando Carga Produto");
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
            }




        }


        public static void ExcluirCargaProduto(int pCargaId, int pCargaProdutoGradeId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                deposito.Database.ExecuteSqlCommand("DELETE FROM CargaProduto WHERE CargaId = @pCargaId AND ProdutoGradeId = @pProdutoGradeId", new SqlParameter("@pCargaId", pCargaId), new SqlParameter("@pProdutoGradeId", pCargaProdutoGradeId));

                //CargaProduto cargaproduto = new CargaProduto() { ProdutoGradeId = pCargaProdutoGradeId, CargaId = pCargaId};
                //deposito.CargaProduto.Attach(cargaproduto);
                //deposito.CargaProduto.Remove(cargaproduto);
                //deposito.SaveChanges();
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


        public static Pedido ObterPedido(string pCodigoPedido)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var pedido = deposito.Pedido.SingleOrDefault(pi => pi.CodigoPedido == pCodigoPedido);

                return pedido;

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

                string query = @"SELECT PedidoItem.Id as PedidoItemId, Produto.CodigoBarras + '' + ProdutoGrade.Digito as CodigoBarras, Produto.Descricao NomeProduto, PedidoItem.Quantidade, PedidoItem.Retorno, PedidoItem.Preco
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

        public static List<ListaAReceber> ObterListaAReceber(long pCargaId)
        {


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var carga = deposito.Carga.FirstOrDefault(c => c.Id == pCargaId);

                int vPracaId = 0;

                vPracaId = carga != null ? Convert.ToInt32(carga.PracaId) : 0;

                string query = @"SELECT Receber.Id, ReceberBaixa.Id as ReceberBaixaId, Documento, Serie, Nome, ValorAReceber, ReceberBaixa.Valor as ValorPago, ReceberBaixa.DataPagamento 
	                                FROM Receber 
		                                INNER JOIN Vendedor ON Receber.VendedorId = Vendedor.Id
		                                LEFT JOIN ReceberBaixa ON Receber.Id = ReceberBaixa.ReceberId
		                                WHERE (Receber.CargaId = @p0 
		                                or VendedorId 
                                            IN(
	                                            SELECT Distinct VendedorId
                                                FROM Pedido
                                                WHERE CargaId in(SELECT Id FROM Carga WHERE Id <=@p0 and PracaId = @p1)
                                            )) 
			                                AND Receber.DataPagamento IS NULL 
			                                AND ValorNF > 0 
			                                AND Receber.CargaId <= @p0
                                ORDER BY Nome";

                Console.WriteLine("Obtendo Lista a Receber - CargaId: " + pCargaId.ToString());

                var result = deposito.Database.SqlQuery<ListaAReceber>(query, pCargaId, vPracaId);

                return result.ToList<ListaAReceber>();

            }

        }

        

        public static void SalvarAReceberBaixa(int pReceberId, int pReceberBaixaId, double pValor, string pData)
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
                        CargaId = null,
                        Valor = pValor,
                        DataPagamento = Convert.ToDateTime(pData),
                        DataBaixa = DateTime.Now

                    };

                    deposito.ReceberBaixa.Add(receberbaixa);
                    deposito.SaveChanges();



                } else
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


        public static void IncluirReceber(int pCargaId, int pVendedorId, double pValor, DateTime pDataVencimento)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var maxReceber = deposito.Receber.OrderByDescending(i => i.Id).FirstOrDefault();

                int newId = maxReceber == null ? 1 : maxReceber.Id + 1;

                Console.WriteLine("Inserindo A Receber - ReceberId: ");

                //insert
                var receber = new Receber
                {
                    Id = newId,
                    VendedorId = pVendedorId,
                    CargaId = pCargaId,
                    Documento = pVendedorId,
                    Serie = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString(),
                    ValorNF = pValor,
                    ValorDuplicata = pValor,
                    ValorAReceber = pValor,
                    DataEmissao = DateTime.Now.Date,
                    DataLancamento = DateTime.Now.Date,
                    DataVencimento = pDataVencimento,
                    DataPagamento = null,
                    QuantidadeRemarcado = 0,
                    Observacoes = "Título gerado automaticamente!",
                    Status = "0"
                };

                deposito.Receber.Add(receber);
                deposito.SaveChanges();


            }
        }

        public static List<Vendedor> ObterListaVendedor(long pCargaId = 0)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                List<Vendedor> result;

                if (pCargaId == 0) {

                    result = deposito.Vendedor.ToList<Vendedor>();

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
                                    )";


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

        public static Vendedor PesquisarVendedor(string pCPFCnpj = "")
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vendedor = deposito.Vendedor.Where(vd => vd.CpfCnpj.Trim() == pCPFCnpj).FirstOrDefault();

                return vendedor;

            }
        }


        public static Pedido ObterVendedorPedido(long pVendedorId, long pCargaId)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var pedido = deposito.Pedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.VendedorId == pVendedorId && p.CargaId == pCargaId);
                return pedido;
            }

        }

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





        public static decimal[] ObterTotalizadores(int pCargaId)
        {


            var ret = new decimal[4];


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {



                var result = (from tt in deposito.Totalizadores
                              where tt.Id == pCargaId
                              select tt).FirstOrDefault<Totalizadores>();

                if (result != null)
                {
                    ret[0] = Convert.ToDecimal(result.QtdProdutos);
                    ret[1] = Convert.ToDecimal(result.TotalProdutos);
                } else
                {
                    ret[0] = 0;
                    ret[1] = 0;
                }


                
                return ret;


            }

        }


    }
}
