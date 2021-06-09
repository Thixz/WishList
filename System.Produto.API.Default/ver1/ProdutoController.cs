using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Contracts.Produtos;
using System.Application.Helpers;
using System.Application.Services.Produtos;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Produto.API.Default.ver1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService produtoService;
        public ProdutoController(ProdutoService _produtoService)
        {
            this.produtoService = _produtoService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] ProdutoPostRequest _postRequest)
        {
            var result = await produtoService.Create(_postRequest);
            return HttpConvert.Convert(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProdutoPutRequest _putRequest)
        {
            var result = await produtoService.Update(_putRequest);
            return HttpConvert.Convert(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await produtoService.Delete(id);
            return HttpConvert.Convert(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await produtoService.Get(id);
            return HttpConvert.Convert(result);
        }
    }
}
