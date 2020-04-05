using CarRent.Entities;
using CarRent.WebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarRent.WebApi.Helpers
{
    public class AuthService
    {
        public Token CreateAccessToken(UserEntity user, IConfiguration config)
        {
            var claims = new List<Claim>
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
            var claimsIdentity = new ClaimsIdentity(claims, "AccessToken", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            // create a JWT token
            var jwt = new JwtSecurityToken(
            issuer: config.GetValue<string>("JwtAuthentication:ValidIssuer"),
            audience: config.GetValue<string>("JwtAuthentication:ValidAudience"),
            notBefore: now,
            claims: claimsIdentity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(config.GetValue<int>("JwtAuthentication:LifeTimeAccessToken"))),
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(config.GetValue<string>("JwtAuthentication:SecurityKey")), SecurityAlgorithms.HmacSha256));
            return new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                RefreshToken = GenerateRefreshToken()
            };
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("JwtAuthentication:SecurityKey"))),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

//public string CreateRefreshToken(string email, IConfiguration config)
//{
//    var claims = new List<Claim>
//                {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
//                new Claim(ClaimsIdentity.DefaultRoleClaimType, email)
//                };
//    var claimsIdentity = new ClaimsIdentity(claims, "RefreshToken", ClaimsIdentity.DefaultNameClaimType,
//    ClaimsIdentity.DefaultRoleClaimType);

//    var now = DateTime.UtcNow;

//    var jwt = new JwtSecurityToken(
//    issuer: config.GetValue<string>("JwtAuthentication:ValidIssuer"),
//    audience: config.GetValue<string>("JwtAuthentication:ValidAudience"),
//    notBefore: now,
//    claims: claimsIdentity.Claims,
//    expires: now.Add(TimeSpan.FromMinutes(config.GetValue<int>("JwtAuthentication:LifeTimeRefreshToken"))),
//    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(config.GetValue<string>("JwtAuthentication:SecurityKey")), SecurityAlgorithms.HmacSha256));
//    var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwt);

//    return refreshToken.Split(".").Last();
//}