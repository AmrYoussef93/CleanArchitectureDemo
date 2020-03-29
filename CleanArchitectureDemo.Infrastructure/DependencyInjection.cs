using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Infrastructure.Identity;
using CleanArchitectureDemo.Infrastructure.Identity.Context;
using CleanArchitectureDemo.Infrastructure.Identity.Entities;
using CleanArchitectureDemo.Infrastructure.Identity.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Identity Resigter
            services.AddScoped<IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                IdentityRoleClaim<int>, IdentityUserToken<int>>, NorthwindIdentityContext>();
            services.AddDbContext<NorthwindIdentityContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("NorthwindContext")));
            services.AddDefaultIdentity<User>().AddRoles<Role>().AddEntityFrameworkStores<NorthwindIdentityContext>();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequiredLength = 2,
                    RequiredUniqueChars = 1,
                    RequireNonAlphanumeric = false
                };
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.Configure<IdentitySetting>(configuration.GetSection("IdentitySetting"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                           .GetBytes(configuration.GetSection("IdentitySetting").GetValue<string>("SigningKey"))),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });
            #endregion

            return services;
        }
    }
}
