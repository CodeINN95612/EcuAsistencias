using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class RolService
	{
		static readonly string Controller = "Rol";
		public static async Task<List<RolDto>> GetAllRolesAsync()
		{
			return await HttpService.GetApiLista<RolDto>(Controller);
		}

		public static async Task<RolDto> GetRolAsync(int id)
		{
			return await HttpService.GetApiById<RolDto, int>(Controller, id);
		}
		public static async Task GuardarAsync(RolDto rol)
		{
			//Validar
			if (!RolValido(rol))
				return;

			await HttpService.Post(Controller, rol);
		}

		public static async Task EliminarAsync(RolDto rol)
		{
			await HttpService.DeleteById(Controller, rol.Id);
		}

		private static bool RolValido(RolDto rol)
		{
			if (string.IsNullOrEmpty(rol.Detalle))
				return false;
			return true;
		}
	}
}
