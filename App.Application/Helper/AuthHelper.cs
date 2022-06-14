using App.Application.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace App.Application.Helper
{
    internal static class AuthHelper
    {
        public static (byte[] hash, byte[] salt) CreatePassword(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }
        public static bool IsPasswordValid(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }

        public static string GenerateJwtToken(AuthUser user)
        {
            List<Claim> cliams = new()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("your secure key that should be much longer"));
            SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken token = new(
                claims: cliams,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(10)
                // set issuer audiance accordingly
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
