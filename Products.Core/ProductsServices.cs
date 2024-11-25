using Microsoft.AspNetCore.Http;
using Products.Core.DTO;

namespace Products.Core
{
    public class ProductsServices : IProductsServices
    {
        private DB.AppDbContext _context;
        private readonly DB.User _user;
        public ProductsServices(DB.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users.First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);


        }

        public Product CreateProduct(DB.Product product)
        {
            product.User = _user;
            _context.Add(product);
            _context.SaveChanges();

            return (Product)product;
        }

        public void DeleteProduct(Product product)
        {
            var dbProduct = _context.Products.First(e => e.User.Id == _user.Id && e.Id == product.Id);

            _context.Products.Remove(dbProduct);
            _context.SaveChanges(); 
        }

        public Product EditProduct(Product product)
        {
            var dbProduct = _context.Products.First(e => e.User.Id == _user.Id && e.Id == product.Id);
            dbProduct.Description = product.Description;
            dbProduct.Quantity = product.Quantity;
            _context.SaveChanges();
            return product;
        }

        public Product GetProduct(int id) =>
        
            _context.Products.Where(e => e.User.Id == _user.Id && e.Id == id)
                .Select(e => (Product)e)
                .First();
        
        public List<Product> GetProducts()
        =>

            _context.Products.Where(e => e.User.Id == _user.Id)
                .Select(e => (Product)e)
                .ToList();
    }
}
