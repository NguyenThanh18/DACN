using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DACN.Models
{
    public class RegisterModel
    {
        [Key]
        public long UserID { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]

        public string UserName { set; get; }

        public string FullName { set; get; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [Display(Name = "Email")]
        public string Email { set; get; }

        public string Phone { set; get; }
        public string GioiTinh { set; get; }
    }
}