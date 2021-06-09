using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Helpers
{
    public static class ToErrorList
    {
        public static List<string> ErrorList(this IList<ValidationFailure> list)
        {
            var _result = new List<string>();

            foreach (var item in list)
            {
                _result.Add(item.ErrorCode);
            }
            return _result;
        }
    }
}
