using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Email.Models
{
    public class EmailContent
    {
        public EmailContent()
        {
            Cc = new List<string>();
        }

        public string To { get; set; }
        public List<string> Cc { get; }
        public string Subject { get; set; }
        public MimeEntity Body { get; set; }
    }
}
