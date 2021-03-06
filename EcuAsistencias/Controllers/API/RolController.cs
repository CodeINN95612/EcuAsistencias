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
    public class RolController : ApiController
    {
        private EcuDB db = new EcuDB();

        public IQueryable<RolDto> GetRol()
        {

            return db.Roles.Select(p => new RolDto
            {
                Detalle = p.Detalle,
                EsSupervisor = p.EsSupervisor,
                Id = p.Id
            });
        }
        // GET: api/Rol/5
        [ResponseType(typeof(RolDto))]
        public IHttpActionResult GetRol(int id)
        {
            Rol rol = db.Roles.Find(id);
            if (rol == null)
            {
                return NotFound();
            }

            return Ok(new RolDto
            {
                Detalle = rol.Detalle,
                EsSupervisor = rol.EsSupervisor,
                Id = rol.Id
            });
        }

        // PUT: api/Rols/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRol(int id, RolDto rolView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rolView.Id)
            {
                return BadRequest();
            }

            db.Entry(new Rol
            {
                Detalle = rolView.Detalle,
                EsSupervisor = rolView.EsSupervisor,
                Id = rolView.Id

            }).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // POST: api/Rols
        [ResponseType(typeof(RolDto))]
        public IHttpActionResult PostRol(RolDto rolView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(RolExists(rolView.Id))
			{
                Rol rol = db.Roles.Find(rolView.Id);
                rol.EsSupervisor = rolView.EsSupervisor;
                rol.Detalle = rolView.Detalle;

                db.Entry(rol).State = EntityState.Modified;
            }
			else
            {
                db.Roles.Add(new Rol
                {
                    Detalle = rolView.Detalle,
                    EsSupervisor = rolView.EsSupervisor
                });
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rolView.Id }, rolView);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(RolDto))]
        public IHttpActionResult DeleteRol(int id)
        {
            Rol Rol = db.Roles.Find(id);
            if (Rol == null)
            {
                return NotFound();
            }

            db.Roles.Remove(Rol);
            db.SaveChanges();

            return Ok(new RolDto
            {
                Detalle = Rol.Detalle,
                EsSupervisor = Rol.EsSupervisor,
                Id = Rol.Id
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

        private bool RolExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}