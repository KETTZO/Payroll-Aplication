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
    public partial class Headcounter1 : Form
    {
        public Headcounter1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result = true;
            int accion = 0;
            string NumDepart = "0";
            if (!(Departamentos.SelectedIndex >= 0))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Por favor seleccione uno de nuestros departamentos", "Error el Departamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (Departamentos.Text == "Todos")
                {
                    accion = 2;
                }

                var All_Departamentos = new DataTable();
                var obj1 = new EnlaceDB();
                All_Departamentos = obj1.get_Departamentos(5, 0, "", 0);
                foreach (DataRow row in All_Departamentos.Rows)
                {
                    string NomDepart = row["NomDepart"].ToString();
                    if (Departamentos.Text == NomDepart)
                    {
                        NumDepart = row["NumDepart"].ToString();
                        accion = 3;
                    }

                }
            }
            if (result)
            {
                dataGridView2.Rows.Clear();
                var headcounter = new DataTable();
                var obj1 = new EnlaceDB();
                headcounter = obj1.get_Reportes(accion, dateTimePicker1.Value, Int32.Parse(NumDepart), 0);
                foreach (DataRow row in headcounter.Rows)
                {
                    string NumDepart2 = row["Departamento"].ToString();
                    string Puesto = row["Puesto"].ToString();
                    string TE = row["TotalEmpleados"].ToString();

                    DataGridViewRow fila = new DataGridViewRow();
                    dataGridView2.Rows.Add(NumDepart2, Puesto, TE);
                }

            }
        }

        private void Headcounter1_Load(object sender, EventArgs e)
        {
            var All_Departamentos = new DataTable();
            var obj1 = new EnlaceDB();
            All_Departamentos = obj1.get_Departamentos(5, 0, "", 0);
            foreach (DataRow row in All_Departamentos.Rows)
            {
                string NomDepart = row["NomDepart"].ToString();
                Departamentos.Items.Add(NomDepart);
            }

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
            dateTimePicker1.ShowUpDown = true;
        }
    }
}
