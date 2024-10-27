using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doan
{
    public class LoaiPhuCap
    {
        public int MaLoaiPhuCap { get; set; }
        public string TenLoaiPhuCap { get; set; }

        public override string ToString() // Tùy chọn: Để hiển thị tên loại phụ cấp trong ComboBox
        {
            return TenLoaiPhuCap;
        }
    }

}
