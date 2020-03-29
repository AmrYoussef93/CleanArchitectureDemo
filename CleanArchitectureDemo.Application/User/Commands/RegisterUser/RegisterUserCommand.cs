using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.User.Models;
using MediatR;
namespace CleanArchitectureDemo.Application.User.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<RegisterUserResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
