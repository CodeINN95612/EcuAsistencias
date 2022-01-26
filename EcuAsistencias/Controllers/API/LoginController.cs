using EcuAsistencias.Core.Dtos;
using EcuAsistencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace EcuAsistencias.Controllers.API
{
	public class LoginController : ApiController
    {
        private EcuDB db = new EcuDB();

        [ResponseType(typeof(UsuarioDto))]
        public IHttpActionResult GetLogin(LoginDto login)
        {
            Usuario usuario = db.Usuarios.Find(login.Identificacion);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(new UsuarioDto
            {
                
            });
        }
	}
}