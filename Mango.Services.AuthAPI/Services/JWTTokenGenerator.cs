using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;    // for accessing all the key values from the appsettings
        public JWTTokenGenerator(IOptions<JwtOptions> jwtOptions)   // can't directly inject with JwtOptions bcoz we don't have a class implementation for it, we have configured it with builder
        {

            _jwtOptions = jwtOptions.Value;

        }

        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {   
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),    // could also use strings "Name", "Email" for types directly
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName),
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // tokendescriptor has config properties for the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
