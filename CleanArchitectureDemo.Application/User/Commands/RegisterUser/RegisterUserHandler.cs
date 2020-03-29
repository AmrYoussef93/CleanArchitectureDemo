using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.User.Models;
using MediatR;
using Serilog;

namespace CleanArchitectureDemo.Application.User.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
    {
        private readonly IIdentityService _identitytService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public RegisterUserHandler(IIdentityService identityService, IMapper mapper, ILogger logger)
        {
            _identitytService = identityService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _identitytService.RegisterUserAsync(_mapper.Map<UserModel>(request));
            }
            catch (Exception exp)
            {
                _logger.Error(exp, "Error in register user");
                return Result<RegisterUserResponse>.InternalServerError(new string[] { exp.Message });
            }
        }
    }
}
