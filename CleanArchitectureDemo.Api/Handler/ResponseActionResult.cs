using CleanArchitectureDemo.Application.Common.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Handler
{
    public class ResponseActionResult<T> : IActionResult
    {

        private readonly Result<T> _serviceResult;
        public ResponseActionResult(Result<T> serviceResult)
        {
            _serviceResult = serviceResult;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (_serviceResult.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await new NotFoundResult().ExecuteResultAsync(context);
            }
            else if (_serviceResult.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                await new NoContentResult().ExecuteResultAsync(context);
            }
            else if (_serviceResult.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await new UnauthorizedResult().ExecuteResultAsync(context);
            }
            else
            {
                object responseBody;
                if (_serviceResult.HasErrors)
                {
                    responseBody = _serviceResult.Errors;
                }
                else
                {
                    responseBody = _serviceResult.Date;
                }
                var jsonResult = new JsonResult(responseBody)
                {
                    StatusCode = (int)_serviceResult.StatusCode,
                    Value = responseBody,
                    ContentType = "application/json"
                };
                await jsonResult.ExecuteResultAsync(context);
            }
        }
    }
}
