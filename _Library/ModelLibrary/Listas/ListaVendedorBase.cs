using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaVendedorBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Cep { get; set; }
        public string TipoPessoa { get; set; }
        public string CpfCnpj { get; set; }
        public string RGInscricao { get; set; }
        public Nullable<System.DateTime> DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string TelefoneComercial { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DataInicial { get; set; }
        public Nullable<System.DateTime> DataFinal { get; set; }
        public Nullable<double> LimitePedido { get; set; }
        public Nullable<double> LimiteCredito { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
        public string PedidoAberto { get; set; }
        public Nullable<double> DebitoAReceber { get; set; }
    }
}
