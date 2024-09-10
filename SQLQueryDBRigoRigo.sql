USE [RigoRigoVentasDB]
GO

/****** Object:  Table [dbo].[PedidoDetalle]    Script Date: 10/09/2024 8:09:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPedidoEncabezado] [int] NULL,
	[IdProductosRigo] [int] NULL,
	[Cantidad] [int] NULL,
	[ValorTotal] [numeric](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PedidoEncabezado]    Script Date: 10/09/2024 8:09:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoEncabezado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroIdentificacion] [nvarchar](50) NULL,
	[DireccionEntrega] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ProductosRigo]    Script Date: 10/09/2024 8:09:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductosRigo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Articulo] [nvarchar](500) NULL,
	[ValorUnitario] [numeric](18, 2) NULL,
	[CantidadExistente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PedidoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PedidoDetalle_PedidoEncabezdo] FOREIGN KEY([IdPedidoEncabezado])
REFERENCES [dbo].[PedidoEncabezado] ([Id])
GO
ALTER TABLE [dbo].[PedidoDetalle] CHECK CONSTRAINT [FK_PedidoDetalle_PedidoEncabezdo]
GO
ALTER TABLE [dbo].[PedidoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PedidoDetalle_ProductosRigo] FOREIGN KEY([IdProductosRigo])
REFERENCES [dbo].[ProductosRigo] ([Id])
GO
ALTER TABLE [dbo].[PedidoDetalle] CHECK CONSTRAINT [FK_PedidoDetalle_ProductosRigo]
GO

/****** Object:  StoredProcedure [dbo].[SP_InsertarPedido]    Script Date: 10/09/2024 8:09:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_InsertarPedido]
    @jsonInput NVARCHAR(MAX)
AS
BEGIN
    DECLARE @IdPedidoEncabezado INT;
    
    -- Insertar el encabezado en la tabla PedidoEncabezado
    INSERT INTO [RigoRigoVentasDB].[dbo].[PedidoEncabezado] (NumeroIdentificacion, DireccionEntrega)
    SELECT NumeroIdentificacion, DireccionEntrega
    FROM OPENJSON(@jsonInput, '$.encabezado')
    WITH (
        NumeroIdentificacion NVARCHAR(50),
        DireccionEntrega NVARCHAR(200)
    );

    -- Obtener el ID del PedidoEncabezado recién insertado
    SET @IdPedidoEncabezado = SCOPE_IDENTITY();

    -- Insertar el detalle en la tabla PedidoDetalle
    INSERT INTO [RigoRigoVentasDB].[dbo].[PedidoDetalle] (IdPedidoEncabezado, IdProductosRigo, Cantidad, ValorTotal)
    SELECT @IdPedidoEncabezado, dp.IdProductosRigo, dp.Cantidad, dp.ValorTotal
    FROM OPENJSON(@jsonInput, '$.detalle')
    WITH (
        IdProductosRigo INT,
        Cantidad INT,
        ValorTotal NUMERIC(18, 2)
    ) AS dp;
END;
GO

/****** Object:  StoredProcedure [dbo].[SP_VerProducto]    Script Date: 10/09/2024 8:09:28 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_VerProducto]
AS
SELECT * FROM [RigoRigoVentasDB].[dbo].[ProductosRigo]
GO
