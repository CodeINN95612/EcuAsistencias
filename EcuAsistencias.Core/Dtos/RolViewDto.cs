using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Core.Dtos
{
	[Table("Rol", Schema = "Ecu")]
	public class RolDto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(100)]
		[Required]
		public string Detalle { get; set; }

		[Required]
		public bool EsSupervisor { get; set; }

	}
}