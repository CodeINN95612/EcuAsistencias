using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Models
{
	[Table("Motivo", Schema = "Ecu")]
	public class Motivo
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