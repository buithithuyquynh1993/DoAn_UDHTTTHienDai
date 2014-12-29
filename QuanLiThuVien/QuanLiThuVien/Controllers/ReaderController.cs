using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using QuanLiThuVien.Models;
//using ProcessRootXML;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using PagedList.Mvc;
using PagedList;

namespace QuanLiThuVien.Controllers
{
    public class ReaderController : Controller
    {
        //
        // GET: /DocGia/
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        public ActionResult save(FormCollection f)
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            DOCGIA p = new DOCGIA();
            p.Hoten = f["Hoten"];
            p.MHV_MSSV = f["MHV_MSSV"];
            p.Email = f["Email"];
            p.CMND = f["CMND"];
            p.DiaChi = f["DiaChi"];
            p.Truong = f["Truong"];
            p.Khoa = f["Khoa"];
            data.DOCGIAs.Add(p);
            data.SaveChanges();
           // data.SaveChanges();
            return RedirectToAction("add");
        }

        public ActionResult add()
        {
            return View();
        }

        #region Lấy thông tin mượn trả

        public ActionResult layDsMuonTra(FormCollection f)
        {
            
            //Lấy giá trị form
            String value = Request.Form["radTuyChon"];
            if (value == null) value = "tatca";

            using (QuanLyThuVienEntities data = new QuanLyThuVienEntities())
            {
                switch (value)
                {
                    //Trường hợp xem sách chưa mượn
                    case "SachChuaTra": //Có hạn trả >= ngày hệ thống
                        var query1 = data.proc_layDSMuonTra("0944873", 1);
                        return View(query1);    
                    //Trường hợp mượn sách quá hạng
                    case "SachQuaHan": //Có hạn trả < ngày hệ thống
                        var query2 = data.proc_layDSMuonTra("0944873", 2);
                        return View(query2);   
                    //Mặc định là trường hợp chọn tất cả
                    default:
                        var query = data.proc_layDSMuonTra("0944873", 0);
                        return View(query);  
                }
            }
        }

        #endregion

        #region Góp ý
        public ActionResult GopY()
        {
            return View();
        }
        public ActionResult luuGopY(FormCollection f)
        {
            try
            {
                String noidungGopY = Request["noidungGopY"];

                QuanLyThuVienEntities data = new QuanLyThuVienEntities();
                THUGOPY gopy = new THUGOPY();
                gopy.NgayGopY = DateTime.Now;
                gopy.NoiDung = noidungGopY;
                data.THUGOPies.Add(gopy);
                data.SaveChanges();

                return View("GopY", gopy);
            }
            catch (Exception) {
                return View("GopY", null);
            }
        }
        #endregion
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
                                 select new
                                 {
                                     id = ls.ID,
                                     idDocGia = ls.IDDocGia,
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
                                         id = ls.ID,
                                         idDocGia = ls.IDDocGia,
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
                                         id = ls.ID,
                                         idDocGia = ls.IDDocGia,
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
                                         id = ls.ID,
                                         idDocGia = ls.IDDocGia,
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
        public ActionResult BorrowedRoom()
        {
            return View();
        }
        public ActionResult SaveBorrowedRoom()
        {
            try
            {
                LICHSUMUONPHONG ls = new LICHSUMUONPHONG();
                ls.IDDocGia = int.Parse(@Request["IDDocGia"].ToString());
                ls.IDPhong = int.Parse(@Request["IDPhong"].ToString());

                string[] CatChuoi = @Request["NgayMuon"].Split('/');
                string ngaymuon = CatChuoi[1] + "/" + CatChuoi[0] + "/" + CatChuoi[2];
                var ThoiGianMuon = ngaymuon + " " + @Request["GioMuon"].ToString();
                var ThoiGianTra = ngaymuon + " " + @Request["GioTra"].ToString();

                ls.ThoiGianMuon = DateTime.Parse(ThoiGianMuon.ToString());
                ls.ThoiGianTra = DateTime.Parse(ThoiGianTra.ToString());

                //if (DateTime.Compare(ls.ThoiGianMuon, ls.ThoiGianTra).Equals(1) || DateTime.Compare(DateTime.Today, ls.ThoiGianMuon).Equals(1))
                if (ls.ThoiGianMuon > ls.ThoiGianTra || DateTime.Today > ls.ThoiGianMuon)
                {
                    TempData["insert"] = "2";
                    return RedirectToAction("BorrowedRoom");
                }
                var test = (from lsTest in data.LICHSUMUONPHONGs
                            where ls.ThoiGianMuon > lsTest.ThoiGianMuon
                                  && lsTest.ThoiGianTra > ls.ThoiGianMuon
                            select lsTest);

                data.LICHSUMUONPHONGs.Add(ls);
                data.SaveChanges();
                TempData["insert"] = "1";
                return RedirectToAction("BorrowedRoom");
            }
            catch (Exception)
            {
                TempData["insert"] = "0";
                return RedirectToAction("BorrowedRoom");
            }
        }
        public ActionResult UpdateBrrowedRoom(int id)
        {
            LICHSUMUONPHONG query = (from ls in data.LICHSUMUONPHONGs where ls.ID == id select ls).First();
            return View("UpdateBrrowedRoom", query);
        }
        public ActionResult SaveUpdateRoom()
        {
            try
            {
                var id = int.Parse(@Request["ID"].ToString());
                LICHSUMUONPHONG query = (from ls in data.LICHSUMUONPHONGs where ls.ID == id select ls).First();
                query.ThoiGianMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
                query.ThoiGianTra = DateTime.Parse(@Request["ThoiGianTra"].ToString());
                data.SaveChanges();
                TempData["update"] = "1";
                return RedirectToAction("GetListBorrowedRoom");
            }
            catch (Exception)
            {
                TempData["update"] = "0";
                return RedirectToAction("GetListBorrowedRoom");
            }

        }

        public ActionResult DeleteBrrowedRoom(int id)
        {
            LICHSUMUONPHONG query = (from ls in data.LICHSUMUONPHONGs where ls.ID == id select ls).First();
            query.delete_flag = "1";
            data.SaveChanges();
            return RedirectToAction("GetListBorrowedRoom");
        }
    }
}
