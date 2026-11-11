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