using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace ProyectoMAD
{
    public partial class Cambios_Empleado_Ingresar_Eleccion : Form
    {
        public Cambios_Empleado_Ingresar_Eleccion()
        {
            InitializeComponent();
        }

        private void Cambios_Empleado_Ingresar_Elección_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
            dateTimePicker1.ShowUpDown = true;

            var Empleados = new DataTable();
            var obj1 = new EnlaceDB();
            Empleados = obj1.get_Empleados(0, 5, "", "", "", "", "", "", new DateTime(), "", "", "", "", "", "", "", "", "", "", "", 0, 0, new DateTime(), 0);
            foreach (DataRow row in Empleados.Rows)
            {
                string NumEmpleado = row["NumEmpleado"].ToString();
                string Nombre = row["Nombre"].ToString();
                string ApPaterno = row["ApPaterno"].ToString();
                string ApMaterno = row["ApMaterno"].ToString();
                string Contraseña = row["Contraseña"].ToString();
                string FechNacim = row["FechNacim"].ToString();
                string CURP = row["CURP"].ToString();
                string NSS = row["NSS"].ToString();
                string RFC = row["RFC"].ToString();
                string Banco = row["Banco"].ToString();
                string NumCuenta = row["NumCuenta"].ToString();
                string Email = row["Email"].ToString();
                string TelCasa = row["TelCasa"].ToString();
                string TelCel = row["TelCel"].ToString();
                string FechIngrEmpr = row["FechIngrEmpr"].ToString();

                string PaisResd = row["PaisResd"].ToString();
                string Estado = row["Estado"].ToString();
                string Muncipio = row["Muncipio"].ToString();
                string Colonia = row["Colonia"].ToString();
                string Calle = row["Calle"].ToString();
                string NumDomicilio = row["NumDomicilio"].ToString();
                string CP = row["CP"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                Empleado_ModEleccion.Rows.Add(NumEmpleado, Nombre, ApPaterno, ApMaterno, Contraseña, FechNacim, CURP
                    , NSS, RFC, Banco, NumCuenta, Email, TelCasa, TelCel, FechIngrEmpr, PaisResd, Estado, Muncipio
                    , Colonia, Calle, NumDomicilio, CP);
            }

            var Cambios = new DataTable();
            var obj2 = new EnlaceDB();
            Cambios = obj2.get_Percepcione_Deducciones(4, 0, "", "", 0, "", "", 0, 0, new DateTime());
            foreach (DataRow row in Cambios.Rows)
            {
                string Codigo = row["Codigo"].ToString();
                string Nombre = row["Nombre"].ToString();
                string Tipo = row["Tipo"].ToString();
                string Cantidad = row["Cantidad"].ToString();
                string TipoCan = row["TipoCan"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                CambiosEliminar.Rows.Add(Codigo, Nombre, Tipo, Cantidad, TipoCan);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();
            bool result = true;
            string id = "";
            if (!(EmpleadoNum.Text == ""))
            {
                Regex reg = new Regex(@"^[0-9]+$");
                if (!reg.IsMatch(EmpleadoNum.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Ingrese un total de 7 dígitos", "Error en el Número de empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {


                if (Empleado_ModEleccion.SelectedRows.Count > 0)
                {
                    id = Empleado_ModEleccion.SelectedCells[0].Value.ToString();
                }
                else
                {

                    result = false;
                }

            }

            //var tablaCam = new DataTable();
            //var obj = new EnlaceDB();
            result = true;
            string id3 = "";
            if (!(CambioNum.Text == ""))
            {
                Regex reg = new Regex(@"^[0-9]+$");
                if (!reg.IsMatch(CambioNum.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Ingrese un total de 7 dígitos", "Error en el Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {


                if (CambiosEliminar.SelectedRows.Count > 0)
                {
                    id3 = CambiosEliminar.SelectedCells[0].Value.ToString();
                }
                else
                {

                    result = false;
                }

            }

            if (result)
            {
                string id2 = "";
                if (EmpleadoNum.Text != "")
                {
                    id2 = EmpleadoNum.Text;
                }
                else
                {
                    id2 = id;
                }

                string id4 = "";
                if (CambioNum.Text != "")
                {
                    id4 = CambioNum.Text;
                }
                else
                {
                    id4 = id3;
                }
                tablaEmpl = obj.get_Percepcione_Deducciones(5, Int32.Parse(id4), "", "", 0, "", "", 0, Int32.Parse(id2), dateTimePicker1.Value);
                var respuesta = MessageBox.Show(this, "Cambio ingresado a Empleado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                //Cambios_de_sueldo_Ingresar puesto = new Cambios_de_sueldo_Ingresar(id2);
                //puesto.ShowDialog();
            }
        }
    }
}
