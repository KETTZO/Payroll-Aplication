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
    public partial class Puesto_Modificar_Eleccion : Form
    {
        public Puesto_Modificar_Eleccion()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Puesto_Modificar_Eleccion_Load(object sender, EventArgs e)
        {
            var Puestos = new DataTable();
            var obj1 = new EnlaceDB();
            Puestos = obj1.get_Puestos(5, 0, "", 0, 0, 0);
            foreach (DataRow row in Puestos.Rows)
            {
                //int x = 0;

                string NumPuesto = row["NumPuesto"].ToString();
                string NomPuesto = row["NomPuesto"].ToString();
                string Proporcion = row["Proporcion"].ToString();
                string Salario = row["Salario"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                MostrarPuesto.Rows.Add(NumPuesto, NomPuesto, Proporcion, Salario);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { 


            bool result = true;
            string id = "";
            if (!(NumPuesto_Eleccion.Text == ""))
            {

                Regex reg = new Regex(@"^[1-9]{5}$");
                if (!reg.IsMatch(NumPuesto_Eleccion.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Asegurese de que el número sea de 5 digitos", "Error en el Número", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                
                
                if (MostrarPuesto.SelectedRows.Count > 0)
                {
                    id = MostrarPuesto.SelectedCells[0].Value.ToString();
                }
                  else
                {

                    result = false;
                }

            }

            if(result)
            {
                string id2 = "";
                if (NumPuesto_Eleccion.Text != "")
                {
                    id2 = NumPuesto_Eleccion.Text;
                }
                else
                {
                    id2 = id;
                }
                Puesto_Modificar puesto = new Puesto_Modificar(id2);
                puesto.ShowDialog();
            }
        }
    }
}
