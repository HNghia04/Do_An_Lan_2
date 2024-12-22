using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            LoadDonNghiPhepData();
        }

        private void LoadBangCapData()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
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
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
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
                    // Đăng ký sự kiện CellValueChanged để cập nhật tức thì
                    dataGridView2.CellValueChanged += dataGridView2_CellValueChanged;
                    // Đăng ký sự kiện CurrentCellDirtyStateChanged để nhận biết khi có thay đổi trong cell
                    dataGridView2.CurrentCellDirtyStateChanged += dataGridView2_CurrentCellDirtyStateChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void LoadHopDongData()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
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
            int maQuyenHan = 1; // Hoặc lấy giá trị từ biến nào đó
            FormQuanLy formQuanLy = new FormQuanLy(maQuyenHan);
            formQuanLy.Show();
            this.Hide();
        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            // Bạn có thể thêm chức năng tìm kiếm ở đây
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

        private void dataGridView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Nếu ô đang chỉnh sửa, cho phép cập nhật giá trị
            if (dataGridView2.IsCurrentCellDirty)
            {
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Gọi lại phương thức CellFormatting để cập nhật màu sắc khi giá trị thay đổi
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["Trạng Thái"].Index)
            {
                dataGridView2.InvalidateRow(e.RowIndex);
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bttthoiviec_Click(object sender, EventArgs e)
        {
            donthoiviec f = new donthoiviec();
            f.Show();
            this.Hide();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadDonNghiPhepData()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = @"
        SELECT 
            Nghi_phep.Ma_nghi_phep AS [Mã Nghỉ Phép],
            Nhan_vien.Ho_va_ten AS [Tên Nhân Viên],
            Nghi_phep.Ngay_bat_dau_nghi AS [Ngày Bắt Đầu],
            Nghi_phep.Ngay_ket_thuc_nghi AS [Ngày Kết Thúc],
            Nghi_phep.Trang_thai AS [Trạng Thái]
        FROM 
            Nghi_phep
        JOIN 
            Nhan_vien ON Nghi_phep.Ma_nhan_vien = Nhan_vien.Ma_nhan_vien
        ORDER BY 
            CASE WHEN Nghi_phep.Trang_thai = N'Chờ duyệt' THEN 0 ELSE 1 END,
            Nghi_phep.Ngay_bat_dau_nghi DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView4.DataSource = dataTable;

                    // Đăng ký sự kiện CellFormatting và CellValueChanged
                    dataGridView4.CellFormatting += dataGridView4_CellFormatting;
                    dataGridView4.CellValueChanged += dataGridView4_CellValueChanged;
                    dataGridView4.CurrentCellDirtyStateChanged += dataGridView4_CurrentCellDirtyStateChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra nếu cột hiện tại là cột "Trạng Thái"
            if (dataGridView4.Columns[e.ColumnIndex].Name == "Trạng Thái")
            {
                if (e.Value != null && e.Value.ToString() == "Chờ duyệt")
                {
                    // Tô màu đỏ cho hàng có trạng thái "Chờ duyệt"
                    dataGridView4.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    dataGridView4.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void dataGridView4_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView4.IsCurrentCellDirty)
            {
                dataGridView4.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView4.Columns["Trạng Thái"].Index)
            {
                dataGridView4.InvalidateRow(e.RowIndex);
            }
        }

        private void bttnghiphep_Click(object sender, EventArgs e)
            {
                try
                {
                    nghiphep1 f = new nghiphep1(); // Khởi tạo form mới
                    f.Show(); // Hiển thị form mới
                    this.Hide(); // Ẩn form hiện tại
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi chuyển form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void btthopdong_Click(object sender, EventArgs e)
        {
            hopdong f = new hopdong();
            f.Show();
            this.Hide();
        }
    }
}
