using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcuAsistencias.Models.ViewModels;
using EcuAsistencias.Models;

using System.Security.Cryptography;

namespace EcuAsistencias.Controllers
{
    public class UsuarioController : Controller
    {
        EcuDBEntities db = new EcuDBEntities();

        [HttpGet]
        public ActionResult Login()
		{
            Usuario usuarioLogeado = db.Usuario.Find((string)Session["UsuarioID"]);
            if (usuarioLogeado != null)
            {
                Session.Clear();
                Session["UsuarioID"] = usuarioLogeado.Identificacion;
                Session["Usuario"] = usuarioLogeado.Nombre;
                Session["EsSupervisor"] = usuarioLogeado.Rol.EsSupervisor;
                return RedirectToAction("Test");
            }

            Session.Clear();
            return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioLoginViewModel loginUser)
        {

            //Validar Modelo
            if (!ModelState.IsValid)
                return View(loginUser);

            //Validar existencia de usuario por identificacion
            Usuario usuarioExistente = db.Usuario.Find(loginUser.Identificacion);
            if (usuarioExistente is null)
            {
                ViewBag.Mensaje = "Usuario incorrecto.";
                return View(loginUser);
            }

            //Validar Contraseña
            string encriptada = Encriptar(loginUser.Contrasenia);
            if (!encriptada.Equals(usuarioExistente.Contrasenia))
            {
                ViewBag.Mensaje = "Contraseña incorrecta.";
                return View(loginUser);
            }

            //Sesion
            Session["UsuarioID"] = usuarioExistente.Identificacion;
            Session["Usuario"] = usuarioExistente.Nombre;
            Session["EsSupervisor"] = usuarioExistente.Rol.EsSupervisor;

            //Ir a pagina principal
            return RedirectToAction("Test");
        }
        public ActionResult viewLista(string cedula)
		{
            if(cedula is null)
			{
                return View(db.Usuario.ToList());
			}else {
                return View(db.Usuario.Where(u => u.Identificacion.Contains(cedula)).ToList());
            }

		}

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Test()
        {
            if (Session["UsuarioID"] is null)
                return RedirectToAction("Login");

            return View();
        }

        //Privadas
        [NonAction]
        public string Encriptar(string pass)
		{
            var encriptador = SHA256.Create();
            return Convert.ToBase64String(encriptador.ComputeHash(System.Text.Encoding.Unicode.GetBytes(pass)));
		}

    }
}