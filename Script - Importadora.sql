-- Creación de la base de datos
CREATE DATABASE importadora;

-- Selección de la base de datos
USE importadora;

-- Tabla de vehículos
CREATE TABLE vehiculos (
  id INT PRIMARY KEY AUTO_INCREMENT,
  marca VARCHAR(50) NOT NULL,
  modelo VARCHAR(50) NOT NULL,
  anio INT NOT NULL,
  precio DECIMAL(10, 2) NOT NULL,
  cantidad INT NULL
);

-- Tabla de clientes
CREATE TABLE clientes (
  id INT PRIMARY KEY AUTO_INCREMENT,
  nombre VARCHAR(50) NOT NULL,
  apellido VARCHAR(50) NOT NULL,
  correo VARCHAR(50) NOT NULL,
  `password` VARCHAR(50) NOT NULL,
  direccion VARCHAR(100) NOT NULL,
  ciudad VARCHAR(50) NULL,
  estado VARCHAR(50) NULL,
  codigo_postal VARCHAR(10) NULL,
  telefono VARCHAR(20) NULL
);

-- Tabla de compras
CREATE TABLE compras (
  id INT PRIMARY KEY AUTO_INCREMENT,
  cliente_id INT NOT NULL,
  vehiculo_id INT NOT NULL,
  fecha_compra DATE NOT NULL,
  cantidad INT NULL,
  precio_total DECIMAL(10, 2) NOT NULL,
  FOREIGN KEY (cliente_id) REFERENCES clientes(id),
  FOREIGN KEY (vehiculo_id) REFERENCES vehiculos(id)
);