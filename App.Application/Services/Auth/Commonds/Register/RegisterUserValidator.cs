using FluentValidation;
using App.Application.Interfaces;

namespace App.Application.Services
{
    public class RegisterUserValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {

            RuleFor(p => p.UserName).NotEmpty().WithMessage("user name is required")
                    .NotNull();
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password is required")
                                    .NotNull();
        }

    }
}
