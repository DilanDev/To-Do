using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace To_do.Modelos
{
    public class DetalleTarea
    {
        [Key]
        public Guid TareaId { get; set; }
        public string? Base64 { get; set; }
        public DateTime Fecha { get; set; }
        public required Tarea Tarea { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

        public async Task<string> ConvertFileToBase64Async()
        {
            string base64 = string.Empty;

            if (File != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await File.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                   base64 = Convert.ToBase64String(fileBytes);
                }
            }
            return base64;
        }
    }
}
