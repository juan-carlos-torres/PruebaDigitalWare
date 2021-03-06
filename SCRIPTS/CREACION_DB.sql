------------------------ CREACIÓN TABLAS ------------------------

CREATE TABLE  pais (
	 pai_id_pais   uniqueidentifier  PRIMARY KEY,
	 pai_nombre   varchar (200) NOT NULL,
	 pai_activo   bit  NOT NULL,
)

CREATE TABLE  departamento (
	 dep_id_departamento   uniqueidentifier PRIMARY KEY,
	 dep_id_pais   uniqueidentifier  NOT NULL,
	 dep_nombre   varchar (200) NOT NULL,
	 dep_activo   bit  NOT NULL,
	 CONSTRAINT  FK_departamento__pais  FOREIGN KEY( dep_id_pais )
		REFERENCES  pais  ( pai_id_pais )
)

CREATE TABLE ciudad (
	 ciu_id_ciudad  uniqueidentifier  PRIMARY KEY,
	 ciu_id_departamento   uniqueidentifier  NOT NULL,
	 ciu_nombre   varchar (200) NOT NULL,
	 ciu_activo   bit  NOT NULL,
	 CONSTRAINT  FK_ciudad__departamento  FOREIGN KEY( ciu_id_departamento )
	 REFERENCES  departamento  (dep_id_departamento)
)

CREATE TABLE  tipo_identificacion (
	 tid_id_tipo_identificacion   uniqueidentifier  PRIMARY KEY,
	 tid_descripcion   varchar (200) NOT NULL,
	 tid_activo   bit  NOT NULL,
)

CREATE TABLE  cliente (
	 cli_id_cliente   uniqueidentifier  PRIMARY KEY,
	 cli_nombres   varchar (200) NOT NULL,
	 cli_apellidos   varchar (200) NOT NULL,
	 cli_id_tipo_identificacion   uniqueidentifier  NOT NULL,
	 cli_identificacion   varchar (20) NOT NULL,
	 cli_id_ciudad_nacimiento   uniqueidentifier  NOT NULL,
	 cli_fecha_nacimiento   datetime  NOT NULL,
	 cli_id_ciudad_residencia   uniqueidentifier  NULL,
	 cli_direccion   varchar (200) NULL,
	 cli_fecha_creacion   datetime  NOT NULL,
	 cli_activo   bit  NOT NULL,
	 CONSTRAINT  FK_cliente__ciudad__nacimiento  FOREIGN KEY( cli_id_ciudad_nacimiento )
		REFERENCES ciudad  ( ciu_id_ciudad ),
	 CONSTRAINT  FK_cliente__ciudad__residencia  FOREIGN KEY( cli_id_ciudad_residencia )
		REFERENCES  ciudad ( ciu_id_ciudad ),
	 CONSTRAINT  FK_cliente__tipo_identificacion  FOREIGN KEY( cli_id_tipo_identificacion )
		REFERENCES  tipo_identificacion  ( tid_id_tipo_identificacion )
)


CREATE TABLE  tipo_correo (
	 tco_id_tipo_correo   uniqueidentifier  PRIMARY KEY,
	 tco_descripcion   varchar (200) NOT NULL,
	 tco_activo   bit  NOT NULL,
)

CREATE TABLE  cliente_correo (
	 cco_id_cliente   uniqueidentifier,
	 cco_id_tipo_correo   uniqueidentifier,
	 cco_correo   varchar (100) NOT NULL,
	 cco_activo   bit  NOT NULL,
	 PRIMARY KEY (cco_id_cliente, cco_id_tipo_correo),
	 CONSTRAINT  FK_cliente_correo__cliente  FOREIGN KEY( cco_id_cliente )
		REFERENCES  cliente  ( cli_id_cliente ),
	 CONSTRAINT  FK_cliente_correo__tipo_correo  FOREIGN KEY( cco_id_tipo_correo )
		REFERENCES  tipo_correo  ( tco_id_tipo_correo )
)


CREATE TABLE  tipo_telefono (
	 tte_id_tipo_telefono   uniqueidentifier  PRIMARY KEY,
	 tte_descripcion   varchar (200) NOT NULL,
	 tte_activo   bit  NOT NULL,
)

