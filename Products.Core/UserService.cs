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

        public async Task<AuthenticatedUser> SignIn(User user)
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

        public async Task<AuthenticatedUser> Signup(User user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if ( checkUser != null)
            {
                throw new UserNameAlreadyExistsException("Username already exists");

            }
            user.Password = _passwordHasher.HashPassword(user.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                UserName = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };
        }
    }
}
