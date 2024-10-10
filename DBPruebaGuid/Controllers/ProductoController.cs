using DBPruebaGuid.Models;
using DBPruebaGuid.Models.ViewModels.Producto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBPruebaGuid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DBPruebaGuidContext _DBContext;

        public ProductoController(DBPruebaGuidContext context)
        {
            _DBContext = context;
        }

        // Método para guardar un nuevo producto
        [HttpPost("save")]
        public async Task<IActionResult> SaveProduct([FromBody] ProductoViewModel model)
        {
            // Validar el modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Verificar si la categoría proporcionada existe
                var categoria = await _DBContext.Categorias
                    .FirstOrDefaultAsync(c => c.Id == model.CategoriaId);

                if (categoria == null)
                {
                    // Si la categoría no existe, retornar un 404 Not Found
                    return NotFound(new { message = "La categoría especificada no existe." });
                }

                // Crear un nuevo producto utilizando el ViewModel
                var nuevoProducto = new Producto
                {
                    Id = Guid.NewGuid(), // Generar un nuevo GUID para el producto
                    Nombre = model.Nombre,
                    Precio = model.Precio,
                    CategoriaId = model.CategoriaId // Relacionar con la categoría existente
                };

                // Agregar el producto al contexto de la base de datos
                _DBContext.Productos.Add(nuevoProducto);

                // Guardar los cambios en la base de datos
                await _DBContext.SaveChangesAsync();

                // Retornar un 201 Created con el producto creado
                return CreatedAtAction(nameof(SaveProduct), new { id = nuevoProducto.Id }, nuevoProducto);
            }
            catch (Exception ex)
            {
                // En caso de error, retornar un 500 con el mensaje de error
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
