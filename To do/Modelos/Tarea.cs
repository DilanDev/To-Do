namespace To_do.Modelos
{
    public class Tarea
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Completado { get; set; }
    }
}
