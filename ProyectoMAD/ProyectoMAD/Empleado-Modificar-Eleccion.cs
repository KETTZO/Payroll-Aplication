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
    public partial class Empleado_Modificar_Eleccion : Form
    {
        public Empleado_Modificar_Eleccion()
        {
            InitializeComponent();
        }

        private void Empleado_Modificar_Eleccion_Load(object sender, EventArgs e)
        {
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();
            bool result = true;
            string id = "";
            if (!(EmpleadoNum.Text == ""))
            {
                Regex reg = new Regex(@"^[0-9]{7}$");
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
                Empleado_Modificar2 puesto = new Empleado_Modificar2(id2);
                puesto.ShowDialog();
            }
        }
    }
}
