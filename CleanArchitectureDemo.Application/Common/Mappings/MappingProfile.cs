using AutoMapper;
using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Role.Commands;
using CleanArchitectureDemo.Application.Role.Models;
using CleanArchitectureDemo.Application.User.Commands.CreateUser;
using CleanArchitectureDemo.Application.User.Commands.LoginUser;
using CleanArchitectureDemo.Application.User.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureDemo.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(Result<>), typeof(Result<>));
            CreateMap<UserModel, CreateUserCommand>().ReverseMap();
            CreateMap<RoleModel, CreateRoleCommand>().ReverseMap();
            CreateMap<LoginModel, LoginUserCommand>().ReverseMap();
        }
    }
}
