using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;
using PagedList;
using PagedList.Mvc;
using System.Dynamic;

namespace QuanLiThuVien.Controllers
{
    public class PhongController : Controller
    {
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
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
            return View("BorrowedRoom");
        }
    }
}
