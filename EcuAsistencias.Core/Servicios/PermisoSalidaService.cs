using EcuAsistencias.Core.ViewModels;
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
		public static async Task<List<PermisoSalidaViewModel>> GetAllPermisosSalidaAsync()
		{
			return await HttpService.GetApiLista<PermisoSalidaViewModel>(Controller);
		}

		public static async Task<PermisoSalidaViewModel> GetPermisoSalidaAsync(int id)
		{
			return await HttpService.GetApiById<PermisoSalidaViewModel, int>(Controller, id);
		}
		public static async Task GuardarAsync(PermisoSalidaViewModel permisoSalida)
		{
			//Validar
			if (!await PermisoSalidaValido(permisoSalida))
				return;

			await HttpService.Post(Controller, permisoSalida);
		}

		public static async Task EliminarAsync(PermisoSalidaViewModel permisoSalida)
		{
			await HttpService.DeleteById(Controller, permisoSalida.Id);
		}

		private static async Task<bool> PermisoSalidaValido(PermisoSalidaViewModel permisoSalida)
		{
			List<MotivoViewModel> motivos = await MotivoService.GetAllMotivosAsync();
			MotivoViewModel motivoActual = motivos.FirstOrDefault(p => p.Id == permisoSalida.IdMotivo);
			if (motivoActual is null)
				return false;

			if (motivoActual.EsOtro && string.IsNullOrEmpty(permisoSalida.MotivoOtros))
				return false;

			return true;
		}
	}
}
