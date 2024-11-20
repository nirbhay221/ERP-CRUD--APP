using System.ComponentModel.DataAnnotations;

namespace Projects.DB
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public String Description { get; set; }
        public double Quantity { get; set; }
    }
}
