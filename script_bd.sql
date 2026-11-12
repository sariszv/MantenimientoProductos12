CREATE DATABASE db_productos;
GO
USE db_productos;
GO

CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(200)
);

CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Codigo NVARCHAR(20) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    CategoriaId INT NOT NULL FOREIGN KEY REFERENCES Categorias(Id),
    Precio DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL
);

CREATE TABLE Proveedores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nit NVARCHAR(30) NOT NULL,
    RazonSocial NVARCHAR(120) NOT NULL,
    Telefono NVARCHAR(30)
);

INSERT INTO Categorias (Nombre, Descripcion) VALUES 
('Electrónicos', 'Dispositivos y equipos de cómputo'),
('Accesorios', 'Periféricos y complementos');

INSERT INTO Productos (Codigo, Nombre, CategoriaId, Precio, Stock) VALUES 
('P001', 'Laptop Dell', 1, 1200.00, 15),
('P002', 'Mouse Inalámbrico', 2, 25.50, 50);

INSERT INTO Proveedores (Nit, RazonSocial, Telefono) VALUES 
('900123456-1', 'Distribuidora Tech S.A.S', '6044445566'),
('800987654-2', 'Importadora Global S.A.', '6043332211');
