using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sample_crm.Application;

namespace sample_crm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("singup")]
        public async Task<ActionResult<AuthResponseDTO>> SignUp(AuthRequestDTO authCredentials)
        {
            var newUser = new IdentityUser { UserName = authCredentials.Email, Email = authCredentials.Email };
            var identityRes = await _userManager.CreateAsync(newUser, authCredentials.Password);

            if(identityRes.Succeeded)
            {
                return BuildToken(authCredentials);
            }
            else
            {
                return BadRequest(identityRes.Errors);
            }
        }

        private AuthResponseDTO BuildToken(AuthRequestDTO authCredentials)
        {
            var claims = new List<Claim>(){
                new Claim("email", authCredentials.Email),
                new Claim("valor especial prueba", "Soy un valor secreto")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, expires: expiration, claims: claims, signingCredentials: credentials);

            return new AuthResponseDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration,
            };
        }
    }
}