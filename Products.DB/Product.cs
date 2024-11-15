using System.ComponentModel.DataAnnotations;

namespace Products.DB
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }
        public String Description {  get; set; }
        public double Quantity { get; set; } 
    }
}
