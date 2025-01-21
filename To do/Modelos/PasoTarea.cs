
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace To_do.Modelos
{
    public class PasoTarea 
    {
        [Key]
        public Guid Id { get; set; }
        public required string Descricion { get; set; }
        public bool Completado { get; set; }
        public Guid TareaId { get; set; }
        public Tarea Tarea { get; set; }
    }
}
