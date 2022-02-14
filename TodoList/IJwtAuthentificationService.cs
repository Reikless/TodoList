using System.Security.Claims;
using TodoList.Models;

namespace TodoList
{
    public interface IJwtAuthentificationService
    {
        User Authenticate(string email, string password);
        string GenerateToken(string secret, List<Claim> claims);
    }
}
