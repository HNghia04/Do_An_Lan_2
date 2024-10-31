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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace doan.cac_form_quan_ly
{
    public partial class phucap : Form
    {
        private string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
        public phucap()
        {
            InitializeComponent();
            LoadNhanVienData();
            LoadLoaiPhuCap();
            LoadDataPhuCap(); // Gọi phương thức này để tải dữ liệu vào DataGridView
            cbxloaphucap.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxhotenphucap.DropDownStyle = ComboBoxStyle.DropDownList;
            LoadComboBoxNhanVien();
        }

        private void LoadLoaiPhuCap()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Ma_loai_phu_cap, Ten_loai_phu_cap FROM Loai_phu_cap";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Thay đổi cách thêm vào ComboBox cho loại phụ cấp
                    cbxloaphucap.Items.Add(new LoaiPhuCap
                    {
                        MaLoaiPhuCap = Convert.ToInt32(reader["Ma_loai_phu_cap"]),
                        TenLoaiPhuCap = reader["Ten_loai_phu_cap"].ToString()
                    });
                }
            }
        }
        private void LoadDataPhuCap()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT pc.Ma_phu_cap, nv.Ho_va_ten AS 'Họ và tên', lp.Ten_loai_phu_cap AS 'Loại phụ cấp', pc.So_tien_phu_cap AS 'Số tiền' 
                         FROM Phu_cap pc 
                         JOIN Nhan_vien nv ON pc.Ma_nhan_vien = nv.Ma_nhan_vien 
                         JOIN Loai_phu_cap lp ON pc.Ma_loai_phu_cap = lp.Ma_loai_phu_cap";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Gán DataTable cho DataGridView
                guna2DataGridView1.DataSource = dt;

                // Tùy chỉnh hiển thị cột
                guna2DataGridView1.Columns[0].HeaderText = "Mã phụ cấp";
                guna2DataGridView1.Columns[1].HeaderText = "Họ và tên";
                guna2DataGridView1.Columns[2].HeaderText = "Loại phụ cấp";
                guna2DataGridView1.Columns[3].HeaderText = "Số tiền";

                // Tùy chọn để tự động điều chỉnh kích thước cột
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Thiết lập hiển thị các hàng
                guna2DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Tự động điều chỉnh kích thước hàng
                guna2DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Cho phép hiển thị nhiều dòng trong ô

                // Tùy chỉnh màu nền và độ cao hàng
                guna2DataGridView1.RowTemplate.Height = 30; // Đặt chiều cao hàng
                guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray; // Màu nền xen kẽ cho các hàng
                guna2DataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue; // Màu nền khi chọn hàng
                guna2DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black; // Màu chữ khi chọn hàng
            }
        }

        private void LoadNhanVienData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Ma_nhan_vien, Ho_va_ten, Gioi_tinh, So_dien_thoai, Ten_chuc_vu FROM Nhan_vien", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
        }
        private void LoadComboBoxNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Ma_nhan_vien, Ho_va_ten FROM Nhan_vien", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // Chuyển đổi DataTable thành danh sách NhanVien
                var employees = dt.AsEnumerable()
                    .Select(row => new NhanVien
                    {
                        MaNhanVien = row.Field<int>("Ma_nhan_vien"),
                        HoVaTen = row.Field<string>("Ho_va_ten")
                    }).ToList();

                cbxhotenphucap.DataSource = employees;
                cbxhotenphucap.DisplayMember = "HoVaTen";
                cbxhotenphucap.ValueMember = "MaNhanVien";
            }
        }
        private void phucap_Load(object sender, EventArgs e)
        {
        }
        private void bttthem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có chọn nhân viên và loại phụ cấp hay không
            if (cbxhotenphucap.SelectedItem == null || cbxloaphucap.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên và loại phụ cấp.");
                return;
            }

            // Lấy thông tin từ SelectedItem
            NhanVien selectedEmployee = (NhanVien)cbxhotenphucap.SelectedItem;
            LoaiPhuCap selectedAllowanceType = (LoaiPhuCap)cbxloaphucap.SelectedItem;

            // Lấy mã nhân viên và mã loại phụ cấp
            int maNhanVien = selectedEmployee.MaNhanVien; // Lấy từ đối tượng NhanVien
            int maLoaiPhuCap = selectedAllowanceType.MaLoaiPhuCap; // Lấy từ đối tượng LoaiPhuCap

            // Lấy mã và số tiền phụ cấp từ các trường nhập
            string maPhucap = txtmaphucap.Text.Trim();
            float soTienPhuCap;

            // Kiểm tra nếu nhập đúng định dạng số
            if (!float.TryParse(txtsotienphucap.Text.Trim(), out soTienPhuCap))
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ.");
                return;
            }

            // Kiểm tra nếu số tiền phụ cấp lớn hơn 5000
            if (soTienPhuCap < 5000)
            {
                MessageBox.Show("Số tiền phụ cấp phải lớn hơn 5000.");
                return;
            }

            // Kiểm tra xem mã phụ cấp đã tồn tại chưa
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Phu_cap WHERE Ma_phu_cap = @MaPhucap";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@MaPhucap", maPhucap);

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Mã phụ cấp đã tồn tại. Vui lòng nhập mã khác.");
                    return;
                }
            }

            // Kết nối đến cơ sở dữ liệu và thực hiện thêm phụ cấp
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Phu_cap (Ma_phu_cap, So_tien_phu_cap, Ma_nhan_vien, Ma_loai_phu_cap) VALUES (@MaPhucap, @SoTienPhuCap, @MaNhanVien, @MaLoaiPhuCap)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhucap", maPhucap);
                cmd.Parameters.AddWithValue("@SoTienPhuCap", soTienPhuCap);
                cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                cmd.Parameters.AddWithValue("@MaLoaiPhuCap", maLoaiPhuCap);

                // Thực thi câu lệnh
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm phụ cấp thành công!");

                LoadDataPhuCap();
            }
        }


        private void bttthoat_Click(object sender, EventArgs e)
        {
            int maQuyenHan = 1; // Hoặc lấy từ tham số hoặc phương thức
            FormQuanLy formQuanLy = new FormQuanLy(maQuyenHan);
            formQuanLy.Show();
        }
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0 && i < guna2DataGridView1.RowCount) // Kiểm tra các hàng hợp lệ
            {
                // Lấy giá trị từ các ô trong DataGridView
                txtmaphucap.Text = guna2DataGridView1.Rows[i].Cells[0].Value?.ToString() ?? string.Empty; // Mã phụ cấp
                var maNhanVien = guna2DataGridView1.Rows[i].Cells[1].Value?.ToString(); // Mã nhân viên
                var maLoaiPhuCap = guna2DataGridView1.Rows[i].Cells[2].Value?.ToString(); // Mã loại phụ cấp
                txtsotienphucap.Text = guna2DataGridView1.Rows[i].Cells[3].Value?.ToString() ?? string.Empty; // Số tiền phụ cấp

                // Kiểm tra và gán mã nhân viên cho ComboBox
                try
                {
                    if (maNhanVien != null && cbxhotenphucap.Items.Cast<NhanVien>().Any(x => x.MaNhanVien.ToString() == maNhanVien))
                    {
                        cbxhotenphucap.SelectedValue = maNhanVien; // Gán mã nhân viên
                    }
                    else
                    {
                        cbxhotenphucap.SelectedIndex = -1; // Nếu không tìm thấy, đặt lại ComboBox
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Lỗi định dạng khi gán mã nhân viên: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không xác định: " + ex.Message);
                }

                // Kiểm tra và gán mã loại phụ cấp cho ComboBox
                try
                {
                    if (maLoaiPhuCap != null && cbxloaphucap.Items.Cast<LoaiPhuCap>().Any(x => x.MaLoaiPhuCap.ToString() == maLoaiPhuCap))
                    {
                        cbxloaphucap.SelectedValue = maLoaiPhuCap; // Gán mã loại phụ cấp
                    }
                    else
                    {
                        cbxloaphucap.SelectedIndex = -1; // Nếu không tìm thấy, đặt lại ComboBox
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Lỗi định dạng khi gán mã loại phụ cấp: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không xác định: " + ex.Message);
                }
            }
        }
        private void bttxoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtmaphucap.Text))
            {
                MessageBox.Show("Vui lòng chọn phụ cấp để xóa.");
                return;
            }

            // Lấy mã phụ cấp từ txtmaphucap
            string maPhucap = txtmaphucap.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Phu_cap WHERE Ma_phu_cap = @MaPhucap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhucap", maPhucap);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa phụ cấp thành công!");
                    LoadDataPhuCap(); // Tải lại dữ liệu sau khi xóa
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phụ cấp để xóa.");
                }
            }
        }
    }
}
