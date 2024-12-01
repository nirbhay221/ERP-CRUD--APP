
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Core.DTO
{
    public class Product
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public double Quantity { get; set; }

        public static explicit operator Product(DB.Product e) => new Product
        {
            Id = e.Id,
            Quantity = e.Quantity,
            Description = e.Description
        };
    }
}
