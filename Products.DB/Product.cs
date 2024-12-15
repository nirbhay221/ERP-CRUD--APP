using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.DB
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        public String Description {  get; set; }

        [Required]
        public LicenseType LicenseType { get; set; }
        public double Quantity { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<ProductProject> ProductProjects { get; set; }
    }

    public enum LicenseType { 
        Free,
        Paid,
        OpenSource
    }
}
