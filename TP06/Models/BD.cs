using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;
namespace TP06.Models
{
    public class BD
    {
        private static string _connectionString = @"Server=localhost;
        DataBase=Integrantes;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuarios login(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Integrante WHERE username = @pUsuario AND password = @pPassword";
                return connection.QueryFirstOrDefault<Usuarios>(
                    query,
                    new { pUsuario = username, pPassword = password }
                );
            }
        }
        public void registrarse(Usuarios usuario)
        {
            string query = @"INSERT INTO Usuarios (Id, nombre, apellido, foto, username, ultLogin, password) 
                            VALUES (@Id, @nombre, @apellido, @foto, @username, @ultLogin, @password)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new
                {
                    Id = usuario.Id,
                    nombre = usuario.nombre,
                    apellido = usuario.apellido,
                    foto = usuario.foto,
                    username = usuario.username,
                    ultLogin = usuario.ultLogin,
                    password = usuario.password
                });
            }
        }
                
        public List<Tareas> devolverTareas(int IdU)
        {
        
        }

        public Tareas devolverTarea(int idtarea)
        {
           on ese Id
        }

        public void modificarTarea(Tareas tarea)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string update = @"UPDATE Tareas SET 
                                  titulo = @titulo, 
                                  descripcion = @descripcion, 
                                  fecha = @fecha, 
                                  finalizado = @finalizado 
                                  WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(update, con))
                {
                    cmd.Parameters.AddWithValue("@titulo", tarea.titulo);
                    cmd.Parameters.AddWithValue("@descripcion", tarea.descripcion);
                    cmd.Parameters.AddWithValue("@fecha", tarea.fecha);
                    cmd.Parameters.AddWithValue("@finalizado", tarea.finalizado);
                    cmd.Parameters.AddWithValue("@Id", tarea.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void eliminarTarea(int idTarea)
        {
         
           
        }

        public void crearTarea(Tareas tarea)
        {
            
        }

        public void finalizarTarea(int idtarea)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string update = @"UPDATE Tareas SET finalizado = 1 WHERE Id = @idtarea";

                using (SqlCommand cmd = new SqlCommand(update, con))
                {
                    cmd.Parameters.AddWithValue("@idtarea", idtarea);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void actLogin(int IdU)
        {
           
        }
    }
}
