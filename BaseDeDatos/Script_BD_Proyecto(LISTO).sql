/****Creacion de la BD*****/
USE MASTER
GO

/*Validamos si la BD ya existe para eliminarla completamente*/
BEGIN
IF EXISTS(SELECT NAME FROM SYS.DATABASES WHERE NAME = 'ProyectoPrograAvanzada')
DROP DATABASE ProyectoPrograAvanzada
END

/*Creamos la BD desde cero*/
CREATE DATABASE ProyectoPrograAvanzada
PRINT 'La base de datos ha sido creada exitosamente'
GO

USE ProyectoPrograAvanzada
GO

/*No toma en cuenta Null*/
SET ANSI_NULLS ON
GO

/*Manejo de comillas*/
SET QUOTED_IDENTIFIER ON
GO

/*Uso de T-SQL (COMMIT Y ROLLBACK de Transact)*/
SET IMPLICIT_TRANSACTIONS OFF 
GO

/****Creacion de las Tablas****/
/*Creamos las Tablas y Campos*/
Create Table CuentaCatalogo
(
IdCtaCatalogo int IDENTITY (1,1),
CtaCatalogo varchar(15) NOT NULL
);
ALTER TABLE CuentaCatalogo ADD CONSTRAINT PK_PPA_Cuenta_Catalogo PRIMARY KEY (CtaCatalogo);
GO

Create Table Metadata
(
IdMetadata	int IDENTITY (1,1),
SiglasMet varchar (3) NOT NULL,
NomMetadata varchar (20) NOT NULL,
DescMetadata varchar (50) NOT NULL
);
ALTER TABLE Metadata ADD CONSTRAINT PK_PPA_Metadata	PRIMARY KEY (SiglasMet);
GO

Create Table Posvalor
(
IdPosvalor int IDENTITY (1,1),
SiglasMet varchar (3) NOT NULL,
SiglasPos varchar (3) NOT NULL,
NomPosvalor varchar (20) NOT NULL,
DescPosvalor varchar (50) NOT NULL
);
ALTER TABLE Posvalor ADD CONSTRAINT PK_PPA_Posvalor	PRIMARY KEY (IdPosvalor);
GO

Create Table IndicadorPosvalor
(
IdIndPosvalor int IDENTITY (1,1),
IdIndicador INT NOT NULL,
IdPosvalor INT,
Orden INT NOT NULL
);
ALTER TABLE IndicadorPosvalor ADD CONSTRAINT PK_PPA_IndPos PRIMARY KEY (IdIndPosvalor);
GO

Create Table Indicador
(
IdIndicador int IDENTITY (1,1),
CtaCatalogo varchar(15) NOT NULL,
SiglasEst varchar (4) NOT NULL,
SiglasPer varchar (1) NOT NULL,
SiglasUni varchar (3) NOT NULL
);
ALTER TABLE Indicador ADD CONSTRAINT PK_PPA_Indicador PRIMARY KEY (IdIndicador);
GO

Create Table UnidadMedida
(
IdUnidad int IDENTITY (1,1),
SiglasUni varchar (3) NOT NULL,
DescUnidad varchar (50) NOT NULL
);
ALTER TABLE UnidadMedida ADD CONSTRAINT PK_PPA_Unidad_Medida PRIMARY KEY (SiglasUni);
GO

Create Table Periodo
(
IdPeriodo int IDENTITY (1,1),
SiglasPer varchar (1) NOT NULL,
DescPeriodo varchar (50) NOT NULL
);
ALTER TABLE Periodo ADD CONSTRAINT PK_PPA_Periodo PRIMARY KEY (SiglasPer);
GO

Create Table Estado
(
IdEstado int IDENTITY (1,1),
SiglasEst varchar (4) NOT NULL,
DescEstado varchar (50) NOT NULL	
);
ALTER TABLE Estado ADD CONSTRAINT PK_PPA_Estado PRIMARY KEY (SiglasEst);
GO

/****Definicion de relaciones Foraneas****/
/*Foreign Key*/
ALTER TABLE Indicador			ADD CONSTRAINT FK_PPA_IndicadorU			FOREIGN KEY (SiglasUni)			REFERENCES UnidadMedida(SiglasUni);
ALTER TABLE Indicador			ADD CONSTRAINT FK_PPA_IndicadorP			FOREIGN KEY (SiglasPer)			REFERENCES Periodo(SiglasPer);
ALTER TABLE Indicador			ADD CONSTRAINT FK_PPA_IndicadorE			FOREIGN KEY (SiglasEst)			REFERENCES Estado(SiglasEst);
ALTER TABLE Indicador			ADD CONSTRAINT FK_PPA_IndicadorC			FOREIGN KEY (CtaCatalogo)		REFERENCES CuentaCatalogo(CtaCatalogo);
ALTER TABLE IndicadorPosvalor	ADD CONSTRAINT FK_PPA_Indicador_Posvalor1	FOREIGN KEY (IdIndicador)		REFERENCES Indicador(IdIndicador);
ALTER TABLE IndicadorPosvalor	ADD CONSTRAINT FK_PPA_Indicador_Posvalor2	FOREIGN KEY (IdPosvalor)		REFERENCES Posvalor(IdPosvalor);
ALTER TABLE Posvalor			ADD CONSTRAINT FK_PPA_Posvalor				FOREIGN KEY (SiglasMet)			REFERENCES Metadata(SiglasMet);
GO