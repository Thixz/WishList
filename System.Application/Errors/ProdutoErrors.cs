using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Application.Errors
{
    public enum ProdutoErrors
    {
        [Description("É necessário informar um título para o produto.")]
        Produto_Post_400_TituloProduto_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar uma descrição.")]
        Produto_Post_400_Descricao_Cannot_Be_Null_Or_Empty,

        [Description("Não foi possível criar o produto. Database Error")]
        Produto_Post_400_Database_Error,



        [Description("É necessário informar um título para o produto.")]
        Produto_Put_400_TituloProduto_Cannot_Be_Null_Or_Empty,

        [Description("É necessário informar uma descrição.")]
        Produto_Put_400_Descricao_Cannot_Be_Null_Or_Empty,

        [Description("Não foi possível atualizar o produto. Database Error")]
        Produto_Put_400_Database_Error,

        [Description("É necessário informar um ID.")]
        Produto_Put_400_ProdutoID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        Produto_Put_400_ProdutoID_DoesNotExists,



        [Description("Não foi possível excluir o produto. Database Error")]
        Produto_Delete_400_Database_Error,

        [Description("É necessário informar um ID.")]
        Produto_Delete_400_ProdutoID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        Produto_Delete_400_ProdutoID_DoesNotExists,

        [Description("Existe um item em uma lista de desejos com este produto.")]
        Produto_Delete_400_ActiveWishListItem,



        [Description("É necessário informar um ID.")]
        Produto_Get_400_ProdutoID_Cannot_Be_Null_Or_Empty,

        [Description("O ID informado não foi encontrado.")]
        Produto_Get_400_ProdutoID_DoesNotExists,
    }
}
