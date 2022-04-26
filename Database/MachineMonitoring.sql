IF NOT EXISTS ( SELECT [name] FROM sys.databases WHERE [name] = 'MachineMonitoring' )
BEGIN
	USE master
	CREATE DATABASE MachineMonitoring
	USE MachineMonitoring
END
ELSE
PRINT 'Database : " MachineMonitoring " already existe'
GO

IF NOT EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'Machine' )
BEGIN
	CREATE TABLE Machine(
	machineId INT NOT NULL IDENTITY(1,1),
	Name VARCHAR(50) NOT NULL,
	Description VARCHAR(250) NULL,
	CONSTRAINT PK_MACHINE PRIMARY KEY (machineId)
	)
END
ELSE
BEGIN
	PRINT 'Table : " Machine " already existe'
END
GO


IF NOT EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'MachineProduction' )
BEGIN
	CREATE TABLE MachineProduction(
	MachineProductionId INT NOT NULL IDENTITY(1,1),
	MachineId INT NOT NULL,
	totalProduction INT NOT NULL DEFAULT 0,
	CONSTRAINT PK_MachineProduction PRIMARY KEY (MachineProductionId),
	CONSTRAINT FK_MachineProduction_Machine FOREIGN KEY (MachineId) REFERENCES Machine (machineId)
	ON DELETE CASCADE
	)
END
ELSE
PRINT 'Table : " MachineProduction " already existe'
GO


IF EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'Machine' )
INSERT INTO Machine (Name,Description) 
VALUES ('Equipement A','Description of Equipement A'),
('Equipement B','Description of Equipement B'),
('Equipement C','Description of Equipement C'),
('Equipement D','Description of Equipement D')
GO
IF EXISTS ( SELECT [name] FROM sys.tables WHERE [name] = 'MachineProduction' )
INSERT INTO MachineProduction (MachineId,totalProduction) 
VALUES (1,6),(2,9),(3,5),(4,7)
GO



