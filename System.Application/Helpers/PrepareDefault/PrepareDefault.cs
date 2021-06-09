using System;
using System.Application.Helpers.Default;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Helpers.PrepareDefault
{
    public class PrepareDefault
    {
        protected DefaultResponse<T> SuccessResponse<T>(T data)
        {
            return new DefaultResponse<T>(data)
            {
                success = true
            };
        }

        protected DefaultResponse<T> ErrorResponse<T>(T data)
        {
            return new DefaultResponse<T>(data)
            {
                success = false
            };
        }
    }
}
