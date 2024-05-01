using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Shared.Extensions;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Lawyeed.API.Lawyeed.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController: ControllerBase
{
    private readonly string secretKey;
    
    public AuthController()
    {
        secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
    } 
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginResource request)
    {  
        if (request.Email == "pepe" && request.Password == "pepe")
        {

            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string createdToken = tokenHandler.WriteToken(tokenConfig);
            
            return Ok(new { token = createdToken });

        }
        else {
            return Ok(StatusCodes.Status401Unauthorized);
        }
    }

}