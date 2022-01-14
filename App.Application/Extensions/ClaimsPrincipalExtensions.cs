using System.Security.Claims;

namespace App.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int Get(this ClaimsPrincipal claimsPrincipal, string type)
        {
            if(claimsPrincipal is null) throw new ArgumentNullException(nameof(claimsPrincipal));
            var currentId = claimsPrincipal.FindFirst(type)?.Value;
            return  currentId is null ? 0 : int.Parse(currentId);
        }
    }
}
