using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.Function
{
    public class FuncAccount
    {
        private DBContext db;
        public FuncAccount()
        {
            db = new DBContext();
        }
        public IQueryable<TaiKhoan> TaiKhoans
        {
            get { return db.TaiKhoans; }
        }
        public string Delete(string Username)
        {
            var dbEntry = db.TaiKhoans.Where(s => s.Username == Username).First();
            
            db.TaiKhoans.Remove(dbEntry);
            db.SaveChanges();
            return Username;
        }
        public string Insert(TaiKhoan model)
        {
            db.TaiKhoans.Add(model);
            db.SaveChanges();
            return model.Username;
        }
    }
}