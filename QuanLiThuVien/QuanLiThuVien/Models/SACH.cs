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
    
    public partial class SACH
    {
        public SACH()
        {
            this.THONGTINMUONTRAs = new HashSet<THONGTINMUONTRA>();
            this.TACGIAs = new HashSet<TACGIA>();
        }
    
        public int ID { get; set; }
        public string TenSach { get; set; }
        public Nullable<System.DateTime> NamXB { get; set; }
        public Nullable<int> Tap { get; set; }
        public Nullable<int> Cuon { get; set; }
        public string TrangThai { get; set; }
        public string NoiDungTomTat { get; set; }
        public string MucLuc { get; set; }
        public Nullable<int> IDNhaXuatBan { get; set; }
        public Nullable<int> IDNhomSach { get; set; }
        public Nullable<int> IDViTri { get; set; }
        public Nullable<int> IDChuDe { get; set; }
    
        public virtual CHUDE CHUDE { get; set; }
        public virtual NHAXUATBAN NHAXUATBAN { get; set; }
        public virtual NHOMSACH NHOMSACH { get; set; }
        public virtual VITRI VITRI { get; set; }
        public virtual ICollection<THONGTINMUONTRA> THONGTINMUONTRAs { get; set; }
        public virtual ICollection<TACGIA> TACGIAs { get; set; }
    }
}
