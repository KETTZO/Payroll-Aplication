namespace ProyectoMAD
{
    partial class Cambios_Departamento_Ingresar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.FechaAplicacion = new System.Windows.Forms.DateTimePicker();
            this.TipoCan = new System.Windows.Forms.ComboBox();
            this.Tipo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NomCamSld = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Fecha de aplicación:";
            // 
            // FechaAplicacion
            // 
            this.FechaAplicacion.Location = new System.Drawing.Point(167, 191);
            this.FechaAplicacion.Name = "FechaAplicacion";
            this.FechaAplicacion.Size = new System.Drawing.Size(295, 20);
            this.FechaAplicacion.TabIndex = 49;
            // 
            // TipoCan
            // 
            this.TipoCan.FormattingEnabled = true;
            this.TipoCan.Items.AddRange(new object[] {
            "F",
            "P"});
            this.TipoCan.Location = new System.Drawing.Point(166, 151);
            this.TipoCan.Name = "TipoCan";
            this.TipoCan.Size = new System.Drawing.Size(296, 21);
            this.TipoCan.TabIndex = 48;
            // 
            // Tipo
            // 
            this.Tipo.FormattingEnabled = true;
            this.Tipo.Items.AddRange(new object[] {
            "P",
            "D"});
            this.Tipo.Location = new System.Drawing.Point(166, 68);
            this.Tipo.Name = "Tipo";
            this.Tipo.Size = new System.Drawing.Size(296, 21);
            this.Tipo.TabIndex = 47;
            this.Tipo.Tag = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(468, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "F=Fija, P=Porcentaje";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(69, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Tipo de Cantidad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(468, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "P=Percepción, D=Deducción";
            // 
            // Cantidad
            // 
            this.Cantidad.Location = new System.Drawing.Point(166, 110);
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Size = new System.Drawing.Size(296, 20);
            this.Cantidad.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Cantidad:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(365, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 42);
            this.button2.TabIndex = 41;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(167, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 42);
            this.button1.TabIndex = 40;
            this.button1.Text = "Ingresar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Nombre de cambio de sueldo:";
            // 
            // NomCamSld
            // 
            this.NomCamSld.Location = new System.Drawing.Point(166, 29);
            this.NomCamSld.Name = "NomCamSld";
            this.NomCamSld.Size = new System.Drawing.Size(296, 20);
            this.NomCamSld.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Tipo:";
            // 
            // Cambios_Departamento_Ingresar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 324);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FechaAplicacion);
            this.Controls.Add(this.TipoCan);
            this.Controls.Add(this.Tipo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Cantidad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NomCamSld);
            this.Controls.Add(this.label1);
            this.Name = "Cambios_Departamento_Ingresar";
            this.Text = "Cambio_Departamento_Ingresar";
            this.Load += new System.EventHandler(this.Cambios_Departamento_Ingresar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker FechaAplicacion;
        private System.Windows.Forms.ComboBox TipoCan;
        private System.Windows.Forms.ComboBox Tipo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Cantidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NomCamSld;
        private System.Windows.Forms.Label label1;
    }
}