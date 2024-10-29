using doan.cac_form_quan_ly;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace doan
{
    public partial class FormQuanLy : Form
    {
        private int userRole;

        public FormQuanLy(int maQuyenHan)
        {
            InitializeComponent();
            userRole = maQuyenHan; // Gán mã quyền cho userRole
            CheckPermissions(); // Gọi hàm kiểm tra quyền hạn
        }

        private void CheckPermissions()
        {
            // Nếu là quản lý (maQuyenHan == 2) thì ẩn nút tài khoản
            guna2Button6.Enabled = userRole != 2;
        }

        private void bttthoat_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void FormQuanLy_Load(object sender, EventArgs e)
        {
            // Thiết lập ComboBox cho tháng
            for (int i = 1; i <= 12; i++)
            {
                cbxtimkiemthang.Items.Add(i);
            }

            // Tải dữ liệu ban đầu cho DataGridView
            LoadData();
        }

        private void LoadData(string tuKhoa = "", int? thang = null)
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = @"
            SELECT 
                cc.Ma_cham_cong,
                nv.Ho_va_ten AS 'Họ tên',
                CONVERT(varchar, cc.Ngay_lam_viec, 103) AS 'Ngày làm việc', 
                cc.So_gio_lam_viec_trong_ngay AS 'Số giờ làm việc'
            FROM 
                Cham_cong cc
            JOIN 
                Nhan_vien nv ON cc.Ma_nhan_vien = nv.Ma_nhan_vien
            WHERE 
                (nv.Ho_va_ten LIKE '%' + @tuKhoa + '%' 
                OR cc.Ma_cham_cong LIKE '%' + @tuKhoa + '%')";

            // Nếu tháng được chọn, thêm điều kiện vào truy vấn
            if (thang.HasValue)
            {
                query += " AND MONTH(cc.Ngay_lam_viec) = @thang";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@tuKhoa", tuKhoa);
                    if (thang.HasValue)
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@thang", thang.Value);
                    }
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    guna2DataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txttimkiem.Text.Trim();
            int? thang = cbxtimkiemthang.SelectedItem != null ? (int?)Convert.ToInt32(cbxtimkiemthang.SelectedItem) : null;
            LoadData(tuKhoa, thang);
        }

        private void cbxtimkiemthang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tuKhoa = txttimkiem.Text.Trim();
            int? thang = cbxtimkiemthang.SelectedItem != null ? (int?)Convert.ToInt32(cbxtimkiemthang.SelectedItem) : null;
            LoadData(tuKhoa, thang);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            themtaikhoan f = new themtaikhoan();
            f.Show();
            this.Hide();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý sự kiện click cho DataGridView nếu cần
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            themnhanvien f = new themnhanvien();
            f.Show();
            this.Hide();
        }

        private void bttphucap_Click(object sender, EventArgs e)
        {
            phucap f = new phucap();
            f.Show();
            this.Hide();
        }

        private void bttbangluong_Click(object sender, EventArgs e)
        {
            bangluong f = new bangluong();
            f.Show();
            this.Hide();
        }

        private void bttbangdon_Click(object sender, EventArgs e)
        {
            cacbangdon f = new cacbangdon();
            f.Show();
            this.Hide();
        }
    }
}
