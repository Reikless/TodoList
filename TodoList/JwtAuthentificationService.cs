using TodoList.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TodoList
{
    public class JwtAuthentificationService : IJwtAuthentificationService
    {

        private readonly TodoListContext _context;

        public JwtAuthentificationService(TodoListContext context)
        {
            _context = context;
        }

        [HttpGet]
        public User Authenticate(string email, string password)
        {
            var result = _context.User.Where(u => u.Email.ToUpper().Equals(email.ToUpper())
                && u.Password.Equals(password)).FirstOrDefault();
            if (result == null)
            {
                return UnauthorizedResult();
  ;          }
            return result;
        }

        private User UnauthorizedResult()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(string secret, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
