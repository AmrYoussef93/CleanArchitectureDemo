using CleanArchitectureDemo.Api.Common.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Common.ApiInstallations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthorizationHandler, AuthorizerHandler>();
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Authorizer", policy => policy.AddRequirements(new AuthorizationFilter()));
            });
            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
                config.Filters.Add<ValidationFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddSwaggerGen(config =>
            {
                config.ResolveConflictingActions(x => x.First());
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean Architecture" });
                var openApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                };
                config.AddSecurityDefinition("Bearer", openApiSecurityScheme);
                var requirement = new Dictionary<OpenApiSecurityScheme, IList<string>>
                {
                    { openApiSecurityScheme,new string[0]}
                };
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                 {
                    {
                      openApiSecurityScheme,
                      new List<string>()
                    }
                 });
            });
            return services;
        }
    }
}
