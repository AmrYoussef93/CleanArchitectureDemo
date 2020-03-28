using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureDemo.Api.Handler;
using CleanArchitectureDemo.Application.User.Commands.CreateUser;
using CleanArchitectureDemo.Application.User.Commands.LoginUser;
using CleanArchitectureDemo.Application.User.Models;
using CleanArchitectureDemo.Common.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.Api.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {

        [HttpPost("login")]
        [Action("Login user", "Login user", false)]
        public async Task<ResponseActionResult<string>> LoginUser([FromBody] LoginUserCommand loginUserCommand)
        {
            return new ResponseActionResult<string>(await _mediator.Send(loginUserCommand));
        }

        [HttpPost]
        [Action("Create user", "Create new user")]
        public async Task<ResponseActionResult<UserModel>> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            return new ResponseActionResult<UserModel>(await _mediator.Send(createUserCommand));
        }
    }
}