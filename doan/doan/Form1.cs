using doan.cac_form_quan_ly;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace doan
{
    public partial class Form1 : Form
    {
        private bool isPasswordVisible = false;
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
    string tenTaiKhoan = txt_account.Text.Trim();
    string matKhau = txt_password.Text.Trim();
    string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

    // Mã hóa mật khẩu người dùng nhập vào
    string hashedPassword = matkhaumahoa.Encrypt(matKhau);

    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        try
        {
            conn.Open();
            string query = "SELECT Ma_quyen_han FROM Tai_khoan WHERE Ten_tai_khoan = @tenTaiKhoan AND Mat_khau = @matKhau";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@tenTaiKhoan", tenTaiKhoan);
            cmd.Parameters.AddWithValue("@matKhau", hashedPassword);

            var result = cmd.ExecuteScalar();

            if (result != null)
            {
                int maQuyenHan = (int)result;

                if (maQuyenHan == 1 || maQuyenHan == 2) // Admin hoặc Quản lý
                {
                    FormQuanLy formQuanLy = new FormQuanLy(maQuyenHan);
                    formQuanLy.Show();
                    this.Hide();
                }
                else if (maQuyenHan == 3) // Nhân viên
                {
                    FormNhanVien formNhanVien = new FormNhanVien();
                    formNhanVien.Show();
                    this.Hide();
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
            txt_password.PasswordChar = '*';
            LoadButtonImage("HidePassword");
        }
        private void LoadButtonImage(string imageName)
        {
            try
            {
                // Đường dẫn thư mục chứa hình ảnh
                string basePath = Application.StartupPath + @"\Images\";
                string imagePath = basePath + imageName + ".png";

                if (System.IO.File.Exists(imagePath))
                {
                    // Tải hình ảnh gốc
                    Image originalImage = Image.FromFile(imagePath);

                    // Tạo hình ảnh mới với kích thước 50x50
                    Bitmap resizedImage = new Bitmap(50, 50);

                    // Dùng Graphics để vẽ lại hình ảnh
                    using (Graphics g = Graphics.FromImage(resizedImage))
                    {
                        g.DrawImage(originalImage, 0, 0, 50, 50); // Vẽ ảnh với kích thước mới
                    }

                    // Gán hình ảnh đã chỉnh kích thước vào nút
                    btnTogglePassword.Image = resizedImage;

                    // Đặt chế độ hiển thị ảnh
                    btnTogglePassword.ImageAlign = ContentAlignment.MiddleCenter;
                    btnTogglePassword.TextImageRelation = TextImageRelation.Overlay; // Không có text
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy hình ảnh: {imagePath}",
                        "Lỗi Hình Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Doimatkhau f = new Doimatkhau();
            f.Show();
            this.Hide();
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                // Đang hiển thị -> Chuyển sang ẩn mật khẩu
                txt_password.PasswordChar = '*';
                LoadButtonImage("HidePassword"); // Hình con mắt đóng
                isPasswordVisible = false;
            }
            else
            {
                // Đang ẩn -> Chuyển sang hiển thị mật khẩu
                txt_password.PasswordChar = '\0'; // '\0' để hiển thị mật khẩu
                LoadButtonImage("ShowPassword"); // Hình con mắt mở
                isPasswordVisible = true;
            }
        }
    }
}
