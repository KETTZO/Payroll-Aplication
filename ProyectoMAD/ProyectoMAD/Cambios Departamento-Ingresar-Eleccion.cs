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
    public partial class Cambios_Departamento_Ingresar_Eleccion : Form
    {
        public Cambios_Departamento_Ingresar_Eleccion()
        {
            InitializeComponent();
        }

        private void Cambios_Departamento_Ingresar_Eleccion_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
            dateTimePicker1.ShowUpDown = true;

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

            var Cambios = new DataTable();
            var obj2 = new EnlaceDB();
            Cambios = obj2.get_Percepcione_Deducciones(4, 0, "", "", 0, "", "", 0, 0, new DateTime());
            foreach (DataRow row in Cambios.Rows)
            {
                string Codigo = row["Codigo"].ToString();
                string Nombre = row["Nombre"].ToString();
                string Tipo = row["Tipo"].ToString();
                string Cantidad = row["Cantidad"].ToString();
                string TipoCan = row["TipoCan"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                CambiosEliminar.Rows.Add(Codigo, Nombre, Tipo, Cantidad, TipoCan);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();
            bool result = true;
            string id = "";
            if (!(NumDepartModificar.Text == ""))
            {

                Regex reg = new Regex(@"^[1-9]+$");
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

            result = true;
            string id3 = "";
            if (!(CambioNum.Text == ""))
            {
                Regex reg = new Regex(@"^[0-9]+$");
                if (!reg.IsMatch(CambioNum.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Ingrese un total de 7 dígitos", "Error en el Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {


                if (CambiosEliminar.SelectedRows.Count > 0)
                {
                    id3 = CambiosEliminar.SelectedCells[0].Value.ToString();
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

                string id4 = "";
                if (CambioNum.Text != "")
                {
                    id4 = CambioNum.Text;
                }
                else
                {
                    id4 = id3;
                }

                var Opcion6 = new DataTable();
                var obj4 = new EnlaceDB();
                Opcion6 = obj4.get_Percepcione_Deducciones(6, 0, "", "", 0, "", "", Int32.Parse(id2), 0, new DateTime());
                foreach (DataRow row in Opcion6.Rows)
                {
                    string Empleado = row["Empleado"].ToString();
                    tablaEmpl = obj.get_Percepcione_Deducciones(5, Int32.Parse(id4), "", "", 0, "", "", 0, Int32.Parse(Empleado), dateTimePicker1.Value);
                    var respuesta = MessageBox.Show(this, "Cambio ingresado a Empleado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                
                //Cambios_Departamento_Ingresar puesto = new Cambios_Departamento_Ingresar(id2);
                //puesto.ShowDialog();
            }
            
        }
    }
}
