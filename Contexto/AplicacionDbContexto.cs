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
        public DbSet<DetalleTarea> DetalleTareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.DetalleTarea)
                .WithOne(d => d.Tarea)
                .HasForeignKey<DetalleTarea>(d => d.TareaId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<PasoTarea>()
                .HasOne(p => p.Tarea)
                .WithMany(t => t.Pasos)
                .HasForeignKey(p => p.TareaId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
        public DbSet<To_do.Modelos.PasoTarea> PasoTarea { get; set; } = default!;
    }
}
