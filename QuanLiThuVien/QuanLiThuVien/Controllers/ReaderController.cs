﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using QuanLiThuVien.Models;
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
        public ActionResult GetListBorrowedRoom(int? page, string key, string keydate)
        {
            DateTime now = DateTime.Today;
            int pageSize = 2;
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
                             }).OrderByDescending (a => a.tgmuon);
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
                                     id = ls.ID,
                                     idDocGia = ls.IDDocGia,
                                     HoTen = docgia.Hoten,
                                     tgmuon = ls.ThoiGianMuon,
                                     tgtra = ls.ThoiGianTra,
                                     phong = phong.ID
                                 }).OrderByDescending(a => a.tgmuon);
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
                                 }).OrderByDescending(a => a.tgmuon);
                    return View("GetListBorrowedRoom", query.ToPagedList(pageNumber, pageSize));
                }

            }
            return View();
        }
        public ActionResult save(FormCollection f)
        {
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
            return RedirectToAction("add");
        }

        public ActionResult add()
        {
            return View();
        }

        private static List<THONGTINMUONTRA> StrQuery_LayDsMuonTra(XmlElement node)
        {
            try
            {
                List<THONGTINMUONTRA> KQ = new List<THONGTINMUONTRA>();
                using (QuanLyThuVienEntities data = new QuanLyThuVienEntities())
                {
                    //Tạo câu truy vấn
                    //Câu truy vấn tương tự:

                    String sql = @"select VALUE thongtinmuon_tra from QuanLyThuVienEntities.THONGTINMUONTRAs as thongtinmuon_tra, QuanLyThuVienEntities.DOCGIAs as dg where thongtinmuon_tra.IDDocGia = dg.ID and dg.MHV_MSSV == '" + node.Attributes[0].Value + "'";
                    for (int i = 1; i < node.Attributes.Count; i++)
                    {
                        //if (i + 1 < node.Attributes.Count)
                        sql += " and ";
                        String name = node.Attributes[i].Name;
                        String value = node.Attributes[i].Value;
                        sql += "thongtinmuon_tra." + name + value;
                    }

                    //Thực hiện truy vấn
                    var temp = (data as IObjectContextAdapter).ObjectContext;
                    ObjectQuery<THONGTINMUONTRA> query = temp.CreateQuery<THONGTINMUONTRA>(sql); //=> Phải thực hiện ép kiểu    
                    foreach (THONGTINMUONTRA i in query)
                        KQ.Add(i);

                    return KQ;
                }
            }
            catch (Exception)
            { return null; }
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
                ls.ThoiGianMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
                ls.ThoiGianTra = DateTime.Parse(@Request["ThoiGianTra"].ToString());
                var test = (from lsTest in data.LICHSUMUONPHONGs
                            where ls.ThoiGianMuon > lsTest.ThoiGianMuon
                                  && lsTest.ThoiGianTra > ls.ThoiGianMuon
                            select lsTest);

                //if (DateTime.Compare(ls.ThoiGianMuon, ls.ThoiGianTra).Equals(1) || DateTime.Compare(DateTime.Today, ls.ThoiGianMuon).Equals(1))
                if (ls.ThoiGianMuon > ls.ThoiGianTra || DateTime.Today > ls.ThoiGianMuon)
                {
                    TempData["insert"] = "2";
                    return RedirectToAction("BorrowedRoom");
                }
                data.LICHSUMUONPHONGs.Add(ls);
                data.SaveChanges();
                TempData["insert"] = "1";
                return RedirectToAction("BorrowedRoom");
            }
            catch(Exception)
            {
                TempData["insert"] = "0";
                return RedirectToAction("BorrowedRoom");
            }
        }
        public ActionResult UpdateBrrowedRoom( int id)
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
            catch(Exception)
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
