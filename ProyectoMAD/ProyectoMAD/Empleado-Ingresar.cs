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
//using System.Net.Mail.MailAddress;

namespace ProyectoMAD
{
    public partial class Empleado_Ingresar : Form
    {
        public Empleado_Ingresar()
        {
            InitializeComponent();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();

            bool result = true;
            Regex reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Nombre.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de que el nombre solo contenga letras", "Error en el nombre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"[A-ZÑ&]{3,4}\d{6}[A-V1-9][A-Z1-9][0-9A]");
            if (!reg.IsMatch(RFC.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de ingresarlo en el formato correcto y con todas las letras en mayúsculas", "Error en el RFC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)$");
            if (!reg.IsMatch(ApPaterno.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "No se permiten espacios y solo debe utilizar letras", "Error en el Apellido Paterno", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (!reg.IsMatch(ApMaterno.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "No se permiten espacios y solo debe utilizar letras", "Error en el Apellido Materno", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /*
            reg = new Regex(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01 -\x08\x0b\x0c\x0e -\x1f\x21\x23 -\x5b\x5d -\x7f] |\\[\x01 -\x09\x0b\x0c\x0e -\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
            if (reg.IsMatch(Email.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Introduzca en Email con formato válido", "Error en el Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
            if (Email.Text == null)
                //Email.Text = "0";
                result = false;
            else
            {
                try
                {
                    Email.Text = new MailAddress(Email.Text).Address;
                }
                catch (ArgumentException)
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Introduzca en Email con formato válido", "Error en el Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // address is invalid
                }
            }
                reg = new Regex(@"[A-Z]{4}[0-9]{6}[HM]{1}[A-Z]{2}[BCDFGHJKLMNPQRSTVWXYZ]{3}([A-Z]{2})?([0-9]{2})?");
            if (!reg.IsMatch(CURP.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Utilice un formato correcto y con todas las letras en mayúsculas", "Error en el CURP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{10}$");
            if (!reg.IsMatch(TelCasa.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Ingrese un total de 10 dígitos", "Error en el Número Telefónico fijo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{10}$");
            if (!reg.IsMatch(TelCel.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Ingrese un total de 10 dígitos", "Error en el Número Celular", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}");
            if (!reg.IsMatch(Contra.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Mínimo 8 dígitos\n -Mínimo una letra mayúscula\n -Mínimo carácter especial", "Error en Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{11}$");
            if (!reg.IsMatch(NSS.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -11 dígitos en total", "Error en el Número de servicio social", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(BANCO.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de insertar solo letras", "Error en Banco", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{10}$");
            if (!reg.IsMatch(NumCuenta.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Un total de 10 dígitos", "Error en número de cuenta bancaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{1,7}$");
            if (!reg.IsMatch(Num.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Solo se permiten númreos\n -Máximo 7 dígitos", "Error en Número de casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{5}$");
            if (!reg.IsMatch(CP.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Solo se permiten númreos\n -5 dígitos", "Error en Código Postal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Pais.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en País", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Estado.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Municipio.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Municipio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Colonia.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Colonia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Calle.Text))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Calle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            int days = (DateTime.Today - fecha.Value).Days;
            //assume 365.25 days per year
            decimal years = days / 365.25m;
            if (!(years >= 18))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Necesita ser mayor de 18 años", "Fecha Inválida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string NumPuesto = "0";
            if (!(PuestoCombo.SelectedIndex >= 0))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Por favor seleccione uno de nuestros puestos", "Error en el Puesto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var All_Puestos = new DataTable();
                var obj1 = new EnlaceDB();
                All_Puestos = obj1.get_Puestos(5, 0, "", 0, 0, 0);
                foreach (DataRow row in All_Puestos.Rows)
                {
                    string NomPuesto = row["NomPuesto"].ToString();
                    if (PuestoCombo.Text == NomPuesto)
                    {
                        NumPuesto = row["NumPuesto"].ToString();
                    }
                }
            }

            if (result)
            {
                tablaEmpl = obj.get_Empleados(0, 1, RFC.Text, Nombre.Text, ApPaterno.Text, ApMaterno.Text, Email.Text, CURP.Text, fecha.Value, TelCasa.Text, TelCel.Text, Contra.Text, NSS.Text, BANCO.Text, NumCuenta.Text, Pais.Text, Estado.Text, Municipio.Text, Colonia.Text, Calle.Text, Int16.Parse(Num.Text), Int32.Parse(CP.Text), fecha_ingreso.Value, Int32.Parse(NumPuesto));
                var respuesta = MessageBox.Show(this, "Empleado Regsitrado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void Empleado_Ingresar_Load(object sender, EventArgs e)
        {
            var All_Departamentos = new DataTable();
            var obj2 = new EnlaceDB();
            All_Departamentos = obj2.get_Departamentos(5, 0, "", 0);
            foreach (DataRow row in All_Departamentos.Rows)
            {
                string NomDepart = row["NomDepart"].ToString();
                Departamento.Items.Add(NomDepart);
            }

           
        }

        private void Seleccionar_Click(object sender, EventArgs e)
        {
            PuestoCombo.Items.Clear();
            string NumDepart = "0";
            if (!(Departamento.SelectedIndex >= 0))
            {
                var respuesta = MessageBox.Show(this, "Por favor seleccione uno de nuestros departamentos", "Error el Departamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                var All_Departamentos = new DataTable();
                var obj2 = new EnlaceDB();
                All_Departamentos = obj2.get_Departamentos(5, 0, "", 0);
                foreach (DataRow row in All_Departamentos.Rows)
                {
                    string NomDepart = row["NomDepart"].ToString();
                    if (Departamento.Text == NomDepart)
                    {
                        NumDepart = row["NumDepart"].ToString();
                    }

                }
                
                var All_Puestos = new DataTable();
                var obj1 = new EnlaceDB();
                All_Puestos = obj1.get_Puestos(6, 0, "", 0, Int32.Parse(NumDepart), 0);
                foreach (DataRow row in All_Puestos.Rows)
                {
                    string NomPuesto = row["NomPuesto"].ToString();
                    PuestoCombo.Items.Add(NomPuesto);
                }
            }
        }
    }
}
