using doan.cac_form_quan_ly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doan
{
    public partial class Doimatkhau : Form
    {
        public Doimatkhau()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void bttdangnhap_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            string taiKhoan = txt_account.Text.Trim();
            string matKhauCu = txt_password.Text.Trim();
            string matKhauMoi = txtmatkhaumoi.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            // Kiểm tra mật khẩu cũ có đúng không
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string queryCheck = "SELECT COUNT(*) FROM Tai_khoan WHERE Ten_tai_khoan = @taiKhoan AND Mat_khau = @matKhauCu";
                SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
                cmdCheck.Parameters.AddWithValue("@taiKhoan", taiKhoan);
                cmdCheck.Parameters.AddWithValue("@matKhauCu", matKhauCu);

                conn.Open();
                int userExists = (int)cmdCheck.ExecuteScalar();

                if (userExists == 0)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu cũ không đúng.");
                    return;
                }

                // Kiểm tra nếu mật khẩu mới giống với mật khẩu cũ
                if (matKhauCu == matKhauMoi)
                {
                    MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ.");
                    return;
                }
            }

            // Cập nhật mật khẩu mới
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string queryUpdate = "UPDATE Tai_khoan SET Mat_khau = @matKhauMoi WHERE Ten_tai_khoan = @taiKhoan";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn);
                cmdUpdate.Parameters.AddWithValue("@matKhauMoi", matKhauMoi);
                cmdUpdate.Parameters.AddWithValue("@taiKhoan", taiKhoan);

                conn.Open();
                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công.");
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại. Vui lòng thử lại.");
                }
            }
        }
    }
}
