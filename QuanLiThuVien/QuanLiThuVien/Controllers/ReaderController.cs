﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using QuanLiThuVien.Models;
using ProcessRootXML;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace QuanLiThuVien.Controllers
{
    public class ReaderController : Controller
    {
        //
        // GET: /DocGia/

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

        public ActionResult BorrowedRoom()
        {
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
                ls.ThoiGianMuon = DateTime.Parse(@Request["ThoiGianMuon"].ToString());
                ls.ThoiGianTra = DateTime.Parse(@Request["ThoiGianTra"].ToString());
                data.LICHSUMUONPHONGs.Add(ls);
                data.SaveChanges();
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Thêm thành công!')</SCRIPT>");
                TempData["insert"] = "1";
                return RedirectToAction("BorrowedRoom");
            }
            catch(Exception)
            {
                TempData["insert"] = "0";
                return RedirectToAction("BorrowedRoom");
            }
        }
    }
}
