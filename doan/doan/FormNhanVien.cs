using doan.cac_form_nhan_vien;
using doan.cac_form_quan_ly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doan
{
    public partial class FormNhanVien : Form
    {
        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void bttthoat_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            chamcong f = new chamcong();
            f.Show();
            this.Hide();
        }

        private void bttphucap_Click(object sender, EventArgs e)
        {
            nghiphepnhanvien f = new nghiphepnhanvien();
            f.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            nghiviecnhanvien f = new nghiviecnhanvien();
            f.Show();
            this.Hide();
        }

        private void bttbangluong_Click(object sender, EventArgs e)
        {
            bangluongnhanvien f = new bangluongnhanvien();
            f.Show();
            this.Hide();
        }
    }
}
