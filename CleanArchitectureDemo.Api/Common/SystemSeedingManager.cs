using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CleanArchitectureDemo.Application.System.Commands;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureDemo.Common.Attributes;

namespace CleanArchitectureDemo.Api.Common
{
    public static class SystemSeedingManager
    {
        public static async Task<IHost> SeedSystem(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _logger = services.GetRequiredService<ILogger>();
                var _mediator = services.GetRequiredService<IMediator>();
                try
                {
                    _logger.Debug($"Starting initializing system");
                    var methods = Assembly.GetExecutingAssembly()
                       .GetTypes()
                       .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                       .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                       .Where(m =>
                               !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any() &&
                               !m.IsDefined(typeof(NonActionAttribute)) &&
                               m.IsDefined(typeof(ActionAttribute))).ToList();
                    await _mediator.Send(new SystemSeederCommand() { Methods = methods });
                }
                catch (Exception exp)
                {
                    _logger.Error(exp, "An error occurred while intializing system please contact administator");
                }

            }
            return host;
        }
    }
}
