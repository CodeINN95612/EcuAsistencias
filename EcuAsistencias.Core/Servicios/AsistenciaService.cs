using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class AsistenciaService
	{
		static readonly string Controller = "Asistencia";
		public static async Task<List<AsistenciaDto>> GetAllAsistenciasAsync()
		{
			return await HttpService.GetApiLista<AsistenciaDto>(Controller);
		}

		public static async Task<AsistenciaDto> GetAsistenciaAsync(int id)
		{
			return await HttpService.GetApiById<AsistenciaDto, int>(Controller, id);
		}
		public static async Task GuardarAsync(AsistenciaDto asistencia)
		{
			//Validar
			if (!AsistenciaValido(asistencia))
				return;

			await HttpService.Post(Controller, asistencia);
		}

		public static async Task EliminarAsync(AsistenciaDto asistencia)
		{
			await HttpService.DeleteById(Controller, asistencia.Id);
		}

		private static bool AsistenciaValido(AsistenciaDto asistencia)
		{
			return true;
		}
	}
}
