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
    public partial class Empleado_Puesto : Form
    {
        public Empleado_Puesto()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool valor = true;
            if(!(comboBox1.SelectedIndex>= 0))
            {
                valor = false;
                var respuesta = MessageBox.Show(this, "Elija un empleado", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if(!(comboBox2.SelectedIndex >= 0))
            {
                valor = false;
                var respuesta = MessageBox.Show(this, "Elija un puesto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (valor)
            {
                var All_Departamentos = new DataTable();
                var obj1 = new EnlaceDB();
                All_Departamentos = obj1.get_Puestos(5, 0, "", 0, 0, 0);
                string NumPuestos = "";
                    string NomPuesto = "";
                foreach (DataRow row in All_Departamentos.Rows)
                {
                    NomPuesto = row["NomPuesto"].ToString();
                    
                    if (NomPuesto == comboBox2.Text)
                    {
                        NumPuestos = row["NumPuesto"].ToString();
                    }
                }


                var All_Departamentos2 = new DataTable();
                var obj2 = new EnlaceDB();
                string NomEmpl = "";
                string NumEmpl = "";
                All_Departamentos2 = obj2.get_Empleados(0, 5, "", "", "", "", "", "", new DateTime(), "", "", "", "", "", "", "", "", "", "", "", 0, 0, new DateTime(), 0);
                foreach (DataRow row in All_Departamentos2.Rows)
                {
                    NomEmpl = row["NomPuesto"].ToString();

                    if (NomEmpl == comboBox1.Text)
                    {
                        NumPuestos = row["NumEmpleado"].ToString();
                    }
                }

                var tablaEmpl = new DataTable();
                var obj = new EnlaceDB();
                tablaEmpl = obj.get_Puesto_Empleado(1, Int32.Parse(NumEmpl), Int32.Parse(NumPuestos));
            }
        }

        private void Empleado_Puesto_Load(object sender, EventArgs e)
        {
            var All_Departamentos = new DataTable();
            var obj1 = new EnlaceDB();
            All_Departamentos = obj1.get_Puestos(5, 0, "", 0, 0, 0);
            foreach (DataRow row in All_Departamentos.Rows)
            {
                string NumPuesto = row["NumPuesto"].ToString();
                comboBox1.Items.Add(NumPuesto);
            }

            var All_Departamentos2 = new DataTable();
            var obj2 = new EnlaceDB();
            All_Departamentos2 = obj2.get_Empleados(0, 5, "", "", "", "", "", "", new DateTime(), "", "", "", "", "", "", "", "", "", "", "", 0, 0, new DateTime(), 0);
            foreach (DataRow row in All_Departamentos2.Rows)
            {
                string NumPuesto = row["NomEmpl"].ToString();
                comboBox2.Items.Add(NumPuesto);
            }
        }
    }
}
