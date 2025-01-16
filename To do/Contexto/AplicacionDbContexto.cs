using Microsoft.EntityFrameworkCore;
using To_do.Modelos;

namespace To_do.Contexto
{
    public class AplicacionDbContexto : DbContext 
    {
        public AplicacionDbContexto(DbContextOptions<AplicacionDbContexto> options) : base(options)
        {
            
        }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
