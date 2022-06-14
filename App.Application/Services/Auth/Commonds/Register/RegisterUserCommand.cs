using App.Application.Exceptions;
using App.Application.Helper;
using App.Application.Interfaces;
using App.Domain.Enities;
using App.Domain.Events;
using MediatR;
using System.Net;

namespace App.Application.Services
{
    public record RegisterUserCommand(string UserName, string Password) : IRequest<Response<string>>;
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
               // Done = false
            };
            user.DomainEvents.Add(new UserCreateEvent(user));
            _userRepository.Add(user);
            var result = await _userRepository.SaveChangesAsync(cancellationToken)>0;
            //if (!isSave) return Error<string>.BadRequest("failed to register user");
            result.IsNotTrue($"failed to register user");
            return new Response<string>("account created successfully", HttpStatusCode.Created, $"welcome {user.UserName}");
        }
    }
}
