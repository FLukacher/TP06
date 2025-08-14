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
    public IActionResult crearTareaGuardar()
    {
        return View();
    }
    public IActionResult VerTareas()
    {
        var usuarioJson = HttpContext.Session.GetString("usuarioLogueado");
        if (usuarioJson == null)
        {
            return RedirectToAction("Login", "Account"); 
        }
        
        ViewBag.Usuario = Objeto.StringToObject<Usuarios>(usuarioJson);
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
