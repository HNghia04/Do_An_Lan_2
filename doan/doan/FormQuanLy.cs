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
            Nhan_vien nv ON cc.Ma_nhan_vien = nv.Ma_nhan_vien;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            themtaikhoan f = new themtaikhoan();
            f.Show();
            this.Hide();
        }
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txttimkiem.Text.Trim();
            string query = @"
        SELECT 
            cc.MaChamCong,
            nv.Ho_va_ten AS 'Họ tên',
            CONVERT(varchar, cc.NgayLamViec, 103) AS 'Ngày làm việc', 
            cc.SoGioLamViec AS 'Số giờ làm việc'
        FROM 
            ChamCong cc
        JOIN 
            Nhan_vien nv ON cc.MaNhanVien = nv.MaNhanVien
        WHERE 
            nv.Ho_va_ten LIKE '%' + @tuKhoa + '%' 
            OR cc.MaChamCong LIKE '%' + @tuKhoa + '%';";

            // Thực hiện truy vấn và cập nhật DataGridView
            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@tuKhoa", tuKhoa);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
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
