using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers
{
    // service for the jwt
    public class JwtService
    {
        private string securitykey = "this is a very secure key"; // a key that will be used for encryption
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey)); // making a symmetric key
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature); //using sha 256 algo for encryption
            var header = new JwtHeader(credentials); // creating the header of the token using credentials

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1)); //setting the issuer and expiration to the payload of the token

            var securityToken = new JwtSecurityToken(header, payload); // creating the token using the header and payload

            return (new JwtSecurityTokenHandler().WriteToken(securityToken)); // returning the security token
        }

        public JwtSecurityToken Verify(string jwt) // Verifying the jwt
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // creating a new jwtsecurity handler
            var key = Encoding.ASCII.GetBytes(securitykey); // encoding the secured key to ASCII
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters // validating the token
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            },
                out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
