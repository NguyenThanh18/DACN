using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class QuanDAO
    {
        DBContext dbContext = null;
        public QuanDAO()
        {
            dbContext = new DBContext();
        }
        public List<Quan> ListAll()
        {
            return dbContext.Quans.ToList();
        }
    }
}