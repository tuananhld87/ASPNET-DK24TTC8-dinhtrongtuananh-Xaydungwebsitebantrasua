namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên menu")]
        [StringLength(256)]
        [Display(Name = "Tên")]
        public string MenuName { get; set; }
        [Display(Name = "Liên kết")]
        [StringLength(256)]
        [Required(ErrorMessage = "Bạn chưa nhập liên kết")]
        public string Link { get; set; }
        [Display(Name = "Thứ tự")]
        public int? DisplayOrder { get; set; }
        [Display(Name = "Menu con")]
        public int? ParentId { get; set; }
        [Display(Name = "Kiểu sang trang")]
        [Required(ErrorMessage = "Bạn chưa nhập kiểu sang trang")]
        [StringLength(256)]
        public string Target { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày tạo")]
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(256)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày cập nhật")]
        public DateTimeOffset? ModifiedOn { get; set; }
        [StringLength(256)]
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsDeleted { get; set; }
    }
}
