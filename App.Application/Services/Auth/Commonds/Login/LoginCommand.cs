using App.Application.Dtos;
using App.Application.Exceptions;
using App.Application.Helper;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Services
{
    public record LoginCommand(string UserName, string Password) : IRequest<Response<AuthResponseModel>>;
    internal class LoginHanler : IRequestHandler<LoginCommand, Response<AuthResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginHanler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<AuthResponseModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            user.IsNull($"invalid credentials ");
            AuthHelper.IsPasswordInValid(request.Password, user.PasswordHash, user.PasswordSalt)
                      .IsInvalid($"invalid credentials");
            var authUser = new AuthUser(user.Id, user.UserName);
            var token = AuthHelper.GenerateJwtToken(authUser);
            AuthResponseModel response = new(user.UserName, token);
            return new Response<AuthResponseModel>(response);


        }
    }

}
