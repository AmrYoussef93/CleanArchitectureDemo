using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Entities
{
    public class IdentityAction
    {
        public IdentityAction()
        {
            RoleActions = new HashSet<RoleAction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string DisplayName { get; set; }
        public ICollection<RoleAction> RoleActions { get; private set; }
    }
}
