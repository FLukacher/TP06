namespace TP06.Models
{
    public class Tareas
    {
        public int Id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public bool finalizado { get; set; }
        public int IdU { get; set; }

        public Usuarios Usuario { get; set; }

        public Tareas() { }

        public Tareas(int Id, string titulo, string descripcion, DateTime fecha, bool finalizado, int IdU)
        {
            this.Id = Id;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha = fecha;
            this.finalizado = finalizado;
            this.IdU = IdU;
        }
    }
}