﻿using MEJORA.Application.Dtos.Auth.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MEJORA.Infrastructure.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(ClaimnsRequest request)
        {
            // Cambiar el tiempo de expiración a 1 minuto
            return GenerateTokenInternal(request, TimeSpan.FromDays(5));
        }

        private string GenerateTokenInternal(ClaimnsRequest request, TimeSpan expirationTime)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, request.Username),
                new Claim(ClaimTypes.Email, request.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(expirationTime),
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
