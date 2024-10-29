namespace doan.cac_form_nhan_vien
{
    partial class bangluongnhanvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bangluongnhanvien));
            this.cbxhoten = new System.Windows.Forms.ComboBox();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bttphucap = new Guna.UI2.WinForms.Guna2Button();
            this.report = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // cbxhoten
            // 
            this.cbxhoten.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxhoten.FormattingEnabled = true;
            this.cbxhoten.Location = new System.Drawing.Point(115, 226);
            this.cbxhoten.Name = "cbxhoten";
            this.cbxhoten.Size = new System.Drawing.Size(402, 50);
            this.cbxhoten.TabIndex = 2;
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.FillColor = System.Drawing.SystemColors.Control;
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CircleButton1.Image")));
            this.guna2CircleButton1.ImageSize = new System.Drawing.Size(60, 60);
            this.guna2CircleButton1.Location = new System.Drawing.Point(12, 12);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(70, 76);
            this.guna2CircleButton1.TabIndex = 17;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 50);
            this.label1.TabIndex = 16;
            this.label1.Text = "Bảng lương nhân viên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(55, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(825, 55);
            this.label2.TabIndex = 18;
            this.label2.Text = "Chọn nhân viên và xem phiếu lương";
            // 
            // bttphucap
            // 
            this.bttphucap.BackColor = System.Drawing.SystemColors.Control;
            this.bttphucap.BorderRadius = 18;
            this.bttphucap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.bttphucap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.bttphucap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.bttphucap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.bttphucap.FillColor = System.Drawing.Color.LightSteelBlue;
            this.bttphucap.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.bttphucap.ForeColor = System.Drawing.Color.Black;
            this.bttphucap.Image = ((System.Drawing.Image)(resources.GetObject("bttphucap.Image")));
            this.bttphucap.ImageSize = new System.Drawing.Size(55, 55);
            this.bttphucap.Location = new System.Drawing.Point(552, 226);
            this.bttphucap.Name = "bttphucap";
            this.bttphucap.Size = new System.Drawing.Size(195, 59);
            this.bttphucap.TabIndex = 19;
            this.bttphucap.Text = "In";
            this.bttphucap.Click += new System.EventHandler(this.bttphucap_Click);
            // 
            // report
            // 
            this.report.Location = new System.Drawing.Point(12, 305);
            this.report.Name = "report";
            this.report.ServerReport.BearerToken = null;
            this.report.Size = new System.Drawing.Size(2322, 740);
            this.report.TabIndex = 20;
            // 
            // reportViewer2
            // 
            this.reportViewer2.Location = new System.Drawing.Point(115, 67);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(8, 20);
            this.reportViewer2.TabIndex = 21;
            // 
            // bangluongnhanvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2346, 1123);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.report);
            this.Controls.Add(this.bttphucap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.guna2CircleButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxhoten);
            this.Name = "bangluongnhanvien";
            this.Text = "bangluongnhanvien";
            this.Load += new System.EventHandler(this.bangluongnhanvien_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxhoten;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button bttphucap;
        private Microsoft.Reporting.WinForms.ReportViewer report;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
    }
}