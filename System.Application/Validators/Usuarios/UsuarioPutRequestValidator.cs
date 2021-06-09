using FluentValidation;
using System;
using System.Application.Contracts.Usuarios;
using System.Application.Errors;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Validators.Usuarios
{
    public class UsuarioPutRequestValidator : AbstractValidator<UsuarioPutRequest>
    {
        public UsuarioPutRequestValidator()
        {
            RuleFor(x => x.Nome).Must(name => !string.IsNullOrEmpty(name)).
                WithErrorCode(UsuarioErrors.Usuario_Put_400_NomeUsuario_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.Documento).Must(document => !string.IsNullOrEmpty(document)).
                WithErrorCode(UsuarioErrors.Usuario_Put_400_Documento_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.Telefone).Must(telefone => !string.IsNullOrEmpty(telefone)).
                WithErrorCode(UsuarioErrors.Usuario_Put_400_Telefone_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.Email).Must(email => !string.IsNullOrEmpty(email)).
                WithErrorCode(UsuarioErrors.Usuario_Put_400_Email_Cannot_Be_Null_Or_Empty.GetDescription());
        }
    }
}
