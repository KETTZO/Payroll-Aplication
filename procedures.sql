use BD_Sistema_Nómina;
Go

IF OBJECT_ID('sp_GestionEmpresa') IS NOT NULL
DROP PROCEDURE sp_GestionEmpresa;
Go

IF OBJECT_ID('sp_GestionEmpleados') IS NOT NULL
DROP PROCEDURE sp_GestionEmpleados;
Go

IF OBJECT_ID('sp_GestionDepartamento') IS NOT NULL
DROP PROCEDURE sp_GestionDepartamento;
Go

IF OBJECT_ID('sp_GestionPuesto') IS NOT NULL
DROP PROCEDURE sp_GestionPuesto;
Go

IF OBJECT_ID('sp_AsignarPuesto_Empleado') IS NOT NULL
DROP PROCEDURE sp_AsignarPuesto_Empleado;
Go

IF OBJECT_ID('sp_GestionPercepcion_Deduccion') IS NOT NULL
DROP PROCEDURE sp_GestionPercepcion_Deduccion;
Go

IF OBJECT_ID('sp_ReciboNomina') IS NOT NULL
DROP PROCEDURE sp_ReciboNomina;
Go

IF OBJECT_ID('dbo.f_CalcularSMB') IS NOT NULL
DROP FUNCTION dbo.f_CalcularSMB;
Go

IF OBJECT_ID('dbo.f_SueldoMensualNeto') IS NOT NULL
DROP FUNCTION dbo.f_SueldoMensualNeto;
Go

IF OBJECT_ID('Recibos_Creados') IS NOT NULL
DROP VIEW Recibos_Creados;
Go

IF OBJECT_ID('Ingresar') IS NOT NULL
DROP VIEW Ingresar;
Go

--IF OBJECT_ID('ConsultarRecibo') IS NOT NULL
--DROP VIEW ConsultarRecibo;
--Go

IF OBJECT_ID('[dbo].[CantidadConLetra]') IS NOT NULL
DROP FUNCTION [dbo].[CantidadConLetra];
Go

IF OBJECT_ID('ReporteGeneralNomina') IS NOT NULL
DROP VIEW ReporteGeneralNomina;
Go

IF OBJECT_ID('sp_Reportes') IS NOT NULL
DROP PROCEDURE sp_Reportes;
Go

CREATE PROCEDURE sp_GestionEmpresa
(
	@Accion				TINYINT,	--1modif, 2consultar
	@RazSocial          VARCHAR(40)     = NULL,
	@Email              VARCHAR(40)     = NULL,
	@TelEmpr1           VARCHAR(20)     = NULL,
	@TelEmpr2           VARCHAR(20)     = NULL,
	@RegPatronal        VARCHAR(20)     = NULL,
	@RFC                VARCHAR(12)     = NULL,
	@Pais				VARCHAR(20)		= NULL,
	@Estado				VARCHAR(20)		= NULL,
	@Municipio			VARCHAR(30)		= NULL,
	@Colonia			VARCHAR(30)		= NULL,
	@Calle				VARCHAR(30)		= NULL,
	@NumDomicilio		SMALLINT		= NULL,
	@CP					INT				= NULL
)
AS
BEGIN
	IF @Accion = 1
	BEGIN
		IF @RazSocial = '' BEGIN SET @RazSocial = NULL END
		IF @Email = '' BEGIN SET @Email = NULL END
		IF @TelEmpr1 = '' BEGIN SET @TelEmpr1 = NULL END
		IF @TelEmpr2 = '' BEGIN SET @TelEmpr2 = NULL END
		IF @RegPatronal = '' BEGIN SET @RegPatronal = NULL END
		IF @RFC = '' BEGIN SET @RFC = NULL END				

		UPDATE Empresa
			SET RazSocial = ISNULL(@RazSocial, RazSocial),
				Email = ISNULL(@Email, Email),
				TelEmpr1 = ISNULL(@TelEmpr1, TelEmpr1),
				TelEmpr2 = ISNULL(@TelEmpr2, TelEmpr2),
				RegPatronal = ISNULL(@RegPatronal, RegPatronal),
				RFC = ISNULL(@RFC, RFC)

		IF @Municipio = '' BEGIN SET @Municipio = NULL END	IF @Pais = '' BEGIN SET @Pais = NULL END
		IF @Estado = '' BEGIN SET @Estado = NULL END		IF @Colonia = '' BEGIN SET @Colonia = NULL END
		IF @Calle = '' BEGIN SET @Calle = NULL END			IF @NumDomicilio = 0 BEGIN SET @NumDomicilio = NULL END
		IF @CP = 0 BEGIN SET @CP = NULL END

		UPDATE Domicilio
		SET Muncipio = ISNULL(@Municipio, Muncipio),
			PaisResd = ISNULL(@Pais, PaisResd),
			Estado = ISNULL(@Estado, Estado),
			Colonia = ISNULL(@Colonia, Colonia),
			Calle = ISNULL(@Calle, Calle),
			NumDomicilio = ISNULL(@NumDomicilio, NumDomicilio),
			CP = ISNULL(@CP, CP)
	END

	IF @Accion = 2
	BEGIN
		SELECT E.DUNS,
	           E.RazSocial,
	           E.Email,
	           E.TelEmpr1,
	           E.TelEmpr2,
	           E.RegPatronal,
	           E.RFC,
	           E.FechInicioOp,
			   D.PaisResd,
			   D.Estado,
	           D.Muncipio, 
	           D.Colonia,
			   D.Calle,
			   D.NumDomicilio,
	           D.CP
		FROM Empresa E
		JOIN Domicilio D
		    ON E.DomFiscal = D.IDDireccion
	END

END
Go

