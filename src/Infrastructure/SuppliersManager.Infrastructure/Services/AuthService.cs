using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuppliersManager.Application.Features.Auth.Commands;
using SuppliersManager.Application.Helpers;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Settings;
using SuppliersManager.Shared.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuppliersManager.Infrastructure.MongoDBDriver.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly string _pepper;
        private readonly int _iteration;

        public AuthService(
            IUserRepository userRepository, 
            IPasswordHasherSettings passwordHasherSettings, 
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _pepper = passwordHasherSettings.Pepper;
            _iteration = passwordHasherSettings.Iteration;
        }

        public async Task<IResult<TokenResponse>> LoginJWT(TokenCommand command)
        {
            var user = await _userRepository.GetUserByUserNameAsync(command.UserName);

            if (user == null)
            {
                return await Result<TokenResponse>.FailAsync("Usuario no encontrado");
            }
            var passwordHash = PasswordHasher.ComputeHash(
                command.Password, user.PasswordSalt, _pepper, _iteration);
            if (user.Password != passwordHash) return await Result<TokenResponse>.FailAsync("Usuario y/o contraseña incorrectas");

            // Generamos un token según los claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            
            return await Result<TokenResponse>.SuccessAsync(new TokenResponse
            {
                AccessToken = jwt
            }, "OK");
        }
    }
}
