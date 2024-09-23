using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuppliersManager.IntegrationTesting
{
    public static class JWTHelperFake
    {
       public static string GetFakeToken()
        {
            // Generamos un token según los claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "test"),
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.Email, "test@example.com"),
                new Claim(ClaimTypes.GivenName, "test"),
                new Claim(ClaimTypes.Surname, "test"),
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("giTXn5mA&yeTMDbKF7dGpV09$u&eKc2EkHmisG0t"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: "SupplierManager.com",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