CREATE PROCEDURE sp_GestionEmpleados
(
	@Accion				TINYINT,	--1add, 2modif, 3deshabilitar
									--4consultar uno, 5consultar todos 
	@NumEmpleado		INT				= NULL,
	@RFC				VARCHAR(13)		= NULL,
	@Nombre				VARCHAR(50)		= NULL,
	@ApPaterno			VARCHAR(25)		= NULL,
	@ApMaterno			VARCHAR(25)		= NULL,
	@Email				VARCHAR(40)		= NULL,
	@CURP				VARCHAR(18)		= NULL,
	@Nacimiento			DATE			= NULL,
	@TelCasa			VARCHAR(20)		= NULL,
	@TelCel				VARCHAR(20)		= NULL,
	@Contraseña			VARCHAR(40)		= NULL,
	@NSS				VARCHAR(11)		= NULL,
	@Banco				VARCHAR(30)		= NULL,
	@NumCuenta			VARCHAR(10)		= NULL,
	@Pais				VARCHAR(20)		= NULL,
	@Estado				VARCHAR(20)		= NULL,
	@Municipio			VARCHAR(30)		= NULL,
	@Colonia			VARCHAR(30)		= NULL,
	@Calle				VARCHAR(30)		= NULL,
	@NumDomicilio		SMALLINT		= NULL,
	@CP					INT				= NULL,
	@FechIngrEmpr       DATE            = NULL,
	@Puesto             INT             = NULL
)
AS
BEGIN
	IF @Accion = 1
	BEGIN
		
		DECLARE @IdDire	INT
		SET @IdDire = ABS(CHECKSUM(NEWID())) % 9999999

		INSERT INTO Domicilio(IDDireccion, PaisResd, Estado, Muncipio, Colonia, Calle, NumDomicilio, CP)
		VALUES(@IdDire, @Pais, @Estado, @Municipio, @Colonia, @Calle, @NumDomicilio, @CP);

		INSERT INTO Empleado(RFC, Nombre, ApPaterno, ApMaterno, Email, CURP, FechNacim, TelCasa, TelCel, DomEmpl, Contraseña, NSS, Banco, NumCuenta, Estatus, FechIngrEmpr)
		VALUES(@RFC, @Nombre, @ApPaterno, @ApMaterno, @Email, @CURP, @Nacimiento, @TelCasa, @TelCel, @IdDire, @Contraseña, @NSS, @Banco, @NumCuenta, 1, @FechIngrEmpr);

		INSERT INTO Empresa_Empleado(Empresa, Empleado)
		VALUES((SELECT TOP 1 DUNS FROM Empresa), (SELECT NumEmpleado FROM Empleado WHERE CURP = @CURP));

		INSERT INTO Puesto_Empleado(Empleado, Puesto)
		VALUES((SELECT NumEmpleado FROM Empleado WHERE CURP = @CURP), @Puesto);
	END
	
	IF @Accion = 2
	BEGIN
		IF @Nacimiento = '' BEGIN SET @Nacimiento = NULL END	IF @Email = '' BEGIN SET @Email = NULL END
		IF @TelCasa = '' BEGIN SET @TelCasa = NULL END			IF @TelCel = '' BEGIN SET @TelCel = NULL END
		IF @Email = '' BEGIN SET @Email = NULL END				IF @CURP = '' BEGIN SET @CURP = NULL END
		IF @RFC = '' BEGIN SET @RFC = NULL END					IF @Contraseña = '' BEGIN SET @Contraseña = NULL END
		IF @Banco = '' BEGIN SET @Banco = NULL END				IF @NumCuenta = '' BEGIN SET @NumCuenta = NULL END	

		UPDATE Empleado
			SET FechNacim = ISNULL(@Nacimiento, FechNacim),
				Email = ISNULL(@Email, Email),
				TelCasa = ISNULL(@TelCasa, TelCasa),
				TelCel = ISNULL(@TelCel, TelCel),
				CURP = ISNULL(@CURP, CURP),
				RFC = ISNULL(@RFC, RFC),
				Contraseña = ISNULL(@Contraseña, Contraseña),
				Banco = ISNULL(@Banco, Banco),
				NumCuenta = ISNULL(@NumCuenta, NumCuenta),
				@IdDire = DomEmpl
			WHERE NumEmpleado = @NumEmpleado

		IF @Municipio = '' BEGIN SET @Municipio = NULL END	IF @Pais = '' BEGIN SET @Pais = NULL END
		IF @Estado = '' BEGIN SET @Estado = NULL END		IF @Colonia = '' BEGIN SET @Colonia = NULL END
		IF @Calle = '' BEGIN SET @Calle = NULL END			IF @NumDomicilio = 0 BEGIN SET @NumDomicilio = NULL END
		IF @CP = 0 BEGIN SET @CP = NULL END

		UPDATE Domicilio
		SET Muncipio = ISNULL(@Municipio, Muncipio),
			PaisResd = ISNULL(@Pais, PaisResd),
			Estado = ISNULL(@Estado, Estado),
			Colonia = ISNULL(@Colonia, Colonia),
			Calle = ISNULL(@Calle, Calle),
			NumDomicilio = ISNULL(@NumDomicilio, NumDomicilio),
			CP = ISNULL(@CP, CP)
		WHERE IDDireccion = @IdDire

		IF @Puesto = 0 BEGIN SET @Puesto = NULL END

		UPDATE Puesto_Empleado
			SET Puesto = ISNULL(@Puesto, Puesto)
			WHERE Empleado = @NumEmpleado
	END
	
	IF @Accion = 3
	BEGIN
			UPDATE Empleado
			SET Estatus = 0
			WHERE NumEmpleado = @NumEmpleado

			UPDATE Puesto_Empleado
			SET Estatus = 0
			WHERE Empleado = @NumEmpleado
	END

	IF @Accion = 4
	BEGIN
		SELECT E.NumEmpleado,
	           CONCAT (E.Nombre, ' ', E.ApPaterno, ' ', ApMaterno),
	           E.FechIngrEmpr,
			   E.Contraseña,
	           E.FechNacim,
	           E.CURP,
	           E.NSS,
	           E.RFC,
	           E.Banco,
	           E.NumCuenta,
	           E.Email,
	           ISNULL(E.TelCasa, 'Sin teléfono'),
	           ISNULL(E.TelCel, 'Sin teléfono'),
			   D.PaisResd,
			   D.Estado,
	           D.Muncipio, 
	           D.Colonia,
			   CONCAT (D.Calle, ' #',D.NumDomicilio),
	           D.CP
		FROM Empleado E
		JOIN Domicilio D
		    ON E.DomEmpl = D.IDDireccion
		WHERE E.NumEmpleado = @NumEmpleado AND E.Estatus = 1;
	END

	IF @Accion = 5
	BEGIN
		--SELECT NumEmpleado, Nombre, ApPaterno, ApMaterno, Contraseña, FechNacim, CURP, 
		--NSS, RFC, Banco, NumCuenta, Email, TelCasa, TelCel, FechIngrEmpr 
		--FROM Empleado WHERE Estatus = 1
		SELECT E.NumEmpleado,
	           E.Nombre, 
			   E.ApPaterno,
			   ApMaterno,
	           E.FechIngrEmpr,
			   E.Contraseña,
	           E.FechNacim,
	           E.CURP,
	           E.NSS,
	           E.RFC,
	           E.Banco,
	           E.NumCuenta,
	           E.Email,
	           E.TelCasa,
	           E.TelCel,
			   D.PaisResd,
			   D.Estado,
	           D.Muncipio, 
	           D.Colonia,
			   D.Calle, 
			   D.NumDomicilio,
	           D.CP
			FROM Empleado E
		        JOIN Domicilio D
		    ON E.DomEmpl = D.IDDireccion
		WHERE E.Estatus = 1;
	END

