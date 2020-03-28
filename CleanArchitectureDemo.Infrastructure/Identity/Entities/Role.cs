using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RoleActions = new HashSet<RoleAction>();
        }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool AllActionsEnabled { get; set; }
        public ICollection<UserRole> UserRoles { get; private set; }
        public ICollection<RoleAction> RoleActions { get; private set; }
    }
}
