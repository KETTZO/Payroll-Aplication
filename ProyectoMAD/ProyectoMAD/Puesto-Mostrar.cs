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
    public partial class Puesto_Mostrar : Form
    {
        public Puesto_Mostrar()
        {
            InitializeComponent();
        }

        private void Puesto_Mostrar_Load(object sender, EventArgs e)
        {
            var Puestos = new DataTable();
            var obj1 = new EnlaceDB();
            Puestos = obj1.get_Puestos(7, 0, "", 0, 0, 0);
            foreach (DataRow row in Puestos.Rows)
            {
                string NumPuesto = row["NumPuesto"].ToString();
                string NomPuesto = row["NomPuesto"].ToString();
                string Proporcion = row["Proporcion"].ToString();
                string Salario = row["Salario"].ToString();
                string Departamento = row["Departamento"].ToString();
                string NomDepart = row["NomDepart"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                MostrarPuesto.Rows.Add(NumPuesto, NomPuesto, Proporcion, Salario, Departamento, NomDepart);
            }
        }
    }
}
