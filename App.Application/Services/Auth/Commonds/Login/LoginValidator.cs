using FluentValidation;
using App.Application.Interfaces;

namespace App.Application.Services
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("password can not be blank")
                                     .NotNull();
            RuleFor(x => x.UserName).NotEmpty().WithMessage("user name can not be empty")
                                    .NotNull();
        }
    }
}
