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
using System.Globalization;

namespace ProyectoMAD//Cambio
{
    public partial class Empleado_Modificar2 : Form
    {
        string NumEmpl;
        public Empleado_Modificar2(string Num)
        {
            NumEmpl = Num;
            InitializeComponent();
        }

        private void Empleado_Modificar2_Load(object sender, EventArgs e)
        {
            var All_Puestos = new DataTable();
            var obj1 = new EnlaceDB();
            All_Puestos = obj1.get_Puestos(5, 0, "", 0, 0, 0);
            foreach (DataRow row in All_Puestos.Rows)
            {
                string NomPuesto = row["NomPuesto"].ToString();
                PuestoCombo.Items.Add(NomPuesto);
            }

            var all_empleados = new DataTable();
            var obj2 = new EnlaceDB();
            All_Puestos = all_empleados = obj1.get_Empleados(0, 5, "", "", "", "", "", "", new DateTime(), "", "", "", "", "", "", "", "", "", "", "", 0, 0, new DateTime(), 0);
            foreach (DataRow row in all_empleados.Rows)
            {
                string NumEmpleado2 = row["NumEmpleado"].ToString();
                string Nombre = row["Nombre"].ToString();
                string ApPaterno = row["ApPaterno"].ToString();
                string ApMaterno = row["ApMaterno"].ToString();
                string Contraseña = row["Contraseña"].ToString();
                string FechNacim = row["FechNacim"].ToString();
                string CURP2 = row["CURP"].ToString();
                string NSS = row["NSS"].ToString();
                string RFC2 = row["RFC"].ToString();
                string Banco = row["Banco"].ToString();
                string NumCuenta2 = row["NumCuenta"].ToString();
                string Email2 = row["Email"].ToString();
                string TelCasa2 = row["TelCasa"].ToString();
                string TelCel2 = row["TelCel"].ToString();
                string FechIngrEmpr = row["FechIngrEmpr"].ToString();

                string PaisResd = row["PaisResd"].ToString();
                string Estado2 = row["Estado"].ToString();
                string Muncipio2 = row["Muncipio"].ToString();
                string Colonia2 = row["Colonia"].ToString();
                string Calle2 = row["Calle"].ToString();
                string NumDomicilio = row["NumDomicilio"].ToString();
                string CP2 = row["CP"].ToString();
                if (NumEmpl == NumEmpleado2)
                {
                    Contra.Text = Contraseña;

                    string fecha2 = FechNacim.Substring(0, 10);

                    fecha.Value = DateTime.ParseExact(fecha2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    CURP.Text = CURP2;
                    RFC.Text = RFC2;
                    BANCO.Text = Banco;
                    NumCuenta.Text = NumCuenta2;
                    Email.Text = Email2;
                    TelCasa.Text = TelCasa2;
                    TelCel.Text = TelCel2;
                    Pais.Text = PaisResd;
                    Estado.Text = Estado2;
                    Municipio.Text = Muncipio2;
                    Colonia.Text = Colonia2;
                    Calle.Text = Calle2;
                    Num.Text = NumDomicilio;
                    CP.Text = CP2;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tablaEmpl = new DataTable();
            var obj = new EnlaceDB();

            bool result = true;

            Regex reg = new Regex(@"[A-ZÑ&]{3,4}\d{6}[A-V1-9][A-Z1-9][0-9A]");
            if (!reg.IsMatch(RFC.Text) && !(RFC.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de ingresarlo en el formato correcto y con todas las letras en mayúsculas", "Error en el RFC", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else if (!(Email.Text == ""))
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
            if (!reg.IsMatch(CURP.Text) && !(CURP.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Utilice un formato correcto y con todas las letras en mayúsculas", "Error en el CURP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{10}$");
            if (!reg.IsMatch(TelCasa.Text) && !(TelCasa.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Ingrese un total de 10 dígitos", "Error en el Número Telefónico fijo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{10}$");
            if (!reg.IsMatch(TelCel.Text) && !(TelCel.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Ingrese un total de 10 dígitos", "Error en el Número Celular", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}");
            if (!reg.IsMatch(Contra.Text) && !(Contra.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Mínimo 8 dígitos\n -Mínimo una letra mayúscula\n -Mínimo carácter especial", "Error en Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(BANCO.Text) && !(BANCO.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de insertar solo letras", "Error en Banco", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{10}$");
            if (!reg.IsMatch(NumCuenta.Text) && !(NumCuenta.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Un total de 10 dígitos", "Error en número de cuenta bancaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[0-9]{1,7}$");
            if (!reg.IsMatch(Num.Text) && !(Num.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Solo se permiten númreos\n -Máximo 7 dígitos", "Error en Número de casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //if (Num.Text == "")
            //  Num.Text = "0";
            string cp2 = "";
            reg = new Regex(@"^[0-9]{5}$");
            if (!reg.IsMatch(CP.Text) && !(CP.Text == ""))
            {
                cp2 = CP.Text;
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de tener el formato correcto\n -Solo se permiten númreos\n -5 dígitos", "Error en Código Postal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (CP.Text == "")
                cp2 = "0";

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Pais.Text) && !(Pais.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en País", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Estado.Text) && !(Estado.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Municipio.Text) && !(Municipio.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Municipio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Colonia.Text) && !(Colonia.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Colonia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reg = new Regex(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$");
            if (!reg.IsMatch(Calle.Text) && !(Calle.Text == ""))
            {
                result = false;
                var respuesta = MessageBox.Show(this, "Asegurese de solo ingresar letras", "Error en Calle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DateTime fechita = DateTime.Now;//Cambio
            int days = (DateTime.Today - fecha.Value).Days;
            //assume 365.25 days per year
            decimal years = days / 365.25m;
            if (!(fechita.Year == fecha.Value.Year && fechita.Month == fecha.Value.Month && fechita.Day == fecha.Value.Day))
            {
                if (!(years >= 18))
                {
                    result = false;
                    var respuesta = MessageBox.Show(this, "Necesita ser mayor de 18 años", "Fecha Inválida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            string NumPuesto = "0";
            if (PuestoCombo.SelectedIndex >= 0)
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

            if (Num.Text == "")
                Num.Text = "0";

            if (CP.Text == "")
                CP.Text = "0";

            if (result)
            {
                //tablaEmpl = obj.get_Empleados(Int32.Parse(NumEmpl.Text),2, RFC.Text, "", "", "", Email.Text, CURP.Text, fecha.Value, TelCasa.Text, TelCel.Text, Contra.Text, "", BANCO.Text, NumCuenta.Text, Pais.Text, Estado.Text, Colonia.Text, Colonia.Text, Calle.Text, Int32.Parse(Num.Text), Int32.Parse(CP.Text));
                //var pruebafecha = DateTime.MinValue;
                tablaEmpl = obj.get_Empleados(Int32.Parse(NumEmpl), 2, RFC.Text, "", "", "", Email.Text, CURP.Text, 
                    fecha.Value, TelCasa.Text, TelCel.Text, Contra.Text, "", BANCO.Text, NumCuenta.Text, Pais.Text, 
                    Estado.Text, Municipio.Text, Colonia.Text, Calle.Text, Int32.Parse(Num.Text), Int32.Parse(CP.Text),
                    new DateTime(), Int32.Parse(NumPuesto));
                var respuesta = MessageBox.Show(this, "Empleado Modificado", "Acción realizada con éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
