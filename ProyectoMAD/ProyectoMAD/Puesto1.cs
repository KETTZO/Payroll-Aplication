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
    public partial class Puesto1 : Form
    {
        public Puesto1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Puesto puesto = new Puesto();
            puesto.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Puesto_Modificar_Eleccion puesto = new Puesto_Modificar_Eleccion();
            puesto.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Puesto_Eliminar puesto = new Puesto_Eliminar();
            puesto.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Puesto_Mostrar puesto = new Puesto_Mostrar();
            puesto.ShowDialog();
        }
    }
}
