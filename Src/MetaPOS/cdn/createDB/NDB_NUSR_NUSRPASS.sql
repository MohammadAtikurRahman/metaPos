CREATE DATABASE [$(NDB)]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'$(NDB)', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\$(NDB).mdf' , SIZE = 8192KB , MAXSIZE = 51200KB , FILEGROWTH = 4096KB )
 LOG ON 
( NAME = N'$(NDB)_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\$(NDB)_log.ldf' , SIZE = 8192KB , MAXSIZE = 10240KB , FILEGROWTH = 1024KB )
GO
ALTER DATABASE [$(NDB)] SET COMPATIBILITY_LEVEL = 140
GO
ALTER DATABASE [$(NDB)] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [$(NDB)] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [$(NDB)] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [$(NDB)] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [$(NDB)] SET ARITHABORT OFF 
GO
ALTER DATABASE [$(NDB)] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [$(NDB)] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [$(NDB)] SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF)
GO
ALTER DATABASE [$(NDB)] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [$(NDB)] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [$(NDB)] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [$(NDB)] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [$(NDB)] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [$(NDB)] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [$(NDB)] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [$(NDB)] SET  DISABLE_BROKER 
GO
ALTER DATABASE [$(NDB)] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [$(NDB)] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [$(NDB)] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [$(NDB)] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [$(NDB)] SET  READ_WRITE 
GO
ALTER DATABASE [$(NDB)] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [$(NDB)] SET  MULTI_USER 
GO
ALTER DATABASE [$(NDB)] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [$(NDB)] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [$(NDB)] SET DELAYED_DURABILITY = DISABLED 
GO
USE [$(NDB)]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = Off;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = Primary;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = On;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = Primary;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = Off;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = Primary;
GO
USE [$(NDB)]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [$(NDB)] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO




USE [master]
GO
CREATE LOGIN [$(NUSR)] WITH PASSWORD=N'$(NUSRPASS)', DEFAULT_DATABASE=[$(NDB)], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [master]
RESTORE DATABASE [$(NDB)] FROM  DISK = N'F:\Scripts\Auto_database_user_Create\DB_A34C8E_cafeavengers_11_8_2020.bak' WITH  FILE = 1,  MOVE N'POS' TO N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\$(NDB)_data.mdf',  MOVE N'POS_log' TO N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\$(NDB)_log.ldf',  KEEP_REPLICATION,  NOUNLOAD,  REPLACE,  STATS = 5
GO
USE [$(NDB)]
GO
CREATE USER [$(NUSR)] FOR LOGIN [$(NUSR)]
GO
USE [$(NDB)]
GO
ALTER USER [$(NUSR)] WITH DEFAULT_SCHEMA=[db_owner]
GO
USE [$(NDB)]
GO
ALTER ROLE [db_owner] ADD MEMBER [$(NUSR)]
GO
UPDATE [$(NDB)].[dbo].[RoleInfo]
SET email = '$(EMAIL)', ExpiryDate = DATEADD(MONTH,1,GETDATE()), password ='ACeq0pMe2KJ2MqGrejKCzQ=='
WHERE roleID = 3;


