using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Contracts.Usuarios
{
    public class UsuarioPostRequest
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
