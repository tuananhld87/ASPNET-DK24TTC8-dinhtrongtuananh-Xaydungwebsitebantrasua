using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopOnline.Model.ViewModel
{
    public class ProductViewModel
    {
        public Guid Id { set; get; }
        public string Images { set; get; }
        public string Name { set; get; }
        public decimal Code { get; set; }
        public decimal? Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int? Quantity { get; set; }
        public string MetaTitle { set; get; }
        public DateTimeOffset? CreatedDate { set; get; }
    }
}