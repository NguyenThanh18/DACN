using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class LoaiBDSDAO
    {
        DBContext dbContext = null;
        public LoaiBDSDAO()
        {
            dbContext = new DBContext();
        }
        public List<LoaiBD> GetListByID(int id)
        {
            return dbContext.LoaiBDS.Where(x => x.idBDS == id).ToList();
        }
        public List<LoaiBD> ListAll()
        {
            return dbContext.LoaiBDS.ToList();
        }
    }
}