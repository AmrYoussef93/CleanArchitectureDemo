using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Application.User.Models
{
    public class UserModel
    {
        public UserModel()
        {
            Roles = new List<string>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get;private set; }
    }
}
