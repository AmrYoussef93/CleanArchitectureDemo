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

namespace CleanArchitectureDemo.Application.User.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public LoginUserHandler(IIdentityService identityService, ILogger logger, IMapper mapper)
        {
            _identityService = identityService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _identityService.Login(_mapper.Map<LoginModel>(request));
            }
            catch (Exception exp)
            {
                _logger.Error(exp, "Login handler");
                return Result<string>.InternalServerError(new string[] { exp.Message });
            }
        }
    }
}
