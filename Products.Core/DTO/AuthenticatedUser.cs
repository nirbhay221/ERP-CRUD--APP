using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.DTO
{
    public class AuthenticatedUser
    {
        public string Token {  get; set; }
        public string UserName { get; set; }
    }
}
