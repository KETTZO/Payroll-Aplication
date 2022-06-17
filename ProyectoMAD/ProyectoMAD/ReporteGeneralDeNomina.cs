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
    public partial class ReporteGeneralDeNomina : Form
    {
        public ReporteGeneralDeNomina()
        {
            InitializeComponent();
        }

        private void ReporteGeneralDeNomina_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Todos");
            var All_Departamentos = new DataTable();
            var obj1 = new EnlaceDB();
            All_Departamentos = obj1.get_Empleados(0, 5, "", "", "", "", "", "", new DateTime(), "", "", "", "", "", "", "", "", "", "", "", 0, 0, new DateTime(), 0);
            foreach (DataRow row in All_Departamentos.Rows)
            {
                string NumEmpleado = row["NumEmpleado"].ToString();
                comboBox1.Items.Add(NumEmpleado);
            }

            fecha.Format = DateTimePickerFormat.Custom;
            fecha.CustomFormat = "MM/yyyy";
            fecha.ShowUpDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Reporte = new DataTable();
            var obj1 = new EnlaceDB();

            if (comboBox1.Text=="Todos")
            {
                Reporte = obj1.get_Reportes(4, fecha.Value, 0, 0);
                foreach (DataRow row in Reporte.Rows)
                {
                    string NomDepart = row["NomDepart"].ToString();
                    string NomPuesto = row["NomPuesto"].ToString();
                    string Salario = row["Salario"].ToString();
                    string FechIngr = row["FechIngrEmpr"].ToString();
                    string fechNacim = row["FechNacim"].ToString();
                    string Nom = row["Nombre"].ToString();
                    string ApPaterno = row["ApPaterno"].ToString();
                    string ApMaterno = row["ApMaterno"].ToString();


                    int days = (DateTime.Today - fecha.Value).Days;
                    //assume 365.25 days per year
                    decimal years = days / 365.25m;
                    int numeroEntero = Convert.ToInt32(Math.Floor(years));

                    DataGridViewRow fila = new DataGridViewRow();

                    dataGridView1.Rows.Add(NomDepart, NomPuesto, Salario, FechIngr, numeroEntero, Nom, ApPaterno, ApMaterno);

                }
            }
            else
            {
                Reporte = obj1.get_Reportes(4, fecha.Value, 0, Int32.Parse(comboBox1.Text));
                foreach (DataRow row in Reporte.Rows)
                {
                    string NomDepart = row["NomDepart"].ToString();
                    string NomPuesto = row["NomPuesto"].ToString();
                    string Salario = row["Salario"].ToString();
                    string FechIngr = row["FechIngrEmpr"].ToString();
                    string fechNacim = row["FechNacim"].ToString();
                    string Nom = row["Nombre"].ToString();
                    string ApPaterno = row["ApPaterno"].ToString();
                    string ApMaterno = row["ApMaterno"].ToString();


                    int days = (DateTime.Today - fecha.Value).Days;
                    //assume 365.25 days per year
                    decimal years = days / 365.25m;
                    int numeroEntero = Convert.ToInt32(Math.Floor(years));

                    DataGridViewRow fila = new DataGridViewRow();

                    dataGridView1.Rows.Add(NomDepart, NomPuesto, Salario, FechIngr, numeroEntero, Nom, ApPaterno, ApMaterno);

                }
            }
        }
    }
}
