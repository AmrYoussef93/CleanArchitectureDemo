using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Application.System.Models
{
    public class ActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string DisplayName { get; set; }
    }
}
