using CleanArchitectureDemo.Api.Handler;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Common.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exp)
            {
                await HandleExceptionAsync(exp, context);
            }
        }

        private Task HandleExceptionAsync(Exception exception, HttpContext context)
        {
            _logger.Error(exception, "Gobal Exception handling error");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(new ErrorDetail()
            {
                StatusCode = 500,
                Message = "Internal Server Error please contact administrator."
            }.ToString());
        }
    }
}
