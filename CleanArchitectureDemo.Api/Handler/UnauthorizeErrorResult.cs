using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Handler
{
    public class UnauthorizeErrorResult
    {
        public UnauthorizeErrorResult(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            StatusCode = httpStatusCode;
        }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
