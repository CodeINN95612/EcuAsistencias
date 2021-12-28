using System;
using System.ComponentModel.DataAnnotations;

namespace EcuAsistencias.Core.ReportModels
{
	public class ReporteAsistenciaDiariaModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public string Asistio { get; set; }

		[DataType(DataType.Time)]
		public DateTime? HoraIngreso { get; set; }

		[DataType(DataType.Time)]
		public DateTime? HoraSalida { get; set; }
	}

}