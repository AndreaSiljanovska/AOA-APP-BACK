using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using AOA.API.Models;

namespace AOA.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Registar(User user, string password);
         
         Task<User> Login(string username, string password);
         
         Task<bool> UserExists(string username);
    }
}