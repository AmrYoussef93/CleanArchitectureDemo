using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Handler
{
    public class ValidationErrorDetail
    {
        public ValidationErrorDetail(ModelStateDictionary modelState)
        {
            Message = "Validation Error";
            StatusCode = HttpStatusCode.BadRequest;
            var modelStateErrors = modelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage)).ToArray();
            Errors = modelStateErrors.Select(m => new Error(m.Key, m.Value.ToList())).ToList();
        }
        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public List<Error> Errors { get; private set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


}
