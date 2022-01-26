using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Security.Cryptography;
using System.Net;
using EcuAsistencias.Core.Dtos;
using EcuAsistencias.Models;

namespace EcuAsistencias.Controllers
{
    public class UsuarioController : Controller
    {
		EcuDB db = new EcuDB();

		[HttpGet]
		public ActionResult Login()
		{
			Usuario usuarioLogeado = db.Usuarios.Find((string)Session["UsuarioID"]);
			if (usuarioLogeado != null)
			{
				Session.Clear();
				Session["UsuarioID"] = usuarioLogeado.Identificacion;
				Session["Usuario"] = usuarioLogeado.Nombre;
				Session["EsSupervisor"] = usuarioLogeado.Rol.EsSupervisor;
				return RedirectToAction("RegistrarEntrada", "Asistencias");
			}

			Session.Clear();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(UsuarioLoginDto loginUser)
		{

			//Validar Modelo
			if (!ModelState.IsValid)
				return View(loginUser);

			//Validar existencia de usuario por identificacion
			Usuario usuarioExistente = db.Usuarios.Find(loginUser.Identificacion);
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
				usuarios = db.Usuarios.ToList();
				page = 1;
			}
			else
				usuarios = db.Usuarios.Where(u => u.Identificacion.Contains(cedula)).ToList();

			return View(usuarios.ToPagedList(pageNumber, pageSize));

		}
		public ActionResult newUser()
		{
			ViewBag.IDRol = new SelectList(db.Roles, "ID", "Detalle");
			return View();
		}

		[HttpPost]
		public ActionResult newUser(UsuarioDto nuevo)
		{
			ViewBag.IdRol = new SelectList(db.Roles, "Id", "Detalle");
			if (!ModelState.IsValid)
				return View(nuevo);

			Usuario existente = db.Usuarios.Find(nuevo.Identificacion);
			if (existente is null)
			{
				Rol rolExistente = db.Roles.Find(nuevo.IdRol);
				if (rolExistente is null)
					return View(nuevo);

				existente = new Usuario
				{
					Activo = true,
					Apellido = nuevo.Apellido,
					CambioContrasenia = true,
					Contrasenia = Encriptar(nuevo.Identificacion),
					FechaNacimiento = nuevo.FechaNacimiento,
					HorarioFin = nuevo.HorarioFin,
					HorarioInicio = nuevo.HorarioInicio,
					Identificacion = nuevo.Identificacion,
					IdRol = rolExistente.Id,
					Nombre = nuevo.Nombre,
					Rol = rolExistente
				};
				db.Usuarios.Add(existente);
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
			Usuario usuario = db.Usuarios.Find(id);
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
			Usuario us = db.Usuarios.Find(id);
			db.Usuarios.Remove(us);
			db.SaveChanges();
			return RedirectToAction("viewLista");
		}

		public ActionResult Editar(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Usuario us = db.Usuarios.Find(id);
			if (us == null)
			{
				return HttpNotFound();
			}
			ViewBag.IDRol = new SelectList(db.Roles, "ID", "Detalle", us.Rol);
			return View(us);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Editar(Usuario us)
		{
			if (ModelState.IsValid)
			{
				Usuario existente = db.Usuarios.Find(us.Identificacion);
				existente.Nombre = us.Nombre;
				existente.Apellido = us.Apellido;
				existente.FechaNacimiento = us.FechaNacimiento;
				existente.CambioContrasenia = us.CambioContrasenia;
				existente.Activo = us.Activo;

				Rol rol = db.Roles.Find(us.IdRol);
				existente.Rol = rol;
				existente.IdRol = rol.Id;

				db.SaveChanges();
				return RedirectToAction("viewLista");
			}
			ViewBag.IDRol = new SelectList(db.Roles, "ID", "Detalle", us.Rol);
			return View(us);
		}

		public ActionResult Detalles(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Usuario us = db.Usuarios.Find(id);
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

		//Privadas
		[NonAction]
		public string Encriptar(string pass)
		{
			var encriptador = SHA256.Create();
			return Convert.ToBase64String(encriptador.ComputeHash(System.Text.Encoding.Unicode.GetBytes(pass)));
		}

	}
}