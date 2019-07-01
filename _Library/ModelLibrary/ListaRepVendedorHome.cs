using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaRepVendedorHome
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CidadeUF { get; set; }
        public string Telefones { get; set; }
        public bool PedidoAnterior { get; set; }
        public string Recebido { get; set; }
        public bool PedidoAtual { get; set; }
        public string CodigoPedido { get; set; }
        public bool Receber { get; set; }
        public bool Negativado { get; set; }

    }
}
