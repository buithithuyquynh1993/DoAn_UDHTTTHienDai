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
        public ActionResult ViewListClassTraining() // Xem danh sach cac lop tap huan
        {
            var result = from a in data.LOPTAPHUANs
                         select a;
            return View(result);
        }

        public ActionResult XemChiTietDatSach()
        {
            var result = from p in data.Temps
                         select p;
            return View(result);
        }

        public ActionResult update(FormCollection f)
        {
            int id = int.Parse(f["stt"]);
            Temp result = (from p in data.Temps
                           where p.stt == id
                           select p).First();
            result.NgayMuon = Convert.ToDateTime(f["NgayMuon"]);
            data.SaveChanges();
            return RedirectToAction("XemChiTietDatSach");
        }

        public ActionResult Edit(int id)
        {
            Temp result = (from p in data.Temps
                           where p.stt == id
                           select p).First();
            return View(result);
        }

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

        public ActionResult DatSach()
        {
            string str = Request.Params["btn2"];
            if (str == "Tìm Kiếm")
            {
                var chude = @Request["chude"];
                var tensach = @Request["tensach"];

                if (chude == "" && tensach == "")
                {
                    var query = (from tt in data.THONGTINMUONTRAs
                                 join s in data.SACHes on tt.IDSach equals s.ID
                                 join cd in data.CHUDEs on s.IDChuDe equals cd.ID
                                 where s.TrangThai != "Đặt"

                                 select new
                                 {
                                     TenSach = s.TenSach,
                                     ChuDe = cd.TenChuDe,
                                     MaSach = s.ID,
                                 });
                    return View("DatSach", query.ToList());
                }
                else
                {
                    if (tensach != "")
                    {

                        var query = (from tt in data.THONGTINMUONTRAs
                                     join s in data.SACHes on tt.IDSach equals s.ID
                                     join cd in data.CHUDEs on s.IDChuDe equals cd.ID
                                     where (s.TrangThai != "Đặt" && s.TenSach == tensach)

                                     select new
                                     {
                                         TenSach = s.TenSach,
                                         ChuDe = cd.TenChuDe,
                                         MaSach = s.ID,

                                     });
                        return View("DatSach", query.ToList());
                    }

                    if (chude != "")
                    {
                        var query = (from tt in data.THONGTINMUONTRAs
                                     join s in data.SACHes on tt.IDSach equals s.ID
                                     join cd in data.CHUDEs on s.IDChuDe equals cd.ID
                                     where (s.TrangThai != "Đặt" && cd.TenChuDe == chude)

                                     select new
                                     {
                                         TenSach = s.TenSach,
                                         ChuDe = cd.TenChuDe,
                                         MaSach = s.ID,
                                     });
                        return View("DatSach", query.ToList());
                    }
                    if (chude != "" && tensach != "")
                    {
                        var query = (from tt in data.THONGTINMUONTRAs
                                     join s in data.SACHes on tt.IDSach equals s.ID
                                     join cd in data.CHUDEs on s.IDChuDe equals cd.ID
                                     where (s.TrangThai != "Đặt" && cd.TenChuDe == chude)
                                     select new
                                     {
                                         TenSach = s.TenSach,
                                         ChuDe = cd.TenChuDe,
                                         MaSach = s.ID,
                                     });
                        return View("DatSach", query.ToList());
                    }
                }
            }
            return View("DatSach");
        }

        public ActionResult GiaHanMuonSach()
        {
            int id = 1;
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = (from p in data.THONGTINMUONTRAs
                          join s in data.SACHes on p.IDSach equals s.ID
                          where p.IDDocGia == id
                          select new
                          {
                              MaSach = s.ID,
                              TenSach = s.TenSach,
                              Tap = s.Tap,
                              Cuon = s.Cuon,
                              ThgMuon = p.NgayMuon,
                              ThgTra = p.HanTra,
                          });
            return View("GiaHanMuonSach", result.ToList());
        }

        public ActionResult saveDatSach(int id)
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            DateTime now = DateTime.Today;

            string str = Request.Params["btn2"];
            int idDocGia = 8;
            //int idDocGia = int.Parse(f["IDDocGia"]);
            var temp = (from tt in data.THONGTINMUONTRAs
                        where (tt.IDDocGia == idDocGia && tt.NgayTra == null)
                        group tt.IDDocGia by tt.IDDocGia into t
                        select new
                        {
                            // ID = tt.IDSach, 
                            ID = t.Key,
                            count = t.Count()
                        }).First();
            if (temp.count >= 2)
            {
                // đã đủ số sách mượn                  
                TempData["datsach"] = "2";
                return RedirectToAction("DatSach");
            }
            else
                if (temp.count == 1)
                    TempData["datsach"] = "1";


            THONGTINMUONTRA info = new THONGTINMUONTRA();
            info.IDDocGia = idDocGia;
            info.IDSach = id;
            info.NgayMuon = DateTime.Now;
            info.HanTra = DateTime.Now.AddDays(7);
            data.THONGTINMUONTRAs.Add(info);
            data.SaveChanges();
            return RedirectToAction("DatSach");
        }

        public ActionResult saveGiaHan(int id)
        {
            string str = Request.Params["btn1"];
            TempData["giahan"] = "1";
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            THONGTINMUONTRA query = (from tt in data.THONGTINMUONTRAs where tt.ID == id select tt).First();
            DateTime temp = DateTime.Parse(query.HanTra.ToString());
            query.GiaHan = true;
            query.HanTra = temp.AddDays(7);
            TempData["giahan"] = "0";
            data.SaveChanges();
            return RedirectToAction("GiaHanMuonSach");
        }

        public ActionResult ChinhSuaDatMuon()
        {
            int id = 1;
            var query = (from ls in data.THONGTINMUONTRAs
                         join s in data.SACHes on ls.IDSach equals s.ID
                         where ls.ID == id
                         select new
                         {
                             MaSach = ls.IDSach,
                             TenSach = s.TenSach,
                             ChuDe = s.IDChuDe,
                         });
            return View("ChinhSuaDatMuon", query.ToList());
        }

        public ActionResult saveChinhSuaDatMuon(FormCollection f, string Check1)
        {
            string str = Request.Params["btn3"];
            if (str == "Lưu")
            {
                var id = int.Parse(@Request["ID"].ToString());
                THONGTINMUONTRA query = (from ls in data.THONGTINMUONTRAs where ls.ID == id select ls).First();

                query.NgayMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
                data.SaveChanges();
            }
            return RedirectToAction("ChinhSuaDatMuon");
        }

        public ActionResult XoaDatMuon(int id)
        {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            THONGTINMUONTRA result = (from p in data.THONGTINMUONTRAs
                                      where p.ID == id
                                      select p).First();
            data.THONGTINMUONTRAs.Remove(result);
            data.SaveChanges();
            return RedirectToAction("ChinhSuaDatMuon");
        }
    }
}
