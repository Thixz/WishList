using Microsoft.AspNetCore.Mvc;
using System;
using System.Application.Helpers.Default;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace System.Application.Helpers
{
    public static class HttpConvert
    {
        public static IActionResult Convert(this DefaultResponse _defaultResponse)
        {
            if (_defaultResponse.success)
                return new ObjectResult(_defaultResponse) { StatusCode = (int)HttpStatusCode.OK };

            return new ObjectResult(_defaultResponse) { StatusCode = (int)HttpStatusCode.BadRequest };
        }
    }
}
