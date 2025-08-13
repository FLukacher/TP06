namespace TP06.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public byte[] foto { get; set; }  // Cambiado a byte[]
        public string username { get; set; }
        public DateTime ultLogin { get; set; }
        public string password { get; set; }
        public List<Tareas> Tareas { get; set; }

        public Usuarios() 
        {
            // Constructor vacío necesario para Dapper
            Tareas = new List<Tareas>();
        }

        public Usuarios(int Id, string nombre, string apellido, byte[] foto, string username, DateTime ultLogin, string password)
        {
            this.Id = Id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.foto = foto;
            this.username = username;
            this.ultLogin = ultLogin;
            this.password = password;
            this.Tareas = new List<Tareas>();
        }
    }
}
