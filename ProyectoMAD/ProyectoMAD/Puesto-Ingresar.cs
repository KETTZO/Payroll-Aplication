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
    public partial class Puesto : Form
    {
        public Puesto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            bool result = true;
            Regex reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Nombre.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de que el nombre solo contenga letras", "Error en el nombre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }




            reg = new Regex(@"^\d+\.\d{2}?$");
            if (!reg.IsMatch(Proporcion.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de que la proporción solo contenga decimales\n eviter usar comas por que solo se aceptan puntos", "Error en la Proporción", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            
            string NumDepart = "0";
            if (!(Departamento.SelectedIndex >= 0))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Por favor seleccione uno de nuestros departamentos", "Error el Departamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                var All_Departamentos = new DataTable();
                var obj1 = new EnlaceDB();
                All_Departamentos = obj1.get_Departamentos(5, 0, "", 0);
                foreach (DataRow row in All_Departamentos.Rows)
                {
                    string NomDepart = row["NomDepart"].ToString();
                    if (Departamento.Text == NomDepart)
                    {
                        NumDepart = row["NumDepart"].ToString();
                    }



                }
            }
            if (result)
            {
                var All_Puestos = new DataTable();
                var obj1 = new EnlaceDB();
                All_Puestos = obj1.get_Puestos(1, 0, Nombre.Text, float.Parse(Proporcion.Text), Int32.Parse(NumDepart), 0);
                var respuesta = MessageBox.Show(this, "Puesto Regsitrado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }



        private void Puesto_Load(object sender, EventArgs e)
        {
           
        }

        private void Puesto_Load_1(object sender, EventArgs e)
        {
            var All_Departamentos = new DataTable();
            var obj1 = new EnlaceDB();
            All_Departamentos = obj1.get_Departamentos(5, 0, "", 0);
            foreach (DataRow row in All_Departamentos.Rows)
            {
                string NomDepart = row["NomDepart"].ToString();
                Departamento.Items.Add(NomDepart);
            }
        }
    }
 
}
