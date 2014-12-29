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
        public ActionResult GetListBorrowedRoom(int? page, string key, string keydate)
        {
            try
            {
                DateTime now = DateTime.Today;
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                string keyword;
                string temp;
                if (page == null)
                {
                    keyword = @Request["keyword"];
                    temp = @Request["date"];
                }
                else
                {
                    keyword = key;
                    temp = keydate;
                }
                ViewBag.keyword = keyword;
                ViewBag.date = temp;
                if ((keyword == null && temp == null) || (keyword == "" && temp == ""))
                {
                    page = 1;
                    var query = (from ls in data.LICHSUMUONPHONGs
                                 join phong in data.PHONGs on ls.IDPhong equals phong.ID
                                 join docgia in data.DOCGIAs on ls.IDDocGia equals docgia.ID
                                 where ls.ThoiGianMuon > now
                                 select new
                                 {
                                     IDDocGia = docgia.ID,
                                     HoTen = docgia.Hoten,
                                     tgmuon = ls.ThoiGianMuon,
                                     tgtra = ls.ThoiGianTra,
                                     phong = phong.ID
                                 }).OrderByDescending(a => a.tgmuon);
                    return View("GetListBorrowedRoom", query.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    DateTime date = DateTime.Today;
                    if (temp != "" && temp != null)
                    {
                        string[] CatChuoi = temp.Split('/');
                        string ngaymuon = CatChuoi[1] + "/" + CatChuoi[0] + "/" + CatChuoi[2];
                        date = DateTime.Parse(ngaymuon);
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
                                         IDDocGia = docgia.ID,
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
                                     select new
                                     {
                                         IDDocGia = docgia.ID,
                                         HoTen = docgia.Hoten,
                                         tgmuon = ls.ThoiGianMuon,
                                         tgtra = ls.ThoiGianTra,
                                         phong = phong.ID
                                     }).OrderByDescending(a => a.tgmuon);
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
                                         IDDocGia = docgia.ID,
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
            catch (Exception)
            {
                return View();
            }
        }
        public ActionResult Instructions()
        {
            return View();
        }

    }
}
