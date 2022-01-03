using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class MotivoService
	{
		static readonly string Controller = "Motivo";
		public static async Task<List<MotivoViewModel>> GetAllMotivosAsync()
		{
			return await HttpService.GetApiLista<MotivoViewModel>(Controller);
		}

		public static async Task<MotivoViewModel> GetMotivoAsync(int id)
		{
			return await HttpService.GetApiById<MotivoViewModel, int>(Controller, id);
		}
		public static async Task GuardarAsync(MotivoViewModel motivo)
		{
			//Validar
			if (!MotivoValido(motivo))
				return;

			await HttpService.Post(Controller, motivo);
		}

		public static async Task EliminarAsync(MotivoViewModel motivo)
		{
			await HttpService.DeleteById(Controller, motivo.Id);
		}

		private static bool MotivoValido(MotivoViewModel motivo)
		{
			if (string.IsNullOrEmpty(motivo.Detalle))
				return false;
			return true;
		}
	}
}
