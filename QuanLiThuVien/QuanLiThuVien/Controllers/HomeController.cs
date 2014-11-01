using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
