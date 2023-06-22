CREATE DATABASE CajeroAutomatico
GO

USE CajeroAutomatico
GO

-- Creación de la tabla Tarjeta
CREATE TABLE Tarjetas (
  NroTarjeta VARCHAR(16) PRIMARY KEY,
  FechaVencimiento DATE,
  Saldo DECIMAL(10, 2),
  Pin varchar(4),
  Reintentos INT,
  Bloqueo BIT
);
GO

-- Creación de la tabla Operacion
CREATE TABLE Operaciones (
  CodigoOperacion INT IDENTITY(1, 1) PRIMARY KEY,
  IdTipoOperacion INT,
  NroTarjeta VARCHAR(16),
  FechaOperacion DATETIME,
  MontoRetirado DECIMAL(10, 2)
);
GO

-- Creación de la tabla TipoOperacion
CREATE TABLE TipoOperacion (
  IdTipoOperacion INT IDENTITY(1, 1) PRIMARY KEY,
  Operacion VARCHAR(50)
);
GO

ALTER TABLE Operaciones
ADD CONSTRAINT Operaciones_Tarjetas_NroTarjeta_FK
FOREIGN KEY (NroTarjeta) REFERENCES Tarjetas (NroTarjeta);
GO

ALTER TABLE Operaciones
ADD CONSTRAINT Operaciones_TipoOperacion_IdTipoOperacion_FK
FOREIGN KEY (IdTipoOperacion) REFERENCES TipoOperacion (IdTipoOperacion);
GO

INSERT INTO TipoOperacion (Operacion) VALUES ('Retiro')
INSERT INTO TipoOperacion (Operacion) VALUES ('Balance')

INSERT INTO Tarjetas(NroTarjeta, FechaVencimiento, Saldo, Pin, Reintentos, Bloqueo) VALUES ('1111111111111111', '2023-12-31', 5000, '1234', 0, 0)
INSERT INTO Tarjetas(NroTarjeta, FechaVencimiento, Saldo, Pin, Reintentos, Bloqueo) VALUES ('1234567812345678', '2023-12-31', 10000, '7777', 0, 0)
INSERT INTO Tarjetas(NroTarjeta, FechaVencimiento, Saldo, Pin, Reintentos, Bloqueo) VALUES ('9999999999999999', '2023-12-31', 10000, '9999', 0, 1)
