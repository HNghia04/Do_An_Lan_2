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
    public partial class bangcap : Form
    {
        public bangcap()
        {
            InitializeComponent();
            LoadData();
            LoadComboBox();
        }

        private void bangcap_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadComboBox()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ho_va_ten FROM Nhan_vien"; // Truy vấn lấy tên nhân viên

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    cbxhoten.Items.Clear(); // Xóa tất cả các mục hiện tại trong ComboBox

                    while (reader.Read())
                    {
                        string tenNhanVien = reader["Ho_va_ten"].ToString();
                        cbxhoten.Items.Add(tenNhanVien); // Thêm tên nhân viên vào ComboBox
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < dataGridView1.RowCount - 1) // Kiểm tra chỉ số hàng hợp lệ
            {
                try
                {
                    txtmabangcap.Text = dataGridView1.Rows[i].Cells[0].Value?.ToString() ?? "";
                    cbxhoten.Text = dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? "";
                    txttenbangcap.Text = dataGridView1.Rows[i].Cells[2].Value?.ToString() ?? "";
                    txtcosogiaoduc.Text = dataGridView1.Rows[i].Cells[3].Value?.ToString() ?? "";

                    // Kiểm tra và gán giá trị ngày cấp bằng
                    string dateValue = dataGridView1.Rows[i].Cells[4].Value?.ToString() ?? "";
                    if (DateTime.TryParse(dateValue, out DateTime parsedDate))
                    {
                        dtpngaycapbang.Value = parsedDate; // Gán giá trị DateTime vào DateTimePicker
                    }
                    else
                    {
                        MessageBox.Show("Ngày cấp bằng không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    txthangbang.Text = dataGridView1.Rows[i].Cells[5].Value?.ToString() ?? "";
                    txtchuyennganh.Text = dataGridView1.Rows[i].Cells[6].Value?.ToString() ?? "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu từ hàng đã chọn: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadData()
        {
            // Kết nối đến cơ sở dữ liệu
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"; // Thay đổi thành chuỗi kết nối của bạn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Câu truy vấn lấy dữ liệu từ bảng BangCap và tên nhân viên từ bảng NhanVien
                    string query = @"
                SELECT 
                    b.Ma_bang_cap, 
                    n.Ho_va_ten, 
                    b.Ten_bang_cap, 
                    b.Ten_co_so_giao_duc, 
                    b.Ngay_cap_bang, 
                    b.Hang_bang, 
                    b.Chuyen_nganh_dao_tao 
                FROM 
                    Bang_cap b 
                INNER JOIN 
                    Nhan_vien n ON b.Ma_nhan_vien = n.Ma_nhan_vien"; // Điều chỉnh theo tên cột trong bảng của bạn

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Thiết lập DataSource cho DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập đầy đủ
            if (string.IsNullOrWhiteSpace(txtmabangcap.Text) ||
                string.IsNullOrWhiteSpace(txttenbangcap.Text) ||
                cbxhoten.SelectedItem == null ||  // ComboBox tên nhân viên
                string.IsNullOrWhiteSpace(txtcosogiaoduc.Text) ||
                dtpngaycapbang.Value == null ||
                string.IsNullOrWhiteSpace(txthangbang.Text) ||
                string.IsNullOrWhiteSpace(txtchuyennganh.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra ngày cấp bằng không phải là ngày trong tương lai
            if (dtpngaycapbang.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày cấp bằng không được là ngày trong tương lai.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra nếu ngày cấp bằng là ngày hôm nay
            if (dtpngaycapbang.Value.Date == DateTime.Today)
            {
                MessageBox.Show("Ngày cấp bằng không nên là ngày hôm nay, vui lòng chọn ngày trước đó.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy tên nhân viên từ ComboBox và tìm mã nhân viên tương ứng
            string tenNhanVien = cbxhoten.SelectedItem.ToString();
            string maNhanVien = GetMaNhanVien(tenNhanVien);

            if (maNhanVien == null)
            {
                MessageBox.Show("Không tìm thấy mã nhân viên tương ứng với tên đã chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mã bằng cấp có trùng trong cơ sở dữ liệu không
            string maBangCap = txtmabangcap.Text;
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryCheck = "SELECT COUNT(*) FROM Bang_cap WHERE Ma_bang_cap = @MaBangCap"; // Đổi tên trường cho đúng

                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaBangCap", maBangCap);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Mã bằng cấp đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Thêm bản ghi với mã nhân viên
                string tenBangCap = txttenbangcap.Text;
                string tenCoSo = txtcosogiaoduc.Text;
                DateTime ngayCapBang = dtpngaycapbang.Value;
                string hangBang = txthangbang.Text;
                string chuyenNganh = txtchuyennganh.Text;

                string queryInsert = "INSERT INTO Bang_cap (Ma_bang_cap, Ma_nhan_vien, Ten_bang_cap, Ten_co_so_giao_duc, Ngay_cap_bang, Hang_bang, Chuyen_nganh_dao_tao) " +
                                     "VALUES (@MaBangCap, @MaNhanVien, @TenBangCap, @TenCoSo, @NgayCapBang, @HangBang, @ChuyenNganh)";

                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@MaBangCap", maBangCap);
                    cmdInsert.Parameters.AddWithValue("@MaNhanVien", maNhanVien); // Sử dụng mã nhân viên
                    cmdInsert.Parameters.AddWithValue("@TenBangCap", tenBangCap);
                    cmdInsert.Parameters.AddWithValue("@TenCoSo", tenCoSo);
                    cmdInsert.Parameters.AddWithValue("@NgayCapBang", ngayCapBang);
                    cmdInsert.Parameters.AddWithValue("@HangBang", hangBang);
                    cmdInsert.Parameters.AddWithValue("@ChuyenNganh", chuyenNganh);

                    cmdInsert.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Thêm bằng cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Cập nhật lại dữ liệu ngay lập tức
            LoadData();
        }


        private string GetMaNhanVien(string tenNhanVien)
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ma_nhan_vien FROM Nhan_vien WHERE Ho_va_ten = @HoVaTen";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoVaTen", tenNhanVien);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString(); // Trả về mã nhân viên nếu tìm thấy
                }
            }
        }


        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            cacbangdon f = new cacbangdon();
            f.Show();
            this.Hide();
        }

        private void bttxoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dataGridView1.CurrentRow != null)
            {
                // Lấy mã bằng cấp từ hàng được chọn
                string maBangCap = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                // Xác nhận xóa
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa bằng cấp này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string queryDelete = "DELETE FROM Bang_cap WHERE Ma_bang_cap = @MaBangCap";

                        using (SqlCommand cmdDelete = new SqlCommand(queryDelete, conn))
                        {
                            cmdDelete.Parameters.AddWithValue("@MaBangCap", maBangCap);
                            cmdDelete.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Xóa bằng cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Cập nhật lại dữ liệu
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
