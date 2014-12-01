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
                    String sql = @"select VALUE thongtinmuon_tra from QuanLyThuVienEntities.THONGTINMUONTRAs as thongtinmuon_tra where ";
                    for (int i = 0; i < node.Attributes.Count; i++)
                    {
                        String name = node.Attributes[i].Name;
                        String value = node.Attributes[i].Value;
                        if( value.IndexOf('<') == 0) //Trương hợp ngày trả < ngày hệ thống => sách mượn đã quá hạn
                            sql += "thongtinmuon_tra." + name + " < @nodeI" + i;
                        else if ( value.IndexOf('>') == 0) //Trương hợp ngày trả >= ngày hệ thống => sách mượn và chưa trả
                            sql += "thongtinmuon_tra." + name + " >= @nodeI" + i;
                        else
                            sql += "thongtinmuon_tra." + name + " = @nodeI" + i;
                        if (i + 1 < node.Attributes.Count)
                            sql += " and ";
                    }

                    //Thực hiện truy vấn
                    query = (data as IObjectContextAdapter).ObjectContext.CreateQuery<THONGTINMUONTRA>(sql); //=> Phải thực hiện ép kiểu               

                    for (int i = 0; i < node.Attributes.Count; i++)
                    {
                        String value = node.Attributes[i].Value;
                        if(value.IndexOf('<') == 0 || value.IndexOf('>') == 0)
                            value = value.Substring(1, value.Length - 1);
                        query.Parameters.Add(new ObjectParameter("nodeI" + i, value));
                    }
                }

                string aa = "select VALUE thongtinmuon_tra from QuanLyThuVienEntities.THONGTINMUONTRAs as thongtinmuon_tra where thongtinmuon_tra.MHV_MSSV = @nodeI0 and thongtinmuon_tra.HanTra >= @nodeI1";

                var tem = query.CommandText;
                //DbQuery query1 = query.Cast<DbQuery

                
                //foreach(THONGTINMUONTRA i in query1)
                //{
                //    var a = i.DOCGIA;
                //}
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

