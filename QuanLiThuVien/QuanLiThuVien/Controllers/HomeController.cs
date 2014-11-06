using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;
namespace QuanLiThuVien.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            //var result = from p in data.DOCGIAs
            //             select p;
            //return View(result);
            return View();
        }

        public ActionResult save(FormCollection f)
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            DOCGIA p = new DOCGIA();
            p.Hoten = f["Hoten"];
            p.MaHocVien = f["MSSV"];
            p.Email = f["Email"];
            p.CMND = f["CMND"];
            p.DiaChi = f["DiaChi"];
            p.Truong = f["Truong"];
            p.Khoa = f["Khoa"];
            data.DOCGIAs.Add(p);

            data.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult add()
        {
            return View();
        }

        public ActionResult DanhSachSachDat()
        {
            return View();
        }
        public ActionResult ChinhSuaDatMuon()
        {
            return View();
        }
        public ActionResult GiaHanMuonSach()
        {
            return View();
        }
        public ActionResult TimKiemSach()
        {
            return View();
        }

    }
}
