using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcuAsistencias.Models.ReporteriaModels
{
	public class AsistenciaReporteModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public string Asistio { get; set; }

		[DataType(DataType.Time)]
		public DateTime? HoraIngreso { get; set; }
		public int? HorasTrabajadas { get; set; }
	}
}