using EcuAsistencias.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcuAsistencias.Core.ReportModels;

namespace EcuAsistencias.Controllers
{
    public class ReporteController : Controller
    {
		EcuDB db = new EcuDB();

		[HttpGet]
		public ActionResult Lista()
		{
			List<ReporteModel> reportes = new List<ReporteModel>();
			reportes.Add(new ReporteModel { Nombre = "Asistencia Diaria", Accion = "AsistenciaDiaria" });
			reportes.Add(new ReporteModel { Nombre = "Asistencia Mensual", Accion = "AsistenciaMensual" });
			reportes.Add(new ReporteModel { Nombre = "Permisos Diarios", Accion = "PermisosDiarios" });
			reportes.Add(new ReporteModel { Nombre = "Permisos Mensuales", Accion = "PermisosMensuales" });

			return View(reportes);
		}

		[HttpGet]
		public ActionResult AsistenciaDiaria(DateTime? fechaDia)
		{
			if (fechaDia is null)
				return View(new List<ReporteAsistenciaDiariaModel>());

			List<ReporteAsistenciaDiariaModel> reporte = new List<ReporteAsistenciaDiariaModel>();
			DateTime diaReporte = fechaDia.GetValueOrDefault();
			
			foreach (Usuario u in db.Usuarios.ToList())
			{
				Asistencia asistencia = u.Asistencias.FirstOrDefault(a => a.Fecha == diaReporte);
				ReporteAsistenciaDiariaModel actual = new ReporteAsistenciaDiariaModel
				{
					Asistio = asistencia is null ? "No" : "Si",
					HoraIngreso = asistencia?.Ingreso,
					HoraSalida = asistencia?.Salida,
					Identificacion = u.Identificacion,
					Nombre = u.Nombre
				};
				reporte.Add(actual);
			}
			return View(reporte);
		}

		[HttpGet]
		public ActionResult AsistenciaMensual(DateTime? fechaMes)
		{
			if (fechaMes is null)
				return View(new List<ReporteAsistenciaMensualModel>());

			List<ReporteAsistenciaMensualModel> reporte = new List<ReporteAsistenciaMensualModel>();
			DateTime mesReporte = fechaMes.GetValueOrDefault();
			foreach (Usuario u in db.Usuarios.ToList())
			{
				List<Asistencia> asistencias = u.Asistencias.Where(
					p => p.Fecha.Year == mesReporte.Year && p.Fecha.Month == mesReporte.Month
				).ToList();

				int cantidad = asistencias.Count();

				ReporteAsistenciaMensualModel actual = new ReporteAsistenciaMensualModel
				{
					CantidadDiasAsistidos = cantidad,
					CantidadHorasTrabajadas = cantidad == 0 ? 0 : asistencias.Sum(p => (p.Salida ?? p.Ingreso).Hour - p.Ingreso.Hour),
					Identificacion = u.Identificacion,
					Nombre = u.Nombre
				};
				reporte.Add(actual);
			}
			return View(reporte);
		}

		[HttpGet]
		public ActionResult PermisosDiarios(DateTime? fechaDia)
		{
			if (fechaDia is null)
				return View(new List<ReportePermisosDiariosModel>());

			List<ReportePermisosDiariosModel> reporte = new List<ReportePermisosDiariosModel>();
			DateTime diaReporte = fechaDia.GetValueOrDefault();

			foreach (Usuario u in db.Usuarios.ToList())
			{
				Asistencia asistencia = db.Asistencias.FirstOrDefault(a => a.Fecha == diaReporte);
				List<PermisoSalida> permisos = asistencia?.PermisosSalida?.ToList();

				ReportePermisosDiariosModel actual = new ReportePermisosDiariosModel
				{
					Identificacion = u.Identificacion,
					Nombre = u.Nombre,
					PidioPermiso = permisos is null ? "No" : "Si",
					CantidadPermisosPedidos = permisos.Count()
				};
				reporte.Add(actual);
				
			}
			return View(reporte);

		}

		[HttpGet]
		public ActionResult PermisosMensuales(DateTime? fechaMes)
		{
			if (fechaMes is null)
				return View(new List<ReportePermisosMensualesModel>());

			List<ReportePermisosMensualesModel> reporte = new List<ReportePermisosMensualesModel>();
			DateTime mesReporte = fechaMes.GetValueOrDefault();

			foreach (Usuario u in db.Usuarios.ToList())
			{
				List<Asistencia> asistencias = u.Asistencias.Where(
					p => p.Fecha.Year == mesReporte.Year && p.Fecha.Month == mesReporte.Month
				).ToList();

				List<PermisoSalida> permisos = new List<PermisoSalida>();
				foreach (Asistencia asistencia in asistencias)
				{
					permisos.AddRange(asistencia.PermisosSalida);
				}
				ReportePermisosMensualesModel actual = new ReportePermisosMensualesModel
				{
					CantidadPermisosPedidos = permisos.Count(),
					Identificacion = u.Identificacion,
					Nombre = u.Nombre,
					TotalHorasPermiso = permisos.Sum(p => p.TiempoPermisoHoras.Hours)
				};
				reporte.Add(actual);
			}
			return View(reporte);
		}

	}
}