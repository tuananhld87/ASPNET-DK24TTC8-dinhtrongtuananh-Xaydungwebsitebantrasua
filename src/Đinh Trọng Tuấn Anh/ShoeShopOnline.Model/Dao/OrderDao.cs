using ShoeShopOnline.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopOnline.Model.Dao
{
    public class OrderDao
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        public Guid Insert(Order order)
        {
            order.Id = Guid.NewGuid();
            db.Order.Add(order);
            db.SaveChanges();
            return order.Id;
        }
        //Change order status
        public bool ChangeStatus(Guid? id)
        {
            var order = db.Order.Find(id);
            order.Status = !order.Status;
            db.SaveChanges();
            return order.Status;
        }
    }
}
