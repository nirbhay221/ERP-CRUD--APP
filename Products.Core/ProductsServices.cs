using Products.DB;

namespace Products.Core
{
    public class ProductsServices : IProductsServices
    {
        private AppDbContext _context;
        public ProductsServices(AppDbContext context)
        {
            _context = context;
        }

        public Product CreateProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();

            return product;
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges(); 
        }

        public Product EditProduct(Product product)
        {
            var dbExpense = _context.Products.First(e => e.Id == product.Id);
            dbExpense.Description = product.Description;
            dbExpense.Quantity = product.Quantity;
            _context.SaveChanges();
            return dbExpense;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.First(e => e.Id == id);    
        }
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
    }
}
