using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureDemo.Application.User.Commands.RegisterUser;
using FluentValidation;
using FluentValidation.Validators;

namespace CleanArchitectureDemo.Api.Common.Validations
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
            RuleFor(x => x.PhoneNumber)
                .NotEmpty();
            RuleFor(x => x.Password)
               .NotEmpty();
            
        }
    }
}
