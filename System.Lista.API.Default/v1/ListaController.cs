using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Listas;
using System.Application.Helpers;
using System.Application.Services.Listas;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Lista.API.Default.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ListaController : ControllerBase
    {
        private readonly ListaService listaService;
        public ListaController(ListaService _listaService)
        {
            this.listaService = _listaService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] ListaPostRequest _postRequest)
        {
            var result = await listaService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await listaService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await listaService.Get(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("GetRandom")]
        public async Task<IActionResult> GetRandom([FromQuery] Guid id)
        {
            var result = await listaService.GetRandomItem(id);
            return HttpConvert.Convert(result);
        }
    }
}
