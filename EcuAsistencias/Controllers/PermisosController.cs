using EcuAsistencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcuAsistencias.Controllers
{
    public class PermisosController : Controller

    {
		EcuDB db = new EcuDB();
		// GET: Permisos
		[HttpGet]
		public ActionResult PermisoSalida()
		{
			ViewBag.IdMotivo = new SelectList(db.Motivos, "ID", "Detalle");
			return View();

		}
		[HttpPost]
		public ActionResult PermisoSalida(PermisoSalida modelo)
		{
			if (!ModelState.IsValid)
			{
				return View(modelo);
			}

			Usuario Logeado = db.Usuarios.Find(Session["UsuarioID"].ToString());
			Asistencia Actual = db.Asistencias.FirstOrDefault(a => a.Fecha == DateTime.Today && a.IdentificacionUsuario.Equals(Logeado.Identificacion));
			Motivo m = db.Motivos.Find(modelo.IdMotivo);
			if (Actual != null)
			{
				PermisoSalida nuevo = new PermisoSalida
				{
					Asistencia = Actual,
					HoraPermiso = modelo.HoraPermiso,
					IdAsistencia = Actual.Id,
					IdMotivo = m.Id,
					Motivo = m,
					MotivoOtros = m.Id == 4 ? modelo.MotivoOtros : null,
					TiempoPermisoHoras = modelo.TiempoPermisoHoras,

				};
				db.PermisosSalida.Add(nuevo);
				db.SaveChanges();
			}
			ViewBag.IdMotivo = new SelectList(db.Motivos, "ID", "Detalle");
			return View();

		}
	}
}