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
    public partial class Departamento_Modificar_Eleccion : Form
    {
        public Departamento_Modificar_Eleccion()
        {
            InitializeComponent();
        }

        private void Departamento_Modificar_Eleccion_Load(object sender, EventArgs e)
        {
            var Departamento = new DataTable();
            var obj1 = new EnlaceDB();
            Departamento = obj1.get_Departamentos(5, 0, "", 0);
            foreach (DataRow row in Departamento.Rows)
            {
                //int x = 0;

                string NumDepart = row["NumDepart"].ToString();
                string NomDepart = row["NomDepart"].ToString();
                string SdBase = row["SdBase"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                ModificarDepartamento.Rows.Add(NumDepart, NomDepart, SdBase);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

       
            bool result = true;
            string id = "";
            if (!(NumDepartModificar.Text == ""))
            {

                Regex reg = new Regex(@"^[1-9]{5}$");
                if (!reg.IsMatch(NumDepartModificar.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Asegurese de que el número sea de 5 digitos", "Error en el Número", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {


                if (ModificarDepartamento.SelectedRows.Count > 0)
                {
                    id = ModificarDepartamento.SelectedCells[0].Value.ToString();
                }
                else
                {

                    result = false;
                }

            }

            if (result)
            {
                string id2 = "";
                if (NumDepartModificar.Text != "")
                {
                    id2 = NumDepartModificar.Text;
                }
                else
                {
                    id2 = id;
                }
                Departamento_Modificar puesto = new Departamento_Modificar(id2);
                puesto.ShowDialog();
            }
            
        }
    }
}
