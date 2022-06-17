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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Empleado empleado = new Empleado();
            empleado.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Nomina nomina = new Nomina();
            nomina.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Puesto1 puesto1 = new Puesto1();
            puesto1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Departamento empleado = new Departamento();
            empleado.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cambios_de_sueldo empleado = new Cambios_de_sueldo();
            empleado.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Headcounter1 empleado = new Headcounter1();
            empleado.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Empresa_Informacion empleado = new Empresa_Informacion();
            empleado.ShowDialog();
        }
    }
}
