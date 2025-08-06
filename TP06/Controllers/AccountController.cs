using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult LoginGuardar()
    {
        return View();
    }
    public IActionResult CerrarSesion()
    {
        return View();
    }
    public IActionResult Registro()
    {
        return View();
    }
    public IActionResult RegistroGuardar()
    {
        return View();
    }
}