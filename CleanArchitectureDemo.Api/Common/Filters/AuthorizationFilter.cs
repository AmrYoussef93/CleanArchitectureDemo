using CleanArchitectureDemo.Api.Handler;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Common.Filters
{
    public class AuthorizationFilter : IAuthorizationRequirement
    {
    }
    public class AuthorizerHandler : AuthorizationHandler<AuthorizationFilter>
    {
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizerHandler(IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
        {
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationFilter requirement)
        {
            var mvcContext = context.Resource as AuthorizationFilterContext;
            var descriptor = mvcContext?.ActionDescriptor as ControllerActionDescriptor;
            var actionName = descriptor.ActionName;
            var controllerName = descriptor.ControllerName;
            if (!context.User.Identity.IsAuthenticated)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                _httpContextAccessor.HttpContext.Response.ContentType = "application/json";
                _httpContextAccessor.HttpContext.Response.WriteAsync(new UnauthorizeErrorResult("Unauthorized user", HttpStatusCode.Unauthorized).ToString());
                return Task.FromResult(0);
            }
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (_identityService.IsUserAuthorized(new AuthorizationModel(userId, controllerName, actionName)).Result)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            _httpContextAccessor.HttpContext.Response.ContentType = "application/json";
            _httpContextAccessor.HttpContext.Response.WriteAsync(new UnauthorizeErrorResult("Unauthorized to access this endpoint", HttpStatusCode.Forbidden).ToString());
            return Task.CompletedTask;
        }
    }
}
