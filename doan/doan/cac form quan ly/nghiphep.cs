using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace doan.cac_form_quan_ly
{
    public partial class nghiphep1 : Form
    {
        private string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

        public nghiphep1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                np.Ma_nghi_phep, 
                np.Ma_nhan_vien,          -- Thêm mã nhân viên để sử dụng trong CellClick
                nv.Ho_va_ten AS Ho_ten, 
                np.Ngay_bat_dau_nghi, 
                np.Ngay_ket_thuc_nghi, 
                np.Trang_thai 
            FROM 
                Nghi_phep np
            JOIN 
                Nhan_vien nv ON np.Ma_nhan_vien = nv.Ma_nhan_vien";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            ColorizeRows(); // Tô màu hàng dựa trên trạng thái
        }


        private void ColorizeRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["Trang_thai"].Value?.ToString();
                if (status == "Chờ duyệt")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White; // Màu chữ để dễ đọc
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void btthem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdonnghiviec.Text) ||
                cbxhoten.SelectedValue == null ||
                dtpngaynopdon.Value >= dtpngaydukiennghỉviec.Value ||
                string.IsNullOrWhiteSpace(cbxtrangthai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra ngày kết thúc không trước ngày bắt đầu
            if (dtpngaydukiennghỉviec.Value < dtpngaynopdon.Value)
            {
                MessageBox.Show("Ngày dự kiến nghỉ việc không thể trước ngày nộp đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maDon = txtdonnghiviec.Text.Trim();
            string maNhanVien = cbxhoten.SelectedValue.ToString(); // Lấy mã nhân viên
            DateTime ngayBatDau = dtpngaynopdon.Value;
            DateTime ngayKetThuc = dtpngaydukiennghỉviec.Value;
            string trangThai = cbxtrangthai.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Kiểm tra mã đơn nghỉ phép có trùng không
                string queryCheck = "SELECT COUNT(*) FROM Nghi_phep WHERE Ma_nghi_phep = @MaDon";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaDon", maDon);
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã đơn nghỉ phép đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Thêm bản ghi mới vào cơ sở dữ liệu
                string queryInsert = "INSERT INTO Nghi_phep (Ma_nghi_phep, Ma_nhan_vien, Ngay_bat_dau_nghi, Ngay_ket_thuc_nghi, Trang_thai) " +
                                     "VALUES (@MaDon, @MaNhanVien, @NgayBatDau, @NgayKetThuc, @TrangThai)";
                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@MaDon", maDon);
                    cmdInsert.Parameters.AddWithValue("@MaNhanVien", maNhanVien); // Sử dụng Ma_nhan_vien
                    cmdInsert.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                    cmdInsert.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                    cmdInsert.Parameters.AddWithValue("@TrangThai", trangThai);

                    try
                    {
                        cmdInsert.ExecuteNonQuery();
                        MessageBox.Show("Thêm đơn nghỉ phép thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Cập nhật lại dữ liệu
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

        private void bttcapnhat_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn để cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            string maDon = selectedRow.Cells["Ma_nghi_phep"].Value.ToString(); // Thay đổi tên cột cho phù hợp
            string newStatus = cbxtrangthai.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryUpdate = "UPDATE Nghi_phep SET Trang_thai = @TrangThai WHERE Ma_nghi_phep = @MaDon"; // Thay đổi tên cột cho phù hợp
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
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi cập nhật trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
                    cbxhoten.DataSource = dt;
                    cbxhoten.DisplayMember = "Ho_va_ten";   // Hiển thị tên nhân viên
                    cbxhoten.ValueMember = "Ma_nhan_vien";  // Lưu giá trị là mã nhân viên
                }
            }
        }


        // Hiển thị dữ liệu từ DataGridView khi chọn hàng
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dataGridView1.RowCount)
            {
                txtdonnghiviec.Text = dataGridView1.Rows[i].Cells["Ma_nghi_phep"].Value?.ToString() ?? "";
                cbxhoten.SelectedValue = dataGridView1.Rows[i].Cells["Ma_nhan_vien"].Value; // Gán mã nhân viên, ComboBox sẽ tự động hiển thị tên
                dtpngaynopdon.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells["Ngay_bat_dau_nghi"].Value);
                dtpngaydukiennghỉviec.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells["Ngay_ket_thuc_nghi"].Value);
                cbxtrangthai.Text = dataGridView1.Rows[i].Cells["Trang_thai"].Value?.ToString() ?? "";
            }
        }


        // Gọi lại LoadData khi form load để cập nhật màu ngay từ đầu
        private void nghiphep_Load_1(object sender, EventArgs e)
        {
            LoadHoTen();
            LoadData(); // Gọi để đảm bảo DataGridView cập nhật từ khi form mở
        }

        // Thêm phương thức xóa
        private void bttxoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng nào được chọn
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Xác nhận trước khi xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn nghỉ phép này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return;

                // Lấy mã đơn nghỉ phép của hàng được chọn
                var maDon = dataGridView1.SelectedRows[0].Cells["Ma_nghi_phep"].Value.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string queryDelete = "DELETE FROM Nghi_phep WHERE Ma_nghi_phep = @MaDon";
                    using (SqlCommand cmdDelete = new SqlCommand(queryDelete, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@MaDon", maDon);

                        try
                        {
                            cmdDelete.ExecuteNonQuery();
                            MessageBox.Show("Xóa đơn nghỉ phép thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData(); // Cập nhật lại dữ liệu
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Đã xảy ra lỗi khi xóa đơn nghỉ phép: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn nghỉ phép để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
        SELECT 
            np.Ma_nghi_phep, 
            nv.Ho_va_ten AS Ho_ten, 
            np.Ngay_bat_dau_nghi, 
            np.Ngay_ket_thuc_nghi, 
            np.Trang_thai 
        FROM 
            Nghi_phep np
        JOIN 
            Nhan_vien nv ON np.Ma_nhan_vien = nv.Ma_nhan_vien
        WHERE 
            nv.Ho_va_ten LIKE @SearchText";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchText", "%" + txtTimKiem.Text.Trim() + "%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            cacbangdon f = new cacbangdon();
            f.Show();
            this.Hide();
        }
    }
}
