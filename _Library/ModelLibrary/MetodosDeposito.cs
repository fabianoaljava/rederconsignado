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

            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                var usuario = context.Usuario.FirstOrDefault(u => u.Login.ToLower() == pLogin.ToLower());

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
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                try
                {
                    context.Database.Connection.Open();
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
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                return context.Praca.ToList<Praca>();
            }
        }

        public static Praca ObterPraca(int i)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var praca = context.Praca.FirstOrDefault(p => p.Id == i);
                return praca;
            }
        }


        public static List<Representante> ObterListaRepresentantes()
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                return context.Representante.ToList<Representante>();
            }
        }


        public static Representante ObterRepresentante(int i)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var representante = context.Representante.FirstOrDefault(p => p.Id == i);
                return representante;
            }
        }


        public static List<Carga> ObterListaCargas()
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                return context.Carga.ToList<Carga>();
            }
        }

        public static Carga ObterCarga(long pRepresentanteId, long pPracaId, int pMes, int pAno)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                var carga = context.Carga.FirstOrDefault(r => r.RepresentanteId == pRepresentanteId && r.PracaId == pPracaId && r.Mes == pMes && r.Ano == pAno);

                return carga;

            }
        }


        public static Carga ObterCargaAnterior(long pRepresentanteId, long pPracaId, DateTime pDataAbertura)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                var viagemanterior = (from c in context.Carga
                                      orderby c.DataAbertura descending
                                      where c.RepresentanteId == pRepresentanteId && c.PracaId == pPracaId && c.DataAbertura < pDataAbertura
                                      select c).FirstOrDefault<Carga>();
                return viagemanterior;

            }
        }

        public static string ValidarInclusaoCarga(int pRepresentanteId, int pPracaId, int pMes, int pAno)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                // verificar se existe carga não finalizada
                var carga = context.Carga.FirstOrDefault(cg => cg.Status != "F");
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


            using (DepositoDBEntities context = new DepositoDBEntities())
            {


                DateTime dataabertura = DateTime.Now;

                var maxCarga = context.Carga.OrderByDescending(i => i.Id).FirstOrDefault();

                int newId = maxCarga == null ? 1 : maxCarga.Id + 1;

                var novacarga = new Carga
                {
                    RepresentanteId = pRepresentanteId,
                    PracaId = pPracaId,
                    Mes = pMes,
                    Ano = pAno,
                    DataAbertura = dataabertura,
                    Status = "A"
                };

                context.Carga.Add(novacarga);
                context.SaveChanges();


                //Obter pedidos pendentes da praça e alterar o CargaId para nova Carga.

                var pedido = context.Pedido
                            .Join(context.Carga, pd => pd.CargaId, ca => ca.Id, (pd, ca) => new { Pedido = pd, Carga = ca })
                            .Where(q => q.Pedido.Status == "1" && q.Carga.PracaId == pPracaId).ToList();

                pedido.ForEach(pd => pd.Pedido.CargaId = newId);

                context.SaveChanges();
                
                
                //Gerar os titulos a receber com base no "ValorAReceber" do Pedido



                return newId;
                

            }

        }


        public static void AlterarStatusCarga(long pCargaId, string pStatus)
        {


            using (DepositoDBEntities context = new DepositoDBEntities())
            {


                var result = context.Carga.SingleOrDefault(cg => cg.Id == pCargaId);
                if (result != null)
                {
                    result.Status = pStatus;


                    switch (pStatus)
                    {
                        case "A": /* Revertendo status */
                            result.DataExportacao = null;
                            result.DataRetorno = null;
                            result.DataConferencia = null;
                            result.DataFinalizacao = null;
                            break;
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
                            //Alterar Status dos Pedidos sem ValorAReceber = 1;
                            var pedido = context.Pedido.Where(pd => pd.CargaId == pCargaId && (pd.Status == "0" || pd.Status == "1"));
                            foreach (Pedido row in pedido)
                            {
                                if (row.Status == "0")
                                {
                                    pedido.SingleOrDefault(pd => pd.Id == row.Id).Status = "1";
                                } else
                                {
                                    if (row.QuantidadeRemarcado >= 2)
                                    {
                                        pedido.SingleOrDefault(pd => pd.Id == row.Id).Status = "3";
                                    } else
                                    {
                                        pedido.SingleOrDefault(pd => pd.Id == row.Id).Remarcado = 1;
                                        pedido.SingleOrDefault(pd => pd.Id == row.Id).QuantidadeRemarcado++;                                        
                                    }                                    
                                }
                            };

                            break;
                    }


                    context.SaveChanges();
                }


            }

        }




        public static List<ListaProdutosCarga> ObterProdutosCarga(int pCargaId, Boolean pRetorno = false)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var result = context.ProdutosCarga
                           .Where(pg => pg.CargaId == pCargaId)
                           .Select(pg => new ListaProdutosCarga()
                           {
                               CodigoBarras = pg.CodigoBarras,
                               Descricao = pg.Descricao,
                               Cor = pg.Cor,
                               Tamanho = pg.Tamanho,
                               Quantidade = pg.Quantidade,
                               Retorno = pg.Retorno,
                               ValorSaida = pg.ValorSaida,
                               ValorCusto = pg.ValorCusto,
                               ProdutoGradeId = pg.ProdutoGradeId
                           });

                if (pRetorno) result = result.Where(pg => pg.Retorno > 0);

                return result.ToList<ListaProdutosCarga>();                

            }
        }

        public static ListaProdutosCarga ObterProdutoCarga(int pCargaId, int pProdutoGradeId)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var produtocarga = context.ProdutosCarga
                           .Where(pg => pg.CargaId == pCargaId && pg.ProdutoGradeId == pProdutoGradeId)
                           .Select(pg => new ListaProdutosCarga()
                           {
                               CodigoBarras = pg.CodigoBarras,
                               Descricao = pg.Descricao,
                               Cor = pg.Cor,
                               Tamanho = pg.Tamanho,
                               Quantidade = pg.Quantidade,
                               Retorno = pg.Retorno,
                               ValorSaida = pg.ValorSaida,
                               ValorCusto = pg.ValorCusto,
                               ProdutoGradeId = pg.ProdutoGradeId
                           }).FirstOrDefault();

                return produtocarga;

            }
        }

        public static Produto ObterProduto(string pCodigo)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                var produto = (from p in context.Produto
                                    where (p.CodigoBarras == pCodigo || p.Id.ToString() == pCodigo)
                                    select p).FirstOrDefault<Produto>();

                return produto;
            }

        }

        public static ProdutoGrade ObterProdutoGrade(string pCodigo)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                string vCodigoSemDigito = pCodigo.Substring(0, pCodigo.Length - 1);
                string vDigito = pCodigo.Substring(pCodigo.Length - 1);

                Console.WriteLine(vCodigoSemDigito + ':' + vDigito);

                var produtograde = (from pg in context.ProdutoGrade
                                    where (pg.CodigoBarras == vCodigoSemDigito && pg.Digito == vDigito) || pg.Id.ToString() == pCodigo
                                    select pg).FirstOrDefault<ProdutoGrade>();

                return produtograde;
            }

        }


        public static List<ListaProdutoConferencia> ObterListaProdutoConferencia(long pCargaId)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {


                var carga = context.Carga.FirstOrDefault(c => c.Id == pCargaId);


                int vCargaId = carga != null ? carga.Id : 0;


                string query = @"SELECT Produto.CodigoBarras + '' + Produto.Digito as CodigoBarras, Produto.Descricao + ' ' + Produto.Tamanho Descricao, 
                                    ISNULL(Vendido.Vendido,0) Vendido, 
                                    isnull(Carga.ViagemPlus,0) Carga, 
                                    ISNULL(Vendido.RetornoPlus,0) Retorno, 
                                    ISNULL(Consignado.Consignado,0) Consignado, 
                                    (ISNULL(Carga.ViagemPlus,0) - ISNULL(Vendido.Vendido,0) - ISNULL(Vendido.RetornoPlus,0) - ISNULL(Consignado.Consignado,0)) SaldoCarro, 
                                    ISNULL(Carga.ContagemCarro,0) ContagemCarro, 
                                    -ISNULL(Carga.ContagemCarro,0)-(ISNULL(Carga.ViagemPlus,0) - ISNULL(Vendido.Vendido,0) - ISNULL(Vendido.RetornoPlus,0) - ISNULL(Consignado.Consignado,0)) Falta, 
                                    -(ISNULL(Carga.ContagemCarro,0)-(ISNULL(Carga.ViagemPlus,0) - ISNULL(Vendido.Vendido,0) - ISNULL(Vendido.RetornoPlus,0) - ISNULL(Consignado.Consignado,0)) ) * ISNULL(Carga.Preco,0) VrDiferenca 
                                    FROM
                                        (SELECT Produto.Id Id, ProdutoGrade.Id ProdutoGradeId, Produto.CodigoBarras, ProdutoGrade.Digito, Produto.Descricao, ProdutoGrade.Tamanho 
                                            FROM Produto 
                                            INNER JOIN ProdutoGrade ON ProdutoGrade.ProdutoId = Produto.Id) AS Produto
                                    LEFT JOIN 
                                        (SELECT ProdutoGrade.Id ProdutoGradeId, ProdutoGrade.ValorSaida Preco, CargaProduto.Quantidade ViagemPlus, CargaProduto.Retorno ContagemCarro 
                                            FROM CargaProduto 
                                            INNER JOIN ProdutoGrade ON ProdutoGrade.Id = CargaProduto.ProdutoGradeId
                                        WHERE CargaProduto.CargaId = @p0) AS Carga ON Produto.ProdutoGradeId = Carga.ProdutoGradeId
                                    LEFT JOIN 
                                        (SELECT ProdutoGradeId, SUM(PedidoItem.Quantidade) Consignado 
                                            FROM Pedido
                                            INNER JOIN PedidoItem ON PedidoItem.PedidoId = Pedido.Id
                                            WHERE  Pedido.CargaId = @p0 AND Pedido.Status = 0
                                            GROUP BY ProdutoGradeId) AS Consignado ON Produto.ProdutoGradeId = Consignado.ProdutoGradeId
                                    LEFT JOIN 
                                        (SELECT ProdutoGradeId, SUM(PedidoItem.Quantidade - PedidoItem.Retorno) Vendido, SUM(PedidoItem.Retorno) RetornoPlus  
                                            FROM Pedido
                                            INNER JOIN PedidoItem ON PedidoItem.PedidoId = Pedido.Id
                                            WHERE (Pedido.CargaId = @p0)
                                                AND Pedido.Status = 2
                                            GROUP BY ProdutoGradeId) AS Vendido ON Produto.ProdutoGradeId = Vendido.ProdutoGradeId
                                    WHERE  Carga.ProdutoGradeId IS NOT NULL
                                    ORDER BY Descricao";

                var result = context.Database.SqlQuery<ListaProdutoConferencia>(query, vCargaId);

                return result.ToList<ListaProdutoConferencia>();

            }

        }


        public static List<ListaProdutosConsignados> ObterListaProdutosConsignados(long pCargaId)
        {


            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                

                string query = @"SELECT Produto.CodigoBarras + '' + ProdutoGrade.Digito as CodigoBarras, Produto.Descricao as Nome, sum(PedidoItem.Quantidade - PedidoItem.Retorno) as Quantidade
                                FROM Pedido 
                                 INNER JOIN PedidoItem ON Pedido.Id = PedidoItem.PedidoId
                                 INNER JOIN ProdutoGrade ON PedidoItem.ProdutoGradeId= ProdutoGrade.Id
                                 INNER JOIN Produto ON ProdutoGrade.ProdutoId = Produto.Id 
                                WHERE Pedido.CargaId = @p0
                                GROUP BY Produto.CodigoBarras, ProdutoGrade.Digito, Produto.Descricao";

                var result = context.Database.SqlQuery<ListaProdutosConsignados>(query, pCargaId);

                return result.ToList<ListaProdutosConsignados>();

            }

        }


        public static void InserirCargaProduto(int pCargaId, int pProdutoGradeId, double pQuantidade)
        {

            /// Modificar rotina para:
            /// Se o CargaProduto existir na lista - incrementar quantidade
            /// 
            Console.WriteLine("InserirCargaProduto");

            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var result = context.CargaProduto.SingleOrDefault(cp => cp.ProdutoGradeId == pProdutoGradeId && cp.CargaId == pCargaId);

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
                        Retorno = 0
                    };

                    context.CargaProduto.Add(novacargaproduto);
                    
                }


                context.SaveChanges();



            }


        }


        public static void AlterarCargaProduto(int pCargaId, int pCargaProdutoGradeId, double pQuantidade)
        {


            ///alterar com base no 
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var result = context.CargaProduto.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pCargaProdutoGradeId);
                if (result != null)
                {
                    result.Quantidade = pQuantidade;
                    context.SaveChanges();
                }
            }




        }


        public static void ExcluirCargaProduto(int pCargaId, int pCargaProdutoGradeId)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                context.Database.ExecuteSqlCommand("DELETE FROM CargaProduto WHERE CargaId = @pCargaId AND ProdutoGradeId = @pProdutoGradeId", new SqlParameter("@pCargaId", pCargaId), new SqlParameter("@pProdutoGradeId", pCargaProdutoGradeId));

                //CargaProduto cargaproduto = new CargaProduto() { ProdutoGradeId = pCargaProdutoGradeId, CargaId = pCargaId};
                //context.CargaProduto.Attach(cargaproduto);
                //context.CargaProduto.Remove(cargaproduto);
                //context.SaveChanges();
            }
        }


        public static void AlterarRetornoProduto(int pCargaId, int pRetornoProdutoGradeId, double pQuantidade)
        {


            ///alterar com base no 
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var result = context.CargaProduto.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pRetornoProdutoGradeId);
                if (result != null)
                {
                    Console.WriteLine("Alterando Retorno Produto - CargaId: " + pCargaId.ToString() + " ProdutoId: " + pRetornoProdutoGradeId.ToString());
                    result.Retorno = pQuantidade;
                    context.SaveChanges();
                } else
                {
                    var novacargaproduto = new CargaProduto
                    {
                        CargaId = pCargaId,
                        ProdutoGradeId = pRetornoProdutoGradeId,
                        Quantidade = 0,
                        Retorno = pQuantidade
                    };

                    context.CargaProduto.Add(novacargaproduto);
                    context.SaveChanges();

                }
            }




        }

        

        public static List<ListaPedidosRetorno> ObterListaPedidosRetorno(long pCargaId, bool pAtual = true)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {



                string query = @"SELECT CodigoPedido, VendedorId, Nome, ValorPedido, DataLancamento  
	                                FROM Pedido 
	                                INNER JOIN Vendedor ON Pedido.VendedorId = Vendedor.Id
                                WHERE CargaId = @p0 AND CargaOriginal = @p0";

                var result = context.Database.SqlQuery<ListaPedidosRetorno>(query, pCargaId);

                return result.ToList<ListaPedidosRetorno>();

            }

        }

        public static List<ListaPedidosFechados> ObterListaPedidosFechados(long pCargaId, bool pAtual = true)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                long vCargaId;

                if (pAtual)
                {
                    vCargaId = pCargaId;

                }
                else
                {

                    var carga = context.Carga.FirstOrDefault(c => c.Id == pCargaId);

                    var cargaanterior = context.Carga.Where(c => c.PracaId == carga.PracaId && c.Id < pCargaId).OrderByDescending(i => i.Id).FirstOrDefault();

                    vCargaId = cargaanterior != null ? cargaanterior.Id : 0;
                }

                string query = @"SELECT CodigoPedido, Nome, ValorPedido, ValorCompra, ValorLiquido, ValorAReceber, ValorAcerto, ValorLiquido+ValorAReceber-ValorAcerto as ValorAberto, DataLancamento  
	                                FROM Pedido 
	                                INNER JOIN Vendedor ON Pedido.VendedorId = Vendedor.Id
                                WHERE (CargaId = @p0 OR CargaOriginal = @p0) AND DataRetorno IS NOT NULL";

                var result = context.Database.SqlQuery<ListaPedidosFechados>(query, vCargaId);

                return result.ToList<ListaPedidosFechados>();

            }

        }



        public static List<ListaPedidoItem> ObterListaPedidoItem(long pPedidoId)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                string query = @"SELECT PedidoItem.Id as PedidoItemId, Produto.CodigoBarras + '' + ProdutoGrade.Digito as CodigoBarras, Produto.Descricao NomeProduto, PedidoItem.Quantidade, PedidoItem.Retorno, PedidoItem.Preco
	                            FROM PedidoItem 
		                            INNER JOIN ProdutoGrade ON PedidoItem.ProdutoGradeId = ProdutoGrade.Id
		                            INNER JOIN Produto ON ProdutoGrade.ProdutoId = Produto.Id
                            WHERE PedidoItem.PedidoId = @p0";

                Console.WriteLine("Exibindo lista de itens do pedido # " + pPedidoId.ToString());

                var result = context.Database.SqlQuery<ListaPedidoItem>(query, pPedidoId);

                return result.ToList<ListaPedidoItem>();
            }

        }



        public static void AlterarPedidoItem(int pPedidoItemId, double pQuantidade, double pQtdRetorno, double pPreco)
        {
            ///alterar com base no 
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var result = context.PedidoItem.SingleOrDefault(pi => pi.Id == pPedidoItemId);
                if (result != null)
                {
                    result.Quantidade = pQuantidade;
                    result.Retorno = pQtdRetorno;
                    result.Preco = pPreco;
                    context.SaveChanges();
                }
            }

        }

        public static void InserirPedidoItem(int pPedidoId, int pProdutoGradeId, double pQuantidade, double pQtdRetorno, double pPreco)
        {


            using (DepositoDBEntities context = new DepositoDBEntities())
            {


                DateTime dataabertura = DateTime.Now;

                var maxPedidoItem = context.PedidoItem.OrderByDescending(i => i.Id).FirstOrDefault();

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

                context.PedidoItem.Add(novopedidoitem);
                context.SaveChanges();                


            }

        }

        public static List<ListaAReceber> ObterListaAReceber(long pCargaId)
        {


            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                var carga = context.Carga.FirstOrDefault(c => c.Id == pCargaId);

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

                var result = context.Database.SqlQuery<ListaAReceber>(query, pCargaId, vPracaId);

                return result.ToList<ListaAReceber>();

            }

        }

        public static void SalvarAReceber(int pReceberId, int pReceberBaixaId, double pValor, string pData)
        {


            
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                if (pReceberBaixaId == 0)
                {

                    var maxReceberBaixa = context.ReceberBaixa.OrderByDescending(i => i.Id).FirstOrDefault();



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

                    context.ReceberBaixa.Add(receberbaixa);
                    context.SaveChanges();



                } else
                {

                    var result = context.ReceberBaixa.SingleOrDefault(rb => rb.Id == pReceberBaixaId);
                    if (result != null)
                    {
                        Console.WriteLine("Alterando Baixa A Receber - Id: " + pReceberBaixaId.ToString());
                        result.Valor = pValor;
                        result.DataPagamento = Convert.ToDateTime(pData);
                        context.SaveChanges();
                    }

                }
                
            }




        }


        public static List<Vendedor> ObterListaVendedor(long pCargaId = 0)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                List<Vendedor> result;

                if (pCargaId == 0) {

                    result = context.Vendedor.ToList<Vendedor>();

                } else
                {


                    var carga = context.Carga.Where(c => c.Id == pCargaId).FirstOrDefault();

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


                        result = context.Database.SqlQuery<Vendedor>(query, carga.RepresentanteId, carga.PracaId, carga.Mes.ToString() + carga.Ano.ToString()).ToList<Vendedor>();

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
            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                var vendedor = context.Vendedor.Where(vd => vd.Id == pVendedorId).FirstOrDefault();

                return vendedor;

            }
        }

        public static Vendedor PesquisarVendedor(string pCPFCnpj = "")
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                var vendedor = context.Vendedor.Where(vd => vd.CpfCnpj.Trim() == pCPFCnpj).FirstOrDefault();

                return vendedor;

            }
        }


        public static Pedido ObterVendedorPedido(long pVendedorId, long pCargaId)
        {
            using (DepositoDBEntities context = new DepositoDBEntities())
            {
                var pedido = context.Pedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.VendedorId == pVendedorId && p.CargaId == pCargaId);
                return pedido;
            }

        }





        public static decimal[] ObterTotalizadores(int pCargaId)
        {


            var ret = new decimal[4];


            using (DepositoDBEntities context = new DepositoDBEntities())
            {



                var result = (from tt in context.Totalizadores
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

        public static void FinalizarExportacao()
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {

                string query = "";


                query = @"UPDATE Vendedor Set temp_old_id = id WHERE left(temp_old_id,3) = '999' and len(temp_old_id) > 5";

                context.Database.ExecuteSqlCommand(query);
            }


        }

    }
}
