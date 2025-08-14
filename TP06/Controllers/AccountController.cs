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
                BD.actLogin(usuario.Id);
                HttpContext.Session.SetString("usuarioLogueado", Objeto.ObjectToString(usuario));
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
        public IActionResult RegistroGuardar(string nombre, string apellido, string username, string password, string confirmarPassword)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmarPassword))
            {
                ViewBag.Error = "Complete todos los campos";
                return View("Registro");
            }

            if (password != confirmarPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View("Registro");
            }
            if (BD.UsernameExiste(username))
            {
                ViewBag.Error = "El nombre de usuario ya está en uso";
                return View("Registro");
            }

            string fotoDefault = "/img/default.jpg";
            Usuarios nuevoUsuario = new Usuarios(nombre, apellido, fotoDefault, username, DateTime.Now, password);
            nuevoUsuario.Tareas = new List<Tareas>();

            BD.registrarse(nuevoUsuario);

            return RedirectToAction("Login");
        }
    }
}
