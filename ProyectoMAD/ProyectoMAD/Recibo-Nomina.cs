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
using System.Globalization;

namespace ProyectoMAD
{
    public partial class Recibo_Nomina : Form
    {
        public Recibo_Nomina()
        {
            InitializeComponent();
        }

        private void Recibo_Nomina_Load(object sender, EventArgs e)
        {
            fecha.Format = DateTimePickerFormat.Custom;
            fecha.CustomFormat = "MM/yyyy";
            fecha.ShowUpDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result = true;
            /*
            DateTime date = DateTime.Now;
            if (fecha.Value > date)
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Ingrese una fecha Válida", "Error en en Año y Mes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            int days = DateTime.DaysInMonth(fecha.Value.Year, fecha.Value.Month);

            if(Dias.Text == "")
            {
                //Dias.Text ="0";
                result = false;
            }
            else if (Int32.Parse(Dias.Text) > days)
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Los días y el mes no concuerdan", "Error días trabajados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Regex reg = new Regex(@"^[0-9]{7}$");
            if (!reg.IsMatch(NumEmpl.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Ingrese un total de 7 dígitos", "Error en el Número de empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            if (result == true)
            {
                var Recibos_Creados = new DataTable();
                var obj1 = new EnlaceDB();
                Recibos_Creados = obj1.get_Recibo(2, 0, new DateTime(), 0);
                foreach (DataRow row in Recibos_Creados.Rows)
                {
                    string FechaFin = row["FechFinPdPago"].ToString();
                    string Empleado = row["EmplNomina"].ToString();

                    string FechaFin2 = FechaFin.Substring(0, 19);

                    DateTime fechita = DateTime.ParseExact(FechaFin2, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);

                    if (fechita.Month == fecha.Value.Month && fechita.Year == fecha.Value.Year && Empleado == NumEmpl.Text)
                    {
                        result = false;
                    }
                }
            }
            */
            if (result)
            {

                var Empleados = new DataTable();
                var obj1 = new EnlaceDB();
                Empleados = obj1.get_Empleados(0, 5, "", "", "", "", "", "", new DateTime(), "", "", "", "", "", "", "", "", "", "", "", 0, 0, new DateTime(), 0);
                foreach (DataRow row in Empleados.Rows)
                {
                    string NumEmpleado = row["NumEmpleado"].ToString();

                    var tablaNomina = new DataTable();
                    var obj = new EnlaceDB();
                    tablaNomina = obj.get_Recibo(1, Int32.Parse(NumEmpleado), fecha.Value, 0);
                    this.Close();
                }
                var respuesta = MessageBox.Show(this, "Recibo Generado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}