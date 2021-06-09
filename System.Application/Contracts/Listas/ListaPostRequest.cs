using System;
using System.Application.Data.Entities.ListaItens;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Listas
{
    public class ListaPostRequest
    {
        public Guid usuarioId { get; set; }
        public string listaNome { get; set; }
    }
}
