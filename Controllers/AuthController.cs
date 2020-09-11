using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using AOA.API.Data;
using AOA.API.Dtos;
using AOA.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace AOA.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registar (UserForRegistarDto userForRegistarDto)
        {
            // if(!ModelState.IsValid){
            //     return BadRequest(ModelState);
            // }
            

            userForRegistarDto.Username = userForRegistarDto.Username.ToLower();
           
           if( await _repo.UserExists(userForRegistarDto.Username))
                return BadRequest("Username already exists");

                
            var UserToCreate = new User(){
                
                Username = userForRegistarDto.Username

            };

            var CreatedUser = await _repo.Registar(UserToCreate, userForRegistarDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto){

            var Userrepo = await _repo.Login(userForLoginDto.Username.ToLower(),userForLoginDto.Password);
           
            if (Userrepo == null)
                return Unauthorized();

            var claims = new []//special class claim
            {
                new Claim(ClaimTypes.NameIdentifier, Userrepo.Id.ToString()), //kao kontsruktor od klasta, prv  argument - tip, vtor-vrednost
                new Claim(ClaimTypes.Name, Userrepo.Username)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}