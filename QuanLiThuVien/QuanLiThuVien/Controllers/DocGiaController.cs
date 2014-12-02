using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using QuanLiThuVien.Models;
using ProcessRootXML;

namespace QuanLiThuVien.Controllers
{
    public class DocGiaController : Controller
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

            if (value != null)
            {
                //Node thể hiện cho từng giá trị tùy chọn
                //XmlElement node = ProcessRoot.CreateNode("NODE", "MHV_MSSV", ""); // "": đưa mã đọc giả vào, dùng session
                XmlElement node = ProcessRoot.CreateNode("NODE", "MHV_MSSV", "0944873");

                switch (value)
                {
                    //Trường hợp xem sách chưa mượn
                    case "SachChuaTra": //Có hạn trả >= ngày hệ thống
                        node.SetAttribute("HanTra", ">" + DateTime.Now.ToString());
                        break;
                    //Trường hợp mượn sách quá hạng
                    case "SachQuaHan": //Có hạn trả < ngày hệ thống
                        node.SetAttribute("HanTra", "<" + DateTime.Now.ToString());
                        break;
                    //Mặc định là trường hợp chọn tất cả
                    default:
                        break;
                }
                var kq = SachController.StrQuery_LayDsMuonTra(node);
                return View(kq);    
                
            }
            return View();
        }
        public ActionResult SaveBorrowedRoom()
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
            return RedirectToAction("BorrowedRoom");
        }
    }
}
