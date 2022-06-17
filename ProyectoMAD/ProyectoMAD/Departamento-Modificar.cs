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
    public partial class Departamento_Modificar : Form
    {
        string NumDepart;
        public Departamento_Modificar(string id)
        {
            NumDepart = id;
            InitializeComponent();
        }

        private void Departamento_Modificar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();
            
            
            bool result = true;
            Regex reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(NomDepart.Text) && !(NomDepart.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Nombre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            reg = new Regex(@"^\d+\.\d{2}?$");
            if (!reg.IsMatch(Sueldo_base.Text) && !(Sueldo_base.Text == ""))
            {
                
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Solo se permiten númreos\n -Máximo 5 dígitos", "Error en Sueldo base", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (Sueldo_base.Text =="")
            {
                Sueldo_base.Text = "0";
            }
            if (result)
            {
                tablaEmpl = obj.get_Departamentos(2, Int32.Parse(NumDepart), NomDepart.Text, float.Parse(Sueldo_base.Text));
                var respuesta = MessageBox.Show(this, "Departamento Cambiado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
