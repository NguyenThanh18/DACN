using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN.Common;
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
    }
}