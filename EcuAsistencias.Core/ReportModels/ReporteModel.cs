using System.ComponentModel.DataAnnotations;

namespace EcuAsistencias.Core.ReportModels
{
	public class ReporteModel
	{
		[Display(Name ="Nombre Reporte")]
		[DataType(DataType.Text)]
		public string Nombre { get; set; }

		public string Accion { get; set; }
	}

}