using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;

namespace QuanLiThuVien.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/  
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

        public ActionResult BorrowedBook()
        {
            return View();
        }
        public ActionResult SaveBorrowedBook()
        {
            // add borrowed book information 
            int IDSach = int.Parse(@Request["IDSach"]);
            THONGTINMUONTRA muontra = new THONGTINMUONTRA();
            muontra.IDDocGia = int.Parse(@Request["IDDocGia"].ToString());
            muontra.IDNhanVienNhan = int.Parse(@Request["IDNhanVien"].ToString());
            muontra.IDSach = IDSach;
            muontra.NgayMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
            muontra.NgayTra = DateTime.Parse(@Request["ThoiGianTra"].ToString());
            data.THONGTINMUONTRAs.Add(muontra);
            data.SaveChanges();

            // update status of book
            SACH sach = (from s in data.SACHes where s.ID == IDSach select s).First();
            sach.TrangThai = "Đang mượn";
            data.SaveChanges();
            return RedirectToAction("BorrowedBook");
        }
        public ActionResult BorrowedRoom()
        {
            return View();
        }
        public ActionResult SaveBorrowedRoom()
        {
            //QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            LICHSUMUONPHONG ls = new LICHSUMUONPHONG();
            ls.IDDocGia = int.Parse(@Request["IDDocGia"].ToString());
            ls.IDPhong = int.Parse(@Request["IDPhong"].ToString());
            ls.ThoiGianMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
            ls.ThoiGianTra = DateTime.Parse(@Request["ThoiGianTra"].ToString());
            data.LICHSUMUONPHONGs.Add(ls);
            data.SaveChanges();
            return RedirectToAction("BorrowedRoom");
        }

        #region Chuc năng trả sách

        public ActionResult layThongTinMuonSach(FormCollection f)
        {
            try
            {
                proc_layTTMuonSach_Result KQ = new proc_layTTMuonSach_Result();
                //QuanLyThuVienEntities data = new QuanLyThuVienEntities();
                String maDocGia = Request.Form["madocgia"];
                String maSach = Request.Form["masach"];
                var query = data.proc_layTTMuonSach(maSach);
                KQ = query.First();
                return View("TraSach", KQ);
            }
            catch (Exception)
            {
                return View("TraSach", null);
            }
        }
        public bool thuchienTraSach(FormCollection f){
            try
            {
                int? ID = int.Parse(f["IDPhieuMuon"]);
                int phat = int.Parse(f["tongphiphat"]);
                String lido = (f["lidophat"]);
                
                //QuanLyThuVienEntities data = new QuanLyThuVienEntities();
                THONGTINMUONTRA result = (from tt in data.THONGTINMUONTRAs where tt.ID == ID select tt).First();
                result.NgayTra = DateTime.Now;
                //if (phat != null)
                //{
                    BANPHAT bphat = new BANPHAT();
                    bphat.IDThongTinMuonTra = ID;
                    bphat.PhiPhat = phat;
                    bphat.LiDo = lido;
                    data.BANPHATs.Add(bphat);
                //}
                data.SaveChanges();
                return true;
            }
            catch (Exception)
            { return false; }
        }
        public ActionResult TraSach()
        {
            return View();
        }
        #endregion


        #region xem danh sách phiếu góp ý
        public ActionResult xemDSPhieuGopY()
        {
            //QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var query = from p in data.THUGOPies where p.DaXem == false select p;
            return View(query);
        }

        [HttpPost]
        public bool xacnhanDaXemGopY(FormCollection f)
        {
            try
            {
                var temp = f["checkGopY"].Replace(",false", "");
                string[] arrtem = temp.Split(',');
                //QuanLyThuVienEntities data = new QuanLyThuVienEntities();
                foreach (var i in arrtem)
                {
                    int iId = int.Parse(i);
                    THUGOPY query = (from p in data.THUGOPies where p.ID == iId select p).First();
                    query.DaXem = true;
                    data.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            { return false; }
            
        }
        #endregion

        public ActionResult layDSNguoiMuon()
        {
            //QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from t in data.VIEW_BORROWERS select t;
            var tem = result.GetType();
            return View(result);
        }
    }
}
