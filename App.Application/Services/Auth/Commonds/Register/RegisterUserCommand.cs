using AutoMapper;
using App.Application.Helper;
using App.Application.Interfaces;
using App.Domain.Enities;
using MediatR;

namespace App.Application.Services
{
    public class RegisterUserCommand : IRequest<Response<string>>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }

    internal class Handler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var password = AuthHelper.CreatePassword(request.Password);
            User user = new()
            {
                UserName = request.UserName,
               PasswordHash = password.hash,
               PasswordSalt = password.salt,
            };
             _userRepository.Add(user);
           bool isSave = await _userRepository.SaveChangesAsync(cancellationToken) > 0;
            if (!isSave) return new Response<string>("failed to register user");
          return new Response<string>("account created successfully",$"welcome {user.UserName}");
        }
    }
}
