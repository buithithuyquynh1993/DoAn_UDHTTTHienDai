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

        public static List<THONGTINMUONTRA> StrQuery_LayDsMuonTra(XmlElement node)
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

        //
        // GET: /Sach/

        public ActionResult Index()
        {
            return View();
        }

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
