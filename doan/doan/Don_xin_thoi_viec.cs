//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace doan
{
    using System;
    using System.Collections.Generic;
    
    public partial class Don_xin_thoi_viec
    {
        public int Ma_don_xin_thoi_viec { get; set; }
        public string Ho_ten { get; set; }
        public string Chuc_vu { get; set; }
        public Nullable<System.DateTime> Ngay_nop_don { get; set; }
        public Nullable<System.DateTime> Ngay_du_kien_thoi_viec { get; set; }
        public string Ly_do { get; set; }
        public string Trang_thai { get; set; }
        public int Ma_nhan_vien { get; set; }
    
        public virtual Nhan_vien Nhan_vien { get; set; }
    }
}
