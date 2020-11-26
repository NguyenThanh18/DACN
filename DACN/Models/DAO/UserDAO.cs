using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN.Common;
using DACN.Models.EF;

namespace DACN.Models.DAO
{
    public class UserDAO
    {
        DBContext db = new DBContext();
        public int Login(string userName, string password)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.Username == userName);
            if (result.RoleTK == CommonConstants.USER_GROUP)
            {
     
                    if (result.Pass == password)
                        return 1;
                    else
                        return -2;
                
            }
            else
            {
                return -3;
            }
        }
        public TaiKhoan GetByID(string name)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.Username == name);
        }
        public long Insert(TaiKhoan entity)
        {
            db.TaiKhoans.Add(entity);
            db.SaveChanges();
            return entity.idTK;
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