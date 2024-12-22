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
    public partial class chamcong : Form
    {
        private float soGioLam; // Số giờ làm
        private int maChamCong;  // Mã chấm công

        public chamcong()
        {
            InitializeComponent();
            cbxhoten.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void chamcong_Load(object sender, EventArgs e)
        {
            Loadcbtennhanvien();
            dtpngaylamviec.Value = DateTime.Now;
            dtpngaylamviec.Enabled = false;// Đặt ngày giờ hiện tại cho DateTimePicker
            LoadDataGridView();
        }

        private void Loadcbtennhanvien()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = "SELECT Ma_nhan_vien, Ho_va_ten FROM Nhan_vien;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Xóa dữ liệu cũ trước khi thêm dữ liệu mới
                    cbxhoten.Items.Clear();

                    while (reader.Read())
                    {
                        string maNhanVien = reader["Ma_nhan_vien"].ToString();
                        string hoVaTen = reader["Ho_va_ten"].ToString();

                        // Thêm mã nhân viên và tên nhân viên vào ComboBox
                        cbxhoten.Items.Add(new { Text = hoVaTen, Value = maNhanVien });
                    }

                    cbxhoten.DisplayMember = "Text"; // Hiển thị tên nhân viên
                    cbxhoten.ValueMember = "Value";   // Lưu mã nhân viên
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            if (cbxhoten.SelectedItem != null && float.TryParse(txtsogiolam.Text, out soGioLam))
            {
                string maNhanVien = ((dynamic)cbxhoten.SelectedItem).Value.ToString();
                DateTime ngayGioLam = dtpngaylamviec.Value; // Ngày giờ làm

                // Kiểm tra xem nhân viên đã chấm công vào ngày này chưa
                if (CheckChamCongExists(maNhanVien, ngayGioLam))
                {
                    MessageBox.Show("Nhân viên đã chấm công vào ngày này. Không thể chấm công lại.");
                    return; // Dừng hàm nếu đã có chấm công
                }

                // Tạo mã chấm công tự động
                maChamCong = GetNextChamCongId(); // Hàm để lấy mã chấm công tiếp theo

                string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
                string insertQuery = "INSERT INTO Cham_cong (Ma_cham_cong, Ma_nhan_vien, Ngay_lam_viec, So_gio_lam_viec_trong_ngay) VALUES (@MaChamCong, @MaNhanVien, @NgayGioLam, @SoGioLam);";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(insertQuery, conn);
                        cmd.Parameters.AddWithValue("@MaChamCong", maChamCong);
                        cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                        cmd.Parameters.AddWithValue("@NgayGioLam", ngayGioLam);
                        cmd.Parameters.AddWithValue("@SoGioLam", soGioLam);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Nhân viên {cbxhoten.Text} đã chấm công thành công vào lúc {ngayGioLam.ToString("HH:mm")}, ngày {ngayGioLam.ToString("dd/MM/yyyy")}.");

                        // Gọi lại LoadDataGridView để làm mới dữ liệu hiển thị
                        LoadDataGridView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên và nhập số giờ làm.");
            }
        }

        // Phương thức kiểm tra sự tồn tại của chấm công
        private bool CheckChamCongExists(string maNhanVien, DateTime ngayGioLam)
        {
            bool exists = false;
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = "SELECT COUNT(*) FROM Cham_cong WHERE Ma_nhan_vien = @MaNhanVien AND CAST(Ngay_lam_viec AS DATE) = @NgayLam;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@NgayLam", ngayGioLam.Date); // Chỉ so sánh ngày

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    exists = (count > 0); // Nếu có bản ghi thì exists sẽ là true
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }

            return exists;
        }




        private int GetNextChamCongId()
        {
            int nextId = 1; // Mặc định bắt đầu từ 1
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = "SELECT ISNULL(MAX(Ma_cham_cong), 0) FROM Cham_cong;"; // Sử dụng ISNULL để trả về 0 nếu không có bản ghi

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        nextId = Convert.ToInt32(result) + 1; // Tăng mã chấm công lên 1
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }

            return nextId;
        }
        private void LoadDataGridView()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            DateTime currentDate = DateTime.Now.Date; // Lấy ngày hiện tại (không bao gồm thời gian)

            string query = @"
        SELECT cc.Ma_cham_cong, nv.Ho_va_ten, cc.Ngay_lam_viec 
        FROM Cham_cong cc
        JOIN Nhan_vien nv ON cc.Ma_nhan_vien = nv.Ma_nhan_vien
        WHERE CAST(cc.Ngay_lam_viec AS DATE) = @CurrentDate;"; // Lọc theo ngày hiện tại

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CurrentDate", currentDate); // Thêm tham số ngày hiện tại

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    adapter.Fill(dt);

                    // Đặt DataSource cho DataGridView
                    dataGridView1.DataSource = dt;

                    // Đặt định dạng cho cột Ngày làm việc
                    dataGridView1.Columns["Ngay_lam_viec"].DefaultCellStyle.Format = "HH:mm dd/MM/yyyy";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }


        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            FormNhanVien f = new FormNhanVien();
            f.Show();
            this.Hide();
        }
    }
}
