CREATE DATABASE RentalCarDB;
GO

USE RentalCarDB;

CREATE TABLE Auto (
    Id INT PRIMARY KEY IDENTITY,
    Marca NVARCHAR(50),
    Modelo NVARCHAR(50),
    Placa NVARCHAR(20),
    Disponible BIT
);

CREATE TABLE Cliente (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Cedula NVARCHAR(20),
    Telefono NVARCHAR(20)
);

CREATE TABLE Reserva (
    Id INT PRIMARY KEY IDENTITY,
    IdCliente INT,
    IdAuto INT,
    FechaReserva DATETIME,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(Id),
    FOREIGN KEY (IdAuto) REFERENCES Auto(Id)
);

CREATE TABLE Pago (
    Id INT PRIMARY KEY IDENTITY,
    IdReserva INT,
    Monto DECIMAL(10, 2),
    FechaPago DATETIME,
    FOREIGN KEY (IdReserva) REFERENCES Reserva(Id)
);
