using EcuAsistencias.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class UsuarioService
	{
		static readonly string Controller = "Usuario";
		public static async Task<List<UsuarioDto>> GetAllUsuariosAsync()
		{
			return await HttpService.GetApiLista<UsuarioDto>(Controller);
		}
		
		public static async Task<UsuarioDto> GetUsuarioAsync(string identificacion)
		{
			return await HttpService.GetApiById<UsuarioDto, string>(Controller, identificacion);
		}

		public static async Task CrearAsync(UsuarioDto usuario)
		{
			//Validar
			if (!UsuarioValido(usuario))
				return;

			await HttpService.Post(Controller, usuario);
		}

		public static async Task EditarAsync(UsuarioDto usuario)
		{
			//Validar
			if (!UsuarioValido(usuario))
				return;

			await HttpService.Post(Controller, usuario);
		}

		public static async Task EliminarAsync(UsuarioDto usuario)
		{
			await HttpService.DeleteById(Controller, usuario.Identificacion);
		}

		private static bool UsuarioValido(UsuarioDto usuario)
		{
			if (string.IsNullOrEmpty(usuario.Identificacion) || string.IsNullOrEmpty(usuario.Nombre) 
				|| string.IsNullOrEmpty(usuario.Apellido) || usuario.IdRol == 0)
				return false;
			return true;
		}
	}
}
