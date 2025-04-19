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
