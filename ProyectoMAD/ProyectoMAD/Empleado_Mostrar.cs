using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoMAD
{
    public partial class Empleado_Mostrar : Form
    {
        public Empleado_Mostrar()
        {
            InitializeComponent();
        }

        private void Empleado_Mostrar_Load(object sender, EventArgs e)
        {
           
        }

        private void Empleado_Mostrar_Load_1(object sender, EventArgs e)
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
                Empleado_desabilitar.Rows.Add(NumEmpleado, Nombre, ApPaterno, ApMaterno, Contraseña, FechNacim, CURP
                    , NSS, RFC, Banco, NumCuenta, Email, TelCasa, TelCel, FechIngrEmpr, PaisResd, Estado, Muncipio
                    , Colonia, Calle, NumDomicilio, CP);
            }
        }

        private void Empleado_desabilitar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
