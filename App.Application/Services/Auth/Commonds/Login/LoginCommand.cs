using App.Application.Dtos;
using App.Application.Helper;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Services
{
    public class LoginCommand:IRequest<Response<AuthResponseModel>>
    {
        public string UserName { get; init;}
        public string Password { get; init;}

    }
    internal class LoginHanler : IRequestHandler<LoginCommand, Response<AuthResponseModel>>
    {
        private readonly IUserRepository _userRepository;

        public LoginHanler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<AuthResponseModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName == request.UserName,cancellationToken);
            if (user is null)
                return new Response<AuthResponseModel> ("invalid credentials",HttpStatusCode.BadRequest);
            if (!AuthHelper.IsPasswordValid(request.Password, user.PasswordHash, user.PasswordSalt))
                return new Response<AuthResponseModel>("invalid credentials", HttpStatusCode.BadRequest);
            var authUser = new AuthUser(user.Id ,user.UserName);
            var token = AuthHelper.GenerateJwtToken(authUser);
            AuthResponseModel response = new(user.UserName, token);
            return new Response<AuthResponseModel>(response);

        }
    }

}
