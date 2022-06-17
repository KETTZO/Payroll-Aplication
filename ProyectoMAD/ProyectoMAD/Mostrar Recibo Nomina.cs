using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.tool.xml;

namespace ProyectoMAD
{
    public partial class Mostrar_Recibo_Nomina : Form//Cambio
    {
        string NumEmpl;
        public Mostrar_Recibo_Nomina(string Num)
        {
            NumEmpl = Num;
            InitializeComponent();
        }

        private void Mostrar_Recibo_Nomina_Load(object sender, EventArgs e)
        {
            fecha.Format = DateTimePickerFormat.Custom;
            fecha.CustomFormat = "MM/yyyy";
            fecha.ShowUpDown = true;
            axAcroPDF1.Hide();
        }

        private void Generar_Click(object sender, EventArgs e)
        {
            var dt = fecha.Value;
            string Fecha2 = dt.ToString().Substring(0, 10);

            SaveFileDialog guardar = new SaveFileDialog();

            string paginahtml = Properties.Resources.plantilla.ToString();

            string filas = string.Empty;
            string aux = "n";
            bool count = true;
            bool iterar = true;
            var Recibos_Creados = new DataTable();
            var obj1 = new EnlaceDB();
            Recibos_Creados = obj1.get_Recibo(3, Int32.Parse(NumEmpl), fecha.Value, 0);
            foreach (DataRow row in Recibos_Creados.Rows)
            {
                count = false;
                string duns = row["DUNS"].ToString();
                string RazonSocial = row["RazSocial"].ToString();
                string Email = row["Email"].ToString();
                string TelEmpr1 = row["TelEmpr1"].ToString();
                string TelEmpr2 = row["TelEmpr2"].ToString();
                string Nom = row["Nombre"].ToString();
                string ApPaterno = row["ApPaterno"].ToString();
                string ApMaterno = row["ApMaterno"].ToString();
                string NSS = row["NSS"].ToString();
                string FechaFin = row["FechFinPdPago"].ToString();
                string Empleado = row["NumEmpleado"].ToString();
                string CURP = row["CURP"].ToString();
                string RFC = row["RFC"].ToString();
                string FechIni = row["FechIniPdPago"].ToString();
                string sdNeto = row["sdNeto"].ToString();
                string sdNetoLetra = row["sdNetoLetra"].ToString();
                string NombrePD = row["NombrePD"].ToString();
                string Tipo = row["Tipo"].ToString();
                string Cantidad = row["Cantidad"].ToString();
                string TipoCan = row["TipoCan"].ToString();
                string resultado = row["Resultado"].ToString();

                if (iterar)
                    {
                        string sdNeto2 = sdNeto.Substring(0, 7);
                        sdNeto2 = sdNeto2 + "$";
                        string FechIni2 = FechIni.Substring(0, 10);
                        string FechFin2 = FechaFin.Substring(0, 10);

                        paginahtml = paginahtml.Replace("@Duns", duns);
                        paginahtml = paginahtml.Replace("@Nombre", Nom);
                        paginahtml = paginahtml.Replace("@ApPaterno", ApPaterno);
                        paginahtml = paginahtml.Replace("@ApMaterno", ApMaterno);
                        paginahtml = paginahtml.Replace("@RazSocial", RazonSocial);
                        paginahtml = paginahtml.Replace("@NSS", NSS);
                        paginahtml = paginahtml.Replace("@Email", Email);
                        paginahtml = paginahtml.Replace("@NumEmpl", NumEmpl);
                        paginahtml = paginahtml.Replace("@Tel1", TelEmpr1);
                        paginahtml = paginahtml.Replace("@CURP", CURP);
                        paginahtml = paginahtml.Replace("@Tel2", TelEmpr2);
                        paginahtml = paginahtml.Replace("@RFC", RFC);
                        paginahtml = paginahtml.Replace("@FechIniPdPago", FechIni2);
                        paginahtml = paginahtml.Replace("@FechFinPdPago", FechFin2);
                        paginahtml = paginahtml.Replace("@SdNeto", sdNeto2);
                        paginahtml = paginahtml.Replace("@SdLetra", sdNetoLetra);
                        //paginahtml = paginahtml.Replace("@resultado", resultado);
                    iterar = false;
                    }

                    filas += "<tr>";
                
               
                    if (Tipo == "P")
                    {
                        if (TipoCan == "F")
                        {
                            filas += "<td>Percepción: " + NombrePD + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Valor: " + Cantidad + "$ &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cantidad Fija</td>";
                        }
                        else
                        {
                            filas += "<td>Percepción: " + NombrePD + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Valor: " + Cantidad + "% = " + resultado +"$ &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cantidad Porcentual</td>";
                        }
                    }
                    else
                    {
                        if (TipoCan == "F")
                        {
                            filas += "<td>Deducción: " + NombrePD + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Valor: " + Cantidad + "$ &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cantidad Fija</td>";
                        }
                        else
                        {
                            filas += "<td>Deducción: " + NombrePD + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Valor: " + Cantidad + "% = " + resultado +"$ &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cantidad Porcentual</td>";
                        }
                    }

                    filas += "</tr>";
                
            }
            paginahtml = paginahtml.Replace("@Filas", filas);
            if (count)
            {
                var respuesta = MessageBox.Show(this, "Posiblemente no exista un recibo con los datos ingresados", "Error al mostrar recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (FileStream fs = new FileStream(@"C:\Users\usuario\Desktop\Recibo Empleado " + NumEmpl + ".pdf", FileMode.Create))
                {
                    Document doc = new Document(PageSize.LETTER, 25, 25, 25, 25);
                    PdfWriter pw = PdfWriter.GetInstance(doc, fs);

                    doc.Open();

                    doc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(paginahtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(pw, doc, sr);
                    }
                    doc.Close();
                    pw.Close();

                }
                var respuesta = MessageBox.Show(this, "Su recibo se encuentra en el escritorio", "Recibo Guardado Exitosamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Generar.Hide();
                fecha.Hide();
                label1.Hide();
                axAcroPDF1.Show();
                axAcroPDF1.src = @"C:\Users\usuario\Desktop\Recibo Empleado " + NumEmpl + ".pdf";
            }
        }
    }
}
