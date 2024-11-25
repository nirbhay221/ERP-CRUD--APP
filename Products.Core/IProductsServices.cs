
using Products.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core
{
    public interface IProductsServices
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        Product CreateProduct(DB.Product product);
        void DeleteProduct(Product product);
        Product EditProduct(Product product);
    }
}
