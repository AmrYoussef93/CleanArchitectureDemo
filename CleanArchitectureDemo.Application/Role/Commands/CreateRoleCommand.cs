using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Role.Models;
using MediatR;
namespace CleanArchitectureDemo.Application.Role.Commands
{
    public class CreateRoleCommand : IRequest<Result<RoleModel>>
    {
        public string Name { get; set; }
        public bool EnableActions { get; set; }
    }
}
