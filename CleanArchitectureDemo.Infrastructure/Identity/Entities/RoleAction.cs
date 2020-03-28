using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Entities
{
    public class RoleAction
    {
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public Role Role { get; set; }
        public IdentityAction Action { get; set; }
    }
}
