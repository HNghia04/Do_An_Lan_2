namespace doan
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bttthoat = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_account = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_password = new Guna.UI2.WinForms.Guna2TextBox();
            this.bttdangnhap = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bttthoat
            // 
            this.bttthoat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.bttthoat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.bttthoat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.bttthoat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.bttthoat.FillColor = System.Drawing.Color.Transparent;
            this.bttthoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bttthoat.ForeColor = System.Drawing.Color.White;
            this.bttthoat.Image = ((System.Drawing.Image)(resources.GetObject("bttthoat.Image")));
            this.bttthoat.ImageSize = new System.Drawing.Size(50, 50);
            this.bttthoat.Location = new System.Drawing.Point(12, 12);
            this.bttthoat.Name = "bttthoat";
            this.bttthoat.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.bttthoat.Size = new System.Drawing.Size(65, 51);
            this.bttthoat.TabIndex = 0;
            this.bttthoat.Click += new System.EventHandler(this.bttthoat_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(12, 106);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(444, 311);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(573, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 58);
            this.label1.TabIndex = 2;
            this.label1.Text = "Đăng nhập";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txt_account
            // 
            this.txt_account.BorderRadius = 18;
            this.txt_account.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_account.DefaultText = "";
            this.txt_account.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_account.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_account.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_account.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_account.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_account.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_account.ForeColor = System.Drawing.Color.Black;
            this.txt_account.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_account.IconLeft = ((System.Drawing.Image)(resources.GetObject("txt_account.IconLeft")));
            this.txt_account.IconLeftSize = new System.Drawing.Size(30, 30);
            this.txt_account.Location = new System.Drawing.Point(489, 150);
            this.txt_account.Name = "txt_account";
            this.txt_account.PasswordChar = '\0';
            this.txt_account.PlaceholderText = "Nhập tài khoản";
            this.txt_account.SelectedText = "";
            this.txt_account.Size = new System.Drawing.Size(415, 67);
            this.txt_account.TabIndex = 3;
            // 
            // txt_password
            // 
            this.txt_password.BorderRadius = 18;
            this.txt_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_password.DefaultText = "";
            this.txt_password.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_password.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_password.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_password.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_password.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_password.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_password.ForeColor = System.Drawing.Color.Black;
            this.txt_password.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_password.IconLeft = ((System.Drawing.Image)(resources.GetObject("txt_password.IconLeft")));
            this.txt_password.IconLeftSize = new System.Drawing.Size(30, 30);
            this.txt_password.Location = new System.Drawing.Point(489, 253);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '\0';
            this.txt_password.PlaceholderText = "Nhập mật khẩu";
            this.txt_password.SelectedText = "";
            this.txt_password.Size = new System.Drawing.Size(415, 67);
            this.txt_password.TabIndex = 4;
            // 
            // bttdangnhap
            // 
            this.bttdangnhap.BorderRadius = 18;
            this.bttdangnhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.bttdangnhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.bttdangnhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.bttdangnhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.bttdangnhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bttdangnhap.Font = new System.Drawing.Font("Segoe UI", 11.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.bttdangnhap.ForeColor = System.Drawing.Color.White;
            this.bttdangnhap.Location = new System.Drawing.Point(502, 353);
            this.bttdangnhap.Name = "bttdangnhap";
            this.bttdangnhap.Size = new System.Drawing.Size(393, 64);
            this.bttdangnhap.TabIndex = 5;
            this.bttdangnhap.Text = "Đăng nhập";
            this.bttdangnhap.Click += new System.EventHandler(this.bttdangnhap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1054, 516);
            this.Controls.Add(this.bttdangnhap);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_account);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.bttthoat);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton bttthoat;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txt_account;
        private Guna.UI2.WinForms.Guna2TextBox txt_password;
        private Guna.UI2.WinForms.Guna2Button bttdangnhap;
    }
}

