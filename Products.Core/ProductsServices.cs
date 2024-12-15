using Microsoft.AspNetCore.Http;
using Products.Core.DTO;
using Products.DB;
using System.Collections.Generic;
using System.Linq;

namespace Products.Core
{
    public class ProductsServices : IProductsServices
    {
        private readonly AppDbContext _context;
        private readonly DB.User _user;

        public ProductsServices(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users.First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public DTO.Product CreateProduct(DB.Product product)
        {
            product.User = _user;
            _context.Products.Add(product);
            _context.SaveChanges();

            return (DTO.Product)product;
        }

        public void DeleteProduct(DTO.Product product)
        {
            var dbProduct = _context.Products.FirstOrDefault(e => e.User.Id == _user.Id && e.Id == product.Id);

            if (dbProduct == null)
                throw new KeyNotFoundException("Product not found or not authorized to delete.");

            _context.Products.Remove(dbProduct);
            _context.SaveChanges();
        }

        public DTO.Product EditProduct(DTO.Product product)
        {
            var dbProduct = _context.Products.FirstOrDefault(e => e.User.Id == _user.Id && e.Id == product.Id);

            if (dbProduct == null)
                throw new KeyNotFoundException("Product not found or not authorized to edit.");

            dbProduct.Description = product.Description;
            dbProduct.Quantity = product.Quantity;
            dbProduct.LicenseType = (DB.LicenseType)product.LicenseType;


            _context.SaveChanges();
            return product;
        }

        public DTO.Product GetProduct(int id)
        {
            var product = _context.Products
                .Where(e => e.User.Id == _user.Id && e.Id == id)
                .Select(e => (DTO.Product)e)
                .FirstOrDefault();

            if (product == null)
                throw new KeyNotFoundException("Product not found or not authorized to access.");

            return product;
        }

        public List<DTO.Product> GetProducts()
        {
            return _context.Products
                .Where(e => e.User.Id == _user.Id)
                .Select(e => (DTO.Product)e)
                .ToList();
        }

        public List<Project> GetProjectsForProduct(int productId)
        {
            var productProjects = _context.ProductProjects
                .Where(pp => pp.ProductId == productId && pp.Product.User.Id == _user.Id)
                .Select(pp => pp.Project)
                .ToList();

            if (!productProjects.Any())
                throw new KeyNotFoundException("No projects found for this product or not authorized to access.");

            return productProjects;
        }
    }
}
