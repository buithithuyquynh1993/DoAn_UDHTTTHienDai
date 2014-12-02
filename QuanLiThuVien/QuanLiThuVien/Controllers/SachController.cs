using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using QuanLiThuVien.Models;


namespace QuanLiThuVien.Controllers
{
    public class SachController : Controller
    {
        public static ObjectQuery<THONGTINMUONTRA> StrQuery_LayDsMuonTra(XmlElement node)
        {
            try
            {
                ObjectQuery<THONGTINMUONTRA> query;
                using (QuanLyThuVienEntities data = new QuanLyThuVienEntities())
                {
                    //Tạo câu truy vấn
                    //Câu truy vấn tương tự:
                    //@"SELECT VALUE Contact FROM AdventureWorksEntities.Contacts as Contact where Contact.LastName = @ln"

                    String sql = @"select VALUE thongtinmuon_tra from QuanLyThuVienEntities.THONGTINMUONTRAs as thongtinmuon_tra, QuanLyThuVienEntities.DOCGIAs as dg where thongtinmuon_tra.IDDocGia = dg.ID and dg.MHV_MSSV == '" + node.Attributes[0].Value + "'";
                    for (int i = 1; i < node.Attributes.Count; i++)
                    {
                        //if (i + 1 < node.Attributes.Count)
                        sql += " and ";
                        String name = node.Attributes[i].Name;
                        String value = node.Attributes[i].Value;
                        if (value.IndexOf('<') == 0) //Trương hợp ngày trả < ngày hệ thống => sách mượn đã quá hạn
                            sql += "thongtinmuon_tra." + name + " < '" + value.Substring(1, value.Length - 1) + "'";
                        else if (value.IndexOf('>') == 0) //Trương hợp ngày trả >= ngày hệ thống => sách mượn và chưa trả
                            sql += "thongtinmuon_tra." + name + " >= '" + value.Substring(1, value.Length - 1) + "'";
                    }

                    //Thực hiện truy vấn
                    var temp = (data as IObjectContextAdapter).ObjectContext;
                    query = temp.CreateQuery<THONGTINMUONTRA>(sql); //=> Phải thực hiện ép kiểu       
                }
                    //THONGTINMUONTRA tryii = query.First<THONGTINMUONTRA>();
                    
                return query;
            }
            catch (Exception)
            { return null; }
        }
        //
        // GET: /Sach/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult layDSNguoiMuon() {
            QuanLyThuVienEntities data = new QuanLyThuVienEntities();
            var result = from t in data.VIEW_BORROWERS select t;
            var tem = result.GetType();
            return View(result);
        }

        #region Chuc năng trả sách

        public THONGTINMUONTRA layThongTinMuonSach() { return null; }
        public decimal tinhTienTienPhatQuaHan() { return 0; }
        public decimal tinhTienTienPhatThem() { return 0; }
        public bool thuchienTraSach() { return false; }

        #endregion
    }
}

