namespace doan.cac_form_nhan_vien
{
    partial class chamcong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chamcong));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bttthem = new Guna.UI2.WinForms.Guna2Button();
            this.dtpngaylamviec = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxhoten = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsogiolam = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtmachamcong = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bttthem);
            this.groupBox1.Controls.Add(this.dtpngaylamviec);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbxhoten);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtsogiolam);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtmachamcong);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1060, 642);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin nhân viên";
            // 
            // bttthem
            // 
            this.bttthem.BackColor = System.Drawing.SystemColors.Control;
            this.bttthem.BorderRadius = 18;
            this.bttthem.BorderStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.bttthem.BorderThickness = 1;
            this.bttthem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.bttthem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.bttthem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(221)))));
            this.bttthem.DisabledState.Font = new System.Drawing.Font("Century Gothic", 11.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttthem.DisabledState.ForeColor = System.Drawing.Color.White;
            this.bttthem.FillColor = System.Drawing.Color.White;
            this.bttthem.Font = new System.Drawing.Font("Century Gothic", 11.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttthem.ForeColor = System.Drawing.Color.Black;
            this.bttthem.Location = new System.Drawing.Point(618, 204);
            this.bttthem.Name = "bttthem";
            this.bttthem.Size = new System.Drawing.Size(313, 69);
            this.bttthem.TabIndex = 21;
            this.bttthem.Text = "Chấm công";
            this.bttthem.Click += new System.EventHandler(this.bttthem_Click);
            // 
            // dtpngaylamviec
            // 
            this.dtpngaylamviec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtpngaylamviec.Location = new System.Drawing.Point(12, 391);
            this.dtpngaylamviec.Name = "dtpngaylamviec";
            this.dtpngaylamviec.Size = new System.Drawing.Size(442, 37);
            this.dtpngaylamviec.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 33);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ngày làm việc";
            // 
            // cbxhoten
            // 
            this.cbxhoten.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxhoten.FormattingEnabled = true;
            this.cbxhoten.Location = new System.Drawing.Point(5, 235);
            this.cbxhoten.Name = "cbxhoten";
            this.cbxhoten.Size = new System.Drawing.Size(442, 38);
            this.cbxhoten.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 33);
            this.label4.TabIndex = 11;
            this.label4.Text = "Tên nhân viên";
            // 
            // txtsogiolam
            // 
            this.txtsogiolam.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtsogiolam.DefaultText = "";
            this.txtsogiolam.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtsogiolam.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtsogiolam.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtsogiolam.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtsogiolam.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtsogiolam.Font = new System.Drawing.Font("Century Gothic", 11.14286F);
            this.txtsogiolam.ForeColor = System.Drawing.Color.Black;
            this.txtsogiolam.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtsogiolam.Location = new System.Drawing.Point(12, 544);
            this.txtsogiolam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtsogiolam.Name = "txtsogiolam";
            this.txtsogiolam.PasswordChar = '\0';
            this.txtsogiolam.PlaceholderText = "Nhập số giờ sẽ làm trong ngày";
            this.txtsogiolam.SelectedText = "";
            this.txtsogiolam.Size = new System.Drawing.Size(442, 56);
            this.txtsogiolam.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 475);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 33);
            this.label3.TabIndex = 9;
            this.label3.Text = "Số giờ làm việc";
            // 
            // txtmachamcong
            // 
            this.txtmachamcong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtmachamcong.DefaultText = "";
            this.txtmachamcong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtmachamcong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtmachamcong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtmachamcong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtmachamcong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtmachamcong.Font = new System.Drawing.Font("Century Gothic", 11.14286F);
            this.txtmachamcong.ForeColor = System.Drawing.Color.Black;
            this.txtmachamcong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtmachamcong.Location = new System.Drawing.Point(5, 103);
            this.txtmachamcong.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtmachamcong.Name = "txtmachamcong";
            this.txtmachamcong.PasswordChar = '\0';
            this.txtmachamcong.PlaceholderText = "Tự động";
            this.txtmachamcong.ReadOnly = true;
            this.txtmachamcong.SelectedText = "";
            this.txtmachamcong.Size = new System.Drawing.Size(442, 56);
            this.txtmachamcong.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 33);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mã chấm công";
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
            this.guna2CircleButton1.Location = new System.Drawing.Point(8, 10);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(70, 76);
            this.guna2CircleButton1.TabIndex = 15;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 50);
            this.label1.TabIndex = 14;
            this.label1.Text = "Chấm công";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1106, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(1036, 642);
            this.dataGridView1.TabIndex = 22;
            // 
            // chamcong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2354, 1125);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.guna2CircleButton1);
            this.Controls.Add(this.label1);
            this.Name = "chamcong";
            this.Text = "chamcong";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.chamcong_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Guna.UI2.WinForms.Guna2Button bttthem;
        private System.Windows.Forms.DateTimePicker dtpngaylamviec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxhoten;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox txtsogiolam;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtmachamcong;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}