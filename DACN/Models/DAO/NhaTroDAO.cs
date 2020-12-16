using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class NhaTroDAO
    {
        DBContext dbContext = null;
        public NhaTroDAO()
        {
            dbContext = new DBContext();
        }
        public List<NhaTro> ListAll()
        {
            return dbContext.NhaTroes.ToList();
        }
    }
}