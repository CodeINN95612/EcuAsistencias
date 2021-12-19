using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EcuAsistencias.Models
{
	public class EcuDB : DbContext
	{
		public virtual DbSet<Rol> Roles { get; set; }
		public virtual DbSet<Motivo> Motivos { get; set; }
		public virtual DbSet<Usuario> Usuarios { get; set; }
		public virtual DbSet<Asistencia> Asistencias { get; set; }
		public virtual DbSet<PermisoSalida> PermisosSalida { get; set; }
	}
}