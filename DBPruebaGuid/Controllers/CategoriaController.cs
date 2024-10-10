using DBPruebaGuid.Models;
using DBPruebaGuid.Models.ViewModels.Categoria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBPruebaGuid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly DBPruebaGuidContext _DBContext;

        public CategoriaController(DBPruebaGuidContext context)
        {
            _DBContext = context;
        }

        // Método para guardar una nueva categoría
        [HttpPost("save")]
        public async Task<IActionResult> SaveCategoria([FromBody] CategoriaViewModel model)
        {
            // Validar el modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Crear una nueva categoría utilizando el ViewModel
                var nuevaCategoria = new Categoria
                {
                    Id = Guid.NewGuid(), // Generar un nuevo GUID para la categoría
                    Nombre = model.Nombre
                };

                // Agregar la categoría al contexto de la base de datos
                _DBContext.Categorias.Add(nuevaCategoria);

                // Guardar los cambios en la base de datos
                await _DBContext.SaveChangesAsync();

                // Retornar un 201 Created con la categoría creada
                return CreatedAtAction(nameof(SaveCategoria), new { id = nuevaCategoria.Id }, nuevaCategoria);
            }
            catch (Exception ex)
            {
                // En caso de error, retornar un 500 con el mensaje de error
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
