using System.Threading.Tasks;
using AOA.API.Data;
using AOA.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AOA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registar (string username, string password)
        {
            //validate request
            //send the username that user is providing (turn to lowercase)

            username = username.ToLower();
           
           if( await _repo.UserExists(username))
                return BadRequest("Username already exists");

                
            var UserToCreate = new User(){
                
                Username = username

            };

            var CreatedUser = await _repo.Registar(UserToCreate, password);

            return StatusCode(201);
        }
    }
}