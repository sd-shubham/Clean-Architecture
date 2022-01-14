using FluentValidation;
using App.Application.Interfaces;

namespace App.Application.Services
{
    public class RegisterUserValidator: AbstractValidator<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public RegisterUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(p => p.UserName).NotEmpty().WithMessage("user name is required")
                    .NotNull()
                    .MustAsync(IsUserExists).WithMessage("user allready exists");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password is required")
                                    .NotNull();
        }
        public async Task<bool> IsUserExists(string userName,CancellationToken cancellationToken)
        {
            return !await _userRepository.AnyAsync(x => x.UserName == userName, cancellationToken);
        }

    }
}
