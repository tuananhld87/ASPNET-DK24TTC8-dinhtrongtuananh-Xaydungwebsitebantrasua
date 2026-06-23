using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeShopOnline.WebApp.Models
{
    public class RegisterModel
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ lót")]
        [Display(Name = "Họ lót")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [Display(Name = "Tên")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Định dạng email không đúng.")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}