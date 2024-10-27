using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doan.cac_form_quan_ly
{
    public partial class bangluong : Form
    {
        public bangluong()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            FormQuanLy f = new FormQuanLy();
            f.Show();
            this.Hide();
        }
    }
}
