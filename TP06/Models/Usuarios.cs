using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace TP06.Models
{
    public class Usuarios
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string nombre { get; set; }

        [JsonProperty]
        public string apellido { get; set; }

        [JsonProperty]
        public string foto { get; set; }

        [JsonProperty]
        public string username { get; set; }

        [JsonProperty]
        public DateTime ultLogin { get; set; }

        [JsonProperty]
        public string password { get; set; }

        [JsonProperty]
        public List<Tareas> Tareas { get; set; }

        public Usuarios()
        {
            Tareas = new List<Tareas>();
        }

        public Usuarios(string nombre, string apellido, string foto, string username, DateTime ultLogin, string password)
        {
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
