using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_do.Modelos
{
    //[Table("TAREA", Schema ="")]
    public class Tarea
    {
        [Key] 
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Completado { get; set; } = false;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
