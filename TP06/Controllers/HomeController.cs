using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult crearTarea()
    {
        return View();
    }
    public IActionResult crearTareaGuardar(string titulo, string descripcion, DateTime fecha)
    {
        string usuarioLogueadoJson = HttpContext.Session.GetString("usuarioLogueado");

        if (string.IsNullOrEmpty(usuarioLogueadoJson))
        {
            ViewBag.Error = "Debes iniciar sesión para crear una tarea";
            return RedirectToAction("Login");
        }

        if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(descripcion) || fecha == null)
        {
            ViewBag.Error = "Complete todos los campos";
            return View("CrearTarea"); 
        }


        Usuarios usuario = Objeto.StringToObject<Usuarios>(usuarioLogueadoJson);

        if (usuario == null)
        {
            ViewBag.Error = "No se pudo obtener la información del usuario. Por favor, vuelve a iniciar sesión.";
            return RedirectToAction("Login");
        }

        Tareas nuevaTarea = new Tareas(titulo, descripcion, fecha, usuario.Id, usuario);

        BD.crearTarea(nuevaTarea);

        return RedirectToAction("VerTareas");
    }


    public IActionResult VerTareas()
    {
  
        if (HttpContext.Session.GetString("usuarioLogueado") == null)
        {
            return RedirectToAction("Login", "Account"); 
        }
        
        ViewBag.Usuario = Objeto.StringToObject<Usuarios>(HttpContext.Session.GetString("usuarioLogueado"));
        return View();
    }
    public IActionResult editarTarea()
    {
        return View();
    }
    public IActionResult eliminarTarea()
    {
        return View();
    }
    public IActionResult eliminarTareaGuardar()
    {
        return View();
    }
    public IActionResult finalizarTarea()
    {
        return View();
    }
}
