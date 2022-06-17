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
    public partial class Departamento_Mostrar : Form
    {
        public Departamento_Mostrar()
        {
            InitializeComponent();
        }

        private void Departamento_Mostrar_Load(object sender, EventArgs e)
        {
            var Departamento = new DataTable();
            var obj1 = new EnlaceDB();
            Departamento = obj1.get_Departamentos(5, 0, "", 0);
            foreach (DataRow row in Departamento.Rows)
            {
                //int x = 0;

                string NumDepart = row["NumDepart"].ToString();
                string NomDepart = row["NomDepart"].ToString();
                string SdBase = row["SdBase"].ToString();

                DataGridViewRow fila = new DataGridViewRow();
                MostrarDepartamento.Rows.Add(NumDepart, NomDepart, SdBase);
            }
        }
    }
}
