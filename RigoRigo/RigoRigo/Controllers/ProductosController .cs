using Microsoft.AspNetCore.Mvc;
using RigoRigo.Modelos.Model;
using WebApplication1;
using System.Linq;

namespace RigoRigo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<ProductoRigoModel>>> GetListaProductos()
        {
            var lstProductos = new List<ProductoRigoModel>();

            return Ok(lstProductos);
        }

        [HttpPost]
        public async Task<ActionResult> GuardarDetallePedido([FromBody] DetallePedidoModel detallePedido)
        {
            // Validación simple de que el objeto no es nulo
            if (detallePedido == null)
            {
                return BadRequest("El detalle del pedido no puede ser nulo.");
            }

            // Validación adicional (opcional): verificar que los IDs de entidades relacionadas sean válidos
            if (detallePedido.IdPedidoEncabezado <= 0 || detallePedido.IdProductosRigo <= 0 || detallePedido.Cantidad <= 0)
            {
                return BadRequest("Datos inválidos para el detalle del pedido.");
            }

            // Validar que el ValorTotal sea coherente (opcional)
            if (detallePedido.ValorTotal <= 0)
            {
                return BadRequest("El valor total debe ser mayor a 0.");
            }

            try
            {
                

                // Retornar el detalle guardado con un código de estado 201 (Created)
                return CreatedAtAction(nameof(GuardarDetallePedido), new { id = detallePedido.Id }, detallePedido);
            }
            catch (Exception ex)
            {
                // Capturar errores durante el guardado y retornar un mensaje de error
                return StatusCode(500, $"Error al guardar el detalle del pedido: {ex.Message}");
            }
        }
    }
}
