using CleanArchitectureDemo.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Configurations
{
    public class RoleActionConfiguration : IEntityTypeConfiguration<RoleAction>
    {
        public void Configure(EntityTypeBuilder<RoleAction> builder)
        {
            builder.ToTable("RoleActions");
            builder.HasKey(ra => new { ra.RoleId, ra.ActionId });
        }
    }
}
