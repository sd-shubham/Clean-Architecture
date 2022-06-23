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
            (await _unitOfWork.UserRepository
                              .AnyAsync(x => x.UserName == request.UserName, cancellationToken))
                              .IsExists($"user already exits");
                      
                             
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
            await Task.Delay(5000);
            var IsInvalid = await _unitOfWork.Complete(cancellationToken)< 1;
            IsInvalid.IsTrue($"failed to register user");
            return new Response<string>("account created successfully", HttpStatusCode.Created, $"welcome {user.UserName}");
        }
    }
}
