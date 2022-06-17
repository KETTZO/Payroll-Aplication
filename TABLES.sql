IF NOT EXISTS (SELECT name FROM sysdatabases WHERE (name = 'BD_Sistema_Nómina')) 
BEGIN
	create database BD_Sistema_Nómina;
END
Go

use BD_Sistema_Nómina;
Go

IF OBJECT_ID('Empresa_Empleado') IS NOT NULL
DROP TABLE Empresa_Empleado;

IF OBJECT_ID('Empresa_Departamento') IS NOT NULL
DROP TABLE Empresa_Departamento;

IF OBJECT_ID('Puesto_Empleado') IS NOT NULL
DROP TABLE Puesto_Empleado;

IF OBJECT_ID('Percepcion_Deduccion_Empleado') IS NOT NULL
DROP TABLE Percepcion_Deduccion_Empleado;

IF OBJECT_ID('Puesto_Departamento') IS NOT NULL
DROP TABLE Puesto_Departamento;

IF OBJECT_ID('Nomina') IS NOT NULL
DROP TABLE Nomina;

IF OBJECT_ID('Gerente') IS NOT NULL
DROP TABLE Gerente;

IF OBJECT_ID('Percepcion_Deduccion') IS NOT NULL
DROP TABLE Percepcion_Deduccion;

IF OBJECT_ID('Puesto') IS NOT NULL
DROP TABLE Puesto;

IF OBJECT_ID('Departamento') IS NOT NULL
DROP TABLE Departamento;

IF OBJECT_ID('Empleado') IS NOT NULL
DROP TABLE Empleado;

IF OBJECT_ID('Empresa') IS NOT NULL
DROP TABLE Empresa;

IF OBJECT_ID('Domicilio') IS NOT NULL
DROP TABLE Domicilio;

CREATE TABLE Domicilio(
	IDDireccion INT UNIQUE DEFAULT ABS(CHECKSUM(NEWID())) % 9999999,
	PaisResd VARCHAR(20) NOT NULL,
	Estado VARCHAR(20) NOT NULL,
	Muncipio VARCHAR(30) NOT NULL, 
	Colonia VARCHAR(30) NOT NULL,
	Calle VARCHAR(30) NOT NULL,
	NumDomicilio SMALLINT NOT NULL,
	CP INT NOT NULL,
	CONSTRAINT PK_Domi
		PRIMARY KEY(IDDireccion)
);

CREATE TABLE Empresa(
	DUNS VARCHAR(9) UNIQUE NOT NULL,
	RazSocial VARCHAR(40) NOT NULL,
	DomFiscal INT NOT NULL,
	Email VARCHAR(40) NOT NULL,
	TelEmpr1 VARCHAR(20) NOT NULL,
	TelEmpr2 VARCHAR(20),
	RegPatronal VARCHAR(20) NOT NULL,
	RFC VARCHAR(12) NOT NULL UNIQUE,
	FechInicioOp DATE NOT NULL,
	CONSTRAINT PK_Empr
		PRIMARY KEY(DUNS),
	CONSTRAINT FK_Empr
		FOREIGN KEY(DomFiscal)
		REFERENCES Domicilio(IDDireccion)
);

INSERT INTO Domicilio(PaisResd, Estado, Muncipio, Colonia, Calle, NumDomicilio, CP)
                VALUES('México', 'Nuevo León', 'Escobedo', 'Gobernadores', 'Guillermino Lozano', 102, 45454);

INSERT INTO Empresa(DUNS, RazSocial, DomFiscal, Email, TelEmpr1, TelEmpr2, RegPatronal, RFC, FechInicioOp)
             VALUES('123412341', 'Ferreteria Gonzalos', (SELECT TOP 1 IDDireccion FROM Domicilio), 'tilines_contacto@gmail.com', '12121212', '34343434', '00000001234', 'RDL0904102F4', '220101');

CREATE TABLE Empleado(
	NumEmpleado INT UNIQUE DEFAULT ABS(CHECKSUM(NEWID())) % 9999999,
	Nombre VARCHAR(50) NOT NULL,
	ApPaterno VARCHAR(25) NOT NULL,
	ApMaterno VARCHAR(25) NOT NULL,
	Contraseña VARCHAR(40) NOT NULL,
	FechNacim DATE NOT NULL,
	CURP VARCHAR(18) NOT NULL UNIQUE,
	NSS VARCHAR(11) NOT NULL UNIQUE,
	RFC VARCHAR(13) NOT NULL UNIQUE,
	DomEmpl INT NOT NULL,
	Banco VARCHAR(30) NOT NULL,
	NumCuenta VARCHAR(10) NOT NULL,
	Email VARCHAR(40) NOT NULL,
	TelCasa VARCHAR(20),
	TelCel VARCHAR(20),
	FechIngrEmpr DATE,
	Estatus BIT,
	CONSTRAINT PK_Empl
		PRIMARY KEY(NumEmpleado),
	CONSTRAINT FK_Empl
		FOREIGN KEY(DomEmpl)
		REFERENCES Domicilio(IDDireccion)
);

CREATE TABLE Departamento(
	NumDepart INT UNIQUE DEFAULT ABS(CHECKSUM(NEWID())) % 99999,
	NomDepart VARCHAR (40) UNIQUE NOT NULL,
	SdBase MONEY NOT NULL,
	Estatus BIT,
	CONSTRAINT PK_Depar
		PRIMARY KEY(NumDepart)
);

