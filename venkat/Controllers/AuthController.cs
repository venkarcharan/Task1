using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

using venkat.Common.DTOs;

namespace venkat.API.Controllers
{
    [Route("api/[controller]")]

    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]

        public IActionResult Login(
            LoginRequest request)
        {
            // STATIC USERNAME & PASSWORD

            string userName = "admin";

            string password = "123";


            // HASH PASSWORD

            string storedHash =
                BCrypt.Net.BCrypt.HashPassword(password);


            // VERIFY PASSWORD

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    storedHash);


            // VALIDATE USER

            if (request.UserName == userName
                && isPasswordValid)
            {
                var claims = new[]
                {
                    new Claim(
                        ClaimTypes.Name,
                        request.UserName),

                    new Claim(
                        ClaimTypes.Role,
                        "Admin")
                };


                // JWT KEY

                var key =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            _configuration["Jwt:Key"]!));


                // SIGNING CREDENTIALS

                var credentials =
                    new SigningCredentials(
                        key,
                        SecurityAlgorithms.HmacSha256);


                // CREATE TOKEN

                var token =
                    new JwtSecurityToken(
                        issuer:
                            _configuration["Jwt:Issuer"],

                        audience:
                            _configuration["Jwt:Audience"],

                        claims: claims,

                        expires:
                            DateTime.Now.AddMinutes(60),

                        signingCredentials:
                            credentials
                    );


                // CONVERT TOKEN TO STRING

                var jwtToken =
                    new JwtSecurityTokenHandler()
                        .WriteToken(token);


                return Ok(
                    new
                    {
                        Token = jwtToken
                    });
            }


            return Unauthorized(
                "Invalid Credentials");
        }
    }
}