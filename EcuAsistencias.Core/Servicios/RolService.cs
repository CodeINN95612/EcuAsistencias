﻿using EcuAsistencias.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcuAsistencias.Core.Servicios
{
	public class RolService
	{
		static readonly string Controller = "Rol";
		public static async Task<List<RolViewModel>> GetAllRolesAsync()
		{
			return await HttpService.GetApiLista<RolViewModel>(Controller);
		}

		public static async Task<RolViewModel> GetRolAsync(int id)
		{
			return await HttpService.GetApiById<RolViewModel, int>(Controller, id);
		}
		public static async Task GuardarAsync(RolViewModel rol)
		{
			//Validar
			if (!RolValido(rol))
				return;

			await HttpService.Post(Controller, rol);
		}

		public static async Task EliminarAsync(RolViewModel rol)
		{
			await HttpService.DeleteById(Controller, rol.Id);
		}

		private static bool RolValido(RolViewModel rol)
		{
			if (string.IsNullOrEmpty(rol.Detalle))
				return false;
			return true;
		}
	}
}
