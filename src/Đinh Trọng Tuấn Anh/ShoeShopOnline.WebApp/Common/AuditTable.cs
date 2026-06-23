using ShoeShopOnline.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopOnline.WebApp.Common
{
    public class AuditTable
    {
        public static void InsertAuditFields(object obj)
        {
            dynamic item = obj;
            item.IsDeleted = false;
            item.CreatedOn = DateTime.Now;
        }
        public static void UpdateAuditFields(object obj)
        {
            var user = new User();
            dynamic item = obj;
            item.ModifiedOn = DateTime.Now;
        }
    }
}
