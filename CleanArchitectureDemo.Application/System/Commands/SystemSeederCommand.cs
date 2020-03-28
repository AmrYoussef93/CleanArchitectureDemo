using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Role.Models;
using CleanArchitectureDemo.Application.System.Models;
using CleanArchitectureDemo.Application.User.Models;
using CleanArchitectureDemo.Common.Attributes;
using MediatR;
using Serilog;

namespace CleanArchitectureDemo.Application.System.Commands
{
    public class SystemSeederCommand : IRequest
    {
        public List<MethodInfo> Methods { get; set; }
    }

    public class SystemSeederHandler : IRequestHandler<SystemSeederCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger _logger;
        public SystemSeederHandler(ILogger logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }
        public async Task<Unit> Handle(SystemSeederCommand request, CancellationToken cancellationToken)
        {
            await CreateSystemRole();
            await CreateSystemAdmin();
            await SeedSystemActions(request.Methods);
            return Unit.Value;
        }
        private async Task SeedSystemActions(List<MethodInfo> methods)
        {
            var actions = methods.Where(x => ((ActionAttribute)Attribute.GetCustomAttribute(x, typeof(ActionAttribute))).ActionMapped)
                                 .Select(m => new ActionModel()
                                 {
                                     Name = m.Name,
                                     ControllerName = m.DeclaringType.Name.Replace("Controller", ""),
                                     DisplayName = ((ActionAttribute)Attribute.GetCustomAttribute(m, typeof(ActionAttribute))).DisplayName
                                 }).ToList();
            await _identityService.CreateActionsAsync(actions);
        }
        private async Task CreateSystemRole()
        {
            if (!await _identityService.RoleExsited(SystemRole.SystemAdmin))
                await _identityService.CreateRoleAsync(new RoleModel() { Name = SystemRole.SystemAdmin, EnableActions = true });
        }
        private async Task CreateSystemAdmin()
        {
            if (!await _identityService.UserExisted("admin@north.com"))
            {
                var user = new UserModel()
                {
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "admin@north.com",
                    Password = "North@123",
                    LockoutEnabled = false,
                    PhoneNumber = "0123456789",
                };
                user.Roles.Add(SystemRole.SystemAdmin);
                await _identityService.CreateUserAsync(user);
            }
        }
    }
}
