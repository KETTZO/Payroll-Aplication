namespace ProyectoMAD
{
    partial class Departamento_Ingresar
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
            this.button1 = new System.Windows.Forms.Button();
            this.Sueldo_base = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NomDepart = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 42);
            this.button1.TabIndex = 9;
            this.button1.Text = "Ingresar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Sueldo_base
            // 
            this.Sueldo_base.Location = new System.Drawing.Point(164, 89);
            this.Sueldo_base.Name = "Sueldo_base";
            this.Sueldo_base.Size = new System.Drawing.Size(296, 20);
            this.Sueldo_base.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Sueldo base:";
            // 
            // NomDepart
            // 
            this.NomDepart.Location = new System.Drawing.Point(164, 47);
            this.NomDepart.Name = "NomDepart";
            this.NomDepart.Size = new System.Drawing.Size(296, 20);
            this.NomDepart.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nombre de departamento:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(363, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 42);
            this.button2.TabIndex = 10;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Departamento_Ingresar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 239);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Sueldo_base);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NomDepart);
            this.Controls.Add(this.label1);
            this.Name = "Departamento_Ingresar";
            this.Text = "Departamento-Ingresar";
            this.Load += new System.EventHandler(this.Departamento_Ingresar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Sueldo_base;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NomDepart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}