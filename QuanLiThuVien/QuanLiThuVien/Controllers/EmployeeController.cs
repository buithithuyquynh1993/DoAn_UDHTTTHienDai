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
            return RedirectToAction("BorrowedBook");
        }
        public ActionResult BorrowedRoom()
        {
            return View();
        }
        public ActionResult SaveBorrowedRoom()
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            LICHSUMUONPHONG ls = new LICHSUMUONPHONG();
            ls.IDDocGia = int.Parse(@Request["IDDocGia"].ToString());
            ls.IDPhong = int.Parse(@Request["IDPhong"].ToString());
            ls.ThoiGianMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
            ls.ThoiGianTra = DateTime.Parse(@Request["ThoiGianTra"].ToString());
            data.LICHSUMUONPHONGs.Add(ls);
            data.SaveChanges();
            return RedirectToAction("BorrowedRoom");
        }

        #region Chuc năng trả sách

        public THONGTINMUONTRA layThongTinMuonSach() { return null; }
        public decimal tinhTienTienPhatQuaHan() { return 0; }
        public decimal tinhTienTienPhatThem() { return 0; }
        public bool thuchienTraSach() { return false; }
        public ActionResult TraSach()
        {
            return View();
        }
        #endregion
        

        public ActionResult layDSNguoiMuon()
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from t in data.VIEW_BORROWERS select t;
            var tem = result.GetType();
            return View(result);
        }
    }
}
