using DACN.Models.DAO;
using DACN.Models.EF;
using DACN.Models.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACN.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XoaBaiViet()
        {
            string id = RouteData.Values["id"].ToString();
            // Trước khi xóa bài viết sau đó xóa nhà trọ
            var bv = db.BaiViets.Find(Int32.Parse(id));
            var nv = db.NhaTroes.Find(bv.idNT);
            var HamP = new FunctionPosts();
            var HamNT = new FunctionNhaTro();
            HamP.Delete(Int32.Parse(id));
            HamNT.Delete(nv.idNT);

            return RedirectToAction("QLBV", "Home");
        }
        public ActionResult XoaBaoCao()
        {
            string id = RouteData.Values["id"].ToString();
            // Trước khi xóa bài viết sau đó xóa nhà trọ
            var bc = db.BaoCaos.Find(Int32.Parse(id));
            var bv = db.BaiViets.Find(bc.idBV);
            var nt = db.NhaTroes.Find(bv.NhaTro);
            var HamP = new FunctionPosts();
            var HamNT = new FunctionNhaTro();
            var HamBC = new ReportDao();
            HamBC.Delete(Int32.Parse(id));
            HamP.Delete(bv.idBV);
            HamNT.Delete(nt.idNT);

            return RedirectToAction("QLBC", "Home");
        }
    }
}