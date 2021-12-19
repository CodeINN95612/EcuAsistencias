using EcuAsistencias.Models;
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
  //      EcuDBEntities db = new EcuDBEntities();

  //      [HttpGet]
  //      public ActionResult Lista()
		//{
  //          List<ReporteModel> reportes = new List<ReporteModel>();
  //          reportes.Add(new ReporteModel { Nombre = "Asistencia Diaria", Accion = "AsistenciaDiaria" });
  //          reportes.Add(new ReporteModel { Nombre = "Asistencia Mensual", Accion = "AsistenciaMensual" });
  //          reportes.Add(new ReporteModel { Nombre = "Permisos Diarios", Accion = "PermisosDiarios" });
  //          reportes.Add(new ReporteModel { Nombre = "Permisos Mensuales", Accion = "PermisosMensuales" });

  //          return View(reportes);
		//}

  //      [HttpGet]
  //      public ActionResult AsistenciaDiaria(DateTime? fechaDia)
		//{
  //          if (fechaDia is null)
  //              return View(new List<ReporteAsistenciaDiariaModel>());

  //          List<ReporteAsistenciaDiariaModel> reporte = new List<ReporteAsistenciaDiariaModel>();
  //          foreach (Usuario u in db.Usuario)
		//	{
  //              Asistencia asistencia = db.Asistencia.FirstOrDefault(a => a.Fecha == (fechaDia ?? DateTime.MinValue));
  //              ReporteAsistenciaDiariaModel actual = new ReporteAsistenciaDiariaModel
  //              {
  //                  Asistio = asistencia is null ? "No" : "Si",
  //                  HoraIngreso = asistencia?.HoraIngreso,
  //                  HoraSalida = asistencia?.HoraSalida,
  //                  Identificacion = u.Identificacion,
  //                  Nombre = u.NombreCompleto
  //              };
  //              reporte.Add(actual);
  //          }
  //          return View(reporte);
		//}

  //      [HttpGet]
  //      public ActionResult AsistenciaMensual(DateTime? fechaMes)
  //      {
  //          if (fechaMes is null)
  //              return View(new List<ReporteAsistenciaMensualModel>());

  //          List<ReporteAsistenciaMensualModel> reporte = new List<ReporteAsistenciaMensualModel>();
  //          foreach(Usuario u in db.Usuario)
		//	{
  //              List<Asistencia> asistencias = u.Asistencia.Where(p => p.Fecha.Year == (fechaMes ?? DateTime.MinValue).Year
  //                  && p.Fecha.Month == (fechaMes ?? DateTime.MinValue).Month).ToList();
  //              ReporteAsistenciaMensualModel actual = new ReporteAsistenciaMensualModel
  //              {
  //                  CantidadDiasAsistidos = asistencias.Count(),
  //                  CantidadHorasTrabajadas = asistencias.Count() == 0 ? 0 : asistencias.Sum(p => (p.HoraSalida ?? p.HoraIngreso).Hour - p.HoraIngreso.Hour),
  //                  Identificacion = u.Identificacion,
  //                  Nombre = u.NombreCompleto
  //              };
  //              reporte.Add(actual);
  //          }
  //          return View(reporte);
  //      }

  //      [HttpGet]
  //      public ActionResult PermisosDiarios(DateTime? fechaDia)
  //      {
  //          if (fechaDia is null)
  //              return View(new List<ReportePermisosDiariosModel>());

  //          List<ReportePermisosDiariosModel> reporte = new List<ReportePermisosDiariosModel>();
  //          foreach (Usuario u in db.Usuario)
  //          {
  //              Asistencia asistencia = db.Asistencia.FirstOrDefault(a => a.Fecha == (fechaDia ?? DateTime.MinValue));
  //              PermisosSalida permisos = asistencia?.PermisosSalida?.First();

  //              ReportePermisosDiariosModel actual = new ReportePermisosDiariosModel
  //              {
  //                  Identificacion = u.Identificacion,
  //                  Nombre = u.NombreCompleto,
  //                  PidioPermiso = permisos is null ? "No" : "Si",
  //                  HoraInicioPermiso = permisos?.HoraPermiso,
  //                  HorasPermiso = permisos?.TiempoPermisoHoras,
  //                  Motivo = permisos is null ? null : (permisos.Motivo.ID == 4 ? permisos.MotivoOtros : permisos.Motivo.Detalle)
  //              };
  //              reporte.Add(actual);
  //          }
  //          return View(reporte);

  //      }

  //      [HttpGet]
  //      public ActionResult PermisosMensuales(DateTime? fechaMes)
  //      {
  //          if (fechaMes is null)
  //              return View(new List<ReportePermisosMensualesModel>());

  //          List<ReportePermisosMensualesModel> reporte = new List<ReportePermisosMensualesModel>();
  //          foreach (Usuario u in db.Usuario)
  //          {
  //              List<Asistencia> asistencias = u.Asistencia.Where(p => p.Fecha.Year == (fechaMes ?? DateTime.MinValue).Year
  //                  && p.Fecha.Month == (fechaMes ?? DateTime.MinValue).Month).ToList();
  //              List<PermisosSalida> permisos = asistencias.Count() == 0 ? asistencias.Select(p => p.PermisosSalida.First()).ToList() : new List<PermisosSalida>();
  //              ReportePermisosMensualesModel actual = new ReportePermisosMensualesModel
  //              {
  //                  CantidadPermisosPedidos = permisos.Count(),
  //                  Identificacion = u.Identificacion,
  //                  Nombre = u.NombreCompleto,
  //                  TotalHorasPermiso = permisos.Sum(p => p.TiempoPermisoHoras.Hours)
  //              };
  //              reporte.Add(actual);
  //          }
  //          return View(reporte);
  //      }

    }
}