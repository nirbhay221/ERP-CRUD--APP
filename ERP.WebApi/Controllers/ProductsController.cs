using Microsoft.AspNetCore.Mvc;
using Products.Core;
using Products.DB;

namespace ERP.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductsServices _productsServices;
        public ProductsController(IProductsServices productsServices)
        {
            _productsServices = productsServices; 
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productsServices.GetProducts());

        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_productsServices.GetProduct(id));
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var newProduct = _productsServices.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { newProduct.Id }, newProduct);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(Product product) { 
            _productsServices.DeleteProduct(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult EditProduct(Product product)
        {
            return Ok(_productsServices.EditProduct(product));
        }
    }
}
