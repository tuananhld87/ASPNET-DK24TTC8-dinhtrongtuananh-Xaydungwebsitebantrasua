namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    [Table("About")]
    public partial class About
    {
        public Guid Id { get; set; }
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề")]
        [StringLength(255)]
        public string Title { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(50)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [Display(Name = "Ngày cập nhật")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? ModifiedOn { get; set; }
        [StringLength(50)]
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsDeleted { get; set; }
    }
}
