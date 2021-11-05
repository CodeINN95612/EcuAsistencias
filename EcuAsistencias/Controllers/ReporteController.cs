using EcuAsistencias.Models;
using EcuAsistencias.Models.ReporteriaModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcuAsistencias.Controllers
{
    public class ReporteController : Controller
    {
        EcuDBEntities db = new EcuDBEntities();

        [HttpGet]
        public ActionResult Asistencia(DateTime? fecha)
		{
            return View(_ObtenerAsistenciaReporteModels(fecha));
		}
        
        [HttpGet]
        public ActionResult ExportarAsistencia(DateTime? fecha)
		{
            List<AsistenciaReporteModel> asistencias = _ObtenerAsistenciaReporteModels(fecha);
            if (asistencias.Count == 0)
                return RedirectToAction("Asistencia");

            using(ExcelPackage pkg = new ExcelPackage(new System.IO.FileInfo("Downloads/Asistencias.xlsx")))
			{
                ExcelWorksheet ws = pkg.Workbook.Worksheets.Add("Asistencias");

                //Header
                _CrearHeadersDefectoR4(ws, "Asistencia", fecha.GetValueOrDefault());

                //Reporte
                ws.Cells[5, 2, 5, 6].Style.Font.Bold = true;
                ws.Cells[5, 2].Value = "IDENTIFICACION";
                ws.Cells[5, 3].Value = "NOMBRE";
                ws.Cells[5, 4].Value = "ASISTIO?";
                ws.Cells[5, 5].Value = "HORA INGRESO";
                ws.Cells[5, 6].Value = "HORAS TRABAJADAS";

                int row = 6;
                foreach(AsistenciaReporteModel asistencia in asistencias)
				{
                    ws.Cells[row, 2].Value = asistencia.Identificacion;
                    ws.Cells[row, 3].Value = asistencia.Nombre;
                    ws.Cells[row, 4].Value = asistencia.Asistio;
                    ws.Cells[row, 5].Value = asistencia.HoraIngreso?.ToString("HH:mm:ss") ?? "";
                    ws.Cells[row, 6].Value = asistencia.HorasTrabajadas.ToString();
                    row++;
                }
			}

            return RedirectToAction("Asistencia");
        }

        //Metodos publicos

        [NonAction]
        public void ExportarExcel(ExcelPackage pkg)
		{
            return;
		}

        //Metodos Privados

        [NonAction]
        private List<AsistenciaReporteModel> _ObtenerAsistenciaReporteModels(DateTime? fecha)
		{
            List<AsistenciaReporteModel> resultado = new List<AsistenciaReporteModel>();

            if (fecha.HasValue && fecha < DateTime.Today)
                foreach (Usuario u in db.Usuario)
                {
                    Asistencia asistencia = u.Asistencia.FirstOrDefault(a => a.Fecha.Date == fecha.GetValueOrDefault().Date);
                    resultado.Add(new AsistenciaReporteModel
                    {
                        Asistio = asistencia != null ? "Si" : "No",
                        HoraIngreso = asistencia?.HoraIngreso,
                        HorasTrabajadas = asistencia != null ? (asistencia.HoraSalida - asistencia.HoraIngreso)?.Hours ?? null : null,
                        Identificacion = u.Identificacion,
                        Nombre = u.Nombre
                    });
                }

            return resultado;
        }

        [NonAction]
        private void _CrearHeadersDefectoR4(ExcelWorksheet ws, string reporte, DateTime fecha)
        {
            ws.Cells[2, 2, 5, 2].Style.Font.Bold = true;

            ws.Cells["B2"].Value = "Nombre Reporte:";
            ws.Cells["C2"].Value = reporte;

            ws.Cells["B3"].Value = "Fecha Impresión:";
            ws.Cells["C3"].Value = fecha.ToString("dd/MM/yyyy");

            ws.Cells["B4"].Value = "Usuario Impresión:";
            ws.Cells["C4"].Value = Session["UsuarioID"];
            ws.Cells["D4"].Value = Session["Usuario"];
        }

    }
}