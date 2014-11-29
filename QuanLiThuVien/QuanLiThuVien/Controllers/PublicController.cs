using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Dynamic;

using QuanLiThuVien.Models;

namespace QuanLiThuVien.Controllers
{
    public class PublicController : Controller
    {
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        public ActionResult GetListBorrowedRoom(int? page)
        {
            DateTime now = DateTime.Today;
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var keyword = @Request["keyword"];
            string temp = @Request["date"];
            if (keyword == null && temp == null)
            {
                page = 1;
                var query = (from ls in data.LICHSUMUONPHONGs
                             join phong in data.PHONGs on ls.IDPhong equals phong.ID
                             join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                             where DateTime.Compare(ls.ThoiGianMuon, now).Equals(1)
                             //select ls).OrderBy(ls=>ls.ThoiGianMuon);
                             select new
                             {
                                 HoTen = docgia.Hoten,
                                 tgmuon = ls.ThoiGianMuon,
                                 tgtra = ls.ThoiGianTra,
                                 phong = phong.ID
                             }).OrderBy(a => a.tgmuon);
                return View("GetListBorrowedRoom", query.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                // var keyword = @Request["keyword"];

                DateTime date = DateTime.Today;
                if (temp != "")
                {
                    // string temp = @Request["date"];
                    date = DateTime.Parse(temp);
                }
                if (keyword != "" && temp != "")
                {
                    var query = (from ls in data.LICHSUMUONPHONGs
                                 join phong in data.PHONGs on ls.IDPhong equals phong.ID
                                 join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                                 where docgia.Hoten.Contains(@keyword)
                                 && DateTime.Compare(ls.ThoiGianMuon, now).Equals(1)
                                 select ls).OrderBy(ls => ls.IDDocGia);
                    ViewBag.data = query.ToPagedList(pageNumber, pageSize);
                    return View("GetListBorrowedRoom");
                }
                if (keyword != "")
                {
                    var query = (from ls in data.LICHSUMUONPHONGs
                                 join phong in data.PHONGs on ls.IDPhong equals phong.ID
                                 join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                                 where docgia.Hoten.Contains(@keyword)
                                 select ls).OrderBy(ls => ls.IDDocGia);
                    ViewBag.data = query.ToPagedList(pageNumber, pageSize);
                    return View("GetListBorrowedRoom");
                }
                if (temp != "")
                {
                    var query = (from ls in data.LICHSUMUONPHONGs
                                 join phong in data.PHONGs on ls.IDPhong equals phong.ID
                                 join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                                 where DateTime.Compare(ls.ThoiGianMuon, now).Equals(1)
                                 select ls).OrderBy(ls => ls.IDDocGia);
                    ViewBag.data = query.ToPagedList(pageNumber, pageSize);
                    return View("GetListBorrowedRoom");
                }

            }
            return View();
        }

    }
}
