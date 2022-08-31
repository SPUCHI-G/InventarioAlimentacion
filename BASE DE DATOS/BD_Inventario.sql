USE [master]
GO
/****** Object:  Database [BD_Inventario]    Script Date: 31/08/2022 1:47:21 ******/
CREATE DATABASE [BD_Inventario]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_Inventario', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BD_Inventario.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BD_Inventario_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BD_Inventario_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BD_Inventario] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_Inventario].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BD_Inventario] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_Inventario] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_Inventario] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_Inventario] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_Inventario] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_Inventario] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BD_Inventario] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_Inventario] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_Inventario] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_Inventario] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_Inventario] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_Inventario] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_Inventario] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_Inventario] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_Inventario] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BD_Inventario] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_Inventario] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_Inventario] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BD_Inventario] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BD_Inventario] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_Inventario] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_Inventario] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BD_Inventario] SET RECOVERY FULL 
GO
ALTER DATABASE [BD_Inventario] SET  MULTI_USER 
GO
ALTER DATABASE [BD_Inventario] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_Inventario] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BD_Inventario] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BD_Inventario] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BD_Inventario] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BD_Inventario] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BD_Inventario', N'ON'
GO
ALTER DATABASE [BD_Inventario] SET QUERY_STORE = OFF
GO
USE [BD_Inventario]
GO
/****** Object:  FullTextCatalog [CATEGORIA]    Script Date: 31/08/2022 1:47:21 ******/
CREATE FULLTEXT CATALOG [CATEGORIA] 
GO
/****** Object:  Table [dbo].[Cargo]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cargo](
	[ID_Cargo] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Cargo] PRIMARY KEY CLUSTERED 
(
	[ID_Cargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[ID_Categoria] [int] NOT NULL,
	[Cat_Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[ID_Categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudad]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudad](
	[ID_Ciudad] [int] NOT NULL,
	[Ciu_Nombre] [varchar](30) NULL,
 CONSTRAINT [PK_Ciudad] PRIMARY KEY CLUSTERED 
(
	[ID_Ciudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Detalle_Entrada]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalle_Entrada](
	[ID_Det_entr] [bigint] NOT NULL,
	[fk_Entrada] [bigint] NULL,
	[fk_TipoEntrada] [int] NULL,
	[fk_Proveedor] [int] NULL,
	[fk_Donante] [int] NULL,
	[fk_Producto] [bigint] NULL,
	[Ent_Cantidad_Peso] [decimal](18, 2) NULL,
	[Ent_Factura] [nvarchar](50) NULL,
	[Ent_Precio_Unitario] [decimal](18, 2) NULL,
	[Ent_Precio_Total] [decimal](18, 2) NULL,
	[Ent_iva] [decimal](18, 2) NULL,
	[Ent_Descuento] [decimal](18, 2) NULL,
	[Ent_Temperatura] [decimal](18, 2) NULL,
	[Ent_FechVencim] [date] NULL,
	[Dispo_stock] [bit] NULL,
 CONSTRAINT [PK_Detalle_Entrada] PRIMARY KEY CLUSTERED 
(
	[ID_Det_entr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Detalle_Salida]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalle_Salida](
	[ID_Det_salid] [bigint] NOT NULL,
	[fk_Salida] [bigint] NOT NULL,
	[fk_TipoSalida] [int] NOT NULL,
	[fk_Producto] [bigint] NOT NULL,
	[Sal_Cantidad_Peso] [decimal](18, 2) NOT NULL,
	[Sal_Observacion] [nvarchar](500) NULL,
 CONSTRAINT [PK_Detalle_Salida] PRIMARY KEY CLUSTERED 
(
	[ID_Det_salid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Donante]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donante](
	[ID_Donante] [int] NOT NULL,
	[Don_Nombre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Donante] PRIMARY KEY CLUSTERED 
(
	[ID_Donante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[Emp_Documento] [int] NOT NULL,
	[Emp_Nombre] [nvarchar](50) NOT NULL,
	[Emp_Apellido] [nvarchar](50) NOT NULL,
	[Emp_Telefono] [bigint] NOT NULL,
	[Emp_Celular] [bigint] NOT NULL,
	[Emp_Email] [nvarchar](100) NOT NULL,
	[fk_Cargo] [int] NOT NULL,
	[Emp_Area] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__BigTable__6BEF7C312963C7E9] PRIMARY KEY CLUSTERED 
(
	[Emp_Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entrada]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrada](
	[ID_Entrada] [bigint] NOT NULL,
	[fk_Empleado] [int] NOT NULL,
	[Ent_Fecha] [datetime] NOT NULL,
	[Ent_Total] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Entrada] PRIMARY KEY CLUSTERED 
(
	[ID_Entrada] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nom_Producto]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nom_Producto](
	[ID_NomProducto] [int] NOT NULL,
	[Subcategoria] [int] NULL,
	[NomProducto] [varchar](50) NULL,
	[Min] [decimal](18, 2) NULL,
	[Punto_pedido] [decimal](18, 2) NULL,
	[Prop_Adicion] [bit] NULL,
 CONSTRAINT [PK_Nom_Producto] PRIMARY KEY CLUSTERED 
(
	[ID_NomProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[ID_Producto] [bigint] NOT NULL,
	[fk_Categoria] [int] NOT NULL,
	[fk_Subcategoria] [int] NOT NULL,
	[fk_NomProducto] [int] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[ID_Producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[Prov_Docum] [int] NOT NULL,
	[fk_Ciudad] [int] NULL,
	[fk_TipoDocum] [int] NULL,
	[Prov_Nombre] [nvarchar](10) NULL,
	[Prov_Telefono] [int] NULL,
	[Prov_Celular] [int] NULL,
	[Prov_Email] [nvarchar](50) NULL,
	[Prov_Direccion] [nvarchar](10) NULL,
	[Prov_Fax] [nvarchar](10) NULL,
	[Prov_ActiEcono] [nvarchar](10) NULL,
 CONSTRAINT [PK_Proveedor}] PRIMARY KEY CLUSTERED 
(
	[Prov_Docum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Salida]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salida](
	[ID_Salida] [bigint] NOT NULL,
	[fk_Empleado] [int] NULL,
	[Sali_Fecha] [datetime] NULL,
 CONSTRAINT [PK_Salida] PRIMARY KEY CLUSTERED 
(
	[ID_Salida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subcategoria]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subcategoria](
	[ID_Subcategoria] [int] NOT NULL,
	[Categoria] [int] NULL,
	[Subca_Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Subcategoria] PRIMARY KEY CLUSTERED 
(
	[ID_Subcategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Temporal]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temporal](
	[ID_Det_entr] [bigint] NOT NULL,
	[fk_Entrada] [bigint] NULL,
	[fk_TipoEntrada] [int] NULL,
	[fk_Proveedor] [int] NULL,
	[fk_Donante] [int] NULL,
	[fk_Producto] [bigint] NULL,
	[Ent_Cantidad_Peso] [decimal](18, 2) NULL,
	[Ent_Factura] [nvarchar](50) NULL,
	[Ent_Precio_Unitario] [decimal](18, 2) NULL,
	[Ent_Precio_Total] [decimal](18, 2) NULL,
	[Ent_iva] [decimal](18, 2) NULL,
	[Ent_Descuento] [decimal](18, 2) NULL,
	[Ent_Temperatura] [decimal](18, 2) NULL,
	[Ent_FechVencim] [date] NULL,
	[ID_Categoria] [int] NULL,
	[ID_Subcategoria] [int] NULL,
	[ID_NomProducto] [int] NULL,
 CONSTRAINT [PK_Temporal] PRIMARY KEY CLUSTERED 
(
	[ID_Det_entr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Temporal2]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temporal2](
	[ID_Det_salid] [bigint] NOT NULL,
	[fk_Salida] [bigint] NULL,
	[fk_TipoSalida] [int] NULL,
	[fk_Producto] [bigint] NULL,
	[Sal_Cantidad_Peso] [decimal](18, 2) NULL,
	[Sal_Observacion] [nvarchar](500) NULL,
	[ID_Categoria] [int] NULL,
	[ID_Subcategoria] [int] NULL,
	[ID_NomProducto] [int] NULL,
 CONSTRAINT [PK_Temporal2] PRIMARY KEY CLUSTERED 
(
	[ID_Det_salid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Documento]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Documento](
	[ID_TipDocum] [int] NOT NULL,
	[TipoDocumento] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tipo_Documento] PRIMARY KEY CLUSTERED 
(
	[ID_TipDocum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Entrada]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Entrada](
	[ID_TipoEntrada] [int] NOT NULL,
	[Tipo_Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Tipo_Entrada_1] PRIMARY KEY CLUSTERED 
(
	[ID_TipoEntrada] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Salida]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Salida](
	[ID_TipoSalida] [int] NOT NULL,
	[Tipo_Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Tipo_Salida] PRIMARY KEY CLUSTERED 
(
	[ID_TipoSalida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 31/08/2022 1:47:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Usu_Nombre] [nvarchar](50) NOT NULL,
	[Usu_Contraseña] [nvarchar](50) NULL,
	[Usu_Activo] [int] NULL,
	[ID_Empleado] [int] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Usu_Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Cargo] ([ID_Cargo], [Nombre]) VALUES (1, N'sistemas')
INSERT [dbo].[Cargo] ([ID_Cargo], [Nombre]) VALUES (2, N'empleado')
INSERT [dbo].[Cargo] ([ID_Cargo], [Nombre]) VALUES (3, N'usuario')
GO
INSERT [dbo].[Categoria] ([ID_Categoria], [Cat_Nombre]) VALUES (1, N'lacteos')
INSERT [dbo].[Categoria] ([ID_Categoria], [Cat_Nombre]) VALUES (4, N'frutas')
INSERT [dbo].[Categoria] ([ID_Categoria], [Cat_Nombre]) VALUES (5, N'Legumbres')
GO
INSERT [dbo].[Ciudad] ([ID_Ciudad], [Ciu_Nombre]) VALUES (1, N'Medellin')
INSERT [dbo].[Ciudad] ([ID_Ciudad], [Ciu_Nombre]) VALUES (2, N'Bogotá')
INSERT [dbo].[Ciudad] ([ID_Ciudad], [Ciu_Nombre]) VALUES (3, N'CAli')
INSERT [dbo].[Ciudad] ([ID_Ciudad], [Ciu_Nombre]) VALUES (4, N'Cartagena')
GO
INSERT [dbo].[Detalle_Entrada] ([ID_Det_entr], [fk_Entrada], [fk_TipoEntrada], [fk_Proveedor], [fk_Donante], [fk_Producto], [Ent_Cantidad_Peso], [Ent_Factura], [Ent_Precio_Unitario], [Ent_Precio_Total], [Ent_iva], [Ent_Descuento], [Ent_Temperatura], [Ent_FechVencim], [Dispo_stock]) VALUES (3, 3, 2, 1, 10075241, 1, CAST(15.00 AS Decimal(18, 2)), N'NA', CAST(5000.00 AS Decimal(18, 2)), CAST(110000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(N'2022-08-26' AS Date), 1)
INSERT [dbo].[Detalle_Entrada] ([ID_Det_entr], [fk_Entrada], [fk_TipoEntrada], [fk_Proveedor], [fk_Donante], [fk_Producto], [Ent_Cantidad_Peso], [Ent_Factura], [Ent_Precio_Unitario], [Ent_Precio_Total], [Ent_iva], [Ent_Descuento], [Ent_Temperatura], [Ent_FechVencim], [Dispo_stock]) VALUES (4, 4, 1, 15468421, 1, 2, CAST(40.00 AS Decimal(18, 2)), N'NA', CAST(2500.00 AS Decimal(18, 2)), CAST(220000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), CAST(N'2025-10-19' AS Date), 0)
INSERT [dbo].[Detalle_Entrada] ([ID_Det_entr], [fk_Entrada], [fk_TipoEntrada], [fk_Proveedor], [fk_Donante], [fk_Producto], [Ent_Cantidad_Peso], [Ent_Factura], [Ent_Precio_Unitario], [Ent_Precio_Total], [Ent_iva], [Ent_Descuento], [Ent_Temperatura], [Ent_FechVencim], [Dispo_stock]) VALUES (5, 0, 1, 15468421, 1, 2, CAST(37.00 AS Decimal(18, 2)), N'NA', CAST(2500.00 AS Decimal(18, 2)), CAST(220000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), CAST(N'2025-10-19' AS Date), 1)
GO
INSERT [dbo].[Detalle_Salida] ([ID_Det_salid], [fk_Salida], [fk_TipoSalida], [fk_Producto], [Sal_Cantidad_Peso], [Sal_Observacion]) VALUES (1, 1, 2, 2, CAST(1.00 AS Decimal(18, 2)), N'')
INSERT [dbo].[Detalle_Salida] ([ID_Det_salid], [fk_Salida], [fk_TipoSalida], [fk_Producto], [Sal_Cantidad_Peso], [Sal_Observacion]) VALUES (2, 2, 1, 2, CAST(2.00 AS Decimal(18, 2)), N'')
GO
INSERT [dbo].[Donante] ([ID_Donante], [Don_Nombre]) VALUES (1, N'No Aplica')
INSERT [dbo].[Donante] ([ID_Donante], [Don_Nombre]) VALUES (10075241, N'Rosio DUque')
GO
INSERT [dbo].[Empleado] ([Emp_Documento], [Emp_Nombre], [Emp_Apellido], [Emp_Telefono], [Emp_Celular], [Emp_Email], [fk_Cargo], [Emp_Area]) VALUES (1007240613, N'sara', N'garces', 3117073904, 3117073904, N'garcessara99@gmail.com', 1, N'1')
INSERT [dbo].[Empleado] ([Emp_Documento], [Emp_Nombre], [Emp_Apellido], [Emp_Telefono], [Emp_Celular], [Emp_Email], [fk_Cargo], [Emp_Area]) VALUES (1007240616, N'sarañ', N'garces', 789, 4654789, N'gadad', 2, N'dasdasd')
GO
INSERT [dbo].[Entrada] ([ID_Entrada], [fk_Empleado], [Ent_Fecha], [Ent_Total]) VALUES (1, 1007240613, CAST(N'2022-08-31T01:22:22.840' AS DateTime), NULL)
INSERT [dbo].[Entrada] ([ID_Entrada], [fk_Empleado], [Ent_Fecha], [Ent_Total]) VALUES (2, 1007240613, CAST(N'2022-08-31T01:24:03.837' AS DateTime), NULL)
INSERT [dbo].[Entrada] ([ID_Entrada], [fk_Empleado], [Ent_Fecha], [Ent_Total]) VALUES (3, 1007240613, CAST(N'2022-08-31T01:24:33.630' AS DateTime), NULL)
INSERT [dbo].[Entrada] ([ID_Entrada], [fk_Empleado], [Ent_Fecha], [Ent_Total]) VALUES (4, 1007240613, CAST(N'2022-08-31T01:26:00.583' AS DateTime), NULL)
GO
INSERT [dbo].[Nom_Producto] ([ID_NomProducto], [Subcategoria], [NomProducto], [Min], [Punto_pedido], [Prop_Adicion]) VALUES (1, 1, N'packx6', CAST(16.00 AS Decimal(18, 2)), CAST(18.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Nom_Producto] ([ID_NomProducto], [Subcategoria], [NomProducto], [Min], [Punto_pedido], [Prop_Adicion]) VALUES (2, 1, N'Quesito', CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[Producto] ([ID_Producto], [fk_Categoria], [fk_Subcategoria], [fk_NomProducto]) VALUES (1, 1, 1, 1)
INSERT [dbo].[Producto] ([ID_Producto], [fk_Categoria], [fk_Subcategoria], [fk_NomProducto]) VALUES (2, 1, 1, 2)
GO
INSERT [dbo].[Proveedor] ([Prov_Docum], [fk_Ciudad], [fk_TipoDocum], [Prov_Nombre], [Prov_Telefono], [Prov_Celular], [Prov_Email], [Prov_Direccion], [Prov_Fax], [Prov_ActiEcono]) VALUES (1, 1, 1, N'NO APLICA', 0, 0, N'Na', N'Na', N'0', N'NA')
INSERT [dbo].[Proveedor] ([Prov_Docum], [fk_Ciudad], [fk_TipoDocum], [Prov_Nombre], [Prov_Telefono], [Prov_Celular], [Prov_Email], [Prov_Direccion], [Prov_Fax], [Prov_ActiEcono]) VALUES (15468421, 2, 1, N'Jose', 1234566, 45678, N'jose@j.com', N'calle32b', N'212', N'lacteos')
GO
INSERT [dbo].[Salida] ([ID_Salida], [fk_Empleado], [Sali_Fecha]) VALUES (1, 1007240613, CAST(N'2022-08-31T01:36:16.587' AS DateTime))
INSERT [dbo].[Salida] ([ID_Salida], [fk_Empleado], [Sali_Fecha]) VALUES (2, 1007240613, CAST(N'2022-08-31T01:36:53.660' AS DateTime))
GO
INSERT [dbo].[Subcategoria] ([ID_Subcategoria], [Categoria], [Subca_Nombre]) VALUES (1, 1, N'Leche')
GO
INSERT [dbo].[Tipo_Documento] ([ID_TipDocum], [TipoDocumento]) VALUES (1, N'CEdula Ciudadania')
INSERT [dbo].[Tipo_Documento] ([ID_TipDocum], [TipoDocumento]) VALUES (2, N'Nit')
INSERT [dbo].[Tipo_Documento] ([ID_TipDocum], [TipoDocumento]) VALUES (3, N'	PASAPORTE')
INSERT [dbo].[Tipo_Documento] ([ID_TipDocum], [TipoDocumento]) VALUES (4, N'Extrajeria')
GO
INSERT [dbo].[Tipo_Entrada] ([ID_TipoEntrada], [Tipo_Nombre]) VALUES (1, N'Compra')
INSERT [dbo].[Tipo_Entrada] ([ID_TipoEntrada], [Tipo_Nombre]) VALUES (2, N'Donacion')
GO
INSERT [dbo].[Tipo_Salida] ([ID_TipoSalida], [Tipo_Nombre]) VALUES (1, N'vencido')
INSERT [dbo].[Tipo_Salida] ([ID_TipoSalida], [Tipo_Nombre]) VALUES (2, N'cocina')
INSERT [dbo].[Tipo_Salida] ([ID_TipoSalida], [Tipo_Nombre]) VALUES (3, N'devolucion')
GO
INSERT [dbo].[Usuario] ([Usu_Nombre], [Usu_Contraseña], [Usu_Activo], [ID_Empleado]) VALUES (N'1007240613', N'123', 1, 1007240613)
GO
ALTER TABLE [dbo].[Detalle_Salida]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Salida_Producto] FOREIGN KEY([fk_Producto])
REFERENCES [dbo].[Producto] ([ID_Producto])
GO
ALTER TABLE [dbo].[Detalle_Salida] CHECK CONSTRAINT [FK_Detalle_Salida_Producto]
GO
ALTER TABLE [dbo].[Detalle_Salida]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Salida_Salida] FOREIGN KEY([fk_Salida])
REFERENCES [dbo].[Salida] ([ID_Salida])
GO
ALTER TABLE [dbo].[Detalle_Salida] CHECK CONSTRAINT [FK_Detalle_Salida_Salida]
GO
ALTER TABLE [dbo].[Detalle_Salida]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Salida_Tipo_Salida] FOREIGN KEY([fk_TipoSalida])
REFERENCES [dbo].[Tipo_Salida] ([ID_TipoSalida])
GO
ALTER TABLE [dbo].[Detalle_Salida] CHECK CONSTRAINT [FK_Detalle_Salida_Tipo_Salida]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Cargo] FOREIGN KEY([fk_Cargo])
REFERENCES [dbo].[Cargo] ([ID_Cargo])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Cargo]
GO
ALTER TABLE [dbo].[Entrada]  WITH CHECK ADD  CONSTRAINT [FK_Entrada_Empleado] FOREIGN KEY([fk_Empleado])
REFERENCES [dbo].[Empleado] ([Emp_Documento])
GO
ALTER TABLE [dbo].[Entrada] CHECK CONSTRAINT [FK_Entrada_Empleado]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([fk_Categoria])
REFERENCES [dbo].[Categoria] ([ID_Categoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Nom_Producto] FOREIGN KEY([fk_NomProducto])
REFERENCES [dbo].[Nom_Producto] ([ID_NomProducto])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Nom_Producto]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Subcategoria] FOREIGN KEY([fk_Subcategoria])
REFERENCES [dbo].[Subcategoria] ([ID_Subcategoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Subcategoria]
GO
ALTER TABLE [dbo].[Proveedor]  WITH NOCHECK ADD  CONSTRAINT [FK_Proveedor_Ciudad] FOREIGN KEY([fk_Ciudad])
REFERENCES [dbo].[Ciudad] ([ID_Ciudad])
GO
ALTER TABLE [dbo].[Proveedor] CHECK CONSTRAINT [FK_Proveedor_Ciudad]
GO
ALTER TABLE [dbo].[Proveedor]  WITH NOCHECK ADD  CONSTRAINT [FK_Proveedor_Tipo_Documento] FOREIGN KEY([fk_TipoDocum])
REFERENCES [dbo].[Tipo_Documento] ([ID_TipDocum])
GO
ALTER TABLE [dbo].[Proveedor] CHECK CONSTRAINT [FK_Proveedor_Tipo_Documento]
GO
ALTER TABLE [dbo].[Salida]  WITH CHECK ADD  CONSTRAINT [FK_Salida_Salida] FOREIGN KEY([fk_Empleado])
REFERENCES [dbo].[Empleado] ([Emp_Documento])
GO
ALTER TABLE [dbo].[Salida] CHECK CONSTRAINT [FK_Salida_Salida]
GO
ALTER TABLE [dbo].[Temporal]  WITH CHECK ADD  CONSTRAINT [FK_Temporal_Donante] FOREIGN KEY([fk_Donante])
REFERENCES [dbo].[Donante] ([ID_Donante])
GO
ALTER TABLE [dbo].[Temporal] CHECK CONSTRAINT [FK_Temporal_Donante]
GO
ALTER TABLE [dbo].[Temporal]  WITH CHECK ADD  CONSTRAINT [FK_Temporal_Entrada] FOREIGN KEY([fk_Entrada])
REFERENCES [dbo].[Entrada] ([ID_Entrada])
GO
ALTER TABLE [dbo].[Temporal] CHECK CONSTRAINT [FK_Temporal_Entrada]
GO
ALTER TABLE [dbo].[Temporal]  WITH CHECK ADD  CONSTRAINT [FK_Temporal_Producto] FOREIGN KEY([fk_Producto])
REFERENCES [dbo].[Producto] ([ID_Producto])
GO
ALTER TABLE [dbo].[Temporal] CHECK CONSTRAINT [FK_Temporal_Producto]
GO
ALTER TABLE [dbo].[Temporal]  WITH CHECK ADD  CONSTRAINT [FK_Temporal_Proveedor] FOREIGN KEY([fk_Proveedor])
REFERENCES [dbo].[Proveedor] ([Prov_Docum])
GO
ALTER TABLE [dbo].[Temporal] CHECK CONSTRAINT [FK_Temporal_Proveedor]
GO
ALTER TABLE [dbo].[Temporal]  WITH CHECK ADD  CONSTRAINT [FK_Temporal_Tipo_Entrada] FOREIGN KEY([fk_TipoEntrada])
REFERENCES [dbo].[Tipo_Entrada] ([ID_TipoEntrada])
GO
ALTER TABLE [dbo].[Temporal] CHECK CONSTRAINT [FK_Temporal_Tipo_Entrada]
GO
ALTER TABLE [dbo].[Temporal2]  WITH CHECK ADD  CONSTRAINT [FK_Temporal2_Producto] FOREIGN KEY([fk_Producto])
REFERENCES [dbo].[Producto] ([ID_Producto])
GO
ALTER TABLE [dbo].[Temporal2] CHECK CONSTRAINT [FK_Temporal2_Producto]
GO
ALTER TABLE [dbo].[Temporal2]  WITH CHECK ADD  CONSTRAINT [FK_Temporal2_Salida] FOREIGN KEY([fk_Salida])
REFERENCES [dbo].[Salida] ([ID_Salida])
GO
ALTER TABLE [dbo].[Temporal2] CHECK CONSTRAINT [FK_Temporal2_Salida]
GO
ALTER TABLE [dbo].[Temporal2]  WITH CHECK ADD  CONSTRAINT [FK_Temporal2_Tipo_Salida] FOREIGN KEY([fk_TipoSalida])
REFERENCES [dbo].[Tipo_Salida] ([ID_TipoSalida])
GO
ALTER TABLE [dbo].[Temporal2] CHECK CONSTRAINT [FK_Temporal2_Tipo_Salida]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Empleado1] FOREIGN KEY([ID_Empleado])
REFERENCES [dbo].[Empleado] ([Emp_Documento])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Empleado1]
GO
/****** Object:  StoredProcedure [dbo].[spAgregar_a_Detalle]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAgregar_a_Detalle]
AS
begin
		INSERT INTO Detalle_Entrada( ID_Det_entr,fk_Entrada,fk_TipoEntrada,fk_Proveedor,fk_Donante,fk_Producto,
				Ent_Cantidad_Peso, Ent_Factura, Ent_Precio_Unitario,Ent_Precio_Total, Ent_iva, Ent_Descuento,
				Ent_Temperatura, Ent_FechVencim,Dispo_stock)
		SELECT ID_Det_entr,fk_Entrada,fk_TipoEntrada,fk_Proveedor,fk_Donante,fk_Producto,
				Ent_Cantidad_Peso, Ent_Factura, Ent_Precio_Unitario,Ent_Precio_Total, Ent_iva, Ent_Descuento,
				Ent_Temperatura, Ent_FechVencim,'True' FROM Temporal 
		delete from Temporal
end
GO
/****** Object:  StoredProcedure [dbo].[spAgregar_a_Detalle2]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAgregar_a_Detalle2]
AS
begin
DECLARE @intFlag INT;
DECLARE @contador int;
declare @cant_Stock decimal(18,2);
DECLARE @cant_Salida decimal(18,2);
DEclare @ID bigint;

select @contador = ( select count(*) from Temporal2)-- hace el conteo de cuantos registros hay en el temporal de salida
select @intFlag = 1 
delete from Temporal;
	WHILE (@intFlag <= @contador) --while para que saque uno por uno los registros y poder maniobrar con los id 
	BEGIN

		select @cant_Salida = (SELECT Top 1 Sal_Cantidad_Peso FROM Temporal2)-- saco la cantidad que deseo sacar 
		
		--selecciona el registro con fecha de vencimiento mas proximo que esten en stock y sean igual al producto que desean sacar	
		select @cant_Stock = (select top 1 Ent_Cantidad_Peso from Detalle_Entrada 
		where fk_Producto = (SELECT Top 1 fk_Producto FROM Temporal2) and Dispo_stock = 'True' 
		order by Ent_FechVencim asc )

		--el mismo filtro solo que guarda el ID
		select @ID = (select top 1 ID_Det_entr from Detalle_Entrada 
		where fk_Producto = (SELECT Top 1 fk_Producto FROM Temporal2) and Dispo_stock = 'True' 
		order by Ent_FechVencim asc )
--si son iguales funciona
		if(@cant_Salida = @cant_Stock)
		begin
		--funciona si son iguales implemente actualiza la variable a false en detalle de entrada
		if exists (select * from Detalle_Entrada WHERE ID_Det_entr = @ID and fk_Entrada = 0)
		begin
			delete from Detalle_Entrada where ID_Det_entr = @ID and fk_Entrada = 0
		end
		else
		begin
			UPDATE Detalle_Entrada
			SET Dispo_stock='False' 
			WHERE ID_Det_entr = @ID
		end
			
		-- funciona registra en detale de salida este registro que sacamos
			INSERT INTO Detalle_Salida( ID_Det_salid,fk_Salida,fk_TipoSalida,fk_Producto, -- se guarda la salida con la informacion 
					Sal_Cantidad_Peso,Sal_Observacion)
			SELECT ID_Det_salid,fk_Salida,fk_TipoSalida,fk_Producto,
						Sal_Cantidad_Peso,Sal_Observacion FROM Temporal2
				where ID_Det_salid = (SELECT Top 1  ID_Det_salid FROM Temporal2)
		--funciona elimina del temporal de salida el dato que acabamos de guardar en detalle de salida
		delete from Temporal2 where ID_Det_salid = (SELECT Top 1 t.ID_Det_salid FROM Temporal2 t)
		end
		else
			begin
--si es menor funciona

			if(@cant_Salida < @cant_Stock)
			begin 
				set @cant_Stock = @cant_Stock- @cant_Salida
				if exists (select * from Detalle_Entrada WHERE ID_Det_entr = @ID and fk_Entrada = 0)
				begin 
					UPDATE Detalle_Entrada
					SET Ent_Cantidad_Peso = @cant_Stock 
					WHERE ID_Det_entr = @ID and fk_Entrada = 0
				end
				else
				begin
					--funciona si es menor creo un registro ID= 0 donde tiene la cantidad que quedo restando
					INSERT INTO Detalle_Entrada( ID_Det_entr,fk_Entrada,fk_TipoEntrada,fk_Proveedor,fk_Donante,fk_Producto, Ent_Cantidad_Peso,
					Ent_Factura,Ent_Precio_Unitario,Ent_Precio_Total,Ent_iva, Ent_Descuento, Ent_Temperatura, Ent_FechVencim,Dispo_stock)
					SELECT ((select max(t.ID_Det_entr)from Detalle_Entrada t) + 1),0,fk_TipoEntrada,fk_Proveedor,fk_Donante,fk_Producto,
							@cant_Stock,Ent_Factura,Ent_Precio_Unitario,Ent_Precio_Total,Ent_iva, Ent_Descuento,Ent_Temperatura,Ent_FechVencim,Dispo_stock FROM Detalle_Entrada
							where ID_Det_entr = @ID
					--Funciona si es menor actualiza la variable a false en detalle de entrada ya que necesitamos de otro registro
					UPDATE Detalle_Entrada
					SET Dispo_stock='False' 
					WHERE ID_Det_entr = @ID
				end
				--Funciona registra en detale de salida del registro que sacamos
				INSERT INTO Detalle_Salida( ID_Det_salid,fk_Salida,fk_TipoSalida,fk_Producto, -- se guarda la salida con la informacion 
						Sal_Cantidad_Peso,Sal_Observacion)
				SELECT ID_Det_salid,fk_Salida,fk_TipoSalida,fk_Producto,
							Sal_Cantidad_Peso,Sal_Observacion FROM Temporal2
					where ID_Det_salid = (SELECT Top 1  ID_Det_salid FROM Temporal2)
				--funciona elimina del temporal de salida el dato que acabamos de guardar en detalle de salida
				delete from Temporal2 where ID_Det_salid = (SELECT Top 1 t.ID_Det_salid FROM Temporal2 t)
			end
				else
				begin
-- si es mayor funciona
				if(@cant_Salida > @cant_Stock)
				begin
					while(@cant_Salida>@cant_Stock)
					begin
						--funciona----------------
						set @cant_Salida = @cant_Salida-@cant_Stock
						--funciona---------------------
						if exists (select * from Detalle_Entrada WHERE ID_Det_entr = @ID and fk_Entrada = 0)--para no dejar el registro 0 temporal
						begin 
							delete from Detalle_Entrada where ID_Det_entr = @ID and fk_Entrada = 0
						end
						else
						begin
							UPDATE Detalle_Entrada
							SET Dispo_stock='False' 
							WHERE ID_Det_entr = @ID
						end
						--------------------------------
						--funciona llamar de nuevo el ultimo registro
						select @cant_Stock = (select top 1 Ent_Cantidad_Peso from Detalle_Entrada 
						where fk_Producto = (SELECT Top 1 fk_Producto FROM Temporal2) and Dispo_stock = 'True' 
						order by Ent_FechVencim asc )
						print @cant_Stock
						--funciona el mismo filtro solo que guarda el ID
						select @ID = (select top 1 ID_Det_entr from Detalle_Entrada 
						where fk_Producto = (SELECT Top 1 fk_Producto FROM Temporal2) and Dispo_stock = 'True' 
						order by Ent_FechVencim asc )
						print @ID
					end
					--- se sale del while 
					if(@cant_Salida = @cant_Stock)
					begin
						if exists (select * from Detalle_Entrada WHERE ID_Det_entr = @ID and fk_Entrada = 0)--para no dejar el registro 0 temporal
						begin 
							delete from Detalle_Entrada where ID_Det_entr = @ID and fk_Entrada = 0
						end
						else
						begin
							UPDATE Detalle_Entrada
							SET Dispo_stock='False' 
							WHERE ID_Det_entr = @ID
						end
					end
					else
					if(@cant_Salida < @cant_Stock)
					begin 
						--funciona si es menor creo un registro ID= 0 donde tiene la cantidad que quedo restando
					INSERT INTO Detalle_Entrada( ID_Det_entr,fk_Entrada,fk_TipoEntrada,fk_Proveedor,fk_Donante,fk_Producto, 
							Ent_Cantidad_Peso,Ent_Factura,Ent_Precio_Unitario,Ent_Precio_Total,Ent_iva, Ent_Descuento,Ent_Temperatura, Ent_FechVencim,Dispo_stock)
					SELECT ((select max(t.ID_Det_entr)from Detalle_Entrada t) + 1),0,fk_TipoEntrada,fk_Proveedor,fk_Donante,fk_Producto,
							@cant_Salida,Ent_Factura,Ent_Precio_Unitario,Ent_Precio_Total,Ent_iva, Ent_Descuento, Ent_Temperatura, Ent_FechVencim,Dispo_stock FROM Detalle_Entrada
							where ID_Det_entr = @ID
						--Funciona si es menor actualiza la variable a false en detalle de entrada ya que necesitamos de otro registro
						UPDATE Detalle_Entrada
						SET Dispo_stock='False' 
						WHERE ID_Det_entr = @ID
					end
					--al terminar el proceso 
					--Funciona registra en detale de salida del registro que sacamos
					INSERT INTO Detalle_Salida( ID_Det_salid,fk_Salida,fk_TipoSalida,fk_Producto,-- se guarda la salida con la informacion 
							Sal_Cantidad_Peso,Sal_Observacion)
					SELECT ID_Det_salid,fk_Salida,fk_TipoSalida,fk_Producto,
								Sal_Cantidad_Peso,Sal_Observacion FROM Temporal2
						where ID_Det_salid = (SELECT Top 1  ID_Det_salid FROM Temporal2)
					--funciona elimina del temporal de salida el dato que acabamos de guardar en detalle de salida
					delete from Temporal2 where ID_Det_salid = (SELECT Top 1 t.ID_Det_salid FROM Temporal2 t)
					end
				end
			END
		SET @intFlag = @intFlag + 1-- sacamos otro registro del temporal de salida y se hace el mismo chequeo 
	End
ENd
GO
/****** Object:  StoredProcedure [dbo].[spBuscar_ProxVencer]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spBuscar_ProxVencer]
@categoria  nvarchar(50) ,
@subcategoria  nvarchar(50) ,
@producto  nvarchar(50) 
as 
begin
			declare @Fecha date = {fn curdate()} 
			declare @Fech_Min date = DATEADD(DAY,3,@Fecha);

select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,x.Ent_Precio_Unitario,x.Ent_Precio_Total,x.Ent_Cantidad_Peso , x.Ent_FechVencim
	into #temp
	from Detalle_Entrada as x
	inner join Entrada as a on a.ID_Entrada = x.fk_Entrada
	inner join Empleado as e on e.Emp_Documento = a.fk_Empleado
	inner join Producto as pro on pro.ID_Producto = x.fk_Producto
	inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
	inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
	inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
	where x.Dispo_stock = 'True' and
		x.Ent_FechVencim >= @Fecha  and 
		x.Ent_FechVencim <= @Fech_Min
	order by Cat_Nombre desc

	if(@categoria = '0' and @subcategoria ='0' and @producto='0') 
	begin 
		select * from #temp --si no ingresa algun filtro
	end
else 
	begin
		if(@categoria != '0' )
			begin
					select * from #temp
					where Cat_Nombre like '%'+@categoria+'%' -- si soloingresa categoria
					order by Cat_Nombre
			end
		else
			begin 
				if(@subcategoria != '0' )
					begin
							select * from #temp
							where Subca_Nombre  like '%'+@subcategoria+'%' -- si soloingresa subcategoria
							order by Cat_Nombre
					end
				else
					begin 
						if(@producto != '0' )
							begin
									select * from #temp
									where NomProducto like '%'+@producto+'%' -- si soloingresa subcategoria
									order by Cat_Nombre
							end
					end
			end
	end
IF OBJECT_ID('tempdb..#temp') IS not NULL
BEGIN
drop table #temp
END
					
END
GO
/****** Object:  StoredProcedure [dbo].[spBuscar_Vencidos]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spBuscar_Vencidos]
@categoria nvarchar(50) ,
@subcategoria nvarchar(50) ,
@producto nvarchar(50) 
as 
begin
	select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,x.Ent_Precio_Unitario,x.Ent_Precio_Total,x.Ent_Cantidad_Peso , x.Ent_FechVencim
	into #temp
	from Detalle_Entrada as x
	inner join Entrada as a on a.ID_Entrada = x.fk_Entrada
	inner join Empleado as e on e.Emp_Documento = a.fk_Empleado
	inner join Producto as pro on pro.ID_Producto = x.fk_Producto
	inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
	inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
	inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
	where x.Dispo_stock = 'True' and
		x.Ent_FechVencim <= {fn curdate()} 
	order by Cat_Nombre desc

if(@categoria = '0' and @subcategoria ='0' and @producto='0') 
	begin 
		select * from #temp --si no ingresa algun filtro
	end
else 
	begin
		if(@categoria != '0' )
			begin
					select * from #temp
					where Cat_Nombre like '%'+@categoria+'%' -- si soloingresa categoria
					order by Cat_Nombre
			end
		else
			begin 
				if(@subcategoria != '0' )
					begin
							select * from #temp
							where Subca_Nombre  like '%'+@subcategoria+'%' -- si soloingresa subcategoria
							order by Cat_Nombre
					end
				else
					begin 
						if(@producto != '0' )
							begin
									select * from #temp
									where NomProducto like '%'+@producto+'%' -- si soloingresa subcategoria
									order by Cat_Nombre
							end
					end
			end
	end
IF OBJECT_ID('tempdb..#temp') IS not NULL
BEGIN
drop table #temp
END
				
END
GO
/****** Object:  StoredProcedure [dbo].[spBuscarDetEmpleado_up]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spBuscarDetEmpleado_up]
@idEmpleado int,
@inicio date,
@fin date
AS
begin
	select concat (e.Emp_Nombre,' ',e.Emp_Apellido)as fullname,a.Ent_Fecha as fecha,c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,t.Tipo_Nombre,x.Ent_Cantidad_Peso ,'Entrada' as Tipo
	from Detalle_Entrada as x
	inner join Entrada as a on a.ID_Entrada = x.fk_Entrada
	inner join Empleado as e on e.Emp_Documento = a.fk_Empleado
	inner join Producto as pro on pro.ID_Producto = x.fk_Producto
	inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
	inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
	inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
	inner join Tipo_Entrada as t on t.ID_TipoEntrada = x.fk_TipoEntrada
	where e.Emp_Documento = @idEmpleado
	and  (a.Ent_Fecha >= @inicio) and (a.Ent_Fecha <= @fin)
	union all
	select concat (e.Emp_Nombre,' ',e.Emp_Apellido)as fullname,a.Sali_Fecha as fecha,c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,t.Tipo_Nombre,y.Sal_Cantidad_Peso , 'Salida' as Tipo
	from Detalle_Salida as y
	inner join Salida as a on a.ID_Salida = y.fk_Salida
	inner join Empleado as e on e.Emp_Documento = a.fk_Empleado
	inner join Producto as pro on pro.ID_Producto = y.fk_Producto
	inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
	inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
	inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
	inner join Tipo_Salida as t on t.ID_TipoSalida = y.fk_TipoSalida
	where e.Emp_Documento = @idEmpleado
	and  (a.Sali_Fecha >= @inicio) and (a.Sali_Fecha <= @fin)
	order by fecha desc
end
GO
/****** Object:  StoredProcedure [dbo].[spConsultar_Inventario]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spConsultar_Inventario]
@categoria nvarchar(50) ,
@subcategoria nvarchar(50) ,
@producto nvarchar(50) 
as 
begin
select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, 0 as Total ,0 as  promedio ,n.Min,n.Punto_pedido,0 as precio_unitario into #temp from Nom_Producto n
				inner join Subcategoria as s on s.ID_Subcategoria = n.Subcategoria
				inner join Categoria as c on c.ID_Categoria = s.Categoria
				union
					select  c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, sum (d.Ent_Cantidad_Peso) as Total  ,AVG(d.Ent_Precio_Total)as promedio,n.Min, n.Punto_pedido ,AVG(d.Ent_Precio_Unitario)as precio_unitario from Detalle_Entrada d -- se guarda la suma o os 0 de las entradas en temp
				inner join Producto as p on p.ID_Producto = d.fk_Producto
				inner join Nom_Producto as n on n.ID_NomProducto = p.fk_NomProducto
				inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
				where d.Dispo_stock = 'True'
				group by c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,c.ID_Categoria,s.ID_Subcategoria,n.ID_NomProducto,n.Min,n.Punto_pedido 
				

				select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, sum (n.Total) as Saldo ,round (sum(n.Promedio),2) as Ent_Precio_Total,n.Min,n.Punto_pedido,round (sum(n.precio_unitario),2) as Ent_Precio_Unitario  into #inventario from #temp n --para que no quede ninguno repetido
				group by n.Cat_Nombre,n.Subca_Nombre,n.NomProducto,n.Min,n.Punto_pedido

if(@categoria = '0' and @subcategoria ='0' and @producto='0') 
	begin 
		select * from #inventario --si no ingresa algun filtro
	end
else 
	begin
		if(@categoria != '0' )
			begin
					select * from #inventario
					where Cat_Nombre like '%'+@categoria+'%' -- si soloingresa categoria
					order by Cat_Nombre
			end
		else
			begin 
				if(@subcategoria != '0' )
					begin
							select * from #inventario
							where Subca_Nombre  like '%'+@subcategoria+'%' -- si soloingresa subcategoria
							order by Cat_Nombre
					end
				else
					begin 
						if(@producto != '0' )
							begin
									select * from #inventario
									where NomProducto like '%'+@producto+'%' -- si soloingresa subcategoria
									order by Cat_Nombre
							end
					end
			end
	end
IF OBJECT_ID('tempdb..#temp') IS not NULL
BEGIN
drop table #temp
END
IF OBJECT_ID('tempdb..#inventario') IS not NULL
BEGIN
drop table #inventario
END
END
GO
/****** Object:  StoredProcedure [dbo].[spCrearCategoria]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCrearCategoria]
@Nombre nvarchar(50)
AS
begin
if not exists (select c.Cat_Nombre from Categoria as c where c.Cat_Nombre = @Nombre) begin
declare @Id int
select @Id = (select max(c.ID_Categoria) from Categoria as c) + 1
		INSERT INTO Categoria (ID_Categoria, Cat_Nombre)
		VALUES (@Id,STUFF (@Nombre, 1, 1, UPPER(left(@Nombre, 1))));
end

end
GO
/****** Object:  StoredProcedure [dbo].[spCrearNom_Producto]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCrearNom_Producto]
@Id_Sub int ,
@Nombre varchar(50),
@Min decimal(18,2),
@Punto_pedido decimal(18,2),
@Prop_Adicion bit
AS
begin
if not exists (select n.NomProducto from Nom_Producto as n where n.NomProducto = @Nombre) begin

declare @IdNom int
select @IdNom = (select max(n.ID_NomProducto) from Nom_Producto as n) + 1
		INSERT INTO Nom_Producto(ID_NomProducto,Subcategoria, NomProducto,Min,Punto_pedido,Prop_Adicion)
		VALUES (@IdNom,@Id_Sub,STUFF (@Nombre, 1, 1, UPPER(left(@Nombre, 1))),@Min,@Punto_pedido,@Prop_Adicion);
end	
end
GO
/****** Object:  StoredProcedure [dbo].[spCrearProducto]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCrearProducto]
@idc int,-- ID de la categoria
@ids int,-- ID de la subcategoria
@idp int, -- ID de la producto
@Id int output
AS
begin
if  not exists (select c.ID_Categoria,s.ID_Subcategoria,n.ID_NomProducto from Producto as p
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
				inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Nom_Producto as n on  n.ID_NomProducto = p.fk_NomProducto
				where c.ID_Categoria = @idc
				and s.ID_Subcategoria = @ids
				and n.ID_NomProducto = @idp) 
	begin
		select @Id = (select max(c.ID_Producto) from Producto as c) + 1
				INSERT INTO Producto(ID_Producto, fk_Categoria,fk_Subcategoria,fk_NomProducto)
				VALUES (@Id, @idc,@ids,@idp);
				return @Id 
	end
else
	begin
		select @Id = (select p.ID_Producto from Producto p
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
				inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Nom_Producto as n on  n.ID_NomProducto = p.fk_NomProducto
				where c.ID_Categoria = @idc
				and s.ID_Subcategoria = @ids
				and n.ID_NomProducto = @idp)
	end
end
GO
/****** Object:  StoredProcedure [dbo].[spCrearSubcategoria]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCrearSubcategoria]
@Id_Cat int ,
@Nombre varchar(50)
AS
begin
if not exists (select s.Subca_Nombre from Subcategoria as s where s.Subca_Nombre = @Nombre) begin
declare @IdSub int
select @IdSub = (select max(s.ID_Subcategoria) from Subcategoria as s) + 1
		INSERT INTO Subcategoria(ID_Subcategoria,Categoria, Subca_Nombre)
		VALUES (@IdSub,@Id_Cat,STUFF (@Nombre, 1, 1, UPPER(left(@Nombre, 1))));
end	
end
GO
/****** Object:  StoredProcedure [dbo].[spCrearTemporal]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCrearTemporal]
@empleado int,
@fk_TipoEntrada int,
@fk_Proveedor int,
@fk_Donante int,
@fk_Producto bigint,
@Ent_Precio_Total decimal(18,2),
@Ent_Factura nvarchar(50),
@Ent_Precio_Unitario decimal(18,2),
@Ent_iva decimal(18,2),
@Ent_Descuento decimal(18,2),
@Ent_Cantidad_Peso decimal(18,2),
@Ent_Temperatura decimal(18,2),
@Ent_FechVencim date,
@ID_Categoria int,
@ID_Subcategoria int,
@ID_NomProducto int
AS
begin
		if(@Ent_FechVencim = '9999-12-30')
		begin
			select @Ent_FechVencim = null --para que no guarde ninguna fecha
		end
		declare @Id int
		declare @fk_Entrada bigint

		select @Id
					if((select max(t.ID_Det_entr)from Temporal t) is not null)
						begin
							select @Id = ((select max(t.ID_Det_entr)from Temporal t) + 1)-- para crear un pk unica	
						end
					else
						begin 
						if((select max(t.ID_Det_entr)from Detalle_Entrada t) is not null)
							begin 
							select @Id =((select max(c.ID_Det_entr) from Detalle_Entrada as c) + 1)  --si el temporal no tiene ningun registro creee el primero
							end
						else
							begin 
							 select @Id =1
							end
						
						end
		select @fk_Entrada = (select max(e.ID_Entrada) from Entrada  e 
								inner join Empleado  emp on emp.Emp_Documento = e.fk_Empleado
								where @empleado = e.fk_Empleado) --- busca la ultima entrada de este usuario


		select @fk_Entrada 
				INSERT INTO Temporal(ID_Det_entr, fk_Entrada,fk_TipoEntrada,fk_Proveedor,fk_Donante,fk_Producto,
				Ent_Precio_Total,Ent_Factura,Ent_Precio_Unitario,Ent_iva,Ent_Descuento,Ent_Cantidad_Peso,Ent_Temperatura,Ent_FechVencim,
				ID_Categoria,ID_Subcategoria,ID_NomProducto)
				VALUES (@Id, @fk_Entrada,@fk_TipoEntrada,@fk_Proveedor,@fk_Donante,@fk_Producto,
				@Ent_Precio_Total,@Ent_Factura,@Ent_Precio_Unitario,@Ent_iva,@Ent_Descuento,@Ent_Cantidad_Peso,@Ent_Temperatura,@Ent_FechVencim,
				@ID_Categoria,@ID_Subcategoria,@ID_NomProducto);
end
GO
/****** Object:  StoredProcedure [dbo].[spCrearTemporal2]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCrearTemporal2]
@empleado int,
@fk_TipoSalida int,
@fk_Producto bigint,
@Sal_Cantidad_Peso decimal(18,2),
@Sal_Observacion nvarchar(500),
@ID_Categoria int,
@ID_Subcategoria int,
@ID_NomProducto int
AS
begin
		declare @Id int
		
		declare @fk_Salida bigint

		select @Id
					if((select max(t.ID_Det_salid)from Temporal2 t) is not null)
						begin
							select @Id = ((select max(t.ID_Det_salid)from Temporal2 t) + 1)-- para crear un pk unica	
						end
					else
						begin 
						if((select max(t.ID_Det_salid)from Detalle_Salida t) is not null)
							begin 
							select @Id =((select max(c.ID_Det_salid) from Detalle_Salida as c) + 1)  
							end
						else
							begin 
							 select @Id =1
							end
						end
			

		select @fk_Salida = (select max(e.ID_Salida) from Salida  e 
								inner join Empleado  emp on emp.Emp_Documento = e.fk_Empleado
								where @empleado = e.fk_Empleado) --- busca la ultima entrada de este usuario

				INSERT INTO Temporal2(ID_Det_salid, fk_Salida,fk_TipoSalida,fk_Producto,
				Sal_Cantidad_Peso,Sal_Observacion,ID_Categoria,ID_Subcategoria,ID_NomProducto)
				VALUES (@Id, @fk_Salida,@fk_TipoSalida,@fk_Producto,@Sal_Cantidad_Peso,@Sal_Observacion,
				@ID_Categoria,@ID_Subcategoria,@ID_NomProducto);
end
GO
/****** Object:  StoredProcedure [dbo].[spEditarDetEntrada]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarDetEntrada]
@Id bigint,
@fk_Entrada bigint,
@fk_TipoEntrada int,
@fk_Proveedor int,
@fk_Donante int,
@fk_Producto bigint,
@Ent_Precio_Total decimal(18,2),
@Ent_Factura nvarchar(50),
@Ent_Precio_Unitario decimal(18,2),
@Ent_iva decimal(18,2),
@Ent_Descuento decimal(18,2),
@Ent_Cantidad_Peso decimal(18,2),
@Ent_Temperatura decimal(18,2),
@Ent_FechVencim date
AS
BEGIN
		if(@Ent_FechVencim = '9999-12-30')
		begin
			select @Ent_FechVencim = null --para que no guarde ninguna fecha
		end

	UPDATE Detalle_Entrada SET fk_Entrada= @fk_Entrada,fk_TipoEntrada = @fk_TipoEntrada,fk_Proveedor =@fk_Proveedor,
	fk_Donante =@fk_Donante,fk_Producto=@fk_Producto,Ent_Precio_Total = @Ent_Precio_Total,Ent_Factura =@Ent_Factura ,@Ent_Precio_Unitario =@Ent_Precio_Unitario ,
	Ent_iva =@Ent_iva,Ent_Descuento =@Ent_Descuento ,Ent_Cantidad_Peso =@Ent_Cantidad_Peso,Ent_Temperatura = @Ent_Temperatura ,Ent_FechVencim = @Ent_FechVencim
           WHERE ID_Det_entr = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spEditarDetSalida]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarDetSalida]
@ID bigint,
@fk_Salida bigint,
@fk_TipoSalida int,
@fk_Producto bigint,
@Sal_Cantidad_Peso decimal(18,2),
@Sal_Observacion nvarchar(500)
AS
begin
UPDATE Detalle_Salida SET  fk_Salida = @fk_Salida, fk_TipoSalida = @fk_TipoSalida,
	fk_Producto=@fk_Producto  ,Sal_Cantidad_Peso =@Sal_Cantidad_Peso, Sal_Observacion =@Sal_Observacion
           WHERE ID_Det_salid = @ID;
end
GO
/****** Object:  StoredProcedure [dbo].[spEditarTemporal]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarTemporal]
@Id bigint,
@fk_Entrada bigint,
@fk_TipoEntrada int,
@fk_Proveedor int,
@fk_Donante int,
@fk_Producto bigint,
@Ent_Precio_Total decimal(18, 2),
@Ent_Factura nvarchar,
@Ent_Precio_Unitario decimal(18, 2),
@Ent_iva decimal(18, 2),
@Ent_Descuento decimal(18, 2),
@Ent_Cantidad_Peso decimal(18, 2),
@Ent_Temperatura decimal(18, 2),
@Ent_FechVencim date,
@ID_Categoria int,
@ID_Subcategoria int,
@ID_NomProducto int
AS
BEGIN
		if(@Ent_FechVencim = '9999-12-30')
		begin
			select @Ent_FechVencim = null --para que no guarde ninguna fecha
		end

	UPDATE Temporal SET fk_Entrada= @fk_Entrada,fk_TipoEntrada = @fk_TipoEntrada,fk_Proveedor =@fk_Proveedor,
	fk_Donante =@fk_Donante,fk_Producto=@fk_Producto ,Ent_Precio_Total =@Ent_Precio_Total,Ent_Factura =@Ent_Factura,
	Ent_Precio_Unitario =@Ent_Precio_Unitario ,Ent_iva =@Ent_iva,Ent_Descuento =@Ent_Descuento ,Ent_Cantidad_Peso =@Ent_Cantidad_Peso ,
    Ent_Temperatura = @Ent_Temperatura ,Ent_FechVencim = @Ent_FechVencim,ID_Categoria = @ID_Categoria,
	ID_Subcategoria =@ID_Subcategoria,ID_NomProducto = @ID_NomProducto
           WHERE ID_Det_entr = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spEditarTemporal2]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarTemporal2]
@ID bigint,
@fk_Salida bigint,
@fk_TipoSalida int,
@fk_Producto bigint,
@Sal_Cantidad decimal(18,2),
@Sal_Observacion nvarchar(500),
@ID_Categoria int,
@ID_Subcategoria int,
@ID_NomProducto int
AS
begin
UPDATE Temporal2 SET ID_Det_salid = @ID, fk_Salida = @fk_Salida, fk_TipoSalida = @fk_TipoSalida,
	fk_Producto=@fk_Producto,Sal_Cantidad_Peso =@Sal_Cantidad, Sal_Observacion =@Sal_Observacion,ID_Categoria = @ID_Categoria,
	ID_Subcategoria =@ID_Subcategoria,ID_NomProducto = @ID_NomProducto
           WHERE ID_Det_salid = @ID;
end
GO
/****** Object:  StoredProcedure [dbo].[spGuardarExcel]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGuardarExcel]
as 
begin
select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, 0 as Total ,0 as  promedio ,n.Min,n.Punto_pedido,0 as precio_unitario into #temp from Nom_Producto n
				inner join Subcategoria as s on s.ID_Subcategoria = n.Subcategoria
				inner join Categoria as c on c.ID_Categoria = s.Categoria
				union
					select  c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, sum (d.Ent_Cantidad_Peso) as Total  ,AVG(d.Ent_Precio_Total)as promedio,n.Min, n.Punto_pedido ,AVG(d.Ent_Precio_Unitario)as precio_unitario from Detalle_Entrada d -- se guarda la suma o os 0 de las entradas en temp
				inner join Producto as p on p.ID_Producto = d.fk_Producto
				inner join Nom_Producto as n on n.ID_NomProducto = p.fk_NomProducto
				inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
				where d.Dispo_stock = 'True'
				group by c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,c.ID_Categoria,s.ID_Subcategoria,n.ID_NomProducto,n.Min,n.Punto_pedido 
				

				select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, sum (n.Total) as Saldo ,round (sum(n.Promedio),2) as Ent_Precio_Total,n.Min,n.Punto_pedido,round (sum(n.precio_unitario),2) as Ent_Precio_Unitario  into #inventario from #temp n --para que no quede ninguno repetido
				group by n.Cat_Nombre,n.Subca_Nombre,n.NomProducto,n.Min,n.Punto_pedido
				select * from #inventario
end
GO
/****** Object:  StoredProcedure [dbo].[spGuardarExcel_Det_Ent]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGuardarExcel_Det_Ent]
as 
begin

		Select q.Ent_Fecha,u.Emp_Nombre+' '+u.Emp_Apellido as Emp_Nombre ,c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,e.Ent_Cantidad_Peso,e.Ent_Temperatura,
		e.Ent_FechVencim,t.Tipo_Nombre,p.Prov_Nombre,d.Don_Nombre,e.Ent_Factura,e.Ent_Precio_Total,e.Ent_Precio_Unitario,e.Ent_iva,e.Ent_Descuento
		  FROM Detalle_Entrada as e 
		  inner join Tipo_Entrada as t on t.ID_TipoEntrada = e.fk_TipoEntrada
		  inner join Proveedor as p on p.Prov_Docum = e.fk_Proveedor
		  inner join Producto as pro on pro.ID_Producto = e.fk_Producto
		  inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
		  inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
		  inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
		  inner join Donante as d on d.ID_Donante = e.fk_Donante
		  inner join Entrada as q on q.ID_Entrada = e.fk_Entrada
		  inner join Empleado as u on u.Emp_Documento = q.fk_Empleado
		where fk_Entrada != 0 and
			  (year(q.Ent_Fecha) = datepart(YYYY, getdate())) and 
			  (month(q.Ent_Fecha) = datepart(mm, getdate()))
		order by Ent_Fecha desc
end
GO
/****** Object:  StoredProcedure [dbo].[spGuardarExcel_Det_Sal]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGuardarExcel_Det_Sal]
as 
begin
Select q.Sali_Fecha,u.Emp_Nombre+' '+u.Emp_Apellido as Emp_Nombre,c.Cat_Nombre,s.Subca_Nombre,
		n.NomProducto,e.Sal_Cantidad_Peso,t.Tipo_Nombre,e.Sal_Observacion
		  FROM Detalle_Salida as e 
		  inner join Tipo_Salida as t on t.ID_TipoSalida = e.fk_TipoSalida
		  inner join Producto as pro on pro.ID_Producto = e.fk_Producto
		  inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
		  inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
		  inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
		  inner join Salida as q on q.ID_Salida = e.fk_Salida
		  inner join Empleado as u on u.Emp_Documento = q.fk_Empleado
		where (year(q.Sali_Fecha) = datepart(YYYY, getdate())) and 
			  (month(q.Sali_Fecha) = datepart(mm, getdate()))
		order by q.Sali_Fecha desc
end
GO
/****** Object:  StoredProcedure [dbo].[spGuardarExcel_Inventario]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGuardarExcel_Inventario]
as 
begin
--------------------sacar la suma de todas las entradas de mes actual--------------------------------------------------------------
select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, 0 as can_ent into #tabla1 from Nom_Producto n
inner join Subcategoria as s on s.ID_Subcategoria = n.Subcategoria
inner join Categoria as c on c.ID_Categoria = s.Categoria
	union
select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,sum (d.Ent_Cantidad_Peso) as can_ent from Detalle_Entrada d
inner join Producto as p on p.ID_Producto = d.fk_Producto
inner join Nom_Producto as n on n.ID_NomProducto = p.fk_NomProducto
inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
inner join Entrada as e on e.ID_Entrada = d.fk_Entrada
where (year(e.Ent_Fecha) = datepart(YYYY, getdate())) and 
	(month(e.Ent_Fecha) = datepart(mm, getdate()))
group by c.Cat_Nombre,s.Subca_Nombre,n.NomProducto
select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, sum (n.can_ent) as can_ent  into #tabla1_ from #tabla1 n --para que no quede ninguno repetido
group by n.Cat_Nombre,n.Subca_Nombre,n.NomProducto

----------------------sacar la suma de todas las salidas de mes actual----------------------------------------
select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, 0 as can_sali into #tabla2 from Nom_Producto n
inner join Subcategoria as s on s.ID_Subcategoria = n.Subcategoria
inner join Categoria as c on c.ID_Categoria = s.Categoria
	union
select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,sum (d.Sal_Cantidad_Peso) as can_sali from Detalle_Salida d
inner join Producto as p on p.ID_Producto = d.fk_Producto
inner join Nom_Producto as n on n.ID_NomProducto = p.fk_NomProducto
inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
inner join Salida as e on e.ID_Salida = d.fk_Salida
where (year(e.Sali_Fecha) = datepart(YYYY, getdate())) and 
	(month(e.Sali_Fecha) = datepart(mm, getdate()))
group by c.Cat_Nombre,s.Subca_Nombre,n.NomProducto
select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, sum (n.can_sali) as can_sali  into #tabla2_ from #tabla2 n --para que no quede ninguno repetido
group by n.Cat_Nombre,n.Subca_Nombre,n.NomProducto

-----------------------------Saldo actual del inventario-------------------------------------------------
select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, 0 as saldo,0 as valor_total into #tabla3 from Nom_Producto n
				inner join Subcategoria as s on s.ID_Subcategoria = n.Subcategoria
				inner join Categoria as c on c.ID_Categoria = s.Categoria
				union
					select  c.Cat_Nombre,s.Subca_Nombre,n.NomProducto, sum (d.Ent_Cantidad_Peso) as saldo  ,sum (d.Ent_Precio_Total)as valor_total from Detalle_Entrada d -- se guarda la suma o os 0 de las entradas en temp
				inner join Producto as p on p.ID_Producto = d.fk_Producto
				inner join Nom_Producto as n on n.ID_NomProducto = p.fk_NomProducto
				inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
				where d.Dispo_stock = 'True'
				group by c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,c.ID_Categoria,s.ID_Subcategoria,n.ID_NomProducto,n.Min,n.Punto_pedido
				select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, sum (n.saldo) as Saldo ,sum(n.valor_total) as valor_total into #tabla3_ from #tabla3 n --para que no quede ninguno repetido
				group by n.Cat_Nombre,n.Subca_Nombre,n.NomProducto
-----------------------------------------------------
select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, 0 as can_ent ,0 as  can_sali ,n.Saldo,n.valor_total into #Inventario from #tabla3_ n
				union
select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, 0 as can_ent  ,n.can_sali ,0 as Saldo,0 as  valor_total  from #tabla2_ n
				union
select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto, n.can_ent, 0 as  can_sali ,0 as Saldo,0 as  valor_total  from #tabla1_ n

select n.Cat_Nombre,n.Subca_Nombre,n.NomProducto,sum(can_ent) as can_ent,sum(can_sali) as can_sali ,sum(Saldo) as Saldo ,sum(valor_total) as valor_total into #Inventario_ from #Inventario n 
group by Cat_Nombre,Subca_Nombre,NomProducto
select * from #Inventario_

IF OBJECT_ID('tempdb..#tabla1') IS not NULL
BEGIN
drop table #tabla1
END	
IF OBJECT_ID('tempdb..#tabla1_') IS not NULL
BEGIN
drop table #tabla1_
END	
IF OBJECT_ID('tempdb..#tabla2') IS not NULL
BEGIN
drop table #tabla2
END	
IF OBJECT_ID('tempdb..#tabla2_') IS not NULL
BEGIN
drop table #tabla2_
END
IF OBJECT_ID('tempdb..#tabla3') IS not NULL
BEGIN
drop table #tabla3
END	
IF OBJECT_ID('tempdb..#tabla3_') IS not NULL
BEGIN
drop table #tabla3_
END
IF OBJECT_ID('tempdb..#Inventario') IS not NULL
BEGIN
drop table #Inventario
END	
IF OBJECT_ID('tempdb..#Inventario_') IS not NULL
BEGIN
drop table #Inventario_
END	
end
GO
/****** Object:  StoredProcedure [dbo].[spGuardarExcel_Vencidos]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[spGuardarExcel_Vencidos]

as 
begin
	select c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,x.Ent_Precio_Unitario,x.Ent_Precio_Total,x.Ent_Cantidad_Peso , x.Ent_FechVencim
	into #temp
	from Detalle_Entrada as x
	inner join Entrada as a on a.ID_Entrada = x.fk_Entrada
	inner join Empleado as e on e.Emp_Documento = a.fk_Empleado
	inner join Producto as pro on pro.ID_Producto = x.fk_Producto
	inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
	inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
	inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
	where x.Dispo_stock = 'True' and
		x.Ent_FechVencim <= {fn curdate()} 
	order by Cat_Nombre desc
 
		select * from #temp 
end
GO
/****** Object:  StoredProcedure [dbo].[spLlenarEntrada]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spLlenarEntrada]
@Usuario nvarchar(50)

AS BEGIN
declare @ID int;
declare @EMP int;
declare @fec datetime;

Select @ID = (select count(*) from Entrada) + 1;
select @EMP =(select Empleado.Emp_Documento from Empleado 
				inner join Usuario on Empleado.Emp_Documento = Usuario.ID_Empleado
				where @Usuario = Usuario.Usu_Nombre);
select @fec = GETDATE();

insert into Entrada (ID_Entrada,fk_Empleado,Ent_Fecha,Ent_Total)
values (@ID, @EMP,@fec,null);
 END
GO
/****** Object:  StoredProcedure [dbo].[spLlenarSalida]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spLlenarSalida]
@Usuario nvarchar(50)
AS BEGIN
declare @ID int;
declare @EMP int;
declare @fec datetime;

Select @ID = (select count(*) from Salida) + 1;
select @EMP =(select Empleado.Emp_Documento from Empleado 
				inner join Usuario on Empleado.Emp_Documento = Usuario.ID_Empleado
				where @Usuario = Usuario.Usu_Nombre);
select @fec = GETDATE();

insert into Salida(ID_Salida,fk_Empleado,Sali_Fecha)
values (@ID, @EMP,@fec);
 END
GO
/****** Object:  StoredProcedure [dbo].[spMostrarDeta_Entradas]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[spMostrarDeta_Entradas]
@inicio date,
@fin date
AS
	begin
		Select e.ID_Det_entr,q.Ent_Fecha,u.Emp_Nombre+' '+u.Emp_Apellido as Emp_Nombre ,c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,e.Ent_Cantidad_Peso,e.Ent_Temperatura,
		e.Ent_FechVencim,t.Tipo_Nombre,p.Prov_Nombre,d.Don_Nombre,e.Ent_Factura,e.Ent_Precio_Total,e.Ent_Precio_Unitario,e.Ent_iva,e.Ent_Descuento,e.Dispo_stock
		  FROM Detalle_Entrada as e 
		  inner join Tipo_Entrada as t on t.ID_TipoEntrada = e.fk_TipoEntrada
		  inner join Proveedor as p on p.Prov_Docum = e.fk_Proveedor
		  inner join Producto as pro on pro.ID_Producto = e.fk_Producto
		  inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
		  inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
		  inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
		  inner join Donante as d on d.ID_Donante = e.fk_Donante
		  inner join Entrada as q on q.ID_Entrada = e.fk_Entrada
		  inner join Empleado as u on u.Emp_Documento = q.fk_Empleado
		where (q.Ent_Fecha >= @inicio) and (q.Ent_Fecha <= @fin) and
				fk_Entrada != 0
		order by q.Ent_Fecha desc
	end
GO
/****** Object:  StoredProcedure [dbo].[spMostrarDeta_Salidas]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spMostrarDeta_Salidas]
@inicio date,
@fin date
AS
	begin
		Select e.ID_Det_salid,q.Sali_Fecha,u.Emp_Nombre+' '+u.Emp_Apellido as Emp_Nombre,c.Cat_Nombre,s.Subca_Nombre,
		n.NomProducto,e.Sal_Cantidad_Peso,t.Tipo_Nombre,e.Sal_Observacion
		  FROM Detalle_Salida as e 
		  inner join Tipo_Salida as t on t.ID_TipoSalida = e.fk_TipoSalida
		  inner join Producto as pro on pro.ID_Producto = e.fk_Producto
		  inner join Categoria as c on c.ID_Categoria = pro.fk_Categoria
		  inner join Subcategoria as s on s.ID_Subcategoria = pro.fk_Subcategoria
		  inner join Nom_Producto as n on n.ID_NomProducto = pro.fk_NomProducto
		  inner join Salida as q on q.ID_Salida = e.fk_Salida
		  inner join Empleado as u on u.Emp_Documento = q.fk_Empleado
		where (q.Sali_Fecha >= @inicio) and (q.Sali_Fecha <= @fin)
		order by q.Sali_Fecha desc
	end
GO
/****** Object:  StoredProcedure [dbo].[spValidaUsuario]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spValidaUsuario]
@usuario NVARCHAR(50),
@permiso int
AS BEGIN
	if exists (SELECT u.Usu_Nombre from Usuario u 
inner join Empleado e on e.Emp_Documento = u.ID_Empleado
inner join Cargo c on c.ID_Cargo = e.fk_Cargo
 WHERE Usu_Nombre =@usuario AND c.ID_Cargo = @permiso)
	begin 
	SELECT Usu_Nombre from Usuario WHERE Usu_Nombre =@usuario
	end
	else
	begin
		if exists (SELECT u.Usu_Nombre from Usuario u 
		inner join Empleado e on e.Emp_Documento = u.ID_Empleado
		inner join Cargo c on c.ID_Cargo = e.fk_Cargo
		 WHERE Usu_Nombre =@usuario AND c.ID_Cargo = 1)
		begin 
		 SELECT Usu_Nombre from Usuario WHERE Usu_Nombre =@usuario
		end
		else
		begin
			if exists (SELECT u.Usu_Nombre from Usuario u 
			inner join Empleado e on e.Emp_Documento = u.ID_Empleado
			inner join Cargo c on c.ID_Cargo = e.fk_Cargo
			 WHERE Usu_Nombre =@usuario AND @permiso = 3 AND c.ID_Cargo = 2)
			begin 
			 SELECT Usu_Nombre from Usuario WHERE Usu_Nombre =@usuario
			end
		end
	end
 END
GO
/****** Object:  StoredProcedure [dbo].[spVerificarCantidad]    Script Date: 31/08/2022 1:47:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spVerificarCantidad]
@categoria int ,
@subcategoria int ,
@producto int,
@Sal_Cantidad_Peso decimal(18,2),
@cant float output
as 
begin

			select  c.ID_Categoria,c.Cat_Nombre,s.ID_Subcategoria,s.Subca_Nombre,n.ID_NomProducto,n.NomProducto, sum (d.Ent_Cantidad_Peso) as Total  into #temp  from Detalle_Entrada d -- se guarda la suma o os 0 de las entradas en temp
				inner join Producto as p on p.ID_Producto = d.fk_Producto
				inner join Nom_Producto as n on n.ID_NomProducto = p.fk_NomProducto
				inner join Subcategoria as s on s.ID_Subcategoria = p.fk_Subcategoria
				inner join Categoria as c on c.ID_Categoria = p.fk_Categoria
				where d.Dispo_stock = 'True' and
						@categoria = ID_Categoria and
						@subcategoria = ID_Subcategoria and
						@producto = ID_NomProducto
						group by c.Cat_Nombre,s.Subca_Nombre,n.NomProducto,c.ID_Categoria,s.ID_Subcategoria,n.ID_NomProducto
			
				if exists(select * from #temp) --si existe un producto con las especificaciones
					begin 
						if exists (select Total from #temp where @Sal_Cantidad_Peso <= Total)
							begin
								select Total from #temp where @Sal_Cantidad_Peso <= Total
								select @cant = 1 -- existe el producto y tiene suficientes
								return @cant
							end
						else
							begin
								select @cant = 0 --existe pero no hay suficiente
								return @cant
							end

					end
				else
					begin 
								select @cant = 0 -- retorna 0 porque no exixtse ningun producto
								return @cant
					end

	IF OBJECT_ID('tempdb..#temp') IS not NULL
BEGIN
drop table #temp
END
end
GO
USE [master]
GO
ALTER DATABASE [BD_Inventario] SET  READ_WRITE 
GO
