using System;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using OnBarcode.Barcode.BarcodeScanner;
using OnBarcode.Barcode;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiThuVien.Models;

namespace QuanLiThuVien.Controllers
{
    public class BarCodeController : Controller
    {
        //
        // GET: /Home/
        string url = "~/Images/Barcode.gif";
       // string url;
        //string url = "./BarCode/Images/BarCode.gif";
         public ActionResult Index()
        {
            QuanLyThuVienEntities myDB = new QuanLyThuVienEntities();
            var query = from dg in myDB.DOCGIAs
                        group dg by dg.ID into docgia
                        select docgia.Max(x => x.ID);
            string hoten = "BuiThiThuyQuynh";
            //var parameter = "I am"hoten;
            var result = query.First();
            ViewBag.message = result;
            return View();
            
        }
        
        public ActionResult SetBarCode()
        {
            
            string IDCode = Request["IDCode"];
            string TypeCode = Request["TypeCode"];
            string TypeSave  = Request["submit"];
            Generator(IDCode, TypeCode, url);
            return View();
            
        }
        void Generator(string data,string typeCode, string url)
        {
            string key =typeCode;
            TypeCode(typeCode, data, url);
        }
        private static void TypeCode(string typecode,string data, string url)
        {
            //http://www.onbarcode.com/csharp/isbn-generator.html
            Linear barcode = new Linear();
            barcode.Data = data;
            if (typecode == "Code128")
            {
                barcode.Type = OnBarcode.Barcode.BarcodeType.CODE128;
                barcode.CodabarStartChar = CodabarStartStopChar.A;
                barcode.CodabarStopChar = CodabarStartStopChar.A;
            }
            else
                if (typecode == "ISBN")
                    barcode.Type = OnBarcode.Barcode.BarcodeType.ISBN;
                else
                {
                    barcode.Type = OnBarcode.Barcode.BarcodeType.CODABAR;
                    barcode.CodabarStartChar = CodabarStartStopChar.A;
                    barcode.CodabarStopChar = CodabarStartStopChar.A;
                }

            barcode.UOM = UnitOfMeasure.PIXEL;
            barcode.X = 1;
            barcode.Y = 80;
            barcode.LeftMargin = 20;
            barcode.RightMargin = 20;
            barcode.TopMargin = 0;
            barcode.BottomMargin = 0;
            barcode.Resolution = 96;
            barcode.Rotate = Rotate.Rotate0;

            barcode.ShowText = true;
            barcode.TextFont = new  Font("Arial", 9f, FontStyle.Regular);
            barcode.TextMargin = 6;
            barcode.Format = System.Drawing.Imaging.ImageFormat.Gif;

            barcode.drawBarcode(url);
        }
        private static void CODABAR(string data, string url)
        {
            Linear barcode = new Linear();
            barcode.Type = OnBarcode.Barcode.BarcodeType.CODABAR;
            barcode.Data = data;

            barcode.CodabarStartChar = CodabarStartStopChar.A;
            barcode.CodabarStopChar = CodabarStartStopChar.A;

            barcode.UOM = UnitOfMeasure.PIXEL;
            barcode.X = 1;
            barcode.Y = 80;
            barcode.LeftMargin = 0;
            barcode.RightMargin = 0;
            barcode.TopMargin = 0;
            barcode.BottomMargin = 0;
            barcode.Resolution = 96;
            barcode.Rotate = Rotate.Rotate0;

            // Codabar Text Settings
            barcode.ShowText = true;
            barcode.TextFont = new Font("Arial", 9f, FontStyle.Regular);
            barcode.TextMargin = 6;

            // Image format setting
            barcode.Format = System.Drawing.Imaging.ImageFormat.Gif;

            barcode.drawBarcode(url);

        }
        private static void Code128(string data, string url)
        {
            Linear barcode = new Linear();
            barcode.Type = OnBarcode.Barcode.BarcodeType.CODE128;
            barcode.Data = data;
            barcode.CodabarStartChar = CodabarStartStopChar.A;
            barcode.CodabarStopChar = CodabarStartStopChar.A;

            barcode.UOM = UnitOfMeasure.PIXEL;
            barcode.X = 1;
            barcode.Y = 80;
            barcode.LeftMargin = 0;
            barcode.RightMargin = 0;
            barcode.TopMargin = 0;
            barcode.BottomMargin = 0;
            barcode.Resolution = 96;
            barcode.Rotate = Rotate.Rotate0;

            barcode.ShowText = true;
            barcode.TextFont = new Font("Arial", 9f, FontStyle.Regular);
            barcode.TextMargin = 6;

            barcode.Format = System.Drawing.Imaging.ImageFormat.Gif;

            barcode.drawBarcode(url);
        }

    }
}
