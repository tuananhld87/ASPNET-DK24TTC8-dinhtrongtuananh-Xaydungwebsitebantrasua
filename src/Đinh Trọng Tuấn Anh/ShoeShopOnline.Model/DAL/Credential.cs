namespace ShoeShopOnline.Model.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Credential")]
    [Serializable]
    public partial class Credential
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        [Display(Name = "Nhóm")]
        public string UserGroupId { get; set; }
        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        [Display(Name = "Quyền")]
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserGroup UserGroup { get; set; }

    }
}
