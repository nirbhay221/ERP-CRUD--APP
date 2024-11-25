using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.DB
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }
        public String Description {  get; set; }
        public double Quantity { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
