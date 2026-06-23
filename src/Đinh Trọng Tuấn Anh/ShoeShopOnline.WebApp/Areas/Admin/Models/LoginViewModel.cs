using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeShopOnline.WebApp.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập User Name")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập Password")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}