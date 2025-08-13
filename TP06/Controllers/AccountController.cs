using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginGuardar(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Complete todos los campos";
                return View("Login");
            }

            Usuarios usuario = BD.login(username, password);

            if (usuario != null)
            {
                  BD.actLogin(usuario.Id); // Actualiza fecha de último login
                return RedirectToAction("VerTareas", "Home");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View("Login");
            }
        }

        public IActionResult CerrarSesion()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistroGuardar(string nombre, string apellido, string username, string contraseña, string confirmarContraseña)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(contraseña) || 
                string.IsNullOrEmpty(confirmarContraseña))
            {
                ViewBag.Error = "Complete todos los campos";
                return View("Registro");
            }

            if (contraseña != confirmarContraseña)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View("Registro");
            }

            // Evitar null en 'foto' y en la lista de tareas
            Usuarios nuevoUsuario = new Usuarios(
                0, nombre, apellido, null, username, DateTime.Now, contraseña
            );
            nuevoUsuario.Tareas = new List<Tareas>();

            BD.registrarse(nuevoUsuario);

            return RedirectToAction("Login");
        }
    }
}
