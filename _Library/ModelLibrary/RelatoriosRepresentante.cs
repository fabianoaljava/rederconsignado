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
    public class RelatoriosRepresentante
    {

        public class VendedorPedido
        {
            public string CodigoPedido { get; set; }
            public Nullable<System.DateTime> DataPedido { get; set; }
            public string Vendedor { get; set; }
            public string CPF { get; set; }
            public string Telefone { get; set; }
            public string Endereco { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Praca { get; set; }
            public string Representante { get; set; }
            public string TelRepresentante { get; set; }
            public Nullable<DateTime> DataRetorno { get; set; }
            public string Comissao { get; set; }
        }



        public class VendedorPedidoItem
        {
            public string CodigoPedido { get; set; }            
            public string Produto { get; set; }
            public double Quantidade { get; set; }
            public double Valor { get; set; }
            public double ValorTotal { get; set; }
        }


        public static VendedorPedido RelatorioVendedorPedido(long pVendedorId, long pCargaId)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                
                var pedido = representante.RepPedido.OrderByDescending(i => i.Id).FirstOrDefault(p => p.VendedorId == pVendedorId && p.CargaId == pCargaId);
 
                var vendedor = representante.RepVendedor.OrderByDescending(i => i.Id).FirstOrDefault(v => v.Id == pVendedorId);

                var carga = representante.RepCarga.OrderByDescending(i => i.Id).FirstOrDefault(c => c.Id == pCargaId);


                ///tratar quando nulo ou usar try...

                var representcomercial = representante.RepUsuario.OrderByDescending(i => i.Id).FirstOrDefault(r => r.Id == carga.RepresentanteId);

                var praca = representante.RepPraca.OrderByDescending(i => i.Id).FirstOrDefault(p => p.Id == carga.PracaId);

                VendedorPedido vendedorPedido = new VendedorPedido
                {
                    CodigoPedido = pedido.CodigoPedido,
                    DataPedido = pedido.DataLancamento,
                    Vendedor = vendedor.Nome,
                    CPF = vendedor.CpfCnpj,
                    Telefone = vendedor.Telefone + " / " + vendedor.Celular,
                    Endereco = vendedor.Endereco + " " + vendedor.Complemento,
                    Bairro = vendedor.Bairro,
                    Cidade = vendedor.Cidade + "/" + vendedor.UF,
                    Praca = praca.Descricao,
                    Representante = representcomercial.Nome,
                    TelRepresentante = representcomercial.Telefone + " / " + representcomercial.Celular,
                    DataRetorno = pedido.DataRetorno,
                    Comissao = ""
                };

                return vendedorPedido;
            }


        }


        public static List<VendedorPedidoItem> RelatorioVendedorPedidoItem(string pCodigoPedido)
        {

            using (RepresentanteDBEntities representante = new RepresentanteDBEntities())
            {

                string query = @"SELECT CodigoPedido, Descricao  || ' ' ||  Tamanho  || ' ' || Cor AS Produto, RepPedidoItem.Quantidade as Quantidade, RepPedidoItem.Preco as Valor, RepPedidoItem.Quantidade * RepPedidoItem.Preco as ValorTotal 
                                FROM RepPedidoItem 
                                    INNER JOIN RepPedido ON RepPedidoItem.PedidoId = RepPedido.Id
                                    INNER JOIN RepProdutoGrade ON RepPedidoItem.ProdutoGradeId = RepProdutoGrade.Id
                                    INNER JOIN RepProduto ON RepProdutoGrade.ProdutoId = RepProduto.Id
                                ";


                var result = representante.Database.SqlQuery<VendedorPedidoItem>(query, pCodigoPedido);

                return result.ToList<VendedorPedidoItem>();

            }
        }
    }
}
