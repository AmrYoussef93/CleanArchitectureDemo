using CleanArchitectureDemo.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired(false);
            builder.HasMany(u => u.UserRoles).WithOne(r => r.User).HasForeignKey(u => u.UserId);
        }
    }
}
