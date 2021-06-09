using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.ListaItens
{
    public class ListaItemPostRequest
    {
        public Guid produtoId { get; set; }
        public Guid listaId { get; set; }
        public bool Comprado { get; set; }
    }
}