END
Go

CREATE PROCEDURE sp_GestionDepartamento
(
	@Accion				TINYINT,	--1add, 2modif, 3deshabilitar,
									--4consultar uno, 5consultar todos 
	@NumDepart          INT             =NULL,
	@NomDepart          VARCHAR(40)     =NULL,
	@SdBase             MONEY           =NULL
)
AS
BEGIN
	IF @Accion = 1
	BEGIN
		INSERT INTO Departamento(NomDepart, SdBase, Estatus)
		VALUES(@NomDepart, @SdBase, 1);

		INSERT INTO Empresa_Departamento(Empresa, Departamento)
		VALUES((SELECT TOP 1 DUNS FROM Empresa), (SELECT NumDepart FROM Departamento WHERE NomDepart = @NomDepart AND SdBase = @SdBase));
	END
	
	IF @Accion = 2
	BEGIN
		IF @NomDepart = '' BEGIN SET @NomDepart = NULL END
		IF @SdBase = 0 BEGIN SET @SdBase = NULL END	

		UPDATE Departamento
			SET NomDepart = ISNULL(@NomDepart, NomDepart),
			    SdBase = ISNULL(@SdBase, SdBase)
			WHERE NumDepart = @NumDepart
	END
	
	IF @Accion = 3
	BEGIN
		UPDATE Departamento
			SET Estatus = 0
			WHERE NumDepart = @NumDepart
	END

	IF @Accion = 4
	BEGIN
		SELECT NumDepart,
		       NomDepart,
		       SdBase
		FROM Departamento
		WHERE NumDepart = @NumDepart AND Estatus = 1
	END

	IF @Accion = 5
	BEGIN
		SELECT NumDepart,
		       NomDepart,
		       SdBase
		FROM Departamento
		WHERE Estatus = 1
		ORDER BY NumDepart;
	END

END
Go

CREATE PROCEDURE sp_GestionPuesto
(
	@Accion				TINYINT,	--1add, 2modif, 3deshabilitar,
									--4consultar uno, 5consultar todos
	@NumPuesto          INT             = NULL,
	@NomPuesto          VARCHAR(40)     = NULL,
	@Proporcion         FLOAT           = NULL,
	@Salario            MONEY           = NULL,
	@NumDepart          INT             = NULL
)
AS
BEGIN
	IF @Accion = 1
	BEGIN
	    SET @NumPuesto = ABS(CHECKSUM(NEWID())) % 99999;
		
		Select @Salario = @Proporcion* SdBase FROM Departamento Where NumDepart = @NumDepart;
		
		INSERT INTO Puesto(NumPuesto, NomPuesto, Proporcion, Salario, Estatus)
		VALUES(@NumPuesto, @NomPuesto, @Proporcion, @Salario,1);

		INSERT INTO Puesto_Departamento(Departamento, Puesto)
		VALUES(@NumDepart, @NumPuesto);
	END
	
	IF @Accion = 2
	BEGIN
		IF @NomPuesto = '' BEGIN SET @NomPuesto = NULL END
		IF @Proporcion = 0 BEGIN SET @Proporcion = NULL END

		UPDATE Puesto
			SET NomPuesto = ISNULL(@NomPuesto, NomPuesto),
			    Proporcion = ISNULL(@Proporcion, Proporcion)
			WHERE NumPuesto = @NumPuesto
		Select @NumDepart= Departamento FROM Puesto_Departamento Where Puesto = @NumPuesto;
		Select @Salario = @Proporcion* SdBase FROM Departamento Where NumDepart = @NumDepart;
		UPDATE Puesto
			SET Salario = @Salario
			WHERE NumPuesto = @NumPuesto
	END
	
	IF @Accion = 3
	BEGIN
		UPDATE Puesto
			SET Estatus = 0
			WHERE NumPuesto = @NumPuesto
	END

	IF @Accion = 4
	BEGIN
		SELECT D.NumDepart,
		       D.NomDepart,
		       D.SdBase,
			   P.NumPuesto,
			   P.NomPuesto,
			   P.Proporcion,
			   P.Salario
		FROM Departamento D
		JOIN Puesto_Departamento PD
		    ON PD.Departamento = D.NumDepart
		JOIN Puesto P
		    ON P.NumPuesto = PD.Puesto
		WHERE PD.Puesto = @NumPuesto AND P.Estatus = 1;
	END

	IF @Accion = 5
	BEGIN
			SELECT NumPuesto, NomPuesto, Proporcion, Salario FROM Puesto Where Estatus = 1
	END

	IF @Accion = 6
	BEGIN
			SELECT PD.Puesto, A.NomPuesto 
			FROM Puesto A
			JOIN Puesto_Departamento PD
			ON A.NumPuesto = PD.Puesto
			Where Departamento = @NumDepart
	END
	IF @Accion = 7
	BEGIN
			SELECT NumPuesto, NomPuesto, Proporcion, Salario, Departamento, NomDepart
			FROM Puesto_Departamento PD
			JOIN Puesto A
			ON A.NumPuesto = PD.Puesto
			JOIN Departamento D
			ON D.NumDepart = PD.Departamento
	END
