using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PasswordEncryptor.Application.Abstraction.IJWTProvider;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.InfraStructure.JWTProvider
{
    public class JWTProvider(IConfiguration configuration) : IJWTProvider
    {
        public string GenerateAuthToken(User user)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(UserClaims.UserId,user.Id.ToString()),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email,user.Email)
                }),
                Audience = configuration["JWT:Audience"],
                Issuer = configuration["JWT:Issuer"],
                Expires=DateTime.UtcNow.AddMinutes(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha384)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokendescriptor=tokenHandler.CreateToken(descriptor);
            var token=tokenHandler.WriteToken(tokendescriptor);
            return token;
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}
