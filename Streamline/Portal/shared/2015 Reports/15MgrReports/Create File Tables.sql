/*

EXEC sp_configure filestream_access_level, 2
RECONFIGURE

-- Create Database
CREATE DATABASE FileTableDB 
ON  PRIMARY  
(
	NAME = N'FileTableDB',
	FILENAME = N'D:\SQL_Data\FileTableDB.mdf'
),
FILEGROUP FilestreamFG CONTAINS FILESTREAM  
(
	NAME = FileStreamGroup1,
	FILENAME = 'D:\SQL_Data\Data'
)
LOG ON
(
	NAME = N'FileTableDB_Log',
	FILENAME = N'D:\SQL_Data\FileTableDB_log.ldf'
)
WITH FILESTREAM
(
	NON_TRANSACTED_ACCESS = FULL,
	DIRECTORY_NAME = N'FileTables'
)
GO

-- Verify Creation of DB
SELECT DB_NAME ( database_id ), directory_name 
    FROM sys.database_filestream_options; 
GO

-- Verify enabling of Non-Transacted Access
SELECT DB_NAME(database_id), non_transacted_access, non_transacted_access_desc 
    FROM sys.database_filestream_options; 
GO
*/

/*
-- Create FileTable
USE FileTableDB
GO

CREATE TABLE DocumentStore AS FileTable 
WITH
( 
	FileTable_Directory = 'DocumentTable', 
	FileTable_Collate_Filename = database_default 
); 
GO

-- Verify Table Creation
SELECT OBJECT_NAME(parent_object_id) AS 'FileTable', OBJECT_NAME(object_id) AS 'System-defined Object' 
    FROM sys.filetable_system_defined_objects 
    ORDER BY FileTable, 'System-defined Object'; 
GO
*/

-- Put files in: \\JMANN-DEV1\mssqlserver\FileTables\DocumentTable
USE FileTableDB
GO

SELECT *
FROM DocumentStore


