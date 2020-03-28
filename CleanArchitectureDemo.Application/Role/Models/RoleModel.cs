using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Application.Role.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool EnableActions { get; set; }

    }
}
