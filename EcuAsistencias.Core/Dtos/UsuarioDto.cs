using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcuAsistencias.Core.Dtos
{
	public class UsuarioLoginDto
	{
		[Required(ErrorMessage = "Identificacion Obligatoria", AllowEmptyStrings = false)]
		[DataType(DataType.Text)]
		[Display(Name = "Identificacion")]
		[StringLength(20, ErrorMessage = "La Identificacion debe tener maximo 20 caracteres")]
		public string Identificacion { get; set; }

		[Required(ErrorMessage = "Contraseña Obligatoria", AllowEmptyStrings = false)]
		[DataType(DataType.Password)]
		[Display(Name = "Contraseña")]
		[StringLength(100, ErrorMessage = "La Identificacion debe tener maximo 100 caracteres")]
		public string Contrasenia { get; set; }

	}
	public class UsuarioDto
	{
		[Required(ErrorMessage = "Identificacion Obligatoria", AllowEmptyStrings = false)]
		[DataType(DataType.Text)]
		[Display(Name = "Identificacion")]
		[StringLength(20, ErrorMessage = "La Identificacion debe tener maximo 20 caracteres")]
		public string Identificacion { get; set; }

		[Required(ErrorMessage = "Nombre Obligatorio", AllowEmptyStrings = false)]
		[DataType(DataType.Text)]
		[Display(Name = "Nombre")]
		[StringLength(100, ErrorMessage = "El Nombre debe tener maximo 100 caracteres")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "Apellido Obligatorio", AllowEmptyStrings = false)]
		[DataType(DataType.Text)]
		[Display(Name = "Nombre")]
		[StringLength(100, ErrorMessage = "El Apellido debe tener maximo 100 caracteres")]
		public string Apellido { get; set; }

		[Required(ErrorMessage = "Fecha de Nacimiento Obligatoria")]
		[DataType(DataType.Date)]
		[Display(Name = "Fecha de Nacimiento")]
		public DateTime FechaNacimiento { get; set; }

		[Required(ErrorMessage = "Activo Obligatorio")]
		[Display(Name = "Activo")]
		public bool Activo { get; set; }

		[Required(ErrorMessage = "Hora Inicio Jornada Obligatoria")]
		[DataType(DataType.Time)]
		[Display(Name = "Hora Inicio Jornada")]
		public DateTime HorarioInicio { get; set; }

		[Required(ErrorMessage = "Hora Fin Jornada Obligatoria")]
		[DataType(DataType.Time)]
		[Display(Name = "Hora Fin Jornada")]
		public DateTime HorarioFin { get; set; }

		[Required(ErrorMessage = "Rol obligatorio")]
		[Display(Name = "Rol")]
		public int IdRol { get; set; }
		public string DetalleRol { get; set; }
	}
}