namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Nhóm")]
        public string GroupId { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn chưa nhập tên đăng nhập")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [StringLength(50)]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [StringLength(50)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Họ lót")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Display(Name = "Tên")]
        [StringLength(100)]
        public string LastName { get; set; }
        [Display(Name = "Họ và tên")]
        public string FullName => FirstName + ' ' + LastName;
        [Required(ErrorMessage = "Bạn chưa nhập email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Định dạng email không đúng.")]
        [Display(Name = "Email")]
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(100)]
        public string ResetPasswordCode { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày tạo")]
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(256)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày cập nhật")]
        public DateTimeOffset? ModifiedOn { get; set; }
        [StringLength(256)]
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsDeleted { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
