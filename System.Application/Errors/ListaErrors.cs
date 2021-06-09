using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors
{
    public enum ListaErrors
    {
        [Description("É necessário informar um ID de usuário.")]
        Lista_Post_400_UsuarioID_Cannot_Be_Null_Or_Empty,

        [Description("Não foi possível criar a lista. Database Error.")]
        Lista_Post_400_Database_Error,



        [Description("Não foi possível excluir a lista. Database Error.")]
        Lista_Delete_400_Database_Error,

        [Description("É necessário informar um ID.")]
        Lista_Delete_400_ListaID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        Lista_Delete_400_ListaID_DoesNotExists,

        [Description("Existe um item ativo nesta lista de desejos.")]
        Lista_Delete_400_ActiveWishListItem,



        [Description("É necessário informar um ID.")]
        Lista_Get_400_ListaID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        Lista_Get_400_ListaID_DoesNotExists,
    }
}
