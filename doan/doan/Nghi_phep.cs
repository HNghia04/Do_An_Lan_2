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
    
    public partial class Nghi_phep
    {
        public int Ma_nghi_phep { get; set; }
        public Nullable<System.DateTime> Ngay_bat_dau_nghi { get; set; }
        public Nullable<System.DateTime> Ngay_ket_thuc_nghi { get; set; }
        public string Trang_thai { get; set; }
        public int Ma_nhan_vien { get; set; }
    
        public virtual Nhan_vien Nhan_vien { get; set; }
    }
}
