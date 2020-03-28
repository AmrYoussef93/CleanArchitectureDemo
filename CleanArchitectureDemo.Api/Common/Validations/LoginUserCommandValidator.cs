using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureDemo.Application.User.Commands.LoginUser;
using FluentValidation;
using FluentValidation.Validators;

namespace CleanArchitectureDemo.Api.Common.Validations
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
