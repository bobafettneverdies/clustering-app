USE [master]
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'ClusteringAppTestDB_Snapshot')
DROP DATABASE ClusteringAppTestDB_Snapshot
GO

CREATE DATABASE ClusteringAppTestDB_Snapshot
ON (NAME = 'ClusteringAppTestDB', FILENAME = 'C:\ClusteringAppTestDB_TestBU.SNP')
   AS SNAPSHOT OF ClusteringAppTestDB;