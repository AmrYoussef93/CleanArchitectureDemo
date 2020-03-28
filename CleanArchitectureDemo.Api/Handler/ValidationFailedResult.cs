using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Handler
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) :base(new ValidationErrorDetail(modelState))
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
