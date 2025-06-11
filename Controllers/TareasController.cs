using Microsoft.AspNetCore.Mvc;
using To_do.Modelos;
using To_do.Services;

namespace To_do.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly TareasService _tareasService;

        public TareasController(TareasService tareasService)
        {
            _tareasService = tareasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerTareas()
        {
            var tareas = await _tareasService.ObtenerTareasAsync();
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> ObtenerTareaPorId(Guid id)
        {
            var tarea = await _tareasService.ObtenerTareaPorIdAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return Ok(tarea);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTarea(Guid id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            var resultado = await _tareasService.EditarTareaAsync(id, tarea);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> CrearTarea(Tarea tarea)
        {
            var resultado = await _tareasService.CrearTareaAsync(tarea);

            if (!resultado)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = tarea.Id }, tarea);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(Guid id)
        {
            var resultado = await _tareasService.EliminarTareaAsync(id);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
