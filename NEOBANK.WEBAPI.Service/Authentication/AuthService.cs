using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NEOBANK.WEBAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.Service.Authentication
{
    public class AuthService
    {
        public string AccessToken { get; }

        public AuthService(LoginModel model, IConfiguration _config)
        {
            TokenModel token = GetTokenInfo(model);
            AccessToken = GenerateAccessToken(token, _config);
        }

        public AuthService() { }

        private TokenModel GetTokenInfo(LoginModel model)
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.userId = model.userId;
            tokenModel.username = model.username;
            tokenModel.email = model.email;
            return tokenModel;
        }

        private string GenerateAccessToken(TokenModel token, IConfiguration _config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", token.userId),
                new Claim("username", token.username),
                new Claim("email", token.email),
            };

            var tokenKey = new JwtSecurityToken(
                null,
                null,
                claims,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenKey);
        }

        public string GenerateRandomKey()
        {
            byte[] keyBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }

            string base64Key = Convert.ToBase64String(keyBytes);

            return base64Key;
        }
    }
}
