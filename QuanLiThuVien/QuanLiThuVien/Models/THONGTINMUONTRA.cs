//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiThuVien.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class THONGTINMUONTRA
    {
        public THONGTINMUONTRA()
        {
            this.BANPHATs = new HashSet<BANPHAT>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> NgayMuon { get; set; }
        public Nullable<System.DateTime> NgayTra { get; set; }
        public Nullable<System.DateTime> HanTra { get; set; }
        public Nullable<bool> GiaHan { get; set; }
        public Nullable<int> IDNhanVienNhan { get; set; }
        public Nullable<int> IDNhanVienTra { get; set; }
        public Nullable<int> IDDocGia { get; set; }
        public Nullable<int> IDSach { get; set; }
    
        public virtual ICollection<BANPHAT> BANPHATs { get; set; }
        public virtual DOCGIA DOCGIA { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
        public virtual NHANVIEN NHANVIEN1 { get; set; }
        public virtual SACH SACH { get; set; }
    }
}
