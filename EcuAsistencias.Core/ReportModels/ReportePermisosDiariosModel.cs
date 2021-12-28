using System;
using System.ComponentModel.DataAnnotations;

namespace EcuAsistencias.Core.ReportModels
{
	public class ReportePermisosDiariosModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public string PidioPermiso { get; set; }
		public int CantidadPermisosPedidos { get; set; }
	}

}