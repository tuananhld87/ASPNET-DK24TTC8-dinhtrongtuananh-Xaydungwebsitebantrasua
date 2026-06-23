namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập chọn hình ảnh")]
        [StringLength(256)]
        [Display(Name = "Hình ảnh")]
        public string UrlImage { get; set; }
        [Display(Name = "Thứ tự")]
        public int? DisplayOrder { get; set; }
        [Display(Name = "Liên kết")]
        [StringLength(256)]
        public string Link { get; set; }
        [Display(Name = "Mô tả")]
        [StringLength(256)]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày tạo")]
        public DateTimeOffset? CreatedOn { get; set; }
        [Display(Name = "Người tạo")]
        [StringLength(256)]
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày cập nhật")]
        public DateTimeOffset? ModifiedOn { get; set; }
        [Display(Name = "Người cập nhật")]
        [StringLength(256)]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsDeleted { get; set; }
    }
}
