using ShoeShopOnline.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopOnline.Model.Dao
{
    public class OrderDetailsDao
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetail.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
