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
    public partial class Puesto_Eliminar : Form
    {
        public Puesto_Eliminar()
        {
            InitializeComponent();
        }

        private void Puesto_Eliminar_Load(object sender, EventArgs e)
        {
            var Puestos = new DataTable();
            var obj1 = new EnlaceDB();
            Puestos = obj1.get_Puestos(5, 0, "", 0, 0, 0);
            foreach (DataRow row in Puestos.Rows)
            {
                string NumPuesto = row["NumPuesto"].ToString();
                string NomPuesto = row["NomPuesto"].ToString();
                string Proporcion = row["Proporcion"].ToString();
                string Salario = row["Salario"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                EliminarPuesto.Rows.Add(NumPuesto, NomPuesto, Proporcion, Salario);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();
            bool result = true;
            string id = "";
            if (!(NumPuestoEliminar.Text == ""))
            {
                Regex reg = new Regex(@"^[0-9]{7}$");
                if (!reg.IsMatch(NumPuestoEliminar.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Ingrese un total de 7 dígitos", "Error en el Número de empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {


                if (EliminarPuesto.SelectedRows.Count > 0)
                {
                    id = EliminarPuesto.SelectedCells[0].Value.ToString();
                }
                else
                {

                    result = false;
                }

            }

            if (result)
            {
                string id2 = "";
                if (NumPuestoEliminar.Text != "")
                {
                    id2 = NumPuestoEliminar.Text;
                }
                else
                {
                    id2 = id;
                }
                tablaEmpl = obj.get_Puestos(3,Int32.Parse(id2), "", 0, 0, 0);
                var respuesta = MessageBox.Show(this, "Puesto Eliminado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
