namespace pryMeteorologiaLantieriLucas
{
    partial class frmMeteorologia
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
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblUbicaciones = new System.Windows.Forms.Label();
            this.trvUbicaciones = new System.Windows.Forms.TreeView();
            this.stsSeleccionado = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsSeleccionado.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(27, 52);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(73, 27);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.AutoRoundedCorners = true;
            this.dtpFecha.Checked = true;
            this.dtpFecha.Font = new System.Drawing.Font("Yu Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFecha.Location = new System.Drawing.Point(106, 43);
            this.dtpFecha.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFecha.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(297, 36);
            this.dtpFecha.TabIndex = 2;
            this.dtpFecha.Value = new System.DateTime(2025, 10, 7, 18, 59, 35, 451);
            // 
            // lblUbicaciones
            // 
            this.lblUbicaciones.AutoSize = true;
            this.lblUbicaciones.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUbicaciones.Location = new System.Drawing.Point(27, 128);
            this.lblUbicaciones.Name = "lblUbicaciones";
            this.lblUbicaciones.Size = new System.Drawing.Size(137, 27);
            this.lblUbicaciones.TabIndex = 5;
            this.lblUbicaciones.Text = "Ubicaciones";
            // 
            // trvUbicaciones
            // 
            this.trvUbicaciones.Location = new System.Drawing.Point(32, 158);
            this.trvUbicaciones.Name = "trvUbicaciones";
            this.trvUbicaciones.Size = new System.Drawing.Size(310, 284);
            this.trvUbicaciones.TabIndex = 6;
            // 
            // stsSeleccionado
            // 
            this.stsSeleccionado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.stsSeleccionado.Location = new System.Drawing.Point(0, 624);
            this.stsSeleccionado.Name = "stsSeleccionado";
            this.stsSeleccionado.Size = new System.Drawing.Size(800, 22);
            this.stsSeleccionado.TabIndex = 7;
            this.stsSeleccionado.Text = "Seleccionado:";
            this.stsSeleccionado.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.stsSeleccionado_ItemClicked);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabel1.Text = "Seleccionado:";
            // 
            // frmMeteorologia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 646);
            this.Controls.Add(this.stsSeleccionado);
            this.Controls.Add(this.trvUbicaciones);
            this.Controls.Add(this.lblUbicaciones);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Name = "frmMeteorologia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meteorologia";
            this.Load += new System.EventHandler(this.frmMeteorologia_Load);
            this.stsSeleccionado.ResumeLayout(false);
            this.stsSeleccionado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFecha;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblUbicaciones;
        private System.Windows.Forms.TreeView trvUbicaciones;
        private System.Windows.Forms.StatusStrip stsSeleccionado;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}