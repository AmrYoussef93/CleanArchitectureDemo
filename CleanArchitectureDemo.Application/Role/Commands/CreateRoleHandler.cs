using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureDemo.Application.Common.Handler;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Role.Models;
using MediatR;
using Serilog;

namespace CleanArchitectureDemo.Application.Role.Commands
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Result<RoleModel>>
    {
        private readonly IIdentityService _identitytService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CreateRoleHandler(IIdentityService identitytService, IMapper mapper, ILogger logger)
        {
            _identitytService = identitytService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<RoleModel>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _identitytService.CreateRoleAsync(_mapper.Map<RoleModel>(request));
            }
            catch (Exception exp)
            {
                _logger.Error(exp, "Create Role Handler");
                return Result<RoleModel>.InternalServerError(new string[] { exp.Message });
            }
        }
    }
}
