using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;
using System.Net;
using System.Net.Mail;
using PagedList;
using PagedList.Mvc;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace QuanLiThuVien.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/  
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        public ActionResult ViewListClassTraining() // Xem danh sach cac lop tap huan
        {
            var result = from a in data.LOPTAPHUANs
                         select a;
            return View(result);
        }

        public ActionResult ViewClassDetail(int ID) // xem chi tiet lop tap huan
        {
            var result = from a in data.NGUOIDANGKies
                         where a.IDLopTapHuan == ID
                         select a;
            return View(result);
        }

        public ActionResult UpdateStatus(int ID) //  cap nhat trang thai hoc vien tham du
        {
            var result = from a in data.NGUOIDANGKies
                         where a.ID == ID
                         select a;

            return View(result);
        }

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
                                // where ls.ThoiGianMuon > now
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

        public ActionResult BorrowedBook()
        {
            return View();
        }
        public ActionResult SaveBorrowedBook()
        {
            try
            {
                int IDSach = int.Parse(@Request["IDSach"]);
                THONGTINMUONTRA muontra = new THONGTINMUONTRA();
                muontra.IDDocGia = int.Parse(@Request["IDDocGia"].ToString());
                muontra.IDNhanVienNhan = int.Parse(@Request["IDNhanVien"].ToString());
                muontra.IDSach = IDSach;
                string[] CatChuoi1 = @Request["ThoiGianMuon"].Split('/');
                string[] CatChuoi2 = @Request["ThoiGianTra"].Split('/');
                string NgayMuon = CatChuoi1[1] + "/" + CatChuoi1[0] + "/" + CatChuoi1[2];
                string NgayTra = CatChuoi2[1] + "/" + CatChuoi2[0] + "/" + CatChuoi2[2];

                muontra.NgayMuon = DateTime.Parse(NgayMuon.ToString());
                muontra.HanTra = DateTime.Parse(NgayTra.ToString());
                if (muontra.NgayMuon > muontra.HanTra || DateTime.Today > muontra.NgayMuon)
                {
                    TempData["insert"] = "2";
                    return RedirectToAction("BorrowedBook");
                }

                data.THONGTINMUONTRAs.Add(muontra);
                data.SaveChanges();

                // update status of book
                SACH sach = (from s in data.SACHes where s.ID == IDSach select s).First();
                sach.TrangThai = "Đang mượn";
                data.SaveChanges();
                TempData["insert"] = "1";
                return RedirectToAction("BorrowedBook");
            }
            catch (Exception)
            {
                TempData["insert"] = "0";
                return RedirectToAction("BorrowedBook");
            }
        }
        public ActionResult BorrowedRoom()
        {
            TempData["to"] = "";
            TempData["subject"] = "";
            return View();
        }
        public ActionResult SaveBorrowedRoom()
        {
            try
            {
                QuanLyThuVienEntities data = new QuanLyThuVienEntities();
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
        public ActionResult Mail(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            List<NGUOIDANGKY> query = (from nguoiDK in data.NGUOIDANGKies select nguoiDK).ToList();
            if (query.ToList().Count == 0)
            {
                TempData["list"] = "0";
            }
            else
                TempData["list"] = "1";

            if (@Request["yourmail"] == null && @Request["password"] == null)
            {
                return View(query);
            }
            else
            {
                var yourmail = @Request["yourmail"];
                var pass = @Request["password"];
                var to = @Request["to"];
                var subject = @Request["subject"];
                var content = @Request["content"];
                try
                {
                    string[] ListMail = to.Split(';');
                    for (int i = 0; i < ListMail.Count() - 1; i++)
                    {
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(@yourmail, @pass);

                        MailMessage mail = new MailMessage(@yourmail, @ListMail[i], @subject, @content);
                        smtp.Send(mail);
                    }
                    TempData["notice"] = "1";
                    return View("Mail", query);
                }
                catch (Exception)
                {
                    TempData["notice"] = "0";
                    TempData["to"] = to;
                    TempData["subject"] = subject;
                    TempData["content"] = content;
                    return View("Mail", query);
                }
            }
        }        



        #region Chuc năng trả sách

        public ActionResult layThongTinMuonSach(FormCollection f)
        {
            try
            {
                proc_layTTMuonSach_Result KQ = new proc_layTTMuonSach_Result();
                String maDocGia = Request.Form["madocgia"];
                String maSach = Request.Form["masach"];
                var query = data.proc_layTTMuonSach(maSach);
                KQ = query.First();
                return View("TraSach", KQ);
            }
            catch (Exception)
            {
                return View();
            }
        }
        public ActionResult thuchienTraSach(FormCollection f)
        {
            try
            {
                int? ID = int.Parse(f["IDPhieuMuon"]);
                int phat = int.Parse(f["tongphiphat"]);
                String lido = (f["lidophat"]);
                
                THONGTINMUONTRA result = (from tt in data.THONGTINMUONTRAs where tt.ID == ID select tt).First();
                result.NgayTra = DateTime.Now;
                BANPHAT bphat = new BANPHAT();
                bphat.IDThongTinMuonTra = ID;
                bphat.PhiPhat = phat;
                bphat.LiDo = lido;
                data.BANPHATs.Add(bphat);
                data.SaveChanges();
                return View("TraSach");
            }
            catch (Exception)
            {return View("TraSach"); }
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
        public ActionResult xacnhanDaXemGopY(FormCollection f)
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
                return RedirectToAction("xemDSPhieuGopY");
            }
            catch (Exception)
            { return RedirectToAction("xemDSPhieuGopY"); }
            
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
