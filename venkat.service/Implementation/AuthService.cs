using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using venkat.Common.DTOs;

using venkat.service.Abstraction;
using venkat.store.Abstraction;

namespace venkat.service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthStore _authStore;

        private readonly IConfiguration _configuration;

        public AuthService(
            IAuthStore authStore,
            IConfiguration configuration)
        {
            _authStore = authStore;

            _configuration = configuration;
        }

        public async Task<LoginResponse?> LoginAsync(
            LoginRequest request)
        {
            var user =
                await _authStore.LoginAsync(request);

            if (user == null)
            {
                return null;
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    user.UserName),

                new Claim(
                    ClaimTypes.Role,
                    user.RoleName)
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]!));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims:
                        claims,

                    expires:
                        DateTime.Now.AddMinutes(60),

                    signingCredentials:
                        creds);

            user.Token =
                new JwtSecurityTokenHandler()
                .WriteToken(token);

            return user;
        }
    }
}