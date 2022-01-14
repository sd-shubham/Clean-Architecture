using App.Application.Interfaces;
using App.Application.Extensions;
using System.Security.Claims;

namespace App.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public int UserId => _httpContextAccessor.HttpContext?.User?.Get(ClaimTypes.NameIdentifier) ?? 0;
    }
}
