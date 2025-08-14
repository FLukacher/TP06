using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;

namespace TP06.Models
{
    public static class BD
    {
        private static string _connectionString = @"Server=localhost;DataBase=TP06;Integrated Security=True;TrustServerCertificate=True;";

        public static Usuarios login(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Usuarios WHERE username = @pUsuario AND password = @pPassword";
                return connection.QueryFirstOrDefault<Usuarios>(
                    query,
                    new { pUsuario = username, pPassword = password }
                );
            }
        }

        public static void registrarse(Usuarios usuario)
        {
            string query = @"INSERT INTO Usuarios (nombre, apellido, foto, username, ultLogin, password) 
                            VALUES (@nombre, @apellido, @foto, @username, @ultLogin, @password)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, 
                new{
                    nombre = usuario.nombre,
                    apellido = usuario.apellido,
                    foto = usuario.foto,
                    username = usuario.username,
                    ultLogin = usuario.ultLogin,
                    password = usuario.password
                });
            }
        }
                
        public static List<Tareas> devolverTareas(int IdU)
        {
            string query = @"SELECT * FROM Tareas WHERE IdU = @pIdU";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Tareas>(query, new { pIdU = IdU }).AsList();
            }
        }

        public static Tareas devolverTarea(int idtarea)
        {
            string query = @"SELECT * FROM Tareas WHERE Id = @pId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Tareas>(query, new { pId = idtarea });
            }
        }

        public static void modificarTarea(Tareas tarea)
        {
            string query = @"UPDATE Tareas SET titulo = @titulo, descripcion = @descripcion, fecha = @fecha, finalizado = @finalizado, IdU = @IdU WHERE Id = @Id"; 
                                                        
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new
                {
                    Id = tarea.Id,
                    titulo = tarea.titulo,
                    descripcion = tarea.descripcion,
                    fecha = tarea.fecha,
                    finalizado = tarea.finalizado,
                    IdU = tarea.IdU
                });
            }
        }

        public static void eliminarTarea(int idTarea)
        {
            string query = @"DELETE FROM Tareas WHERE Id = @pId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { pId = idTarea });
            }
        }

        public static void crearTarea(Tareas tarea)
        {
            string query = @"INSERT INTO Tareas (Id, titulo, descripcion, fecha, finalizado, IdU)
                            VALUES (@Id, @titulo, @descripcion, @fecha, @finalizado, @IdU)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new
                {
                    Id = tarea.Id,
                    titulo = tarea.titulo,
                    descripcion = tarea.descripcion,
                    fecha = tarea.fecha,
                    finalizado = tarea.finalizado,
                    IdU = tarea.IdU
                });
            }
        }

        public static void finalizarTarea(int idtarea)
        {
            string query = @"UPDATE Tareas SET finalizado = 1 WHERE Id = @pId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { pId = idtarea });
            }
        }

        public static void actLogin(int IdU)
        {
            string query = @"UPDATE Usuarios SET ultLogin = GETDATE() WHERE Id = @pId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { pId = IdU });
            }
        }
    }
}
