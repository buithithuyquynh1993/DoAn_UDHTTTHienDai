using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLiThuVien
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Sach", action = "layDSNguoiMuon", id = UrlParameter.Optional }
<<<<<<< HEAD
                defaults: new { controller = "Public", action = "GetListBorrowedRoom", id = UrlParameter.Optional }
=======
                defaults: new { controller = "Docgia", action = "layDsMuonTra", id = UrlParameter.Optional }
>>>>>>> be5bbc365d4ece2f3c2c3da0074e16b843dc47fb
            );
        }
    }
}