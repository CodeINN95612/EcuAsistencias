using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcuAsistencias.Models.ViewModels;
using EcuAsistencias.Models;
using PagedList;

using System.Security.Cryptography;
using System.Net;
using System.Data.Entity;

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
            return RedirectToAction("RegistrarEntrada", "Asistencias");
        }
        public ActionResult viewLista(string cedula, int? page)
		{
            const int pageSize = 5;
            int pageNumber = page ?? 1;

            List<Usuario> usuarios;
            if (cedula is null)
            {
                usuarios = db.Usuario.ToList();
                page = 1;
            }
            else
                usuarios = db.Usuario.Where(u => u.Identificacion.Contains(cedula)).ToList();

            return View(usuarios.ToPagedList(pageNumber, pageSize));

		}
        public ActionResult newUser()
		{
            ViewBag.IDRol = new SelectList(db.Rol, "ID", "Detalle");
            return View();
		}

        [HttpPost]
        public ActionResult newUser(Usuario nuevo)
        {
            ViewBag.IDRol = new SelectList(db.Rol, "ID", "Detalle");
            if (!ModelState.IsValid)
                return View(nuevo);

            Usuario existente = db.Usuario.Find(nuevo.Identificacion);
            if(existente is null)
			{
                nuevo.Contrasenia = Encriptar(nuevo.Identificacion);
                nuevo.CambioContrasenia = true;
                nuevo.Activo = true;
                db.Usuario.Add(nuevo);
                db.SaveChanges();
                return RedirectToAction("viewLista");
			}

            return View(nuevo);
        }

        public ActionResult Borrar(string id)
		{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarConfirmado(string id)
        {
            Usuario us = db.Usuario.Find(id);
            db.Usuario.Remove(us);
            db.SaveChanges();
            return RedirectToAction("viewLista");
        }

        public ActionResult Editar(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario us = db.Usuario.Find(id);
            if (us == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDRol = new SelectList(db.Rol, "ID", "Detalle", us.Rol);
            return View(us);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Usuario us)
        {
            if (ModelState.IsValid)
            {
                Usuario existente = db.Usuario.Find(us.Identificacion);
                existente.Nombre = us.Nombre;
                existente.Apellido = us.Apellido;
                existente.FechaNacimiento = us.FechaNacimiento;
                existente.CambioContrasenia = us.CambioContrasenia;
                existente.Activo = us.Activo;

                Rol rol = db.Rol.Find(us.IDRol);
                existente.Rol = rol;
                existente.IDRol = rol.ID;

                db.SaveChanges();
                return RedirectToAction("viewLista");
            }
            ViewBag.IDRol = new SelectList(db.Rol, "ID", "Detalle", us.Rol);
            return View(us);
        }

        public ActionResult Detalles(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario us = db.Usuario.Find(id);
            if (us == null)
            {
                return HttpNotFound();
            }
            return View(us);
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