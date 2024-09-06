/****** Object:  StoredProcedure [dbo].[usp_ObtenerHistorialTransaccionesPorTarjeta]    Script Date: 3/9/2024 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Gustavo Pineda
-- Create date: 03-09-2024
-- Description:	Obtener el historial de transacciones de una tarjeta
-- =============================================
CREATE PROCEDURE [dbo].[usp_ObtenerHistorialTransaccionesPorTarjeta] 
	@IdTarjeta as bigint
AS
BEGIN
	DECLARE @PrimerDiaDelMes DATE = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0);
    DECLARE @UltimoDiaDelMes DATE = DATEADD(DAY, -1, DATEADD(MONTH, 1, @PrimerDiaDelMes));

    SELECT Tipo, IdTarjeta, Fecha, Descripcion, Monto
    FROM Transaccion
    WHERE IdTarjeta = @IdTarjeta
      AND Fecha >= @PrimerDiaDelMes
      AND Fecha <= @UltimoDiaDelMes
    ORDER BY Fecha DESC;
END
GO