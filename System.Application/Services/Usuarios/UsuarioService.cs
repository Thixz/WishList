using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Application.Contracts.Usuarios;
using System.Application.Data.Entities.Usuarios;
using System.Application.Data.Repositories.Listas;
using System.Application.Data.Repositories.Usuarios;
using System.Application.Errors;
using System.Application.Helpers;
using System.Application.Helpers.Default;
using System.Application.Helpers.PrepareDefault;
using System.Application.Validators.Usuarios;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Services.Usuarios
{
    public class UsuarioService : PrepareDefault
    {
        private readonly ILogger<UsuarioService> logger;
        private readonly UsuarioRepository usuarioRepository;
        private readonly ListaRepository listaRepository;
        public UsuarioService(ILogger<UsuarioService> _logger, UsuarioRepository _usuarioRepository, ListaRepository _listaRepository )
        {
            this.logger = _logger;
            this.usuarioRepository = _usuarioRepository;
            this.listaRepository = _listaRepository;
        }

        public async Task<DefaultResponse> Create(UsuarioPostRequest _postRequest)
        {
            var validator = new UsuarioPostRequestValidator().Validate(_postRequest);
            if (!validator.IsValid)
            {
                logger.LogError($"[UsuarioService][Create] Erro ao tentar criar usuário: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            if (await usuarioRepository.GetByDocument(_postRequest.Documento) != null)
            {
                logger.LogError($"[UsuarioService][Create] Um usuário com este documento já existe.");
                return ErrorResponse(UsuarioErrors.Usuario_Post_400_Documento_Cannot_Be_Duplicated.GetDescription());
            }

            if (await usuarioRepository.GetByEmail(_postRequest.Email) != null)
            {
                logger.LogError($"[UsuarioService][Create] Um usuário com este email já existe.");
                return ErrorResponse(UsuarioErrors.Usuario_Post_400_Email_Cannot_Be_Duplicated.GetDescription());
            }

            var result = await usuarioRepository.Create(new UsuarioEntity(_postRequest));
            if (result.Id == Guid.Empty)
            {
                logger.LogError($"[UsuarioService][Create] Database Error.");
                return ErrorResponse(UsuarioErrors.Usuario_Post_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Update(UsuarioPutRequest _putRequest)
        {
            if (_putRequest.Id == Guid.Empty)
            {
                logger.LogError($"[UsuarioService][Update] ID was null or empty.");
                return ErrorResponse(UsuarioErrors.Usuario_Put_400_UsuarioID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var validator = new UsuarioPutRequestValidator().Validate(_putRequest);
            if (!validator.IsValid)
            {
                logger.LogError($"[UsuarioService][Update] Erro ao tentar atualizar usuário: {JsonConvert.SerializeObject(validator.Errors.ErrorList())}");
                return ErrorResponse(validator.Errors.ErrorList());
            }

            if (await usuarioRepository.Get(_putRequest.Id) == null)
            {
                logger.LogError($"[UsuarioService][Update] Não foi possível encontrar um usuário com o ID informado.");
                return ErrorResponse(UsuarioErrors.Usuario_Put_400_UsuarioID_DoesNotExists.GetDescription());
            }

            var userDocument = await usuarioRepository.GetByDocument(_putRequest.Documento);
            if (userDocument != null && userDocument.Id != _putRequest.Id)
            {
                logger.LogError($"[UsuarioService][Update] Já existe um usuário com o documento informado.");
                return ErrorResponse(UsuarioErrors.Usuario_Put_400_Documento_Cannot_Be_Duplicated.GetDescription());
            }

            var userEmail = await usuarioRepository.GetByEmail(_putRequest.Documento);
            if (userEmail != null && userEmail.Id != _putRequest.Id)
            {
                logger.LogError($"[UsuarioService][Update] Já existe um usuário com o email informado.");
                return ErrorResponse(UsuarioErrors.Usuario_Put_400_Email_Cannot_Be_Duplicated.GetDescription());
            }

            var result = await usuarioRepository.Update(new UsuarioEntity(_putRequest));
            if (result.Id == Guid.Empty)
            {
                logger.LogError($"[UsuarioService][Update] Database Error.");
                return ErrorResponse(UsuarioErrors.Usuario_Put_400_Database_Error.GetDescription());
            }

            return SuccessResponse(result);
        }
        public async Task<DefaultResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[UsuarioService][Delete] ID was null or empty.");
                return ErrorResponse(UsuarioErrors.Usuario_Delete_400_UsuarioID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            if (await listaRepository.GetListByUserId(id) != null)
            {
                logger.LogError($"[UsuarioService][Delete] Existe uma lista de desejos ativa neste usuário.");
                return ErrorResponse(UsuarioErrors.Usuario_Delete_400_ActiveWishList.GetDescription());
            }

            if (await usuarioRepository.Get(id) == null)
            {
                logger.LogError($"[UsuarioService][Delete] Não foi possível encontrar um usuário com o ID informado.");
                return ErrorResponse(UsuarioErrors.Usuario_Delete_400_UsuarioID_DoesNotExists.GetDescription());
            }

            if (!await usuarioRepository.Delete(id))
            {
                logger.LogError($"[UsuarioService][Delete] Database Error.");
                return ErrorResponse(UsuarioErrors.Usuario_Delete_400_Database_Error.GetDescription());
            }

            return SuccessResponse("O usuário foi deletado com sucesso.");
        }
        public async Task<DefaultResponse> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                logger.LogError($"[UsuarioService][Get] ID was null or empty.");
                return ErrorResponse(UsuarioErrors.Usuario_Get_400_UsuarioID_Cannot_Be_Null_Or_Empty.GetDescription());
            }

            var result = await usuarioRepository.Get(id);
            if (result == null)
            {
                logger.LogError($"[UsuarioService][Get] Não foi possível encontrar um usuário com este ID.");
                return ErrorResponse(UsuarioErrors.Usuario_Get_400_UsuarioID_DoesNotExists.GetDescription());
            }

            return SuccessResponse(result);
        }
    }
}
