namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product
    {
        [Display(Name = "Tên sản phẩm")]
        public Guid Id { get; set; }

        [StringLength(256)]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Giá nhập kho")]
        public decimal Code { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên link liên kết")]
        [Display(Name = "Liên kết")]
        [StringLength(256)]
        public string MetaTitle { get; set; }
        [Display(Name = "Mô tả")]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "Ảnh")]
        [StringLength(256)]
        public string UrlImage { get; set; }
        [Display(Name = "Giá")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? PromotionPrice { get; set; }
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public Guid? ProductCategoryId { get; set; }
        [Column(TypeName = "ntext")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }
        [Display(Name = "Tên khóa")]
        [StringLength(256)]
        public string MetaKeywords { get; set; }
        [Display(Name = "Sản phẩm Hot")]
        public bool? TopHot { get; set; }
        [Display(Name = "Sản phẩm binh thường mức 1")]
        public bool? IsNormalProduct1 { get; set; }
        [Display(Name = "Sản phẩm bình thường mức 2")]
        public bool? IsNormalProduct2 { get; set; }
        [Display(Name = "Sản phẩm mới")]
        public bool? IsNewProduct { get; set; }
        [Display(Name = "Lượt xem")]
        public int? ViewCount { get; set; }
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
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
