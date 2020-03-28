using CleanArchitectureDemo.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Context
{
    public class NorthwindIdentityContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
         IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public NorthwindIdentityContext(DbContextOptions<NorthwindIdentityContext> options) : base(options)
        {

        }
        public DbSet<IdentityAction> Actions { get; set; }
        public DbSet<RoleAction> RoleActions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("security");
            builder.ApplyConfigurationsFromAssembly(typeof(NorthwindIdentityContext).Assembly);
        }
    }
}
