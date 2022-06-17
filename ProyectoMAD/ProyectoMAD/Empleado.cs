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
    public partial class Empleado : Form
    {
        public Empleado()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Empleado_Ingresar empleado = new Empleado_Ingresar();
            empleado.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Empleado_Modificar_Eleccion empleado = new Empleado_Modificar_Eleccion();
            empleado.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Empleado_Eliminar empleado = new Empleado_Eliminar();
            empleado.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Empleado_Mostrar empleado = new Empleado_Mostrar();
            empleado.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Empleado_Puesto empleado = new Empleado_Puesto();
            empleado.ShowDialog();
        }
    }
}
