using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PantAPIDreamsStyle.models.sale;
using PantAPIDreamsStyle.repositories;

namespace ApiPersons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository saleRepository;

        public SaleController(ISaleRepository saleRepository) {
            this.saleRepository = saleRepository;
        }

        [HttpGet("sales/")]
        public async Task<IActionResult> GetSales()
        {
            var sales = await saleRepository.getListSales();
            try
            {
                return (sales == null || sales.Count() == 0) ? NotFound("No se encontraron pantalones.") : Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpGet("users/sales/{idSale}")]
        public async Task<IActionResult> GetSale(int idSale)
        {
            var sale = await saleRepository.getSale(idSale);
            try
            {
                if (idSale <= 0)
                {
                    return BadRequest("El ID del pantalon no es válido.");
                }
                if (sale == null)
                {
                    return NotFound($"No se encontro el pantalón: {idSale}");
                }
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpGet("user/{idUser}/sales/")]
        public async Task<IActionResult> GetSalesUser(int idUser)
        {
            var sales = await saleRepository.getListSalesUser(idUser);
            try
            {
                return (sales == null || sales.Count() == 0) ? NotFound("No se encontraron pantalones.") : Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno al obtener la lista de pantalones.");
            }
        }

        [HttpPost("add-sale")]
        public async Task<IActionResult> AddSale([FromBody] Sale sale)
        {
            if (sale == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El cuerpo de la solicitud no puede estar vacío."
                });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationProblemDetails(ModelState)
                {
                    Title = "La solicitud contiene errores de validación."
                });
            }
            var addedPant = await saleRepository.addSale(sale);
            return CreatedAtAction("GetPant", addedPant);
        }

        [HttpPost("sales/add-sale-details")]
        public async Task<IActionResult> AddSaleDetails([FromBody] SaleDetails saleDetails)
        {
            if (saleDetails == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El cuerpo de la solicitud no puede estar vacío."
                });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationProblemDetails(ModelState)
                {
                    Title = "La solicitud contiene errores de validación."
                });
            }
            var addedPant = await saleRepository.addSaleDetails(saleDetails);
            return CreatedAtAction("GetPant", addedPant);
        }

        [HttpDelete("remove-sale/{sale_id}")]
        public async Task<IActionResult> RemoveSale(int sale_id)
        {
            if (sale_id <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = "El identificador de la venta no es válido."
                });
            }
            var deletedPant = await saleRepository.removeSale(sale_id);
            if (deletedPant != true)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Recurso no encontrado",
                    Detail = "La venta con el ID especificado no existe."
                });
            }
            return NoContent();
        }
    }
}
