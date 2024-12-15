using System.ComponentModel.DataAnnotations;
using Products.DB;

namespace Projects.DB
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
       // public ICollection<ProductProject> ProductProjects { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
    }
}
