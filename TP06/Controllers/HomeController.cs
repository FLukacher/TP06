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
        else
        {
            ViewBag.Error = "Complete Todos los campos";
        }

    return RedirectToAction("VerTareas"); 
    }
    public IActionResult editarTarea(int id)
    {
        Tareas tarea = BD.obtenerTareaPorId(id);

        if (tarea == null)
        {
            return RedirectToAction("VerTareas");
        }

        HttpContext.Session.SetString("nuevaTarea", Objeto.ObjectToString(tarea));

        return View();
    }
    [HttpPost]
    public IActionResult editarTareaGuardar(string titulo, string descripcion, DateTime fecha)
    {
        string tareaJson = HttpContext.Session.GetString("nuevaTarea");
<<<<<<< HEAD
        if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(fecha.ToString()))
        {
            ViewBag.Error = "Complete todos los campos";
            return View("editarTarea");
        }

        else if (string.IsNullOrEmpty(tareaJson))
=======

        if (string.IsNullOrEmpty(tareaJson))
>>>>>>> fc0932cf73f9a4560033fd48b9847ec4e06560d8
        {
            return RedirectToAction("VerTareas");
        }

        Tareas tareaEditar = Objeto.StringToObject<Tareas>(tareaJson);

        if (tareaEditar == null)
        {
            return RedirectToAction("VerTareas");
        }

        tareaEditar.titulo = titulo;
        tareaEditar.descripcion = descripcion;
        tareaEditar.fecha = fecha;


        BD.modificarTarea(tareaEditar);

    
        return RedirectToAction("VerTareas");
    }

    public IActionResult finalizarTarea(int id)
    {
        BD.finalizarTarea(id);
        return RedirectToAction("VerTareas");
    }
    public IActionResult eliminarTarea(int id)
    {
        BD.eliminarTarea(id);
        return RedirectToAction("VerTareas");
    }


}
