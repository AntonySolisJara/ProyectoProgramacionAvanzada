USE ProyectoPrograAvanzada
GO

/****Creacion de Stored Procedure****/
/*Guardar Unidad de Medida*/
ALTER PROCEDURE SP_Guardar_Indicador
@cuenta_catalogo varchar(15),
@ID_Unidad INT,
@ID_Periodo INT,
@ID_Estado INT,
@ID_Posvalor INT
AS
BEGIN
BEGIN TRAN Guardar_Indicador
BEGIN TRY
IF NOT EXISTS(SELECT cuenta_catalogo FROM Cuenta_Catalogo WHERE cuenta_catalogo = @cuenta_catalogo)
BEGIN


INSERT INTO Cuenta_Catalogo
(cuenta_catalogo)
VALUES
(@cuenta_catalogo);



INSERT INTO Indicador
(ID_cta_Catalogo, ID_Unidad, ID_Periodo, ID_Estado)
VALUES
(IDENT_CURRENT( 'Cuenta_Catalogo' ), @ID_Unidad, @ID_Periodo, @ID_Estado);



INSERT INTO Indicador_Posvalor
(ID_Indicador,ID_Posvalor)
VALUES
(IDENT_CURRENT( 'Indicador' ), @ID_Posvalor),
(IDENT_CURRENT( 'Indicador' ), @ID_Posvalor),
(IDENT_CURRENT( 'Indicador' ), @ID_Posvalor);


COMMIT TRAN Guardar_Indicador
END
ELSE
BEGIN
PRINT 'La cuenta catalogo ya existe en la Base de Datos'
END
END TRY
BEGIN CATCH
ROLLBACK TRAN Guardar_Indicador
END CATCH
END
GO