END
Go

CREATE PROCEDURE sp_AsignarPuesto_Empleado
(
	@Accion				TINYINT,	--1add, 
									--2consultar uno, 3consultar todos
	@Empleado           INT             = NULL,
	@Puesto             INT             = NULL
)
AS
BEGIN
	IF @Accion = 1
	BEGIN
		INSERT INTO Puesto_Empleado(Empleado, Puesto)
		VALUES(@Empleado, @Puesto);
	END

	IF @Accion = 2
	BEGIN
		SELECT E.NumEmpleado,
	           CONCAT (E.Nombre, ' ', E.ApPaterno, ' ', ApMaterno),
		       D.NumDepart,
		       D.NomDepart,
		       D.SdBase,
			   P.NumPuesto,
			   P.NomPuesto,
			   P.Proporcion,
			   P.Salario
		FROM Departamento D
		JOIN Puesto_Departamento PD
		    ON PD.Departamento = D.NumDepart
		JOIN Puesto P
		    ON P.NumPuesto = PD.Puesto
		JOIN Puesto_Empleado PE
		    ON PE.Puesto = P.NumPuesto
		JOIN Empleado E
		    ON E.NumEmpleado = PE.Empleado
		WHERE PE.Empleado= @Empleado AND D.Estatus = 1 AND P.Estatus = 1 AND E.Estatus = 1;
	END

	IF @Accion = 3
	BEGIN
			SELECT E.NumEmpleado,
	           CONCAT (E.Nombre, ' ', E.ApPaterno, ' ', ApMaterno),
		       D.NumDepart,
		       D.NomDepart,
		       D.SdBase,
			   P.NumPuesto,
			   P.NomPuesto,
			   P.Proporcion,
			   P.Salario
		FROM Departamento D
		JOIN Puesto_Departamento PD
		    ON PD.Departamento = D.NumDepart
		JOIN Puesto P
		    ON P.NumPuesto = PD.Puesto
		JOIN Puesto_Empleado PE
		    ON PE.Puesto = P.NumPuesto
		JOIN Empleado E
		    ON E.NumEmpleado = PE.Empleado
			WHERE D.Estatus = 1 AND P.Estatus = 1 AND E.Estatus = 1
		ORDER BY D.NomDepart, P.NomPuesto;

	END

END
Go

CREATE PROCEDURE sp_GestionPercepcion_Deduccion
(
	@Accion				TINYINT,	--1add, 2modif, 
									--3consultar uno, 4consultar todos 
	@Codigo             INT             = NULL,
	@Nombre             VARCHAR(35)     = NULL,
	@Tipo               CHAR(1)         = NULL,
	@Cantidad           NUMERIC(7,2)    = NULL,
	@TipoCan            CHAR(1)         = NULL,
	@Destino            CHAR(1)         = NULL,
	@Departamento       INT             = NULL,
	@Empleado           INT             = NULL,
	@FechAplic          DATE            = NULL
)
AS
BEGIN
	IF @Accion = 1
	BEGIN
	SET @Codigo = ABS(CHECKSUM(NEWID())) % 999999
		INSERT INTO Percepcion_Deduccion(Codigo, Nombre, Tipo, Cantidad, TipoCan, Estatus, FechaPD)
		VALUES(@Codigo, @Nombre, @Tipo, @Cantidad, @TipoCan, 1, @FechAplic);

		--IF (@Destino = 'D')
		--BEGIN
		--INSERT INTO Percepcion_Deduccion_Departamento(Codigo, Departamento)
		--VALUES(@Codigo, @Departamento);
		--CREATE TABLE #DPDepart(
			--Codigo INT, 
			--Empleado INT,
			--FechaDP Date
		--);
		--INSERT INTO Percepcion_Deduccion_Empleado(Codigo, Empleado, FechaPDE)
		--VALUES(@Codigo, @Empleado, @FechAplic);
		--END

		--IF (@Destino = 'E')
		--BEGIN
		--INSERT INTO Percepcion_Deduccion_Empleado(Codigo, Empleado, FechaPDE)
		--VALUES(@Codigo, @Empleado, @FechAplic);
		--END
	END
	
	IF @Accion = 2
	BEGIN
		IF @Nombre = '' BEGIN SET @Nombre = NULL END
		IF @Tipo = '' BEGIN SET @Tipo = NULL END
		IF @Cantidad = 0 BEGIN SET @Cantidad = NULL END
		IF @TipoCan = '' BEGIN SET @TipoCan = NULL END

		UPDATE Percepcion_Deduccion
			SET Nombre = ISNULL(@Nombre, Nombre),
				Tipo = ISNULL(@Tipo, Tipo),
				Cantidad = ISNULL(@Cantidad, Cantidad),
				TipoCan = ISNULL(@TipoCan, TipoCan)
			WHERE Codigo = @Codigo;
	END
	IF @Accion = 3
	BEGIN
		SELECT Codigo,
			   Nombre,
	           Tipo,
	           Cantidad,
	           TipoCan,
			   FechaPD
		FROM Percepcion_Deduccion
		WHERE Codigo = @Codigo AND Estatus = 1;
	END

	IF @Accion = 4
	BEGIN
		SELECT Codigo,
			   Nombre,
	           Tipo,
	           Cantidad,
	           TipoCan
		FROM Percepcion_Deduccion 
		WHERE Estatus = 1
	END
	IF @Accion = 5--Asignacion
	BEGIN
	   INSERT INTO Percepcion_Deduccion_Empleado(Codigo, Empleado, FechaPDE, Resultado)
		VALUES(@Codigo, @Empleado, @FechAplic, 0);
	    DECLARE @SMB MONEY = dbo.f_CalcularSMB(@Empleado)
		
		Declare @DiaTrab INT
		SET @DiaTrab = DAY(EOMONTH(@FechAplic))

		DECLARE @SueldoMensualBruto MONEY
		SET @SueldoMensualBruto = @SMB * @DiaTrab

		DECLARE @SueldoMensualNeto MONEY
		SET @SueldoMensualNeto = dbo.f_SueldoMensualNeto(@SueldoMensualBruto, @Codigo, @FechAplic)

		DECLARE @Resultado MONEY = @SueldoMensualBruto + @SueldoMensualNeto
		----select @SMB
		--select @SueldoMensualBruto
		--select @SueldoMensualNeto
		--select @FechAplic
		----select @Resultado
		UPDATE Percepcion_Deduccion_Empleado
			SET Resultado = @SueldoMensualNeto
			WHERE Codigo = @Codigo AND Empleado = @Empleado AND FechaPDE = @FechAplic;
	END
	IF @Accion = 6--Empleado-Departamento
	BEGIN
		SELECT PD.Departamento, PE.Empleado
			FROM Puesto_Departamento PD
			JOIN Puesto_Empleado PE
			ON PE.Puesto = PD.Puesto
			Where PD.Departamento=@Departamento
	END

