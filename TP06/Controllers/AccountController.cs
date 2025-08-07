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
    public IActionResult LoginGuardar(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Complete todos los campos";
            return View("Login");
        }


        if (BD.login(username, password) != null)
        {
            return RedirectToAction("Index", "Home");
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
        int i = 0;
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(confirmarContraseña))
        {
            ViewBag.Error = "Complete todos los campos";
            return View("Registro");
        }

        if (contraseña != confirmarContraseña)
        {
            ViewBag.Error = "Las contraseñas no coinciden";
            return View("Registro");
        }

        Usuarios nuevoUsuario = new Usuarios(i++, nombre, apellido, null, username, DateTime.Now.Date, contraseña);
        BD.registrarse(nuevoUsuario);

        return View("Login");

        }
    }