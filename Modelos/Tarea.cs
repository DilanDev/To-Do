using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_do.Modelos
{
    public class Tarea
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }
        public bool Completado { get; set; }
        public ICollection<PasoTarea> Pasos { get; set; } = new List<PasoTarea>();
        public DetalleTarea? DetalleTarea { get; set; }
    }
}
