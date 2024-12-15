
using Products.DB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Core.DTO
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
        public double Quantity { get; set; }

        public LicenseType LicenseType { get; set; }

        public ICollection<ProductProject> ProductProjects { get; set; }

        public static explicit operator Product(DB.Product e) => new Product
        {
            Id = e.Id,
            Name = e.Name,
            Quantity = e.Quantity,
            LicenseType = (LicenseType)e.LicenseType,
            ProductProjects = e.ProductProjects,
            Description = e.Description
        };
    }
    public enum LicenseType
    {
        Free,
        Paid,
        OpenSource
    }
}

