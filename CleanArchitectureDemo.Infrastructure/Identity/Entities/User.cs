using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Infrastructure.Identity.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRole> UserRoles { get; private set; }
    }
}
