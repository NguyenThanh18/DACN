using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DACN.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }
        public string RememberMe { get; set; }
    }
}