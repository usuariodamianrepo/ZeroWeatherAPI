USE [master]
GO
/****** Object:  Database [WebApi]    Script Date: 16/8/2024 09:15:48 ******/
CREATE DATABASE [WebApi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebApi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\WebApi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WebApi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\WebApi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [WebApi] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebApi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebApi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebApi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebApi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebApi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebApi] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebApi] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [WebApi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebApi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebApi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebApi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebApi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebApi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebApi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebApi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebApi] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WebApi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebApi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebApi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebApi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebApi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebApi] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [WebApi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebApi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebApi] SET  MULTI_USER 
GO
ALTER DATABASE [WebApi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebApi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebApi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebApi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WebApi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WebApi] SET QUERY_STORE = OFF
GO
USE [WebApi]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [WebApi]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 16/8/2024 09:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](0) NOT NULL,
	[UpdateDate] [datetime2](0) NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Latitude] [numeric](9, 6) NOT NULL,
	[Longitude] [numeric](9, 6) NOT NULL,
	[Country] [nvarchar](60) NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Weathers]    Script Date: 16/8/2024 09:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Weathers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime2](0) NOT NULL,
	[UpdateDate] [datetime2](0) NULL,
	[CityId] [int] NOT NULL,
	[CoordLon] [float] NOT NULL,
	[CoordLat] [float] NOT NULL,
	[WeatherId] [int] NOT NULL,
	[WeatherMain] [nvarchar](max) NOT NULL,
	[WeatherDescription] [nvarchar](max) NOT NULL,
	[WeatherIcon] [nvarchar](max) NOT NULL,
	[Base] [nvarchar](max) NOT NULL,
	[MainTemp] [float] NOT NULL,
	[MainFeelsLike] [float] NOT NULL,
	[MainTempMin] [float] NOT NULL,
	[MainTempMax] [float] NOT NULL,
	[MainPressure] [int] NOT NULL,
	[MainHumidity] [int] NOT NULL,
	[MainSeaLevel] [int] NOT NULL,
	[MainGrndLevel] [int] NOT NULL,
	[Visibility] [int] NOT NULL,
	[WindSpeed] [float] NOT NULL,
	[WindDeg] [int] NOT NULL,
	[WindGust] [float] NOT NULL,
	[CloudsAll] [int] NOT NULL,
	[Dt] [int] NOT NULL,
	[SysType] [int] NOT NULL,
	[SysId] [int] NOT NULL,
	[SysCountry] [nvarchar](max) NOT NULL,
	[SysSunrise] [int] NOT NULL,
	[SysSunset] [int] NOT NULL,
	[Timezone] [int] NOT NULL,
	[OpenWeatherId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Cod] [int] NOT NULL,
 CONSTRAINT [PK_Weathers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Weathers]  WITH CHECK ADD  CONSTRAINT [FK_Weathers_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[Weathers] CHECK CONSTRAINT [FK_Weathers_Cities]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWeathersByCity]    Script Date: 16/8/2024 09:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetWeathersByCity]
	-- Add the parameters for the stored procedure here
	@CityId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
		  ,[Name]
		  ,[SysCountry] as [Description]
	  FROM [dbo].[Weathers] WHERE [CityId] = @CityId

END
GO
USE [master]
GO
ALTER DATABASE [WebApi] SET  READ_WRITE 
GO
