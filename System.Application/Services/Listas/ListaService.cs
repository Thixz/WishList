using Microsoft.Extensions.Logging;
using System;
using System.Application.Contracts.ListaItens;
using System.Application.Contracts.Listas;
using System.Application.Data.Entities.ListaItens;
using System.Application.Data.Entities.Listas;
using System.Application.Data.Repositories.ListaItens;
using System.Application.Data.Repositories.Listas;
using System.Application.Errors;
using System.Application.Helpers;
using System.Application.Helpers.Default;
using System.Application.Helpers.PrepareDefault;
using System.Application.Services.ListaItens;
using System.Application.Views;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.Listas
{
    public class ListaService : PrepareDefault
    {
        private readonly ILogger<ListaService> logger;
        private readonly ListaRepository listaRepository;
        private readonly ListaItemRepository listaItemRepository;
        public ListaService(ILogger<ListaService> _logger, ListaRepository _listaRepository, ListaItemRepository _listaItemRepository)
        {
            this.logger = _logger;
            this.listaRepository = _listaRepository;
            this.listaItemRepository = _listaItemRepository;
        }

        public async Task<DefaultResponse> Create(ListaPostRequest _postRequest)
        {
            if (_postRequest.usuarioId == Guid.Empty)
            {
                logger.LogError($"[ListaService][Create] O ID de usuário não pode ser nulo ou vazio.");
                return ErrorResponse(ListaErrors.Lista_Post_400_UsuarioID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await listaRepository.Create(new ListaEntity(_postRequest));
            if (result.Id == Guid.Empty)
            {
                logger.LogError($"[ListaService][Create] Database Error.");
                return ErrorResponse(ListaErrors.Lista_Post_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[ListaService][Delete] ID was null or empty.");
                return ErrorResponse(ListaErrors.Lista_Delete_400_ListaID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await listaItemRepository.GetListItemByListID(id) != null)
            {
                logger.LogError($"[ListaService][Delete] Existe um item ativo nesta lista de desejos.");
                return ErrorResponse(ListaErrors.Lista_Delete_400_ActiveWishListItem.GetDescription());
            }

            if (await listaRepository.Get(id) == null)
            {
                logger.LogError($"[ListaService][Delete] Não foi possível encontrar uma lista com o ID informado.");
                return ErrorResponse(ListaErrors.Lista_Delete_400_ListaID_DoesNotExists.GetDescription());
            }

            if (!await listaRepository.Delete(id))
            {
                logger.LogError($"[ListaService][Delete] Database Error.");
                return ErrorResponse(ListaErrors.Lista_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("A lista foi deletada com sucesso.");
        }
        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[ListaService][Get] ID was null or empty.");
                return ErrorResponse(ListaErrors.Lista_Get_400_ListaID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await listaRepository.Get(id);
            if (result == null)
            {
                logger.LogError($"[ListaService][Get] Não foi possível encontrar uma lista com este ID.");
                return ErrorResponse(ListaErrors.Lista_Get_400_ListaID_DoesNotExists.GetDescription());
            }

            result.Itens = new List<ListaItemViewEntity>();
            foreach (var item in await listaItemRepository.GetListItemViewByListID(id))
            {
                result.Itens.Add(item);
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> GetRandomItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[ListaService][Get] ID was null or empty.");
                return ErrorResponse(ListaErrors.Lista_Get_400_ListaID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await listaRepository.Get(id);
            if (result == null)
            {
                logger.LogError($"[ListaService][Get] Não foi possível encontrar uma lista com este ID.");
                return ErrorResponse(ListaErrors.Lista_Get_400_ListaID_DoesNotExists.GetDescription());
            }

            result.Itens = new List<ListaItemViewEntity>();
            foreach (var item in await listaItemRepository.GetListItemViewByListID(id))
            {
                result.Itens.Add(item);
            }

            System.Random random = new System.Random();
            var randomList = new List<ListaItemViewEntity>();

            int index = random.Next(result.Itens.Count);
            randomList.Add(result.Itens[index]);

            return SuccessResponse(randomList);
        }
    }
}
