namespace EcuAsistencias.Core.ReportModels
{
	public class ReporteAsistenciaMensualModel
	{
		public string Identificacion { get; set; }
		public string Nombre { get; set; }
		public int CantidadDiasAsistidos { get; set; }
		public int CantidadHorasTrabajadas { get; set; }
	}

}