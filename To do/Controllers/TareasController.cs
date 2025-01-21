using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_do.Contexto;
using To_do.Modelos;

namespace To_do.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly AplicacionDbContexto _context;

        public TareasController(AplicacionDbContexto context)
        {
            _context = context;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
        {
            var tareas = await _context.Tareas
                .Include(t => t.Pasos) 
                .Include(t => t.DetalleTarea) 
                .ToListAsync();

            return Ok(new { success = true, data = tareas });
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(Guid id)
        {
            var tarea = await _context.Tareas
                .Include(t => t.Pasos)
                .Include(t => t.DetalleTarea)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
            {
                return NotFound(new { success = false, message = "La tarea no existe." });
            }

            return Ok(new { success = true, data = tarea });
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(Guid id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest(new { success = false, message = "El ID de la tarea no " +
                    "coincide con el proporcionado en la URL." });
            }

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TareaExists(id))
                {
                    return NotFound(new { success = false, message = "La tarea no existe." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Error de concurrencia " +
                        "al intentar actualizar la tarea.", error = ex.Message });
                }
            }

            return NoContent();
        }

        // POST: api/Tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, errors = ModelState });
            }

            try
            {
                tarea.Id = Guid.NewGuid();
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTarea", new { id = tarea.Id }, new { success = true, data = tarea });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { success = false, message = "No se pudo guardar " +
                    "la tarea. Verifica los datos ingresados.", error = ex.Message });
            }
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(Guid id)
        {
            var tarea = await _context.Tareas
                .Include(t => t.Pasos)
                .Include(t => t.DetalleTarea)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
            {
                return NotFound(new { success = false, message = "La tarea no existe." });
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TareaExists(Guid id) => _context.Tareas.Any(t => t.Id == id);
    }
}
