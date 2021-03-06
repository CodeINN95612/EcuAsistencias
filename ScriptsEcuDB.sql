USE [master]
GO

/****** Object:  Database [EcuDB]    Script Date: 02/01/2022 22:17:58 ******/
CREATE DATABASE [EcuDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EcuDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DAMIAN\MSSQL\DATA\EcuDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EcuDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DAMIAN\MSSQL\DATA\EcuDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EcuDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [EcuDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [EcuDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [EcuDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [EcuDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [EcuDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [EcuDB] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [EcuDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [EcuDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [EcuDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [EcuDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [EcuDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [EcuDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [EcuDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [EcuDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [EcuDB] SET  ENABLE_BROKER 
GO

ALTER DATABASE [EcuDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [EcuDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [EcuDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [EcuDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [EcuDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [EcuDB] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [EcuDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [EcuDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [EcuDB] SET  MULTI_USER 
GO

ALTER DATABASE [EcuDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [EcuDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [EcuDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [EcuDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [EcuDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [EcuDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [EcuDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [EcuDB] SET  READ_WRITE 
GO

USE [EcuDB]
GO

CREATE SCHEMA [Ecu];
GO
/****** Object:  Table [Ecu].[Asistencia]    Script Date: 02/01/2022 22:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Ecu].[Asistencia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdentificacionUsuario] [nvarchar](20) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Ingreso] [datetime] NOT NULL,
	[Salida] [datetime] NULL,
 CONSTRAINT [PK_Ecu.Asistencia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Ecu].[Motivo]    Script Date: 02/01/2022 22:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Ecu].[Motivo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Detalle] [nvarchar](100) NOT NULL,
	[EsOtro] [bit] NOT NULL,
 CONSTRAINT [PK_Ecu.Motivo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Ecu].[PermisosSalida]    Script Date: 02/01/2022 22:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Ecu].[PermisosSalida](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdAsistencia] [int] NOT NULL,
	[HoraPermiso] [datetime] NOT NULL,
	[TiempoPermisoHoras] [time](7) NOT NULL,
	[IdMotivo] [int] NOT NULL,
	[MotivoOtros] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Ecu.PermisosSalida] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Ecu].[Rol]    Script Date: 02/01/2022 22:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Ecu].[Rol](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Detalle] [nvarchar](100) NOT NULL,
	[EsSupervisor] [bit] NOT NULL,
 CONSTRAINT [PK_Ecu.Rol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Ecu].[Usuario]    Script Date: 02/01/2022 22:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Ecu].[Usuario](
	[Identificacion] [nvarchar](20) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellido] [nvarchar](100) NOT NULL,
	[FechaNacimiento] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Contrasenia] [nvarchar](max) NOT NULL,
	[HorarioInicio] [datetime] NOT NULL,
	[HorarioFin] [datetime] NOT NULL,
	[CambioContrasenia] [bit] NOT NULL,
	[IdRol] [int] NOT NULL,
 CONSTRAINT [PK_Ecu.Usuario] PRIMARY KEY CLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [Ecu].[Asistencia] ON 

INSERT [Ecu].[Asistencia] ([Id], [IdentificacionUsuario], [Fecha], [Ingreso], [Salida]) VALUES (1, N'1720306867', CAST(N'2022-01-02T00:00:00.000' AS DateTime), CAST(N'2022-01-02T11:00:00.000' AS DateTime), CAST(N'2022-01-02T21:11:00.000' AS DateTime))
SET IDENTITY_INSERT [Ecu].[Asistencia] OFF
GO
SET IDENTITY_INSERT [Ecu].[Motivo] ON 

INSERT [Ecu].[Motivo] ([Id], [Detalle], [EsOtro]) VALUES (1, N'Calamidad Domestica', 0)
INSERT [Ecu].[Motivo] ([Id], [Detalle], [EsOtro]) VALUES (2, N'Cita Medica', 0)
INSERT [Ecu].[Motivo] ([Id], [Detalle], [EsOtro]) VALUES (3, N'Almuerzo', 0)
INSERT [Ecu].[Motivo] ([Id], [Detalle], [EsOtro]) VALUES (4, N'Otros', 1)
SET IDENTITY_INSERT [Ecu].[Motivo] OFF
GO
SET IDENTITY_INSERT [Ecu].[PermisosSalida] ON 

INSERT [Ecu].[PermisosSalida] ([Id], [IdAsistencia], [HoraPermiso], [TiempoPermisoHoras], [IdMotivo], [MotivoOtros]) VALUES (2, 1, CAST(N'2022-01-02T00:00:00.000' AS DateTime), CAST(N'02:00:00' AS Time), 4, N'swDWasfaw')
SET IDENTITY_INSERT [Ecu].[PermisosSalida] OFF
GO
SET IDENTITY_INSERT [Ecu].[Rol] ON 

INSERT [Ecu].[Rol] ([Id], [Detalle], [EsSupervisor]) VALUES (1, N'Administrador', 1)
INSERT [Ecu].[Rol] ([Id], [Detalle], [EsSupervisor]) VALUES (2, N'Supervisor', 1)
INSERT [Ecu].[Rol] ([Id], [Detalle], [EsSupervisor]) VALUES (3, N'Colaborador', 0)
SET IDENTITY_INSERT [Ecu].[Rol] OFF
GO
INSERT [Ecu].[Usuario] ([Identificacion], [Nombre], [Apellido], [FechaNacimiento], [Activo], [Contrasenia], [HorarioInicio], [HorarioFin], [CambioContrasenia], [IdRol]) VALUES (N'1720306867', N'Damian', N'Briones', CAST(N'2001-01-01T00:00:00.000' AS DateTime), 1, N'7CeKOJASh7J3GhNzlSA4TUPksHj3iv/nAt7xCHdMziQ=', CAST(N'2001-01-01T08:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), 0, 2)
INSERT [Ecu].[Usuario] ([Identificacion], [Nombre], [Apellido], [FechaNacimiento], [Activo], [Contrasenia], [HorarioInicio], [HorarioFin], [CambioContrasenia], [IdRol]) VALUES (N'9999999999', N'Otro', N'Colaborador', CAST(N'2001-01-01T00:00:00.000' AS DateTime), 1, N'7CeKOJASh7J3GhNzlSA4TUPksHj3iv/nAt7xCHdMziQ=', CAST(N'2001-01-01T05:00:00.000' AS DateTime), CAST(N'2001-01-01T19:00:00.000' AS DateTime), 0, 3)
INSERT [Ecu].[Usuario] ([Identificacion], [Nombre], [Apellido], [FechaNacimiento], [Activo], [Contrasenia], [HorarioInicio], [HorarioFin], [CambioContrasenia], [IdRol]) VALUES (N'ADMIN', N'Administrador', N'Administrador', CAST(N'2001-01-01T00:00:00.000' AS DateTime), 1, N'7CeKOJASh7J3GhNzlSA4TUPksHj3iv/nAt7xCHdMziQ=', CAST(N'2001-01-01T01:00:00.000' AS DateTime), CAST(N'2001-01-01T23:59:59.000' AS DateTime), 0, 1)
GO
ALTER TABLE [Ecu].[Asistencia]  WITH CHECK ADD  CONSTRAINT [FK_Ecu.Asistencia_Ecu.Usuario_IdentificacionUsuario] FOREIGN KEY([IdentificacionUsuario])
REFERENCES [Ecu].[Usuario] ([Identificacion])
ON DELETE CASCADE
GO
ALTER TABLE [Ecu].[Asistencia] CHECK CONSTRAINT [FK_Ecu.Asistencia_Ecu.Usuario_IdentificacionUsuario]
GO
ALTER TABLE [Ecu].[PermisosSalida]  WITH CHECK ADD  CONSTRAINT [FK_Ecu.PermisosSalida_Ecu.Asistencia_IdAsistencia] FOREIGN KEY([IdAsistencia])
REFERENCES [Ecu].[Asistencia] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Ecu].[PermisosSalida] CHECK CONSTRAINT [FK_Ecu.PermisosSalida_Ecu.Asistencia_IdAsistencia]
GO
ALTER TABLE [Ecu].[PermisosSalida]  WITH CHECK ADD  CONSTRAINT [FK_Ecu.PermisosSalida_Ecu.Motivo_IdMotivo] FOREIGN KEY([IdMotivo])
REFERENCES [Ecu].[Motivo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Ecu].[PermisosSalida] CHECK CONSTRAINT [FK_Ecu.PermisosSalida_Ecu.Motivo_IdMotivo]
GO
ALTER TABLE [Ecu].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Ecu.Usuario_Ecu.Rol_IdRol] FOREIGN KEY([IdRol])
REFERENCES [Ecu].[Rol] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Ecu].[Usuario] CHECK CONSTRAINT [FK_Ecu.Usuario_Ecu.Rol_IdRol]
GO
