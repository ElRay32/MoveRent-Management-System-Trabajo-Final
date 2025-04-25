CREATE DATABASE MoveRentDB;
GO

USE MoveRentDB;

CREATE TABLE Auto (
    Id INT PRIMARY KEY IDENTITY,
    Marca NVARCHAR(50),
    Modelo NVARCHAR(50),
    Placa NVARCHAR(20),
    Año int,
    Color NVARCHAR(20),
    Disponible BIT,
    FechaCreacion datetime,
    FechaModificacion datetime
);

CREATE TABLE Cliente (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Cedula NVARCHAR(20),
    Edad int,
    Telefono NVARCHAR(20),
    FechaCreacion datetime,
    FechaModificacion datetime
);

CREATE TABLE Reserva (
    Id INT PRIMARY KEY IDENTITY,
    IdCliente INT,
    IdAuto INT,
    FechaReserva DATETIME,
	MontoTotal DECIMAL(10, 2),
    FechaModificacion datetime
    FOREIGN KEY (IdCliente) REFERENCES Cliente(Id),
    FOREIGN KEY (IdAuto) REFERENCES Auto(Id)

);

CREATE TABLE Pago (
    Id INT PRIMARY KEY IDENTITY,
    IdReserva INT,
    Monto DECIMAL(10, 2),
    FechaPago DATETIME,
    Pagado BIT NOT NULL DEFAULT 0
    FOREIGN KEY (IdReserva) REFERENCES Reserva(Id) ON DELETE CASCADE
);
