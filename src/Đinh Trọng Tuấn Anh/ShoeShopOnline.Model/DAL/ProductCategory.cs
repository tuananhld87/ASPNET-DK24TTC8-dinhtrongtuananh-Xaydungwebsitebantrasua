namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategory()
        {
            Product = new HashSet<Product>();
        }

        public Guid Id { get; set; }

        [StringLength(256)]
        [Display(Name = "Loại sản phẩmm")]
        public string Name { get; set; }

        [StringLength(256)]
        [Display(Name = "Liên kết")]
        public string MetaTitle { get; set; }
        //[Display(Name = "Loa�i sa�n ph��m")]
        public int? ParentId { get; set; }
        [Display(Name = "STT")]
        public int? DisplayOrder { get; set; }
        [Display(Name = "Tiêu Đề SEO")]
        [StringLength(256)]
        public string SeoTitle { get; set; }
        [Display(Name = "Tên khoá")]
        [StringLength(256)]
        public string MetaKeywords { get; set; }
        [Display(Name = "Mô tả tên khóa")]
        [StringLength(256)]
        public string MetaDescriptions { get; set; }
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? CreatedOn { get; set; }
        [StringLength(256)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngà cập nhật")]
        public DateTimeOffset? ModifiedOn { get; set; }
        [StringLength(256)]
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
