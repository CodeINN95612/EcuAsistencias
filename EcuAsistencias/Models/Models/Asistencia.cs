using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Models
{
	[Table("Asistencia", Schema = "Ecu")]
	public class Asistencia
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(20)]
		[Required]
		public string IdentificacionUsuario { get; set; }

		[Required]
		public DateTime Fecha { get; set; }

		[Required]
		public DateTime Ingreso { get; set; }

		public DateTime? Salida { get; set; }


		[ForeignKey("IdentificacionUsuario")]
		[Association("Usuario", "IdentificacionUsuario", "Identificacion", IsForeignKey = true)]
		public virtual Usuario Usuario { get; set; }

		public virtual ICollection<PermisoSalida> PermisosSalida { get; set; }
	}
}