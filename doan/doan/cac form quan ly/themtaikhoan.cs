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
    public partial class themtaikhoan : Form
    {
        public themtaikhoan()
        {
            InitializeComponent();
        }

        private void themtaikhoan_Load(object sender, EventArgs e)
        {
            LoadQuyenHan();
            LoadData();
        }

        public void LoadData()
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            // Câu truy vấn SQL
            string query = "SELECT [Ten_tai_khoan], [Mat_khau], [Ma_quyen_han] FROM [Tai_khoan]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt); // Đổ dữ liệu vào DataTable
                    dataGridView1.DataSource = dt; // Gán DataTable vào DataGridView

                    // Đặt lại tên các cột trong DataGridView
                    dataGridView1.Columns["Ten_tai_khoan"].HeaderText = "Tài khoản";
                    dataGridView1.Columns["Mat_khau"].HeaderText = "Mật khẩu";
                    dataGridView1.Columns["Ma_quyen_han"].HeaderText = "Mã quyền hạn";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }


        private void LoadQuyenHan()
        {
            // Kết nối đến SQL Server
            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "SELECT Ma_quyen_han, Ten_quyen_han FROM Quyen_han";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dtQuyenHan = new DataTable();
                adapter.Fill(dtQuyenHan);

                // Thiết lập nguồn dữ liệu cho ComboBox
                cmbquyenhan.DataSource = dtQuyenHan;
                cmbquyenhan.DisplayMember = "Ten_quyen_han";
                cmbquyenhan.ValueMember = "Ma_quyen_han";
            }
        }
        private void bttthem_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txttaikhoan.Text;
            string matKhau = txtmatkhau.Text;
            int maQuyenHan = (int)cmbquyenhan.SelectedValue;

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "INSERT INTO Tai_khoan (Ten_tai_khoan, Mat_khau, Ma_quyen_han) VALUES (@TenTaiKhoan, @MatKhau, @MaQuyenHan)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                command.Parameters.AddWithValue("@MatKhau", matKhau);
                command.Parameters.AddWithValue("@MaQuyenHan", maQuyenHan);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Tài khoản đã được tạo thành công!");
                LoadData();
            }
        }
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            int maQuyenHan = 1; // Hoặc lấy giá trị từ biến nào đó
            FormQuanLy formQuanLy = new FormQuanLy(maQuyenHan);
            formQuanLy.Show();
            this.Hide();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i < dataGridView1.RowCount - 1)
            {
                txttaikhoan.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtmatkhau.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttaikhoan.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa.");
                return;
            }

            string tenTaiKhoan = txttaikhoan.Text;
            string matKhau = txtmatkhau.Text;
            int maQuyenHan = (int)cmbquyenhan.SelectedValue;

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "UPDATE Tai_khoan SET Mat_khau = @MatKhau, Ma_quyen_han = @MaQuyenHan WHERE Ten_tai_khoan = @TenTaiKhoan";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                command.Parameters.AddWithValue("@MatKhau", matKhau);
                command.Parameters.AddWithValue("@MaQuyenHan", maQuyenHan);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Tài khoản đã được cập nhật thành công!");
                LoadData();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttaikhoan.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa.");
                return;
            }

            string tenTaiKhoan = txttaikhoan.Text;

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True"))
            {
                string query = "DELETE FROM Tai_khoan WHERE Ten_tai_khoan = @TenTaiKhoan";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Tài khoản đã được xóa thành công!");
                LoadData();
            }
        }
    }
}
