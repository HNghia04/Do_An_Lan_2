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
    public partial class hopdong : Form
    {
        public hopdong()
        {
            InitializeComponent();
            LoadComboBoxNhanVien();
            LoadData();
        }
        private void LoadComboBoxNhanVien()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ho_va_ten FROM Nhan_vien"; // Truy vấn để lấy tên nhân viên

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    cbxhoten.Items.Clear(); // Xóa các mục hiện tại trong ComboBox

                    while (reader.Read())
                    {
                        string tenNhanVien = reader["Ho_va_ten"].ToString();
                        cbxhoten.Items.Add(tenNhanVien); // Thêm tên nhân viên vào ComboBox
                    }
                }
            }
        }
        private void LoadData()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT 
                h.Ma_hop_dong, 
                n.Ho_va_ten, 
                h.Ngay_bat_dau, 
                h.Ngay_ket_thuc, 
                h.Muc_luong_co_ban 
            FROM 
                Hop_dong h 
            INNER JOIN 
                Nhan_vien n ON h.Ma_nhan_vien = n.Ma_nhan_vien";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                // Tô màu đỏ cho các hợp đồng đã hết hạn
                DateTime currentDate = DateTime.Now;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DateTime? endDate = row.Cells["Ngay_ket_thuc"].Value as DateTime?;
                    if (endDate.HasValue && endDate.Value < currentDate)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red; // Tô màu đỏ cho hàng hết hạn
                    }
                }
            }
        }



        private void cbxhoten_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            cacbangdon f = new cacbangdon();
            f.Show();
            this.Hide();
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmahopdong.Text) || cbxhoten.SelectedItem == null ||
                dtpngaybatdau.Value == null || dtpngayketthuc.Value == null ||
                string.IsNullOrWhiteSpace(txtmucluongcoban.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem lương cơ bản có phải là số âm hay không
            if (!float.TryParse(txtmucluongcoban.Text, out float luongCoBan) || luongCoBan < 0)
            {
                MessageBox.Show("Mức lương cơ bản không hợp lệ hoặc không được âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpngayketthuc.Value <= dtpngaybatdau.Value)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tenNhanVien = cbxhoten.SelectedItem.ToString();
            string maNhanVien = GetMaNhanVien(tenNhanVien);

            if (maNhanVien == null)
            {
                MessageBox.Show("Không tìm thấy mã nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maHopDong = txtmahopdong.Text;
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryCheck = "SELECT COUNT(*) FROM Hop_dong WHERE Ma_hop_dong = @MaHopDong";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaHopDong", maHopDong);
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã hợp đồng đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string queryInsert = "INSERT INTO Hop_dong (Ma_hop_dong, Ma_nhan_vien, Ngay_bat_dau, Ngay_ket_thuc, Muc_luong_co_ban) " +
                                     "VALUES (@MaHopDong, @MaNhanVien, @NgayBatDau, @NgayKetThuc, @LuongCoBan)";
                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@MaHopDong", maHopDong);
                    cmdInsert.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdInsert.Parameters.AddWithValue("@NgayBatDau", dtpngaybatdau.Value);
                    cmdInsert.Parameters.AddWithValue("@NgayKetThuc", dtpngayketthuc.Value);
                    cmdInsert.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    cmdInsert.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private string GetMaNhanVien(string tenNhanVien)
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ma_nhan_vien FROM Nhan_vien WHERE Ho_va_ten = @HoVaTen";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoVaTen", tenNhanVien);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dataGridView1.RowCount) // Kiểm tra chỉ số hàng hợp lệ
            {
                try
                {
                    txtmahopdong.Text = dataGridView1.Rows[i].Cells["Ma_hop_dong"].Value?.ToString() ?? "";
                    cbxhoten.Text = dataGridView1.Rows[i].Cells["Ho_va_ten"].Value?.ToString() ?? "";

                    // Kiểm tra và gán giá trị cho Ngày bắt đầu
                    DateTime? startDate = dataGridView1.Rows[i].Cells["Ngay_bat_dau"].Value as DateTime?;
                    dtpngaybatdau.Value = startDate ?? DateTime.Now;

                    // Kiểm tra và gán giá trị cho Ngày kết thúc
                    DateTime? endDate = dataGridView1.Rows[i].Cells["Ngay_ket_thuc"].Value as DateTime?;
                    dtpngayketthuc.Value = endDate ?? DateTime.Now;

                    txtmucluongcoban.Text = dataGridView1.Rows[i].Cells["Muc_luong_co_ban"].Value?.ToString() ?? "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu từ hàng đã chọn: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột có phải là Ngày kết thúc không
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Ngay_ket_thuc")
            {
                DateTime? endDate = e.Value as DateTime?;
                if (endDate.HasValue && endDate.Value < DateTime.Now)
                {
                    e.CellStyle.BackColor = Color.Red; // Tô màu đỏ cho ô đã hết hạn
                }
                else
                {
                    e.CellStyle.BackColor = Color.White; // Đặt lại màu nền bình thường cho ô không hết hạn
                }
            }
        }
        private void bttxoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem mã hợp đồng có được chọn không
            if (string.IsNullOrWhiteSpace(txtmahopdong.Text))
            {
                MessageBox.Show("Vui lòng chọn hợp đồng cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maHopDong = txtmahopdong.Text;
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS01;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryCheck = "SELECT COUNT(*) FROM Hop_dong WHERE Ma_hop_dong = @MaHopDong";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaHopDong", maHopDong);
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Không tìm thấy hợp đồng với mã đã nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Xóa hợp đồng
                string queryDelete = "DELETE FROM Hop_dong WHERE Ma_hop_dong = @MaHopDong";
                using (SqlCommand cmdDelete = new SqlCommand(queryDelete, conn))
                {
                    cmdDelete.Parameters.AddWithValue("@MaHopDong", maHopDong);
                    cmdDelete.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Xóa hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData(); // Tải lại dữ liệu để cập nhật DataGridView
        }

        private void hopdong_Load(object sender, EventArgs e)
        {

        }
    }
}
