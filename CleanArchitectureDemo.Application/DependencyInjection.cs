using AutoMapper;
using CleanArchitectureDemo.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using System.Reflection;

namespace CleanArchitectureDemo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            var _logger = new LoggerConfiguration()
                              .Enrich.WithExceptionDetails()
                              .ReadFrom.Configuration(configuration)
                              .CreateLogger();
            services.AddSingleton<ILogger>(_logger);

            return services;
        }
    }
}
