using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcuAsistencias.Models.ReporteriaModels
{
	public class ReporteModel
	{
		[Display(Name ="Nombre Reporte")]
		[DataType(DataType.Text)]
		public string Nombre { get; set; }

		public string Accion { get; set; }
	}

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

	public class ReporteAsistenciaMensualModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public int CantidadDiasAsistidos { get; set; }
		public int CantidadHorasTrabajadas { get; set; }
	}

	public class ReportePermisosDiariosModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public string PidioPermiso { get; set; }

		[DataType(DataType.Time)]
		public DateTime? HoraInicioPermiso { get; set; }
		public TimeSpan? HorasPermiso { get; set; }
		public string Motivo { get; set; }
	}

	public class ReportePermisosMensualesModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public int CantidadPermisosPedidos { get; set; }
		public int TotalHorasPermiso { get; set; }
	}

}