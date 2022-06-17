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
    public partial class Cambios_de_sueldo_Ingresar : Form
    {
        
        public Cambios_de_sueldo_Ingresar()
        {
            
            InitializeComponent();
        }

        private void Cambios_de_sueldo_Ingresar_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaPercepcion_Deduccion = new DataTable();
            var obj = new EnlaceDB();

            bool result = true;
            Regex reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(NomCamSld.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de que el nombre solo contenga letras", "Error en el nombre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string tipocan = TipoCan.Text;
            if (tipocan == "F")
            {
                reg = new Regex(@"^\d+\.\d{2}?$");
                if (!reg.IsMatch(Cantidad.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Solo se permiten números\n -Máximo 5 dígitos(Antes del punto)\n -Poner .00 al final de la cantidad", "Error en la Cantidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if ((tipocan == "P"))
            {
                reg = new Regex(@"^\d+\.\d{2}?$");
                if (!reg.IsMatch(Cantidad.Text))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Asegurese de que la proporción solo contenga decimales\n eviter usar comas por que solo se aceptan puntos", "Error en la Cantidad", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            if (result)
            {
                tablaPercepcion_Deduccion = obj.get_Percepcione_Deducciones(1, 0, NomCamSld.Text, Tipo.Text, float.Parse(Cantidad.Text), TipoCan.Text, "E", 0, 0, new DateTime());
                var respuesta = MessageBox.Show(this, "Cambio Regsitrado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
