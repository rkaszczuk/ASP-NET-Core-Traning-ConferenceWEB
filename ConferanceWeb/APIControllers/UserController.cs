using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ConferanceWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            if (username.ToLower() == "admin" && password == "admin123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superTajnyKlucz123$"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDesc = new SecurityTokenDescriptor
                {
                    Issuer = "http://localhost:54658",
                    Audience = "http://localhost:54658",
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.Now.AddMinutes(15),
                    SigningCredentials = signinCredentials
                };
                var token = tokenHandler.CreateToken(tokenDesc);
                return Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return BadRequest("Nazwa użytkownika lub hasło są niepoprawne");
            }
        }
        [Authorize]
        [HttpGet("GetCurrentUsername")]
        public IActionResult GetCurrentUsername()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            return Ok(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
        }
    }
}