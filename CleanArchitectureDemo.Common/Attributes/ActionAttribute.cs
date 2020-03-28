using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ActionAttribute : Attribute
    {
        public string DisplayName { get; }
        public string Description { get; }
        public bool ActionMapped { get; }
        public ActionAttribute(string displayName, string description, bool actionMapped = true)
        {
            DisplayName = displayName;
            Description = description;
            ActionMapped = actionMapped;
        }
    }
}
