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
    public partial class Departamento_Eliminar : Form
    {
        public Departamento_Eliminar()
        {
            InitializeComponent();
        }

        private void Departamento_Eliminar_Load(object sender, EventArgs e)
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
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();
            bool result = true;
            string id = "";
            if (!(NumEliminar.Text == ""))
            {
                Regex reg = new Regex(@"^[1-9]{5}$");
                if (!reg.IsMatch(NumEliminar.Text))
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
                if (NumEliminar.Text != "")
                {
                    id2 = NumEliminar.Text;
                }
                else
                {
                    id2 = id;
                }
                tablaEmpl = obj.get_Departamentos(3, Int32.Parse(id2), "", 0);
                var respuesta = MessageBox.Show(this, "Departamento Eliminado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
