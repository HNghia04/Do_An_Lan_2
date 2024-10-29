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

namespace doan.cac_form_nhan_vien
{
    public partial class nghiphepnhanvien : Form
    {
        private string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

        public nghiphepnhanvien()
        {
            InitializeComponent();
            cbxhoten1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            FormNhanVien f = new FormNhanVien();
            f.Show();
            this.Hide();
        }
        private void LoadHoTen()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ma_nhan_vien, Ho_va_ten FROM Nhan_vien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Gán DataSource cho ComboBox với DisplayMember và ValueMember
                    cbxhoten1.DataSource = dt;
                    cbxhoten1.DisplayMember = "Ho_va_ten";   // Hiển thị tên nhân viên
                    cbxhoten1.ValueMember = "Ma_nhan_vien";  // Lưu giá trị là mã nhân viên
                }
            }
        }

        private void nghiphepnhanvien_Load(object sender, EventArgs e)
        {
            LoadHoTen();
        }
        private void btthem_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra điều kiện nhập liệu
            if (cbxhoten1.SelectedValue == null ||
                dtpngaynopdon1.Value >= dtpngaydukiennghỉviec1.Value)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra ngày kết thúc không trước ngày bắt đầu
            if (dtpngaydukiennghỉviec1.Value < dtpngaynopdon1.Value)
            {
                MessageBox.Show("Ngày dự kiến nghỉ việc không thể trước ngày nộp đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy mã đơn tự động
            string maDon = GetNextMaDon(); // Gọi hàm để lấy mã đơn tiếp theo
            string maNhanVien = cbxhoten1.SelectedValue.ToString(); // Lấy mã nhân viên
            DateTime ngayBatDau = dtpngaynopdon1.Value;
            DateTime ngayKetThuc = dtpngaydukiennghỉviec1.Value;
            string trangThai = "Chờ duyệt"; // Gán trạng thái là "Chờ duyệt"
            string hoVaTen = cbxhoten1.Text; // Lấy họ và tên nhân viên từ ComboBox

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Thêm bản ghi mới vào cơ sở dữ liệu
                string queryInsert = "INSERT INTO Nghi_phep (Ma_nghi_phep, Ma_nhan_vien, Ngay_bat_dau_nghi, Ngay_ket_thuc_nghi, Trang_thai) " +
                                     "VALUES (@MaDon, @MaNhanVien, @NgayBatDau, @NgayKetThuc, @TrangThai)";
                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@MaDon", maDon);
                    cmdInsert.Parameters.AddWithValue("@MaNhanVien", maNhanVien); // Sử dụng Ma_nhan_vien
                    cmdInsert.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    cmdInsert.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                    cmdInsert.Parameters.AddWithValue("@TrangThai", trangThai); // Gán trạng thái "Chờ duyệt"

                    try
                    {
                        cmdInsert.ExecuteNonQuery();
                        // Thông báo thành công
                        MessageBox.Show($"Nhân viên {hoVaTen} đã nộp đơn xin nghỉ phép thành công, vui lòng chờ quản lý duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi thêm đơn nghỉ phép: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private string GetNextMaDon()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Lấy giá trị tối đa từ Ma_nghi_phep
                string query = "SELECT ISNULL(MAX(Ma_nghi_phep), 0) + 1 FROM Nghi_phep";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int nextMaDon = (int)cmd.ExecuteScalar(); // Lấy mã đơn tiếp theo
                    return nextMaDon.ToString(); // Trả về mã đơn dưới dạng chuỗi
                }
            }
        }

        private void cbxhoten1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
