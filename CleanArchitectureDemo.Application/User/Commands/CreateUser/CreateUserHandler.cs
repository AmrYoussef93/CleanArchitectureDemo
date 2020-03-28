using AutoMapper;
using CleanArchitectureDemo.Application.Common;
using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.User.Models;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Application.User.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserModel>>
    {
        private readonly IIdentityService _identitytService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CreateUserHandler(IIdentityService identityService, IMapper mapper, ILogger logger)
        {
            _identitytService = identityService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<UserModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _identitytService.CreateUserAsync(_mapper.Map<UserModel>(request));
            }
            catch (Exception exp)
            {
                _logger.Error(exp, "Create User Handler");
                return Result<UserModel>.InternalServerError(new string[] { exp.Message });
            }
        }
    }
}
