using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitectureDemo.Application.Common.Handler;
using MediatR;
namespace CleanArchitectureDemo.Application.User.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
