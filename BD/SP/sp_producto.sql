IF OBJECT_ID('sp_producto', 'P') IS NOT NULL
    DROP PROCEDURE sp_producto;
GO

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Yhefferson Cicery>
-- Create date: <08/12/2020>
-- Description:	<sp para manejar crud a tabla de producto>
-- =============================================
CREATE PROCEDURE  sp_producto
	-- Add the parameters for the stored procedure here
@Opcion VARCHAR(2)
AS
BEGIN

IF @Opcion = 'C'
BEGIN 
  SELECT * FROM producto
END 
	
END
GO
