using Microsoft.EntityFrameworkCore;
using To_do.Contexto;
using To_do.Modelos;

namespace To_do.Services
{
    public class TareasService
    {
        private readonly AplicacionDbContexto _context;

        public TareasService(AplicacionDbContexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> ObtenerTareasAsync()
        {
            return await _context.Tareas.ToListAsync();
        }

        public async Task<Tarea?> ObtenerTareaPorIdAsync(Guid id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task<bool> CrearTareaAsync(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditarTareaAsync(Guid id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return false;
            }

            var tareaExistente = await _context.Tareas
                .AsTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tareaExistente == null)
            {
                return false;
            }

            tareaExistente.Nombre = tarea.Nombre;
            tareaExistente.Descripcion = tarea.Descripcion;
            tareaExistente.Completado = tarea.Completado;
            tareaExistente.FechaInicio = tarea.FechaInicio;
            tareaExistente.FechaFinal = tarea.FechaFinal;

            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TareaExistsAsync(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> EliminarTareaAsync(Guid id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return false;
            }

            _context.Tareas.Remove(tarea);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> TareaExistsAsync(Guid id)
        {
            return await _context.Tareas.AnyAsync(e => e.Id == id);
        }
    }
}
