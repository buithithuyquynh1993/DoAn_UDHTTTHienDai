using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using QuanLiThuVien.Models;


namespace QuanLiThuVien.Controllers
{
    public class SachController : Controller
    {
        public static String StrQuery_LayDsMuonTra(XmlElement node)
        {
            return "";
        }
        //
        // GET: /Sach/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult layDSNguoiMuon() {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from t in data.VIEW_BORROWERS select t;
            return View(result);
        }
        public ActionResult layDsMuonTra()
        {
            return View();
        }
        
    }
}
