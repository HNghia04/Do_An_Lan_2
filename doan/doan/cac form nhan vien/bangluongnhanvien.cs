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
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Interfaces;

namespace doan.cac_form_nhan_vien
{
    public partial class bangluongnhanvien : Form
    {
        public bangluongnhanvien()
        {
            InitializeComponent();
            cbxhoten.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            FormNhanVien f = new FormNhanVien();
            f.Show();
            this.Hide();
        }

        private void bangluongnhanvien_Load(object sender, EventArgs e)
        {
            LoadHoTen();
            this.report.RefreshReport();
            this.reportViewer2.RefreshReport();
        }
        private void LoadHoTen()
        {
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";
            string query = @"
        SELECT nv.Ho_va_ten 
        FROM Bang_luong bl 
        JOIN Nhan_vien nv ON bl.Ma_nhan_vien = nv.Ma_nhan_vien;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
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
        private void bttphucap_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa chọn tên nhân viên
            if (cbxhoten.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tên nhân viên.");
                return;
            }

            // Lấy tên nhân viên từ ComboBox
            string tenNhanVien = cbxhoten.SelectedItem.ToString();

            // Tìm mã nhân viên từ tên nhân viên
            string maNhanVien = string.Empty;
            string connectionString = "Data Source=LAPTOP-G689TECS\\SQLEXPRESS;Initial Catalog=QuanLy_NhanVien;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string queryMaNV = "SELECT Ma_nhan_vien FROM Nhan_vien WHERE Ho_va_ten = @tenNhanVien";
                SqlCommand cmd = new SqlCommand(queryMaNV, conn);
                cmd.Parameters.AddWithValue("@tenNhanVien", tenNhanVien);

                conn.Open();
                maNhanVien = cmd.ExecuteScalar()?.ToString();
            }

            if (!string.IsNullOrEmpty(maNhanVien))
            {
                // Đặt đường dẫn báo cáo
                string reportPath = System.IO.Path.Combine(Application.StartupPath, "luongnhanvien.rdlc");

                // Kiểm tra nếu file báo cáo tồn tại
                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show("Không tìm thấy file báo cáo tại đường dẫn: " + reportPath);
                    return;
                }

                // Thiết lập báo cáo
                report.ProcessingMode = ProcessingMode.Local;
                report.LocalReport.ReportPath = reportPath;

                // Truy vấn dữ liệu
                string query = @"
        SELECT bl.Thang_tinh_luong, bl.Ma_bang_luong, bl.Ma_nhan_vien, 
               bl.Luong_co_ban, bl.Phu_cap, bl.Luong_thuc_nhan 
        FROM Bang_luong bl
        WHERE bl.Ma_nhan_vien = @maNhanVien";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@maNhanVien", maNhanVien);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Gán dữ liệu cho báo cáo
                        ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);
                        report.LocalReport.DataSources.Clear();
                        report.LocalReport.DataSources.Add(dataSource);

                        // Làm mới báo cáo
                        report.RefreshReport();
                    }
                }

                // Khóa ComboBox sau khi xem lương
                cbxhoten.Enabled = false; // Khóa ComboBox
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã nhân viên tương ứng với tên nhân viên đã chọn.");
            }
        }

    }
}
