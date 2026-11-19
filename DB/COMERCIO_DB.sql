CREATE DATABASE COMERCIO_DB;
GO

USE COMERCIO_DB;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- CREAR TABLA MARCAS
CREATE TABLE dbo.MARCAS (
    Id              INT IDENTITY(1,1)   NOT NULL,
    Descripcion     VARCHAR(100)        NOT NULL,
    Activo          BIT                 NOT NULL        DEFAULT 1, -- 1 = Activo, 0 = Inactivo
    
    CONSTRAINT PK_MARCAS                PRIMARY KEY (Id)
);
GO

-- INSERT TABLA MARCAS
INSERT INTO MARCAS (Descripcion) VALUES ('Samsung'), ('Philips'), ('Logitech'), ('Sony')
GO

-- CREAR TABLA CLIENTES
CREATE TABLE dbo.CLIENTES (
    Id          INT IDENTITY(1,1)   NOT NULL,
    Nombre      VARCHAR(100)        NOT NULL,
    Apellido    VARCHAR(100)        NOT NULL,
    Dni         VARCHAR(20)         NOT NULL UNIQUE,
    Email       VARCHAR(150)        NOT NULL UNIQUE,
    Activo      BIT                 NOT NULL        DEFAULT 1,

    CONSTRAINT PK_CLIENTES          PRIMARY KEY (Id)
);
GO

-- INSERT TABLA CLIENTES
INSERT INTO Clientes (Nombre, Apellido, Dni, Email, Activo)
VALUES ('Juan', 'Perez', '35123456', 'juan.perez@email.com', 1);
GO

-- STORE PROCEDURE TABLA CLIENTES
CREATE PROCEDURE listarClientes as
SELECT Id, Nombre, Apellido, Dni, Email, Activo FROM CLIENTES

-- CREAR TABLA USUARIOS
CREATE TABLE dbo.USUARIOS (
    Id                  INT IDENTITY(1,1)   NOT NULL,
    Nombre              VARCHAR(100)        NOT NULL,
    Apellido            VARCHAR(100)        NOT NULL,
    Dni                 VARCHAR(20)         NOT NULL UNIQUE,
    Email               VARCHAR(150)        NOT NULL UNIQUE,
    Activo              BIT                 NOT NULL            DEFAULT 1,
    NombreUsuario       VARCHAR(50)         NOT NULL UNIQUE,
    Clave               VARCHAR(100)        NOT NULL,
    Perfil              VARCHAR(20)         NOT NULL,

    CONSTRAINT PK_USUARIOS                  PRIMARY KEY (Id)
);
GO

-- INSERT TABLA USUARIOS
INSERT INTO USUARIOS (Nombre, Apellido, Dni, Email, Activo, NombreUsuario, Clave, Perfil)
VALUES ('Admin', 'Sistema', '10000001', 'admin@comercio.com', 1, 'admin', '1234', 'Administrador'),
       ('Vera', 'Vendedora', '10000002', 'vera@comercio.com', 1, 'vera', '1234', 'Vendedor');
GO

-- CREAR TABLA CATEGORIAS
CREATE TABLE dbo.CATEGORIAS (
    Id          INT IDENTITY(1,1)   NOT NULL,
    Descripcion VARCHAR(100)        NOT NULL,
    Activo      BIT                 NOT NULL    DEFAULT 1, 
    
    CONSTRAINT PK_CATEGORIAS        PRIMARY KEY (Id)
);
GO

-- INSERT TABLA CATEGORIAS
INSERT INTO CATEGORIAS (Descripcion) VALUES ('Electrodomésticos'), ('Tecnología');
GO

-- CREAR TABLA PROVEEDORES
CREATE TABLE dbo.PROVEEDORES (
    Id      INT IDENTITY(1, 1)      NOT NULL,
    RazonSocial VARCHAR(50)         NOT NULL,
    Cuit VARCHAR(15)                NOT NULL UNIQUE,
    Email VARCHAR(150)              NOT NULL UNIQUE,
    Telefono VARCHAR(16)            NOT NULL,
    Activo BIT                      NOT NULL            DEFAULT 1,
    CONSTRAINT PK_PROVEEDORES       PRIMARY KEY (Id)
);
GO

-- INSERT TABLA PROVEEDORES
INSERT INTO dbo.PROVEEDORES (RazonSocial, Cuit, Email, Telefono, Activo)
VALUES 
    ('Distribuidora Argentina S.A.', '30-71234567-8', 'contacto@distribuidoraarg.com', '+541144445555', 1),
    ('Tecnologia y Servicios S.R.L.', '30-68987654-3', 'ventas@tecnologiaservicios.com', '+541155556666', 1),
    ('Logistica del Sur S.A.', '30-55443322-1', 'info@logisticadelsur.com.ar', '+541122223333', 0);
GO

-- CREAR TABLA VENTAS
CREATE TABLE dbo.VENTAS (
    Id INT IDENTITY(1, 1)               NOT NULL,
    NumeroFactura VARCHAR(50)           NOT NULL,
    Fecha DATETIME                      NOT NULL DEFAULT GETDATE(),
    IdCliente INT                       NOT NULL,
    TotalFinal DECIMAL(18, 2)           NOT NULL,

    CONSTRAINT PK_VENTAS                PRIMARY KEY (Id),
    CONSTRAINT FK_VENTAS_CLIENTES       FOREIGN KEY(IdCliente) REFERENCES dbo.CLIENTES(Id)
);
GO

