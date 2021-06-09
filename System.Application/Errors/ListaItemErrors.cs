using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors
{
    public enum ListaItemErrors
    {
        [Description("É necessário informar um ID de Produto e um ID de Lista.")]
        ListaItem_Post_400_ProdutoID_ListaID_Cannot_Be_Null_Or_Empty,

        [Description("Não foi possível criar o item. Database Error.")]
        ListaItem_Post_400_Database_Error,

        [Description("Já existe um produto com este ID nesta lista.")]
        ListaItem_Post_400_ProdutoId_CannotBeDuplicated,



        [Description("Não foi possível excluir o item. Database Error.")]
        ListaItem_Delete_400_Database_Error,

        [Description("É necessário informar um ID.")]
        ListaItem_Delete_400_ListaItemID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        ListaItem_Delete_400_ListaItemID_DoesNotExists,



        [Description("É necessário informar um ID.")]
        ListaItem_Get_400_ListaItemID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        ListaItem_Get_400_ListaItemID_DoesNotExists,
    }
}
