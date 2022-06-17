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
    public partial class Menu_Empleado : Form
    {
        string NumEmpl;
        public Menu_Empleado(string Num)
        {
            NumEmpl = Num;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Empleado_Modificar2 empleado = new Empleado_Modificar2(NumEmpl);//Cambio
            empleado.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mostrar_Recibo_Nomina recibo = new Mostrar_Recibo_Nomina(NumEmpl);//Cambio
            recibo.ShowDialog();
        }

        private void Menu_Empleado_Load(object sender, EventArgs e)
        {

        }
    }
}
