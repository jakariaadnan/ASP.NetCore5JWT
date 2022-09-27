using Auth.Model;
using Auth.Services.JWT.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Services.JWT
{
    public class JwtFactoryService: IJwtFactoryService
    {
        public readonly JwtIssuerOptions _jwtIssuerOption;

        public JwtFactoryService(IOptions<JwtIssuerOptions> jwtIssuerOption)
        {
            _jwtIssuerOption = jwtIssuerOption.Value;
        }

        public async Task<string> GenerateToken(string userName, IList<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOption.JtiGenerator()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOption.IssuedAt).ToString(), ClaimValueTypes.Integer64));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var jwt = new JwtSecurityToken(
                issuer: _jwtIssuerOption.Issuer,
                audience: _jwtIssuerOption.Audience,
                claims: claims.ToArray(),
                notBefore: _jwtIssuerOption.NotBefore,
                expires: _jwtIssuerOption.Expiration,
                signingCredentials: _jwtIssuerOption.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
        private static long ToUnixEpochDate(DateTime date)
         => (long)Math.Round((date.ToUniversalTime() -
                              new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                             .TotalSeconds);
    }
}