CREATE TABLE  cliente_telefono (
	 cte_id_cliente   uniqueidentifier,
	 cte_id_tipo_telefono   uniqueidentifier,
	 cte_telefono   varchar (50) NOT NULL,
	 PRIMARY KEY(cte_id_cliente, cte_id_tipo_telefono),
	 CONSTRAINT  FK_cliente_telefono__cliente  FOREIGN KEY( cte_id_cliente )
		REFERENCES  cliente  ( cli_id_cliente ),
	 CONSTRAINT  FK_cliente_telefono__tipo_telefono  FOREIGN KEY( cte_id_tipo_telefono )
		REFERENCES  tipo_telefono  (tte_id_tipo_telefono)
)

CREATE TABLE  factura (
	 fac_id_factura   uniqueidentifier PRIMARY KEY,
	 fac_id_cliente   uniqueidentifier  NOT NULL,
	 fac_fecha_registro   datetime  NOT NULL,
	 fac_valor_total   numeric (18, 2) NOT NULL,
	 CONSTRAINT  FK_factura__cliente  FOREIGN KEY( fac_id_cliente )
		REFERENCES  cliente  ( cli_id_cliente )
)

CREATE TABLE  producto (
	 pro_id_producto   uniqueidentifier  PRIMARY KEY,
	 pro_nombre   varchar (200) NOT NULL,
	 pro_descripcion   varchar (500) NOT NULL,
	 pro_precio   numeric (18, 2) NOT NULL,
	 pro_fecha_creacion   datetime  NOT NULL,
	 pro_activo   bit  NOT NULL
)

CREATE TABLE  factura_producto (
	 fpr_id_factura   uniqueidentifier,
	 fpr_id_producto   uniqueidentifier,
	 fpr_cantidad   int  NOT NULL,
	 fpr_valor   numeric (18, 2) NOT NULL,
	 PRIMARY KEY (fpr_id_factura, fpr_id_producto),
	 CONSTRAINT  FK_factura_producto__factura  FOREIGN KEY( fpr_id_factura )
		REFERENCES  factura( fac_id_factura ),
	 CONSTRAINT  FK_factura_producto__producto  FOREIGN KEY( fpr_id_producto )
		REFERENCES  producto( pro_id_producto)
)

CREATE TABLE inventario (
	 inv_id_inventario   uniqueidentifier  PRIMARY KEY,
	 inv_id_producto   uniqueidentifier  NOT NULL,
	 inv_cantidad   int  NOT NULL,
	 CONSTRAINT  FK_inventario__producto  FOREIGN KEY( inv_id_producto )
		REFERENCES  producto  ( pro_id_producto )
)

------------------------ FIN CREACIÓN TABLAS ------------------------



------------------------ DATOS DE PRUEBA ------------------------

------------------------ TIPOS IDENTIFICACIÓN ------------------------

INSERT INTO tipo_identificacion 
VALUES (NEWID(), 'Cédula de ciudadanía', 1)

------------------------ FIN TIPOS IDENTIFICACIÓN ------------------------


------------------------ PAÍS ------------------------

INSERT INTO pais
VALUES (NEWID(), 'Colombia', 1)

------------------------ FIN PAÍS ------------------------


------------------------ DEPARTAMENTO ------------------------

INSERT INTO departamento 
VALUES (NEWID(), 'ebd62ff9-246f-4686-a825-59bf75507abe', 'Cundinamarca', 1)

------------------------ FIN DEPARTAMENTO ------------------------


------------------------ CIUDAD ------------------------

INSERT INTO ciudad 
VALUES (NEWID(), '63da9fdc-0904-434e-baba-56604d955371', 'Bogotá', 1)

------------------------ FIN CIUDAD ------------------------


------------------------ CLIENTES ------------------------

INSERT INTO cliente 
VALUES (NEWID(), 'José', 'Lopez', '7c034ebe-131e-4321-b6c0-d3bfddd19f31', 
		'1002946242', '5de50ac4-3d86-4ed7-98e2-16d29a370bb1', 
		'1980-03-25 21:54:41.600', '5de50ac4-3d86-4ed7-98e2-16d29a370bb1', NULL, 
		GETDATE(), 1)

INSERT INTO cliente   
VALUES (NEWID(), 'Juan Carlos', 'Torres Torres', 
		'7c034ebe-131e-4321-b6c0-d3bfddd19f31', '1001053192', 
		'5de50ac4-3d86-4ed7-98e2-16d29a370bb1', '1998-03-21 21:50:46.110', 
		'5de50ac4-3d86-4ed7-98e2-16d29a370bb1', 'Calle 41 dbis # 81 k 17 sur', 
		 GETDATE(), 1)

