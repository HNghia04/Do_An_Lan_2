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
    
    public partial class Quyen_han
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quyen_han()
        {
            this.Tai_khoan = new HashSet<Tai_khoan>();
        }
    
        public int Ma_quyen_han { get; set; }
        public string Ten_quyen_han { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tai_khoan> Tai_khoan { get; set; }
    }
}
