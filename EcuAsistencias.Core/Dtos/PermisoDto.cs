using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Core.Dtos
{
	[Table("PermisosSalida", Schema = "Ecu")]
	public class PermisoSalidaDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IdAsistencia { get; set; }

        [Required]
        public DateTime HoraPermiso { get; set; }

        [Required]
        public TimeSpan TiempoPermisoHoras { get; set; }

        [Required]
        public int IdMotivo { get; set; }

        [MaxLength(300)]
        [Required]
        public string MotivoOtros { get; set; }
    }
}