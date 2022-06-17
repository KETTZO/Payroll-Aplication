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
    public partial class Cambios_de_sueldo_Mostrar : Form
    {
        public Cambios_de_sueldo_Mostrar()
        {
            InitializeComponent();
        }

        private void Cambios_de_sueldo_Mostrar_Load(object sender, EventArgs e)
        {
            var Cambios = new DataTable();
            var obj1 = new EnlaceDB();
            Cambios = obj1.get_Percepcione_Deducciones(4, 0, "", "", 0, "", "", 0, 0, new DateTime());
            foreach (DataRow row in Cambios.Rows)
            {
                string Codigo = row["Codigo"].ToString();
                string Nombre = row["Nombre"].ToString();
                string Tipo = row["Tipo"].ToString();
                string Cantidad = row["Cantidad"].ToString();
                string TipoCan = row["TipoCan"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                CambiosEliminar.Rows.Add(Codigo, Nombre, Tipo, Cantidad, TipoCan);
            }
        }
    }
}
