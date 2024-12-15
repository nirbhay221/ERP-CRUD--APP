
using System.ComponentModel.DataAnnotations;

namespace Products.DB
{
    public class User
    {
        [Key]
        public int Id {  get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }

        public ICollection<Product> Products { get; set; }  

    }
}
