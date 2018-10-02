
--创建数据库
CREATE DATABASE QuarkDoc
GO

--访问数据库
USE QuarkDoc
GO


------------------------User-------------------------------
IF  not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[User]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [nvarchar](1024) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[IsEnabled] [bit] NULL,
	[IsAdmin] [bit] NULL
)
GO
SET ANSI_PADDING OFF
GO

DECLARE @Id varchar(50)
SELECT @Id =  NEWID()
select @Id=replace(cast(@Id as nvarchar(50)),'-','')
SET @Id=Lower(@Id)
INSERT INTO [dbo].[User]
           ([Id],[Password],[UserName],[Email],[CreateTime],[IsEnabled],[IsAdmin])
VALUES
			(@Id,'oxdarsx6j+qaxiozikd2yq==','jonins','jonins@admin.com',GETDATE(),1,1)
GO

------------------------Application-------------------------------
IF  not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Application]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[Application](
	[Id] [nvarchar](50) NOT NULL,
	[ProjectName] [nvarchar](50) NULL,
	[Description] [nvarchar](210) NULL,
	[UserId] [nvarchar](50) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[IsEnabled] [bit] NULL
)
GO
SET ANSI_PADDING OFF
GO

------------------------Directories-------------------------------
IF  not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Directories]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[Directories](
	[Id] [nvarchar](50) NOT NULL,
	[ApplicationId] [nvarchar](50) NOT NULL,
	[DirectoryId] [nvarchar](50) NOT NULL,
	[DirectoryName] [nvarchar](50) NULL,
	[Sort] INT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[IsEnabled] [bit] NULL
)
GO
SET ANSI_PADDING OFF
GO


------------------------Documents-------------------------------
IF  not EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Documents]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[Documents](
	[Id] [nvarchar](50) NOT NULL,
	[ApplicationId] [nvarchar](50) NOT NULL,
	[DirectoryId] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Document] [text] NULL,
	[Sort] INT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[IsEnabled] [bit] NULL
)
GO
SET ANSI_PADDING OFF
GO














