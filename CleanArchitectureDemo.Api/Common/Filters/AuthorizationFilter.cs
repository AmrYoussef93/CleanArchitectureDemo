using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public AuthorizerHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationFilter requirement)
        {
            var mvcContext = context.Resource as AuthorizationFilterContext;
            var descriptor = mvcContext?.ActionDescriptor as ControllerActionDescriptor;
            var actionName = descriptor.ActionName;
            var controllerName = descriptor.ControllerName;
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _identityService.IsUserAuthorized(new AuthorizationModel(userId, controllerName, actionName)))
            {
                context.Succeed(requirement);
            }
        }
    }
}
