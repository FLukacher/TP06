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
        public Usuarios usuario { get; set; }

        public Tareas() { }
        public Tareas(string titulo, string descripcion, DateTime fecha, int IdU, Usuarios usuario)
        {
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha = fecha;
            this.finalizado = false;
            this.IdU = IdU;
            this.usuario = usuario;
        }

        
    }
}