using EcuAsistencias.Core.Dtos;
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
    public class PermisoSalidaController : ApiController
    {
        private EcuDB db = new EcuDB();

        public IQueryable<PermisoSalidaDto> GetPermisoSalida()
        {

            return db.PermisosSalida.Select(p => new PermisoSalidaDto
            {
                Id = p.Id,
                HoraPermiso = p.HoraPermiso,
                IdAsistencia = p.IdAsistencia,
                IdMotivo = p.IdMotivo,
                MotivoOtros = p.MotivoOtros,
                TiempoPermisoHoras = p.TiempoPermisoHoras,
            });
        }
        // GET: api/PermisoSalida/5
        [ResponseType(typeof(PermisoSalidaDto))]
        public IHttpActionResult GetPermisoSalida(int id)
        {
            PermisoSalida permisoSalida = db.PermisosSalida.Find(id);
            if (permisoSalida == null)
            {
                return NotFound();
            }

            return Ok(new PermisoSalidaDto
            {
                Id = permisoSalida.Id,
                HoraPermiso = permisoSalida.HoraPermiso,
                IdAsistencia = permisoSalida.IdAsistencia,
                IdMotivo = permisoSalida.IdMotivo,
                MotivoOtros = permisoSalida.MotivoOtros,
                TiempoPermisoHoras = permisoSalida.TiempoPermisoHoras
            });
        }

        // PUT: api/PermisoSalidas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermisoSalida(int id, PermisoSalidaDto permisoSalidaView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permisoSalidaView.Id)
            {
                return BadRequest();
            }

            db.Entry(new PermisoSalida
            {
                Id = permisoSalidaView.Id,
                HoraPermiso = permisoSalidaView.HoraPermiso,
                IdAsistencia = permisoSalidaView.IdAsistencia,
                IdMotivo = permisoSalidaView.IdMotivo,
                MotivoOtros = permisoSalidaView.MotivoOtros,
                TiempoPermisoHoras = permisoSalidaView.TiempoPermisoHoras
            }).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisoSalidaExists(id))
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

        // POST: api/PermisoSalidas
        [ResponseType(typeof(PermisoSalidaDto))]
        public IHttpActionResult PostPermisoSalida(PermisoSalidaDto permisoSalidaView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(PermisoSalidaExists(permisoSalidaView.Id))
            {
                PermisoSalida permiso = db.PermisosSalida.Find(permisoSalidaView.Id);
                permiso.HoraPermiso = permisoSalidaView.HoraPermiso;
                permiso.IdAsistencia = permisoSalidaView.IdAsistencia;
                permiso.IdMotivo = permisoSalidaView.IdMotivo;
                permiso.MotivoOtros = permisoSalidaView.MotivoOtros;
                permiso.TiempoPermisoHoras = permisoSalidaView.TiempoPermisoHoras;

                db.Entry(permiso).State = EntityState.Modified;
            }
			else
			{
                db.PermisosSalida.Add(new PermisoSalida
                {
                    Id = permisoSalidaView.Id,
                    HoraPermiso = permisoSalidaView.HoraPermiso,
                    IdAsistencia = permisoSalidaView.IdAsistencia,
                    IdMotivo = permisoSalidaView.IdMotivo,
                    MotivoOtros = permisoSalidaView.MotivoOtros,
                    TiempoPermisoHoras = permisoSalidaView.TiempoPermisoHoras
                });
            }

            
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = permisoSalidaView.Id }, permisoSalidaView);
        }

        // DELETE: api/PermisoSalidaes/5
        [ResponseType(typeof(PermisoSalidaDto))]
        public IHttpActionResult DeletePermisoSalida(int id)
        {
            PermisoSalida permisoSalida = db.PermisosSalida.Find(id);
            if (permisoSalida == null)
            {
                return NotFound();
            }

            db.PermisosSalida.Remove(permisoSalida);
            db.SaveChanges();

            return Ok(new PermisoSalidaDto
            {
                Id = permisoSalida.Id,
                HoraPermiso = permisoSalida.HoraPermiso,
                IdAsistencia = permisoSalida.IdAsistencia,
                IdMotivo = permisoSalida.IdMotivo,
                MotivoOtros = permisoSalida.MotivoOtros,
                TiempoPermisoHoras = permisoSalida.TiempoPermisoHoras
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

        private bool PermisoSalidaExists(int id)
        {
            return db.PermisosSalida.Count(e => e.Id == id) > 0;
        }
    }
}
