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
    public class PasoTareasController : ControllerBase
    {
        private readonly AplicacionDbContexto _context;

        public PasoTareasController(AplicacionDbContexto context)
        {
            _context = context;
        }

        // GET: api/PasoTareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasoTarea>>> GetPasoTarea()
        {
            return await _context.PasoTarea.ToListAsync();
        }

        // GET: api/PasoTareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PasoTarea>> GetPasoTarea(Guid id)
        {
            var pasoTarea = await _context.PasoTarea.FindAsync(id);

            if (pasoTarea == null)
            {
                return NotFound();
            }

            return pasoTarea;
        }

        // PUT: api/PasoTareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasoTarea(Guid id, PasoTarea pasoTarea)
        {
            if (id != pasoTarea.Id)
            {
                return BadRequest();
            }

            _context.Entry(pasoTarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoTareaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PasoTareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PasoTarea>> PostPasoTarea(PasoTarea pasoTarea)
        {
            _context.PasoTarea.Add(pasoTarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasoTarea", new { id = pasoTarea.Id }, pasoTarea);
        }

        // DELETE: api/PasoTareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasoTarea(Guid id)
        {
            var pasoTarea = await _context.PasoTarea.FindAsync(id);
            if (pasoTarea == null)
            {
                return NotFound();
            }

            _context.PasoTarea.Remove(pasoTarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasoTareaExists(Guid id)
        {
            return _context.PasoTarea.Any(e => e.Id == id);
        }
    }
}
