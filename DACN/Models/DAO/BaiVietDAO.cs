using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class BaiVietDAO
    {
        DBContext db = new DBContext();
        public List<BaiViet> ListAll()
        {
            return db.BaiViets.ToList();
        }
        public BaiViet GetByID(int id)
        {
            return db.BaiViets.Find(id);
        }
    }
}