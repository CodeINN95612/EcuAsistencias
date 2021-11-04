using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcuAsistencias.Models.ViewModels
{
	public class UsuarioLoginViewModel
	{
		[Required]
		[DataType(DataType.Text)]
		public string Identificacion { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Contrasenia { get; set; }

	}
}