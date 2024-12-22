using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace doan.cac_form_quan_ly
{
    public partial class themtaikhoan : Form
    {
        public themtaikhoan()
        {
            InitializeComponent();
        }

        private void themtaikhoan_Load(object sender, EventArgs e)
        {
            LoadQuyenHan();
            LoadData();
        }

        public void LoadData()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            // Câu truy vấn SQL
            string query = "SELECT [Ten_tai_khoan], [Mat_khau], [Ma_quyen_han] FROM [Tai_khoan]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt); // Đổ dữ liệu vào DataTable
                    dataGridView1.DataSource = dt; // Gán DataTable vào DataGridView

                    // Đặt lại tên các cột trong DataGridView
                    dataGridView1.Columns["Ten_tai_khoan"].HeaderText = "Tài khoản";
                    dataGridView1.Columns["Mat_khau"].HeaderText = "Mật khẩu (mã hóa)";
                    dataGridView1.Columns["Ma_quyen_han"].HeaderText = "Mã quyền hạn";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void LoadQuyenHan()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "SELECT Ma_quyen_han, Ten_quyen_han FROM Quyen_han";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dtQuyenHan = new DataTable();
                adapter.Fill(dtQuyenHan);

                cmbquyenhan.DataSource = dtQuyenHan;
                cmbquyenhan.DisplayMember = "Ten_quyen_han";
                cmbquyenhan.ValueMember = "Ma_quyen_han";
            }
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txttaikhoan.Text;
            string matKhau = txtmatkhau.Text;
            int maQuyenHan = (int)cmbquyenhan.SelectedValue;

            // Kiểm tra nếu tất cả các trường đều được điền
            if (string.IsNullOrWhiteSpace(tenTaiKhoan) || string.IsNullOrWhiteSpace(matKhau) || cmbquyenhan.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin (Tên tài khoản, Mật khẩu và Quyền hạn).");
                return; // Dừng hàm nếu thiếu thông tin
            }

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
                string encryptedPassword = matkhaumahoa.Encrypt(matKhau); // Dùng lớp mã hóa đã tạo

                // Kiểm tra xem tên tài khoản đã tồn tại hay chưa
                string checkQuery = "SELECT COUNT(*) FROM Tai_khoan WHERE Ten_tai_khoan = @TenTaiKhoan";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);

                connection.Open();
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại. Vui lòng chọn tên khác.");
                    connection.Close();
                    return;
                }

                // Thêm tài khoản mới với mật khẩu đã mã hóa
                string query = "INSERT INTO Tai_khoan (Ten_tai_khoan, Mat_khau, Ma_quyen_han) VALUES (@TenTaiKhoan, @MatKhau, @MaQuyenHan)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                command.Parameters.AddWithValue("@MatKhau", encryptedPassword);  // Lưu mật khẩu đã mã hóa
                command.Parameters.AddWithValue("@MaQuyenHan", maQuyenHan);

                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Tài khoản đã được tạo thành công!");
                LoadData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i < dataGridView1.RowCount - 1)
            {
                txttaikhoan.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string encryptedPassword = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtmatkhaumahoa.Text = encryptedPassword;  // Hiển thị mật khẩu đã mã hóa
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttaikhoan.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa.");
                return;
            }

            string tenTaiKhoan = txttaikhoan.Text;

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "DELETE FROM Tai_khoan WHERE Ten_tai_khoan = @TenTaiKhoan";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Tài khoản đã được xóa thành công!");
                LoadData();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các trường cần thiết đã được điền đầy đủ hay chưa
            if (string.IsNullOrEmpty(txttaikhoan.Text) || string.IsNullOrEmpty(txtmatkhau.Text) ||
                string.IsNullOrEmpty(txtmatkhaumahoa.Text) || cmbquyenhan.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ các trường thông tin.");
                return;
            }

            string tenTaiKhoan = txttaikhoan.Text;
            string matKhauGoc = txtmatkhau.Text;
            string matKhauMaHoa = txtmatkhaumahoa.Text;
            int maQuyenHan = (int)cmbquyenhan.SelectedValue;

            // Kiểm tra xem tên tài khoản có tồn tại trong cơ sở dữ liệu không
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string checkQuery = "SELECT COUNT(*) FROM Tai_khoan WHERE Ten_tai_khoan = @TenTaiKhoan";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);

                connection.Open();
                int count = (int)checkCommand.ExecuteScalar();

                if (count == 0)
                {
                    MessageBox.Show("Tên tài khoản không tồn tại.");
                    connection.Close();
                    return;
                }
                connection.Close();
            }

            // Mã hóa mật khẩu mới trước khi cập nhật
            string newEncryptedPassword = matkhaumahoa.Encrypt(matKhauGoc);

            // Cập nhật tài khoản và mật khẩu
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string updateQuery = "UPDATE Tai_khoan SET Mat_khau = @MatKhau, Ma_quyen_han = @MaQuyenHan WHERE Ten_tai_khoan = @TenTaiKhoan";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                command.Parameters.AddWithValue("@MatKhau", newEncryptedPassword);  // Lưu mật khẩu đã mã hóa
                command.Parameters.AddWithValue("@MaQuyenHan", maQuyenHan);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Tài khoản đã được cập nhật thành công!");
                LoadData(); // Tải lại dữ liệu sau khi cập nhật
            }
        }

        private void bttmahoamatkhau_Click_1(object sender, EventArgs e)
        {
            string key = txtkeymahoa.Text;  // Nhập key mã hóa

            if (key != matkhaumahoa.GetEncryptionKey())  // Kiểm tra khóa nhập vào có đúng không
            {
                MessageBox.Show("Key mã hóa không chính xác.");
                return;
            }

            string encryptedPassword = txtmatkhaumahoa.Text;
            string decryptedPassword = matkhaumahoa.Decrypt(encryptedPassword);  // Giải mã mật khẩu
            txtmatkhau.Text = decryptedPassword;  // Hiển thị mật khẩu gốc
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            int maQuyenHan = 1; // Hoặc lấy giá trị từ biến nào đó
            FormQuanLy formQuanLy = new FormQuanLy(maQuyenHan);
            formQuanLy.Show();
            this.Hide();
        }
    }
}
