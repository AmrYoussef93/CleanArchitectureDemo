using System.Linq;
using CleanArchitectureDemo.Application;
using CleanArchitectureDemo.Infrastructure;
using CleanArchitectureDemo.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using CleanArchitectureDemo.Api.Common.Filters;
using CleanArchitectureDemo.Api.Common.Middlewares;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitectureDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(pol =>
                {
                    pol.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddInfrastructure(Configuration);
            services.AddApplication(Configuration);
            services.AddPersistence(Configuration);
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
            .AddFluentValidation(confg => confg.RegisterValidatorsFromAssemblyContaining<Startup>());



            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(x => x.First());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean Architecture" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Clean Architecture");
            });
            app.UseCustomExceptionHandler();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
