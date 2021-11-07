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
        EcuDBEntities db = new EcuDBEntities();

       [HttpGet]
        public ActionResult RegistrarEntrada()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistrarEntrada(string action)
        {
            Usuario Logeado = db.Usuario.Find(Session["UsuarioID"].ToString());
            if (action.Equals("Ingresar"))
            {
                
                Asistencia Actual = db.Asistencia.FirstOrDefault(a => a.Fecha == DateTime.Today && a.IdentificacionUsuario.Equals(Logeado.Identificacion));
                if(Actual is null)
                {
                    
                    Asistencia nueva = new Asistencia
                    {
                        Fecha = DateTime.Now.Date,
                        HoraIngreso = DateTime.Now,
                        HoraSalida = null,
                        IdentificacionUsuario = Logeado.Identificacion,
                        Usuario = Logeado
                    };
                    db.Asistencia.Add(nueva);
                    
                }
            }
            else
            {
                Asistencia Actual = db.Asistencia.FirstOrDefault(a => a.Fecha == DateTime.Today && a.IdentificacionUsuario.Equals(Logeado.Identificacion));
                if (Actual != null)
                {
                    Actual.HoraSalida = DateTime.Now;

                }

            }
            db.SaveChanges();
            return View();
        }
    }

}