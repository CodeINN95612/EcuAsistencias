namespace EcuAsistencias.Core.ReportModels
{
	public class ReportePermisosMensualesModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public int CantidadPermisosPedidos { get; set; }
		public int TotalHorasPermiso { get; set; }
	}

}