CREATE TABLE Puesto(
	NumPuesto INT UNIQUE DEFAULT ABS(CHECKSUM(NEWID())) % 99999,
	NomPuesto VARCHAR(40) NOT NULL,
	Proporcion NUMERIC(3,2) NOT NULL,--
	Salario MONEY,
	Estatus BIT,
	CONSTRAINT PK_Pues
		PRIMARY KEY(NumPuesto)
);

CREATE TABLE Gerente(
	IDGerente INT UNIQUE DEFAULT ABS(CHECKSUM(NEWID())) % 999999,
	GerenteEmpl INT NOT NULL,
	CONSTRAINT PK_Gerente
		PRIMARY KEY(IDGerente),
	CONSTRAINT FK_Gere
		FOREIGN KEY(GerenteEmpl)
		REFERENCES Empleado(NumEmpleado)
);

CREATE TABLE Empresa_Empleado(
	Empresa VARCHAR(9) NOT NULL,
	Empleado INT NOT NULL,
	CONSTRAINT PK_EE
		PRIMARY KEY(Empresa,Empleado),
	CONSTRAINT FK_EmplEE
		FOREIGN KEY(Empleado)
		REFERENCES Empleado(NumEmpleado),
	CONSTRAINT FK_EmprEE
		FOREIGN KEY(Empresa)
		REFERENCES Empresa(DUNS)
);

CREATE TABLE Empresa_Departamento(
	Empresa VARCHAR(9) NOT NULL,
	Departamento INT NOT NULL,
	CONSTRAINT PK_ED
		PRIMARY KEY(Empresa,Departamento),
	CONSTRAINT FK_DepartED
		FOREIGN KEY(Departamento)
		REFERENCES Departamento(NumDepart),
	CONSTRAINT FK_EmprED
		FOREIGN KEY(Empresa)
		REFERENCES Empresa(DUNS)
);

CREATE TABLE Puesto_Departamento(
	Departamento INT NOT NULL,
	Puesto INT NOT NULL,
	CONSTRAINT PK_PD
		PRIMARY KEY(Puesto, Departamento),
	CONSTRAINT FK_PuestoPD
		FOREIGN KEY(Puesto)
		REFERENCES Puesto(NumPuesto),
	CONSTRAINT FK_DepaPD
		FOREIGN KEY(Departamento)
		REFERENCES Departamento(NumDepart)
);

CREATE TABLE Puesto_Empleado(
	Empleado INT NOT NULL,
	Puesto INT NOT NULL,
	Estatus BIT DEFAULT 1,
	CONSTRAINT PK_PE
		PRIMARY KEY(Puesto, Empleado),
	CONSTRAINT FK_EmplPE
		FOREIGN KEY(Puesto)
		REFERENCES Puesto(NumPuesto),
	CONSTRAINT FK_PuesPE
		FOREIGN KEY(Empleado)
		REFERENCES Empleado(NumEmpleado)
);

CREATE TABLE Nomina(
	IDNomina UNIQUEIDENTIFIER DEFAULT NEWID(),
	DiaTrab SMALLINT NOT NULL,
	EmplNomina INT NOT NULL,
	SdDia MONEY NOT NULL,
	SdBruto MONEY NOT NULL,
	sdNeto MONEY NOT NULL,
	SdNetoLetra VARCHAR(200),
	FechPag DATE NOT NULL,
	FechNom DATETIME DEFAULT GETDATE(),
	FechFinPdPago DATE NOT NULL,
	FechIniPdPago DATE NOT NULL,
	CONSTRAINT PK_Nomina
		PRIMARY KEY(IDNomina),
	CONSTRAINT FK_Nom_Emplnom
		FOREIGN KEY(EmplNomina)
		REFERENCES Empleado(NumEmpleado)
);

CREATE TABLE Percepcion_Deduccion(
	Codigo INT UNIQUE DEFAULT ABS(CHECKSUM(NEWID())) % 999999,
	Nombre VARCHAR(35) NOT NULL,
	Tipo CHAR(1) NOT NULL,
	Cantidad NUMERIC(7,2) NOT NULL,
	TipoCan  CHAR(1) NOT NULL,
	Estatus BIT,
	FechaPD Date,
	CONSTRAINT PK_PerDed
		PRIMARY KEY(Codigo),
	CONSTRAINT CK_Tipo
		CHECK (Tipo = 'P' OR Tipo = 'D'),
	CONSTRAINT CK_TipoCan
		CHECK (TipoCan = 'F' OR TipoCan = 'P')
);

CREATE TABLE Percepcion_Deduccion_Empleado(
	Codigo INT DEFAULT ABS(CHECKSUM(NEWID())) % 999999,
	Empleado INT NOT NULL,
	FechaPDE Date,
	Resultado MONEY NOT NULL,
	CONSTRAINT PK_PDE
		PRIMARY KEY(Codigo, Empleado),
	CONSTRAINT FK_EmplPDE
		FOREIGN KEY(Empleado)
		REFERENCES Empleado(NumEmpleado),
	CONSTRAINT FK_CodPDE
		FOREIGN KEY(Codigo)
		REFERENCES Percepcion_Deduccion(Codigo)
);



