using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doan
{
    // NhanVien.cs
    public class NhanVien
    {
        public int MaNhanVien { get; set; } // Mã nhân viên
        public string HoVaTen { get; set; } // Họ và tên
        public string GioiTinh { get; set; } // Giới tính
        public string SoDienThoai { get; set; } // Số điện thoại
        public string TenChucVu { get; set; } // Tên chức vụ

        // Phương thức này để hiển thị họ tên trong ComboBox
        public override string ToString()
        {
            return HoVaTen;
        }
    }


}
