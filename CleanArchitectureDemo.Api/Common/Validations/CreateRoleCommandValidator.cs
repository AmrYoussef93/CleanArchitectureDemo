using CleanArchitectureDemo.Application.Role.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Api.Common.Validations
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Role is required");
        }
    }
}
