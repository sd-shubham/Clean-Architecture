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
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            _unitOfWork.UserRepository.Add(user);
            var result = await _unitOfWork.UserRepository.SaveChangesAsync(cancellationToken)>0;
            //if (!isSave) return Error<string>.BadRequest("failed to register user");
            result.IsNotTrue($"failed to register user");
            return new Response<string>("account created successfully", HttpStatusCode.Created, $"welcome {user.UserName}");
        }
    }
}
