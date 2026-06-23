using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShoeShopOnline.Model.Dao
{
    public class ProductDao
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        public List<string> ListName(string keyword)
        {
            return db.Product.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public List<ProductViewModel> Search(string keyword)
        {
            var model = (from a in db.Product
                         join b in db.ProductCategory
                         on a.ProductCategoryId equals b.Id
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CreatedOn = a.CreatedOn,
                             Id = a.Id,
                             Images = a.UrlImage,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Code = a.Code,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice,
                             Quantity = a.Quantity
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CreatedDate = x.CreatedOn,
                             Id = x.Id,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Code = x.Code,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice,
                             Quantity = x.Quantity
                         });
            model.OrderByDescending(x => x.CreatedDate);
            return model.ToList();
        }

        public void setQuantity(Guid id, int? quantity)
        {
            Product product = db.Product.Find(id);
            product.Quantity -= quantity;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
