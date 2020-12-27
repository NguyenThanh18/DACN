using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace DACN.Models.DAO
{
    public class AdminDao
    {
        DBContext db = new DBContext();

        public int Login(string userName, string password)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.Username == userName);
            if (result == null)
                return 0;
            else if (result.RoleTK != "admin")
                return -3;
            else if (result.Pass == password)
                return 1;

            else
            {
                if (result.Pass != password)
                    return -2;
                else return -1;
            }
        }

        public List<TaiKhoan> ListAdmin()
        {
            return db.TaiKhoans.ToList();
        }
        public int Insert(TaiKhoan entity)
        {
            db.TaiKhoans.Add(entity);
            db.SaveChanges();
            return entity.idTK;
        }

        //Update
        public bool Update(TaiKhoan entity)
        {
            try
            {
                var admin = db.TaiKhoans.Find(entity.idTK);
                admin.HoTen = entity.HoTen;
                if (!string.IsNullOrEmpty(entity.Pass))
                {
                    admin.Pass = entity.Pass;
                }
                admin.Email = entity.Email;
                admin.SDT = entity.SDT;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //login
                return false;
            }

        }

        public TaiKhoan ViewDetail(int id)
        {
            return db.TaiKhoans.Find(id);
        }

        public long InsertForFacebook(TaiKhoan entity)
        {
            var user = db.TaiKhoans.SingleOrDefault(x => x.Username == entity.Username);
            if (user == null)
            {
                db.TaiKhoans.Add(entity);
                db.SaveChanges();
                return entity.idTK;
            }
            else
            {
                return user.idTK;
            }

        }
        public TaiKhoan GetByID(string name)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.Username == name);
        }

        public bool CheckUserName(string userName)
        {
            return db.TaiKhoans.Count(x => x.Username == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.TaiKhoans.Count(x => x.Email == email) > 0;
        }
    }
}