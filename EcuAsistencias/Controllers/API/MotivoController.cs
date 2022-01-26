using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using EcuAsistencias.Core.Dtos;
using EcuAsistencias.Models;

namespace EcuAsistencias.Controllers.API
{
    public class MotivoController : ApiController
    {
        private EcuDB db = new EcuDB();

        public IQueryable<MotivoDto> GetMotivo()
        {

            return db.Motivos.Select(p => new MotivoDto
            {
                Detalle = p.Detalle,
                EsOtro = p.EsOtro,
                Id = p.Id         
            });
        }
        // GET: api/Motivo/5
        [ResponseType(typeof(MotivoDto))]
        public IHttpActionResult GetMotivo(int id)
        {
            Motivo motivo = db.Motivos.Find(id);
            if (motivo == null)
            {
                return NotFound();
            }

            return Ok(new MotivoDto
            {
                Detalle = motivo.Detalle,
                EsOtro = motivo.EsOtro,
                Id = motivo.Id
                
            });
        }

        // PUT: api/Motivos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMotivo(int id, MotivoDto motivoView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != motivoView.Id)
            {
                return BadRequest();
            }

            db.Entry(new Motivo
            {
                Detalle = motivoView.Detalle,
                EsOtro= motivoView.EsOtro,
                Id = motivoView.Id
                
            }).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotivoExists(id))
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

        // POST: api/Motivos
        [ResponseType(typeof(MotivoDto))]
        public IHttpActionResult PostMotivo(MotivoDto motivoView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (MotivoExists(motivoView.Id))
            {
                Motivo motivo = db.Motivos.Find(motivoView.Id);
                motivo.Detalle = motivoView.Detalle;
                motivo.EsOtro = motivoView.EsOtro;

                db.Entry(motivo).State = EntityState.Modified;
            }
            else
            {
                db.Motivos.Add(new Motivo
                {
                    Detalle = motivoView.Detalle,
                    EsOtro = motivoView.EsOtro,
                    Id = motivoView.Id
                });
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = motivoView.Id }, motivoView);
        }

        // DELETE: api/Motivoes/5
        [ResponseType(typeof(MotivoDto))]
        public IHttpActionResult DeleteMotivo(int id)
        {
            Motivo motivo = db.Motivos.Find(id);
            if (motivo == null)
            {
                return NotFound();
            }

            db.Motivos.Remove(motivo);
            db.SaveChanges();

            return Ok(new MotivoDto
            {
                Detalle = motivo.Detalle,
                EsOtro = motivo.EsOtro,
                Id = motivo.Id
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

        private bool MotivoExists(int id)
        {
            return db.Motivos.Count(e => e.Id == id) > 0;
        }

     
    }
}