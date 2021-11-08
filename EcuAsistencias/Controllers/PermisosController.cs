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
        EcuDBEntities db = new EcuDBEntities();
        // GET: Permisos
        [HttpGet]
        public ActionResult PermisoSalida   ()
        {
            ViewBag.IdMotivo = new SelectList(db.Motivo, "ID", "Detalle");
            return View();

        }
        [HttpPost]
        public ActionResult PermisoSalida(PermisosSalida modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            Usuario Logeado = db.Usuario.Find(Session["UsuarioID"].ToString());
            Asistencia Actual = db.Asistencia.FirstOrDefault(a => a.Fecha == DateTime.Today && a.IdentificacionUsuario.Equals(Logeado.Identificacion));
            Motivo m = db.Motivo.Find(modelo.IdMotivo);
            if(Actual != null)
            {
                PermisosSalida nuevo = new PermisosSalida
                {
                    Asistencia = Actual,
                    HoraPermiso = modelo.HoraPermiso,
                    IDAsistencia = Actual.ID,
                    IdMotivo = m.ID,
                    Motivo = m,
                    MotivoOtros = m.ID == 4 ? modelo.MotivoOtros : null,
                    TiempoPermisoHoras = modelo.TiempoPermisoHoras,

                };
                db.PermisosSalida.Add(nuevo);
                db.SaveChanges();
            }
            ViewBag.IdMotivo = new SelectList(db.Motivo, "ID", "Detalle");
            return View();

        }
    }
}