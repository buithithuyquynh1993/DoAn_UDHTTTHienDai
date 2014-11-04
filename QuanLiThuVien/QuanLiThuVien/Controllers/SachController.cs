using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;

namespace QuanLiThuVien.Controllers
{
    public class SachController : Controller
    {
        //
        // GET: /Sach/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getListBorrowers() {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from t in data.View_Borrowers select t;
            return View(result);
        }
    }
}
