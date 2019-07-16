using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Windows.Forms;


namespace ModelLibrary
{
    public class ImportarExportar
    {
        public static string cResult;
        public static long cCargaId;
        public static long cPracaId;
        public static long cRepresentanteId;
        public static int cMes;
        public static int cAno;



        //////////////////////////////////////////////////////////////////////////////////
        ////////// ROTINAS BÁSICAS DE IMPORTAÇÃO / EXPORTAÇÃO                  ///////////
        //////////////////////////////////////////////////////////////////////////////////


        public static Boolean ProcessarRotina(string pRotina)
        {

            Type vTipo = Type.GetType("ModelLibrary.ImportarExportar");
            MethodInfo vMetodo = vTipo.GetMethod(pRotina);
            Boolean result = (Boolean)vMetodo.Invoke(null, null);

            return true;

        }


        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        ////////// IMPORTAÇÃO                                                  ///////////
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////


        public static List<ListaImportacaoExportacao> ObterListaImportacao(long pCargaId)
        {



            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var result = new List<ListaImportacaoExportacao>();

                var vTable = new ListaImportacaoExportacao();


                cCargaId = pCargaId;

                var cargaatual = deposito.Carga.FirstOrDefault(c => c.Id == cCargaId);

                cPracaId = Convert.ToInt64(cargaatual.PracaId);
                cRepresentanteId = Convert.ToInt64(cargaatual.RepresentanteId);
                cMes = Convert.ToInt32(cargaatual.Mes);
                cAno = Convert.ToInt32(cargaatual.Ano);


                int count = 0;

                string query = "";

                count = deposito.Pedido
                        .Join(deposito.Carga, pd => pd.CargaId, ca => ca.Id, (pd, ca) => new { Pedido = pd, Carga = ca })
                        .Where(q => q.Pedido.Status == "1" && q.Carga.PracaId == cPracaId).Count();

                count += deposito.Pedido
                    .Join(deposito.Carga, pd => pd.CargaId, ca => ca.Id, (pd, ca) => new { Pedido = pd, Carga = ca })
                    .Where(pd => pd.Pedido.ValorAcerto > 0 && pd.Carga.PracaId == cPracaId)
                    .Select(pd => pd.Pedido).Count();

                vTable.Tabela = "Todas";
                vTable.Acao = "Preparar Importacao";
                vTable.Rotina = "ImportarPreparar";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);



                vTable = new ListaImportacaoExportacao();

                count = deposito.Usuario.Count();

                vTable.Tabela = "Usuario";
                vTable.Acao = "Importar Usuarios";
                vTable.Rotina = "ImportarUsuario";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);



                vTable = new ListaImportacaoExportacao();

                count = deposito.Praca.Count();

                vTable.Tabela = "Praca";
                vTable.Acao = "Importar Pracas";
                vTable.Rotina = "ImportarPraca";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                count = deposito.Categoria.Count();

                vTable.Tabela = "Categoria";
                vTable.Acao = "Importar Categorias";
                vTable.Rotina = "ImportarCategoria";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                count = deposito.Fornecedor.Count();

                vTable.Tabela = "Fornecedor";
                vTable.Acao = "Importar Fornecedores";
                vTable.Rotina = "ImportarFornecedor";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                count = deposito.Produto.Count();

