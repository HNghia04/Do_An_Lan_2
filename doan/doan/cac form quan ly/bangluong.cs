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

namespace doan.cac_form_quan_ly
{
    public partial class bangluong : Form
    {
        private string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
        public bangluong()
        {
            InitializeComponent();
            comboboxtennhanvien();
            LoadData();
            LoadLuongCoBan();
            LoadPhuCap();
        }
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT 
                b.Ma_bang_luong AS 'Mã Bảng Lương', 
                n.Ho_va_ten AS 'Tên Nhân Viên', 
                CONVERT(VARCHAR, b.Thang_tinh_luong, 103) AS 'Ngày Tính Lương', 
                b.Luong_co_ban AS 'Lương Cơ Bản', 
                b.Phu_cap AS 'Phụ Cấp', 
                b.Luong_thuc_nhan AS 'Lương Thực Nhận'
            FROM 
                Bang_luong b
            JOIN 
                Nhan_vien n ON b.Ma_nhan_vien = n.Ma_nhan_vien";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; // Hiển thị dữ liệu lên DataGridView

                // Đặt lại tên cột nếu cần thiết
                dataGridView1.Columns["Mã Bảng Lương"].HeaderText = "Mã Bảng Lương";
                dataGridView1.Columns["Tên Nhân Viên"].HeaderText = "Tên Nhân Viên";
                dataGridView1.Columns["Ngày Tính Lương"].HeaderText = "Ngày Tính Lương";
                dataGridView1.Columns["Lương Cơ Bản"].HeaderText = "Lương Cơ Bản";
                dataGridView1.Columns["Phụ Cấp"].HeaderText = "Phụ Cấp";
                dataGridView1.Columns["Lương Thực Nhận"].HeaderText = "Lương Thực Nhận";
            }
        }
        private void LoadLuongCoBan()
        {
            // Đường dẫn kết nối đến cơ sở dữ liệu của bạn
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT nv.Ho_va_ten, hd.Muc_luong_co_ban
                FROM Nhan_vien nv
                INNER JOIN Hop_dong hd ON nv.Ma_nhan_vien = hd.Ma_nhan_vien";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbxluongcoban.Items.Clear(); // Xóa tất cả các mục hiện có trong ComboBox

                    while (reader.Read())
                    {
                        string hoVaTen = reader["Ho_va_ten"].ToString();
                        string mucLuongCoBan = reader["Muc_luong_co_ban"].ToString();

                        // Định dạng chuỗi hiển thị
                        string displayText = $"{hoVaTen} ({mucLuongCoBan})";
                        cbxluongcoban.Items.Add(displayText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void LoadPhuCap()
        {
            // Đường dẫn kết nối đến cơ sở dữ liệu của bạn
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
            SELECT nv.Ho_va_ten, pc.So_tien_phu_cap
            FROM Nhan_vien nv
            INNER JOIN Phu_cap pc ON nv.Ma_nhan_vien = pc.Ma_nhan_vien";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbxphucap.Items.Clear(); // Xóa tất cả các mục hiện có trong ComboBox

                    while (reader.Read())
                    {
                        string hoVaTen = reader["Ho_va_ten"].ToString();
                        string soTienPhuCap = reader["So_tien_phu_cap"].ToString();

                        // Định dạng chuỗi hiển thị
                        string displayText = $"{hoVaTen} ({soTienPhuCap})";
                        cbxphucap.Items.Add(displayText);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            int maQuyenHan = 1; // Hoặc lấy giá trị từ biến nào đó
            FormQuanLy formQuanLy = new FormQuanLy(maQuyenHan);
            formQuanLy.Show();
            this.Hide();
        }

        private void cbxtennhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhLuongThucNhan();
        }
        private void comboboxtennhanvien()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"; // Thay thế với chuỗi kết nối của bạn

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Ho_va_ten FROM Nhan_vien"; // Giả sử cột tên nhân viên là Ten_nhan_vien
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Thêm từng tên nhân viên vào ComboBox
                        cbxtennhanvien.Items.Add(reader["Ho_va_ten"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải tên nhân viên: " + ex.Message);
                }
            }
        }
            private void bangluong_Load(object sender, EventArgs e)
            {
                // Lấy ngày, tháng, năm hiện tại
                DateTime ngayHienTai = DateTime.Now;

                // Hiển thị ngày tháng năm hiện tại vào ComboBox dưới định dạng mong muốn
                cbxthangtinhluong.Items.Add(ngayHienTai.ToString("dd/MM/yyyy"));
                cbxthangtinhluong.SelectedIndex = 0; // Chọn giá trị đầu tiên

                // Vô hiệu hóa ComboBox để người dùng không thể thay đổi
                cbxthangtinhluong.Enabled = false;
            }

        private void cbxthangtinhluong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxluongcoban_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhLuongThucNhan();
        }

        private void cbxphucap_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhLuongThucNhan();
        }
        private void TinhLuongThucNhan()
        {
            // Kiểm tra xem có mục nào được chọn không
            if (cbxluongcoban.SelectedItem == null || cbxphucap.SelectedItem == null)
            {
                txtluongthucnhan.Text = "0"; // Hoặc bạn có thể để trống
                return; // Dừng thực hiện nếu không có mục nào được chọn
            }

            // Lấy lương cơ bản từ ComboBox lương cơ bản
            string[] luongCoBanParts = cbxluongcoban.SelectedItem.ToString().Split('(');
            string mucLuongCoBanStr = luongCoBanParts[1].TrimEnd(')').Trim(); // Lấy số tiền lương cơ bản
            float mucLuongCoBan = float.Parse(mucLuongCoBanStr);

            // Lấy phụ cấp từ ComboBox phụ cấp
            string[] phuCapParts = cbxphucap.SelectedItem.ToString().Split('(');
            string soTienPhuCapStr = phuCapParts[1].TrimEnd(')').Trim(); // Lấy số tiền phụ cấp
            float soTienPhuCap = float.Parse(soTienPhuCapStr);

            // Tính lương thực nhận
            float luongThucNhan = mucLuongCoBan + soTienPhuCap;

            // Định dạng lương thực nhận với dấu phẩy
            string formattedLuongThucNhan = string.Format("{0:N0}", luongThucNhan);

            // Hiển thị lương thực nhận vào TextBox
            txtluongthucnhan.Text = formattedLuongThucNhan;
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có thông tin cần thiết không
            if (string.IsNullOrWhiteSpace(txtmabangluong.Text) ||
                cbxtennhanvien.SelectedItem == null ||
                string.IsNullOrWhiteSpace(cbxluongcoban.Text) ||
                string.IsNullOrWhiteSpace(cbxphucap.Text) ||
                string.IsNullOrWhiteSpace(txtluongthucnhan.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }

            string maBangLuong = txtmabangluong.Text;
            string tenNhanVien = cbxtennhanvien.SelectedItem.ToString();

            // Lấy lương cơ bản từ ComboBox lương cơ bản
            string[] luongCoBanParts = cbxluongcoban.SelectedItem.ToString().Split('(');
            string mucLuongCoBanStr = luongCoBanParts[1].TrimEnd(')').Trim(); // Lấy số tiền lương cơ bản
            float luongCoBan = float.Parse(mucLuongCoBanStr);

            // Lấy phụ cấp từ ComboBox phụ cấp
            string[] phuCapParts = cbxphucap.SelectedItem.ToString().Split('(');
            string soTienPhuCapStr = phuCapParts[1].TrimEnd(')').Trim(); // Lấy số tiền phụ cấp
            float phuCap = float.Parse(soTienPhuCapStr);

            float luongThucNhan = float.Parse(txtluongthucnhan.Text.Replace(",", "")); // Đảm bảo không có dấu phẩy

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
            INSERT INTO Bang_luong (Ma_bang_luong, Ma_nhan_vien, Luong_co_ban, Phu_cap, Luong_thuc_nhan, Thang_tinh_luong)
            VALUES (@MaBangLuong, (SELECT Ma_nhan_vien FROM Nhan_vien WHERE Ho_va_ten = @TenNhanVien), @LuongCoBan, @PhuCap, @LuongThucNhan, @ThangTinhLuong)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaBangLuong", maBangLuong);
                    command.Parameters.AddWithValue("@TenNhanVien", tenNhanVien);
                    command.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    command.Parameters.AddWithValue("@PhuCap", phuCap);
                    command.Parameters.AddWithValue("@LuongThucNhan", luongThucNhan);
                    command.Parameters.AddWithValue("@ThangTinhLuong", DateTime.Now); // Có thể thay đổi theo nhu cầu

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công!");
                        LoadData(); // Cập nhật DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Thêm dữ liệu không thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có nhấp vào một hàng hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy hàng đã chọn
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Điền thông tin vào các điều khiển
                txtmabangluong.Text = selectedRow.Cells["Mã Bảng Lương"].Value.ToString();
                cbxtennhanvien.SelectedItem = selectedRow.Cells["Tên Nhân Viên"].Value.ToString();
                cbxluongcoban.SelectedItem = $"{selectedRow.Cells["Tên Nhân Viên"].Value} ({selectedRow.Cells["Lương Cơ Bản"].Value})";
                cbxphucap.SelectedItem = $"{selectedRow.Cells["Tên Nhân Viên"].Value} ({selectedRow.Cells["Phụ Cấp"].Value})";
                txtluongthucnhan.Text = selectedRow.Cells["Lương Thực Nhận"].Value.ToString();

                // Nếu bạn cần hiển thị ngày tính lương (nếu có), bạn có thể thêm một cột trong DataGridView để lưu giá trị này
                // Hoặc có thể sử dụng giá trị ngày tháng hiện tại như trong mã hiện tại
            }
        }
        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            // Lấy giá trị tìm kiếm từ TextBox
            string thangTimKiem = txttimkiem.Text.Trim();

            // Kiểm tra xem có giá trị nào được nhập không
            if (string.IsNullOrWhiteSpace(thangTimKiem))
                return; // Dừng nếu không có giá trị được nhập

            // Kết nối đến cơ sở dữ liệu và thực hiện truy vấn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    b.Ma_bang_luong AS 'Mã Bảng Lương', 
                    n.Ho_va_ten AS 'Tên Nhân Viên', 
                    CONVERT(VARCHAR, b.Thang_tinh_luong, 103) AS 'Ngày Tính Lương', 
                    b.Luong_co_ban AS 'Lương Cơ Bản', 
                    b.Phu_cap AS 'Phụ Cấp', 
                    b.Luong_thuc_nhan AS 'Lương Thực Nhận'
                FROM 
                    Bang_luong b
                JOIN 
                    Nhan_vien n ON b.Ma_nhan_vien = n.Ma_nhan_vien
                WHERE 
                    CONVERT(VARCHAR, b.Thang_tinh_luong, 103) LIKE '%' + @Thang + '%'";

                    // Thêm tham số cho truy vấn
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Thang", thangTimKiem);

                    // Thực hiện truy vấn và cập nhật DataGridView
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Cập nhật DataGridView với kết quả tìm kiếm
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
                }
            }
        }

    }
}
