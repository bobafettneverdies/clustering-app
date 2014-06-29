USE master;

DECLARE @DatabaseName nvarchar(50)
SET @DatabaseName = N'ClusteringAppTestDB'

DECLARE @SQL varchar(max)

SELECT @SQL = COALESCE(@SQL,'') + ' BEGIN TRY Kill ' + Convert(varchar, SPId) + '; END TRY BEGIN CATCH END CATCH;'
FROM MASTER..SysProcesses
WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId

EXEC(@SQL)

RESTORE DATABASE ClusteringAppTestDB from 
DATABASE_SNAPSHOT = 'ClusteringAppTestDB_Snapshot';
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'ClusteringAppTestDB_Snapshot')
DROP DATABASE ClusteringAppTestDB_Snapshot
GO