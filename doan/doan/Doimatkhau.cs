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
        private bool isPasswordVisible = false;
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

            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            // Kiểm tra tài khoản và mật khẩu cũ
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Lấy mật khẩu đã mã hóa từ cơ sở dữ liệu
                string queryGetOldPassword = "SELECT Mat_khau FROM Tai_khoan WHERE Ten_tai_khoan = @taiKhoan";
                SqlCommand cmdGetOldPassword = new SqlCommand(queryGetOldPassword, conn);
                cmdGetOldPassword.Parameters.AddWithValue("@taiKhoan", taiKhoan);

                conn.Open();
                object result = cmdGetOldPassword.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại.");
                    return;
                }

                string matKhauCuDatabase = result.ToString();

                // So sánh mật khẩu cũ chưa mã hóa với mật khẩu đã mã hóa trong cơ sở dữ liệu
                if (matKhauCuDatabase != matkhaumahoa.Encrypt(matKhauCu))  // Mã hóa mật khẩu cũ và so sánh
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

            // Mã hóa mật khẩu mới
            string encryptedPasswordNew = matkhaumahoa.Encrypt(matKhauMoi); // Dùng lớp mã hóa để mã hóa mật khẩu mới

            // Cập nhật mật khẩu mới vào cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string queryUpdate = "UPDATE Tai_khoan SET Mat_khau = @matKhauMoi WHERE Ten_tai_khoan = @taiKhoan";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn);
                cmdUpdate.Parameters.AddWithValue("@matKhauMoi", encryptedPasswordNew); // Lưu mật khẩu mới đã mã hóa
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

        private void Doimatkhau_Load(object sender, EventArgs e)
        {
            txt_password.PasswordChar = '*';
            txtmatkhaumoi.PasswordChar = '*';
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
                    btnTogglePassword1.Image = resizedImage;

                    // Đặt chế độ hiển thị ảnh
                    btnTogglePassword1.ImageAlign = ContentAlignment.MiddleCenter;
                    btnTogglePassword1.TextImageRelation = TextImageRelation.Overlay; // Không có text
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

        private void btnTogglePassword1_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                // Đang hiển thị -> Chuyển sang ẩn mật khẩu
                txtmatkhaumoi.PasswordChar = '*';
                txt_password.PasswordChar = '*';
                LoadButtonImage("HidePassword"); // Hình con mắt đóng
                isPasswordVisible = false;
            }
            else
            {
                // Đang ẩn -> Chuyển sang hiển thị mật khẩu
                txtmatkhaumoi.PasswordChar = '\0';
                txt_password.PasswordChar = '\0';// '\0' để hiển thị mật khẩu
                LoadButtonImage("ShowPassword"); // Hình con mắt mở
                isPasswordVisible = true;
            }
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
