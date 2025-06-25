using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {

            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "ADMIN")
            {
                return Ok(await _productService.GetAllAsync());
            }
            else
            {
                if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

                var entitiId = Guid.Parse(entitiIdClaim);
                var productos = await _productService.GetByEntitiAsync(entitiId);
                return Ok(productos);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateDto dto)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if(role == "vaneaf")
            {
                return NotFound("Usted no tiene acceso a esta funcion.");
            }

            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();
            dto.EntitiId = Guid.Parse(entitiIdClaim);

            var id = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProductUpdateDto product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("El ID del producto no coincide.");
            }

            try
            {
                var updatedProduct = await _productService.UpdateAsync(product);
                if (updatedProduct == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                return Ok(new { message = $"{updatedProduct}" });  // Retorna el producto actualizado
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);  // Maneja los errores
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _productService.DeleteAsync(id);
            return Ok(new { message = $"{success}" });
        }

        // 🔍 Buscar por nombre
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

            var entitiId = Guid.Parse(entitiIdClaim);

            var results = await _productService.SearchByNameAsync(name, entitiId);
            return Ok(results);
        }

        // 🔍 Buscar por código de barras
        [HttpGet("barcode/{code}")]
        public async Task<IActionResult> GetByBarCode(string code)
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

            var entitiId = Guid.Parse(entitiIdClaim);
            var product = await _productService.GetByBarCodeAsync(code, entitiId);
            
            return Ok(product);
        }

        [HttpPut("{id}/increaseStock")]
        public async Task<IActionResult> IncreaseStock(Guid id, [FromBody] int quantity)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (role == "vaneaf")
            {
                return NotFound("Usted no tiene acceso a esta funcion.");
            }
            if (quantity <= 0)
            {
                return BadRequest("La cantidad a aumentar debe ser mayor que 0.");
            }

            try
            {
                var updatedProduct = await _productService.IncreaseStockAsync(id, quantity);
                if (updatedProduct == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                return Ok(updatedProduct); // Retorna el producto actualizado con el nuevo stock
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);  // Manejo de errores
            }
        }

        [HttpPut("{id}/decreaseStock")]
        public async Task<IActionResult> DecreaseStock(Guid id, [FromBody] int quantity)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (role == "vaneaf")
            {
                return NotFound("Usted no tiene acceso a esta funcion.");
            }
            if (quantity <= 0)
            {
                return BadRequest("La cantidad a reducir debe ser mayor que 0.");
            }

            try
            {
                var updatedProduct = await _productService.DecreaseStockAsync(id, quantity);
                if (updatedProduct == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                return Ok(updatedProduct); // Retorna el producto actualizado con el nuevo stock
            }
            catch (InvalidOperationException ex)
            {
                // Devolver un BadRequest con el mensaje de la excepción
                return BadRequest(ex.Message); // El mensaje de la excepción se enviará al frontend
            }
            catch (Exception ex)
            {
                // Si ocurre otro tipo de error, devuelve un error 500
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
