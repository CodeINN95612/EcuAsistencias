using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Core.ViewModels
{
	[Table("Asistencia", Schema = "Ecu")]
	public class AsistenciaViewModel
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

	}
}