using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class UsuarioService
	{
		static readonly string Controller = "Usuario";
		public static async Task<List<UsuarioViewModel>> GetAllUsuariosAsync()
		{
			return await HttpService.GetApiLista<UsuarioViewModel>(Controller);
		}
		
		public static async Task<UsuarioViewModel> GetUsuarioAsync(string identificacion)
		{
			return await HttpService.GetApiById<UsuarioViewModel, string>(Controller, identificacion);
		}

		public static async Task CrearAsync(UsuarioViewModel usuario)
		{
			//Validar
			if (!UsuarioValido(usuario))
				return;

			await HttpService.Post(Controller, usuario);
		}

		public static async Task EditarAsync(UsuarioViewModel usuario)
		{
			//Validar
			if (!UsuarioValido(usuario))
				return;

			await HttpService.Post(Controller, usuario);
		}

		public static async Task EliminarAsync(UsuarioViewModel usuario)
		{
			await HttpService.DeleteById(Controller, usuario.Identificacion);
		}

		private static bool UsuarioValido(UsuarioViewModel usuario)
		{
			if (string.IsNullOrEmpty(usuario.Identificacion) || string.IsNullOrEmpty(usuario.Nombre) 
				|| string.IsNullOrEmpty(usuario.Apellido) || usuario.IdRol == 0)
				return false;
			return true;
		}
	}
}
