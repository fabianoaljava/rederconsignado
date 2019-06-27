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

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var usuario = context.RepUsuario.FirstOrDefault(u => u.Login.ToLower() == pLogin.ToLower());

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
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                var count = context.RepUsuario.Count();

                if (count > 0) return true; else return false;  

                

            }            
            
        }



        public static List<RepPraca> ObterListaPracas()
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                return context.RepPraca.ToList<RepPraca>();
            }
        }

        public static RepPraca ObterPraca(int i)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                var praca = context.RepPraca.FirstOrDefault(p => p.Id == i);
                return praca;
            }
        }


        public static List<RepRepresentante> ObterListaRepresentantes()
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                return context.RepRepresentante.ToList<RepRepresentante>();
            }
        }


        public static RepRepresentante ObterRepresentante(int i)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                var representante = context.RepRepresentante.FirstOrDefault(p => p.Id == i);
                return representante;
            }
        }


        public static List<RepCarga> ObterListaCargas()
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                return context.RepCarga.ToList<RepCarga>();
            }
        }

        public static RepCarga ObterCarga(long pRepresentanteId, long pPracaId, int pMes, int pAno)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var carga = context.RepCarga.FirstOrDefault(r => r.RepresentanteId == pRepresentanteId && r.PracaId == pPracaId && r.Mes == pMes && r.Ano == pAno);

                return carga;

            }
        }

        public static RepCarga ObterCargaAtual()
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var carga = context.RepCarga.FirstOrDefault();

                return carga;

            }
        }


        public static RepCargaAnterior ObterCargaAnterior()
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var carganterior = context.RepCargaAnterior.OrderByDescending(c => c.Id).FirstOrDefault();

                return carganterior;

            }
        }


        public static void AlterarStatusCarga(long pCargaId, string pStatus)
        {


            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {


                var result = context.RepCarga.SingleOrDefault(cg => cg.Id == pCargaId);
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

        public static List<ListaRepProdutosConferencia> ObterProdutosConferencia(long pCargaId)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {


                string query = @"SELECT RepProduto.CodigoBarras || RepProdutoGrade.Digito as CodigoBarras,
	                                RepProduto.Descricao,
	                                RepCargaProduto.Quantidade as QuantidadeCarga,
	                                RepCargaConferencia.Quantidade as QuantidadeInformada,
	                                RepCargaProduto.Quantidade - IFNULL(RepCargaConferencia.Quantidade, 0) as Diferenca,
	                                RepProduto.Id as ProdutoId,
	                                RepCargaProduto.Id as CargaProdutoId,
	                                RepProdutoGrade.Id as ProdutoGradeId,
	                                RepCarga.Id as CargaId
	                                FROM RepCarga 
                                INNER JOIN RepCargaProduto ON RepCarga.Id = RepCargaProduto.CargaId
                                INNER JOIN RepProdutoGrade ON RepCargaProduto.ProdutoGradeId = RepProdutoGrade.Id
                                INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id
                                LEFT JOIN RepCargaConferencia ON RepCargaConferencia.CargaId = RepCarga.Id AND RepCargaConferencia.ProdutoGradeId = RepProdutoGrade.Id";

                var result = context.Database.SqlQuery<ListaRepProdutosConferencia>(query);

                //var result = context.RepProdutosConferencia
                //           .Where(pg => pg.CargaId == pCargaId)
                //           .Select(pg => new ListaRepProdutosConferencia()
                //           {
                //               CodigoBarras = pg.CodigoBarras.ToString(),
                //               Descricao = pg.Descricao,
                //               QuantidadeCarga = pg.QuantidadeCarga,
                //               QuantidadeInformada = pg.QuantidadeInformada,
                //               Diferenca = pg.Diferenca,
                //               CargaId = pg.CargaId,
                //               ProdutoGradeId = pg.ProdutoGradeId,
                //               ProdutoId = pg.ProdutoId                              
                //           });

                return result.ToList<ListaRepProdutosConferencia>();

            }
        }





        public static RepProduto ObterProduto(string pCodigo)
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var produto = (from p in context.RepProduto
                               where (p.CodigoBarras == pCodigo || p.Id.ToString() == pCodigo)
                               select p).FirstOrDefault<RepProduto>();

                return produto;
            }

        }

        public static RepProdutoGrade ObterProdutoGrade(string pCodigo)
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                if (pCodigo != "")
                {
                    string vCodigoSemDigito = pCodigo.Substring(0, pCodigo.Length - 1);
                    string vDigito = pCodigo.Substring(pCodigo.Length - 1);

                    Console.WriteLine(vCodigoSemDigito + ':' + vDigito);

                    var produtograde = (from pg in context.RepProdutoGrade
                                        where (pg.CodigoBarras == vCodigoSemDigito && pg.Digito == vDigito) || pg.Id.ToString() == pCodigo
                                        select pg).FirstOrDefault<RepProdutoGrade>();

                    return produtograde;
                } else
                {
                    return null;
                }

                
            }

        }


        public static void InserirProdutoConferencia(long pCargaId, long pProdutoGradeId, decimal pQuantidade)
        {


            Console.WriteLine("InserirProdutoConferencia");

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                var result = context.RepCargaConferencia.SingleOrDefault(cp => cp.ProdutoGradeId == pProdutoGradeId && cp.CargaId == pCargaId);

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

                    context.RepCargaConferencia.Add(novacargaproduto);

                }


                context.SaveChanges();



            }


        }


        public static void AlterarProdutoConferencia(long pCargaId, long pCargaProdutoGradeId, decimal pQuantidade)
        {

            Console.WriteLine("Alterando Produto Conferencia - CargaId: " + pCargaId.ToString() + " CargaProdutoGradeId >" + pCargaProdutoGradeId.ToString() + " Quantidade>" + pQuantidade.ToString());

            ///alterar com base no 
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                var result = context.RepCargaConferencia.SingleOrDefault(cp => cp.CargaId == pCargaId && cp.ProdutoGradeId == pCargaProdutoGradeId);
                if (result != null)
                {
                    result.Quantidade = pQuantidade;
                    context.SaveChanges();
                }
            }

        }


        public static void ExcluirProdutoConferencia(long pCargaId, long pCargaProdutoGradeId)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                context.Database.ExecuteSqlCommand("DELETE FROM RepCargaConferencia WHERE CargaId = @pCargaId AND ProdutoGradeId = @pProdutoGradeId", new SQLiteParameter("@pCargaId", pCargaId), new SQLiteParameter("@pProdutoGradeId", pCargaProdutoGradeId));

            }
        }
     

        public static List<RepVendedor> ObterListaVendedor()
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                return context.RepVendedor.ToList<RepVendedor>();
            }
        }

        public static List<RepCidade> ObterListaCidade(long pEstadoId = 0)
        {
            

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                if (pEstadoId == 0)
                {
                    return context.RepCidade.ToList<RepCidade>();
                } else
                {
                    return context.RepCidade.Where(cid => cid.EstadoId == pEstadoId).ToList<RepCidade>();
                }                
            }

        }

        public static List<RepEstado> ObterListaEstado()
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                return context.RepEstado.ToList<RepEstado>();
            }
        }


        public static RepVendedor ObterVendedor(long pVendedorId)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var vendedor = context.RepVendedor.Where(vd => vd.Id == pVendedorId).FirstOrDefault();

                return vendedor;

            }
        }





        public static List<ListaRepVendedorHome> ObterListaVendedorHome(long pCargaId, string pFiltro = "")
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                string query = @"SELECT RepVendedor.Id, Nome, CpfCnpj as Documento, Endereco, Complemento, Bairro, Cidade || UF as CidadeUF, Telefone || '/' || Celular as Telefones,
                                    CASE WHEN PedidoAnterior.VendedorId IS NOT NULL
                                            THEN true
                                            ELSE false
                                            END AS PedidoAnterior,
                                    CASE 
	                                    WHEN ValorAberto  = 0 THEN 'Total'
	                                    WHEN ValorAberto >0  THEN 'Parcial ' || Recebido
	                                    ELSE 'Não ' || Recebido
	                                    END AS Recebido,	   
                                    CASE WHEN PedidoAtual.VendedorId IS NOT NULL
                                            THEN true
                                            ELSE false
                                            END AS PedidoAtual, 
                                    CodigoPedido,
                                    CASE 
	                                    WHEN ValorAberto  = 0 THEN false
	                                    WHEN ValorAberto >0 THEN true
	                                    ELSE true
	                                    END AS Receber    
                                    FROM RepVendedor
                                    LEFT JOIN (SELECT VendedorId, CodigoPedido  FROM RepPedido WHERE CargaId = @p0) AS PedidoAtual ON RepVendedor.Id = PedidoAtual.VendedorId
                                    LEFT JOIN (SELECT VendedorId, count(RepCargaAnterior.Id) Recebido
													FROM RepPedido 
														INNER JOIN RepCargaAnterior ON RepPedido.CargaId = RepCargaAnterior.Id 
														WHERE CargaId != @p0 and ValorAcerto = 0
														GROUP BY VendedorId) AS PedidoAnterior ON RepVendedor.Id = PedidoAnterior.VendedorId
                                    LEFT JOIN RepPosicaoFinanceira ON RepVendedor.Id = RepPosicaoFinanceira.VendedorId";


                if (pFiltro != "") query += " WHERE " + pFiltro;

                var result = context.Database.SqlQuery<ListaRepVendedorHome>(query, pCargaId);


                return result.ToList<ListaRepVendedorHome>();
            }
        }





        public static RepVendedor PesquisarVendedor(string pCodigo = "", string pCPFCnpj = "")
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var vendedor = context.RepVendedor.Where(vd => vd.Id.ToString() == pCodigo || vd.CpfCnpj.Trim() == pCPFCnpj).FirstOrDefault();

                return vendedor;

            }
        }


        public static void SalvarVendedor(string pModo, RepVendedor pVendedor, long pVendedorId = 0)
        {


            if (pModo == "Create")
            {
                using (RepresentanteDBEntities context = new RepresentanteDBEntities())
                {

                    if (pVendedorId > 0)
                    {

                        pVendedor.Id = pVendedorId;

                    } else
                    {
                        var maxVendedor = context.RepVendedor.OrderByDescending(i => i.Id).FirstOrDefault();

                        long newId = maxVendedor == null ? 1 : maxVendedor.Id + 1;

                        pVendedor.Id = newId;
                    }
                        


                    Console.WriteLine("Incluindo Vendedor - " + pVendedorId.ToString() + " Nome> " + pVendedor.Nome + " ...");

                    context.RepVendedor.Add(pVendedor);

                    context.SaveChanges();

                    Console.WriteLine("Vendedor Incluído!");
                }

            } else
            {

                using (RepresentanteDBEntities context = new RepresentanteDBEntities())
                {
                    Console.WriteLine("Alterando Vendedor - " + pVendedorId.ToString() + " Nome> " + pVendedor.Nome + " ...");
                    var result = context.RepVendedor.SingleOrDefault(vd => vd.Id == pVendedorId);
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

                        context.SaveChanges();

                        Console.WriteLine("Vendedor Alterado!");
                    }
                }


            }

        }

        public static RepVendedorBase PesquisarVendedorBase(string pCPFCnpj = "")
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var vendedor = context.RepVendedorBase.Where(vd => vd.CpfCnpj.Trim() == pCPFCnpj).FirstOrDefault();
                return vendedor;

            }
        }


        public static RepVendedorBase ObterVendedorBase(long pVendedorId)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var vendedor = context.RepVendedorBase.Where(vd => vd.Id == pVendedorId).FirstOrDefault();

                return vendedor;

            }
        }


        public static RepPedido ObterVendedorPedido(long pVendedorId, long pCargaId)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {
                var pedido = context.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.VendedorId == pVendedorId && p.CargaId == pCargaId);
                return pedido;
            }
            
        }


        public static List<ListaRepVendedorPedido> ObterVendedorPedidoItem(long pVendedorId, long pCargaId)
        {
            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var result = context.RepPedido
                           .Join(context.RepPedidoItem, pd => pd.Id, pi => pi.PedidoId, (pd, pi) => new { RepPedido = pd, RepPedidoItem = pi })
                           .Join(context.RepProdutoGrade, pi => pi.RepPedidoItem.ProdutoGradeId, pg => pg.Id, (pi, pg) => new { RepPedidoItem = pi, RepProdutoGrade = pg })
                           .Join(context.RepProduto, pg => pg.RepProdutoGrade.ProdutoId, pr => pr.Id, (pg, pr) => new { RepProdutoGrade = pg, RepProduto = pr })
                           .Where(pd => pd.RepProdutoGrade.RepPedidoItem.RepPedido.VendedorId == pVendedorId && pd.RepProdutoGrade.RepPedidoItem.RepPedido.CargaId == pCargaId)
                           .Select(ls => new ListaRepVendedorPedido()
                           {
                               Id = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Id,
                               PedidoId = ls.RepProdutoGrade.RepPedidoItem.RepPedido.Id,
                               ProdutoGradeId = ls.RepProdutoGrade.RepProdutoGrade.Id,
                               CodigoBarras = ls.RepProdutoGrade.RepProdutoGrade.CodigoBarras + ls.RepProdutoGrade.RepProdutoGrade.Digito,
                               Descricao = ls.RepProduto.Descricao,
                               Cor = ls.RepProdutoGrade.RepProdutoGrade.Tamanho,
                               Tamanho = ls.RepProdutoGrade.RepProdutoGrade.Cor,
                               Quantidade = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Quantidade,
                               Retorno = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Retorno,
                               Preco = ls.RepProdutoGrade.RepPedidoItem.RepPedidoItem.Preco
                           })
                           .OrderBy(pd => pd.CodigoBarras) ;

                return result.ToList<ListaRepVendedorPedido>();

            }
        }


        public static long InserirPedido(long pVendedorId, long pCargaId)
        {


            Console.WriteLine("Inserindo Pedido");

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var maxPedido = context.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault();

                long newId = maxPedido == null ? 1 : maxPedido.Id + 1;

                var carga = context.RepCarga.FirstOrDefault();

                /***********
                 * 
                 * A FaixaComissao será implementada manualmente, via código por enquanto. 
                 * Durante a atividade (Trello) Configuração - será implementado via banco de dados
                 * Sugestão: Criar uma tabela especifica para comissao com adição de N faixas.
                 * 
                */

                string vCodigoPedido = "";

                //vCodigoPedido += carga.PracaId.ToString().PadLeft(3, '0');
                //vCodigoPedido += carga.RepresentanteId.ToString().PadLeft(3, '0');
                //vCodigoPedido += carga.Mes.ToString().PadLeft(2, '0');
                //vCodigoPedido += carga.Ano.ToString();
                vCodigoPedido += pVendedorId.ToString().PadLeft(5, '0');
                vCodigoPedido += newId.ToString().PadLeft(7,'0');


                var novopedido = new RepPedido
                {
                    Id = newId,
                    VendedorId = pVendedorId,                    
                    CargaId = pCargaId,
                    CargaAtual = pCargaId,
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
                    ValorAReceber = 0,
                    ValorAcerto = 0,
                    QuantidadeRetorno = 0,
                    Remarcado = 0,
                    Status = "0"
                };

                context.RepPedido.Add(novopedido);


                context.SaveChanges();


                return newId;
            }
        }


        public static void AtualizarPedido(long pPedidoId, decimal pValor)
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {


                var pedido = context.RepPedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {

                    var tmpValor = pedido.ValorPedido;


                    Console.WriteLine("ValorTotal:" + tmpValor.ToString());


                    pedido.ValorPedido = (tmpValor + pValor);

                    context.SaveChanges();

                }



            }

        }

        public static void RetornarPedido(long pPedidoId, decimal pValor)
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {


                var pedido = context.RepPedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {

                    var tmpDataRetorno = pedido.DataRetorno;

                    if (tmpDataRetorno == null)
                    {
                        pedido.DataRetorno = DateTime.Now.Date;
                    }


                    context.SaveChanges();

                }



            }

        }



        public static void InserirPedidoItem(long pCargaId, long pVendedorId, long pProdutoGradeId, decimal pQuantidade, decimal pPreco)
        {


            Console.WriteLine("InserirPedidoItem");


            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var pedido = context.RepPedido.SingleOrDefault(pi => pi.VendedorId == pVendedorId && pi.CargaId == pCargaId);
                long vPedidoId = 0;

                if (pedido == null)
                {

                    vPedidoId = InserirPedido(pVendedorId, pCargaId);


                } else
                {

                    vPedidoId = pedido.Id;
                }


                var result = context.RepPedidoItem.SingleOrDefault(pi => pi.PedidoId == vPedidoId && pi.ProdutoGradeId == pProdutoGradeId);

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

                    var maxPedidoItem = context.RepPedidoItem.OrderByDescending(i => i.Id).FirstOrDefault();

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

                    context.RepPedidoItem.Add(novopedidoitem);

                }

                AtualizarPedido(vPedidoId, pQuantidade * pPreco);
                context.SaveChanges();



            }


        }


        public static void AtualizarPedidoItem(long pPedidoId, long pProdutoGradeId, decimal pQuantidade, decimal pPreco)
        {

            Console.WriteLine("AtualizarPedidoItem: " + pProdutoGradeId.ToString());


            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var pedidoitem = context.RepPedidoItem.FirstOrDefault(pi => pi.ProdutoGradeId == pProdutoGradeId && pi.PedidoId == pPedidoId);

                if (pedidoitem != null)
                {
                    var vValor = pedidoitem.Quantidade * pedidoitem.Preco;
                    AtualizarPedido(pedidoitem.PedidoId, -(Convert.ToDecimal(vValor)));
                    pedidoitem.Quantidade = pQuantidade;
                    pedidoitem.Preco = pPreco;

                    context.SaveChanges();

                    AtualizarPedido(pedidoitem.PedidoId, pQuantidade * pPreco);
                }
            }
        }




        public static void ExcluirPedidoItem(long pPedidoItemId, long pPedidoId, decimal pQuantidade, decimal pPreco)
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                context.Database.ExecuteSqlCommand("DELETE FROM RepPedidoItem WHERE Id = @pPedidoItemId", new SQLiteParameter("@pPedidoItemId", pPedidoItemId));

                AtualizarPedido(pPedidoId, -(pQuantidade*pPreco));

            }

        }


        public static void RetornarPedidoItem(long pPedidoId, long pProdutoGradeId, decimal pQuantidade)
        {

            Console.WriteLine("AtualizarPedidoItem: " + pProdutoGradeId.ToString());


            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                var pedidoitem = context.RepPedidoItem.FirstOrDefault(pi => pi.ProdutoGradeId == pProdutoGradeId && pi.PedidoId == pPedidoId);

                if (pedidoitem != null)
                {
                    //var vValor = pedidoitem.Quantidade * pedidoitem.Preco;
                    RetornarPedido(pedidoitem.PedidoId, 0);
                    pedidoitem.Retorno = pQuantidade;                    
                    context.SaveChanges();

                }
            }
        }


        public static List<ListaRecebimentos> ObterListaRecebimentos(long pVendedorId, long pCargaId)
        {


            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {

                string query = @"SELECT RepReceberBaixa.Id, RepReceber.Id ReceberId, Documento, Serie, ValorNF 
                                as ValorDuplicata, Valor as ValorRecebido, DataEmissao, DataVencimento, 
                                RepReceberBaixa.DataPagamento, Observacoes 
                                FROM RepReceber LEFT JOIN RepReceberBaixa ON RepReceber.Id = RepReceberBaixa.ReceberId 
                                WHERE vendedorId = @p0 AND (RepReceberBaixa.DataPagamento IS NULL OR RepReceberBaixa.CargaId = @p1)";  

                var result = context.Database.SqlQuery<ListaRecebimentos>(query, pVendedorId, pCargaId);

                return result.ToList<ListaRecebimentos>();


            }

        }


        public static void ReceberAcerto(long pPedidoId, decimal pValor)
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {


                var pedido = context.RepPedido.SingleOrDefault(pd => pd.Id == pPedidoId);

                if (pedido != null)
                {


                    pedido.ValorAcerto = pValor;

                    context.SaveChanges();

                }



            }

        }


        public static void ReceberDuplicata(long pId, long pReceberId, long pCargaId, decimal pValor)
        {

            Console.WriteLine("Inserindo Receber Baixa");

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {


                if (pId == 0)
                {
                    var maxReceberBaixa = context.RepReceberBaixa.OrderByDescending(i => i.Id).FirstOrDefault();

                    long newId = maxReceberBaixa == null ? 1 : maxReceberBaixa.Id + 1;


                    var novoreceberbaixa = new RepReceberBaixa
                    {
                        Id = newId,
                        ReceberId = pReceberId,
                        CargaId = pCargaId,
                        Valor = pValor,
                        DataPagamento = DateTime.Now,
                        DataBaixa = DateTime.Now,
                        Juros = 0,
                        Desconto = 0
                    };

                    context.RepReceberBaixa.Add(novoreceberbaixa);


                    context.SaveChanges();
                } else
                {

                    var receberbaixa = context.RepReceberBaixa.SingleOrDefault(rb => rb.Id == pId);

                    if (receberbaixa != null)
                    {


                        receberbaixa.DataPagamento = DateTime.Now;
                        receberbaixa.DataBaixa = DateTime.Now;
                        receberbaixa.Valor = pValor;

                        context.SaveChanges();

                    }

                }
                

                var receber = context.RepReceber.SingleOrDefault(rc => rc.Id == pReceberId);

                if (receber != null)
                {


                    receber.DataPagamento = DateTime.Now;
                    receber.Status = "1";

                    context.SaveChanges();

                }


            }

        }

        public static List<ListaRepPosicaoFinanceira> ObterPosicaoFinanceira()
        {

            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {


                string query = @"SELECT RepVendedor.Id, RepVendedor.Nome,
		                            CASE WHEN Receber.ValorAReceber IS NULL
		                               THEN (Pedido.ValorAReceber)
		                               ELSE (Receber.ValorAReceber)
		                               END AS Receber,
		                            CASE WHEN Receber.ValorRecebido IS NULL
		                               THEN (Pedido.ValorRecebido)
		                               ELSE (Receber.ValorRecebido)
		                               END AS Recebido,
		                            CASE WHEN Receber.ValorAReceber IS NULL
		                               THEN (IFNULL(Pedido.ValorAReceber,0)-IFNULL(Pedido.ValorRecebido,0))
		                               ELSE (IFNULL(Receber.ValorAReceber,0)-IFNULL(Receber.ValorRecebido,0))
		                               END AS Aberto,
		                            Pedido.Quantidade Quantidade, Pedido.Retorno Retorno
                                FROM RepVendedor 
                                INNER JOIN
                                (SELECT VendedorId, SUM(ValorAReceber) AS ValorAReceber, Sum(ValorAcerto) AS ValorRecebido, Sum(ValorAReceber-ValorAcerto) AS ValorAberto, Sum(Quantidade) AS Quantidade, Sum(Retorno) AS Retorno 
                                FROM RepPedido 
                                LEFT JOIN RepPedidoItem ON RepPedido.Id = RepPedidoItem.PedidoId
                                GROUP BY VendedorId) AS Pedido ON RepVendedor.Id = Pedido.VendedorId
                                LEFT JOIN (
                                SELECT VendedorId, SUM(ValorNF) AS ValorAReceber, Sum(Valor) AS ValorRecebido, Sum(ValorNF-Valor) AS ValorAberto 
                                FROM RepReceber 
                                LEFT JOIN RepReceberBaixa ON RepReceber.Id = RepReceberBaixa.ReceberId
                                GROUP BY VendedorId
                                ) AS Receber ON RepVendedor.Id = Receber.VendedorId";


                var result = context.Database.SqlQuery<ListaRepPosicaoFinanceira>(query);


                return result.ToList<ListaRepPosicaoFinanceira>();


                /*
                var result = context.RepPosicaoFinanceira
                           .Join(context.RepVendedor, pf => pf.VendedorId, vd => vd.Id, (pf, vd) => new { RepPosicaoFinanceira = pf, RepVendedor = vd })
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


            using (RepresentanteDBEntities context = new RepresentanteDBEntities())
            {



                ListaTotalizadores vTotalizador = new ListaTotalizadores();



                string query = @"SELECT RepCarga.Id, sum(RepCargaProduto.Quantidade) QtdProdutos, sum(ValorSaida) TotalProdutos 
                                    FROM RepCarga 
                                    INNER JOIN RepCargaProduto On RepCargaProduto.CargaId = RepCarga.Id 
                                    INNER JOIN RepProdutoGrade ON ProdutoGradeId = RepProdutoGrade.Id
                                WHERE RepCarga.Id = @p0
                                GROUP BY RepCarga.Id";




                var totalizador = context.Database.SqlQuery<ListaTotalizadores>(query, pCargaId).FirstOrDefault();

                vTotalizador.QtdProdutos = totalizador.QtdProdutos;
                vTotalizador.TotalProdutos = totalizador.TotalProdutos;

                return vTotalizador;

            }
        }






    }
}
