using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors
{
    public enum UsuarioErrors
    {
        [Description("É necessário informar um nome de usuário.")]
        Usuario_Post_400_NomeUsuario_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar um documento.")]
        Usuario_Post_400_Documento_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar um número de telefone.")]
        Usuario_Post_400_Telefone_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar um email.")]
        Usuario_Post_400_Email_Cannot_Be_Null_Or_Empty,

        [Description("Já existe um usuário com este documento.")]
        Usuario_Post_400_Documento_Cannot_Be_Duplicated,

        [Description("Já existe um usuário com este email.")]
        Usuario_Post_400_Email_Cannot_Be_Duplicated,

        [Description("Não foi possível criar o usuário. Database Error")]
        Usuario_Post_400_Database_Error,




        [Description("É necessário informar um nome de usuário.")]
        Usuario_Put_400_NomeUsuario_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar um documento.")]
        Usuario_Put_400_Documento_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar um número de telefone.")]
        Usuario_Put_400_Telefone_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar um email.")]
        Usuario_Put_400_Email_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar um ID.")]
        Usuario_Put_400_UsuarioID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        Usuario_Put_400_UsuarioID_DoesNotExists,

        [Description("Já existe um usuário com este documento.")]
        Usuario_Put_400_Documento_Cannot_Be_Duplicated,

        [Description("Já existe um usuário com este email.")]
        Usuario_Put_400_Email_Cannot_Be_Duplicated,

        [Description("Não foi possível atualizar o usuário. Database Error")]
        Usuario_Put_400_Database_Error,



        [Description("É necessário informar um ID.")]
        Usuario_Delete_400_UsuarioID_Cannot_Be_Null_Or_Empty,

        [Description("Existe uma lista de desejos ativa.")]
        Usuario_Delete_400_ActiveWishList,

        [Description("O ID informado não foi encontrado.")]
        Usuario_Delete_400_UsuarioID_DoesNotExists,

        [Description("Não foi possível atualizar o usuário. Database Error")]
        Usuario_Delete_400_Database_Error,



        [Description("É necessário informar um ID.")]
        Usuario_Get_400_UsuarioID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        Usuario_Get_400_UsuarioID_DoesNotExists,
    }
}
