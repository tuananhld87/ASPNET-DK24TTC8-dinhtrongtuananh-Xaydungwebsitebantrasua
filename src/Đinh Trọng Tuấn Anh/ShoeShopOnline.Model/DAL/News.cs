namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("News")]
    public partial class News
    {
        public Guid Id { get; set; }
        [StringLength(256)]
        [Required(ErrorMessage = "Bạn chưa nhập tiêu Đề")]
        public string Name { get; set; }
        public string Summary { get; set; }
        [StringLength(256)]
        public string MetaTitle { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [StringLength(256)]
        public string UrlImage { get; set; }
        public Guid? NewsCategoryId { get; set; }
        public int? Warranty { get; set; }
        [StringLength(256)]
        public string MetaKeywords { get; set; }
        [StringLength(256)]
        public string MetaDescriptions { get; set; }
        public DateTime? TopHot { get; set; }
        public int? ViewCount { get; set; }
        [StringLength(256)]
        public string Tag { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày tạo")]
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(256)]
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? ModifiedOn { get; set; }
        [StringLength(256)]
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
