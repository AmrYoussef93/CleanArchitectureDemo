using CleanArchitectureDemo.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Configurations
{
    public class IdentityActionConfiguration : IEntityTypeConfiguration<IdentityAction>
    {
        public void Configure(EntityTypeBuilder<IdentityAction> builder)
        {
            builder.ToTable("Actions");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasMaxLength(150).IsRequired(true);
            builder.Property(a => a.ControllerName).HasMaxLength(150).IsRequired(true);
            builder.Property(a => a.DisplayName).HasMaxLength(150).IsRequired(true);
            builder.HasMany(a => a.RoleActions).WithOne(ra => ra.Action).HasForeignKey(ra => ra.ActionId);
        }
    }
}
