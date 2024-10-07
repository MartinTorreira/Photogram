

 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\source\'

 
USE [master]


/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'photogram')
DROP DATABASE [photogram]


USE [master]


/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [photogram] 
    ON PRIMARY ( NAME = photogram, FILENAME = "' + @Default_DB_Path + N'photogram.mdf")
    LOG ON ( NAME = photogram_log, FILENAME = "' + @Default_DB_Path + N'photogram_log.ldf")'

EXEC(@sql)
PRINT N'Database [Photogram] created.'
GO