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
    public class UsuarioController : ApiController
    {
        private EcuDB db = new EcuDB();

        // GET: api/Usuarios
        public IQueryable<UsuarioDto> GetUsuario()
        {
            return db.Usuarios.Select( p => new UsuarioDto
            {
                Activo = p.Activo,
                Apellido = p.Apellido,
                FechaNacimiento = p.FechaNacimiento,
                HorarioFin = p.HorarioFin,
                Identificacion = p.Identificacion,
                HorarioInicio = p.HorarioInicio,
                IdRol = p.IdRol,
                DetalleRol = p.Rol.Detalle,
                Nombre = p.Nombre
            });
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(UsuarioDto))]
        public IHttpActionResult GetUsuario(string id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(new UsuarioDto
            {
                Activo = usuario.Activo,
                Apellido = usuario.Apellido,
                FechaNacimiento = usuario.FechaNacimiento,
                HorarioFin = usuario.HorarioFin,
                Identificacion = usuario.Identificacion,
                HorarioInicio = usuario.HorarioInicio,
                IdRol = usuario.IdRol,
                DetalleRol = usuario.Rol.Detalle,
                Nombre = usuario.Nombre
            });
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(string id, UsuarioDto usuarioView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(usuarioView.Identificacion))
            {
                return BadRequest();
            }

            db.Entry(new Usuario
            {
                Activo = usuarioView.Activo,
                Apellido = usuarioView.Apellido,
                FechaNacimiento = usuarioView.FechaNacimiento,
                HorarioFin = usuarioView.HorarioFin,
                Identificacion = usuarioView.Identificacion,
                HorarioInicio = usuarioView.HorarioInicio,
                IdRol = usuarioView.IdRol,
                CambioContrasenia = true,
                Contrasenia = Encriptar(usuarioView.Identificacion),
                Nombre = usuarioView.Nombre
            }).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(UsuarioDto))]
        public IHttpActionResult PostUsuario(UsuarioDto usuarioView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (UsuarioExists(usuarioView.Identificacion))
            {
                Usuario usuario = db.Usuarios.Find(usuarioView.Identificacion);
                usuario.Activo = usuarioView.Activo;
                usuario.Apellido = usuarioView.Apellido;
                usuario.FechaNacimiento = usuarioView.FechaNacimiento;
                usuario.HorarioFin = usuarioView.HorarioFin;
                usuario.HorarioInicio = usuarioView.HorarioInicio;
                usuario.IdRol = usuarioView.IdRol;
                usuario.Nombre = usuarioView.Nombre;

                db.Entry(usuario).State = EntityState.Modified;
            }
            else
            {
                db.Usuarios.Add(new Usuario
                {
                    Activo = usuarioView.Activo,
                    Apellido = usuarioView.Apellido,
                    FechaNacimiento = usuarioView.FechaNacimiento,
                    HorarioFin = usuarioView.HorarioFin,
                    Identificacion = usuarioView.Identificacion,
                    HorarioInicio = usuarioView.HorarioInicio,
                    IdRol = usuarioView.IdRol,
                    CambioContrasenia = true,
                    Contrasenia = Encriptar(usuarioView.Identificacion),
                    Nombre = usuarioView.Nombre
                });
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuarioView.Identificacion }, usuarioView);
        }

        // DELETE: api/Usuarioes/5
        [ResponseType(typeof(UsuarioDto))]
        public IHttpActionResult DeleteUsuario(string id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(new UsuarioDto
            {
                Activo = usuario.Activo,
                Apellido = usuario.Apellido,
                FechaNacimiento = usuario.FechaNacimiento,
                HorarioFin = usuario.HorarioFin,
                Identificacion = usuario.Identificacion,
                HorarioInicio = usuario.HorarioInicio,
                IdRol = usuario.IdRol,
                //DetalleRol = usuario.Rol.Detalle,
                Nombre = usuario.Nombre
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

        private bool UsuarioExists(string id)
        {
            return db.Usuarios.Count(e => e.Identificacion.Equals(id)) > 0;
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
