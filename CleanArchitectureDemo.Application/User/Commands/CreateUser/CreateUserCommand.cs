using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.User.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Application.User.Commands.CreateUser
{
    // should  return response dto
    public class CreateUserCommand : IRequest<Result<UserModel>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
