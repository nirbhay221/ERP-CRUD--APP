using Products.Core.DTO;
using Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core
{
    public interface IUserService
    {
        Task<AuthenticatedUser> Signup(User user);
        Task<AuthenticatedUser> SignIn(User user);

    }
}
