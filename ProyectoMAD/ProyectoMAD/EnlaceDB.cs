using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ProyectoMAD
{
    class EnlaceDB
    {
        static private string _aux { set; get; }
        static private SqlConnection _conexion;
        static private SqlDataAdapter _adaptador = new SqlDataAdapter();
        static private SqlCommand _comandosql = new SqlCommand();
        static private DataTable _tabla = new DataTable();
        static private DataSet _DS = new DataSet();

        public DataTable obtenertabla
        {
            get
            {
                return _tabla;
            }
        }

        private static void conectar()
        {
            //string cnn = ConfigurationManager.AppSettings["desarrollo1"];
            string cnn = ConfigurationManager.ConnectionStrings["Nomina"].ToString();
            _conexion = new SqlConnection(cnn);
            _conexion.Open();
        }
        private static void desconectar()
        {
            _conexion.Close();
        }

        public bool Autentificar(string us, string ps)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "SP_ValidaUser";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@u", SqlDbType.Char, 20);
                parametro1.Value = us;
                var parametro2 = _comandosql.Parameters.Add("@p", SqlDbType.Char, 20);
                parametro2.Value = ps;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(_tabla);

                if (_tabla.Rows.Count > 0)
                {
                    isValid = true;
                }

            }
            catch (SqlException e)
            {
                isValid = false;
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }

        public DataTable get_Empleados(int NumEmpleado, int accion, string RFC, string Nombre, 
            string ApPaterno, string ApMaterno, string Email, string CURP, DateTime Nacimiento, string TelCasa, 
            string TelCel, string Contra, string NSS, string Banco, string NumCuenta, string Pais, string Estado,
            string Municipio, string Colonia, string Calle, int Num, int CP, DateTime FechIngrEmpr, int Puesto)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_GestionEmpleados";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.TinyInt, 1);
                parametro1.Value = accion;
                var parametro0 = _comandosql.Parameters.Add("@NumEmpleado", SqlDbType.Int, 4);
                parametro0.Value = NumEmpleado;
                var parametro2 = _comandosql.Parameters.Add("@RFC", SqlDbType.VarChar, 13);
                parametro2.Value = RFC;
                var parametro3 = _comandosql.Parameters.Add("@Nombre", SqlDbType.VarChar, 50);
                parametro3.Value = Nombre;
                var parametro4 = _comandosql.Parameters.Add("@ApPaterno", SqlDbType.VarChar, 25);
                parametro4.Value = ApPaterno;
                var parametro5 = _comandosql.Parameters.Add("@ApMaterno", SqlDbType.VarChar, 25);
                parametro5.Value = ApMaterno;
                var parametro6 = _comandosql.Parameters.Add("@Email", SqlDbType.VarChar, 40);
                parametro6.Value = Email;
                var parametro7 = _comandosql.Parameters.Add("@CURP", SqlDbType.VarChar, 18);
                parametro7.Value = CURP;
                var parametro8 = _comandosql.Parameters.Add("@Nacimiento", SqlDbType.Date, 4);
                parametro8.Value = Nacimiento;
                var parametro16 = _comandosql.Parameters.Add("@TelCasa", SqlDbType.VarChar, 20);
                parametro16.Value = TelCasa;
                var parametro9 = _comandosql.Parameters.Add("@TelCel", SqlDbType.VarChar, 20);
                parametro9.Value = TelCel;
                var parametro10 = _comandosql.Parameters.Add("@Municipio", SqlDbType.VarChar, 30);
                parametro10.Value = Municipio;
                var parametro11 = _comandosql.Parameters.Add("@Estado", SqlDbType.VarChar, 20);
                parametro11.Value = Estado;
                var parametro13 = _comandosql.Parameters.Add("@Pais", SqlDbType.VarChar, 20);
                parametro13.Value = Pais;
                var parametro15 = _comandosql.Parameters.Add("@Contraseña", SqlDbType.VarChar, 40);
                parametro15.Value = Contra;
                var parametro17 = _comandosql.Parameters.Add("@Colonia", SqlDbType.VarChar, 30);
                parametro17.Value = Colonia;
                var parametro18 = _comandosql.Parameters.Add("@Calle", SqlDbType.VarChar, 30);
                parametro18.Value = Calle;
                var parametro19 = _comandosql.Parameters.Add("@NumDomicilio", SqlDbType.SmallInt, 2);
                parametro19.Value = Num;
                var parametro20 = _comandosql.Parameters.Add("@CP", SqlDbType.Int, 4);
                parametro20.Value = CP;
                var parametro21 = _comandosql.Parameters.Add("@NSS", SqlDbType.VarChar, 11);
                parametro21.Value = NSS;
                var parametro22 = _comandosql.Parameters.Add("@Banco", SqlDbType.VarChar, 30);
                parametro22.Value = Banco;
                var parametro23 = _comandosql.Parameters.Add("@NumCuenta", SqlDbType.VarChar, 10);
                parametro23.Value = NumCuenta;
                var parametro24 = _comandosql.Parameters.Add("@FechIngrEmpr", SqlDbType.Date, 4);
                parametro24.Value = FechIngrEmpr;
                var parametro25 = _comandosql.Parameters.Add("@Puesto", SqlDbType.Int, 4);
                parametro25.Value = Puesto;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

        public DataTable get_Departamentos(int accion, int NumDepart, string NomDepart, float SdBase)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_GestionDepartamento";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;



                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.TinyInt, 1);
                parametro1.Value = accion;
                var parametro2 = _comandosql.Parameters.Add("@NumDepart", SqlDbType.Int, 4);
                parametro2.Value = NumDepart;
                var parametro3 = _comandosql.Parameters.Add("@NomDepart", SqlDbType.VarChar, 40);
                parametro3.Value = NomDepart;
                var parametro4 = _comandosql.Parameters.Add("@SdBase", SqlDbType.Money, 8);
                parametro4.Value = SdBase;




                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);



            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }



            return tabla;
        }

        public DataTable get_Percepcione_Deducciones(int accion, int codigo, string nombre, string tipo , float cantidad, string tipocan, string destino, int departamento, int empleado, DateTime Fecha)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_GestionPercepcion_Deduccion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;



                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.TinyInt, 1);
                parametro1.Value = accion;
                var parametro2 = _comandosql.Parameters.Add("@Codigo", SqlDbType.Int, 4);
                parametro2.Value = codigo;
                var parametro3 = _comandosql.Parameters.Add("@Nombre", SqlDbType.VarChar, 35);
                parametro3.Value = nombre;
                var parametro4 = _comandosql.Parameters.Add("@Tipo", SqlDbType.Char, 1);
                parametro4.Value = tipo;
                var parametro5 = _comandosql.Parameters.Add("@Cantidad", SqlDbType.Float, 8);
                parametro5.Value = cantidad;
                var parametro6= _comandosql.Parameters.Add("@tipocan", SqlDbType.Char, 1);
                parametro6.Value = tipocan;
                var parametro7 = _comandosql.Parameters.Add("@Destino", SqlDbType.Char, 1);
                parametro7.Value = destino;
                var parametro8 = _comandosql.Parameters.Add("@Departamento", SqlDbType.Int, 4);
                parametro8.Value = departamento;
                var parametro9 = _comandosql.Parameters.Add("@Empleado", SqlDbType.Int, 4);
                parametro9.Value = empleado;
                var parametro10 = _comandosql.Parameters.Add("@FechAplic", SqlDbType.Date, 4);
                parametro10.Value = Fecha;
                




                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);



            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }



            return tabla;
        }

        public DataTable get_Puestos(int accion, int NumPuesto, string NomPuesto, float Proporcion, int NumDepartamento, float salario)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_GestionPuesto";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;



                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.TinyInt, 1);
                parametro1.Value = accion;
                var parametro2 = _comandosql.Parameters.Add("@NumPuesto", SqlDbType.Int, 4);
                parametro2.Value = NumPuesto;
                var parametro3 = _comandosql.Parameters.Add("@NomPuesto", SqlDbType.VarChar, 40);
                parametro3.Value = NomPuesto;
                var parametro4 = _comandosql.Parameters.Add("@Proporcion", SqlDbType.Float, 4);
                parametro4.Value = Proporcion;
                var parametro5 = _comandosql.Parameters.Add("@NumDepart", SqlDbType.Int, 4);
                parametro5.Value = NumDepartamento;
                var parametro6 = _comandosql.Parameters.Add("@Salario", SqlDbType.Money, 8);
                parametro6.Value = salario;




                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);



            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }



            return tabla;
        }

        public DataTable get_Recibo(int accion, int NumEmpleado, DateTime fecha, int dias)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_ReciboNomina";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.TinyInt, 1);
                parametro1.Value = accion;
                var parametro2 = _comandosql.Parameters.Add("@ID", SqlDbType.Int, 4);
                parametro2.Value = NumEmpleado;
                var parametro3 = _comandosql.Parameters.Add("@fecha", SqlDbType.Date, 4);
                parametro3.Value = fecha;
                var parametro4 = _comandosql.Parameters.Add("@DiaTrab", SqlDbType.Int, 4);
                parametro4.Value = dias;


                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

        public DataTable get_Puesto_Empleado(int accion, int NumEmpleado, int Puesto)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_AsignarPuesto_Empleado";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.TinyInt, 1);
                parametro1.Value = accion;
                var parametro2 = _comandosql.Parameters.Add("@Empleado", SqlDbType.Int, 4);
                parametro2.Value = NumEmpleado;
                var parametro3 = _comandosql.Parameters.Add("@Puesto", SqlDbType.Int, 4);
                parametro3.Value = Puesto;
                


                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

        public DataTable get_Reportes(int accion, DateTime dateTime, int NumDepart, int NumEmpl)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_Reportes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Accion", SqlDbType.TinyInt, 1);
                parametro1.Value = accion;
                var parametro2 = _comandosql.Parameters.Add("@Fecha", SqlDbType.Date, 4);
                parametro2.Value = dateTime;
                var parametro3 = _comandosql.Parameters.Add("@NumDepart", SqlDbType.Int, 4);
                parametro3.Value = NumDepart;
                var parametro4 = _comandosql.Parameters.Add("@NumEmpl", SqlDbType.Int, 4);
                parametro4.Value = NumEmpl;




                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }
    }
}
