using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class PermisoSalidaService
	{
		static readonly string Controller = "PermisoSalida";
		public static async Task<List<PermisoSalidaDto>> GetAllPermisosSalidaAsync()
		{
			return await HttpService.GetApiLista<PermisoSalidaDto>(Controller);
		}

		public static async Task<PermisoSalidaDto> GetPermisoSalidaAsync(int id)
		{
			return await HttpService.GetApiById<PermisoSalidaDto, int>(Controller, id);
		}
		public static async Task GuardarAsync(PermisoSalidaDto permisoSalida)
		{
			//Validar
			if (!await PermisoSalidaValido(permisoSalida))
				return;

			await HttpService.Post(Controller, permisoSalida);
		}

		public static async Task EliminarAsync(PermisoSalidaDto permisoSalida)
		{
			await HttpService.DeleteById(Controller, permisoSalida.Id);
		}

		private static async Task<bool> PermisoSalidaValido(PermisoSalidaDto permisoSalida)
		{
			List<MotivoDto> motivos = await MotivoService.GetAllMotivosAsync();
			MotivoDto motivoActual = motivos.FirstOrDefault(p => p.Id == permisoSalida.IdMotivo);
			if (motivoActual is null)
				return false;

			if (motivoActual.EsOtro && string.IsNullOrEmpty(permisoSalida.MotivoOtros))
				return false;

			return true;
		}
	}
}
