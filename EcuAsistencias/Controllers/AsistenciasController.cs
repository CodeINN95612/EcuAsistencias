using EcuAsistencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcuAsistencias.Controllers
{
    public class AsistenciasController : Controller
    {
		EcuDB db = new EcuDB();

		[HttpGet]
		public ActionResult RegistrarEntrada()
		{
			return View();
		}
		[HttpPost]
		public ActionResult RegistrarEntrada(string action)
		{
			Usuario Logeado = db.Usuarios.Find(Session["UsuarioID"].ToString());
			if (action.Equals("Ingresar"))
			{

				Asistencia Actual = db.Asistencias.FirstOrDefault(a => a.Fecha == DateTime.Today && a.IdentificacionUsuario.Equals(Logeado.Identificacion));
				if (Actual is null)
				{

					Asistencia nueva = new Asistencia
					{
						Fecha = DateTime.Now.Date,
						Ingreso = DateTime.Now,
						Salida = null,
						IdentificacionUsuario = Logeado.Identificacion,
						Usuario = Logeado
					};
					db.Asistencias.Add(nueva);

				}
			}
			else
			{
				Asistencia Actual = db.Asistencias.FirstOrDefault(a => a.Fecha == DateTime.Today && a.IdentificacionUsuario.Equals(Logeado.Identificacion));
				if (Actual != null)
				{
					Actual.Salida = DateTime.Now;

				}

			}
			db.SaveChanges();
			return View();
		}
	}

}