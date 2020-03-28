using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Handler
{
    public class Error
    {
        public Error(string property, List<string> messages)
        {
            Property = property;
            Messages = messages;
        }
        public string Property { get; }
        public List<string> Messages { get; }
    }
}
