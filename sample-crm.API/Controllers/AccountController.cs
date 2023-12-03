using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
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
                return await BuildToken(authCredentials);
            }
            else
            {
                return BadRequest(identityRes.Errors);
            }
        }

        private async Task<AuthResponseDTO> BuildToken(AuthRequestDTO authCredentials)
        {
            var claims = new List<Claim>(){
                new Claim("email", authCredentials.Email),
                new Claim("valor especial prueba", "Soy un valor secreto")
            };

            var user = await _userManager.FindByEmailAsync(authCredentials.Email);
            var claimsDb = await _userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDb);

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

        [HttpPost("login")]
        public async Task <ActionResult<AuthResponseDTO>> Login(AuthRequestDTO authCredentials)
        {
            var identityRes = await _signInManager.PasswordSignInAsync(authCredentials.Email, authCredentials.Password,
                isPersistent: false, lockoutOnFailure: false);
            
            if(identityRes.Succeeded)
            {
                return await BuildToken(authCredentials);
            }
            else
            {
                return BadRequest("Incorrect login!");
            }
        }

        [HttpGet("token/refresh")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AuthResponseDTO>> Refresh()
        {
            var userEmailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var userEmail = userEmailClaim.Value;
            var userCredentials = new AuthRequestDTO()
            {
                Email = userEmail
            };

            return await BuildToken(userCredentials);
        }

        [HttpPost("grant/admin")]
        public async Task<ActionResult> GrantAdmin(GrantAdminAuthorizationDTO grantAdminDTO)
        {
            var user = await _userManager.FindByEmailAsync(grantAdminDTO.Email);
            await _userManager.AddClaimAsync(user, new Claim("admin", ""));
            return NoContent();
        }

        [HttpPost("revoke/admin")]
        public async Task<ActionResult> RevokeAdmin(GrantAdminAuthorizationDTO grantAdminDTO)
        {
            var user = await _userManager.FindByEmailAsync(grantAdminDTO.Email);
            await _userManager.RemoveClaimAsync(user, new Claim("admin", null));
            return NoContent();
        }
    }
}