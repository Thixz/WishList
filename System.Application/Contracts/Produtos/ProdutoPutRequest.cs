using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Produtos
{
    public class ProdutoPutRequest
    {
        public Guid Id { get; set; }
        public string tituloProduto { get; set; }
        public string Descricao { get; set; }
    }
}
