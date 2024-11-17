using Auth.Models;
using Microsoft.IdentityModel.Tokens;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Auth
{
    public static class JwtAuthManager
    {
        public const string JWT_SECURITY_KEY = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iL";
        private const int JWT_TOKEN_VALIDITY_MINS = 9999;


        public static AuthResponse GenerateJwtToken(this UserReadDto user)
        {

            // nåværende tid + JWT_TOKEN_VALIDITY_MINS
            var expiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);

            // converts secret key to byte array.
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);


            // defines the claims (key-value pairs) to be injected into the token payload
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)

            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = expiryTimeStamp,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)// telling descriptor which algorithm we should use to generate the token.
            };

            // object used to generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(createdToken);


            var AuthResponse = new AuthResponse
            {
                UserId = user.Id,
                Email = user.UserEmail,
                FirstName = user.FirstName,
                Token = token,
                Role = user.Role,
                ExpiresIn = (int)expiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };

            return AuthResponse;
        }
    }
}