INSERT INTO cliente  
VALUES (NEWID(), 'Luisa', 'Martinez', 
		'7c034ebe-131e-4321-b6c0-d3bfddd19f31', '8048574224', 
		'5de50ac4-3d86-4ed7-98e2-16d29a370bb1', '1990-03-23 21:53:48.637', 
		'5de50ac4-3d86-4ed7-98e2-16d29a370bb1', 'carrera 7 # 17', 
		GETDATE(), 1)

INSERT INTO cliente
VALUES (NEWID(), 'Juliana', 'Sanchez', 
		'7c034ebe-131e-4321-b6c0-d3bfddd19f31', '1048532543', 
		'5de50ac4-3d86-4ed7-98e2-16d29a370bb1', '1955-04-01 21:56:44.010', 
		'5de50ac4-3d86-4ed7-98e2-16d29a370bb1', NULL, 
		GETDATE(), 1)

------------------------ FIN CLIENTES ------------------------


------------------------ FACTURAS ------------------------

INSERT INTO factura 
VALUES (NEWID(), '0ffbb9f3-2188-4d85-b9b3-5103771a5fd2', 
'2001-03-12 00:00:00.000', 10000000)

INSERT INTO factura 
VALUES (NEWID(), '0ffbb9f3-2188-4d85-b9b3-5103771a5fd2', 
'2000-04-20 00:00:00.000', 2000000)

INSERT INTO factura 
VALUES (NEWID(), '53104cda-1598-4654-a1c9-688716a709e7', 
'2000-04-10 00:00:00.000', 9000000)

INSERT INTO factura 
VALUES (NEWID(), 'b5e7c0d0-fbcc-4431-9db6-0e9fb3e26c5b', 
'2000-02-12 00:00:00.000', 20000000)

INSERT INTO factura
VALUES (NEWID(), 'b5e7c0d0-fbcc-4431-9db6-0e9fb3e26c5b', 
'2014-08-20 00:00:00.000', 7000000)

------------------------ FIN FACTURAS ------------------------


------------------------ PRODUCTOS ------------------------

INSERT INTO producto
VALUES (NEWID(), 'computador', 'lenovo idepad 320', 1700000, 
GETDATE(), 1)

INSERT INTO producto
VALUES (NEWID(), 'Cámara', 'Camara profesional ', 
2500000, GETDATE(), 1)

INSERT INTO producto  
VALUES (NEWID(), 'pantalla led', 'pantalla de 20 pulgadas', 
200000, GETDATE(), 1)

INSERT INTO producto 
VALUES (NEWID(), 'Celular', 'samsung galaxy s20', 
3000000, GETDATE(), 1)

INSERT INTO producto  
VALUES (NEWID(), 'Disco duro', 'Capacidad 2 T', 
500000, GETDATE(), 1)

------------------------ FIN PRODUCTOS ------------------------


------------------------ FACTURAS PRODUCTOS ------------------------

INSERT INTO factura_producto 
VALUES (NEWID(), '14cbc960-0217-47cb-ab8e-4833bb5b75f0', 10, 
1500000)

INSERT INTO factura_producto
VALUES (NEWID(), '100e0bb7-1e77-4f72-b189-d616791e2a59', 5, 
1000000)

------------------------ FIN FACTURAS PRODUCTOS ------------------------


------------------------ INVENTARIOS ------------------------

INSERT INTO inventario 
VALUES (NEWID(), 'e30c1fb9-9cd1-4ad0-be3a-9772ab7b3334', 5)

INSERT INTO inventario 
VALUES (NEWID(), '14cbc960-0217-47cb-ab8e-4833bb5b75f0', 10)

INSERT INTO inventario
VALUES (NEWID(), '9f2286f5-58c4-4746-a25e-d7bebe678dd8', 5)

INSERT INTO inventario 
VALUES (NEWID(), '2d8de198-5e0e-492f-876e-d78343379d53', 5)

INSERT INTO inventario  
VALUES (NEWID(), '100e0bb7-1e77-4f72-b189-d616791e2a59', 100)

------------------------ FIN INVENTARIOS ------------------------


------------------------ FIN DATOS DE PRUEBA ------------------------