using AttendanceTracker.Application.Interfaces;
using AttendanceTracker.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AttendanceTracker.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TokenService> _logger;

        public TokenService(
            IConfiguration config,
            ILogger<TokenService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string CreateToken(User user)
        {
            _logger.LogInformation(
                "CreateToken started for Email: {Email}",
                user.Email);

            try
            {
                var jwtKey = _config["Jwt:Key"];

                if (string.IsNullOrWhiteSpace(jwtKey))
                {
                    _logger.LogError(
                        "JWT Key is missing in configuration");

                    throw new Exception("JWT Key is missing in configuration");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Role?.RoleName ?? string.Empty)
                };

                _logger.LogInformation(
                    "JWT claims prepared successfully for Email: {Email}",
                    user.Email);

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                );

                var creds = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                _logger.LogInformation(
                    "JWT token created successfully for Email: {Email}",
                    user.Email);

                return tokenString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while creating token for Email: {Email}",
                    user.Email);

                throw;
            }
        }
    }
}