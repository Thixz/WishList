using Microsoft.Extensions.Logging;
using System;
using System.Application.Contracts.ListaItens;
using System.Application.Data.Entities.ListaItens;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Errors;
using System.Application.Helpers;
using System.Application.Helpers.Default;
using System.Application.Helpers.PrepareDefault;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.ListaItens
{
    public class ListaItemService : PrepareDefault
    {
        private readonly ILogger<ListaItemService> logger;
        private readonly ListaItemRepository listaItemRepository;
        public ListaItemService(ILogger<ListaItemService> _logger, ListaItemRepository _listaItemRepository)
        {
            this.logger = _logger;
            this.listaItemRepository = _listaItemRepository;
        }

        public async Task<DefaultResponse> Create(ListaItemPostRequest _postRequest)
        {
            if (_postRequest.listaId == Guid.Empty || _postRequest.produtoId == Guid.Empty)
            {
                logger.LogError($"[ListaItemService][Create] É necessário informar um ID de Produto e um ID de Lista.");
                return ErrorResponse(ListaItemErrors.ListaItem_Post_400_ProdutoID_ListaID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var checarListaMesmoProduto = await listaItemRepository.GetListItemByProductID(_postRequest.produtoId);
            if (checarListaMesmoProduto != null && checarListaMesmoProduto.listaId == _postRequest.listaId)
            {
                logger.LogError($"[ListaItemService][Create] Já existe um produto com este ID nesta lista.");
                return ErrorResponse(ListaItemErrors.ListaItem_Post_400_ProdutoId_CannotBeDuplicated.GetDescription());
            }

            var result = await listaItemRepository.Create(new ListaItemEntity(_postRequest));
            if (result.Id == Guid.Empty)
            {
                logger.LogError($"[ListaItemService][Create] Database Error.");
                return ErrorResponse(ListaItemErrors.ListaItem_Post_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[ListaItemService][Delete] ID was null or empty.");
                return ErrorResponse(ListaItemErrors.ListaItem_Delete_400_ListaItemID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await listaItemRepository.Get(id) == null)
            {
                logger.LogError($"[ListaItemService][Delete] Não foi possível encontrar um item com o ID informado.");
                return ErrorResponse(ListaItemErrors.ListaItem_Delete_400_ListaItemID_DoesNotExists.GetDescription());
            }

            if (!await listaItemRepository.Delete(id))
            {
                logger.LogError($"[ListaItemService][Delete] Database Error.");
                return ErrorResponse(ListaItemErrors.ListaItem_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("O item foi deletada com sucesso.");
        }
        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[ListaItemService][Get] ID was null or empty.");
                return ErrorResponse(ListaItemErrors.ListaItem_Get_400_ListaItemID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await listaItemRepository.Get(id);
            if (result == null)
            {
                logger.LogError($"[ListaItemService][Get] Não foi possível encontrar um item com este ID.");
                return ErrorResponse(ListaItemErrors.ListaItem_Get_400_ListaItemID_DoesNotExists.GetDescription());
            }

            return SuccessResponse(result);
        }
    }
}
