using App.Application.Dtos;
using App.Application.Exceptions;
using App.Application.Helper;
using App.Application.Interfaces;
using MediatR;

namespace App.Application.Services
{
    public record LoginCommand(string UserName, string Password) : IRequest<AuthResponseModel>;
    internal class LoginHanler : IRequestHandler<LoginCommand, AuthResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginHanler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            (user is null).IsTrue($"Invalid Credential");
            AuthHelper.IsPasswordInValid(request.Password, user.PasswordHash, user.PasswordSalt)
                      .IsTrue($"invalid credentials");
            var authUser = new AuthUser(user.Id, user.UserName);
            var token = AuthHelper.GenerateJwtToken(authUser);
            AuthResponseModel response = new(user.UserName, token);
            return response;


        }
    }

}
