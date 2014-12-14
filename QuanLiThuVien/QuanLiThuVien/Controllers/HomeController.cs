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
            return View();
        }
        public ActionResult XemChiTietDocGia()
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from p in data.DOCGIAs
                         select p;
            return View(result);
        }
        


        public ActionResult XemChiTietSach()
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from p in data.SACHes
                         select p;
            return View(result);
            
        }
    }
}
