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
    public partial class cacbangdon : Form
    {
        public cacbangdon()
        {
            InitializeComponent();
            LoadBangCapData();
            LoadDonXinThoiViecData();
            LoadHopDongData();
        }
        private void LoadBangCapData()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = @"
                SELECT 
                    Bang_cap.Ma_bang_cap AS [Mã Bằng Cấp],
                    Nhan_vien.Ho_va_ten AS [Tên Nhân Viên],
                    Bang_cap.Ten_bang_cap AS [Tên Bằng Cấp],
                    Bang_cap.Ngay_cap_bang AS [Ngày Cấp Bằng],
                    Bang_cap.Hang_bang AS [Hạng Bằng],
                    Bang_cap.Chuyen_nganh_dao_tao AS [Chuyên Ngành Đào Tạo]
                FROM 
                    Bang_cap
                JOIN 
                    Nhan_vien ON Bang_cap.Ma_nhan_vien = Nhan_vien.Ma_nhan_vien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void LoadDonXinThoiViecData()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = @"
    SELECT 
        Don_xin_thoi_viec.Ma_don_xin_thoi_viec AS [Mã Đơn],
        Nhan_vien.Ho_va_ten AS [Tên Nhân Viên],
        Nhan_vien.ten_chuc_vu AS [Chức Vụ],
        Don_xin_thoi_viec.Ngay_nop_don AS [Ngày Nộp Đơn],
        Don_xin_thoi_viec.Ngay_du_kien_thoi_viec AS [Ngày Dự Kiến Thôi Việc],
        Don_xin_thoi_viec.Ly_do AS [Lý Do],
        Don_xin_thoi_viec.Trang_thai AS [Trạng Thái]
    FROM 
        Don_xin_thoi_viec
    JOIN 
        Nhan_vien ON Don_xin_thoi_viec.Ma_nhan_vien = Nhan_vien.Ma_nhan_vien
    ORDER BY 
        CASE WHEN Don_xin_thoi_viec.Trang_thai = N'Chờ duyệt' THEN 0 ELSE 1 END, 
        Don_xin_thoi_viec.Ngay_nop_don DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView2.DataSource = dataTable;

                    // Đăng ký sự kiện CellFormatting để tô màu cho các đơn chờ duyệt
                    dataGridView2.CellFormatting += dataGridView2_CellFormatting;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void LoadHopDongData()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = @"
        SELECT 
            Hop_dong.Ma_hop_dong AS [Mã Hợp Đồng],
            Nhan_vien.Ho_va_ten AS [Tên Nhân Viên],
            Hop_dong.Ngay_bat_dau AS [Ngày Bắt Đầu],
            Hop_dong.Ngay_ket_thuc AS [Ngày Kết Thúc],
            Hop_dong.Muc_luong_co_ban AS [Mức Lương Cơ Bản]
        FROM 
            Hop_dong
        JOIN 
            Nhan_vien ON Hop_dong.Ma_nhan_vien = Nhan_vien.Ma_nhan_vien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView3.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void cacbangdon_Load(object sender, EventArgs e)
        {

        }

        private void bttthoat_Click(object sender, EventArgs e)
        {
            FormQuanLy f = new FormQuanLy();
            f.Show();
            this.Hide();
        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra nếu cột hiện tại là cột "Trạng Thái"
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Trạng Thái")
            {
                if (e.Value != null && e.Value.ToString() == "Chờ duyệt")
                {
                    // Tô màu đỏ cho hàng có trạng thái "Chờ duyệt"
                    dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bttbangcap_Click(object sender, EventArgs e)
        {
            bangcap f = new bangcap();
            f.Show();
            this.Hide();
        }
    }
}
