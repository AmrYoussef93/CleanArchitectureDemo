using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitectureDemo.Api.Handler;
using CleanArchitectureDemo.Application.Role.Models;
using CleanArchitectureDemo.Application.Role.Commands;
using CleanArchitectureDemo.Common.Attributes;
using CleanArchitectureDemo.Api.Common.Filters;

namespace CleanArchitectureDemo.Api.Controllers
{
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Action("Create role", "Create new role")]
        public async Task<ResponseActionResult<RoleModel>> CreateRole([FromBody]CreateRoleCommand createRoleCommand)
        {
            return new ResponseActionResult<RoleModel>(await _mediator.Send(createRoleCommand));
        }
    }
}