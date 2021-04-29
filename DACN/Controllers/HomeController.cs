using DACN.Models;
using DACN.Models.DAO;
using DACN.Models.EF;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace DACN.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Home
        public ActionResult HomePage()
        {
            ViewBag.listBaiViet = new BaiVietDAO().ListAll();
            ViewBag.Quan = db.Quans.ToList();
            ViewBag.listKieuBDS = db.KieuBDS.ToList();
            ViewBag.listLoaiBDS = db.LoaiBDS.ToList();
            ViewBag.Phuong = db.Phuongs.SqlQuery("select distinct * from Phuong").ToList();
            ViewBag.listnew = db.BaiViets.SqlQuery("select * from BaiViet where NgayDang >= GETDATE()").ToList();
            return View();
        }
        public ActionResult ListKieuBDS()
        {
            ViewBag.listKieuBDS = new KieuBDSDAO().ListAll();
            return PartialView();
        }
        public JsonResult ListAllPhuong()
        {
            var listPhuong = new PhuongDAO().ListAll();
            return Json(listPhuong, JsonRequestBehavior.AllowGet);
        }
        public JsonResult listPhuong(string name)
        {
            var listPhuong = db.Phuongs.ToList();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(listPhuong, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListPhuongByID(int id)
        {
            var listPhuong = new PhuongDAO().GetListByID(id);
            return Json(listPhuong, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListPhuongByName(string name)
        {
            var listAllPhuong = new PhuongDAO().ListAll();
            var listResult = new List<List<string>>();
            name = name.ToUpper();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                name = name.Replace(convert[i], To[i]);
            }
            foreach (var item in listAllPhuong)
            {
                for (int i = 0; i < To.Length; i++)
                {
                    item.TenPhuong = item.TenPhuong.Replace(convert[i], To[i]);
                }
                foreach (var phuong in listAllPhuong)
                {
                    if (phuong.idPhuong == item.idPhuong)
                    {
                        listResult.Add(new List<string>
                        {
                            phuong.idPhuong.ToString(),
                            phuong.TenPhuong
                        });
                    }
                }
            }
            return Json(listResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListQuanByName(string name)
        {
            var listAllQuan = new QuanDAO().ListAll();
            var listAllPhuong = new PhuongDAO().ListAll();
            var listResult = new List<List<string>>();
            name = name.ToUpper();
            string convert = "ĂÂÀẰẦÁẮẤẢẲẨÃẴẪẠẶẬỄẼỂẺÉÊÈỀẾẸỆÔÒỒƠỜÓỐỚỎỔỞÕỖỠỌỘỢƯÚÙỨỪỦỬŨỮỤỰÌÍỈĨỊỲÝỶỸỴĐăâàằầáắấảẳẩãẵẫạặậễẽểẻéêèềếẹệôòồơờóốớỏổởõỗỡọộợưúùứừủửũữụựìíỉĩịỳýỷỹỵđ";
            string To = "AAAAAAAAAAAAAAAAAEEEEEEEEEEEOOOOOOOOOOOOOOOOOUUUUUUUUUUUIIIIIYYYYYDaaaaaaaaaaaaaaaaaeeeeeeeeeeeooooooooooooooooouuuuuuuuuuuiiiiiyyyyyd";
            for (int i = 0; i < To.Length; i++)
            {
                name = name.Replace(convert[i], To[i]);
            }
            foreach (var item in listAllPhuong)
            {
                item.TenPhuong = item.TenPhuong.ToUpper();
                for (int i = 0; i < To.Length; i++)
                {
                    item.TenPhuong = item.TenPhuong.Replace(convert[i], To[i]);
                }
                if (item.TenPhuong == name)
                {
                    foreach (var quan in listAllQuan)
                    {
                        if (quan.idQuan == item.idQuan)
                        {
                            listResult.Add(new List<string>
                            {
                                quan.idQuan.ToString(),
                                quan.TenQuan
                            });
                        }
                    }
                }

            }
            var list = listResult;
            return Json(listResult, JsonRequestBehavior.AllowGet);
        }
        static string NullToString(string Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();

        }
        public JsonResult Search(SearchModel entity)
        {
            List<BaiViet> listBaiViet = new List<BaiViet>();
            var res = db.BaiViets.ToList();
            //Search Quan
            Quan q = db.Quans.Find(entity.idQuan);
            Phuong p = db.Phuongs.Find(entity.idPhuong);
            KieuBD k = db.KieuBDS.Find(entity.idBDS);
            LoaiBD l = db.LoaiBDS.Find(entity.idLoaiBDS);

            string temp = "";
            if (q == null) temp = "";
            else temp = q.TenQuan;
            if (p == null) temp = temp;
            else temp += " " + p.TenPhuong;
            if (l == null) temp = temp;
            else temp += " " + l.TenLoai;
            if (k == null) temp = temp;
            else temp += " " + k.TenKieu;

            var listNT = db.NhaTroes.SqlQuery("select * from NhaTro n where n.SoNha like N'%" + temp + "%'").ToList();
            foreach (var item in res)
            {
                foreach (var item1 in listNT)
                {
                    if (item.idNT == item1.idNT)
                        listBaiViet.Add(item);
                }
            }
            //string tempJ = string.Empty;
            //tempJ = JsonConvert.SerializeObject(listBaiViet, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            return Json(listBaiViet, JsonRequestBehavior.AllowGet);
        }
    }
}
