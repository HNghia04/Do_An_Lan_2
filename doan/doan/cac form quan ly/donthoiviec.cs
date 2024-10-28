using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace doan.cac_form_quan_ly
{
    public partial class donthoiviec : Form
    {
        private string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

        public donthoiviec()
        {
            InitializeComponent();
            LoadData(); // Gọi hàm LoadData khi khởi tạo form
        }

        // Load dữ liệu vào DataGridView
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Ma_don_xin_thoi_viec, Ho_ten, Chuc_vu, Ngay_nop_don, Ngay_du_kien_thoi_viec, Ly_do, Trang_thai FROM Don_xin_thoi_viec";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }

            // Gọi hàm tô màu sau khi tải dữ liệu
            ColorizeRows();
        }

        // Hàm tô màu các hàng dựa trên trạng thái
        private void ColorizeRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Trang_thai"].Value?.ToString();
                if (status == "Chờ duyệt")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White; // Màu chữ để dễ đọc trên nền đỏ
                }
                else
                {
                    // Đặt lại màu mặc định nếu không phải là "Chờ duyệt"
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        // Nút trở về form khác
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            cacbangdon f = new cacbangdon();
            f.Show();
            this.Hide();
        }

        // Nút thêm đơn thôi việc
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập
            if (string.IsNullOrWhiteSpace(txtdonthoiviec.Text) ||
                string.IsNullOrWhiteSpace(cbxhoten.Text) ||
                cbxchucvu.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtlydo.Text) ||
                string.IsNullOrWhiteSpace(cbxtrangthai.Text) ||
                dtpngaynopdon.Value >= dtpngaydukienthoiviec.Value)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maDon = txtdonthoiviec.Text.Trim();
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

                // Kiểm tra mã nhân viên đã lấy ra
                if (string.IsNullOrEmpty(maNhanVien))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên cho họ tên đã chọn. Vui lòng kiểm tra lại họ tên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra mã đơn thôi việc có trùng không
                string queryCheck = "SELECT COUNT(*) FROM Don_xin_thoi_viec WHERE Ma_don_xin_thoi_viec = @MaDon";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaDon", maDon);
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã đơn thôi việc đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Thêm bản ghi mới vào cơ sở dữ liệu
                string queryInsert = "INSERT INTO Don_xin_thoi_viec (Ma_don_xin_thoi_viec, Ma_nhan_vien, Ho_ten, Chuc_vu, Ngay_nop_don, Ngay_du_kien_thoi_viec, Ly_do, Trang_thai) " +
                                     "VALUES (@MaDon, @MaNhanVien, @HoTen, @ChucVu, @NgayNopDon, @NgayDuKien, @LyDo, @TrangThai)";
                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@MaDon", maDon);
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
                        MessageBox.Show("Thêm đơn thôi việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Cập nhật lại dữ liệu ngay lập tức
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi thêm đơn thôi việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Nút cập nhật trạng thái
        private void bttcapnhat_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn để cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            string maDon = selectedRow.Cells["Ma_don_xin_thoi_viec"].Value.ToString();
            string newStatus = cbxtrangthai.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryUpdate = "UPDATE Don_xin_thoi_viec SET Trang_thai = @TrangThai WHERE Ma_don_xin_thoi_viec = @MaDon";
                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@TrangThai", newStatus);
                    cmdUpdate.Parameters.AddWithValue("@MaDon", maDon);

                    try
                    {
                        cmdUpdate.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Cập nhật lại dữ liệu
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi cập nhật trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Tải dữ liệu chức vụ vào ComboBox
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

        // Hiển thị dữ liệu từ DataGridView khi chọn hàng
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dataGridView1.RowCount)
            {
                try
                {
                    txtdonthoiviec.Text = dataGridView1.Rows[i].Cells[0].Value?.ToString() ?? "";
                    cbxhoten.Text = dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? "";
                    cbxchucvu.Text = dataGridView1.Rows[i].Cells[2].Value?.ToString() ?? "";
                    dtpngaynopdon.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value);

                    // Kiểm tra và gán giá trị cho ngày dự kiến thôi việc
                    string dateDuKienValue = dataGridView1.Rows[i].Cells[4].Value?.ToString() ?? "";
                    if (DateTime.TryParse(dateDuKienValue, out DateTime parsedDuKienDate))
                    {
                        dtpngaydukienthoiviec.Value = parsedDuKienDate;
                    }
                    else
                    {
                        dtpngaydukienthoiviec.Value = DateTime.Now;
                    }

                    txtlydo.Text = dataGridView1.Rows[i].Cells[5].Value?.ToString() ?? "";
                    cbxtrangthai.Text = dataGridView1.Rows[i].Cells[6].Value?.ToString() ?? "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hiển thị dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Gọi lại LoadData khi form load để cập nhật màu ngay từ đầu
        private void donthoiviec_Load(object sender, EventArgs e)
        {
            LoadChucVu();
            LoadHoTen();
            LoadData(); // Gọi để đảm bảo DataGridView cập nhật từ khi form mở
        }
        // Thêm phương thức xóa
        private void bttxoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng nào được chọn
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy mã đơn thôi việc từ hàng được chọn
                string maDon = dataGridView1.SelectedRows[0].Cells["Ma_don_xin_thoi_viec"].Value?.ToString();

                // Xác nhận việc xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa đơn thôi việc có mã: {maDon}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string queryDelete = "DELETE FROM Don_xin_thoi_viec WHERE Ma_don_xin_thoi_viec = @MaDon";
                        using (SqlCommand cmdDelete = new SqlCommand(queryDelete, conn))
                        {
                            cmdDelete.Parameters.AddWithValue("@MaDon", maDon);

                            try
                            {
                                cmdDelete.ExecuteNonQuery();
                                MessageBox.Show("Xóa đơn thôi việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData(); // Tải lại dữ liệu để cập nhật DataGridView
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi xóa đơn thôi việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn thôi việc để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
