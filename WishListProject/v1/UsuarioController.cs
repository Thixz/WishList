using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Usuarios;
using System.Application.Helpers;
using System.Application.Services.Usuarios;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Usuario.API.Default.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        public UsuarioController(UsuarioService _usuarioService)
        {
            this.usuarioService = _usuarioService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] UsuarioPostRequest _postRequest)
        {
            var result = await usuarioService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UsuarioPutRequest _putRequest)
        {
            var result = await usuarioService.Update(_putRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await usuarioService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await usuarioService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
