using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Application.Contracts.Produtos;
using System.Application.Data.Entities.Produtos;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Data.Repositories.Produtos;
using System.Application.Errors;
using System.Application.Helpers;
using System.Application.Helpers.Default;
using System.Application.Helpers.PrepareDefault;
using System.Application.Validators.Produtos;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.Produtos
{
    public class ProdutoService : PrepareDefault
    {
        private readonly ILogger<ProdutoService> logger;
        private readonly ProdutoRepository produtoRepository;
        private readonly ListaItemRepository listaItemRepository;
        public ProdutoService(ILogger<ProdutoService> _logger, ProdutoRepository _produtoRepository, ListaItemRepository _listaItemRepository)
        {
            this.logger = _logger;
            this.produtoRepository = _produtoRepository;
            this.listaItemRepository = _listaItemRepository;
        }

        public async Task<DefaultResponse> Create(ProdutoPostRequest _postRequest)
        {
            var validator = new ProdutoPostRequestValidator().Validate(_postRequest);
            if (!validator.IsValid)
            {
                logger.LogError($"[ProdutoService][Create] Erro ao tentar criar produto: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            var result = await produtoRepository.Create(new ProdutoEntity(_postRequest));
            if (result.Id == Guid.Empty)
            {
                logger.LogError($"[ProdutoService][Create] Database Error.");
                return ErrorResponse(ProdutoErrors.Produto_Post_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Update(ProdutoPutRequest _putRequest)
        {
            if (_putRequest.Id == Guid.Empty)
            {
                logger.LogError($"[ProdutoService][Update] ID was null or empty.");
                return ErrorResponse(ProdutoErrors.Produto_Put_400_ProdutoID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new ProdutoPutRequestValidator().Validate(_putRequest);
            if (!validator.IsValid)
            {
                logger.LogError($"[ProdutoService][Update] Erro ao tentar atualizar produto: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            if (await produtoRepository.Get(_putRequest.Id) == null)
            {
                logger.LogError($"[ProdutoService][Update] Não foi possível encontrar um produto com o ID informado.");
                return ErrorResponse(ProdutoErrors.Produto_Put_400_ProdutoID_DoesNotExists.GetDescription());
            }

            var result = await produtoRepository.Update(new ProdutoEntity(_putRequest));
            if (result.Id == Guid.Empty)
            {
                logger.LogError($"[ProdutoService][Update] Database Error.");
                return ErrorResponse(ProdutoErrors.Produto_Put_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[ProdutoService][Delete] ID was null or empty.");
                return ErrorResponse(ProdutoErrors.Produto_Delete_400_ProdutoID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await listaItemRepository.GetListItemByProductID(id) != null)
            {
                logger.LogError($"[ProdutoService][Delete] Existe um item em uma lista de desejos com este produto.");
                return ErrorResponse(ProdutoErrors.Produto_Delete_400_ActiveWishListItem.GetDescription());
            }

            if (await produtoRepository.Get(id) == null)
            {
                logger.LogError($"[ProdutoService][Delete] Não foi possível encontrar um produto com o ID informado.");
                return ErrorResponse(ProdutoErrors.Produto_Delete_400_ProdutoID_DoesNotExists.GetDescription());
            }

            if (!await produtoRepository.Delete(id))
            {
                logger.LogError($"[ProdutoService][Delete] Database Error.");
                return ErrorResponse(ProdutoErrors.Produto_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("O produto foi deletado com sucesso.");
        }
        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[ProdutoService][Get] ID was null or empty.");
                return ErrorResponse(ProdutoErrors.Produto_Get_400_ProdutoID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await produtoRepository.Get(id);
            if (result == null)
            {
                logger.LogError($"[ProdutoService][Get] Não foi possível encontrar um produto com este ID.");
                return ErrorResponse(ProdutoErrors.Produto_Get_400_ProdutoID_DoesNotExists.GetDescription());
            }

            return SuccessResponse(result);
        }

    }
}
