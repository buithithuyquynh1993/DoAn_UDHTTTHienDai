//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiThuVien.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TACGIA
    {
        public TACGIA()
        {
            this.SACHes = new HashSet<SACH>();
        }
    
        public int ID { get; set; }
        public string TenTacGia { get; set; }
        public string QuocTich { get; set; }
    
        public virtual ICollection<SACH> SACHes { get; set; }
    }
}
