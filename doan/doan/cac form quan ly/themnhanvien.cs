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
    public partial class themnhanvien : Form
    {
        public themnhanvien()
        {
            InitializeComponent();
            LoadData();
            LoadChucVu();
            LoadPhongBan();
            cmbgioitinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbchucvu.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbphongban.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadData()
        {
            // Khởi tạo kết nối với cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                // Chuẩn bị truy vấn SQL để lấy dữ liệu
                string query = @"
            SELECT 
                Nhan_vien.Ho_va_ten AS [Họ và Tên], 
                Nhan_vien.Gioi_tinh AS [Giới Tính], 
                Nhan_vien.So_dien_thoai AS [Số Điện Thoại], 
                Chuc_vu.Ten_chuc_vu AS [Tên Chức Vụ], 
                (SELECT Ten_phong_ban FROM Phong_ban WHERE Phong_ban.Ma_phong_ban = Nhan_vien.Ma_phong_ban) AS [Tên Phòng Ban], 
                Nhan_vien.Ngay_bat_dau_lam_viec AS [Ngày Bắt Đầu Làm Việc] 
            FROM Nhan_vien
            JOIN Chuc_vu ON Nhan_vien.Ma_chuc_vu = Chuc_vu.Ma_chuc_vu";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                // Mở kết nối, điền dữ liệu vào DataTable
                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                // Gán DataTable cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }
        private void LoadPhongBan()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "SELECT Ma_phong_ban, Ten_phong_ban FROM Phong_ban";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                cmbphongban.DataSource = dataTable;
                cmbphongban.DisplayMember = "Ten_phong_ban"; // Tên hiển thị trong ComboBox
                cmbphongban.ValueMember = "Ma_phong_ban"; // Giá trị thực tế của ComboBox
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Kiểm tra xem số điện thoại chỉ chứa các chữ số
            return phoneNumber.All(char.IsDigit) && (phoneNumber.Length == 10 || phoneNumber.Length == 11);
        }

        private void LoadChucVu()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "SELECT Ma_chuc_vu, Ten_chuc_vu FROM Chuc_vu";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                cmbchucvu.DataSource = dataTable;
                cmbchucvu.DisplayMember = "Ten_chuc_vu"; // Tên hiển thị trong ComboBox
                cmbchucvu.ValueMember = "Ma_chuc_vu"; // Giá trị thực tế của ComboBox
            }
        }
        private void AddNhanVien(string hoVaTen, string gioiTinh, string soDienThoai, int maChucVu, int maPhongBan, DateTime ngayBatDau)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = @"
        INSERT INTO Nhan_vien (Ho_va_ten, Gioi_tinh, So_dien_thoai, Ma_chuc_vu, Ma_phong_ban, Ngay_bat_dau_lam_viec) 
        VALUES (@HoVaTen, @GioiTinh, @SoDienThoai, @MaChucVu, @MaPhongBan, @NgayBatDau)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HoVaTen", hoVaTen);
                    command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@MaChucVu", maChucVu);
                    command.Parameters.AddWithValue("@MaPhongBan", maPhongBan);
                    command.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            int maQuyenHan = 1; // Gán giá trị cho maQuyenHan (hoặc lấy từ nguồn khác)

            // Khởi tạo FormQuanLy với tham số maQuyenHan
            FormQuanLy formQuanLy = new FormQuanLy(maQuyenHan);
            formQuanLy.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i < dataGridView1.RowCount - 1)
            {
                txthoten.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                cmbgioitinh.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtsdt.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                cmbchucvu.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                cmbphongban.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                dtpngaybatdau.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            }
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường thông tin đã đầy đủ chưa
            if (string.IsNullOrEmpty(txthoten.Text) ||
                string.IsNullOrEmpty(txtsdt.Text) ||
                cmbgioitinh.SelectedItem == null ||
                cmbchucvu.SelectedValue == null ||
                cmbphongban.SelectedValue == null||
                dtpngaybatdau.Value == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }

            // Kiểm tra định dạng số điện thoại
            if (!IsValidPhoneNumber(txtsdt.Text))
            {
                MessageBox.Show("Số điện thoại phải là số và có 10 hoặc 11 chữ số.");
                return;
            }

            if (dtpngaybatdau.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày bắt đầu làm việc không thể trong tương lai.");
                return;
            }

            // Thêm nhân viên
            AddNhanVien(txthoten.Text, cmbgioitinh.SelectedItem.ToString(), txtsdt.Text,
                         (int)cmbchucvu.SelectedValue, (int)cmbphongban.SelectedValue, dtpngaybatdau.Value);
            MessageBox.Show("Thêm thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadData(); // Tải lại dữ liệu vào DataGridView
        }

        private void bttsua_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường thông tin đã đầy đủ chưa
            if (string.IsNullOrEmpty(txthoten.Text) ||
                string.IsNullOrEmpty(txtsdt.Text) ||
                cmbgioitinh.SelectedItem == null ||
                cmbchucvu.SelectedValue == null ||
                cmbphongban.SelectedValue == null ||
                dtpngaybatdau.Value == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }

            // Kiểm tra định dạng số điện thoại
            if (!IsValidPhoneNumber(txtsdt.Text))
            {
                MessageBox.Show("Số điện thoại phải là số và có 10 hoặc 11 chữ số.");
                return;
            }

            if (dtpngaybatdau.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày bắt đầu làm việc không thể trong tương lai.");
                return;
            }

            // Cập nhật nhân viên
            UpdateNhanVien(txthoten.Text, cmbgioitinh.SelectedItem.ToString(), txtsdt.Text,
                           (int)cmbchucvu.SelectedValue, (int)cmbphongban.SelectedValue, dtpngaybatdau.Value);
            MessageBox.Show("Sửa thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            LoadData(); // Tải lại dữ liệu vào DataGridView
        }

        private void UpdateNhanVien(string hoTen, string gioiTinh, string soDienThoai, int maChucVu, int maPhongBan, DateTime ngayBatDau)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = @"
            UPDATE Nhan_vien
            SET 
                Gioi_tinh = @Gioi_tinh,
                So_dien_thoai = @So_dien_thoai,
                Ma_chuc_vu = @Ma_chuc_vu,
                Ma_phong_ban = @Ma_phong_ban,
                Ngay_bat_dau_lam_viec = @Ngay_bat_dau
            WHERE 
                Ho_va_ten = @Ho_va_ten";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ho_va_ten", hoTen);
                    command.Parameters.AddWithValue("@Gioi_tinh", gioiTinh);
                    command.Parameters.AddWithValue("@So_dien_thoai", soDienThoai);
                    command.Parameters.AddWithValue("@Ma_chuc_vu", maChucVu);
                    command.Parameters.AddWithValue("@Ma_phong_ban", maPhongBan);
                    command.Parameters.AddWithValue("@Ngay_bat_dau", ngayBatDau);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void bttxoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra trường họ tên đã được chọn chưa
            if (string.IsNullOrEmpty(txthoten.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
                return;
            }

            // Xác nhận hành động xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên " + txthoten.Text + " không?",
                                                 "Xác nhận xóa",
                                                 MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                // Xóa nhân viên
                DeleteNhanVien(txthoten.Text);
                LoadData(); // Tải lại dữ liệu vào DataGridView
            }
        }

        private void DeleteNhanVien(string hoTen)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "DELETE FROM Nhan_vien WHERE Ho_va_ten = @Ho_va_ten";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ho_va_ten", hoTen);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void themnhanvien_Load(object sender, EventArgs e)
        {

        }
    }
}
