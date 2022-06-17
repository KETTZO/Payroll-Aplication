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
    public partial class Departamento : Form
    {
        public Departamento()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Departamento_Ingresar empleado = new Departamento_Ingresar();
            empleado.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Departamento_Modificar_Eleccion empleado = new Departamento_Modificar_Eleccion();
            empleado.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Departamento_Mostrar empleado = new Departamento_Mostrar();
            empleado.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Departamento_Eliminar empleado = new Departamento_Eliminar();
            empleado.ShowDialog();
        }
    }
}
