using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class AsistenciaService
	{
		static readonly string Controller = "Asistencia";
		public static async Task<List<AsistenciaViewModel>> GetAllAsistenciasAsync()
		{
			return await HttpService.GetApiLista<AsistenciaViewModel>(Controller);
		}

		public static async Task<AsistenciaViewModel> GetAsistenciaAsync(int id)
		{
			return await HttpService.GetApiById<AsistenciaViewModel, int>(Controller, id);
		}
		public static async Task GuardarAsync(AsistenciaViewModel asistencia)
		{
			//Validar
			if (!AsistenciaValido(asistencia))
				return;

			await HttpService.Post(Controller, asistencia);
		}

		public static async Task EliminarAsync(AsistenciaViewModel asistencia)
		{
			await HttpService.DeleteById(Controller, asistencia.Id);
		}

		private static bool AsistenciaValido(AsistenciaViewModel asistencia)
		{
			return true;
		}
	}
}
