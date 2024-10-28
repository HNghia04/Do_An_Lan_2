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
                SELECT nv.Ho_va_ten, nv.Gioi_tinh, nv.So_dien_thoai, cv.Ten_chuc_vu, pb.Ten_phong_ban, nv.Ngay_bat_dau_lam_viec
                FROM Nhan_vien nv
                JOIN Chuc_vu cv ON nv.Ma_chuc_vu = cv.Ma_chuc_vu
                JOIN Phong_ban pb ON nv.Ma_phong_ban = pb.Ma_phong_ban;";

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
            string query = $@"
    SELECT 
        Nhan_vien.Ho_va_ten AS 'Họ tên',
        Nhan_vien.Gioi_tinh AS 'Giới tính',
        Nhan_vien.So_dien_thoai AS 'Số điện thoại',
        Chuc_vu.Ten_chuc_vu AS 'Chức vụ',
        Phong_ban.Ten_phong_ban AS 'Phòng ban',
        Nhan_vien.Ngay_bat_dau_lam_viec AS 'Ngày bắt đầu làm việc'
    FROM 
        Nhan_vien
    JOIN 
        Chuc_vu ON Nhan_vien.Ma_chuc_vu = Chuc_vu.Ma_chuc_vu
    JOIN 
        Phong_ban ON Nhan_vien.Ma_phong_ban = Phong_ban.Ma_phong_ban
    WHERE 
        Nhan_vien.Ho_va_ten LIKE '%' + @tuKhoa + '%' 
        OR Nhan_vien.So_dien_thoai LIKE '%' + @tuKhoa + '%'
        OR Chuc_vu.Ten_chuc_vu LIKE '%' + @tuKhoa + '%'
        OR Phong_ban.Ten_phong_ban LIKE '%' + @tuKhoa + '%'
        OR Nhan_vien.Gioi_tinh = @tuKhoa;
    ";

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
