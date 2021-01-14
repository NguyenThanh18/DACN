using DACN.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DACN.Models.DAO
{
    public class CommentDAO
    {
        DBContext db = new DBContext();
        public int Insert(Comment entity)
        {
            db.Comments.Add(entity);
            db.SaveChanges();
            return entity.IdComment;
        }
    }
}