using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using QuanLiThuVien.Models;
using System.Data.Entity.Core.Objects;


namespace QuanLiThuVien.Controllers
{
    public class SachController : Controller
    {
        public static Object StrQuery_LayDsMuonTra(XmlElement node)
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();

            for(int i = 0; i< node.Attributes.Count; i++)
            {
                String name = node.Attributes[i].Name;
                String value = node.Attributes[i].Value;
                data.THONGTINMUONTRAs.Where(
            }
            
                ObjectQuery<THONGTINMUONTRA> query = data.THONGTINMUONTRAs.Where("it."+ name == @dk");
            var kq = from t in data.THONGTINMUONTRAs select t;
            return kq;
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
            Type i = result.GetType();
            return View(result);
        }
        public ActionResult layDsMuonTra()
        {
            return View();
        }
        
    }
}

