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
using EcuAsistencias.Core.ViewModels;
using EcuAsistencias.Models;

namespace EcuAsistencias.Controllers.API
{
    public class MotivoWebController : ApiController
    {
        private EcuDB db = new EcuDB();

        public IQueryable<MotivoViewModel> GetMotivo()
        {

            return db.Motivos.Select(p => new MotivoViewModel
            {
                Detalle = p.Detalle,
                EsOtro = p.EsOtro,
                Id = p.Id         
            });
        }
        // GET: api/Motivo/5
        [ResponseType(typeof(MotivoViewModel))]
        public IHttpActionResult GetMotivo(string id)
        {
            Motivo motivo = db.Motivos.Find(id);
            if (motivo == null)
            {
                return NotFound();
            }

            return Ok(new MotivoViewModel
            {
                Detalle = motivo.Detalle,
                EsOtro = motivo.EsOtro,
                Id = motivo.Id
                
            });
        }

        // PUT: api/Motivos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMotivo(string id, MotivoViewModel motivoView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(motivoView.Id))
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
        [ResponseType(typeof(MotivoViewModel))]
        public IHttpActionResult PostMotivo(MotivoViewModel motivoView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Motivos.Add(new Motivo
            {
                Detalle = motivoView.Detalle,
                EsOtro = motivoView.EsOtro,
                Id = motivoView.Id
            });
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = motivoView.Id }, motivoView);
        }

        // DELETE: api/Motivoes/5
        [ResponseType(typeof(MotivoViewModel))]
        public IHttpActionResult DeleteMotivo(string id)
        {
            Motivo motivo = db.Motivos.Find(id);
            if (motivo == null)
            {
                return NotFound();
            }

            db.Motivos.Remove(motivo);
            db.SaveChanges();

            return Ok(new MotivoViewModel
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

        private bool MotivoExists(string id)
        {
            return db.Motivos.Count(e => e.Id.Equals(id)) > 0;
        }

     
    }
}