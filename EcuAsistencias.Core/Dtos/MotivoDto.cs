using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Core.Dtos
{
	[Table("Motivo", Schema = "Ecu")]
	public class MotivoDto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(100)]
		[Required]
		public string Detalle { get; set; }

		[Required]
		public bool EsOtro { get; set; }
	}
}