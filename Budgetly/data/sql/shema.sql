CREATE DATABASE Budgetly;
GO
USE Budgetly;
GO

-- Usuario (solo UNO HABRA prque es personal)
CREATE TABLE Usuario(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(100) NOT NULL,
    CorreoElectronico VARCHAR(100) NOT NULL,
    PassWordHash VARCHAR(255) NOT NULL,
    Telefono VARCHAR(50),
    FechaRegistro DATETIME NOT NULL,
    EsAutenticadoGoogle BIT NOT NULL
);
GO

-- Categorías
CREATE TABLE Categoria(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL, 
    Descripcion VARCHAR(200),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id)
); 
GO

-- Transacciones
CREATE TABLE Transaccion(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(500), 
    Monto DECIMAL(18,2),
    FechaTransaccion DATETIME NOT NULL,
    Tipo VARCHAR(20) NOT NULL, -- 'Gasto' o 'Ingreso'
    CategoriaId INT NOT NULL FOREIGN KEY REFERENCES Categoria(Id),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id)
); 
GO

-- Presupuesto
CREATE TABLE Presupuesto (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Monto DECIMAL(18,2),
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL,
    CategoriaId INT NOT NULL FOREIGN KEY REFERENCES Categoria(Id),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id)
); 
GO

-- Informe
CREATE TABLE Informe(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Titulo VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(MAX) NOT NULL,
    FechaInforme DATETIME NOT NULL,
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id)
); 
GO

-- Sesiones
CREATE TABLE Sesiones(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id),
    FechaIngreso DATETIME NOT NULL,
    FechaSalida DATETIME,
    DuracionSesion INT -- en minutos o segundos, según tu lógica
); 
GO

-- Configuración personal
CREATE TABLE Configuracion(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuario(Id),
    Moneda VARCHAR(10) NOT NULL -- Ej: USD, PEN, MXN, etc.
); 
GO
