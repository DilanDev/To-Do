using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_do.Contexto;
using To_do.Modelos;

namespace To_do.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DetalleTareaController : ControllerBase
    {
        private readonly AplicacionDbContexto _context;

        public DetalleTareaController(AplicacionDbContexto context)
        {
            _context = context;
        }

        // GET: [controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleTarea>>> Get()
        {
            return await _context.DetalleTareas.ToListAsync();
        }

        // GET [controller]/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleTarea>> Get(Guid id)
        {
            var detalleTarea = await _context.DetalleTareas
                                             .FirstOrDefaultAsync(dt => dt.TareaId == id);

            if (detalleTarea == null)
            {
                return NotFound();
            }

            return detalleTarea;
        }

        // POST [controller]
        [HttpPost]
        public async Task<ActionResult<DetalleTarea>> Post([FromForm] DetalleTarea detalleTarea)
        {
            detalleTarea.Base64 = await detalleTarea.ConvertFileToBase64Async();
            _context.DetalleTareas.Add(detalleTarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = detalleTarea.TareaId }, detalleTarea);
        }

        // PUT [controller]/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] DetalleTarea detalleTarea)
        {
            if (id != detalleTarea.TareaId)
            {
                return BadRequest();
            }

            var detalletarea = await _context.DetalleTareas
                                              .FirstOrDefaultAsync(dt => dt.TareaId == id);

            if (detalletarea == null)
            {
                return NotFound();
            }

            try
            {
                detalletarea.Base64 = await detalleTarea.ConvertFileToBase64Async();
                _context.DetalleTareas.Update(detalletarea);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleTareaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE [controller]/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var detalleTarea = await _context.DetalleTareas
                                              .FirstOrDefaultAsync(dt => dt.TareaId == id);
            if (detalleTarea == null)
            {
                return NotFound();
            }

            _context.DetalleTareas.Remove(detalleTarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleTareaExists(Guid id)
        {
            return _context.DetalleTareas.Any(e => e.TareaId == id);
        }
    }
}
