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
    public partial class Puesto_Modificar : Form
    {
        string numpuesto;
        public Puesto_Modificar(string id)
        {
            numpuesto = id;
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result = true;
            Regex reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(NomPuesto.Text) && !(NomPuesto.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de que el nombre solo contenga letras", "Error en el nombre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            reg = new Regex(@"^\d+\.\d{2}?$");
            if (!reg.IsMatch(Proporcion.Text) && !(Proporcion.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de que la proporción solo contenga decimales\n eviter usar comas por que solo se aceptan puntos", "Error en la Proporción", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (Proporcion.Text == "")
            {
                Proporcion.Text = "0";
            }
            if (result)
            {
                var All_Puestos = new DataTable();
                var obj1 = new EnlaceDB();
                All_Puestos = obj1.get_Puestos(2,  Int32.Parse(numpuesto), NomPuesto.Text, float.Parse(Proporcion.Text), 0, 0);
                var respuesta = MessageBox.Show(this, "Puesto Modificado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void Puesto_Modificar_Load(object sender, EventArgs e)
        {

        }
    }
}
