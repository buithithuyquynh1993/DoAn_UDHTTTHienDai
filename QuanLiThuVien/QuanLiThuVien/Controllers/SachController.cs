using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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

        public ActionResult layDSNguoiMuon()
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from t in data.VIEW_BORROWERS select t;
            var tem = result.GetType();
            return View(result);
        }

        #region Chuc năng trả sách

        public THONGTINMUONTRA layThongTinMuonSach() { return null; }
        public decimal tinhTienTienPhatQuaHan() { return 0; }
        public decimal tinhTienTienPhatThem() { return 0; }
        public bool thuchienTraSach() { return false; }

        #endregion
    }
}