
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
    
public partial class CHUDE
{

    public CHUDE()
    {

        this.SACHes = new HashSet<SACH>();

    }


    public int ID { get; set; }

    public string TenChuDe { get; set; }



    public virtual ICollection<SACH> SACHes { get; set; }

}

}
