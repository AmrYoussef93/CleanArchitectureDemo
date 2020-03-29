using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Email.Models
{
    public class EmailConfiguration
    {
        public string Server { get; set; }
        public string From { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Ssl { get; set; }
    }
}
