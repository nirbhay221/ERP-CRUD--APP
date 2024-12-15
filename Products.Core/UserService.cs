using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Products.Core.CustomExceptions;
using Products.Core.DTO;
using Products.Core.Utilities;
using Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(AppDbContext context, IPasswordHasher passwordHasher) {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> SignIn(DTO.User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == Microsoft.AspNet.Identity.PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            return new AuthenticatedUser
            {
                UserName = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };
        }

        public async Task<AuthenticatedUser> Signup(DTO.User user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if ( checkUser != null)
            {
                throw new UserNameAlreadyExistsException("Username already exists");

            }
            var dbUser = new Products.DB.User
            {
                Username = user.Username,
                Password = _passwordHasher.HashPassword(user.Password), 
                Email = user.Email
            };
            await _context.AddAsync(dbUser);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                UserName = dbUser.Username,
                Token = JwtGenerator.GenerateUserToken(dbUser.Username)
            };
        }
    }
}
