
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
    
public partial class DOCGIA
{

    public DOCGIA()
    {

        this.LICHSUMUONPHONGs = new HashSet<LICHSUMUONPHONG>();

        this.THONGTINMUONTRAs = new HashSet<THONGTINMUONTRA>();

        this.USERs = new HashSet<USER>();

    }


    public int ID { get; set; }

    public string MHV_MSSV { get; set; }

    public string Hoten { get; set; }

    public string DiaChi { get; set; }

    public string CMND { get; set; }

    public string Email { get; set; }

    public string Truong { get; set; }

    public string Khoa { get; set; }

    public string Avatar { get; set; }

    public string LoaiDocGia { get; set; }



    public virtual ICollection<LICHSUMUONPHONG> LICHSUMUONPHONGs { get; set; }

    public virtual ICollection<THONGTINMUONTRA> THONGTINMUONTRAs { get; set; }

    public virtual ICollection<USER> USERs { get; set; }

}

}
