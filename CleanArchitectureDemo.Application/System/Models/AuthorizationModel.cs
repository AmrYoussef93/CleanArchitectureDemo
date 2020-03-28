using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Application.System.Models
{
    public class AuthorizationModel
    {
        public int UserId { get; }
        public string ControllerName { get; }
        public string ActionName { get; }
        public AuthorizationModel(int userId, string controllerName, string actionName)
        {
            UserId = userId;
            ControllerName = controllerName;
            ActionName = actionName;
        }
    }
}
