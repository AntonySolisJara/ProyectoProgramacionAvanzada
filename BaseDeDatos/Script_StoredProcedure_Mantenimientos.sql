USE ProyectoPrograAvanzada
GO



 -------------------------------------------------------------------------------------------------------------------------------
 -------------------------------------------------------------------------------------------------------------------------------
 ------------------------------------------- UNIDAD DE MEDIDA-------------------------------------------------------------------
 -------------------------------------------------------------------------------------------------------------------------------
 -------------------------------------------------------------------------------------------------------------------------------
		
		
		/****Creacion de Stored Procedure****/
		/*Guardar Unidad de Medida*/


GO
CREATE PROCEDURE Guardar_UnidadDeMedida
	@Siglas VARCHAR(3),
	@Descripcion VARCHAR(50)
AS
	BEGIN
		BEGIN TRAN Guardar_UnidadDeMedida
		BEGIN TRY
			IF NOT EXISTS(SELECT Siglas FROM Unidad_Medida WHERE Siglas = @Siglas)
		BEGIN
			INSERT INTO Unidad_Medida
			(Siglas, Descripcion)
		VALUES
			(@Siglas, @Descripcion);
	COMMIT TRAN Guardar_UnidadDeMedida
	END
	ELSE
	BEGIN
		PRINT 'La unidad de medida ya existe en la Base de Datos'
	END
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN Guardar_UnidadDeMedida
	END CATCH
	END
GO


----------------------------------------------------------------------------------------------------------------------------------



		/****Creacion de Stored Procedure****/
		/*Modificar Unidad de Medida*/
GO
CREATE PROCEDURE UNIDAD_MEDIDA_MODIFICAR
	@ID_Unidad int,
    @Siglas varchar (3),
    @Descripcion varchar (50)
AS
	BEGIN TRAN ModificarUnidadMedida
	BEGIN TRY 		
		UPDATE
			ProyectoPrograAvanzada.dbo.Unidad_Medida
		SET
		ID_Unidad = @ID_Unidad,
        Siglas = @Siglas,
        Descripcion = @Descripcion

			
		WHERE ID_Unidad = @ID_Unidad;
		COMMIT TRAN ModificarUnidadMedida;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN ModificarUnidadMedida;
	END CATCH;
GO


-----------------------------------------------------------------------------------------------------------------------------------------


		/****Creacion de Stored Procedure****/
		/*Eliminar Unidad de Medida*/
GO
	CREATE PROCEDURE UNIDAD_MEDIDA_ELIMINAR
	@ID_Unidad int,
    @Siglas varchar (3),
    @Descripcion varchar (50)
	 
AS
	BEGIN
		BEGIN TRAN eliminarUnidadMedida
			BEGIN TRY		
				DELETE FROM Unidad_Medida WHERE ID_Unidad = 1;
				COMMIT TRAN eliminarUnidadMedida	
			END TRY

			BEGIN CATCH
				ROLLBACK TRAN eliminarUnidadMedida;
			END CATCH;
	
	END;
GO


-----------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------- METADATA -----------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------




		/****Creacion de Stored Procedure****/
		/*Consultar Metadata*/

GO
CREATE PROCEDURE CONSULTAR_METADATA 
AS
	SELECT

	ID_Metadata,
    Nombre_metadata,
    Prefijo,
    Descripcion
		
	FROM
	ProyectoPrograAvanzada.dbo.Metadata
	ORDER BY Descripcion;
GO


	------------------------------------------------------------------------------------------------------------------------------------


	/****Creacion de Stored Procedure****/
		/*Modificar Metadata*/
GO
CREATE PROCEDURE METADATA_MODIFICAR
	@ID_Metadata int,
	@Nombre_metadata varchar(20),
	@Prefijo varchar (3),
	@Descripcion varchar (50)
AS
	BEGIN TRAN ModificarMetadata
	BEGIN TRY 		
		UPDATE
			ProyectoPrograAvanzada.dbo.Metadata
		SET
		ID_Metadata = @ID_Metadata,
        Nombre_metadata = @Nombre_metadata,
        Prefijo = @Prefijo,
		Descripcion = @Descripcion

			
		WHERE ID_Metadata = @ID_Metadata;
		COMMIT TRAN ModificarMetadata;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN ModificarMetadata;
	END CATCH;
	GO


	-----------------------------------------------------------------------------------------------------------------------------------------


	/****Creacion de Stored Procedure****/
		/*Crear Metadata*/
GO
	CREATE PROCEDURE CREAR_METADATA
	@ID_Metadata	int,
	@Nombre_metadata varchar (20),
	@Prefijo varchar (3),
	@Descripcion varchar (50)

	
AS
	BEGIN
		BEGIN TRAN CrearMetadata
		BEGIN TRY
			IF NOT EXISTS(SELECT ID_Metadata FROM Metadata WHERE ID_Metadata = @ID_Metadata)
		BEGIN
			INSERT INTO Metadata
			(ID_Metadata, Nombre_metadata, Prefijo, Descripcion)
		VALUES
			(@ID_Metadata, @Nombre_metadata, @Prefijo, @Descripcion);
	COMMIT TRAN CrearMetadata
	END
	ELSE
	BEGIN
		PRINT 'La Metadata ya existe en la Base de Datos'
	END
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN CrearMetadata
	END CATCH
	END
GO



-----------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------- POSVALOR -----------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------




		/****Creacion de Stored Procedure****/
		/*Consultar Posvalor*/
GO
CREATE PROCEDURE CONSULTAR_POSVALOR
AS
	SELECT

	ID_Posvalor,
	Posvalor,
	Descripcion,
	ID_Metadata
		
	FROM
	ProyectoPrograAvanzada.dbo.Posvalor
	ORDER BY Posvalor;
GO

	------------------------------------------------------------------------------------------------------------------------------------


	/****Creacion de Stored Procedure****/
		/*Modificar Posvalor*/
GO
CREATE PROCEDURE POSVALOR_MODIFICAR
	@ID_Posvalor int,
	@Posvalor varchar (3),
	@Descripcion varchar (50),
	@ID_Metadata INT
AS
	BEGIN TRAN ModificarPosvalores
	BEGIN TRY 		
		UPDATE
			ProyectoPrograAvanzada.dbo.Posvalor
		SET
		ID_Posvalor = @ID_Posvalor,
        Posvalor = @Posvalor,
        Descripcion = @Descripcion,
		ID_Metadata = @ID_Metadata

			
		WHERE ID_Posvalor = @ID_Posvalor;
		COMMIT TRAN ModificarPosvalor;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN ModificarPosvalor;
	END CATCH;
	GO


	-----------------------------------------------------------------------------------------------------------------------------------------


	/****Creacion de Stored Procedure****/
		/*Crear Posvalor*/

GO
	CREATE PROCEDURE CREAR_POSVALOR
	@ID_Posvalor int,
	@Posvalor varchar (3),
	@Descripcion varchar (50),
	@ID_Metadata INT

		
AS
	BEGIN
		BEGIN TRAN CrearPosvalor
		BEGIN TRY
			IF NOT EXISTS(SELECT ID_Posvalor FROM Posvalor WHERE ID_Posvalor = @ID_Posvalor)
		BEGIN
			INSERT INTO Posvalor
			(ID_Posvalor, Posvalor, Descripcion, ID_Metadata)
		VALUES
			(@ID_Posvalor, @Posvalor, @Descripcion, @ID_Metadata);
	COMMIT TRAN CrearPosvalor
	END
	ELSE
	BEGIN
		PRINT 'El Posvalor ya existe en la Base de Datos'
	END
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN CrearPosvalor
		END CATCH
	END


GO


