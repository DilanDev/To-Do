using Microsoft.AspNetCore.Mvc;
using To_do.Modelos;
using To_do.Services;

namespace To_do.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly TareasService _tareasService;

        public TareasController(TareasService tareasService)
        {
            _tareasService = tareasService;
        }

        [HttpGet("ObtenerTareas")]
        public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerTareas()
        {
            var tareas = await _tareasService.ObtenerTareasAsync();
            return Ok(tareas);
        }

        [HttpGet("ObtenerTareaPorId/{id}")]
        public async Task<ActionResult<Tarea>> ObtenerTareaPorId(Guid id)
        {
            var tarea = await _tareasService.ObtenerTareaPorIdAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return Ok(tarea);
        }

        [HttpPut("EditarTarea/{id}")]
        public async Task<IActionResult> EditarTarea(Guid id, [FromBody] Tarea tarea)
        {
            var resultado = await _tareasService.EditarTareaAsync(id, tarea);

            if (!resultado)
                return BadRequest();

            return Ok(tarea); 
        }


        [HttpPost("CrearTarea")]
        public async Task<ActionResult<Tarea>> CrearTarea(Tarea tarea)
        {
            var resultado = await _tareasService.CrearTareaAsync(tarea);

            if (!resultado) 
            {
                return BadRequest(new { ErrorCode = "Error", ErrorMessage = "No se pudo crear la tarea" });
            }

            return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = tarea.Id }, tarea);
        }

        [HttpDelete("EliminarTarea/{id}")]
        public async Task<IActionResult> EliminarTarea(Guid id)
        {
            var resultado = await _tareasService.EliminarTareaAsync(id);

            if (!resultado) 
            {
                return NotFound(new { ErrorCode = "Error", ErrorMessage = "No se pudo eliminar la tarea" });
            }

            return NoContent();
        }
    }
}
