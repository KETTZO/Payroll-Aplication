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
    public partial class Nomina : Form
    {
        public Nomina()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Recibo_Nomina recibo = new Recibo_Nomina();
            recibo.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReporteGeneralDeNomina recibo = new ReporteGeneralDeNomina();
            recibo.ShowDialog();
        }
    }
}
