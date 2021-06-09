using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.ListaItens;
using System.Application.Helpers;
using System.Application.Services.ListaItens;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Lista.API.Default.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ListaItemController : ControllerBase
    {
        private readonly ListaItemService listaItemService;
        public ListaItemController(ListaItemService _listaItemService)
        {
            this.listaItemService = _listaItemService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] ListaItemPostRequest _postRequest)
        {
            var result = await listaItemService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await listaItemService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await listaItemService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
