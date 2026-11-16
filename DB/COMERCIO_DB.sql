CREATE DATABASE COMERCIO_DB;
GO

USE COMERCIO_DB;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.MARCAS (
    Id              INT IDENTITY(1,1)   NOT NULL,
    Descripcion     VARCHAR(100)        NOT NULL,
    Activo          BIT                 NOT NULL        DEFAULT 1, -- 1 = Activo, 0 = Inactivo
    
    CONSTRAINT PK_MARCAS                PRIMARY KEY (Id)
);
GO

INSERT INTO MARCAS (Descripcion) VALUES ('Samsung'), ('Philips'), ('Logitech'), ('Sony')
GO

CREATE TABLE dbo.CLIENTES (
    Id          INT IDENTITY(1,1)   NOT NULL,
    Nombre              VARCHAR(100)        NOT NULL,
    Apellido            VARCHAR(100)        NOT NULL,
    Dni                 VARCHAR(20)         NOT NULL UNIQUE,
    Email               VARCHAR(150)        NOT NULL UNIQUE,
    Activo              BIT                 NOT NULL        DEFAULT 1,

    CONSTRAINT PK_CLIENTES                  PRIMARY KEY (Id)
);
GO

INSERT INTO Clientes (Nombre, Apellido, Dni, Email, Activo)
VALUES ('Juan', 'Perez', '35123456', 'juan.perez@email.com', 1);
GO

CREATE TABLE dbo.USUARIOS (
    Id                  INT IDENTITY(1,1)   NOT NULL,
    Nombre              VARCHAR(100)        NOT NULL,
    Apellido            VARCHAR(100)        NOT NULL,
    Dni                 VARCHAR(20)         NOT NULL UNIQUE,
    Email               VARCHAR(150)        NOT NULL UNIQUE,
    Activo              BIT                 NOT NULL        DEFAULT 1,
    NombreUsuario       VARCHAR(50)         NOT NULL UNIQUE,
    Contraseña          VARCHAR(100)        NOT NULL,
    Perfil              VARCHAR(20)         NOT NULL,

    CONSTRAINT PK_USUARIOS                  PRIMARY KEY (Id)
);
GO

INSERT INTO USUARIOS (Nombre, Apellido, Dni, Email, Activo, NombreUsuario, Contraseña, Perfil)
VALUES ('Admin', 'Sistema', '10000001', 'admin@comercio.com', 1, 'admin', '1234', 'Administrador'),
       ('Vera', 'Vendedora', '10000002', 'vera@comercio.com', 1, 'vera', '1234', 'Vendedor');
GO

CREATE TABLE dbo.CATEGORIAS (
    Id          INT IDENTITY(1,1)   NOT NULL,
    Descripcion VARCHAR(100)        NOT NULL,
    Activo      BIT                 NOT NULL    DEFAULT 1, 
    
    CONSTRAINT PK_CATEGORIAS            PRIMARY KEY (Id)
);
GO


INSERT INTO CATEGORIAS (Descripcion) VALUES ('Electrodomésticos'), ('Tecnología');
GO

CREATE TABLE dbo.PROVEEDORES (
    Id      INT IDENTITY(1, 1)  NOT NULL,
    RazonSocial VARCHAR(50)     NOT NULL,
    Cuit VARCHAR(15)            NOT NULL UNIQUE,
    Email VARCHAR(150)          NOT NULL UNIQUE,
    Telefono VARCHAR(16)        NOT NULL,
    Activo BIT                  NOT NULL    DEFAULT 1,
    CONSTRAINT PK_PROVEEDORES               PRIMARY KEY (Id)
);
GO

INSERT INTO dbo.PROVEEDORES (RazonSocial, Cuit, Email, Telefono, Activo)
VALUES 
    ('Distribuidora Argentina S.A.', '30-71234567-8', 'contacto@distribuidoraarg.com', '+541144445555', 1),
    ('Tecnologia y Servicios S.R.L.', '30-68987654-3', 'ventas@tecnologiaservicios.com', '+541155556666', 1),
    ('Logistica del Sur S.A.', '30-55443322-1', 'info@logisticadelsur.com.ar', '+541122223333', 0);
GO

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