END
Go

CREATE PROCEDURE sp_ReciboNomina(
	@Accion		TINYINT,
	@ID			INT,
	@fecha		DATE,
	--@mes		DATE,
	@DiaTrab	INT
)
AS
BEGIN
	IF @Accion = 1 
	BEGIN
		DECLARE @count INT, @Puesto INT, @Depa INT, @cod INT, @tipo CHAR(1), @Can NUMERIC(7,2), @TipoCan CHAR(1)

		SELECT @Puesto = Puesto FROM Puesto_Empleado WHERE Empleado = @ID
		SELECT @Depa = Departamento FROM Puesto_Departamento WHERE Puesto = @Puesto

		CREATE TABLE #PDE(
			Codigo INT,-------
			Emplado INT
		);

		----CREATE TABLE #PDD(
			----Codigo INT, -----
			----Departamento INT
		----);

		----INSERT INTO #PDD(Codigo, Departamento)
		----SELECT Codigo, Departamento FROM Percepcion_Deduccion_Departamento WHERE @Depa = Departamento

		----SELECT @count = count(Codigo) FROM #PDD

		DECLARE @SueldoMensualBruto MONEY, @SueldoDiaBruto MONEY
		SET @SueldoDiaBruto = dbo.f_CalcularSMB(@ID)
		
		SET @DiaTrab = DAY(EOMONTH(@fecha))
		SET @SueldoMensualBruto = @SueldoDiaBruto * @DiaTrab

		--SELECT  dbo.f_CalcularSMB(@ID, @año, @mes, @DiaTrab)
		----SELECT * FROM #PDD AS PDD
		select @sueldoMensualBruto
		DECLARE @SueldoMensualNeto MONEY SET @SueldoMensualNeto = @SueldoMensualBruto
		DECLARE @SMB MONEY SET @SMB = @SueldoMensualBruto
		----WHILE @count > 0
		----BEGIN
			----SELECT @cod = Codigo FROM #PDD
			--SELECT @fecha, @cod Aqui
			----SET @SueldoMensualNeto += dbo.f_SueldoMensualNeto(@SMB, @cod, @fecha)
			--SELECT [dbo].[f_SueldoMensualNeto](@SueldoMensualBruto, @cod)
			----SELECT @SueldoMensualNeto, @SMB 

			----DELETE #PDD WHERE @cod = Codigo
			----SET @count = (SELECT count(Codigo) FROM #PDD)
		----END
		----DROP TABLE #PDD;

		INSERT INTO #PDE(Codigo, Emplado)
		SELECT Codigo, Empleado FROM Percepcion_Deduccion_Empleado WHERE @ID = Empleado
		SELECT @count = count(Codigo) FROM #PDE

		SELECT * FROM #PDE AS PDE

		WHILE @count > 0
		BEGIN
			SELECT @cod = Codigo FROM #PDE

			SET @SueldoMensualNeto += dbo.f_SueldoMensualNeto(@SMB, @cod, @fecha)
			--SELECT [dbo].[f_SueldoMensualNeto](@SueldoMensualBruto, @cod)
			
			SELECT @SueldoMensualNeto, @SMB

			DELETE #PDE WHERE @cod = Codigo
			SET @count = (SELECT count(Codigo) FROM #PDE)
		END
		DROP TABLE #PDE;

	--select DAY(EOMONTH(@fecha))
		DECLARE @fecha2 DATE = @fecha
		SET @fecha = datefromparts(YEAR(@fecha), MONTH(@fecha), DAY(EOMONTH(@fecha)))
		SET @fecha2 = datefromparts(YEAR(@fecha2), MONTH(@fecha2), 1)

		DECLARE @sdNetoLetra VARCHAR(200)
		SET @sdNetoLetra = [dbo].[CantidadConLetra](@SueldoMensualNeto)

		INSERT INTO Nomina(DiaTrab, EmplNomina, SdDia, SdBruto, sdNeto, FechFinPdPago, FechIniPdPago, FechPag, SdNetoLetra)
		VALUES(@DiaTrab, @ID, @SueldoDiaBruto, @SueldoMensualBruto, @SueldoMensualNeto, @fecha, @fecha2, GETDATE(), @sdNetoLetra)
	END
	IF @Accion = 2
	BEGIN
		SELECT FechFinPdPago, EmplNomina FROM Recibos_Creados
	END
	IF @Accion = 3
	BEGIN
	SET @fecha = datefromparts(YEAR(@fecha), MONTH(@fecha), DAY(EOMONTH(@fecha)))
	declare @Puesto1 INT
	SET @Puesto1 = (SELECT Puesto FROM Puesto_Empleado where Empleado = @ID)
	declare @Departamento INT
	SET @Departamento = (SELECT Departamento FROM Puesto_Departamento where Puesto = @Puesto1)
	declare @IDNomina UNIQUEIDENTIFIER
	SET @IDNomina = (SELECT IDNomina FROM Nomina where EmplNomina = @ID AND YEAR(EOMONTH(FechFinPdPago)) = YEAR(EOMONTH(@fecha)) AND   MONTH(EOMONTH(FechFinPdPago)) = MONTH(EOMONTH(@fecha)))
	--declare @sdNeto int
	--SET @sdNeto = (SELECT sdNeto FROM Nomina where EmplNomina = @ID)

	--DECLARE @COUNTDEPART INT 
	--SET @COUNTDEPART = (SELECT count(Departamento) From Percepcion_Deduccion_Departamento where Departamento=@Departamento)
	--DECLARE @COUNTEmpl INT
	--SET @COUNTEmpl = (SELECT count(Empleado) From Percepcion_Deduccion_Empleado where Empleado=@ID)
    --DECLARE @COUNT1 INT 
	--SET @COUNT1 = @COUNTDEPART + @COUNTEmpl
	--SElect @COUNT1 AS SSSSS

		----Select TOP 50 PERCENT DUNS, RazSocial, A.Email, TelEmpr1, TelEmpr2, NumEmpleado, B.Nombre, ApPaterno, ApMaterno, NSS, CURP, B.RFC, FechIniPdPago, FechFinPdPago, sdNeto, sdNetoLetra,D.Nombre as NombrePD, Tipo, Cantidad, TipoCan 
		----from Empresa A, Empleado B, Nomina C, Percepcion_Deduccion D
	   ---- Where NumEmpleado = @ID AND IDNomina=@IDNomina AND YEAR(EOMONTH(FechFinPdPago)) = YEAR(EOMONTH(@fecha)) AND   MONTH(EOMONTH(FechFinPdPago)) = MONTH(EOMONTH(@fecha))

  select DUNS, RazSocial, A.Email, TelEmpr1, TelEmpr2, NumEmpleado, B.Nombre, ApPaterno, ApMaterno, NSS, CURP, B.RFC, FechIniPdPago, FechFinPdPago, sdNeto, Resultado, sdNetoLetra,D.Nombre as NombrePD, Tipo, Cantidad, TipoCan 
  from Empleado B
  JOIN Nomina C
  ON C.EmplNomina = B.NumEmpleado
  JOIN Empresa_Empleado EE
  ON EE.Empleado = B.NumEmpleado
  JOIN Empresa A
  ON A.DUNS = EE.Empresa
  JOIN Percepcion_Deduccion_Empleado PDE
  ON PDE.Empleado = B.NumEmpleado
  JOIN Percepcion_Deduccion D
  ON D.Codigo = PDE.Codigo
  Where NumEmpleado = @ID --AND IDNomina=@IDNomina AND YEAR(EOMONTH(FechFinPdPago)) = YEAR(EOMONTH(@fecha)) AND   MONTH(EOMONTH(FechFinPdPago)) = MONTH(EOMONTH(@fecha))
			
				UPDATE Empleado
			SET Estatus = 1
			WHERE NumEmpleado = @ID

				UPDATE Puesto_Empleado
			SET Estatus = 1
			WHERE Empleado = @ID

	END
END
GO

CREATE FUNCTION dbo.f_CalcularSMB
     (
          @ID                   AS INT
		  
                )
RETURNS  MONEY
AS
BEGIN
	DECLARE @PB MONEY, @SD MONEY, @SBD MONEY, @NS MONEY, @Puesto INT, @Depa INT, @SMB MONEY

	SELECT @Puesto = Puesto FROM Puesto_Empleado WHERE Empleado = @ID
	SELECT @NS = Proporcion FROM Puesto WHERE NumPuesto = @Puesto
	SELECT @Depa = Departamento FROM Puesto_Departamento WHERE Puesto = @Puesto
	SELECT @SBD = SdBase FROM Departamento WHERE NumDepart = @Depa
	SET @SD = @SBD * @NS

	RETURN @SD
 END
GO

CREATE FUNCTION dbo.f_SueldoMensualNeto(
@SMB MONEY,
@cod INT,
@fecha DATE
)
RETURNS MONEY
AS
BEGIN
DECLARE @tipo CHAR(1), @Can NUMERIC(7,2), @TipoCan CHAR(1)
DECLARE @SMN MONEY SET @SMN = @SMB



--SELECT @tipo = Tipo, @Can = Cantidad, @TipoCan = TipoCan FROM Percepcion_Deduccion WHERE Codigo = @cod AND @fecha = fechaPD
----SELECT @tipo = Tipo, @Can = Cantidad, @TipoCan = TipoCan FROM Percepcion_Deduccion WHERE Codigo = @cod AND YEAR(EOMONTH(@fecha)) = YEAR(EOMONTH(fechaPD)) AND MONTH(EOMONTH(@fecha)) = MONTH(EOMONTH(fechaPD))
SELECT @tipo = Tipo, @Can = Cantidad, @TipoCan = TipoCan 
FROM Percepcion_Deduccion PD
JOIN  Percepcion_Deduccion_Empleado PDE
ON PDE.Codigo = PD.Codigo
WHERE PDE.Codigo = @cod AND YEAR(EOMONTH(@fecha)) = YEAR(EOMONTH(fechaPDE)) AND MONTH(EOMONTH(@fecha)) = MONTH(EOMONTH(fechaPDE))

IF(@tipo = 'P') BEGIN
IF(@TipoCan = 'F') BEGIN
SET @SMN = @Can END
ELSE BEGIN
SET @SMN = @SMB * @Can END
END ELSE BEGIN
IF(@TipoCan = 'F') BEGIN
SET @SMN = @Can * -1 END
ELSE BEGIN
SET @SMN = @SMB * @Can * -1 END
END

DECLARE @FN DATE
SELECT @FN = FechaPDE FROM Percepcion_Deduccion_Empleado WHERE Codigo = @cod
--IF @FN != @fecha
IF YEAR(EOMONTH(@fecha)) != YEAR(EOMONTH(@FN)) OR MONTH(EOMONTH(@fecha)) != MONTH(EOMONTH(@FN))
BEGIN
	SET @SMN = 0
END

RETURN @SMN
END
GO

CREATE VIEW Recibos_Creados AS SELECT FechFinPdPago, EmplNomina FROM Nomina
GO

CREATE VIEW Ingresar AS SELECT NumEmpleado, Contraseña FROM Empleado
GO
--Cambio datos empresa, Nombre completo, nss, curp, rfc, fecha, percepciones deducciones, y sueldo neto, letra
		
--CREATE VIEW ConsultarRecibo AS SELECT DUNS, RazSocial, A.Email, TelEmpr1, TelEmpr2, NumEmpleado, B.Nombre, ApPaterno, ApMaterno, NSS, CURP, B.RFC, FechIniPdPago, FechFinPdPago, sdNeto, sdNetoLetra, D.Nombre as NombrePD, Tipo, Cantidad, TipoCan FROM Empresa A, Empleado B, Nomina C, Percepcion_Deduccion D--Cambio
--GO


CREATE FUNCTION [dbo].[CantidadConLetra]
(
    @Numero             NUMERIC(7,2)
)
RETURNS Varchar(180)
AS
BEGIN
    DECLARE @ImpLetra Varchar(200)
        DECLARE @lnEntero INT,
                        @lcRetorno VARCHAR(512),
                        @lnTerna INT,
                        @lcMiles VARCHAR(512),
                        @lcCadena VARCHAR(512),
                        @lnUnidades INT,
                        @lnDecenas INT,
                        @lnCentenas INT,
                        @lnFraccion INT
        SELECT  @lnEntero = CAST(@Numero AS INT),
                        @lnFraccion = (@Numero - @lnEntero) * 100,
                        @lcRetorno = '',
                        @lnTerna = 1
  WHILE @lnEntero > 0
  BEGIN /* WHILE */
            -- Recorro terna por terna
            SELECT @lcCadena = ''
            SELECT @lnUnidades = @lnEntero % 10
            SELECT @lnEntero = CAST(@lnEntero/10 AS INT)
            SELECT @lnDecenas = @lnEntero % 10
            SELECT @lnEntero = CAST(@lnEntero/10 AS INT)
            SELECT @lnCentenas = @lnEntero % 10
            SELECT @lnEntero = CAST(@lnEntero/10 AS INT)
            -- Analizo las unidades
            SELECT @lcCadena =
            CASE /* UNIDADES */
              WHEN @lnUnidades = 1 THEN 'UN ' + @lcCadena
              WHEN @lnUnidades = 2 THEN 'DOS ' + @lcCadena
              WHEN @lnUnidades = 3 THEN 'TRES ' + @lcCadena
              WHEN @lnUnidades = 4 THEN 'CUATRO ' + @lcCadena
              WHEN @lnUnidades = 5 THEN 'CINCO ' + @lcCadena
              WHEN @lnUnidades = 6 THEN 'SEIS ' + @lcCadena
              WHEN @lnUnidades = 7 THEN 'SIETE ' + @lcCadena
              WHEN @lnUnidades = 8 THEN 'OCHO ' + @lcCadena
              WHEN @lnUnidades = 9 THEN 'NUEVE ' + @lcCadena
              ELSE @lcCadena
            END /* UNIDADES */
            -- Analizo las decenas
            SELECT @lcCadena =
            CASE /* DECENAS */
              WHEN @lnDecenas = 1 THEN
                CASE @lnUnidades
                  WHEN 0 THEN 'DIEZ '
                  WHEN 1 THEN 'ONCE '
                  WHEN 2 THEN 'DOCE '
                  WHEN 3 THEN 'TRECE '
                  WHEN 4 THEN 'CATORCE '
                  WHEN 5 THEN 'QUINCE '
                  WHEN 6 THEN 'DIEZ Y SEIS '
                  WHEN 7 THEN 'DIEZ Y SIETE '
                  WHEN 8 THEN 'DIEZ Y OCHO '
                  WHEN 9 THEN 'DIEZ Y NUEVE '
                END
              WHEN @lnDecenas = 2 THEN
              CASE @lnUnidades
                WHEN 0 THEN 'VEINTE '
                ELSE 'VEINTI' + @lcCadena
              END
              WHEN @lnDecenas = 3 THEN
              CASE @lnUnidades
                WHEN 0 THEN 'TREINTA '
                ELSE 'TREINTA Y ' + @lcCadena
              END
              WHEN @lnDecenas = 4 THEN
                CASE @lnUnidades
                    WHEN 0 THEN 'CUARENTA'
                    ELSE 'CUARENTA Y ' + @lcCadena
                END
              WHEN @lnDecenas = 5 THEN
                CASE @lnUnidades
                    WHEN 0 THEN 'CINCUENTA '
                    ELSE 'CINCUENTA Y ' + @lcCadena
                END
              WHEN @lnDecenas = 6 THEN
                CASE @lnUnidades
                    WHEN 0 THEN 'SESENTA '
                    ELSE 'SESENTA Y ' + @lcCadena
                END
              WHEN @lnDecenas = 7 THEN
                 CASE @lnUnidades
                    WHEN 0 THEN 'SETENTA '
                    ELSE 'SETENTA Y ' + @lcCadena
                 END
              WHEN @lnDecenas = 8 THEN
                CASE @lnUnidades
                    WHEN 0 THEN 'OCHENTA '
                    ELSE  'OCHENTA Y ' + @lcCadena
                END
              WHEN @lnDecenas = 9 THEN
                CASE @lnUnidades
                    WHEN 0 THEN 'NOVENTA '
                    ELSE 'NOVENTA Y ' + @lcCadena
                END
              ELSE @lcCadena
            END /* DECENAS */
            -- Analizo las centenas
            SELECT @lcCadena =
            CASE /* CENTENAS */
              WHEN @lnCentenas = 1 THEN 'CIENTO ' + @lcCadena
              WHEN @lnCentenas = 2 THEN 'DOSCIENTOS ' + @lcCadena
              WHEN @lnCentenas = 3 THEN 'TRESCIENTOS ' + @lcCadena
              WHEN @lnCentenas = 4 THEN 'CUATROCIENTOS ' + @lcCadena
              WHEN @lnCentenas = 5 THEN 'QUINIENTOS ' + @lcCadena
              WHEN @lnCentenas = 6 THEN 'SEISCIENTOS ' + @lcCadena
              WHEN @lnCentenas = 7 THEN 'SETECIENTOS ' + @lcCadena
              WHEN @lnCentenas = 8 THEN 'OCHOCIENTOS ' + @lcCadena
              WHEN @lnCentenas = 9 THEN 'NOVECIENTOS ' + @lcCadena
              ELSE @lcCadena
            END /* CENTENAS */
            -- Analizo la terna
            SELECT @lcCadena =
            CASE /* TERNA */
              WHEN @lnTerna = 1 THEN @lcCadena
              WHEN @lnTerna = 2 THEN @lcCadena + 'MIL '
              WHEN @lnTerna = 3 THEN @lcCadena + 'MILLONES '
              WHEN @lnTerna = 4 THEN @lcCadena + 'MIL '
              ELSE ''
            END /* TERNA */
            -- Armo el retorno terna a terna
            SELECT @lcRetorno = @lcCadena  + @lcRetorno
            SELECT @lnTerna = @lnTerna + 1
   END /* WHILE */
   IF @lnTerna = 1
       SELECT @lcRetorno = 'CERO'
   DECLARE @sFraccion VARCHAR(15)
   SET @sFraccion = '00' + LTRIM(CAST(@lnFraccion AS varchar))
   SELECT @ImpLetra = RTRIM(@lcRetorno) + ' PESOS ' + SUBSTRING(@sFraccion,LEN(@sFraccion)-1,2) + '/100 PESOS'

   RETURN @ImpLetra
END
GO

CREATE VIEW ReporteGeneralNomina AS SELECT NomDepart, NomPuesto, Salario ,FechIngrEmpr, FechNacim, NumEmpleado, Nombre, ApPaterno, ApMaterno , FechFinPdPago FROM Empleado, Departamento, Puesto, Nomina
GO

create procedure sp_Reportes(
@Accion INT = NULL,
@fecha DATE = NULL,
@NumDepart INT = NULL,
@NumEmpl INT = NULL
)
AS
BEGIN
IF @Accion = 1--Para uno
BEGIN
select NomDepart, NomPuesto, Salario ,FechIngrEmpr, FechNacim, Nombre, ApPaterno, ApMaterno from ReporteGeneralNomina WHERE @fecha >= FechFinPdPago AND NumEmpleado = @NumEmpl 
END
IF @Accion = 2
BEGIN
Select count(PE.Empleado) TotalEmpleados,PD.Departamento, PD.Puesto
FROM Puesto_Empleado PE RIGHT OUTER JOIN Puesto_Departamento PD
ON PE.Puesto = PD.Puesto
WHERE PE.Estatus = 1
group by PD.Departamento, PD.Puesto
END
IF @Accion = 3
BEGIN 
Select count(PE.Empleado) TotalEmpleados,PD.Departamento, PD.Puesto
FROM Puesto_Empleado PE RIGHT OUTER JOIN Puesto_Departamento PD
ON PE.Puesto = PD.Puesto
WHERE @NumDepart = PD.Departamento AND PE.Estatus = 1
group by PD.Departamento, PD.Puesto
END
IF @Accion = 4--Para todos
BEGIN
Declare @fecha2 DATE
SET @fecha2 = datefromparts(YEAR(@fecha), MONTH(@fecha), DAY(EOMONTH(@fecha)))
  select NomDepart, NomPuesto, Salario ,FechIngrEmpr, FechNacim, Nombre, ApPaterno, ApMaterno
  from Departamento
  JOIN Puesto_Departamento PD
  ON Departamento.NumDepart = PD.Departamento
  JOIN Puesto P
  ON PD.Puesto = P.NumPuesto 
  JOIN Puesto_Empleado PE
  ON  P.NumPuesto = PE.Puesto
  JOIN Empleado E
  ON PE.Empleado = E.NumEmpleado
  JOIN Nomina N
  ON E.NumEmpleado = N.EmplNomina
  ----WHERE YEAR(EOMONTH('220101')) >= YEAR(EOMONTH(FechFinPdPago)) AND MONTH(EOMONTH('220101')) >= MONTH(EOMONTH(FechFinPdPago))
  WHERE @fecha2 >= FechFinPdPago
END
END