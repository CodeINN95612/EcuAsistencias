namespace EcuAsistencias.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EcuAsistencias.Models.EcuDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EcuAsistencias.Models.EcuDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(
                p => p.Id,
                new Models.Rol
				{
                    Detalle = "Administrador",
                    EsSupervisor = true,
				},
                new Models.Rol
                {
                    Detalle = "Supervisor",
                    EsSupervisor = true,
                },
                new Models.Rol
                {
                    Detalle = "Colaborador",
                    EsSupervisor = false,
                }
            );

            context.Motivos.AddOrUpdate(
                p => p.Id,
                new Models.Motivo
				{
                    Detalle = "Calamidad Domestica",
                    EsOtro = false
                },
                new Models.Motivo
                {
                    Detalle = "Cita Medica",
                    EsOtro = false
                },
                new Models.Motivo
                {
                    Detalle = "Almuerzo",
                    EsOtro = false
                },
                new Models.Motivo
                {
                    Detalle = "Otros",
                    EsOtro = true
                }
            );

            context.Usuarios.AddOrUpdate(
                p => p.Identificacion,
                new Models.Usuario
				{
                    Identificacion = "ADMIN",
                    Activo = true,
                    Nombre = "Administrador",
                    Apellido = "Administrador",
                    CambioContrasenia = false,
                    Contrasenia = "7CeKOJASh7J3GhNzlSA4TUPksHj3iv/nAt7xCHdMziQ=",
                    FechaNacimiento = new DateTime(2001, 01, 01),
                    HorarioInicio = new DateTime(2001, 01, 01, 1, 0, 0),
                    HorarioFin = new DateTime(2001, 01, 01, 23, 59, 59),
                    IdRol = 1
                }, new Models.Usuario
                {
                    Identificacion = "1720306867",
                    Activo = true,
                    Nombre = "Damian",
                    Apellido = "Briones",
                    CambioContrasenia = false,
                    Contrasenia = "7CeKOJASh7J3GhNzlSA4TUPksHj3iv/nAt7xCHdMziQ=",
                    FechaNacimiento = new DateTime(2001, 01, 01),
                    HorarioInicio = new DateTime(2001, 01, 01, 8, 0, 0),
                    HorarioFin = new DateTime(2001, 01, 01, 17, 0, 0),
                    IdRol = 2
                }, new Models.Usuario
                {
                    Identificacion = "9999999999",
                    Activo = true,
                    Nombre = "Otro",
                    Apellido = "Colaborador",
                    CambioContrasenia = false,
                    Contrasenia = "7CeKOJASh7J3GhNzlSA4TUPksHj3iv/nAt7xCHdMziQ=",
                    FechaNacimiento = new DateTime(2001, 01, 01),
                    HorarioInicio = new DateTime(2001, 01, 01, 5, 0, 0),
                    HorarioFin = new DateTime(2001, 01, 01, 19, 0, 0),
                    IdRol = 3
                }
            );
        }
    }
}
