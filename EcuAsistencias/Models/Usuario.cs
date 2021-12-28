using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Models
{
	[Table("Usuario", Schema = "Ecu")]
	public class Usuario
	{
		[Key]
		[MaxLength(20)]
		public string Identificacion { get; set; }

		[MaxLength(100)]
		[Required]
		public string Nombre { get; set; }

		[MaxLength(100)]
		[Required]
		public string Apellido { get; set; }

		[Required]
		public DateTime FechaNacimiento { get; set; }

		[Required]
		public bool Activo { get; set; }

		[MaxLength(int.MaxValue)]
		[Required]
		public string Contrasenia { get; set; }

		[Required]
		public DateTime HorarioInicio { get; set; }

		[Required]
		public DateTime HorarioFin { get; set; }

		[Required]
		public bool CambioContrasenia { get; set; }

		[Required]
		public int IdRol { get; set; }


		public virtual ICollection<Asistencia> Asistencias { get; set; }

		[ForeignKey("IdRol")]
		[Association("Rol", "IdRol", "Id", IsForeignKey = true)]
		public virtual Rol Rol { get; set; }
	}
}