using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models
{
    public class CommentModel
    {
        public string UserName { get; set; }
        public DateTime DateComment { get; set; }
        public string ContentComment { get; set; }
    }
}