                vTable.Tabela = "Produto";
                vTable.Acao = "Importar Produtos";
                vTable.Rotina = "ImportarProduto";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);


                vTable = new ListaImportacaoExportacao();

                count = deposito.ProdutoGrade.Count();

                vTable.Tabela = "ProdutoGrade";
                vTable.Acao = "Importar Grade de Produtos";
                vTable.Rotina = "ImportarProdutoGrade";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                count = 1;

                vTable.Tabela = "Carga";
                vTable.Acao = "Importar Carga Atual";
                vTable.Rotina = "ImportarCarga";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                count = deposito.Carga.Where(cg => cg.Id != cargaatual.Id && cg.PracaId == cargaatual.PracaId).Count();

                vTable.Tabela = "Carga";
                vTable.Acao = "Importar Carga Anterior";
                vTable.Rotina = "ImportarCargaAnterior";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);


                vTable = new ListaImportacaoExportacao();

                count = deposito.CargaProduto.Where(pg => pg.CargaId == cCargaId).Count();

                vTable.Tabela = "CargaProduto";
                vTable.Acao = "Importar Produtos da Carga";
                vTable.Rotina = "ImportarCargaProduto";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();



                query = @"SELECT * 
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


                count = deposito.Vendedor.SqlQuery(query, cRepresentanteId, cPracaId, cMes.ToString() + cAno.ToString()).Count();

                vTable.Tabela = "Vendedor";
                vTable.Acao = "Importar Vendedores associados a carga/praça";
                vTable.Rotina = "ImportarVendedor";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();


                count = deposito.Vendedor.Count();

                vTable.Tabela = "Vendedor";
                vTable.Acao = "Importar Todos Vendedores";
                vTable.Rotina = "ImportarVendedorBase";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();
                
                count = deposito.Pedido
                        .Join(deposito.Carga, pd => pd.CargaId, ca => ca.Id, (pd, ca) => new { Pedido = pd, Carga = ca })
                        .Where(q => q.Pedido.Status == "1" && q.Carga.PracaId == cPracaId).Count();



                //count = deposito.Pedido.Join(deposito.Carga, pd => pd.CargaId, cg => cg.Id, (pd, cg) => new { Pedido = pd, Carga = cg }).Where(pd => pd.Carga.PracaId == cPracaId && pd.Pedido.DataRetorno == null).Count();

                vTable.Tabela = "Pedido";
                vTable.Acao = "Importar Pedidos";
                vTable.Rotina = "ImportarPedido";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                query = @"SELECT *
                            FROM PedidoItem
                            INNER JOIN Pedido ON PedidoItem.PedidoId = Pedido.Id
							INNER JOIN Carga ON Pedido.CargaId = Carga.Id
                            WHERE 
                                (Pedido.Status = '1'
                                OR Pedido.Status = '0')
								AND PracaId = @p0";


                count = deposito.PedidoItem.SqlQuery(query, cPracaId).Count();

                //count = deposito.PedidoItem.Join(deposito.Pedido, pi => pi.PedidoId, pd => pd.Id, (pi, pd) => new { RepPedidoItem = pi, RepPedido = pd }).Where(pi => pi.RepPedido.CargaId == cCargaId).Count();

                vTable.Tabela = "PedidoItem";
                vTable.Acao = "Importar Itens do Pedidos";
                vTable.Rotina = "ImportarPedidoItem";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                query = @"SELECT * 
                            FROM Receber 
                                WHERE CargaId = @p0 
                                    OR (VendedorId 
                                        IN(
	                                        SELECT Distinct VendedorId
                                            FROM Pedido
                                            WHERE CargaId in(SELECT Id FROM Carga WHERE PracaId = @p1)
                                        ) 
                                        AND DataPagamento IS NULL)";

                count = deposito.Receber.SqlQuery(query, cCargaId, cPracaId).Count();

                vTable.Tabela = "Receber";
                vTable.Acao = "Importar pagamentos a receber";
                vTable.Rotina = "ImportarReceber";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                query = @"SELECT * FROM ReceberBaixa WHERE 
                                       ReceberId IN (
                                            SELECT Id FROM Receber 
                                            WHERE CargaId = @p0 
                                                OR (VendedorId 
                                                    IN(
	                                                    SELECT Distinct VendedorId
                                                        FROM Pedido
                                                        WHERE CargaId in(SELECT Id FROM Carga WHERE PracaId = @p1)
                                                    ) 
                                                    AND DataPagamento IS NULL)
                                        )";

                count = deposito.ReceberBaixa.SqlQuery(query, cCargaId, cPracaId).Count();

                vTable.Tabela = "ReceberBaixa";
                vTable.Acao = "Importar baixa de pagamentos a receber";
                vTable.Rotina = "ImportarReceberBaixa";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);



                vTable = new ListaImportacaoExportacao();


                count = 0;

                vTable.Tabela = "Carga";
                vTable.Acao = "Atualizar Status Carga";
                vTable.Rotina = "AlterarStatusCarga";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();


                count = 0;

                vTable.Tabela = "Todas";
                vTable.Acao = "Finalizar Importacao...";
                vTable.Rotina = "ImportarFinalizar";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                if (cargaatual.Status != "A")
                {
                    result.Clear();
                    vTable = new ListaImportacaoExportacao();
                    vTable.Tabela = "Carga com status: " + cargaatual.Status;
                    vTable.Acao = "A carga selecionada não pode ser importada pois já foi processada.";
                    vTable.Rotina = "NA";
                    vTable.TotalLinhas = 0;
                    vTable.Status = "Processada.";

                    result.Add(vTable);


                }

                return result.ToList<ListaImportacaoExportacao>();


            }


        }

        public static void NA()
        {
            Thread.Sleep(1000);
        }

        // Atualizar Tabela Carga: Data Exportação / Status
        public static Boolean ImportarPreparar()
        {



            //Obter pedidos pendentes da praça e alterar o CargaId para nova Carga.


            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {
                var pedido = deposito.Pedido
                        .Join(deposito.Carga, pd => pd.CargaId, ca => ca.Id, (pd, ca) => new { Pedido = pd, Carga = ca })
                        .Where(q => q.Pedido.Status == "1" && q.Carga.PracaId == cPracaId).ToList();

                pedido.ForEach(pd => pd.Pedido.CargaId = Convert.ToInt32(cCargaId));

                deposito.SaveChanges();


                var receber = deposito.Pedido
                    .Join(deposito.Carga, pd => pd.CargaId, ca => ca.Id, (pd, ca) => new { Pedido = pd, Carga = ca })
                    .Where(pd => pd.Pedido.ValorAcerto > 0 && pd.Carga.PracaId == cPracaId && pd.Pedido.Status != "3" && pd.Pedido.Status != "4")
                    .Select(pd => pd.Pedido);


                foreach (Pedido row in receber)
                {
                    if (row.ValorLiquido - row.ValorAcerto > 0)
                    {
                        ModelLibrary.MetodosDeposito.IncluirReceber(Convert.ToInt32(cCargaId), Convert.ToInt32(row.VendedorId), Convert.ToDouble(row.ValorLiquido + row.ValorAReceber - row.ValorAcerto), Convert.ToDateTime(row.DataPrevisaoRetorno));
                    }
                }
            }

            //Gerar os titulos a receber com base no "ValorAReceber" do Pedido

            Console.WriteLine("Importar Preparar Executado.");
            return true;
        }



        public static Boolean ImportarUsuario()
        {
            try
            {


                //RepUsuario

                cResult = "Importando usuário...<br>";
                Console.WriteLine(cResult);

                var newUsuario = new List<RepUsuario>();
                int count = 0;


                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Usuario)
                    {
                        var newReg = new RepUsuario
                        {
                            Id = row.Id,
                            Nome = row.Nome.Trim(),
                            Login = row.Login.Trim(),
                            Senha = row.Senha.Trim(),
                            TipoModulo = row.TipoModulo,
                            Endereco = row.Endereco,
                            Complemento = row.Complemento,
                            Bairro = row.Bairro,
                            Cidade = row.Cidade,
                            UF = row.UF,
                            Cep = row.Cep,
                            TipoPessoa = row.TipoPessoa,
                            CpfCnpj = row.CpfCnpj,
                            RGInscricao = row.RGInscricao,
                            Telefone = row.Telefone,
                            TelefoneComercial = row.TelefoneComercial,
                            Celular = row.Celular,
                            Email = row.Email,
                            DataInicial = row.DataInicial,
                            DataFinal = row.DataFinal,
                            Comissao = Convert.ToDecimal(row.Comissao),
                            Observacao = row.Observacao
                        };

                        newUsuario.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepUsuario.AddRange(newUsuario);
                    representante.SaveChanges();
                }


                cResult = count.ToString() + " usuario(s) importado(s) <br>";
                Console.WriteLine(cResult);


                return true;
            }
            catch
            {
                return false;
            }

        }


        public static Boolean ImportarPraca()
        {

            try
            {

                int count = 0;
                //RepPraca

                cResult = "Importando praça...<br>";
                Console.WriteLine(cResult);
                var newPraca = new List<RepPraca>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Praca)
                    {
                        var newReg = new RepPraca
                        {
                            Id = row.Id,
                            Descricao = row.Descricao.Trim()
                        };

                        newPraca.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepPraca.AddRange(newPraca);
                    representante.SaveChanges();
                }

                cResult = count.ToString() + " praça(s) importada(s). <br>";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static Boolean ImportarCategoria()
        {

            try
            {

                int count = 0;


                //RepCategoria

                cResult = "Importando categoria...<br>";
                Console.WriteLine(cResult);

                var newCategoria = new List<RepCategoria>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Categoria)
                    {
                        var newReg = new RepCategoria
                        {
                            Id = row.Id,
                            Descricao = row.Descricao.Trim()
                        };

                        newCategoria.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepCategoria.AddRange(newCategoria);
                    representante.SaveChanges();
                }


                cResult = count.ToString() + " categoria(s) importada(s). <br>";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }


        public static Boolean ImportarFornecedor()
        {

            try
            {
                int count = 0;

                //RepFornecedor

                cResult = "Importando fornecedor...<br>";
                Console.WriteLine(cResult);

                var newFornecedor = new List<RepFornecedor>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Fornecedor)
                    {
                        var newReg = new RepFornecedor
                        {
                            Id = row.Id,
                            NomeFantasia = row.NomeFantasia,
                            RazaoSocial = row.RazaoSocial,
                            Endereco = row.Endereco,
                            Complemento = row.Complemento,
                            Bairro = row.Bairro,
                            Cidade = row.Cidade,
                            UF = row.UF,
                            Cep = row.Cep,
                            Telefone = row.Telefone,
                            TelefoneComercial = row.TelefoneComercial,
                            Celular = row.Celular,
                            Email = row.Email,
                            Website = row.Website,
                            TipoPessoa = row.TipoPessoa,
                            CpfCnpj = row.CpfCnpj,
                            RGInscricao = row.RGInscricao,
                            DataCadastro = row.DataCadastro,
                            Status = row.Status,
                            Observacao = row.Observacao
                        };

                        newFornecedor.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepFornecedor.AddRange(newFornecedor);
                    representante.SaveChanges();
                }

                cResult = count.ToString() + " fornecedor(es) importado(s). <br>";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }



        public static Boolean ImportarProduto()
        {

            try
            {
                int count = 0;

                //RepProduto
                cResult = "Importando produto...<br>";
                Console.WriteLine(cResult);

                var newProduto = new List<RepProduto>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Produto)
                    {
                        var newReg = new RepProduto
                        {
                            Id = row.Id,
                            Descricao = row.Descricao,
                            CategoriaId = row.CategoriaId,
                            FornecedorId = row.FornecedorId,
                            Unidade = row.Unidade,
                            CodigoBarras = row.CodigoBarras,
                            Digito = row.Digito,
                            Quantidade = Convert.ToDecimal(row.Quantidade),
                            Status = row.Status
                        };

                        newProduto.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepProduto.AddRange(newProduto);
                    representante.SaveChanges();
                }

                cResult = count.ToString() + " produto(s) importado(s). <br>";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }



        public static Boolean ImportarProdutoGrade()
        {

            try
            {
                int count = 0;

                //RepProdutoGrade

                cResult = "Importando grade de produtos ...<br>";
                Console.WriteLine(cResult);

                var newProdutoGrade = new List<RepProdutoGrade>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.ProdutoGrade)
                    {
                        var newReg = new RepProdutoGrade
                        {
                            Id = row.Id,
                            ProdutoId = row.ProdutoId,
                            Tamanho = row.Tamanho,
                            Cor = row.Cor,
                            ValorSaida = Convert.ToDecimal(row.ValorSaida),
                            DataInicial = row.DataInicial,
                            DataFinal = row.DataFinal,
                            PesoLiquido = Convert.ToDecimal(row.PesoLiquido),
                            PesoBruto = Convert.ToDecimal(row.PesoBruto),
                            CodigoBarras = row.CodigoBarras,
                            Digito = row.Digito,
                            ValorCusto = Convert.ToDecimal(row.ValorCusto),
                            Quantidade = Convert.ToDecimal(row.Quantidade),
                            Status = row.Status
                        };

                        newProdutoGrade.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepProdutoGrade.AddRange(newProdutoGrade);
                    representante.SaveChanges();
                }

                cResult = count.ToString() + " grade(s) importada(s). <br>";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }



        public static Boolean ImportarCarga()
        {

            try
            {

                //RepCarga

                cResult = "Importando carga...<br>";
                Console.WriteLine(cResult);

                List<RepCarga> newCarga = new List<RepCarga>();


                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    var carga = deposito.Carga.FirstOrDefault(C => C.Id == cCargaId);


                    var newReg = new RepCarga
                    {
                        Id = carga.Id,
                        PracaId = carga.PracaId,
                        RepresentanteId = carga.RepresentanteId,
                        Ano = carga.Ano,
                        Mes = carga.Mes,
                        DataAbertura = carga.DataAbertura,
                        DataExportacao = carga.DataExportacao,
                        DataRetorno = carga.DataRetorno,
                        DataConferencia = carga.DataConferencia,
                        DataFinalizacao = carga.DataFinalizacao,
                        Status = carga.Status
                    };

                    newCarga.Add(newReg);


                    cResult = "Carga importada - Praça: " + carga.PracaId.ToString() + " Representante:" + carga.RepresentanteId.ToString() + ". <br>";
                    Console.WriteLine(cResult);


                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepCarga.AddRange(newCarga);
                    representante.SaveChanges();
                }




                return true;
            }
            catch
            {

                return false;

            }
        }


        public static Boolean ImportarCargaAnterior()
        {

            try
            {
                int count = 0;

                //RepCargaAnterior

                cResult = "Importando carga anterior...<br>";
                Console.WriteLine(cResult);

                List<RepCargaAnterior> newCargaAnterior = new List<RepCargaAnterior>();


                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Carga.Where(cg => cg.Id != cCargaId && cg.PracaId == cPracaId))
                    {
                        var newReg = new RepCargaAnterior
                        {

                            Id = row.Id,
                            PracaId = row.PracaId,
                            RepresentanteId = row.RepresentanteId,
                            Ano = row.Ano,
                            Mes = row.Mes,
                            DataAbertura = row.DataAbertura,
                            DataExportacao = row.DataExportacao,
                            DataRetorno = row.DataRetorno,
                            DataConferencia = row.DataConferencia,
                            DataFinalizacao = row.DataFinalizacao,
                            Status = row.Status
                        };

                        newCargaAnterior.Add(newReg);
                        count++;
                    };




                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepCargaAnterior.AddRange(newCargaAnterior);
                    representante.SaveChanges();
                }


                cResult = count.ToString() + " carga(s) anterior(es) importada(s). <br>";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }

        public static Boolean ImportarCargaProduto()
        {

            try
            {
                int count = 0;

                //RepCargaProduto
                cResult = "Importando produto(s) da carga...<br>";
                Console.WriteLine(cResult);
                var newCargaProduto = new List<RepCargaProduto>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.CargaProduto.Where(pg => pg.CargaId == cCargaId))
                    {
                        var newReg = new RepCargaProduto
                        {
                            Id = row.Id,
                            CargaId = row.CargaId,
                            ProdutoGradeId = row.ProdutoGradeId,
                            Quantidade = Convert.ToDecimal(row.Quantidade),
                            Retorno = Convert.ToDecimal(row.Retorno)
                        };

                        newCargaProduto.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepCargaProduto.AddRange(newCargaProduto);
                    representante.SaveChanges();
                }


                cResult += count.ToString() + " produto(s) da carga importado(s).";
                Console.WriteLine(cResult);



                return true;
            }
            catch
            {

                return false;

            }
        }


        public static Boolean ImportarVendedor()
        {

            try
            {
                int count = 0;

                //RepVendedor
                cResult = "Importando vendedor(es) ...<br>";
                Console.WriteLine(cResult);
                var newVendedor = new List<RepVendedor>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {
                    string query = @"SELECT * 
                                        FROM Vendedor 
                                         WHERE                                  
                                            Id IN(
                                                SELECT Distinct VendedorId 
                                                FROM Pedido 
                                                WHERE CargaId in(SELECT Id FROM Carga WHERE RepresentanteId = @p0 and PracaId = @p1 and FORMAT(DataAbertura, 'yyyyMM') <= @p2)
                                                ) 
                                            OR  Id IN (
                                                SELECT Distinct VendedorId 
                                                FROM Receber 
                                                INNER JOIN Carga ON Receber.CargaId = Carga.Id
                                                WHERE PracaId = @p1
                                            )";


                    foreach (var row in deposito.Vendedor.SqlQuery(query, cRepresentanteId, cPracaId, cMes.ToString() + cAno.ToString()))
                    {
                        var newReg = new RepVendedor
                        {
                            Id = row.Id,
                            Nome = row.Nome,
                            RazaoSocial = row.RazaoSocial,
                            Endereco = row.Endereco,
                            Complemento = row.Complemento,
                            Bairro = row.Bairro,
                            Cidade = row.Cidade.Trim(),
                            UF = row.UF.Trim(),
                            Cep = row.Cep,
                            TipoPessoa = row.TipoPessoa,
                            CpfCnpj = row.CpfCnpj,
                            RGInscricao = row.RGInscricao,
                            DataNascimento = row.DataNascimento,
                            Telefone = row.Telefone.Trim(),
                            TelefoneComercial = row.TelefoneComercial,
                            Celular = row.Celular.Trim(),
                            Email = row.Email,
                            DataInicial = row.DataInicial,
                            DataFinal = row.DataFinal,
                            LimitePedido = Convert.ToDecimal(row.LimitePedido),
                            LimiteCredito = Convert.ToDecimal(row.LimiteCredito),
                            Status = row.Status,
                            Observacao = row.Observacao
                        };

                        newVendedor.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepVendedor.AddRange(newVendedor);
                    representante.SaveChanges();
                }


                cResult += count.ToString() + " vendedor(es) importado(s).";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }



        public static Boolean ImportarVendedorBase()
        {

            try
            {
                int count = 0;

                //RepVendedorBase
                cResult = "Importando base total de vendedor(es) ...<br>";
                Console.WriteLine(cResult);
                var newVendedorBase = new List<RepVendedorBase>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Vendedor)
                    {
                        var newReg = new RepVendedorBase
                        {
                            Id = row.Id,
                            Nome = row.Nome,
                            RazaoSocial = row.RazaoSocial,
                            Endereco = row.Endereco,
                            Complemento = row.Complemento,
                            Bairro = row.Bairro,
                            Cidade = row.Cidade,
                            UF = row.UF,
                            Cep = row.Cep,
                            TipoPessoa = row.TipoPessoa,
                            CpfCnpj = row.CpfCnpj,
                            RGInscricao = row.RGInscricao,
                            DataNascimento = row.DataNascimento,
                            Telefone = row.Telefone,
                            TelefoneComercial = row.TelefoneComercial,
                            Celular = row.Celular,
                            Email = row.Email,
                            DataInicial = row.DataInicial,
                            DataFinal = row.DataFinal,
                            LimitePedido = Convert.ToDecimal(row.LimitePedido),
                            LimiteCredito = Convert.ToDecimal(row.LimiteCredito),
                            Status = row.Status,
                            Observacao = row.Observacao
                        };

                        newVendedorBase.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepVendedorBase.AddRange(newVendedorBase);
                    representante.SaveChanges();
                }


                cResult += count.ToString() + " vendedor(es) da base total importado(s).";
                Console.WriteLine(cResult);


                return true;
            }
            catch
            {

                return false;

            }
        }


        public static Boolean ImportarPedido()
        {

            try
            {
                int count = 0;

                //RepPedido

                cResult = "Importando pedido(s) ...<br>";
                Console.WriteLine(cResult);
                var newPedido = new List<RepPedido>();

                //int vPedidoId;

                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {


                    foreach (var row in deposito.Pedido.Where(pd => pd.CargaId == cCargaId))
                    //foreach (var row in deposito.Pedido.SqlQuery(query, cCargaId, cPracaId))
                    {
                        var newReg = new RepPedido
                        {
                            Id = row.Id,
                            VendedorId = row.VendedorId,
                            CargaId = row.CargaId,
                            CargaOriginal = row.CargaOriginal,
                            RepresentanteId = row.RepresentanteId,
                            CodigoPedido = row.CodigoPedido,
                            DataLancamento = row.DataLancamento,
                            DataPrevisaoRetorno = row.DataPrevisaoRetorno,
                            DataRetorno = row.DataRetorno,
                            ValorPedido = Convert.ToDecimal(row.ValorPedido),
                            ValorCompra = Convert.ToDecimal(row.ValorCompra),
                            PercentualCompra = Convert.ToDecimal(row.PercentualCompra),
                            FaixaComissao = Convert.ToDecimal(row.FaixaComissao),
                            PercentualFaixa = Convert.ToDecimal(row.PercentualFaixa),
                            ValorComissao = Convert.ToDecimal(row.ValorComissao),
                            ValorLiquido = Convert.ToDecimal(row.ValorLiquido),
                            ValorAReceber = Convert.ToDecimal(row.ValorAReceber),
                            ValorAcerto = Convert.ToDecimal(row.ValorAcerto),
                            QuantidadeRemarcado = row.QuantidadeRemarcado,
                            Remarcado = row.Remarcado,
                            Status = row.Status
                        };

                        newPedido.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepPedido.AddRange(newPedido);
                    representante.SaveChanges();
                }


                cResult = count.ToString() + " pedido(s) importado(s).";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }



        public static Boolean ImportarPedidoItem()
        {

            try
            {
                int count = 0;

                //RepPedidoItem
                cResult = "Importando item(s) do(s) pedido(s) ...<br>";
                Console.WriteLine(cResult);
                var newPedidoItem = new List<RepPedidoItem>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {


                    foreach (var row in deposito.PedidoItem.Join(deposito.Pedido, pi => pi.PedidoId, pd => pd.Id, (pi, pd) => new { RepPedidoItem = pi, RepPedido = pd }).Where(pi => pi.RepPedido.CargaId == cCargaId))
                    {
                        var newReg = new RepPedidoItem
                        {
                            Id = row.RepPedidoItem.Id,
                            PedidoId = row.RepPedidoItem.PedidoId,
                            ProdutoGradeId = row.RepPedidoItem.ProdutoGradeId,
                            Quantidade = Convert.ToDecimal(row.RepPedidoItem.Quantidade),
                            Retorno = Convert.ToDecimal(row.RepPedidoItem.Retorno),
                            Preco = Convert.ToDecimal(row.RepPedidoItem.Preco)
                        };

                        newPedidoItem.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepPedidoItem.AddRange(newPedidoItem);
                    representante.SaveChanges();
                }


                cResult = count.ToString() + " item(s) do(s) pedido(s) importado(s).";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }




        public static Boolean ImportarReceber()
        {

            try
            {
                int count = 0;

                //Receber
                cResult = "Importando pagamento(s) a receber ...<br>";
                Console.WriteLine(cResult);

                var newReceber = new List<RepReceber>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {


                    string query = @"SELECT * 
                                        FROM Receber 
                                            WHERE CargaId = @p0 
                                                OR (VendedorId 
                                                    IN(
	                                                    SELECT Distinct VendedorId
                                                        FROM Pedido
                                                        WHERE CargaId in(SELECT Id FROM Carga WHERE PracaId = @p1)
                                                    ) 
                                                    AND DataPagamento IS NULL)";


                    foreach (var row in deposito.Receber.SqlQuery(query, cCargaId, cPracaId))
                    {
                        var newReg = new RepReceber
                        {
                            Id = row.Id,
                            VendedorId = row.VendedorId,
                            CargaId = row.CargaId,
                            Documento = row.Documento,
                            Serie = row.Serie,
                            ValorNF = Convert.ToDecimal(row.ValorNF),
                            ValorDuplicata = Convert.ToDecimal(row.ValorDuplicata),
                            ValorAReceber = Convert.ToDecimal(row.ValorAReceber),
                            DataEmissao = row.DataEmissao,
                            DataLancamento = row.DataLancamento,
                            DataPagamento = row.DataPagamento,
                            DataVencimento = row.DataVencimento,
                            Observacoes = row.Observacoes,
                            Status = row.Status

                        };

                        newReceber.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepReceber.AddRange(newReceber);
                    representante.SaveChanges();
                }


                cResult = count.ToString() + " pagamento(s) a receber importado(s).";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }



        public static Boolean ImportarReceberBaixa()
        {

            try
            {
                int count = 0;

                //ReceberBaixa
                cResult = "Importando baixa(s) do pagamento a receber ...<br>";
                Console.WriteLine(cResult);

                var newReceberBaixa = new List<RepReceberBaixa>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {


                    string query = @"SELECT * FROM ReceberBaixa WHERE 
                                       ReceberId IN (
                                            SELECT Id FROM Receber 
                                            WHERE CargaId = @p0 
                                                OR (VendedorId 
                                                    IN(
	                                                    SELECT Distinct VendedorId
                                                        FROM Pedido
                                                        WHERE CargaId in(SELECT Id FROM Carga WHERE PracaId = @p1)
                                                    ) 
                                                    AND DataPagamento IS NULL)
                                        )";


                    foreach (var row in deposito.ReceberBaixa.SqlQuery(query, cCargaId, cPracaId))
                    {
                        var newReg = new RepReceberBaixa
                        {
                            Id = row.Id,
                            ReceberId = row.ReceberId,
                            CargaId = row.CargaId,
                            Valor = Convert.ToDecimal(row.Valor),
                            DataPagamento = row.DataPagamento,
                            DataBaixa = row.DataBaixa
                        };

                        newReceberBaixa.Add(newReg);
                        count++;
                    }

                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepReceberBaixa.AddRange(newReceberBaixa);
                    representante.SaveChanges();
                }


                cResult = count.ToString() + " baixa(s) de pagamento a receber importado(s).";
                Console.WriteLine(cResult);

                return true;
            }
            catch
            {

                return false;

            }
        }


        public static Boolean AlterarStatusCarga()
        {
            try
            {
                ModelLibrary.MetodosDeposito.AlterarStatusCarga(cCargaId, "E");
                ModelLibrary.MetodosRepresentante.AlterarStatusCarga(cCargaId, "E");                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void ImportarFinalizar()
        {
            Thread.Sleep(1000);
        }





        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        ////////// EXCLUIR IMPORTACAO                                          ///////////
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////

        public static void ExcluirImportacao()
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                representante.Database.ExecuteSqlCommand("delete from RepCategoria");
                representante.Database.ExecuteSqlCommand("delete from RepFornecedor");
                representante.Database.ExecuteSqlCommand("delete from RepPraca");
                representante.Database.ExecuteSqlCommand("delete from RepCargaProduto");
                representante.Database.ExecuteSqlCommand("delete from RepProdutoGrade");
                representante.Database.ExecuteSqlCommand("delete from RepProduto");
                representante.Database.ExecuteSqlCommand("delete from RepCarga");
                representante.Database.ExecuteSqlCommand("delete from RepCargaAnterior");
                representante.Database.ExecuteSqlCommand("delete from RepCargaConferencia");
                representante.Database.ExecuteSqlCommand("delete from RepUsuario");
                representante.Database.ExecuteSqlCommand("delete from RepPedidoItem");
                representante.Database.ExecuteSqlCommand("delete from RepPedido");
                representante.Database.ExecuteSqlCommand("delete from RepVendedor");
                representante.Database.ExecuteSqlCommand("delete from RepVendedorBase");
                representante.Database.ExecuteSqlCommand("delete from RepReceber");
                representante.Database.ExecuteSqlCommand("delete from RepReceberBaixa");
                representante.Database.ExecuteSqlCommand("delete from RepSuplemento");


            }
        }


        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        ////////// EXPORTAÇÃO                                                  ///////////
        //////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////


        public static List<ListaImportacaoExportacao> ObterListaExportacao(long pCargaId)
        {

            cCargaId = pCargaId;

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {


                var result = new List<ListaImportacaoExportacao>();

                var vTable = new ListaImportacaoExportacao();

                int count = 0;

                vTable.Tabela = "Todas";
                vTable.Acao = "Preparar Exportacao";
                vTable.Rotina = "ExportarPreparar";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);


                vTable = new ListaImportacaoExportacao();


                count = representante.RepVendedor.Count();

                vTable.Tabela = "Vendedor";
                vTable.Acao = "Exportar Vendedor...";
                vTable.Rotina = "ExportarVendedor";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                count = 1;

                vTable = new ListaImportacaoExportacao();

                vTable.Tabela = "Carga";
                vTable.Acao = "Exportar Carga";
                vTable.Rotina = "ExportarCarga";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);


                string query = "SELECT DISTINCT 0 Id, " + cCargaId.ToString() + " CargaId, ProdutoGradeId, 0 Quantidade, 0 Retorno FROM RepPedidoItem WHERE ProdutoGradeId NOT IN (SELECT ProdutoGradeId FROM RepCargaProduto)";
                count = representante.Database.SqlQuery<RepCargaProduto>(query).Count();
                count += representante.RepCargaProduto.Count();

                vTable = new ListaImportacaoExportacao();

                vTable.Tabela = "CargaProduto";
                vTable.Acao = "Exportar Produto da Carga.";
                vTable.Rotina = "ExportarCargaProduto";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);


                vTable = new ListaImportacaoExportacao();

                count = representante.RepPedido.Where(rp => rp.CargaId == pCargaId).Count();

                vTable.Tabela = "Pedido";
                vTable.Acao = "Exportar Pedido(s)";
                vTable.Rotina = "ExportarPedido";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();


                count = representante.RepPedidoItem.Join(representante.RepPedido, pi => pi.PedidoId, pd => pd.Id, (pi, pd) => new { RepPedidoItem = pi, RepPedido = pd }).Where(pd => pd.RepPedido.CargaId == pCargaId).Count();

                vTable.Tabela = "PedidoItem";
                vTable.Acao = "Atualizar Item(s) do(s) Pedido(s)";
                vTable.Rotina = "ExportarPedidoItem";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);


                vTable = new ListaImportacaoExportacao();


                count = representante.RepReceber.Count();

                vTable.Tabela = "Receber";
                vTable.Acao = "Exportar Contas a Receber";
                vTable.Rotina = "ExportarReceber";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                vTable = new ListaImportacaoExportacao();

                count = representante.RepReceberBaixa.Where(rb => rb.CargaId != pCargaId).Count();

                vTable.Tabela = "ReceberBaixa";
                vTable.Acao = "Exportar Baixa de Contas a Receber";
                vTable.Rotina = "ExportarReceberBaixa";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);



                vTable = new ListaImportacaoExportacao();


                count = 0;

                vTable.Tabela = "Todas";
                vTable.Acao = "Finalizar Exportacao...";
                vTable.Rotina = "ExportarFinalizar";
                vTable.TotalLinhas = count;
                vTable.Status = "Preparando...";

                result.Add(vTable);

                return result.ToList<ListaImportacaoExportacao>();

            }





        }



        // Atualizar Tabela Carga: Data Exportação / Status
        public static Boolean ExportarPreparar()
        {
            ModelLibrary.MetodosRepresentante.LimparPedidoVazio();
            Thread.Sleep(1000);
            return true;
        }


        public static Boolean ExportarVendedor()
        {
            try
            {
                cResult = "Exportando vendedor(es) ...<br>";
                Console.WriteLine(cResult);


                int count = 0;
                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {

                    foreach (var row in representante.RepVendedor)
                    {


                        var regVendedor = new Vendedor
                        {
                            Id = Convert.ToInt32(row.Id),
                            Nome = row.Nome,
                            RazaoSocial = row.RazaoSocial,
                            Endereco = row.Endereco,
                            Complemento = row.Complemento,
                            Bairro = row.Bairro,
                            Cidade = row.Cidade,
                            UF = row.UF,
                            Cep = row.Cep,
                            TipoPessoa = row.TipoPessoa,
                            CpfCnpj = row.CpfCnpj,
                            RGInscricao = row.RGInscricao,
                            DataNascimento = row.DataNascimento,
                            Telefone = row.Telefone,
                            TelefoneComercial = row.TelefoneComercial,
                            Celular = row.Celular,
                            Email = row.Email,
                            DataInicial = row.DataInicial,
                            DataFinal = row.DataFinal,
                            LimitePedido = Convert.ToDouble(row.LimitePedido),
                            LimiteCredito = Convert.ToDouble(row.LimiteCredito),
                            Status = row.Status,
                            Observacao = row.Observacao,
                            temp_old_id = Convert.ToInt32(row.Id)
                        };

                        VendedorAtualizarInserir(regVendedor);
                        count++;
                    }

                }


                cResult = count.ToString() + " vendedor(es) exportado(s).";
                Console.WriteLine(cResult);


                return true;
            }
            catch
            {
                return false;
            }

        }

        public static void VendedorAtualizarInserir(Vendedor pVendedor)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vVendedor = deposito.Vendedor.FirstOrDefault(rc => rc.Id == pVendedor.Id);

                if (vVendedor != null)
                {

                    Console.WriteLine("Atualizando Vendedor id: " + pVendedor.Id.ToString());

                    vVendedor.Nome = pVendedor.Nome;
                    vVendedor.RazaoSocial = pVendedor.RazaoSocial;
                    vVendedor.Endereco = pVendedor.Endereco;
                    vVendedor.Complemento = pVendedor.Complemento;
                    vVendedor.Bairro = pVendedor.Bairro;
                    vVendedor.Cidade = pVendedor.Cidade;
                    vVendedor.UF = pVendedor.UF;
                    vVendedor.Cep = pVendedor.Cep;
                    vVendedor.TipoPessoa = pVendedor.TipoPessoa;
                    vVendedor.CpfCnpj = pVendedor.CpfCnpj;
                    vVendedor.RGInscricao = pVendedor.RGInscricao;
                    vVendedor.DataNascimento = pVendedor.DataNascimento;
                    vVendedor.Telefone = pVendedor.Telefone;
                    vVendedor.TelefoneComercial = pVendedor.TelefoneComercial;
                    vVendedor.Celular = pVendedor.Celular;
                    vVendedor.Email = pVendedor.Email;
                    vVendedor.DataInicial = pVendedor.DataInicial;
                    vVendedor.DataFinal = pVendedor.DataFinal;
                    vVendedor.LimitePedido = Convert.ToDouble(pVendedor.LimitePedido);
                    vVendedor.LimiteCredito = Convert.ToDouble(pVendedor.LimiteCredito);
                    vVendedor.Status = "0";
                    vVendedor.Observacao = pVendedor.Observacao;

                    deposito.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Inserindo Vendedor id: " + pVendedor.Id.ToString());
                    deposito.Vendedor.Add(pVendedor);
                    deposito.SaveChanges();
                }

            }

        }


        // Atualizar Tabela Carga: Data Exportação / Status
        public static Boolean ExportarCarga()
        {
            try
            {
                


                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {

                    foreach (var row in representante.RepCarga)
                    {


                        var regCarga = new Carga
                        {
                            Id = Convert.ToInt32(row.Id),
                            PracaId = Convert.ToInt32(row.PracaId),
                            RepresentanteId = Convert.ToInt32(row.RepresentanteId),
                            Mes = Convert.ToInt32(row.Mes),
                            Ano = Convert.ToInt32(row.Ano),
                            DataAbertura = row.DataAbertura,
                            DataExportacao = row.DataExportacao,
                            DataRetorno = row.DataRetorno,
                            DataConferencia = row.DataConferencia,
                            DataFinalizacao = row.DataFinalizacao,
                            Status = row.Status
                        };

                        CargaAtualizar(regCarga);
                        
                    }

                }

                Console.WriteLine("Carga Alterada: " + cCargaId);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public static void CargaAtualizar(Carga pCarga)
        {
            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vCarga = deposito.Carga.FirstOrDefault(cg => cg.Id == pCarga.Id);

                if (vCarga != null)
                {

                    Console.WriteLine("Atualizando Carga id: " + pCarga.Id.ToString());

                    vCarga.DataExportacao = pCarga.DataExportacao;
                    vCarga.Status = pCarga.Status;

                    deposito.SaveChanges();

                }

            }
        }



        // Inerir na Carga Produtos que estão em PedidoItem porém não estão em CargaProduto
        public static Boolean ExportarCargaProduto()
        {


            try
            {
                cResult = "Inserindo na Carga Produtos que estão em PedidoItem porém não estão em CargaProduto Id: " + cCargaId;
                Console.WriteLine(cResult);


                int count = 0;
                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {

                    string query = "SELECT DISTINCT 0 Id, " + cCargaId.ToString() + " CargaId, ProdutoGradeId, 0 Quantidade, 0 Retorno FROM RepPedidoItem WHERE ProdutoGradeId NOT IN (SELECT ProdutoGradeId FROM RepCargaProduto)";

                    foreach (var row in representante.Database.SqlQuery<RepCargaProduto>(query))
                    {


                        var maxcargaproduto = representante.RepCargaProduto.OrderByDescending(i => i.Id).FirstOrDefault();

                        long newId = maxcargaproduto == null ? 1 : maxcargaproduto.Id + 1;

                        var regRepCargaProduto = new RepCargaProduto
                        {
                            Id = newId,
                            CargaId = Convert.ToInt32(cCargaId),
                            ProdutoGradeId = Convert.ToInt32(row.ProdutoGradeId),
                            Quantidade = 0,
                            Retorno = 0
                        };

                        representante.RepCargaProduto.Add(regRepCargaProduto);

                    }


                    foreach(var row in representante.RepCargaProduto)
                    {

                        var regCargaProduto = new CargaProduto
                        {
                            CargaId = Convert.ToInt32(cCargaId),
                            ProdutoGradeId = Convert.ToInt32(row.ProdutoGradeId),
                            Quantidade = Convert.ToDouble(row.Quantidade),
                            Retorno = Convert.ToDouble(row.Retorno)
                        };




                        representante.SaveChanges();

                        CargaProdutoAtualizarInserir(regCargaProduto);
                        count++;
                    }

                }


                cResult = count.ToString() + " produtos(s) exportado(s).";
                Console.WriteLine(cResult);


                return true;
            }
            catch
            {
                return false;
            }



        }

        public static void CargaProdutoAtualizarInserir(CargaProduto pCargaProduto)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {


                var vCargaProduto = deposito.CargaProduto.FirstOrDefault(cp => cp.CargaId == pCargaProduto.CargaId && cp.ProdutoGradeId == pCargaProduto.ProdutoGradeId);

                if (vCargaProduto != null)
                {

                    Console.WriteLine("Atualizando Produto Grade id: " + pCargaProduto.ProdutoGradeId.ToString());

                    vCargaProduto.Quantidade = pCargaProduto.Quantidade;
                    vCargaProduto.Retorno = pCargaProduto.Retorno;


                    deposito.SaveChanges();

                }
                else
                { 
                    Console.WriteLine("Inserindo Produto Grade id: " + pCargaProduto.ProdutoGradeId.ToString());

                    deposito.CargaProduto.Add(pCargaProduto);
                    deposito.SaveChanges();
                }

            }

        }

        // Atualizar Pedido QuantidadeRetorno / Remarcado / Status
        public static Boolean ExportarPedido()
        {


            try
            {
                cResult = "Exportando pedido(s) da carga: " + cCargaId;
                Console.WriteLine(cResult);


                int count = 0;
                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {


                    foreach (var row in representante.RepPedido)
                    {


                        var regPedido = new Pedido
                        {
                            Id = Convert.ToInt32(row.Id),
                            VendedorId = Convert.ToInt32(row.VendedorId),
                            CargaId = Convert.ToInt32(row.CargaId),
                            CargaOriginal = Convert.ToInt32(row.CargaOriginal),
                            RepresentanteId = Convert.ToInt32(row.RepresentanteId),
                            CodigoPedido = row.CodigoPedido,
                            DataLancamento = row.DataLancamento,
                            DataPrevisaoRetorno = row.DataPrevisaoRetorno,
                            DataRetorno = row.DataRetorno,
                            ValorPedido = Convert.ToDouble(row.ValorPedido),
                            ValorCompra = Convert.ToDouble(row.ValorCompra),
                            PercentualCompra = Convert.ToDouble(row.PercentualCompra),
                            FaixaComissao = Convert.ToDouble(row.FaixaComissao),
                            PercentualFaixa = Convert.ToDouble(row.PercentualFaixa),
                            ValorComissao = Convert.ToDouble(row.ValorComissao),
                            ValorLiquido = Convert.ToDouble(row.ValorLiquido),
                            ValorAReceber = Convert.ToDouble(row.ValorAReceber),
                            ValorAcerto = row.ValorAcerto == null ? 0 : Convert.ToDouble(row.ValorAcerto),
                            QuantidadeRemarcado = Convert.ToInt32(row.QuantidadeRemarcado),
                            Remarcado = Convert.ToInt32(row.Remarcado),
                            Status = row.Status
                        };


                        PedidoAtualizarInserir(regPedido);
                        count++;
                    }

                }


                cResult = count.ToString() + " pedido(s) exportado(s).";
                Console.WriteLine(cResult);


                return true;
        }
            catch
            {
                return false;
            }



        }

        public static void PedidoAtualizarInserir(Pedido pPedido)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vPedido = deposito.Pedido.FirstOrDefault(pd => pd.CodigoPedido == pPedido.CodigoPedido);

                if (vPedido != null)
                {

                    Console.WriteLine("Atualizando pedido id: " + pPedido.Id.ToString() + " codigo: " + pPedido.CodigoPedido);

                    vPedido.DataRetorno = pPedido.DataRetorno;
                    vPedido.ValorPedido = pPedido.ValorPedido;
                    vPedido.ValorCompra = pPedido.ValorCompra;
                    vPedido.PercentualCompra = pPedido.PercentualCompra;
                    vPedido.FaixaComissao = pPedido.FaixaComissao;
                    vPedido.PercentualFaixa = pPedido.PercentualFaixa;
                    vPedido.ValorComissao = pPedido.ValorComissao;
                    vPedido.ValorLiquido = pPedido.ValorLiquido;
                    vPedido.ValorAReceber = pPedido.ValorAReceber;
                    vPedido.ValorAcerto = pPedido.ValorAcerto;
                    vPedido.QuantidadeRemarcado = pPedido.QuantidadeRemarcado;
                    vPedido.Remarcado = pPedido.Remarcado;
                    vPedido.Status = pPedido.Status;

                    deposito.SaveChanges();

                }
                else
                {

                    Console.WriteLine("Inserindo pedido id: " + pPedido.Id.ToString() + " codigo: " + pPedido.CodigoPedido);


                    string vendedorid = pPedido.VendedorId.ToString();
                    vendedorid = vendedorid.PadLeft(6, '0');


                    if (vendedorid.Substring(0, 3) == "999")
                    {
                        var vendedor = deposito.Vendedor.Where(vd => vd.temp_old_id == pPedido.VendedorId).FirstOrDefault();
                        pPedido.VendedorId = vendedor.Id;
                    }


                    deposito.Pedido.Add(pPedido);
                    deposito.SaveChanges();
                }

            }

        }

        // Atualizar PedidoItem / Retorno / Preço
        public static Boolean ExportarPedidoItem()
        {

            try
            {
                cResult = "Exportando item(ns) do(s) pedido(s) ...<br>";
                Console.WriteLine(cResult);


                int count = 0;
                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {

                    foreach (var row in representante.RepPedidoItem.Join(representante.RepPedido, pi => pi.PedidoId, pd => pd.Id, (pi, pd) => new { RepPedidoItem = pi, RepPedido = pd }).Where(pd => pd.RepPedido.CargaId == cCargaId))
                    {

                        var regPedidoItem = new PedidoItem
                        {
                            Id = Convert.ToInt32(row.RepPedidoItem.Id),
                            PedidoId = Convert.ToInt32(row.RepPedidoItem.PedidoId),
                            ProdutoGradeId = Convert.ToInt32(row.RepPedidoItem.ProdutoGradeId),
                            Quantidade = Convert.ToDouble(row.RepPedidoItem.Quantidade),
                            Retorno = Convert.ToDouble(row.RepPedidoItem.Retorno),
                            Preco = Convert.ToDouble(row.RepPedidoItem.Preco)
                        };

                        PedidoItemAtualizarInserir(regPedidoItem, row.RepPedido.CodigoPedido);
                        count++;
                    }

                }


                cResult = count.ToString() + " item(s) do (s) pedido(s) exportado(s).";
                Console.WriteLine(cResult);




                return true;
            }
            catch
            {
                return false;
            }

        }


        public static void PedidoItemAtualizarInserir(PedidoItem pPedidoItem, string pCodigoPedido)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vPedidoItem = deposito.PedidoItem.Join(deposito.Pedido, pi => pi.PedidoId, pd => pd.Id, (pi, pd) => new { PedidoItem = pi, Pedido = pd }).Where(pd => pd.Pedido.CodigoPedido == pCodigoPedido && pd.PedidoItem.Id == pPedidoItem.Id ).FirstOrDefault();

                if (vPedidoItem != null)
                {

                    Console.WriteLine("Atualizando PedidoItem id: " + pPedidoItem.Id.ToString() + " PedidoId: " + pPedidoItem.PedidoId.ToString());
                    vPedidoItem.PedidoItem.Retorno = pPedidoItem.Retorno;
                    vPedidoItem.PedidoItem.Preco = pPedidoItem.Preco;


                    deposito.SaveChanges();

                }
                else
                {

                    Console.WriteLine("Inserindo PedidoItem CodigoPedido: " + pPedidoItem.PedidoId.ToString());

                    var pedido = deposito.Pedido.Where(pd => pd.CodigoPedido == pCodigoPedido).FirstOrDefault();

                    

                    pPedidoItem.PedidoId = pedido.Id;
                    
                    deposito.PedidoItem.Add(pPedidoItem);
                    deposito.SaveChanges();
                    
                }




            }

        }



        // Atualizar Receber - DataPagamento / Status
        public static Boolean ExportarReceber()
        {
            try
            {
                cResult = "Exportando Conta(s) a Receber ...<br>";
                Console.WriteLine(cResult);


                int count = 0;
                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {

                    foreach (var row in representante.RepReceber)
                    {


                        var regReceber = new Receber
                        {
                            Id = Convert.ToInt32(row.Id),
                            VendedorId = Convert.ToInt32(row.VendedorId),
                            CargaId = Convert.ToInt32(row.CargaId),
                            Documento = Convert.ToInt32(row.Documento),
                            Serie = row.Serie,
                            ValorNF = Convert.ToDouble(row.ValorNF),
                            ValorDuplicata = Convert.ToDouble(row.ValorDuplicata),
                            ValorAReceber = Convert.ToDouble(row.ValorAReceber),
                            DataEmissao = row.DataEmissao,
                            DataLancamento = row.DataLancamento,
                            DataPagamento = row.DataPagamento,
                            DataVencimento = row.DataVencimento,
                            Observacoes = row.Observacoes
                        };

                        ReceberAtualizarInserir(regReceber);
                        count++;
                    }

                }


                cResult = count.ToString() + " Conta(s) a receber exportado(s).";
                Console.WriteLine(cResult);


                return true;
            }
            catch
            {
                return false;
            }

        }


        public static void ReceberAtualizarInserir(Receber pReceber)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vReceber = deposito.Receber.FirstOrDefault(rc => rc.Id == pReceber.Id);

                if (vReceber != null)
                {

                    Console.WriteLine("Atualizando Conta a Receber id: " + pReceber.Id.ToString());
                    vReceber.DataPagamento = pReceber.DataPagamento;
                    //vReceber.Status = pReceber.Status;

                    deposito.SaveChanges();

                }
                else
                {

                    var vendedor = deposito.Vendedor.Where(vd => vd.temp_old_id == pReceber.VendedorId).FirstOrDefault();

                    pReceber.VendedorId = vendedor.Id;


                    Console.WriteLine("Inserindo Conta a Receber id: " + pReceber.Id.ToString());
                    deposito.Receber.Add(pReceber);
                    deposito.SaveChanges();
                }

            }

        }

        // Atualizar ReceberBaixa / DataPagamento
        public static Boolean ExportarReceberBaixa()
        {
            try
            {
                cResult = "Exportando baixa(s) de conta a receber...<br>";
                Console.WriteLine(cResult);


                int count = 0;
                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {

                    foreach (var row in representante.RepReceberBaixa)
                    {


                        var regReceberBaixa = new ReceberBaixa
                        {
                            Id = Convert.ToInt32(row.Id),
                            ReceberId = Convert.ToInt32(row.ReceberId),
                            CargaId = Convert.ToInt32(row.CargaId),
                            Valor = Convert.ToDouble(row.Valor),
                            DataPagamento = row.DataPagamento,
                            DataBaixa = row.DataBaixa
                        };

                        ReceberBaixaAtualizarInserir(regReceberBaixa);
                        count++;
                    }

                }


                cResult = count.ToString() + " baixa(s) de contas a receber exportado(s).";
                Console.WriteLine(cResult);


                return true;
            }
            catch
            {
                return false;
            }

        }


        public static void ReceberBaixaAtualizarInserir(ReceberBaixa pReceberBaixa)
        {

            using (DepositoDBEntities deposito = new DepositoDBEntities())
            {

                var vReceberBaixa = deposito.ReceberBaixa.FirstOrDefault(rc => rc.Id == pReceberBaixa.Id);

                if (vReceberBaixa != null)
                {

                    Console.WriteLine("Atualizando Baixa de Conta a Receber id: " + pReceberBaixa.Id.ToString());
                    vReceberBaixa.DataPagamento = pReceberBaixa.DataPagamento;

                    deposito.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Inserindo Baixa de Conta a Receber id: " + pReceberBaixa.Id.ToString());
                    deposito.ReceberBaixa.Add(pReceberBaixa);
                    deposito.SaveChanges();
                }

            }

        }


        

        public static Boolean ExportarFinalizar()
        {
            /// Verificar se a tabela Carga precisa ser atualizada.
            /// 
            try
            {
                ModelLibrary.MetodosDeposito.AlterarStatusCarga(cCargaId, "R");
                ModelLibrary.MetodosRepresentante.AlterarStatusCarga(cCargaId, "R");

                
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {
                    string query = @"UPDATE Vendedor Set temp_old_id = id WHERE left(temp_old_id,3) = '999' and len(temp_old_id) > 5";
                    deposito.Database.ExecuteSqlCommand(query);
                }

                return true;
            } catch
            {
                return false;
            }
            
        }

        //Tratar a tabela Suplemento - aguardando definição no Trello.



            

    }
}