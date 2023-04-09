using App.Application.Dtos;
using App.Application.Exceptions;
using App.Application.Helper;
using App.Application.Interfaces;
using App.Domain.Enities;
using App.Domain.Events;
using MediatR;
using System.Net;

namespace App.Application.Services
{
    // we can move registercommand to diffrent folder
    public record RegisterUserCommand(string UserName, string Password, UserAddressDto UserAddress) : IRequest<string>;
    internal class Handler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            (await _unitOfWork.UserRepository
                              .AnyAsync(x => x.UserName == request.UserName, cancellationToken))
                              .IsExists($"user already exits");
            var password = AuthHelper.CreatePassword(request.Password);
            var user = User.CreateUser(DateOnly.MinValue, request.UserName,
                               password.hash, password.salt);
            var userAddress = UserAddress.AddUserAddress(request.UserAddress.Pincode);
            user.AddUserAddress(userAddress);
            //user.DomainEvents.Add(new UserCreateEvent(user));
            _unitOfWork.UserRepository.Add(user);
            var IsInvalid = await _unitOfWork.Complete(cancellationToken) < 1;
            IsInvalid.IsTrue($"failed to register user");
            return $"welcome {user.UserName}";
        }
    }
}
