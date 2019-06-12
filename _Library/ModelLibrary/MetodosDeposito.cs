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

                var usuario = context.Usuario.FirstOrDefault(u => u.Login == pLogin);

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


        public static Carga ObterViagemAnterior(long pRepresentanteId, long pPracaId, DateTime pDataAbertura)
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

                return newId;


            }

        }


        public static void AlterarrStatusCarga(long pCargaId, string pStatus)
        {


            using (DepositoDBEntities context = new DepositoDBEntities())
            {


                var result = context.Carga.SingleOrDefault(cg => cg.Id == pCargaId);
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


                    context.SaveChanges();
                }


            }

        }




        public static List<ListaProdutosCarga> ObterProdutosCarga(int pCargaId)
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
                               QuantidadeRetorno = pg.QuantidadeRetorno,
                               ValorSaida = pg.ValorSaida,
                               ValorCusto = pg.ValorCusto,
                               ProdutoGradeId = pg.ProdutoGradeId
                           });

                return result.ToList<ListaProdutosCarga>();                

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


        public static List<ListaProdutoConferencia> ObterProdutoConferencia(long pCargaId)
        {

            using (DepositoDBEntities context = new DepositoDBEntities())
            {


                string query = @"SELECT ProdutoGrade.CodigoBarras, Produto.Descricao, 
                                    sum(PedidoItem.Quantidade-PedidoItem.Retorno) Vendido, sum(CargaProduto.Quantidade) Viagem, 
                                    sum(CargaProduto.QuantidadeRetorno) Retorno, sum(PedidoItem.Quantidade) Consignado, 
                                    sum(CargaProduto.Quantidade-CargaProduto.QuantidadeRetorno-PedidoItem.Quantidade) SaldoCarro   
                                FROM CargaProduto 
	                                INNER JOIN ProdutoGrade ON CargaProduto.ProdutoGradeId = ProdutoGrade.Id
	                                INNER JOIN Produto ON ProdutoGrade.ProdutoId = Produto.Id
	                                LEFT JOIN Pedido ON Pedido.CargaId = CargaProduto.CargaId
	                                LEFT JOIN PedidoItem ON CargaProduto.ProdutoGradeId = PedidoItem.ProdutoGradeId AND PedidoId = Pedido.Id
                                WHERE CargaProduto.CargaId = @p0
                                GROUP BY ProdutoGrade.CodigoBarras, Produto.Descricao";

                var result = context.Database.SqlQuery<ListaProdutoConferencia>(query, pCargaId);

                return result.ToList<ListaProdutoConferencia>();

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
                        QuantidadeRetorno = 0
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
                    result.QuantidadeRetorno = pQuantidade;
                    context.SaveChanges();
                }
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


                ret[0] = Convert.ToDecimal(result.QtdProdutos);
                ret[1] = Convert.ToDecimal(result.TotalProdutos);

                /*

                

                ret[0] = Convert.ToDouble(quantidade.FirstOrDefault().QtdProdutos);
                //ret = context.Database.ExecuteSqlCommand("SELECT ");;

                ret[1] = 111;

                */
                /*var cargaproduto = (from cp in context.CargaProduto
                                    join pg in context.ProdutoGrade on cp.ProdutoGradeId equals pg.Id
                                    where cp.CargaId == pCargaId
                                    select new { cp.Id, cp.Quantidade, pg.ValorSaida } into x
                                    group x by x.Id into grupo
                                    select new
                                    {
                                        QtdProdutos = grupo.Sum(g => g.Quantidade),
                                        TotalProdutos = grupo.Sum(g => g.ValorSaida)
                                    });*/

                /*

                var cargaproduto = (from cp in context.CargaProduto
                                    join pg in context.ProdutoGrade on cp.ProdutoGradeId equals pg.Id
                                    where cp.CargaId == pCargaId
                                    select new { cp.Quantidade, pg.ValorSaida }
                                    );


                var result = cargaproduto.Select(tt => new Totalizadores()
                {
                    QtdProdutos = Convert.ToDouble(tt.Quantidade),
                    TotalProdutos = Convert.ToDouble(tt.ValorSaida)
                }
                ).ToList<Totalizadores>();

                ret[0] = Convert.ToDouble(result["QtdProdutos"].Sum()) ;





                var cargaproduto = (from cp in context.CargaProduto
                                join pg in context.ProdutoGrade on cp.ProdutoGradeId equals pg.Id into subs
                                from sub in subs.DefaultIfEmpty()
                                group sub by new { cp.Id } into gr
                                select new
                                {
                                    QtdProdutos = 0,
                                    TotalProdutos = gr.Sum(g => g.ValorSaida)                                    
                                }).ToList();






                var result = context.CargaProduto
                           .Where(cp => cp.CargaId == pCargaId)
                           .Join(context.ProdutoGrade on cp.ProdutoGradeId equals pg.Id)
                           .Select(pg => new ListaProdutosCarga()
                           {
                               CodigoBarras = pg.CodigoBarras,
                               Descricao = pg.Descricao,
                               Cor = pg.Cor,
                               Tamanho = pg.Tamanho,
                               Quantidade = pg.Quantidade,
                               QuantidadeRetorno = pg.QuantidadeRetorno,
                               ValorSaida = pg.ValorSaida,
                               ValorCusto = pg.ValorCusto,
                               ProdutoGradeId = pg.ProdutoGradeId
                           });

                Console.WriteLine(cargaproduto.);

                ret[0] = 0; //Convert.ToDouble(cargaproduto.FirstOrDefault().QtdProdutos);
                ret[1] = Convert.ToDouble(cargaproduto.FirstOrDefault().TotalProdutos);

               

    */

                return ret;


            }
            
        }

    }
}
