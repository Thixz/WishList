using FluentValidation;
using System;
using System.Application.Contracts.Produtos;
using System.Application.Errors;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Validators.Produtos
{
    public class ProdutoPutRequestValidator : AbstractValidator<ProdutoPutRequest>
    {
        public ProdutoPutRequestValidator()
        {
            RuleFor(x => x.tituloProduto).Must(titulo => !string.IsNullOrEmpty(titulo)).
                WithErrorCode(ProdutoErrors.Produto_Put_400_TituloProduto_Cannot_Be_Null_Or_Empty.GetDescription());

            RuleFor(x => x.Descricao).Must(descricao => !string.IsNullOrEmpty(descricao)).
                WithErrorCode(ProdutoErrors.Produto_Put_400_Descricao_Cannot_Be_Null_Or_Empty.GetDescription());
        }
    }
}
