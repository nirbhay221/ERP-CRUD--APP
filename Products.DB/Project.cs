
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.DB
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
        
        public ICollection<ProductProject> ProductProjects { get; set; }
        
        public ICollection<UserProject> UserProjects { get; set; }
    }
}
