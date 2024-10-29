using doan.cac_form_quan_ly;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace doan.cac_form_nhan_vien
{
    public partial class nghiviecnhanvien : Form
    {
        private string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

        public nghiviecnhanvien()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            FormNhanVien f = new FormNhanVien();
            f.Show();
            this.Hide();
        }

        private void nghiviecnhanvien_Load(object sender, EventArgs e)
        {
            LoadHoTen();
            LoadChucVu();
        }

        private void LoadChucVu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ten_chuc_vu FROM Chuc_vu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbxchucvu.Items.Add(reader["Ten_chuc_vu"].ToString());
                    }
                }
            }
        }

        // Tải dữ liệu họ tên vào ComboBox
        private void LoadHoTen()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ho_va_ten FROM Nhan_vien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbxhoten.Items.Add(reader["Ho_va_ten"].ToString());
                    }
                }
            }
        }

        private void btthem_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập
            if (string.IsNullOrWhiteSpace(cbxhoten.Text) ||
                cbxchucvu.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtlydo.Text) ||
                string.IsNullOrWhiteSpace(cbxtrangthai.Text) ||
                dtpngaynopdon.Value >= dtpngaydukienthoiviec.Value)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hoTen = cbxhoten.Text.Trim();
            string chucVu = cbxchucvu.SelectedItem?.ToString();
            DateTime ngayNopDon = dtpngaynopdon.Value;
            DateTime ngayDuKien = dtpngaydukienthoiviec.Value;
            string lyDo = txtlydo.Text.Trim();
            string trangThai = cbxtrangthai.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Lấy mã nhân viên dựa trên họ tên
                string maNhanVien = null;
                string queryGetMaNV = "SELECT Ma_nhan_vien FROM Nhan_vien WHERE Ho_va_ten = @HoTen";
                using (SqlCommand cmdGetMaNV = new SqlCommand(queryGetMaNV, conn))
                {
                    cmdGetMaNV.Parameters.AddWithValue("@HoTen", hoTen);
                    maNhanVien = cmdGetMaNV.ExecuteScalar()?.ToString();
                }

                if (string.IsNullOrEmpty(maNhanVien))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên cho họ tên đã chọn. Vui lòng kiểm tra lại họ tên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy mã đơn hiện tại cao nhất và tăng lên 1
                int maDonMoi = 1; // Khởi đầu là 1 nếu không có bản ghi nào
                string queryGetMaxMaDon = "SELECT ISNULL(MAX(Ma_don_xin_thoi_viec), 0) + 1 FROM Don_xin_thoi_viec";
                using (SqlCommand cmdGetMaxMaDon = new SqlCommand(queryGetMaxMaDon, conn))
                {
                    maDonMoi = (int)cmdGetMaxMaDon.ExecuteScalar();
                }

                // Thêm bản ghi mới vào cơ sở dữ liệu
                string queryInsert = "INSERT INTO Don_xin_thoi_viec (Ma_don_xin_thoi_viec, Ma_nhan_vien, Ho_ten, Chuc_vu, Ngay_nop_don, Ngay_du_kien_thoi_viec, Ly_do, Trang_thai) " +
                                     "VALUES (@MaDon, @MaNhanVien, @HoTen, @ChucVu, @NgayNopDon, @NgayDuKien, @LyDo, @TrangThai)";
                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@MaDon", maDonMoi);
                    cmdInsert.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdInsert.Parameters.AddWithValue("@HoTen", hoTen);
                    cmdInsert.Parameters.AddWithValue("@ChucVu", chucVu);
                    cmdInsert.Parameters.AddWithValue("@NgayNopDon", ngayNopDon);
                    cmdInsert.Parameters.AddWithValue("@NgayDuKien", ngayDuKien);
                    cmdInsert.Parameters.AddWithValue("@LyDo", lyDo);
                    cmdInsert.Parameters.AddWithValue("@TrangThai", trangThai);

                    try
                    {
                        cmdInsert.ExecuteNonQuery();
                        string message = $"Nhân viên {hoTen} đã nộp đơn thành công vào lúc {DateTime.Now:HH:mm dd/MM/yyyy}, vui lòng chờ quản lý để duyệt đơn!";
                        MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi thêm đơn thôi việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
