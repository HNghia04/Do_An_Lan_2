using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace doan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện nếu cần
        }

        private void bttthoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát ứng dụng
            }
        }

        private void bttdangnhap_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txt_account.Text.Trim(); // Tên tài khoản từ TextBox
            string matKhau = txt_password.Text.Trim(); // Mật khẩu từ TextBox

            // Định nghĩa chuỗi kết nối
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Ma_quyen_han FROM Tai_khoan WHERE Ten_tai_khoan = @tenTaiKhoan AND Mat_khau = @matKhau";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenTaiKhoan", tenTaiKhoan);
                    cmd.Parameters.AddWithValue("@matKhau", matKhau); // Sử dụng mật khẩu thô

                    // Lấy quyền hạn của tài khoản
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int maQuyenHan = (int)result;

                        // Mở form tương ứng dựa trên quyền hạn
                        if (maQuyenHan == 1) // Admin
                        {
                            FormQuanLy formQuanLy = new FormQuanLy();
                            formQuanLy.Show();
                            this.Hide(); // Ẩn form chính
                        }
                        else if (maQuyenHan == 2) // Quản lý
                        {
                            FormQuanLy formQuanLy = new FormQuanLy();
                            formQuanLy.Show();
                            this.Hide(); // Ẩn form chính
                        }
                        else if (maQuyenHan == 3) // Nhân viên
                        {
                            FormNhanVien formNhanVien = new FormNhanVien();
                            formNhanVien.Show();
                            this.Hide(); // Ẩn form chính
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
