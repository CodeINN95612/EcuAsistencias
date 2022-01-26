using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class MotivoService
	{
		static readonly string Controller = "Motivo";
		public static async Task<List<MotivoDto>> GetAllMotivosAsync()
		{
			return await HttpService.GetApiLista<MotivoDto>(Controller);
		}

		public static async Task<MotivoDto> GetMotivoAsync(int id)
		{
			return await HttpService.GetApiById<MotivoDto, int>(Controller, id);
		}
		public static async Task GuardarAsync(MotivoDto motivo)
		{
			//Validar
			if (!MotivoValido(motivo))
				return;

			await HttpService.Post(Controller, motivo);
		}

		public static async Task EliminarAsync(MotivoDto motivo)
		{
			await HttpService.DeleteById(Controller, motivo.Id);
		}

		private static bool MotivoValido(MotivoDto motivo)
		{
			if (string.IsNullOrEmpty(motivo.Detalle))
				return false;
			return true;
		}
	}
}
