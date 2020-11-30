using DACN.Common;
using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DACN.Models.Function
{
    public class FunctionAccount
    {
        private DBContext db;
        public FunctionAccount()
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
        public string Update(TaiKhoan model, int id)
        {
            TaiKhoan dbEntry = db.TaiKhoans.Find(id);
            if (dbEntry == null)
            {
                return null;
            }
            dbEntry.Username = model.Username;
            dbEntry.Pass = Encryptor.MD5Hash(model.Pass);
            dbEntry.HoTen = model.HoTen;
            dbEntry.SDT = model.SDT;
            dbEntry.Email = model.Email;
            dbEntry.RoleTK = model.RoleTK;
            dbEntry.TrangThai = model.TrangThai;
            db.SaveChanges();
            return model.Username;
        }
    }
}