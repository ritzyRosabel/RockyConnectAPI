﻿using RockyConnectBackend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RockyConnectBackend.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        //  private readonly DatabaseContext _context;

        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AppUser use)
        {
            if (use != null && use.AppID != null && use.AppSecret !=null)
            {

                var user = RockyConnectBackend.Data.UserData.GetAppUser(use.AppID, use.AppSecret);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                      new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                      new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                      new Claim("AppID", user.AppID),
                      new Claim("AppSecret", user.AppSecret)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(120),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}