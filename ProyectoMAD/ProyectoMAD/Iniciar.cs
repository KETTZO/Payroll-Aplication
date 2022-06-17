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

namespace ProyectoMAD
{
    public partial class Iniciar : Form
    {
        public Iniciar()
        {
            InitializeComponent();
        }

        private void Iniciar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tipo = comboBox1.Text;

            if (tipo == "Empleado")
            {
                bool result = true;

                var Recibos_Creados = new DataTable();
                var obj1 = new EnlaceDB();
                Regex reg = new Regex(@"^[0-9]+$");
                if (!reg.IsMatch(usuario.Text))
                    result = false;
                
                if (result)
                {
                    Recibos_Creados = obj1.get_Empleados(Int32.Parse(usuario.Text), 3, null, null, null, null, null, null, new DateTime(), null, null, contra.Text, null, null, null, null, null, null, null, null, 0, 0, new DateTime(), 0);
                    foreach (DataRow row in Recibos_Creados.Rows)
                    {
                        string Contraseña = row["Contraseña"].ToString();
                        string Empleado = row["NumEmpleado"].ToString();

                        if (!(Empleado == usuario.Text && Contraseña == contra.Text))
                        {
                            result = false;
                        }
                    }
                }
                if (result)
                {
                    
                    Menu_Empleado menu = new Menu_Empleado(usuario.Text);//Cambio
                    usuario.Text = "";
                    contra.Text = "";
                    menu.ShowDialog();
                }
                else
                {
                    var respuesta = MessageBox.Show(this, "Error al Iniciar Sesión", "Usuario y/o contraseña no coinciden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else if ((tipo == "Gerente General de Nómina"))
            {
                if(usuario.Text == "admin" && contra.Text == "admin")
                {
                    usuario.Text = "";
                    contra.Text = "";
                    Menu menu = new Menu();
                    menu.ShowDialog();
                }
                else
                {
                    var respuesta = MessageBox.Show(this, "Error al Iniciar Sesión", "Usuario y/o contraseña no coinciden", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
