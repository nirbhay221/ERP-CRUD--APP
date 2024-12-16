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
            var product = _productsServices.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid Product Data");
            }

            var dbProduct = new Products.DB.Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                LicenseType = (Products.DB.LicenseType)productDto.LicenseType,
                Quantity = productDto.Quantity
            };

            var newProduct = _productsServices.CreateProduct(dbProduct);
            return CreatedAtRoute("GetProduct", new { newProduct.Id }, newProduct);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(Product product) {
            if (product == null)
            {
                return BadRequest("Invalid Product Data");
            }
            _productsServices.DeleteProduct(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditProduct(int id, Product product)
        {
            if (product == null || id != product.Id)
            {
                return BadRequest("Invalid Product Data");
            }

            var updatedProduct = _productsServices.EditProduct(product);
            return Ok(updatedProduct);
        }

    }
}
