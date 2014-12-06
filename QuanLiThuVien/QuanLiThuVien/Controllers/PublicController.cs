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
            if ((keyword == null && temp == null ) || (keyword == "" && temp == ""))
            {
                page = 1;
                var query = (from ls in data.LICHSUMUONPHONGs
                             join phong in data.PHONGs on ls.IDPhong equals phong.ID
                             join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                             where DateTime.Compare(ls.ThoiGianMuon, now).Equals(1)
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
                DateTime date = DateTime.Today;
                if (temp != "" && temp != null)
                {
                    date = DateTime.Parse(temp);
                }
                if ((keyword != "" && temp != "") && (keyword != null && temp != null))
                {
                    var query = (from ls in data.LICHSUMUONPHONGs
                                 join phong in data.PHONGs on ls.IDPhong equals phong.ID
                                 join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                                 where docgia.Hoten.Contains(@keyword)
                                 && DateTime.Compare(ls.ThoiGianMuon, date).Equals(1)
                                 select new
                                 {
                                     HoTen = docgia.Hoten,
                                     tgmuon = ls.ThoiGianMuon,
                                     tgtra = ls.ThoiGianTra,
                                     phong = phong.ID
                                 }).OrderBy(a => a.tgmuon);
                    return View("GetListBorrowedRoom", query.ToPagedList(pageNumber, pageSize));
                }
                if (keyword != "" && keyword != null)
                {
                    var query = (from ls in data.LICHSUMUONPHONGs
                                 join phong in data.PHONGs on ls.IDPhong equals phong.ID
                                 join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                                 where docgia.Hoten.Contains(@keyword)
                                 && DateTime.Compare(ls.ThoiGianMuon, now).Equals(1)
                                 select new
                                 {
                                     HoTen = docgia.Hoten,
                                     tgmuon = ls.ThoiGianMuon,
                                     tgtra = ls.ThoiGianTra,
                                     phong = phong.ID
                                 }).OrderBy(a => a.tgmuon);
                    return View("GetListBorrowedRoom", query.ToPagedList(pageNumber, pageSize));
                }
                if (temp != "" && temp != null)
                {
                    var query = (from ls in data.LICHSUMUONPHONGs
                                 join phong in data.PHONGs on ls.IDPhong equals phong.ID
                                 join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                                 where DateTime.Compare(ls.ThoiGianMuon, date).Equals(1)
                                 select new
                                 {
                                     HoTen = docgia.Hoten,
                                     tgmuon = ls.ThoiGianMuon,
                                     tgtra = ls.ThoiGianTra,
                                     phong = phong.ID
                                 }).OrderBy(a => a.tgmuon);
                    return View("GetListBorrowedRoom", query.ToPagedList(pageNumber, pageSize));
                }

            }
            return View();
        }

    }
}
