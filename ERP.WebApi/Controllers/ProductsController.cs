using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core;
using Products.Core.DTO;

namespace ERP.WebAPI.Controllers
{
    [Authorize]
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
        public IActionResult CreateProduct(Product productDto)
        {
            var dbProduct = new Products.DB.Product
            {
                Description = productDto.Description,
                Quantity = productDto.Quantity
            };

            var newProduct = _productsServices.CreateProduct(dbProduct);
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