-- CREAR TABLA PRODUCTOS
CREATE TABLE dbo.PRODUCTOS (
    Id                  INT IDENTITY(1,1)   NOT NULL,
    CodigoProducto      VARCHAR(50)         NOT NULL UNIQUE,
    Nombre              VARCHAR(100)        NOT NULL,
    Descripcion         VARCHAR(250)        NULL,
    PrecioCompraActual  DECIMAL(18, 2)      NOT NULL        DEFAULT 0,
    PorcentajeGanancia  DECIMAL(5, 2)       NOT NULL        DEFAULT 0, 
    StockActual         INT                 NOT NULL        DEFAULT 0,
    StockMinimo         INT                 NOT NULL        DEFAULT 0,
    IdMarca             INT                 NOT NULL,
    IdCategoria         INT                 NOT NULL,
    Activo              BIT                 NOT NULL        DEFAULT 1,

    CONSTRAINT PK_PRODUCTOS             PRIMARY KEY (Id),
    CONSTRAINT FK_PRODUCTOS_MARCAS      FOREIGN KEY (IdMarca)       REFERENCES dbo.MARCAS(Id),
    CONSTRAINT FK_PRODUCTOS_CATEGORIAS  FOREIGN KEY (IdCategoria)   REFERENCES dbo.CATEGORIAS(Id)
);
GO

-- INSERTAR PRODUCTOS (StockActual y PrecioCompraActual en 0)
INSERT INTO dbo.PRODUCTOS (
    CodigoProducto,
    Nombre,
    Descripcion,
    PrecioCompraActual,
    PorcentajeGanancia,
    StockActual,
    StockMinimo,
    IdMarca,
    IdCategoria,
    Activo
)
VALUES
    ('SAM-TV50', 'Smart TV 50" 4K UHD', 'Televisor LED 50 pulgadas resolución 4K con HDR', 0, 35.00, 0, 5, 1, 1, 1),
    ('PHI-CAF01', 'Cafetera Express Automática', 'Cafetera con espumador de leche integrado 15 bares', 0, 40.00, 0, 3, 2, 1, 1),
    ('LOG-G502', 'Mouse Gamer G502 Hero', 'Mouse óptico con cable, sensor 25K DPI, 11 botones', 0, 50.00, 0, 10, 3, 2, 1),
    ('SON-WH1000', 'Auriculares Sony WH-1000XM5', 'Auriculares inalámbricos con cancelación de ruido', 0, 30.00, 0, 4, 4, 2, 1);
GO

-- CREAR TABLA PARA ASOCIAR PRODUCTOS_PROVEEDORES
CREATE TABLE dbo.PRODUCTOS_PROVEEDORES (
    IdProducto      INT     NOT NULL,
    IdProveedor     INT     NOT NULL,

    CONSTRAINT PK_PRODUCTOS_PROVEEDORES             PRIMARY KEY (IdProducto, IdProveedor),
    CONSTRAINT FK_PRODUCTOS_PROVEEDORES_PRODUCTO    FOREIGN KEY (IdProducto)    REFERENCES dbo.PRODUCTOS(Id),
    CONSTRAINT FK_PRODUCTOS_PROVEEDORES_PROVEEDOR   FOREIGN KEY (IdProveedor)   REFERENCES dbo.PROVEEDORES(Id)
);
GO

-- ASOCIAR PRODUCTOS CON PROVEEDORES
INSERT INTO dbo.PRODUCTOS_PROVEEDORES (IdProducto, IdProveedor)
VALUES 
    (1, 1),
    (1, 2),
    (2, 1),
    (3, 2),
    (3, 3),
    (4, 1),
    (4, 2),
    (4, 3);
GO

-- STORE PROCEDURE TABLA PRODUCTOS - MARCAS - CATEGORIAS
CREATE PROCEDURE dbo.listarProductos
AS
BEGIN
    SELECT  
        P.Id, 
        P.CodigoProducto, 
        P.Nombre, 
        P.Descripcion, 
        P.StockActual, 
        P.StockMinimo, 
        P.PorcentajeGanancia, 
        P.PrecioCompraActual, 
        P.Activo,
        M.Id AS IdMarca, 
        M.Descripcion AS MarcaDescripcion,
        C.Id AS IdCategoria, 
        C.Descripcion AS CategoriaDescripcion
    FROM dbo.PRODUCTOS P
    INNER JOIN dbo.MARCAS M 
        ON P.IdMarca = M.Id
    INNER JOIN dbo.CATEGORIAS C 
        ON P.IdCategoria = C.Id;
END
GO

-- SP PARA LISTAR PROVEEDORES DE UN PRODUCTO
CREATE PROCEDURE dbo.listarProveedoresPorProducto
    @IdProducto INT
AS
BEGIN
    SELECT 
        PR.Id, 
        PR.RazonSocial, 
        PR.Cuit, 
        PR.Email, 
        PR.Telefono, 
        PR.Activo
    FROM dbo.PROVEEDORES PR
    INNER JOIN dbo.PRODUCTOS_PROVEEDORES PP 
        ON PR.Id = PP.IdProveedor
    WHERE PP.IdProducto = @IdProducto;
END
GO