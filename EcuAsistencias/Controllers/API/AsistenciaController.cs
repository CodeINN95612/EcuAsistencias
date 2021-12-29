using EcuAsistencias.Core.ViewModels;
using EcuAsistencias.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EcuAsistencias.Controllers.API
{
    public class AsistenciaController : ApiController
    {
        private EcuDB db = new EcuDB();

        public IQueryable<AsistenciaViewModel> GetAsistencia()
        {

            return db.Asistencias.Select(p => new AsistenciaViewModel
            {
                Id = p.Id,
                Fecha = p.Fecha,
                IdentificacionUsuario = p.IdentificacionUsuario,
                Ingreso = p.Ingreso,
                Salida = p.Salida
            });
        }
        // GET: api/Asistencia/5
        [ResponseType(typeof(AsistenciaViewModel))]
        public IHttpActionResult GetAsistencia(int id)
        {
            Asistencia asistencia = db.Asistencias.Find(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return Ok(new AsistenciaViewModel
            {
                Id = asistencia.Id,
                Fecha = asistencia.Fecha,
                IdentificacionUsuario = asistencia.IdentificacionUsuario,
                Ingreso = asistencia.Ingreso,
                Salida = asistencia.Salida
            });
        }

        // PUT: api/Asistencias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsistencia(int id, AsistenciaViewModel asistenciaView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asistenciaView.Id)
            {
                return BadRequest();
            }

            db.Entry(new Asistencia
            {
                Id = asistenciaView.Id,
                Fecha = asistenciaView.Fecha,
                IdentificacionUsuario = asistenciaView.IdentificacionUsuario,
                Ingreso = asistenciaView.Ingreso,
                Salida = asistenciaView.Salida
            }).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Asistencias
        [ResponseType(typeof(AsistenciaViewModel))]
        public IHttpActionResult PostAsistencia(AsistenciaViewModel asistenciaView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asistencias.Add(new Asistencia
            {
                Id = asistenciaView.Id,
                Fecha = asistenciaView.Fecha,
                IdentificacionUsuario = asistenciaView.IdentificacionUsuario,
                Ingreso = asistenciaView.Ingreso,
                Salida = asistenciaView.Salida
            });
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asistenciaView.Id }, asistenciaView);
        }

        // DELETE: api/Asistenciaes/5
        [ResponseType(typeof(AsistenciaViewModel))]
        public IHttpActionResult DeleteAsistencia(int id)
        {
            Asistencia asistencia = db.Asistencias.Find(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            db.Asistencias.Remove(asistencia);
            db.SaveChanges();

            return Ok(new AsistenciaViewModel
            {
                Id = asistencia.Id,
                Fecha = asistencia.Fecha,
                IdentificacionUsuario = asistencia.IdentificacionUsuario,
                Ingreso = asistencia.Ingreso,
                Salida = asistencia.Salida
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsistenciaExists(int id)
        {
            return db.Asistencias.Count(e => e.Id == id) > 0;
        }
    }
}
