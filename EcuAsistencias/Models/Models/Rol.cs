using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Models
{
	[Table("Rol", Schema = "Ecu")]
	public class Rol
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(100)]
		[Required]
		public string Detalle { get; set; }

		[Required]
		public bool EsSupervisor { get; set; }

		public virtual ICollection<Usuario> Usuarios { get; set; }
	}
}