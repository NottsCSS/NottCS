using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models.Request
{
    public class RequestResult<TResult>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ServerResult<TResult> Result { get; set; }
    }
}
