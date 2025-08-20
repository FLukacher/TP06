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
    public IActionResult EditarFoto()
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

        string usuarioLogueadoJson = HttpContext.Session.GetString("usuarioLogueado");
        Usuarios usuario = Objeto.StringToObject<Usuarios>(usuarioLogueadoJson);

        List<Tareas> tareas = BD.obtenerTareasPorUsuario(usuario.Id);

        List<Tareas> tareasPendientes = new List<Tareas>();

        foreach (Tareas tarea in tareas)
        {
            if (!tarea.finalizado)
            {
                tareasPendientes.Add(tarea);
            }
        }

        ViewBag.Usuario = usuario;
        ViewBag.Tareas = tareasPendientes;
        return View();
    }
    public IActionResult GuardarFotoUrl(string fotoUrl)
    {
    string usuarioLogueadoJson = HttpContext.Session.GetString("usuarioLogueado");
    Usuarios usuario = Objeto.StringToObject<Usuarios>(usuarioLogueadoJson);

    if (!string.IsNullOrEmpty(fotoUrl))
    {
        usuario.foto = fotoUrl;
        BD.ActualizarFoto(usuario.Id, fotoUrl); 
    }

    return RedirectToAction("VerTareas"); 
    }
    public IActionResult editarTarea()
    {
        return View();
    }
    public IActionResult editarTareaGuardar(string titulo, string descripcion, DateTime fecha)
    {
        string tareaJson = HttpContext.Session.GetString("nuevaTarea");

        if (string.IsNullOrEmpty(tareaJson))
        {
            ViewBag.Error = "No se encontró la tarea para editar.";
            return RedirectToAction("VerTareas");
        }

        Tareas tareaEditar = Objeto.StringToObject<Tareas>(tareaJson);

        if (tareaEditar == null)
        {
            ViewBag.Error = "Error al cargar la tarea para editar.";
            return RedirectToAction("VerTareas");
        }


        BD.modificarTarea(tareaEditar);
        return RedirectToAction("VerTareas");
    }

    public IActionResult finalizarTarea(int id)
    {
        BD.eliminarTarea(id);
        return RedirectToAction("VerTareas");
    }


}
