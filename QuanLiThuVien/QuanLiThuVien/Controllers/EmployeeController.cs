using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;

namespace QuanLiThuVien.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/  
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        public ActionResult BorrowedBook()
        {
            return View();
        }
        public ActionResult SaveBorrowedBook()
        {
            // add borrowed book information 
            int IDSach = int.Parse(@Request["IDSach"]);
            THONGTINMUONTRA muontra = new THONGTINMUONTRA();
            muontra.IDDocGia = int.Parse(@Request["IDDocGia"].ToString());
            muontra.IDNhanVienNhan = int.Parse(@Request["IDNhanVien"].ToString());
            muontra.IDSach = IDSach;
            muontra.NgayMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
            muontra.NgayTra = DateTime.Parse(@Request["ThoiGianTra"].ToString());
            data.THONGTINMUONTRAs.Add(muontra);
            data.SaveChanges();

            // update status of book
            SACH sach = (from s in data.SACHes where s.ID == IDSach select s).First();
            sach.TrangThai = "Đang mượn";
            data.SaveChanges();
            return Redirect("Employee/BorrowedBook");
        }
        public ActionResult BorrowedRoom()
        {
            return Redirect("~/Phong/BorrowedRoom");
        }
        public ActionResult TraSach() {
            return View();
        }
    }
}
