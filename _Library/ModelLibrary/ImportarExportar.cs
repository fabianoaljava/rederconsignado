using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace ModelLibrary
{
    public class ImportarExportar
    {
        public static string cResult; 

        public static Boolean Importar(long pRepresentanteId, long pPracaId, int pMes, int pAno)
        {


            try
            {
                //RepUsuario

                cResult = "Importando usuário...<br>";
                Console.WriteLine(cResult);

                var newUsuario = new List<RepUsuario>();
                int count = 0;

                /*using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {*/


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

                //RepCarga

                cResult = "Importando carga...<br>";
                Console.WriteLine(cResult);

                List<RepCarga> newCarga = new List<RepCarga>();

                int vCargaId;

                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    var carga = deposito.Carga.FirstOrDefault(r => r.RepresentanteId == pRepresentanteId && r.PracaId == pPracaId && r.Mes == pMes && r.Ano == pAno);

                    vCargaId = carga.Id;
                    

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


                }

                using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
                {
                    representante.RepCarga.AddRange(newCarga);
                    representante.SaveChanges();
                }


                cResult = "Carga importada - Praça: " + pPracaId.ToString() + " Representante:" + pRepresentanteId.ToString() + ". <br>";
                Console.WriteLine(cResult);




                //RepCargaAnterior

                cResult = "Importando carga anterior...<br>";
                Console.WriteLine(cResult);

                List<RepCargaAnterior> newCargaAnterior = new List<RepCargaAnterior>();


                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.Carga.Where(cg => cg.Id != vCargaId && cg.RepresentanteId == pRepresentanteId && cg.PracaId == pPracaId))
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



                //RepCargaProduto
                cResult = "Importando produto(s) da carga...<br>";
                Console.WriteLine(cResult);
                var newCargaProduto = new List<RepCargaProduto>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.CargaProduto.Where(pg => pg.CargaId == vCargaId))
                    {
                        var newReg = new RepCargaProduto
                        {
                            Id = row.Id,
                            CargaId = row.CargaId,
                            ProdutoGradeId = row.ProdutoGradeId,
                            Quantidade = Convert.ToDecimal(row.Quantidade),
                            QuantidadeRetorno = Convert.ToDecimal(row.QuantidadeRetorno)
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


                    foreach (var row in deposito.Vendedor.SqlQuery( query , pRepresentanteId, pPracaId, pMes.ToString()+pAno.ToString()))
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




                //RepPedido

                cResult = "Importando pedido(s) ...<br>";
                Console.WriteLine(cResult);
                var newPedido = new List<RepPedido>();

                int vPedidoId;

                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {



                    var pedido = deposito.Pedido.FirstOrDefault(pd => pd.CargaId == vCargaId);

                    if (pedido != null) vPedidoId = pedido.Id; else vPedidoId = 0;

                    foreach (var row in deposito.Pedido.Join(deposito.Carga, pd => pd.CargaId, cg => cg.Id, (pd, cg) => new { Pedido = pd, Carga = cg }).Where(pd => pd.Carga.PracaId == pPracaId))
                    {
                        var newReg = new RepPedido
                        {
                            Id = row.Pedido.Id,
                            VendedorId = row.Pedido.VendedorId,
                            CargaId = row.Pedido.CargaId,
                            CodigoPedido = row.Pedido.CodigoPedido,
                            DataLancamento = row.Pedido.DataLancamento,
                            DataPrevisaoRetorno = row.Pedido.DataPrevisaoRetorno,
                            DataRetorno = row.Pedido.DataRetorno,
                            ValorPedido = Convert.ToDecimal(row.Pedido.ValorPedido),
                            ValorCompra = Convert.ToDecimal(row.Pedido.ValorCompra),
                            PercentualCompra = Convert.ToDecimal(row.Pedido.PercentualCompra),
                            FaixaComissao = Convert.ToDecimal(row.Pedido.FaixaComissao),
                            PercentualFaixa = Convert.ToDecimal(row.Pedido.PercentualFaixa),
                            ValorComissao = Convert.ToDecimal(row.Pedido.ValorComissao),                            
                            ValorLiquido = Convert.ToDecimal(row.Pedido.ValorLiquido),
                            RecebidoAnterior = Convert.ToDecimal(row.Pedido.RecebidoAnterior),                            
                            ValorAcerto = Convert.ToDecimal(row.Pedido.ValorAcerto),  
                            QuantidadeRetorno = row.Pedido.QuantidadeRetorno,
                            Remarcado = row.Pedido.Remarcado,
                            Status = row.Pedido.Status
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


                cResult += count.ToString() + " pedido(s) importado(s).";
                Console.WriteLine(count.ToString() + " pedido(s) importado(s).");

                //RepPedidoItem
                cResult = "Importando item(s) do(s) pedido(s) ...<br>";
                Console.WriteLine(cResult);
                var newPedidoItem = new List<RepPedidoItem>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {

                    foreach (var row in deposito.PedidoItem.Join(deposito.Pedido, pi => pi.PedidoId, pd => pd.Id, (pi, pd) => new { RepPedidoItem = pi, RepPedido = pd }).Where(pi => pi.RepPedido.CargaId == vCargaId))
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


                cResult += count.ToString() + " item(s) do(s) pedido(s) importado(s).";
                Console.WriteLine(count.ToString() + " item(s) do(s) pedido(s) importado(s).");


               

                //Receber
                cResult = "Importando pagamento(s) a receber ...<br>";
                Console.WriteLine(cResult);

                var newReceber = new List<RepReceber>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {


                    string query = @"SELECT * FROM Receber WHERE CargaId = @p0 or VendedorId 
                                    IN(
	                                    SELECT Distinct VendedorId
                                        FROM Pedido
                                        WHERE CargaId in(SELECT Id FROM Carga WHERE PracaId = @p1)
                                    )";


                    foreach (var row in deposito.Receber.SqlQuery(query, vCargaId, pPracaId))
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
                            ValorJuros = Convert.ToDecimal(row.ValorJuros),
                            ValorDesconto = Convert.ToDecimal(row.ValorDesconto),
                            DataEmissao = row.DataEmissao,
                            DataLancamento = row.DataLancamento,
                            DataPagamento = row.DataPagamento,
                            DataVencimento = row.DataVencimento,
                            Observacoes = row.Observacoes
                            
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


                cResult += count.ToString() + " pagamento(s) a receber importado(s).";
                Console.WriteLine(count.ToString() + " pagamento(s) a receber importado(s).");

               
                //ReceberBaixa
                cResult = "Importando baixa(s) do pagamento a receber ...<br>";
                Console.WriteLine(cResult);

                var newReceberBaixa = new List<RepReceberBaixa>();
                count = 0;
                using (DepositoDBEntities deposito = new DepositoDBEntities())
                {


                    string query = @"SELECT * FROM ReceberBaixa WHERE 
                                       ReceberId IN (
                                            SELECT Id FROM Receber WHERE CargaId = @p0 or 
                                                VendedorId IN(
	                                                SELECT Distinct VendedorId
                                                    FROM Pedido
                                                    WHERE CargaId in(SELECT Id FROM Carga WHERE PracaId = @p1)
                                                ) 
                                        )";


                    foreach (var row in deposito.ReceberBaixa.SqlQuery(query, vCargaId, pPracaId))
                    {
                        var newReg = new RepReceberBaixa
                        {
                            Id = row.Id,
                            ReceberId = row.ReceberId,
                            CargaId = row.CargaId,
                            Valor = Convert.ToDecimal(row.Valor),
                            DataPagamento = row.DataPagamento,
                            DataBaixa = row.DataBaixa,
                            Juros = Convert.ToDecimal(row.Juros),
                            Desconto = Convert.ToDecimal(row.Desconto)
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


                cResult += count.ToString() + " baixa(s) de pagamento a receber importado(s).";
                Console.WriteLine(count.ToString() + " baixa(s) pagamento a receber importado(s).");



                return true;


                /*
                scope.Complete();

            }*/



            
            } catch (Exception ex)
            {

                MessageBox.Show("Erro ao importar carga:" + ex.Message);
                
                Console.WriteLine("Erro ao importar carga:" + ex.Message);
                
                return false;

            }
            

        }


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
    }
}
