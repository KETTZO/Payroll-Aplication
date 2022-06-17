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
    public partial class Departamento_Ingresar : Form
    {
        public Departamento_Ingresar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();

            bool result = true;
            Regex reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(NomDepart.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Nombre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            reg = new Regex(@"^\d+\.\d{2}?$");
            if (!reg.IsMatch(Sueldo_base.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Solo se permiten númreos\n -Máximo 5 dígitos", "Error en Sueldo base", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (result)
            {
                tablaEmpl = obj.get_Departamentos(1, 0, NomDepart.Text, float.Parse(Sueldo_base.Text));
                var respuesta = MessageBox.Show(this, "Departamento Regsitrado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void Departamento_Ingresar_Load(object sender, EventArgs e)
        {

        }
    }
}
