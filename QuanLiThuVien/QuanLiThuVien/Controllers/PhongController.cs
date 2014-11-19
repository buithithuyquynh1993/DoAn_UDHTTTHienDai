using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;

namespace QuanLiThuVien.Controllers
{
    public class PhongController : Controller
    {
        //
        // GET: /Phong/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListBorrowedRoom()
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var query = (from ls in data.LICHSUMUONPHONGs
                        join phong in data.PHONGs on ls.IDPhong equals phong.ID
                        join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                        select ls);
                        //select new {HoTen = docgia.Hoten,
                        //            tgmuon = ls.ThoiGianMuon,
                        //            tgtra = ls.ThoiGianTra,
                        //            phong = phong.ID});
            ViewBag.data = query.ToArray();
            return View();
        }
        public ActionResult BorrowedRoom()
        {
            return View();
        }
    }
}
