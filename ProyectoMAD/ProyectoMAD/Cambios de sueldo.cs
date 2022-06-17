using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoMAD
{
    public partial class Cambios_de_sueldo : Form
    {
        public Cambios_de_sueldo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cambios_Empleado_Ingresar_Eleccion cambio = new Cambios_Empleado_Ingresar_Eleccion();
            cambio.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cambios_de_sueldo_Mostrar cambio = new Cambios_de_sueldo_Mostrar();
            cambio.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Cambios_Departamento_Ingresar_Eleccion cambio = new Cambios_Departamento_Ingresar_Eleccion();
            cambio.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cambios_de_sueldo_Ingresar cambio = new Cambios_de_sueldo_Ingresar();
            cambio.ShowDialog();
        }
    }
}
